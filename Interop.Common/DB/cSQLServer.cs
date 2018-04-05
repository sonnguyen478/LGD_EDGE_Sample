


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using Interop.Common.Util;

namespace Interop.Common.DB
{
    #region  [ Table Struct 정의 ]
    //도어 이력
    public struct InspDoorHistory
    {
        public string Date_Time;
        public string Car_Type;
        public string SeqNo;
        public string VIN;
        public string BodyNo;
        public string SpecName;

        public string InspResult;

        public double[] InspDoorValue;

        //public string doorFRT_FLR;
        //public string doorRR_FLR01;
        //public string doorRR_FLR02;
        //public string doorSIDE_LH;
        //public string doorSIDE_RH;
        //public string doorCRP;
        //public string doorBB;

        public string Apply_YN;

        public void clearInspDoorHistory()
        {
            this.Date_Time = string.Empty;
            this.Car_Type = string.Empty;
            this.SeqNo = string.Empty;
            this.VIN = string.Empty;
            this.BodyNo = string.Empty;
            this.SpecName = string.Empty;
            this.InspResult = string.Empty;

            //this.doorFRT_FLR = string.Empty;
            //this.doorRR_FLR01 = string.Empty;
            //this.doorRR_FLR02 = string.Empty;
            //this.doorSIDE_LH = string.Empty;
            //this.doorSIDE_RH = string.Empty;
            //this.doorCRP = string.Empty;
            //this.doorBB = string.Empty;

            for (int idx = 0; idx < InspDoorValue.Length; idx++)
            {
                this.InspDoorValue[idx] = 0;
            }

            this.Apply_YN = string.Empty;
        }
    }

    //메인벅 이력
    public struct InspMainBuckHistory
    {
        public string Date_Time;
        public string Car_Type;
        public string SeqNo;
        public string VIN;
        public string BodyNo;
        public string SpecName;
        public string WeldingGB; // 용접 전 후
        public string SideGB; // LH/RH

        public string InspResult;

        public double[] InspMainBuckValue;

        public string mainbuckFRT_FLR;
        public string mainbuckRR_FLR01;
        public string mainbuckRR_FLR02;
        public string mainbuckSIDE_LH;
        public string mainbuckSIDE_RH;
        public string mainbuckCRP;
        public string mainbuckBB;

        public string Apply_YN;
    }

    //도어 셋팅
    public struct InspDoorSettings
    {
        public string doorDateTime;
        public string doorApplyDate;
        public string doorCarType;
        public string doorSpecName;

        public double doorOverSpec;

        public double[] settingDoorMin;
        public double[] settingDoorMax;
        public double[] settingDoorOffset;
    }

    //메인벅 셋팅
    public struct InspMainbuckSettings
    {
        public string mainBuckDateTime;
        public string mainBuckApplyDate;
        public string mainBuckCarType;
        public string mainBuckSpecName;

        public string mainBuckWeldingGB;
        public string mainBuckSideGB;

        public double[] settingMainBuckMin;
        public double[] settingMainBuckMax;
        public double[] settingMainBuckOffset;
    }
    public struct InspFemPartsSettings
    {
        public string femCarType;
        public string femSideGB;
        public float femInsp01;
        public float femInsp02;
        public float femInsp03;
        public float femInsp04;
        public float femOffset01;
        public float femOffset02;
        public float femOffset03;
        public float femOffset04;

        public float femInspNew01;
        public float femInspNew02;
        public float femOffsetNew01;
        public float femOffsetNew02;

        public float femOverSpec;
    }

    //차량정보 -FA MES
    public struct CarNo_Info
    {
        public string seqNo;
        public string carNo;
        public string carType;
    }

    #endregion  [ Table Struct 정의 ]

    public class cSQLServer
    {
        #region [ 변수 선언 ]
        private SqlConnection connection = null;
        private SqlCommand command = null;
        private SqlDataReader dataReader = null;

        private string connectionStr = string.Empty;

        private string db_IP = string.Empty;
        private string db_name = string.Empty;
        private string db_id = string.Empty;
        private string db_pw = string.Empty;

        public string DB_IP
        {
            get { return this.db_IP; }
            set { this.db_IP = value; }
        }

        public string DB_NAME
        {
            get { return this.db_name; }
            set { this.db_name = value; }
        }

        public string DB_ID
        {
            get { return this.db_id; }
            set { this.db_id = value; }
        }

        public string DB_PW
        {
            get { return this.db_pw; }
            set { this.db_pw = value; }
        }

        #endregion [ 변수 선언 ]

        #region [ DB 연결 ]

