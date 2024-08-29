using PostNLApi.Models.Request;

namespace PostNL.Tests.Shipment.Netherlands;

public class Minimal : TestBase
{
    /// <summary>
    /// Basic shipment with minimal requirements
    /// </summary>
    [Test]
    public async Task TestMinimal()
    {
        var shipment = new PostNLApi.Models.Request.Shipment
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
        };

        var response = await Client.Shipment.GenerateLabel(shipment);

        Assert.That(response.ResponseShipments, Has.Length.EqualTo(1), "Expected 1 shipment, got " + response.ResponseShipments.Length);
        Assert.That(response.ResponseShipments[0].Labels, Has.Length.EqualTo(1), "Expected 1 label, got " + response.ResponseShipments[0].Labels.Length);
#if DEBUG
        TestHelper.SaveLabels("NL", response);
#endif
    }
}