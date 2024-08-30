# PostNLApi

A package to register PostNL shipments (including multicollo)

> Disclaimer: This is not an official package from PostNL

## Initialize

```csharp
var client = new PostNLClient("<apikey>", "<customer_code>", "<customer_number>");

// or use the staging (sandbox) environment

var client = new PostNLClient("<apikey>", "<customer_code>", "<customer_number>", sandbox: true);
```

## Basic shipment

This is the bare minimum required to register a shipment

```csharp
var shipment = new Shipment
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
    ProductCodeDelivery = "3085",   // standard shipment
};

var response = await client.Shipment.GenerateLabel(shipment);
```

> Note: PostNL has a [list of product codes](https://developer.postnl.nl/docs/#/http/reference-data/product-codes) to fill in the `ProductCodeLivery` field.

## Multicollo

You can register multiple shipments at once:

```csharp
var response = await client.Shipment.GenerateLabel(shipments);
```

The PostNL API supports grouping different shipments together to form a multicollo if you ship to The Netherlands or Belgium. You can enable this in your request:

```csharp
var response = await client.Shipment.GenerateLabel(shipments, multicollo: true);
```

This feature comes with a few notes:

- This will group **ALL** shipments in that request together if possible
- This will **OVERWRITE** any groups you manually defined
- All sender addresses and all receiver addresses should match across the shipments. It doesn't make sense to group shipments with different sender 
  addresses or receiver addresses.

## Debug mode

You can enable debug mode to see the request and response from PostNL:

```csharp
client.EnableDebug = true;
client.Debug += (s, e) => Console.WriteLine(e);
```

## Supported countries

Currently, the project supports creating shipments to countries that are in the EU. Non-EU shipments is being worked on, since customs is a bit tricky.

Though being able to send to EU countries will probably fit about 95% of all use cases.