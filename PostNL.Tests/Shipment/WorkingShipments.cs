using PostNLApi.Models.Request;

namespace PostNL.Tests.Shipment;

public class WorkingShipments : TestBase
{
    [Test]
    public async Task NetherlandsMinimal()
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

        foreach (var label in response.ResponseShipments.SelectMany(x => x.Labels))
        {
            await File.WriteAllBytesAsync($"C:\\Temp\\{Guid.NewGuid()}.pdf", label.Content);
        }

        Assert.That(response.ResponseShipments, Has.Length.EqualTo(1), "Expected 1 shipment, got " + response.ResponseShipments.Length);
        Assert.That(response.ResponseShipments[0].Labels, Has.Length.EqualTo(1), "Expected 1 label, got " + response.ResponseShipments[0].Labels.Length);
#if DEBUG
        TestHelper.SaveLabels(response);
#endif
    }

    [Test]
    public async Task NetherlandsMultiCollo()
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
        TestHelper.SaveLabels(response);
#endif
    }

    [Test]
    public async Task GermanyMinimal()
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
        TestHelper.SaveLabels(response);
#endif
    }

    [Test]
    public async Task GermanyMultiCollo()
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
        TestHelper.SaveLabels(response);
#endif
    }
}