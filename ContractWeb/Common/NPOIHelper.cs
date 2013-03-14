using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Web;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using System.Text;

namespace ContractWeb.Common
{
    /// <summary>
    /// 使用第三方组件NPOI动态创建EXCEL文件流
    /// </summary>
    public class NPOIHelper
    {
        #region DataSet导出为Excel文件流
        /// <summary>
        /// 动态创建DataSet对应的Excel文件流
        /// </summary>
        /// <param name="sourceDs">DataSet对象</param>
        /// <returns>Stream流</returns>
        public static MemoryStream ExportDataSetToExcel(DataSet sourceDs)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();

            try
            {
                for (int i = 0; i < sourceDs.Tables.Count; i++)
                {
                    string sheetname = sourceDs.Tables[i].TableName.Trim() == string.Empty ? "$Sheet" + i.ToString() : sourceDs.Tables[i].TableName.Trim();
                    NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet(sheetname);
                    NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(0);
                    foreach (DataColumn column in sourceDs.Tables[i].Columns)
                    {
                        headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                    }

                    int rowIndex = 1;

                    foreach (DataRow row in sourceDs.Tables[i].Rows)
                    {
                        NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);

                        foreach (DataColumn column in sourceDs.Tables[i].Columns)
                        {
                            dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                        }

                        rowIndex++;
                    }

                }

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
            catch (Exception e)
            {
                return ms;
            }
            finally
            {
                if (ms != null) { ms.Dispose(); }
                workbook = null;
            }
        }
        #endregion

        #region DataTable导出为Excel文件流
        /// <summary>
        /// 动态创建DataTable对应的Excel文件流
        /// </summary>
        /// <param name="sourceDt">DataTable对象</param>
        /// <returns>Stream流</returns>
        public static MemoryStream ExportDataTableToExcel(DataTable sourceDt)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();

            try
            {
                string sheetname = sourceDt.TableName.Trim() == string.Empty ? "$Sheet1" : sourceDt.TableName.Trim();
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet(sheetname);
                NPOI.SS.UserModel.IRow headerRow = sheet.CreateRow(0);

                foreach (DataColumn column in sourceDt.Columns)
                {
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                }

                int rowIndex = 1;

                foreach (DataRow row in sourceDt.Rows)
                {
                    NPOI.SS.UserModel.IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in sourceDt.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }

                    rowIndex++;
                }

                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }
            catch (Exception e)
            {
                return ms;
            }
            finally
            {
                if (ms != null) { ms.Dispose(); }
                workbook = null;
            }
        }
        #endregion

        #region Excel文件导入为DataTable
        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportDataTableFromExcel(Stream excelFileStream, string sheetName, int headerRowIndex)
        {
            HSSFWorkbook workbook;
            NPOI.SS.UserModel.ISheet sheet;
            DataTable table = null;

            try
            {
                workbook = new HSSFWorkbook(excelFileStream);
                sheet = workbook.GetSheet(sheetName);
                table = new DataTable();

                NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(headerRowIndex);
                int cellCount = headerRow.LastCellNum;
                
                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    NPOI.SS.UserModel.IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                excelFileStream.Close();
                return table;
            }
            catch (Exception e)
            {
                return table;
            }
            finally
            {
                if (table != null) { table.Dispose(); }
                workbook = null;
                sheet = null;
            }
        }

        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="sheetName">Excel工作表索引</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportDataTableFromExcel(Stream excelFileStream, int sheetIndex, int headerRowIndex)
        {
            HSSFWorkbook workbook;
            NPOI.SS.UserModel.ISheet sheet;
            DataTable table = null;

            try
            {
                workbook = new HSSFWorkbook(excelFileStream);
                sheet = workbook.GetSheetAt(sheetIndex);
                table = new DataTable();

                NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(headerRowIndex);
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                    {
                        // 如果遇到第一个空列，则不再继续向后读取
                        cellCount = i + 1;
                        break;
                    }

                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }

                for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                {
                    NPOI.SS.UserModel.IRow row = sheet.GetRow(i);

                    if (row == null || row.GetCell(0) == null || row.GetCell(0).ToString().Trim() == "")
                    {
                        // 如果遇到第一个空行，则不再继续向后读取
                        break;
                    }

                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        dataRow[j] = row.GetCell(j);
                    }

                    table.Rows.Add(dataRow);
                }

                excelFileStream.Close();
                return table;
            }
            catch (Exception e)
            {
                return table;
            }
            finally
            {
                if (table != null) { table.Dispose(); }
                workbook = null;
                sheet = null;
            }
        }
        #endregion

        #region Excel文件导入为DataSet对象
        /// <summary>
        /// 由Excel导入DataSet，如果有多个工作表，则导入多个DataTable
        /// </summary>
        /// <param name="excelFileStream">Excel文件流</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataSet</returns>
        public static DataSet ImportDataSetFromExcel(Stream excelFileStream, int headerRowIndex)
        {
            DataSet ds = null;
            HSSFWorkbook workbook;

            try
            {
                workbook = new HSSFWorkbook(excelFileStream);
                ds = new DataSet();

                for (int a = 0, b = workbook.NumberOfSheets; a < b; a++)
                {
                    NPOI.SS.UserModel.ISheet sheet = workbook.GetSheetAt(a);
                    DataTable table = new DataTable();

                    NPOI.SS.UserModel.IRow headerRow = sheet.GetRow(headerRowIndex);
                    int cellCount = headerRow.LastCellNum;

                    for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                        {
                            // 如果遇到第一个空列，则不再继续向后读取
                            cellCount = i + 1;
                            break;
                        }

                        DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                        table.Columns.Add(column);
                    }

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        NPOI.SS.UserModel.IRow row = sheet.GetRow(i);

                        if (row == null || row.GetCell(0) == null || row.GetCell(0).ToString().Trim() == "")
                        {
                            // 如果遇到第一个空行，则不再继续向后读取
                            break;
                        }

                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }
                        }

                        table.Rows.Add(dataRow);
                    }

                    ds.Tables.Add(table);
                }

                excelFileStream.Close();
                return ds;
            }
            catch (Exception e)
            {
                return ds;
            }
            finally
            {
                if (ds != null) { ds.Dispose(); }
                workbook = null;
            }
        }
        #endregion
    }
}