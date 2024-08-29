using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PostNLApi.Models.Response;

namespace PostNLApi.Endpoints
{
    public class BarcodeEndpoint
    {
        private readonly PostNLClient _client;

        public BarcodeEndpoint(PostNLClient client)
        {
            _client = client;
        }

        public async Task<string> GenerateBarcode(string type = "3S", string serie = "100000000")
        {
            if (!Regex.IsMatch("^2S|3S|S10|CC|CP|CD|CF|LA|RI|UE$", type.ToUpper()))
            {
                throw new ValidationException($"Invalid barcode type {type}");
            }

            if (!Regex.IsMatch(serie, @"^\d{6,9}$"))
            {
                throw new ValidationException("Invalid serie");
            }

            var url = $"shipment/v1_1/barcode?CustomerCode={_client.CustomerCode}&CustomerNumber={_client.CustomerNumber}&Type={type.ToUpper()}&Serie={serie}";
            var response = await _client.Http.Get<BarcodeResponse>(url);
            return response.Barcode;
        }
    }
}