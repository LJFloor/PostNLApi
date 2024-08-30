using PostNLApi.Models.Response;

namespace PostNL.Tests;

public abstract class TestHelper
{
    internal const string CustomerNumber = "11223344";
    internal const string CustomerCode = "DEVC";

    public static async void SaveLabels(LabelResponse response)
    {
        var pdfs = response.ResponseShipments.SelectMany(x => x.Labels).Select(x => x.Content);
        foreach (var pdf in pdfs)
        {
            var tempDir = Path.Combine(Path.GetTempPath(), "PostNL");
            Directory.CreateDirectory(tempDir);
            var path = Path.Combine(tempDir, $"${DateTime.Now.Ticks}.pdf");
            await File.WriteAllBytesAsync(path, pdf);
        }
    }
}