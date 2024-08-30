namespace PostNL.Tests.Shipment.Barcode;

public class Barcode3S : TestBase
{
    [Test]
    public async Task Type3S()
    {
        var barcode = await Client.Barcode.GenerateBarcode();
        const string pattern = $@"^3S{TestHelper.CustomerCode}\d{{9}}$";
        Assert.That(barcode, Does.Match(pattern));
    }
}