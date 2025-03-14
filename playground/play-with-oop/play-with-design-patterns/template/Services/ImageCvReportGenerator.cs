using template.Models;

namespace template.Services;

class ImageCvReportGenerator:CvReportGenerator
{
    protected override ExtractedData ExtractData(FileContent content)
    {
        Console.WriteLine("Extracting Data From Image file...");
        return new ExtractedData();
    }

    protected override FileContent ReadFile(string filePath)
    {
        Console.WriteLine("Reading Image file...");
        return new FileContent();
    }
}
