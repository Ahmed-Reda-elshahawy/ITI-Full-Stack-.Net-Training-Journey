
using template.Models;

namespace template.Services;

class PdfCvReportGenerator : CvReportGenerator
{
    protected override ExtractedData ExtractData(FileContent content)
    {
        Console.WriteLine("Extracting Data From PDF file...");
        return new ExtractedData();
    }

    protected override FileContent ReadFile(string filePath)
    {
        Console.WriteLine("Reading PDF file...");
        return new FileContent();
    }
}
