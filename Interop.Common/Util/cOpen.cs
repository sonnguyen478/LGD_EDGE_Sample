using System;
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
        FolderBrowserDialog folderPath = null;
        int inx = 0;
        int inx_next = 0;
        int inx_prev = 0;
        string sFileName = string.Empty;


        // Summary:
        //     Set browser folder
        //
        // Parameters:
        //   _flag:
        //     True     Enable
        //     False    Disable         
        public bool OpenFolder()
        {
            bool bResult = false;

            try
            {
                folderPath = new FolderBrowserDialog();
                bResult = folderPath.ShowDialog() == DialogResult.OK;
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str("FileOpen " + ex.ToString(), cLog.eLogType.LOG);
            }

            return bResult;
        }

        // 파일 Open
        public bool OpenFile(eFileType _eFT, bool _isMultiSelect)
        {
            bool bResult = false;

            ofd = new OpenFileDialog();

            switch (_eFT)
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
                inx_next = -1;
                inx_prev = -1;
                bResult = ofd.ShowDialog() == DialogResult.OK;
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str("FileOpen " + ex.ToString(), cLog.eLogType.LOG);
            }

            return bResult;
        }


        // 파일 숫자
        public int Length()
        {
            if (ofd == null) return 0;
            return ofd.FileNames.Length;
        }

        // 현재 선택한 Index
        public int IndexForward()
        {
            if (ofd == null) return 0;
            return inx_next + 1;
        }

        public int IndexBackward()
        {
            if (ofd == null) return 0;
            return inx_prev + 1;
        }

        // 다음 파일
        public string Next()
        {
            if (ofd == null) return "";

            string retStr = string.Empty;
            if (ofd.FileNames.Length > (inx_next + 1))
            {
                inx_next++;
                if (inx_next % 4 == 3)
                    inx_prev = inx_next - 3;
                retStr = ofd.FileNames[inx_next];
            }
            else
                inx_prev = inx_next - (inx_next % 4);

            return retStr;
        }

        public string Prev()
        {
            if (ofd == null) return "";

            string retStr = string.Empty;
            if (0 <= (inx_prev - 1))
            {
                inx_prev--;
                if (inx_prev % 4 == 0)
                    inx_next = inx_prev + 3;
                retStr = ofd.FileNames[inx_prev];
            }

            return retStr;
        }

        public bool NextSet()
        {
            if (ofd == null) return false;

            bool bResult = false;
            if (ofd.FileNames.Length > (inx_next + 1))
                bResult = true;

            return bResult;
        }

        public bool PrevSet()
        {
            if (ofd == null) return false;

            bool bResult = false;
            if (0 <= (inx_prev - 1))
                bResult = true;

            return bResult;
        }

        // 선택한 현재파일명
        public string CurrFileNameForward()
        {
            if (ofd == null) return "";
            //return Encoding.GetEncoding("EUC-KR").GetString(Encoding.GetEncoding("ISO-8859-1").GetBytes(ofd.FileNames[inx_next].ToString().Trim()));
            return ofd.FileNames[inx_next];
        }

        public string CurrFileNameBackward()
        {
            if (ofd == null) return "";

            return ofd.FileNames[inx_prev];
        }

        //public string Curr()
        //{
        //    if (ofd == null) return ""; 

        //    return ofd.FileNames[inx];
        //}


        public string CurrentCntDisplayForward()
        {
            return string.Format("{0}/{1}", this.IndexForward(), this.Length());
        }

        public string CurrentCntDisplayBackward()
        {
            return string.Format("{0}/{1}", this.IndexBackward(), this.Length());
        }

    }
}
