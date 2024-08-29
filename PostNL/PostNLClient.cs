using System;
using System.Text.RegularExpressions;
using PostNLApi.Endpoints;
using PostNLApi.Http;

namespace PostNLApi
{
    public class PostNLClient
    {
        private const string ProductionUrl = "https://api.postnl.nl";
        private const string SandboxUrl = "https://api-sandbox.postnl.nl";

        internal readonly JsonHttpClient Http;
        internal readonly bool Sandbox;
        internal readonly string CustomerCode;
        internal readonly string CustomerNumber;

        /// <summary>
        /// Create a new PostNL client using your api key, customer code and customer number
        /// </summary>
        /// <param name="apiKey">Your PostNL api key</param>
        /// <param name="customerCode">Your customer code</param>
        /// <param name="customerNumber">Your customer number</param>
        /// <param name="sandbox">Whether to use the staging api server</param>
        /// <exception cref="ArgumentException">You gave an invalid argument</exception>
        public PostNLClient(string apiKey, string customerCode, string customerNumber, bool sandbox = false)
        {
            if (!Guid.TryParse(apiKey, out _))
            {
                throw new ArgumentException("Invalid API key");
            }

            if (!Regex.IsMatch(customerCode, "^[A-Z]{4}$"))
            {
                throw new ArgumentException("Invalid customer code");
            }

            if (!Regex.IsMatch(customerNumber, "^[0-9]{8}$"))
            {
                throw new ArgumentException("Invalid customer number");
            }

            CustomerCode = customerCode;
            CustomerNumber = customerNumber;
            Sandbox = sandbox;
            Http = new JsonHttpClient();
            Http.BaseAddress = new Uri(sandbox ? SandboxUrl : ProductionUrl);
            Http.DefaultRequestHeaders.Add("apikey", apiKey);
        }

        /// <summary>
        /// Endpoint for shipments
        /// </summary>
        public ShipmentEndpoint Shipment => _shipmentEndpoint ?? (_shipmentEndpoint = new ShipmentEndpoint(this));
        private ShipmentEndpoint _shipmentEndpoint;

        /// <summary>
        /// Endpoint for barcodes
        /// </summary>
        public BarcodeEndpoint Barcode => _barcodeEndpoint ?? (_barcodeEndpoint = new BarcodeEndpoint(this));
        private BarcodeEndpoint _barcodeEndpoint;
    }
}