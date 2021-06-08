using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Dynamic;
using System.IO;
using System.Windows;

namespace Ikigai.TestChallenge.Client.App.Common
{
    public static class Utilities
    {
        public static DataTable DynamicListToDataTable(string tableName, List<dynamic> data)
        {
            DataTable retVal = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(data), typeof(DataTable));
            retVal.TableName = tableName;
            return retVal;
        }

        public static dynamic ToDynamic<T>(this T obj)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var currentValue = propertyInfo.GetValue(obj);
                expando.Add(propertyInfo.Name, currentValue);
            }
            return expando as ExpandoObject;
        }

        public static DataSet ConvertToDataTableSet(Dictionary<string, List<dynamic>> data)
        {
            DataSet xlSheetsData = new DataSet();

            foreach (var kvpData in data)
            {
                xlSheetsData.Tables.Add(DynamicListToDataTable(kvpData.Key, kvpData.Value));
            }

            return xlSheetsData;
        }

        public static SaveFileDialog PromptForFileSave(string fileName,string fileExtension,string fileTypes)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = fileTypes,
                FilterIndex = 0,
                RestoreDirectory = true,
                CreatePrompt = true,
                Title = "Export Excel File To",
                InitialDirectory = @"C:\",
                FileName = $"{fileName}-{DateTime.Now:yyyyMMdd_HHmmss}.{fileExtension}"
            };

            return saveFileDialog;
        }

        public static void WriteDataToFile(string fileName, DataSet dsToExport, string fileType)
        {
            switch (fileType)
            {
                case "excel":
                    WriteExcelFile(fileName, dsToExport);
                    break;
                case "json":
                    WriteJsonFile(fileName, dsToExport);
                    break;
                default:
                    break;
            }
        }

        public static void WriteJsonFile(string fileName, DataSet dsToExport)
        {
            string json = JsonConvert.SerializeObject(dsToExport, Formatting.Indented);

            SaveFileDialog saveFileDialog = PromptForFileSave(fileName, "json", "JSON files (*.json)|*.json");

            if (saveFileDialog.ShowDialog() ?? false)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, json);
                MessageBox.Show($"The file is successfully saved to path {saveFileDialog.FileName}.", "File Saved", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }
        }

        public static void WriteExcelFile(string fileName, DataSet dsToExport)
        {
            SaveFileDialog saveFileDialog = PromptForFileSave(fileName, "xlsx", "Excel files (*.xlsx)|*.xlsx");

            if (saveFileDialog.ShowDialog() ?? false)
            {
                string path = saveFileDialog.FileName;

                using (SpreadsheetDocument document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook))
                {
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();
                    Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                    for (int i = 0; i < dsToExport.Tables.Count; i++)
                    {
                        DataTable table = dsToExport.Tables[i];

                        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                        SheetData sheetData = new SheetData();
                        worksheetPart.Worksheet = new Worksheet(sheetData);

                        Sheet sheet = new Sheet()
                        {
                            Id = workbookPart.GetIdOfPart(worksheetPart),
                            SheetId = (uint)i + 1,
                            Name = table.TableName
                        };

                        sheets.Append(sheet);

                        Row headerRow = new Row();

                        List<string> columns = new List<string>();

                        foreach (System.Data.DataColumn column in table.Columns)
                        {
                            columns.Add(column.ColumnName);

                            Cell cell = new Cell();
                            cell.DataType = CellValues.String;
                            cell.CellValue = new CellValue(column.ColumnName);
                            headerRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(headerRow);

                        foreach (DataRow dsrow in table.Rows)
                        {
                            Row newRow = new Row();
                            foreach (string col in columns)
                            {
                                Cell cell = new Cell();
                                cell.DataType = CellValues.String;
                                object cellValue = dsrow[col];

                                if (cellValue != null)
                                {
                                    switch (Type.GetTypeCode(cellValue.GetType()))
                                    {
                                        case TypeCode.Int32:
                                        case TypeCode.Int64:
                                            cell.DataType = CellValues.Number;
                                            cell.CellValue = new CellValue(Convert.ToInt32(cellValue));
                                            break;
                                        case TypeCode.Decimal:
                                            cell.DataType = CellValues.Number;
                                            cell.CellValue = new CellValue(Convert.ToDecimal(cellValue));
                                            break;
                                        case TypeCode.Double:
                                            cell.DataType = CellValues.Number;
                                            cell.CellValue = new CellValue(Convert.ToDouble(cellValue));
                                            break;
                                        case TypeCode.DateTime:
                                            cell.DataType = CellValues.String;
                                            DateTime dtCellValue = Convert.ToDateTime(cellValue);

                                            if (dtCellValue.TimeOfDay.TotalSeconds == 0)
                                                cell.CellValue = new CellValue(dtCellValue.ToString("yyyy-MM-dd"));
                                            else
                                                cell.CellValue = new CellValue(dtCellValue.ToString("yyyy-MM-ddTHH:mm:ss.sssZ"));

                                            break;
                                        default:
                                            cell.CellValue = new CellValue(Convert.ToString(cellValue));
                                            break;

                                    }

                                }
                                newRow.AppendChild(cell);
                            }

                            sheetData.AppendChild(newRow);
                        }
                    }
                    workbookPart.Workbook.Save();
                }

                MessageBox.Show($"The file is successfully saved to path {path}.", "File Saved", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
            }
        }

    }
}
