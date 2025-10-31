// files: https://catalog.data.gov/dataset?q=&sort=metadata_created+desc
// This thing will read the files and store what is necessary.

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;


// This is just round 1 of planning this whole thing
internal class DatafileReader
{
    internal static string ImportFiles()
    {
        var pth = "C:\\Users\\duran\\source\\repos\\USAr-PUBLIC\\WebApplication1\\WebApplication1\\data_files\\Nationwide Collection of Heat Flow.xlsx";
        File.ReadLines(pth); // todo: read all lines and store what is needed for each file
        // Possible have 1 config per file to identify which fields to import

        ReadExcelFile(pth);
        return File.ReadAllText(pth);
    }


public static void ReadExcelFile(string filePath)
{
    using (SpreadsheetDocument document = SpreadsheetDocument.Open(filePath, false))
    {
        WorkbookPart workbookPart = document.WorkbookPart;
        Sheets sheets = workbookPart.Workbook.Sheets;

        foreach (Sheet sheet in sheets)
        {
            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            foreach (Row row in sheetData.Elements<Row>())
            {
                foreach (Cell cell in row.Elements<Cell>())
                {
                        try
                        {
                            string cellValue = GetCellValue(cell, workbookPart);
                            Console.WriteLine(cellValue);
                        }
                        catch (Exception ex)
                        {
                            // TODO: logger 
                            Console.WriteLine(ex.ToString());
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
        return String.Empty;
}

}