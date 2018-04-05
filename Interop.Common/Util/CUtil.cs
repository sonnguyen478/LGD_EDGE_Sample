using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.Data;
using System.Windows.Forms;

namespace Interop.Common.Util
{
    public sealed class CUtil
    {
        /// <SUMMARY> 
        /// Delay 함수 MS 
        /// </SUMMARY> 
        /// <PARAM name="MS">(단위 : MS)</PARAM> 
        public static DateTime mDelay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan( 0, 0, 0, 0, MS );
            DateTime AfterWards = ThisMoment.Add( duration );

            while ( AfterWards >= ThisMoment )
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }
        public static int mGetHDDFreeSpace(string _DriveName)
        {
            int rtnPer = 0;
            System.IO.DriveInfo[] allDrives = System.IO.DriveInfo.GetDrives();

            foreach ( System.IO.DriveInfo d in allDrives )
            {
                if ( d.Name.Substring( 0, 1 ).Equals( _DriveName ) )
                {
                    if ( d.IsReady == true )
                    {
                        rtnPer = 100 - (int)( (double)d.AvailableFreeSpace / d.TotalSize * 100 );
                    }
                }
            }
            return rtnPer;
        }
        public static bool IsNumeric(string s)
        {
            float output;
            return float.TryParse( s, out output );
        }
        public static int mStringToInt(string _tmp)
        {
            int iTemp = 0;
            int.TryParse( _tmp, out iTemp );
            return iTemp;
        }

        public static bool mStringToBool(string _tmp)
        {
            bool bTemp = false;
            bool.TryParse( _tmp, out bTemp );
            return bTemp;
        }

        #region [ 팝업 윈도우 Close ]
        public static void activatePopupClose()
        {
            activatePopupClose( "frmChartPopup" );
        }

        private static void activatePopupClose(string _closeFormName)
        {
            try
            {
                foreach ( System.Windows.Forms.Form targetForm in System.Windows.Forms.Application.OpenForms )
                {
                    if ( targetForm.Name == _closeFormName )
                    {
                        targetForm.Close();
                        targetForm.Dispose();
                        return;
                    }
                }
            }
            catch
            {
                return;
            }
        }
        #endregion [  팝업 윈도우 Close ]

        #region [ 숫자 소수점 아래 자리수 맞추기 ]

        /// <summary>
        ///  특정 소수점 자리수까지 표시한다.
        ///   _digits+1 자리에서 반올림함.
        /// </summary>
        /// <param name="_value">double</param>
        /// <param name="_digits">표시할 소수점</param>
        /// <returns></returns>
        public static double NumberToNumber(double _value, int _digits)
        {
            return Convert.ToDouble( _value.ToString( "F" + _digits, System.Globalization.CultureInfo.InvariantCulture ) );
        }


        public static int StringToInt(string _value)
        {
            int rtn = 0;
            Int32.TryParse( _value.Trim(), out rtn );
            return rtn;
        }

        /// <summary>
        /// 문자를 Double 로 리턴
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static double StringToDouble(string _value)
        {
            double rtn = 0;
            double.TryParse( _value.Trim(), out rtn );
            return rtn;
        }
        /// <summary>
        ///  문자를 Flaot 로 변환
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static float StringToFloat(string _value)
        {
            float rtn = 0;
            float.TryParse( _value.Trim(), out rtn );
            return rtn;
        }
        /// <summary>
        /// 문자를 숫자로 변경 후 다시 문자로 변경. 소수점 아래 3자리까지 표기
        /// </summary>
        /// <param name="_value">숫자문자</param>
        /// <returns>string</returns>
        public static string DoubleString_To_DoubleString(string _value)
        {
            return DoubleString_To_DoubleString( _value, 3 ); // 2013.12.20 2에서 3으로 변경
        }

