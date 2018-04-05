using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace Interop.Common.Util
{
    public sealed class cOpen
    {
        public enum eFileType
        {
            JPGBMP = 0,
            MANTO,
            ALL,
            BMPJPG,
        }

        OpenFileDialog ofd = null;
        int inx = 0;
        string sFileName = string.Empty;

        // 파일 Open
        public bool OpenFile(eFileType _eFT, bool _isMultiSelect)
        {
            bool bResult = false;

            ofd = new OpenFileDialog();

            switch ( _eFT )
            {
                case eFileType.JPGBMP:
                    ofd.Filter = "JPG파일(*.jpg)|*.jpg|BMP파일(*.bmp)|*.bmp|TIF파일(*.tif)|*.tif|ALL 파일(*.*)|*.*";
                    break;
                case eFileType.MANTO:
                    ofd.Filter = "MCF파일(*.MCF)|*.mcf";
                    break;
                case eFileType.ALL:
                    ofd.Filter = "ALL 파일(*.*)|*.*";
                    break;
                case eFileType.BMPJPG:
                    ofd.Filter = "BMP파일(*.bmp)|*.bmp|JPG파일(*.jpg)|*.jpg";
                    break;
            }

            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;
            ofd.Multiselect = _isMultiSelect;

            try
            {
                inx = 0;
                bResult = ofd.ShowDialog() == DialogResult.OK;
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "FileOpen " + ex.ToString(), cLog.eLogType.LOG );
            }

            return bResult;
        }


        // 파일 숫자
        public int Length()
        {
            if ( ofd == null ) return 0;
            return ofd.FileNames.Length;
        }

        // 현재 선택한 Index
        public int Index()
        {
            if ( ofd == null ) return 0;
            return inx + 1;
        }

        // 다음 파일
        public string Next()
        {
            if ( ofd == null ) return "";

            if ( ofd.FileNames.Length <= ++inx )
            {

                // inx = ofd.FileNames.Length - 1;
                inx = 0;

            }
            return ofd.FileNames[ inx ];
        }

        public string Prev()
        {
            if ( ofd == null ) return "";

            if ( 0 > --inx )
            {
                inx = ofd.FileNames.Length - 1; ;
            }

            return ofd.FileNames[ inx ];
        }

        // 선택한 현재파일명
        public string CurrFileName()
        {
            if ( ofd == null ) return "";

            return ofd.FileNames[ inx ];
        }

        //public string Curr()
        //{
        //    if (ofd == null) return ""; 

        //    return ofd.FileNames[inx];
        //}


        public string CurrentCntDisplay()
        {
            return string.Format( "{0}/{1}", this.Index(), this.Length() );
        }

    }
}
