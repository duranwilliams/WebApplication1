// files: https://catalog.data.gov/dataset?q=&sort=metadata_created+desc
// This thing will read the files and store what is necessary.

// This is just round 1 of planning this whole thing
internal class DatafileReader
{
    internal static string ImportFiles()
    {
        var pth = "C:\\Users\\duran\\source\\repos\\USAr-PUBLIC\\WebApplication1\\WebApplication1\\data_files\\Nationwide Collection of Heat Flow.xlsx";
        File.ReadLines(pth); // todo: read all lines and store what is needed for each file
        // Possible have 1 config per file to identify which fields to import

        return File.ReadAllText(pth);
    }
}