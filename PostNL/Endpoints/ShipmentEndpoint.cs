using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ImageMagick;
using Newtonsoft.Json;
using PostNLApi.Models.Request;
using PostNLApi.Models.Response;
using Group = PostNLApi.Models.Request.Group;

namespace PostNLApi.Endpoints
{
    public class ShipmentEndpoint
    {
        private readonly PostNLClient _client;

        public ShipmentEndpoint(PostNLClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Generate a label for a shipment
        /// </summary>
        /// <param name="shipment">The shipment to register</param>
        /// <param name="confirm">
        /// With the confirm boolean in the request, you can determine if you want to confirm the shipment in the same call or not. If the Boolean variable is true the
        /// shipment will be preannounced. If this is set to false, then an additional Confirming API request needs to be made.
        /// </param>
        /// <param name="multicollo">Whether to generate a multicollo label. If set to true, all shipments will be combined into a multicollo if possible</param>
        /// <param name="labelSignature">Image of signature to use to sign customs form</param>
        public async Task<LabelResponse> GenerateLabel(Shipment shipment, bool confirm = true, bool multicollo = false, byte[] labelSignature = null)
        {
            return await GenerateLabel(new[] { shipment }, confirm, multicollo, labelSignature);
        }

        /// <summary>
        /// Generate a label for a shipment
        /// </summary>
        /// <param name="shipments">The shipments to register</param>
        /// <param name="confirm">
        /// With the confirm boolean in the request, you can determine if you want to confirm the shipment in the same call or not. If the Boolean variable is true the
        /// shipment will be preannounced. If this is set to false, then an additional Confirming API request needs to be made.
        /// </param>
        /// <param name="multicollo">Whether to generate a multicollo label. If set to true, all shipments will be combined into a multicollo if possible</param>
        /// <param name="labelSignature">Image of signature to use to sign customs form</param>
        public async Task<LabelResponse> GenerateLabel(IEnumerable<Shipment> shipments, bool confirm = true, bool multicollo = false, byte[] labelSignature = null)
        {
            var request = new LabelRequest
            {
                Shipments = shipments.ToList(),
                Confirm = confirm,
                MultiCollo = multicollo,
                LabelSignature = labelSignature,
                Customer = new Customer
                {
                    CustomerCode = _client.CustomerCode,
                    CustomerNumber = _client.CustomerNumber
                },
            };

            var multiColloCountries = new[] { "NL", "BE" };
            var insideCountries = request.Shipments.All(x => x.Addresses.All(y => multiColloCountries.Contains(y.CountryCode)));

            // if not eligible for multicollo, just send the request
            if (!request.MultiCollo || !insideCountries || request.Shipments.Count == 1)
            {
                return await SendRequests(request);
            }

            var mainBarcode = await _client.Barcode.GenerateBarcode();

            for (var i = 0; i < request.Shipments.Count; i++)
            {
                var shipment = request.Shipments[i];
                shipment.Groups = new[]
                {
                    new Group
                    {
                        GroupType = "03",
                        MainBarcode = mainBarcode,
                        GroupCount = request.Shipments.Count,
                        GroupSequence = i + 1
                    }
                };
            }

            return await SendRequests(request);
        }

        /// <summary>
        /// Helper function that sends the requests to the API. This function is used to split up the requests into smaller requests if needed.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ValidationException"></exception>
        private async Task<LabelResponse> SendRequests(LabelRequest labelRequest)
        {
            SanitizeRequest(labelRequest);
            ValidateRequest(labelRequest);

            // split up the shipments into separate requests. add 4 shipments per request.
            // this is the maximum amount of shipments per request according to the API documentation
            var shipments = labelRequest.Shipments;
            var labelRequests = new List<LabelRequest>();
            for (var i = 0; i < shipments.Count; i += 4)
            {
                var request = new LabelRequest
                {
                    Customer = labelRequest.Customer,
                    Shipments = shipments.Skip(i).Take(4).ToList(),
                    Message = labelRequest.Message,
                    LabelSignature = labelRequest.LabelSignature,
                    MultiCollo = labelRequest.MultiCollo
                };
                labelRequests.Add(request);
            }

            var responses = new List<LabelResponse>();
            foreach (var request in labelRequests)
            {
                var response = await _client.Http.Post<LabelResponse>($"shipment/v2_2/label?confirm={request.Confirm}", request);
                responses.Add(response);
            }

            // merge responses back into one response
            var mergedResponse = new LabelResponse
            {
                ResponseShipments = responses.SelectMany(r => r.ResponseShipments).ToArray()
            };

            return mergedResponse;
        }

        private static void SanitizeRequest(LabelRequest request)
        {
            if (request.LabelSignature?.Length > 0)
            {
                var image = new MagickImage(request.LabelSignature);

                image.Resize(1058, 226);
                image.Format = MagickFormat.Gif;
                request.LabelSignature = image.ToByteArray();
                image.Dispose();
            }

            foreach (var shipment in request.Shipments)
            {
                SanitizeShipment(shipment);
            }
        }

        private static void SanitizeShipment(Shipment shipment)
        {
            // trim HS codes and remove spaces
            foreach (var content in shipment.Customs?.Content ?? Array.Empty<Content>())
            {
                content.HsTariffNr = content.HsTariffNr?.Replace(" ", "");
            }

            foreach (var address in shipment.Addresses)
            {
                address.Zipcode = address.Zipcode?.Replace(" ", "");
            }
        }

        private static void ValidateRequest(LabelRequest request)
        {
            if (request.Shipments.Count == 0)
                throw new ValidationException("At least one shipment is required");

            if (request.MultiCollo)
            {
                var groupedByType = request.Shipments.SelectMany(x => x.Addresses).GroupBy(x => x.AddressType);
                foreach (var address in groupedByType)
                {
                    var areTheSame = address.Select(x => JsonConvert.SerializeObject(x)).Distinct().Count() == 1;
                    if (!areTheSame)
                        throw new ValidationException("For multicollo shipment, addresses with the same address type must have the same address details");
                }
            }


            var serialized = JsonConvert.SerializeObject(request);
            if (serialized.Length > 200000)
                throw new ValidationException("Request too large (> 200KB). Please split up the request into smaller requests");

            if (request.LabelSignature?.Length > 0 && serialized.Length > 200000)
                throw new ValidationException("Request too large (> 200KB). Please split up the request into smaller requests or reduce the size of the signature");

            foreach (var shipment in request.Shipments)
            {
                ValidateShipment(shipment);
            }
        }

        private static void ValidateShipment(Shipment shipment)
        {
            var addressTypes = shipment.Addresses.Select(x => x.AddressType).ToArray();
            if (!addressTypes.Contains("01") || !addressTypes.Contains("02"))
                throw new ValidationException("Address 01 (receiver) and 02 (sender) are required");

            foreach (var address in shipment.Addresses)
            {
                if (address.AddressType == "09" && string.IsNullOrEmpty(address.CompanyName?.Trim()))
                    throw new ValidationException("Company name is required for address type 09");

                var beneluxCountries = new[] { "NL", "BE", "LU" };
                if (beneluxCountries.Contains(address.CountryCode) && string.IsNullOrEmpty(address.Zipcode?.Trim()))
                    throw new ValidationException("Zipcode is required for Benelux countries");

                switch (address.CountryCode)
                {
                    case "NL" when !Regex.IsMatch(address.Zipcode ?? "", @"^\d{4}[A-Za-z]{2}$"):
                        throw new ValidationException("Invalid zipcode for Netherlands");
                    case "BE" when !Regex.IsMatch(address.Zipcode ?? "", @"^\d{4}$"):
                    case "LU" when !Regex.IsMatch(address.Zipcode ?? "", @"^\d{4}$"):
                        throw new ValidationException("Invalid zipcode for Belgium or Luxembourg");
                }

                if (beneluxCountries.Contains(address.CountryCode) && Regex.IsMatch(@"\d{5}", address.HouseNr ?? ""))
                    throw new ValidationException("House number must be numeric for Benelux countries");

                if (string.IsNullOrEmpty(address.Name?.Trim()) && string.IsNullOrEmpty(address.CompanyName?.Trim()))
                    throw new ValidationException("Either name or company name is required");
            }
        }
    }
}