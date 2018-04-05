using System;
using System.Text;
using System.Runtime.InteropServices;
//using CVB = Cvb.Image;

namespace Interop.Common.Util
{
    //INI 내용은 모두 대문자를 사용하자

    public sealed class cIni
    {
        //public struct sArea {
        //    public double X0;
        //    public double Y0;
        //    public double X1;
        //    public double Y1;
        //    public double X2;
        //    public double Y2;
        //}

        // ---- ini 파일 의 읽고 쓰기를 위한 API 함수 선언 ----
        [DllImport( "kernel32.dll" )]
        private static extern int GetPrivateProfileString(    // ini Read 함수
                    String section,
                    String key,
                    String def,
                    StringBuilder retVal,
                    int size,
                    String filePath);

        [DllImport( "kernel32.dll" )]
        private static extern long WritePrivateProfileString(  // ini Write 함수
                    String section,
                    String key,
                    String val,
                    String filePath);

        static string INIPath = string.Empty;
        static string INIPathFile = string.Empty;

        public cIni(string path)
        {
            INIPath = path;
            INIPathFile = path + @"\SETTING.ini";

            //디렉토리 생성하기
            if ( !System.IO.Directory.Exists( path ) ) System.IO.Directory.CreateDirectory( path );
        }

        public static void mPathIni(string path)
        {
            INIPath = path;
            INIPathFile = path + @"\SETTING.ini";
            //디렉토리 생성하기
            if ( !System.IO.Directory.Exists( path ) ) System.IO.Directory.CreateDirectory( path );
        }

        /// ini파일에서 읽어 오기
        public static String IniReadValue(String Section, String Key)
        {
            StringBuilder temp = new StringBuilder( 255 );

            int i = GetPrivateProfileString( Section, Key, "", temp, 255, INIPathFile );

            return temp.ToString().Trim();
        }

        /// ini파일에 쓰기
        public static void IniWriteValue(String Section, String Key, String Value)
        {
            WritePrivateProfileString( Section, Key, Value, INIPathFile );
        }

        /// ini파일에서 읽어 오기
        public static string IniReadValue(String Section, String Key, string PathFile)
        {
            StringBuilder temp = new StringBuilder( 255 );

            int i = GetPrivateProfileString( Section, Key, "", temp, 255, PathFile );

            return temp.ToString().Trim();
        }

        /// ini파일에 쓰기
        public static void IniWriteValue(String Section, String Key, String Value, string PathFile)
        {
            WritePrivateProfileString( Section, Key, Value, PathFile );
        }


        // public static void ReadProdCnt(ref CGlobal.structProductCnt _stProdCnt)
        // {
        //     string tmp = IniReadValue("PRODUCT_CNT", "TOTAL");
        //     if (!tmp.Equals("")) _stProdCnt.iTotal = Convert.ToUInt32(tmp);
        //     else IniWriteValue("PRODUCT_CNT", "TOTAL", "0");

        //     tmp = IniReadValue("PRODUCT_CNT", "OK");
        //     if (!tmp.Equals("")) _stProdCnt.iOK = Convert.ToUInt32(tmp);
        //     else IniWriteValue("PRODUCT_CNT", "OK", "0");

        //     tmp = IniReadValue("PRODUCT_CNT", "NG");
        //     if (!tmp.Equals("")) _stProdCnt.iNG = Convert.ToUInt32(tmp);
        //     else IniWriteValue("PRODUCT_CNT", "NG", "0");
        // }

        // public static void WriteProdCnt(CGlobal.structProductCnt _stProdCnt)
        // {
        //     IniWriteValue("PRODUCT_CNT", "TOTAL", _stProdCnt.iTotal.ToString());
        //     IniWriteValue("PRODUCT_CNT", "OK", _stProdCnt.iOK.ToString());
        //     IniWriteValue("PRODUCT_CNT", "NG", _stProdCnt.iNG.ToString());
        // }



        //public static void ReadProdCnt_Camera(ref CGlobal.structProductCnt _stProdCnt)
        // {
        //     string tmp = IniReadValue("PRODUCT_CNT_CAMERA", "TOTAL");
        //     if (!tmp.Equals("")) _stProdCnt.iTotal = Convert.ToUInt32(tmp);
        //     else IniWriteValue("PRODUCT_CNT_CAMERA", "TOTAL", "0");

        //     tmp = IniReadValue("PRODUCT_CNT_CAMERA", "OK");
        //     if (!tmp.Equals("")) _stProdCnt.iOK = Convert.ToUInt32(tmp);
        //     else IniWriteValue("PRODUCT_CNT_CAMERA", "OK", "0");

        //     tmp = IniReadValue("PRODUCT_CNT_CAMERA", "NG");
        //     if (!tmp.Equals("")) _stProdCnt.iNG = Convert.ToUInt32(tmp);
        //     else IniWriteValue("PRODUCT_CNT_CAMERA", "NG", "0");
        // }

        // public static void WriteProdCnt_Camera(CGlobal.structProductCnt _stProdCnt)
        // {
        //     IniWriteValue("PRODUCT_CNT_CAMERA", "TOTAL", _stProdCnt.iTotal.ToString());
        //     IniWriteValue("PRODUCT_CNT_CAMERA", "OK", _stProdCnt.iOK.ToString());
        //     IniWriteValue("PRODUCT_CNT_CAMERA", "NG", _stProdCnt.iNG.ToString());
        // }




        #region [ 암호 ]

        public static void ReadAdmin(ref string _value)
        {
            string tmp = IniReadValue( "ADMIN", "ADMIN" );
            if ( !tmp.Equals( "" ) )
            {
                _value = tmp.ToString();
            }
            else
            {
                _value = "1111";
                IniWriteValue( "ADMIN", "ADMIN", _value );
            }
        }

        public static void WriteDAdmin(string _value)
        {
            IniWriteValue( "ADMIN", "ADMIN", _value );
        }

        #endregion
    }
}