using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace DDD.Domain.Providers.Office;

public class OfficeProvider : IOfficeProvider
{
    public async Task<string> ExportAndUploadExcel<T>(IList<T> data, IList<ExcelFormat> formats, string fileName)
    {
        var dt = ToDataTable<T>(data);
        // dt.TableName = "";
        var dataSet = new DataSet();
        dataSet.Tables.Add(dt);

        var str = await ExportExcel(dataSet, formats, fileName);
        return str;
    }

    private async Task<string> ExportExcel(DataSet dataSet, IList<ExcelFormat> formats, string fileName)
    {
        using (IWorkbook workbook = new XSSFWorkbook())
        {
            for (var tableIndex = 0; tableIndex < dataSet.Tables.Count; tableIndex++)
            {
                var dt = dataSet.Tables[tableIndex];
                CreateSheetFromDataTable(workbook, tableIndex, dt, formats);
            }

            var stream = new MemoryStream();
            workbook.Write(stream, leaveOpen: true);

            // TODO: Upload

            stream.Close();

            return string.Empty;
        }
    }

    private static void CreateSheetFromDataTable(IWorkbook workbook, int dataTableIndex, DataTable dataTable, IList<ExcelFormat> formats)
    {
        var tableName = string.IsNullOrEmpty(dataTable.TableName) ? $"Sheet {dataTableIndex}" : dataTable.TableName;
        var sheet = (XSSFSheet)workbook.CreateSheet(tableName);
        var columnCount = dataTable.Columns.Count;
        var rowCount = dataTable.Rows.Count;

        //create the format instance
        IDataFormat format = workbook.CreateDataFormat();

        // add column headers
        var row = sheet.CreateRow(0);
        for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            var col = dataTable.Columns[columnIndex];

            var colFormat = formats.FirstOrDefault(x => x.ColId == col.ColumnName);
            if (colFormat is null)
            {
                row.CreateCell(columnIndex).SetCellValue(col.ColumnName);
                continue;
            }

            if (colFormat.IsHide) continue;

            var cell = row.CreateCell(columnIndex);

            if (!string.IsNullOrEmpty(colFormat.ColName))
            {
                cell.SetCellValue(colFormat.ColName);
            }
            // if (colFormat.IsBold)
            // {
            //     var font = workbook.CreateFont();
            //     font.IsBold = true;
            //     cell.CellStyle.SetFont(font);
            // }
        }

        // add data rows
        for(var rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            var dataRow = dataTable.Rows[rowIndex];
            var sheetRow = sheet.CreateRow(rowIndex + 1);

            for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
            {
                var cellRawValue = dataRow[columnIndex];
                if (string.IsNullOrEmpty(cellRawValue.ToString()))
                {
                    continue;
                }

                var col = dataTable.Columns[columnIndex];
                var colFormat = formats.FirstOrDefault(x => x.ColId == col.ColumnName);
                if (colFormat is null)
                {
                    sheetRow.CreateCell(columnIndex).SetCellValue(cellRawValue.ToString());
                    continue;
                }

                if (colFormat.IsHide) continue;

                var cell = sheetRow.CreateCell(columnIndex);

                if (col.DataType == typeof(String))
                {
                    cell.SetCellValue(cellRawValue.ToString());
                }
                else if (col.DataType == typeof(Double) || col.DataType == typeof(Decimal))
                {
                    SetValueAndFormat(workbook, cell, (double)cellRawValue, format.GetFormat("$#,##"));
                }
                else if (col.DataType == typeof(Int16) || col.DataType == typeof(Int32) || col.DataType == typeof(Int64))
                {
                    SetValueAndFormat(workbook, cell, (int)cellRawValue, format.GetFormat("0.00"));
                }
                else if (col.DataType == typeof(DateTime))
                {
                    SetValueAndFormat(workbook, cell, (DateTime)cellRawValue, format.GetFormat("mm/dd/yyyy"));
                }
            }
        }

        // auto size columns
        for (var columnIndex = 0; columnIndex < columnCount; columnIndex++)
        {
            sheet.AutoSizeColumn(columnIndex);
        }
    }

    private DataTable ToDataTable<T>(IList<T> data)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new DataTable();
        foreach (PropertyDescriptor prop in properties)
        {
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;
    }

    static void SetValueAndFormat(IWorkbook workbook, ICell cell, int value, short formatId)
    {
        cell.SetCellValue(value);
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.DataFormat = formatId;
        cell.CellStyle = cellStyle;
    }

    static void SetValueAndFormat(IWorkbook workbook, ICell cell, double value, short formatId)
    {
        cell.SetCellValue(value);
        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.DataFormat = formatId;
        cell.CellStyle = cellStyle;
    }

    static void SetValueAndFormat(IWorkbook workbook, ICell cell, DateTime value, short formatId)
    {
        //set value for the cell
        cell.SetCellValue(value);

        ICellStyle cellStyle = workbook.CreateCellStyle();
        cellStyle.DataFormat = formatId;
        cell.CellStyle = cellStyle;
    }
}
