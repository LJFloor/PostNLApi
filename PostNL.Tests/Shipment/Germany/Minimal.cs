using PostNLApi.Models.Request;

namespace PostNL.Tests.Shipment.Germany;

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
                    Street = "Fontanestraße",
                    Name = "Wir Machen Das",
                    HouseNr = "2",
                    HouseNrExt = "A",
                    City = "Kassel",
                    Zipcode = "34125",
                    CountryCode = "DE",
                },
                new Address
                {
                    AddressType = AddressType.Sender,
                    Name = "Hazeleger",
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
        };
        
        var response = await Client.Shipment.GenerateLabel(shipment);
        Assert.That(response.ResponseShipments, Has.Length.EqualTo(1), "Expected 1 shipment, got " + response.ResponseShipments.Length);
        Assert.That(response.ResponseShipments[0].Labels, Has.Length.EqualTo(1), "Expected 1 label, got " + response.ResponseShipments[0].Labels.Length);
#if DEBUG
        TestHelper.SaveLabels("DE", response);
#endif
    }
}