        public bool IsConnect()
        {
            //bool result = false;
            //try
            //{
            //    if ( connection == null )
            //    {
            //        result = false;
            //    }
            //    else
            //    {
            //        if ( connection.State == ConnectionState.Open )
            //        {
            //            result = true;
            //        }
            //        else
            //        {
            //            result = false;
            //        }
            //    }
            //}
            //catch ( Exception ex )
            //{
            //    System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            //    result = false;
            //}
            //return result;

            if (connection == null) return false;
            return (connection.State == ConnectionState.Open ? true : false);
        }

        public cSQLServer()
        {
            //connectionStr = "SERVER=" + Environment.MachineName + @"\SQLEXPRESS;DATABASE=" + db_name + ";UID=" + db_id + ";PWD=" + db_pw;
            //this.SQLOpen();
        }
        public void CloseDataBase()
        {
            if (connection != null)
            {
                try
                {
                    if (IsConnect()) connection.Close();
                }
                catch { }

                connection = null;
            }
        }

        #endregion [ DB 연결 ]

        ~cSQLServer()
        {
            CloseDataReader();
            CloseDataCommand();
            CloseDataBase();
        }

        public string SQLOpen()
        {
            try
            {
                if ((false == string.IsNullOrEmpty(this.db_IP)) && (false == string.IsNullOrEmpty(this.db_name)) &&
                     (false == string.IsNullOrEmpty(this.db_id)) && (false == string.IsNullOrEmpty(this.db_pw)))
                {
                    connectionStr = string.Format("SERVER={0};DATABASE={1};UID={2};PWD={3}", this.db_IP, this.db_name, this.db_id, this.db_pw);

                    if (this.DB_IP.IndexOf("SQLEXPRESS") > -1 || this.DB_IP.IndexOf("local") > -1)// || this.DB_IP.IndexOf("127.0.0.1") > -1)
                    {
                        // Local 이면
                    }
                    else
                    {
                        // Remote 이면 남기지 않는다.
                        //connectionStr = string.Format("integrated security=SSPI;SERVER={0};DATABASE={1};UID={2};PWD={3};Pooling=false;Connect Timeout=3;", this.db_IP, this.db_name, this.db_id, this.db_pw);
                        connectionStr = string.Format("SERVER={0};DATABASE={1};UID={2};PWD={3};Pooling=false;", this.db_IP, this.db_name, this.db_id, this.db_pw);
                    }

                    if (connection == null)
                    {
                        connection = new SqlConnection(connectionStr);
                        connection.Open();
                    }

                    if (connection.State != ConnectionState.Open) //== ConnectionState.Closed)
                    {
                        connection.Open();
                    }
                }
                else
                {
                    return "Open Error";
                }
            }
            catch (Exception ex)
            {
                this.SetExectionLog("SQLOpen", ex.ToString(), "");
                return "SQLOpen " + ex.ToString();
            }

            return "";
        }

        #region [ 기본 Local 명령 ]
        public int SQLExecute(string sql)
        {
            int indexReturn = 0;

            try
            {
                // if (connection == null || connection.State != ConnectionState.Open)
                if (IsConnect() == false)
                {
                    SQLOpen();
                }

                command = new SqlCommand(sql, connection);
                indexReturn = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.SetExectionLog("SQLExecute", ex.ToString(), sql);
                indexReturn = 0;
            }
            finally
            {
                CloseDataCommand();
                // if (IsConnect()) connection.Close();
            }

            return indexReturn;
        }
        private void CloseDataReader()
        {
            if (dataReader != null)
            {
                dataReader.Dispose();
                dataReader = null;
            }
        }
        private void CloseDataCommand()
        {
            if (command != null)
            {
                command.Dispose();
                command = null;
            }
        }
        public DataTable GetSelectTable(string _sqlString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(this.connectionStr))
                {
                    sqlConn.Open();
                    using (SqlDataAdapter sqlDA = new SqlDataAdapter(_sqlString, sqlConn))
                    {
                        sqlDA.Fill(dt);
                    }

                    if (sqlConn.State == ConnectionState.Open) sqlConn.Close();
                }
            }
            catch (Exception ex)
            {
                this.SetExectionLog("GetSelectTable", ex.ToString(), _sqlString);

            }
            finally
            {
                CloseDataCommand();
                CloseDataReader();
            }
            return dt;
        }
        #endregion [ 기본 Local 명령 ]

    
        private void SetExectionLog(string _classId, string _ex, string _seq)
        {
            cLog.FileWrite_Str( string.Format( "Class : {0} \n ex.tostring : {1} \n SQL : {2}", _classId, _ex, _seq ), cLog.eLogType.EXCEPTION );           
        }






        public DataTable fnGetSettingsLast(string _model)
        {
            DataTable makeData = new DataTable();
            string strSQL = string.Empty;
            try
            {
                strSQL = string.Format( "SELECT * FROM TB_SETTINGS WHERE MODEL = '{0}'", _model );
                makeData = GetSelectTable( strSQL );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
                makeData = null;
            }
            return makeData;
        }






    }
}