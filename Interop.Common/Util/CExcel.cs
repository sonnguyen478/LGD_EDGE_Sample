using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Interop.Common.Util
{
    public sealed class CExcel
    {

        private static object LockObject = new object();

        #region [ 엑셀 저장 대화창 출력 ]
        /// <summary>
        /// 출력한 파일명을 받아서
        /// </summary>
        /// <param name="saveFileDialog1"></param>
        /// <param name="comboBox_carType"></param>
        /// <param name="_excelList">출력내용 배열</param>
        /// <returns></returns>
        public static string ToCSV(System.Windows.Forms.SaveFileDialog saveFileDialog1, string comboBox_carType, string[] _excelList)
        {  
            string strPath = "";
            string dateTime = "";

            DateTime date_now = DateTime.Now;

            //month = date_now.ToString("MM") + "월\\";
            // days = date_now.ToString("dd")  ;
            dateTime = date_now.ToString("yyyyMMdd_HHmmss");

            saveFileDialog1.FileName = dateTime + "_" + comboBox_carType + ".CSV";

            if (saveFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return "";

            strPath = saveFileDialog1.FileName;

           return  ToCSV(strPath, _excelList);
        }
        #endregion [ 엑셀 저장 대화창 출력 ]

        #region [ CSV 출력 ]

        /// <summary>
        /// CSV 출력
        /// </summary>
        /// <param name="_excelName">출력 파일 Full 경로 및 파일명</param>
        /// <param name="_excelList">출력내용 배열</param>
        /// <returns>에러 메세지</returns>
         public static string ToCSV(string _excelName, string[] _excelList)
         { 
             string strErrMsg = "";

            FileStream fs = null;
            StreamWriter sw = null;

            lock (LockObject)
            {
                try
                {
                    fs = new FileStream(_excelName, FileMode.Create, FileAccess.Write, FileShare.None);
                    sw = new StreamWriter(fs, System.Text.Encoding.UTF8);

                    for (int inx = 0; inx < _excelList.Length; inx++)
                    {
                        sw.WriteLine(_excelList[inx]);
                    }

                    sw.Flush();
                    fs.Flush();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                    //System.Windows.Forms.MessageBox.Show("Error:FileWrite_Str() " + ex.ToString());

                    strErrMsg = ex.Message.ToString();
                }
                finally
                {
                    if (sw != null)
                    {
                        sw.Close();
                        sw.Dispose();
                        sw = null;
                    }

                    if (fs != null)
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                }
            }// end Lock

            return strErrMsg;
         }

        #endregion [ CSV 출력 ]


         //public string excelExport(System.Windows.Forms.DataGridView _dataGridView)
         //{
         //    string errMessage = string.Empty;
         //    string _saveFilePath = string.Empty;
         //    System.Windows.Forms.DialogResult _result;

         //    if (_dataGridView.Rows.Count <= 0)
         //    {
         //        //MessageBox.Show("Export 할 Data가 없습니다.\nLog 파일을 Load 하여 주십시요", "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
         //        errMessage = "Export 할 Data가 없습니다";
         //        return errMessage;
         //    }

         //    using (System.Windows.Forms.SaveFileDialog _saveFileDialog = new System.Windows.Forms.SaveFileDialog())
         //    {
         //        _saveFileDialog.DefaultExt = "xls";
         //        _saveFileDialog.Filter = "Excel Files (*.xls)|*.xls";
         //        _saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

         //        _result = _saveFileDialog.ShowDialog();

         //        _saveFilePath = _saveFileDialog.FileName;
         //    }

         //    if (_result == System.Windows.Forms.DialogResult.OK)
         //    {                 
         //        if (true == string.IsNullOrEmpty(_saveFilePath))
         //        {
         //            //MessageBox.Show("저장할 경로와 파일 이름을 지정하여 주십시요", "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
         //            //return;

         //            errMessage = "저장할 경로와 파일 이름을 지정하여 주십시요";
         //        }
         //        else {
         //        errMessage = ToXLS(_saveFilePath, _dataGridView);
         //        }
         //    }
         //    else
         //    {
         //        errMessage = "";
         //    }

         //    return errMessage;
         //}


        // #region [ XLS 출력 ]



        // public string ToXLS(string _fileName, System.Windows.Forms.DataGridView _dataGridView)
        // {
        //     string errMessage=string.Empty;
        //     int num = 0;
        //     object missingType = Type.Missing;

             
        //     //if (_LogLoadFlag == false)
        //     //{
        //     //    //MessageBox.Show("먼저 Log 파일을 Load 하여 주십시요", "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //     //    errMessage = "먼저 Log 파일을 Load 하여 주십시요";
        //     //    return errMessage;
        //     //}

        //         //_ExcelStatus = new StatusForm();


        //         //_ExcelStatus.Show();
        //         //_ExcelStatus.progressBar.Maximum = 100;

        //         Microsoft.Office.Interop.Excel.Application objApp;
        //         Microsoft.Office.Interop.Excel._Workbook objBook;
        //         Microsoft.Office.Interop.Excel.Workbooks objBooks;
        //         Microsoft.Office.Interop.Excel.Sheets objSheets;
        //         Microsoft.Office.Interop.Excel._Worksheet objSheet;
        //         Microsoft.Office.Interop.Excel.Range range;

        //         //_ExcelStatus.progressBar.Value = 5;

        //         string[] headers = new string[_dataGridView.ColumnCount];
        //         string[] columns = new string[_dataGridView.ColumnCount];

        //         for (int idx = 0; idx < _dataGridView.ColumnCount; idx++)
        //         {
        //             headers[idx] = _dataGridView.Rows[0].Cells[idx].OwningColumn.HeaderText.ToString();
        //             num = idx + 65;
        //             columns[idx] = Convert.ToString((char)num);

        //             //_ExcelStatus.progressBar.Value++;
        //         }

        //         #region BeforeProcess
                
        //         System.Diagnostics.Process[] BeforeExcelProcess;
        //         BeforeExcelProcess = System.Diagnostics.Process.GetProcessesByName("EXCEL");
        //         System.  ArrayList arlProcessID = new ArrayList();
        //         for (int i = 0; i < BeforeExcelProcess.Length; i++)
        //         {
        //             arlProcessID.Add(BeforeExcelProcess[i].Id);
        //         }
        //         #endregion

        //         try
        //         {
        //             objApp = new Microsoft.Office.Interop.Excel.Application();
        //             objBooks = objApp.Workbooks;
        //             objBook = objBooks.Add(Missing.Value);
        //             objSheets = objBook.Worksheets;
        //             objSheet = (Microsoft.Office.Interop.Excel._Worksheet)objSheets.get_Item(1);

        //             for (int idx = 0; idx < _dataGridView.ColumnCount; idx++)
        //             {
        //                 range = objSheet.get_Range(columns[idx] + "1", Missing.Value);
        //                 range.set_Value(Missing.Value, headers[idx]);

        //             }

        //             _ExcelStatus.progressBar.Value = 60;

        //             for (int idx = 0; idx < _dataGridView.RowCount - 1; idx++)
        //             {
        //                 for (int jdx = 0; jdx < _dataGridView.ColumnCount; jdx++)
        //                 {
        //                     range = objSheet.get_Range(columns[jdx] + Convert.ToString(idx + 2), Missing.Value);
        //                     range.set_Value(Missing.Value, _dataGridView.Rows[idx].Cells[jdx].Value.ToString());
        //                 }
        //             }

        //             _ExcelStatus.progressBar.Value = 80;

        //             objApp.Visible = false;
        //             objApp.UserControl = false;

        //             objBook.SaveAs(@_saveFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
        //                                                           missingType, missingType, missingType, missingType,
        //                                                           Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
        //                                                           missingType, missingType, missingType, missingType, missingType);
        //             objBook.Close(false, missingType, missingType);

        //             _ExcelStatus.progressBar.Value = 100;

        //             _ExcelStatus.Close();
        //             _ExcelStatus.Dispose();
        //             _ExcelStatus = null;

        //             //releaseObject( objSheet );
        //             //releaseObject( objSheets );
        //             //releaseObject( objBook );
        //             //releaseObject( objBooks );
        //             //objApp.Quit();

        //             #region AfterProcess
        //             System.Diagnostics.Process[] AfterExcelProcess;
        //             AfterExcelProcess = System.Diagnostics.Process.GetProcessesByName("EXCEL");
        //             for (int i = 0; i < AfterExcelProcess.Length; i++)
        //             {
        //                 if (!arlProcessID.Contains(AfterExcelProcess[i].Id))
        //                 {
        //                     AfterExcelProcess[i].Kill();
        //                 }
        //             }
        //             #endregion

        //             //MessageBox.Show("저장 되었습니다.\n\n저장경로 : \n" + _saveFilePath, "저장 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //             errMessage = ""; //"저장 되었습니다.\n\n저장경로 : \n" + _saveFilePath;
        //         }
        //         catch (Exception ex)
        //         {
        //             #region AfterProcess
        //             System.Diagnostics.Process[] AfterExcelProcess;
        //             AfterExcelProcess = System.Diagnostics.Process.GetProcessesByName("EXCEL");
        //             for (int i = 0; i < AfterExcelProcess.Length; i++)
        //             {
        //                 if (!arlProcessID.Contains(AfterExcelProcess[i].Id))
        //                 {
        //                     AfterExcelProcess[i].Kill();
        //                 }
        //             }
        //             #endregion

        //             _ExcelStatus.Close();
        //             _ExcelStatus.Dispose();
        //             _ExcelStatus = null;

        //             System.Diagnostics.Trace.WriteLine(ex);
        //         }
        //     }
        //     else
        //     {
        //         //MessageBox.Show("저장할 경로와 파일 이름을 지정하여 주십시요", "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //         //return;
        //         errMessage = "저장할 경로와 파일 이름을 지정하여 주십시요";
        //         return errMessage;

        //     }

        //     return errMessage;
        // }

        //#endregion [ XLS 출력 ]

    }
}
