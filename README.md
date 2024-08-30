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
            CountryCode = "NL"
        },
        new Address
        {
            AddressType = AddressType.Sender,
            CompanyName = "PostNL",
            Street = "Burgemeester van Reenensingel",
            HouseNr = "101",
            City = "Gouda",
            Zipcode = "2803 PA",
            CountryCode = "NL"
        }
    ],
    Dimension = new Dimension
    {
        Weight = 1000 // in grams
    },
    ProductCodeDelivery = "3085" // standard shipment
};

var response = await client.Shipment.GenerateLabel(shipment);

// now we save the labels
foreach (var label in response.ResponseShipments.SelectMany(x => x.Labels))
{
    await File.WriteAllBytesAsync($"C:\\Temp\\postnl-{Guid.NewGuid()}.pdf", label.Content);
}
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

Currently, the project supports creating shipments to countries that are in the EU. Non-EU shipments is being worked on, but a developer account doesn't allow generating labels for
non-EU countries. This is why I have not been able to test this yet.

Though being able to send to EU countries will probably fit about 95% of all use cases.

## FAQ

### Is this an official package from PostNL?

No, this is a third-party package that uses the PostNL API. This package is not affiliated with PostNL. The package is created to make it easier for you to work with the PostNL
API.

### I got a ValidationException

The PostNL API is quite strict on the data you send. Also, the API error messages aren't always very clear on what went wrong.

For these reasons a lot of fields are checked before sending the request to PostNL. This way a lot of low-hanging fruit
can be caught. Think about catching invalid zipcodes, invalid country codes, etc.

If you get a `ValidationException`, it means that the data you sent is not correct. The exception message will contain more
information on what went wrong.

```csharp
try
{
    var response = await client.Shipment.GenerateLabel(shipment);
}
catch (ValidationException ex)
{
    Console.WriteLine(ex.Message);
}
```

If you get a `ValidationException` and you are sure the data is correct, please open an issue.

### My shipment is not converting to a multicollo

Shipments are only grouped to multicollo if the shipments are going to The Netherlands or Belgium. This is a requirement from PostNL.

If you have multiple shipments, and they are not converting to a multicollo, it is likely that the shipments are not going to The Netherlands or Belgium or you did not enable
the `multicollo` parameter. Especially that last scenario made me collectively waste about 2 hours of debugging.

### The height, length and width I entered are in the wrong order on the label

The PostNL API expects the dimensions in the order of `length`, `width`, `height`. This is different from the order you might expect.
Before sending the shipment, the dimensions are automatically reordered to match the PostNL API requirements.
More info: https://developer.postnl.nl/docs/#/http/models/structures/dimension

### What is the Magick.NET dependency used for?

The Magic.NET library is for resizing and converting your signature you set for customs to GIF. This is a requirement from PostNL. This way you don't have to deal with this, and
can simply set any image you want.

```csharp
var signature = await File.ReadAllBytesAsync("C:\\Temp\\signature.png");
await client.Shipment.GenerateLabel(shipment, labelSignature: signature);
```

### I have a question or found a bug

Please open an issue on GitHub. I will try to respond as soon as possible.