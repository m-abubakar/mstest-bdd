using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.IO;

namespace Utils
{
    public class XLReader
    {
        public XLReader(string xlFile, string xlSheet) {
            xlFile_ = xlFile;
            xlSheet_ = xlSheet;
        }

        public int GetRowCount()
        {
            using (FileStream fs = new FileStream(xlFile_, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb = new XSSFWorkbook(fs);
                ISheet ws = wb.GetSheet(xlSheet_);
                int rowCount = ws.LastRowNum;
                return rowCount;
            }
        }

        public int GetCellCount(int rowNum)
        {
            using (FileStream fs = new FileStream(xlFile_, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb = new XSSFWorkbook(fs);
                ISheet ws = wb.GetSheet(xlSheet_);
                IRow row = ws.GetRow(rowNum);
                int cellCount = row.LastCellNum;
                return cellCount;
            }
        }

        public string GetCellData(string xlFile, string xlSheet, int rowNum, int colNum)
        {
            using (FileStream fs = new FileStream(xlFile, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook wb = new XSSFWorkbook(fs);
                ISheet ws = wb.GetSheet(xlSheet);
                IRow row = ws.GetRow(rowNum);
                ICell cell = row.GetCell(colNum);
                string data = cell?.ToString() ?? string.Empty;
                return data;
            }
        }

        public void SetCellData(string xlFile, string xlSheet, int rowNum, int colNum, string data)
        {
            using (FileStream fs = new FileStream(xlFile, FileMode.Open, FileAccess.ReadWrite))
            {
                XSSFWorkbook wb = new XSSFWorkbook(fs);
                ISheet ws = wb.GetSheet(xlSheet);
                IRow row = ws.GetRow(rowNum) ?? ws.CreateRow(rowNum);
                ICell cell = row.GetCell(colNum) ?? row.CreateCell(colNum);
                cell.SetCellValue(data);
                fs.Seek(0, SeekOrigin.Begin);
                wb.Write(fs);
            }
        }


        String xlFile_;
        String xlSheet_;
    }
}