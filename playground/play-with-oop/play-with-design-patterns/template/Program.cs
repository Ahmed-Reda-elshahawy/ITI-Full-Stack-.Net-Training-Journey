// See https://aka.ms/new-console-template for more information
using template.Services;

Console.WriteLine("Hello, World!");

GenerateCvReport(new PdfCvReportGenerator());
GenerateCvReport(new DocCvReportGenerator());
GenerateCvReport(new ImageCvReportGenerator());

void GenerateCvReport(CvReportGenerator generator)
{
    Console.WriteLine("====================================================");
    generator.GenerateCvReport("path");
    Console.WriteLine("====================================================");
}