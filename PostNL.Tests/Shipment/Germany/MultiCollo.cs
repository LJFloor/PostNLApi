using PostNLApi.Models.Request;

namespace PostNL.Tests.Shipment.Germany;

public class MultiCollo : TestBase
{
    [Test]
    public async Task TestMultiCollo()
    {
        PostNLApi.Models.Request.Shipment[] shipments =
        [
            new PostNLApi.Models.Request.Shipment
            {
                Addresses =
                [
                    new Address
                    {
                        AddressType = AddressType.Receiver,
                        CompanyName = "Wir Machen Das",
                        Street = "Fontanestraße",
                        HouseNr = "2",
                        HouseNrExt = "A",
                        City = "Kassel",
                        Zipcode = "34125",
                        CountryCode = "DE",
                    },
                    new Address
                    {
                        AddressType = AddressType.Sender,
                        CompanyName = "PostNL",
                        Street = "Burgemeester van Reenensingel",
                        HouseNr = "101",
                        City = "Gouda",
                        Zipcode = "2803 PA",
                        CountryCode = "NL",
                    }
                ],
                Dimension = new Dimension
                {
                    Weight = 1000,
                },
                ProductCodeDelivery = "4907",
            },
            new PostNLApi.Models.Request.Shipment
            {
                Addresses =
                [
                    new Address
                    {
                        AddressType = AddressType.Receiver,
                        CompanyName = "Wir Machen Das",
                        Street = "Fontanestraße",
                        HouseNr = "2",
                        HouseNrExt = "A",
                        City = "Kassel",
                        Zipcode = "34125",
                        CountryCode = "DE",
                    },
                    new Address
                    {
                        AddressType = AddressType.Sender,
                        CompanyName = "PostNL",
                        Street = "Burgemeester van Reenensingel",
                        HouseNr = "101",
                        City = "Gouda",
                        Zipcode = "2803 PA",
                        CountryCode = "NL",
                    }
                ],
                Dimension = new Dimension
                {
                    Weight = 2000,
                },
                ProductCodeDelivery = "4907",
            }
        ];

        var response = await Client.Shipment.GenerateLabel(shipments, multicollo: true);
        Assert.That(response.ResponseShipments, Has.Length.EqualTo(2), "Expected 2 shipments, got " + response.ResponseShipments.Length);
#if DEBUG
        TestHelper.SaveLabels("NL_MC", response);
#endif
    }
}