        /// <summary>
        /// 문자를 숫자로 변경 후 다시 문자로 변경. 소수점 아래 3자리까지 표기
        /// </summary>
        /// <param name="_value">숫자 OBJECT</param>
        /// <returns></returns>
        public static string StringToDoubleString(object _value)
        {
            return StringToDoubleString( _value, 3 );// 2013.12.20 2에서 3으로 변경
        }

        /// <summary>
        /// 문자를 숫자로 변경 후 다시 문자로 변경
        /// </summary>
        /// <param name="_value">숫자문자</param>
        /// <param name="_digits">소수점아래 표기할 길이</param>
        /// <returns>string</returns>
        public static string DoubleString_To_DoubleString(string _value, int _digits)
        {
            double rtn = 0;
            double.TryParse( _value.Trim(), out rtn );

            return NumberToString( rtn, _digits );
        }

        /// <summary>
        /// 문자를 숫자로 변경 후 다시 문자로 변경
        /// </summary>
        /// <param name="_value">숫자 OBJECT</param>
        /// <param name="_digits">소수점아래 표기할 길이</param>
        /// <returns>string</returns>
        public static string StringToDoubleString(object _value, int _digits)
        {
            double rtn = 0;
            double.TryParse( _value.ToString().Trim(), out rtn );

            return NumberToString( rtn, _digits );
        }


        /// <summary>
        /// 문자 정수를 받아서  double 로 반환 후 다시 string 
        /// </summary>
        /// <param name="_value"></param>
        /// <returns>string</returns>
        public static string IntString_To_DoubleString(string _value)
        {
            return IntString_To_DoubleString( _value, 2 );
        }

        public static string IntString_To_DoubleString(string _value, int _digits)
        {
            int tmp = 0;

            int.TryParse( _value, out tmp );

            return NumerToStirngPoint( tmp, _digits, _digits + 1 );

        }
        public static float mFloatToDigits(float _value, int _digits)
        {

            float xx = (int)System.Math.Pow( 10, _digits );
            float t = 0;
            t = ( (int)(float)( _value * xx ) ) / xx;

            return t;
        }
        #region  [ 정수를 소수점이 있는 숫자로 변환 ]

        /// <summary>
        /// 숫자을 문자로 변환 ( 소수점에서 반올림 )
        ///  2째자리까지 표시 하게되면 3째자리에서 반올림함.
        /// </summary>
        /// <param name="_value">float</param>
        /// <param name="_digits">변환할 수소점자리수</param>
        /// <returns></returns>
        public static string NumberToString(float _value, int _digits)
        {
            return NumberToString( (double)_value, _digits );
        }

        /// <summary>
        /// 숫자을 문자로 변환 ( 소수점에서 반올림 )
        ///  2째자리까지 표시 하게되면 3째자리에서 반올림함.
        /// </summary>
        /// <param name="_value">double</param>
        /// <param name="_digits">변환할 수소점</param>
        /// <returns></returns>
        public static string NumberToString(double _value, int _digits)
        {
            //http://msdn.microsoft.com/ko-kr/library/dwhawy9k(v=vs.90).aspx

            // 소수점 반올림
            return _value.ToString( "F" + _digits, System.Globalization.CultureInfo.InvariantCulture );
        }

        /// <summary>
        /// 숫자을 문자로 변환 ( 소수점에서 반올림 )
        ///  2째자리까지 표시 하게되면 3째자리에서 반올림함.
        /// </summary>
        /// <param name="_value">int</param>
        /// <param name="_digits">변환할 수소점자리수</param>
        /// <returns></returns>
        public static string NumberToStirng(int _value, int _digits)
        {
            // 정수의 특정 값만큼 짜름
            // NumerToStirng(12345678 , 2) ==> 12345700 // 정수 2째자리에서 반올림
            // NumerToStirng(12345678 , 1) ==> 12345780 // 정수 1째 자리에서 반올림
            //NumerToStirng(12345678 , 0) ,NumerToStirng(12345678 , -1) ==> 12345678 //==> 원래값 그대로 리턴


            if ( _digits > 0 )
            {
                return ( System.Math.Round( (float)_value / System.Math.Pow( 10, _digits ) ) * System.Math.Pow( 10, _digits ) ).ToString();
            }
            else
            {
                return _value.ToString();
            }
        }

