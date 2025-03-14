using template.Models;

namespace template.Services;

abstract class CvReportGenerator
{
    protected abstract FileContent ReadFile(string filePath);
    protected abstract ExtractedData ExtractData(FileContent content);
    private AnalyzedData AnalyzeData(ExtractedData data)
    {
        Console.WriteLine("Analyzing CV Data....");
        return new AnalyzedData();
    }

    public CvReport GenerateCvReport(string filePath)
    {
        var fileContent = ReadFile(filePath);
        var extractedData = ExtractData(fileContent);
        var analyzedData = AnalyzeData(extractedData);
        Console.WriteLine("Generating CV Report...");
        return new CvReport();
    }
}
