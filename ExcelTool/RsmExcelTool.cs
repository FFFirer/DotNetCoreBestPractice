using System;
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Data;

namespace ExcelTool
{
    public class RsmExcelTool
    {
        private string _folderpath { get; set; }
        private string _outputfile { get; set; }
        public RsmExcelTool(string folderpath, string outputfile)
        {
            _folderpath = folderpath;
            _outputfile = outputfile;
        }

        public void MergeExcelFiles()
        {
            int ColNum = 14;
            // 获取所有Excel文件
            string[] files = Directory.GetFiles(_folderpath, "*.xlsx");

            ISheet inputSheet;
            ISheet outputSheet;
            int excelColNum = 0;
            int excelRowNum = 0;
            DataTable temp = new DataTable();
            for (int i = 0; i < ColNum; i++)
            {
                temp.Columns.Add();
            }

            IRow eachRow;
            IRow outputRow;

            int outputRowNum = 0;
            using (var outfs = new FileStream(_outputfile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var outputExcel = new XSSFWorkbook();
                outputSheet = outputExcel.CreateSheet();

                foreach (string path in files)
                {
                    if (path == _outputfile)
                    {
                        continue;
                    }
                    //temp.Rows.Clear();
                    // 读取每个文件内地内容
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        var workbook = new XSSFWorkbook(fs);
                        inputSheet = workbook.GetSheetAt(0);

                        excelColNum = ColNum;
                        excelRowNum = inputSheet.LastRowNum;
                        for (int i = 0; i < excelRowNum; i++)
                        {
                            eachRow = inputSheet.GetRow(i);

                            outputRow = outputSheet.CreateRow(outputRowNum);
                            outputRowNum++;

                            if(eachRow==null)
                            {
                                continue;
                            }
                            for (int j = 0; j < eachRow.Cells.Count; j++)
                            {
                                try
                                {
                                    outputRow.CreateCell(j).SetCellValue(eachRow.GetCell(j).ToString());
                                }
                                catch (NullReferenceException ex)
                                {
                                    Console.WriteLine($"{path}, col:{j}, row:{i}");
                                }
                            }
                        }
                    }
                }

                outputExcel.Write(outfs);
            }
        }
    }
}