        /// <summary>
        /// 숫자을 문자로 변환 ( 소수점에서 반올림 )
        ///  2째자리까지 표시 하게되면 3째자리에서 반올림함.
        /// </summary>
        /// <param name="_value">int</param>
        /// <param name="_digits">변환할 수소점자리수</param>
        /// <returns></returns>
        public static string NumerToStirngPoint(int _value, int _digits)
        {
            // 정수를 소수점으로만 표시
            // NumerToStirngPoint(123456,3) ==> 123.456
            // NumerToStirngPoint(123456,2) ==> 1234.56

            if ( _digits > 0 )
            {
                return NumberToString( (double)_value / System.Math.Pow( 10, _digits ), _digits );

            }
            else
            {
                return _value.ToString();
            }
        }

        /// <summary>
        /// 정수를 소수로 변경 후 반올림
        /// 소수점으로 변환한 것이 반올림자리보다 클수가 없다
        /// </summary>
        /// <param name="_value">int</param>
        /// <param name="_digits">변환할 수소점자리수</param>
        /// <param name="_roundDigits">표시할 소수점자리수</param>
        /// <returns></returns>
        public static string NumerToStirngPoint(int _value, int _digits, int _roundDigits)
        {
            // Ex) NumerToStirngPoint(123456,2,3) ==> 1234.56 이 된다.
            //     NumerToStirngPoint(123456, 3, 2) ==> 123.46
            //     NumerToStirngPoint(123456, 3, 1) ==> 123.5
            //     NumerToStirngPoint(123456, 3, 3) ==> 123.456 ,즉  NumerToStirngPoint(123456, 3 ) 와 동일
            //     NumerToStirngPoint(123456, 3, 4) ==> 123.456 ,즉  NumerToStirngPoint(123456, 3 ) 와 동일

            if ( _digits > 0 && _roundDigits > 0 )
            {
                if ( _digits > _roundDigits )
                {
                    // 반올림 해야 하는 숫자가 작아야 한다.
                    return NumberToString( (double)_value / System.Math.Pow( 10, _digits ), _roundDigits );
                }
                else
                {
                    return NumberToString( (double)_value / System.Math.Pow( 10, _digits ), _digits );
                }

            }
            else
            {
                return _value.ToString();
            }
        }

        /// <summary>
        /// 정수를 소수로 변경 후 반올림
        /// 소수점으로 변환한 것이 반올림자리보다 클수가 없다
        /// </summary>
        /// <param name="_value">object</param>
        /// <param name="_digits">변환할 수소점자리수</param>
        /// <param name="_roundDigits">표시할 소수점자리수</param>
        /// <returns></returns>
        public static string NumerToStirngPoint(object _value, int _digits, int _roundDigits)
        {
            return NumerToStirngPoint( _value.ToString(), _digits, _roundDigits );
        }


        /// <summary>
        /// 정수를 소수로 변경 후 반올림
        /// 소수점으로 변환한 것이 반올림자리보다 클수가 없다
        /// </summary>
        /// <param name="_value">string</param>
        /// <param name="_digits">변환할 수소점자리수</param>
        /// <param name="_roundDigits">표시할 소수점자리수</param>
        /// <returns></returns>
        public static string NumerToStirngPoint(string _value, int _digits, int _roundDigits)
        {
            int tmp = 0;
            int.TryParse( _value, out tmp );

            return NumerToStirngPoint( tmp, _digits, _roundDigits );
        }

        /// <summary>
        /// 정수를 소수로 변경, 소수점자리:2, 반올림자리:3
        /// ex) 12345 => 12.34 , 45679=>45.68
        /// </summary>
        /// <param name="_value">string</param>
        public static string NumerToStirngPointDefault(string _value)
        {

            return NumerToStirngPoint( _value, 3, 3 );
        }
        #endregion   [ 정수를 소수점이 있는 숫자로 변환 ]

