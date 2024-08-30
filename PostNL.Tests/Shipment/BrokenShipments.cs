using System.ComponentModel.DataAnnotations;
using PostNLApi.Models.Request;

namespace PostNL.Tests.Shipment;

public class BrokenShipments : TestBase
{
    [Test]
    public void EmptyShipments()
    {
        var shipments = Array.Empty<PostNLApi.Models.Request.Shipment>();
        Assert.ThrowsAsync<ValidationException>(async () => await Client.Shipment.GenerateLabel(shipments));
    }

    [Test]
    public void NoDeliveryAddress()
    {
        var shipment = new PostNLApi.Models.Request.Shipment
        {
            Addresses =
            [
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

        Assert.ThrowsAsync<ValidationException>(async () => await Client.Shipment.GenerateLabel(shipment));
    }

    [Test]
    public void NoWeight()
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
            ProductCodeDelivery = "3085",
        };

        Assert.ThrowsAsync<ValidationException>(async () => await Client.Shipment.GenerateLabel(shipment));
    }

    [Test]
    public void NoProductCode()
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
        };

        Assert.ThrowsAsync<ValidationException>(async () => await Client.Shipment.GenerateLabel(shipment));
    }

    [Test]
    public void InvalidProductCode()
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
            ProductCodeDelivery = "abcd",
        };

        Assert.ThrowsAsync<ValidationException>(async () => await Client.Shipment.GenerateLabel(shipment));
    }

    [Test]
    public void WrongCountryCode()
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
                    CountryCode = "ABC",
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

        Assert.ThrowsAsync<ValidationException>(async () => await Client.Shipment.GenerateLabel(shipment));
    }

    [Test]
    public void DifferentAddressesMultiCollo()
    {
        var shipments = new[]
        {
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
                        Street = "Hoofdstraat",
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
            }
        };

        Assert.ThrowsAsync<ValidationException>(async () => await Client.Shipment.GenerateLabel(shipments, multicollo: true));
    }
}