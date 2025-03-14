using template.Models;

namespace template.Services;

class DocCvReportGenerator:CvReportGenerator
{
    protected override ExtractedData ExtractData(FileContent content)
    {
        Console.WriteLine("Extracting Data From Doc file...");
        return new ExtractedData();
    }

    protected override FileContent ReadFile(string filePath)
    {
        Console.WriteLine("Reading Doc file...");
        return new FileContent();
    }
}