        #endregion [ 숫자 소수점 아래 자리수 맞추기 ]

        #region [ DataGridView 관련 함수 ]
        public static void convertDataFromGridView(System.Windows.Forms.DataGridView target, string carType)
        {
            convertDataFromGridView( target, carType, false );
        }
        public static void convertDataFromGridView(System.Windows.Forms.DataGridView target, string carType, bool _deaCaInfo)
        {
   
            string[] valueData;
            string[] strTempData;
            string[] _arrData = null;
            string[] _arrData2 = null;

            string strTemp = string.Empty;
            System.Data.DataTable makeData = new System.Data.DataTable();
            System.Data.DataRow makeRow = null;
            System.Data.DataColumn makeColumn = null;

            int tmpIndex1 = 0;
            int tmpIndex2= 0;
            try
            {
                valueData = new string[ target.Rows.Count - 1 ];

                for ( int idx = 0 ; idx < target.Rows.Count - 1 ; idx++ )
                {
                    valueData[ idx ] = target[ 0, idx ].Value.ToString();
                }

                _arrData = new string[  4] { "DateTime", "SeqNo", "CarType", "BodyNo" };
                _arrData2 = new string[ 7 ] { "FTR FLR", "RR FLR", "RR FLR2", "SIDE LH" ,"SIDE RH", "CRP", "BB" };

                for ( int idx = 0 ; idx < _arrData.Length ; idx++ )
                {
                    makeColumn = new DataColumn( _arrData[ idx ], System.Type.GetType( "System.String" ) );
                    makeData.Columns.Add( makeColumn );
                }

                for ( int idx = 1 ; idx < target.Columns.Count ; idx++ )
                {
                    makeColumn = new DataColumn( target.Columns[ idx ].HeaderText.Trim(), System.Type.GetType( "System.String" ) );
                    makeData.Columns.Add( makeColumn );
                }
 
                if ( _deaCaInfo )
                {
                    for ( int idx = 0 ; idx < _arrData2.Length ; idx++ )
                    {
                        makeColumn = new DataColumn( _arrData2[ idx ], System.Type.GetType( "System.String" ) );
                        makeData.Columns.Add( makeColumn );
                    }
                }

                for ( int idx = 0 ; idx < target.Rows.Count - 1 ; idx++ )
                {

                    tmpIndex1 = idx;

                    try
                    {
                        makeRow = makeData.NewRow();
                        strTempData = valueData[idx].Split('/');
                        if ( strTempData == null )
                        {
                            makeRow[ 0 ] = "";
                            makeRow[ 1 ] = "0";
                            makeRow[ 2 ] = "0";
                            makeRow[ 3 ] = "0";

                        } 
						else if( strTempData.Length == 1 && strTempData[0].Equals( "NONE_FLAG" ) )
                        {
                            makeRow[ 0 ] = "";
                            makeRow[1] = "0";
                            makeRow[2] = "0";
                            makeRow[3] = "0";
                        }
                        else
                        {
                            strTemp = string.Format("'{0}/{1}/{2} {3}:{4}:{5}", strTempData[0].Substring(0, 4), strTempData[0].Substring(4, 2), strTempData[0].Substring(6, 2)
                                                                                             , strTempData[0].Substring(8, 2), strTempData[0].Substring(10, 2), strTempData[0].Substring(12, 2));
                            makeRow[0] = strTemp;
                            makeRow[1] = string.Format("'{0}", strTempData[1].Trim());
                            makeRow[2] = carType;
                            makeRow[3] = string.Format("'{0}", strTempData[3].Trim());

                            
                            //대차
                            if ( _deaCaInfo && makeRow.ItemArray.Length == 22 )
                            {
                                makeRow[ 15 ] = string.Format( "'{000}", strTempData[ 4 ].Trim() );
                                makeRow[ 16 ] = string.Format( "'{000}", strTempData[ 5 ].Trim() );
                                makeRow[ 17 ] = string.Format( "'{000}", strTempData[ 6 ].Trim() );
                                makeRow[ 18 ] = string.Format( "'{000}", strTempData[ 7 ].Trim() );
                                makeRow[ 19 ] = string.Format( "'{000}", strTempData[ 8 ].Trim() );
                                makeRow[ 20 ] = string.Format( "'{000}", strTempData[ 9 ].Trim() );
                                makeRow[ 21 ] = string.Format( "'{000}", strTempData[ 10 ].Trim() );
                            }
                        }     
                    }
                    catch { }
                
                    try
                    {
                      //  if ( !valueData[ idx ].Equals( "NONE_FLAG" ) )
                        if ( makeRow[ 0 ].ToString().Equals(""))
                        {
                        } 
						else  
                        {
                            for ( int jdx = 1 ; jdx < target.Columns.Count ; jdx++ )
                            {
                                tmpIndex2 = jdx;
                                makeRow[ jdx + 3 ] = target[ jdx, idx ].Value.ToString();

                            }

                            makeData.Rows.Add( makeRow );
                        }
                    }
                    catch { }
                }

                target.DataSource = null;
                target.DataSource = makeData;

                //if ( _sortASC )
                //{
                //    target.Sort( target.Columns[ 0 ], System.ComponentModel.ListSortDirection.Ascending );
                //}

            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        public static bool mSetDataGridViewHeadText(System.Windows.Forms.DataGridView _dataGrid, string[] _gstring, int[] _iWidth, DataGridViewContentAlignment[] _align)
        {
            bool result = false;
            try
            {
                for ( int inx = 0 ; inx < _gstring.Length ; inx++ )
                {
                    _dataGrid.Columns[ inx ].Width = _iWidth[ inx ];
                    _dataGrid.Columns[ inx ].HeaderText = _gstring[ inx ];
                    if ( _iWidth[ inx ] == 0 )
                        _dataGrid.Columns[ inx ].Visible = false;
                    _dataGrid.Columns[ inx ].DefaultCellStyle.Alignment = _align[ inx ];
                }
                result = true;
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( "mSetDataGridViewHeadText Error : " + ex.Message.ToString() );
                result = false;
            }
            return result;
        }
        #endregion [ DataGridView 관련 함수 ]

        #region [ Excel 관련 함수 ]
        public static void ExcelExportFromDataGrid(System.Windows.Forms.DataGridView target, string _saveFilePath, int fileTotCnt, int fileCnt)
        {
            int num = 0;
            object missingType = Type.Missing;
            StatusForm _ExcelStatus;

            if ( true == string.IsNullOrEmpty( _saveFilePath ) )
            {
                MessageBox.Show( "저장할 경로와 파일 이름을 지정하여 주십시요", "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            _ExcelStatus = new StatusForm();
            _ExcelStatus.lblFile.Text = string.Format( "(파일 : {0}/{1})", fileCnt, fileTotCnt );
            _ExcelStatus.Show();
            _ExcelStatus.progressBar.Maximum = 100;

            Microsoft.Office.Interop.Excel.Application objApp;
            Microsoft.Office.Interop.Excel._Workbook objBook;
            Microsoft.Office.Interop.Excel.Workbooks objBooks;
            Microsoft.Office.Interop.Excel.Sheets objSheets;
            Microsoft.Office.Interop.Excel._Worksheet objSheet;
            Microsoft.Office.Interop.Excel.Range range;

            _ExcelStatus.progressBar.Value = 5;

            string[] headers = new string[ target.ColumnCount ];
            string[] columns = new string[ target.ColumnCount ];

            for ( int idx = 0 ; idx < target.ColumnCount ; idx++ )
            {
                headers[ idx ] = target.Rows[ 0 ].Cells[ idx ].OwningColumn.HeaderText.ToString();
                num = idx + 65;
                
                if (idx > 25)
                {
                    // 26 이상 이면 즉 A~Z 가지 하고 AA, AB~AZ, BA~BZ.... 로 증가 하게 함.
                    //columns[idx] = "A"+ System.Convert.ToString((char)(num - 26));
                    columns[idx] = System.Convert.ToString((char)(idx / 26 + 65 - 1)) + System.Convert.ToString((char)(num - 26));
                }
                else
                {
                    columns[idx] = Convert.ToString((char)num);
                }
                _ExcelStatus.progressBar.Value++;
            }

            #region BeforeProcess
            System.Diagnostics.Process[] BeforeExcelProcess;
            BeforeExcelProcess = System.Diagnostics.Process.GetProcessesByName( "EXCEL" );
            ArrayList arlProcessID = new ArrayList();
            for ( int i = 0 ; i < BeforeExcelProcess.Length ; i++ )
            {
                arlProcessID.Add( BeforeExcelProcess[ i ].Id );
            }
            #endregion

            try
            {
                objApp = new Microsoft.Office.Interop.Excel.Application();
                objBooks = objApp.Workbooks;
                objBook = objBooks.Add( Missing.Value );
                objSheets = objBook.Worksheets;
                objSheet = (Microsoft.Office.Interop.Excel._Worksheet)objSheets.get_Item( 1 );

                for ( int idx = 0 ; idx < target.ColumnCount ; idx++ )
                {
                    range = objSheet.get_Range( columns[ idx ] + "1", Missing.Value );
                    range.set_Value( Missing.Value, headers[ idx ] );
                }

                _ExcelStatus.progressBar.Value = 60;

                for ( int idx = 0 ; idx < target.RowCount - 1 ; idx++ )
                {
                    for ( int jdx = 0 ; jdx < target.ColumnCount ; jdx++ )
                    {
                        range = objSheet.get_Range( columns[ jdx ] + Convert.ToString( idx + 2 ), Missing.Value );
                        range.set_Value( Missing.Value, target.Rows[ idx ].Cells[ jdx ].Value.ToString() );
                    }
                }

                _ExcelStatus.progressBar.Value = 80;

                objApp.Visible = false;
                objApp.UserControl = false;

                objBook.SaveAs( @_saveFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                                                              missingType, missingType, missingType, missingType,
                                                              Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                                                              missingType, missingType, missingType, missingType, missingType );
                objBook.Close( false, missingType, missingType );

                _ExcelStatus.progressBar.Value = 100;

                _ExcelStatus.Close();
                _ExcelStatus.Dispose();
                _ExcelStatus = null;

                #region AfterProcess
                System.Diagnostics.Process[] AfterExcelProcess;
                AfterExcelProcess = System.Diagnostics.Process.GetProcessesByName( "EXCEL" );
                for ( int i = 0 ; i < AfterExcelProcess.Length ; i++ )
                {
                    if ( !arlProcessID.Contains( AfterExcelProcess[ i ].Id ) )
                    {
                        AfterExcelProcess[ i ].Kill();
                    }
                }
                #endregion

                //MessageBox.Show( "저장 되었습니다.\n\n저장경로 : \n" + _saveFilePath, "저장 성공", MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
            catch ( Exception ex )
            {
                #region AfterProcess
                System.Diagnostics.Process[] AfterExcelProcess;
                AfterExcelProcess = System.Diagnostics.Process.GetProcessesByName( "EXCEL" );
                for ( int i = 0 ; i < AfterExcelProcess.Length ; i++ )
                {
                    if ( !arlProcessID.Contains( AfterExcelProcess[ i ].Id ) )
                    {
                        AfterExcelProcess[ i ].Kill();
                    }
                }
                #endregion

                _ExcelStatus.Close();
                _ExcelStatus.Dispose();
                _ExcelStatus = null;

                System.Diagnostics.Trace.WriteLine( ex );
            }
        }
        #endregion [ Excel 관련 함수 ]

    }
}
