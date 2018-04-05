using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Interop.Common.Util
{
    public sealed class cLog
    {
        private static object LockObject = new object();

        public enum eLogType
        {
            LOG = 1,
            COMM = 2,
            COMPLETE = 3,
            EXCEPTION = 4,
            ALARM = 5,
            SETTING = 6,
            RUNTIME = 7,
            TEST = 99
        }

        static string path_Log = string.Empty;
        static string path_Basic = @"\BASIC\";
        static string path_Comm = @"\COMM\";
        static string path_Complete = @"\COMPLETE\";
        static string path_Exception = @"\EXCEPTION\";
        static string path_Alarm = @"\ALARM\";
        static string path_Setting = @"\SETTING\";
        static string path_Test = @"\DB\";

        public static void mPathLog(string sPath)
        {
            path_Log = sPath;
        }


        public static void FileWrite_Str(string _str, eLogType _log_type)
        {
            lock ( LockObject )
            {
                string strPath = "";
                string strLog = "";
                //string month = "";
                string days = "";
                string dateTime = "";

                DateTime date_now = DateTime.Now;

                //month = date_now.ToString("MM") + "월\\";
                days = date_now.ToString( "dd" ) + " Day.log";
                dateTime = date_now.ToString( "yyyy-MM-dd HH:mm:ss" );

                switch ( _log_type )
                {
                    case eLogType.LOG:
                        strPath = path_Basic;
                        break;
                    case eLogType.COMM:
                        strPath = path_Comm;
                        break;
                    case eLogType.COMPLETE:
                        strPath = path_Complete;
                        break;
                    case eLogType.EXCEPTION:
                        strPath = path_Exception;
                        break;
                    case eLogType.ALARM:
                        strPath = path_Alarm;
                        break;
                    case eLogType.SETTING:
                        strPath = path_Setting;
                        break;
                    case eLogType.TEST:
                        strPath = path_Test;
                        break;
                    default:
                        break;
                }

                strPath = path_Log + strPath;

                strLog = dateTime + " | " + _str;

                FileStream fs = null;
                StreamWriter sw = null;


                try
                {
                    //디렉토리 생성하기
                    if ( !Directory.Exists( strPath ) )
                        Directory.CreateDirectory( strPath );

                    //파일이 없을 경우 1601을 리턴한다.
                    //if (File.GetCreationTime(@strPath + days).Month != date_now.Month) {
                    if ( File.GetLastWriteTime( @strPath + days ).Month != date_now.Month )
                    {
                        ( new FileInfo( @strPath + days ) ).Delete();
                        fs = new FileStream( @strPath + days, FileMode.Create, FileAccess.Write, FileShare.None );
                    }
                    else
                    {
                        fs = new FileStream( @strPath + days, FileMode.Append, FileAccess.Write, FileShare.None );
                    }

                    sw = new StreamWriter( fs, System.Text.Encoding.UTF8 );

                    sw.WriteLine( strLog );
                    sw.Flush();
                    fs.Flush();
                }
                catch ( Exception ex )
                {
                    System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
                    //System.Windows.Forms.MessageBox.Show("Error:FileWrite_Str() " + ex.ToString());
                }
                finally
                {
                    if ( sw != null )
                    {
                        sw.Close();
                        sw.Dispose();
                        sw = null;
                    }

                    if ( fs != null )
                    {
                        fs.Close();
                        fs.Dispose();
                        fs = null;
                    }
                }
            }// end Lock
        }// end method
    }
}