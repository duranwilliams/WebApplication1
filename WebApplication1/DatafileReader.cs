// files: https://catalog.data.gov/dataset?q=&sort=metadata_created+desc
// This thing will read the files and store what is necessary.

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Numerics;


// This is just round 1 of planning this whole thing
internal class DatafileReader
{
    public static String heatFilePath = "public_data_files\\Nationwide Collection of Heat Flow.xlsx";
    private static String pdfUrl = "https://data.wa.gov/api/views/f6w7-q2d2/rows.rdf?accessType=DOWNLOAD";
    public static BigInteger currentFileRowCounter = 0;
    private static String psScriptPath = "C:\\Users\\duran\\source\\repos\\USAr-PUBLIC\\WebApplication1\\WebApplication1\\getFileFromUrl.ps1";
    internal static string ImportFiles()
    {
        var pth = heatFilePath;
        File.ReadLines(pth); // todo: read all lines and store what is needed for each file
        // Possible have 1 config per file to identify which fields to import

        // If the environment variable is set to clear all records, call the clear stored proc
        if (!Environment.GetEnvironmentVariable("isClearRowsBeforeImport").IsNullOrEmpty())
            AffinityTables.spClearMainTable();



        ReadExcelFile(pth);
        return File.ReadAllText(pth);
    }


public static void ReadExcelFile(string filePath)
{
    using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
    {
        WorkbookPart? workbookPart = document.WorkbookPart;
        Sheets? sheets = (workbookPart != null ? workbookPart.Workbook.Sheets : null);
        var rowList = new List<string>();
            rowList.Add(filePath);
            rowList.Add(heatFilePath);

            if (sheets != null && workbookPart != null)
            {
                foreach (Sheet sheet in sheets)
                {
                    if (sheet != null && sheet.Id != null)
                    {
                        WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                        SheetData? sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                        foreach (Row row in sheetData.Elements<Row>())
                        {
                            var rowIncrementer = 0;
                            foreach (Cell cell in row.Elements<Cell>())
                            {
                                try
                                {
                                    string cellValue = GetCellValue(cell, workbookPart);
                                    Debug.WriteLine(cellValue);
                                    rowList.Add(cellValue);
                                    rowIncrementer++;
                                }
                                catch (Exception ex)
                                {
                                    // TODO: logger 
                                    Debug.WriteLine(ex.ToString());
                                    AffinityLogger.spCaptureLogRow(ex.InnerException.ToString());
                                }
                            }
                            AffinityTables.spInsertGeneralDataRow(rowList);
                            currentFileRowCounter++;
                        }
                    }
                }
            }
    }
}

private static string GetCellValue(Cell cell, WorkbookPart workbookPart)
{
        // Logic to retrieve the cell value
        // Handle shared strings and other data types
        Debug.WriteLine("Printing out cell inner text");
        return cell.InnerText.ToString();
}

    internal static object callPowershellDownloadPdf()
    {
        string scriptPath = psScriptPath;

        var processInfo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\"",
            UseShellExecute = false, // Required to redirect output/error streams
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using var process = Process.Start(processInfo);
        process.WaitForExit();

        // Read the output and error streams
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        Console.WriteLine("Output: " + output);
        Console.WriteLine("Error: " + error);
        Console.WriteLine("Exit Code: " + process.ExitCode);

        return error;
    }
}