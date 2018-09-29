using Aspose.Cells;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUI.Common
{
    public class OperateExcel
    {
        private static string ExportFilesPath = System.Configuration.ConfigurationManager.AppSettings["exportFilesPath"].ToString();

        #region DataTable生成Excel
        /// <summary>
        /// DataTable生成Excel
        /// </summary>
        /// <param name="dtList">DataTable</param>
        /// <param name="fileName">文件名</param>
        /// <returns>返回文件名(包含扩展名)</returns>
        public static string ExportToExcel(DataTable dtList, string fileName)
        {
            string pathToFiles = System.Web.HttpContext.Current.Server.MapPath(ExportFilesPath);
            string etsName = ".xls";
            //获取保存路径
            string path = pathToFiles + fileName + etsName;
            Workbook wb = new Workbook();
            Worksheet ws = wb.Worksheets[0];
            Cells cell = ws.Cells;
            //cell.SetRowHeight(0, 20);   //设置行高

            //表头样式
            Style stHeadLeft = wb.Styles[wb.Styles.Add()];
            stHeadLeft.HorizontalAlignment = TextAlignmentType.Left;       //文字居中
            stHeadLeft.Font.Name = "宋体";
            stHeadLeft.Font.IsBold = true;                                                             //设置粗体
            stHeadLeft.Font.Size = 14;                                                                    //设置字体大小
            Style stHeadRight = wb.Styles[wb.Styles.Add()];
            stHeadRight.HorizontalAlignment = TextAlignmentType.Right;       //文字居中
            stHeadRight.Font.Name = "宋体";
            stHeadRight.Font.IsBold = true;                                                             //设置粗体
            stHeadRight.Font.Size = 14;                                                                    //设置字体大小

            //内容样式
            Style stContentLeft = wb.Styles[wb.Styles.Add()];
            stContentLeft.HorizontalAlignment = TextAlignmentType.Left;
            stContentLeft.Font.Size = 10;
            Style stContentRight = wb.Styles[wb.Styles.Add()];
            stContentRight.HorizontalAlignment = TextAlignmentType.Right;
            stContentRight.Font.Size = 10;

            //赋值给Excel内容
            for (int col = 0; col < dtList.Columns.Count; col++)
            {
                Style stHead = null;
                Style stContent = null;
                //设置表头
                string columnType = dtList.Columns[col].DataType.ToString();
                switch (columnType.ToLower())
                {
                    case "system.string":
                        stHead = stHeadLeft;
                        stContent = stContentLeft;
                        break;
                    default:
                        stHead = stHeadRight;
                        stContent = stContentRight;
                        break;
                }
                //插入表头到Excel内
                cell[0, col].PutValue(dtList.Columns[col].ColumnName);
                cell[0, col].SetStyle(stHead);

                for (int row = 0; row < dtList.Rows.Count; row++)
                {
                    object _value=dtList.Rows[row][col];
                    if (_value != Convert.DBNull)
                    {
                        //插入内容到Excel内
                        switch (columnType.ToLower())
                        {
                            case "system.int32":
                                cell[row + 1, col].PutValue(Convert.ToInt32(_value));
                                break;
                            case "system.int64":
                                cell[row + 1, col].PutValue(Convert.ToInt64(_value));
                                break;
                            case "system.datetime":
                                cell[row + 1, col].PutValue(Convert.ToDateTime(_value).ToString("yyyy/M/d HH:mm"));
                                break;
                            case "system.boolean":
                                cell[row + 1, col].PutValue(Convert.ToBoolean(_value));
                                break;
                            default:
                                cell[row + 1, col].PutValue(_value.ToString());
                                break;
                        }
                    }
                    cell[row + 1, col].SetStyle(stContent);
                }
            }
            wb.Save(path);

            return ExportFilesPath + fileName + etsName;
        }
        #endregion

        private static void putValue(Cells cell, object value, int row, int column, Style st)
        {
            cell[row, column].PutValue(value);
            cell[row, column].SetStyle(st);
        }

        #region  将easyUI grid数据直接导出生成Excel
        /// <summary>
        /// 将easyUI grid数据直接导出生成Excel
        /// </summary>
        /// <param name="data">数据内容</param>
        /// <param name="fileName">excel文件名</param>
        /// <returns>返回文件路径</returns>
        public static string GridToExcel(string data, string fileName)
        {
            //获取前台post提交的数据  
            //定义生成文件的目录，获取绝对地址  
            string pathToFiles = System.Web.HttpContext.Current.Server.MapPath("/ExcelFile/UploadFile");
            //定义生成文件的名称  
            string fname = fileName + ".xls";
            //组合成文件的路径  
            string path = @"" + pathToFiles + "\\" + fname;

            //判断是否已经存在文件  
            if (!System.IO.File.Exists(path))
            {
                //新建文件，并写入数据  
                System.IO.File.WriteAllText(path, data, Encoding.UTF8);
            }
            else
            {
                //文件已存在，添加写入数据  
                System.IO.File.AppendAllText(path, data, Encoding.UTF8);//如果是gb2312的xml申明，第三个编码参数修改为Encoding.GetEncoding(936)  
            }
            return ExportFilesPath + fname;
        }
        #endregion

    }
}
