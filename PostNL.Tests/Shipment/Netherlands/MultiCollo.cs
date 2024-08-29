using PostNLApi.Models.Request;

namespace PostNL.Tests.Shipment.Netherlands;

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
                        Name = "Hazeleger",
                        Street = "Postweg",
                        HouseNr = "1",
                        HouseNrExt = "B",
                        City = "Leiden",
                        Zipcode = "1234 AB",
                        CountryCode = "NL",
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
                ProductCodeDelivery = "3085",
            },
            new PostNLApi.Models.Request.Shipment
            {
                Addresses =
                [
                    new Address
                    {
                        AddressType = AddressType.Receiver,
                        Name = "Hazeleger",
                        Street = "Postweg",
                        HouseNr = "1",
                        HouseNrExt = "B",
                        City = "Leiden",
                        Zipcode = "1234 AB",
                        CountryCode = "NL",
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
                ProductCodeDelivery = "3085",
            }
        ];

        var response = await Client.Shipment.GenerateLabel(shipments, multicollo: true);
        Assert.That(response.ResponseShipments, Has.Length.EqualTo(2), "Expected 2 shipments, got " + response.ResponseShipments.Length);
#if DEBUG
        TestHelper.SaveLabels("NL_MC", response);
#endif
    }
}