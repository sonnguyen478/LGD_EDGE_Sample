using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.Common.Util
{
    /// <summary>
    ///  외부 프로그램 실행
    /// </summary>
    public class CEXProcessRun
    {
        // 메세지를 상위로 전달 하기위한 event
        public delegate void MessageEventHandler(string _msg);
        public event MessageEventHandler MessageEvent = null;

        #region [ 생성자 ]
        public CEXProcessRun()
        { }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="_a">파일명</param>
        /// <param name="_b">전체파일경로</param>
        public CEXProcessRun(string _a, string _b)
        {
            this.sFileName = _a;
            this.sFileDirectory = _b;
        }
        #endregion [ 생성자 ]

        #region [ process 간 메세지 전달 API  정의 ]
        const int WM_COPYDATA = 0x4A;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, ref COPYDATASTRUCT lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, ref COPYDATASTRUCT lParam);

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
            public string lpData;
        }
        #endregion [ process 간 메세지 전달 API 정의 ]

        #region [ 외부 프로그램 실행관련 변수 ]

        private bool sEventhandler = false; //   프로그램 시작 여부
        private string sFileName = "InspectionSearch.exe"; //   프로그램 명
        private string sFileDirectory = "C:\\MainBuck-Search\\";//Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public string FileName
        {
            set { this.sFileName = value; }
            get { return this.sFileName; }
        }

        public string FileDirectory
        {
            set { this.sFileDirectory = value; }
            get { return this.sFileDirectory; }
        }


        public bool Eventhandler
        {
            set { this.sEventhandler = value; }
            get { return this.sEventhandler; }
        }

        #endregion [ 외부 프로그램 실행관련 변수 ]

        #region [ 외부 실행파일 종료 Event ]
        public void cmdProcess_Exited(object sender, System.EventArgs e)
        { 
            //MessageBox.Show("외부 프로그램 실행이 종료되었습니다.");
            // this.spcEventhandler = false;
            // timer1.Enabled = false;
            //this.mMsgVision("검색 프로그램 Close");     

            this.sEventhandler = false; // 외부 프로그램이 종료되면 spcEventhandler 전역변수를 false 로 전환

            if (null != MessageEvent)
            {
                MessageEvent("Close");// 실행 종료 메세지
            }
        }
        #endregion [ 외부 실행파일 종료 Event ]

        #region [ process 간 메세지 전달 ]
        public void OnButtonSend(string _msg)
        {
            if (string.IsNullOrEmpty(_msg))
            {
                return;
            }

            System.Diagnostics.Process[] pro = System.Diagnostics.Process.GetProcessesByName(this.sFileName);

            // debug 모드 일때
            if (pro.Length == 0) pro = System.Diagnostics.Process.GetProcessesByName(this.sFileName + ".vshost");


            if (pro.Length > 0)
            {
                byte[] buff = System.Text.Encoding.Default.GetBytes(_msg);

                COPYDATASTRUCT cds = new COPYDATASTRUCT();
                cds.dwData = IntPtr.Zero;
                cds.cbData = buff.Length + 1;
                cds.lpData = _msg;

                SendMessage(pro[0].MainWindowHandle, WM_COPYDATA, 0, ref cds);
            }
        }
        #endregion [process 간 메세지 전달 ]

        #region [ 외부실행파일 실행 ]
        public string mRunProcess()
        {
            string errormsg = "";

            try
            {
                System.Diagnostics.Process UserProcess = new System.Diagnostics.Process();
                UserProcess.StartInfo.UseShellExecute = true;
                UserProcess.StartInfo.WorkingDirectory = this.sFileDirectory;
                UserProcess.StartInfo.FileName = this.sFileName;
                UserProcess.StartInfo.CreateNoWindow = true;
                // UserProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden; // dos command 창을 열지않고 실행
                UserProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                //UserProcess.StartInfo.Arguments = "-1"; //argument가 필요없으면 삭제하세요.

                UserProcess.EnableRaisingEvents = true;
                UserProcess.Exited += new EventHandler(cmdProcess_Exited); // 프로그램 종료 이벤트에 cmdProcess_Exited 메소드 추가
                this.Eventhandler = true;

                UserProcess.Start();
                // this.mMsgVision("검색 프로그램 OPEN");
            }
            catch (Exception ex)
            {
                //this.mMsgVision("검색프로그램을 실행하지 못했습니다.");
                //this.mMsgVision(string.Format("{0}{1} 에 파일이 없습니다.",this.exRun.FileDirectory , this.exRun.FileName));
                //CLog.FileWrite_Str("검색 프로그램 실행 에러 : " + ex.Message.ToString(), CLog.eLogType.EXCEPTION);
                this.Eventhandler = false;
                errormsg = ex.ToString();
            }

            return errormsg;
        }
        #endregion
    }
}
