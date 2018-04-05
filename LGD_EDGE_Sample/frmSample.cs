#define UsePLC
//#define UseCam
//#define UseDatabase


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LGD_EDGE_Sample
{
    public partial class frmSample : Form
    {
        private bool bIsDelay = true;
        private bool bIsStartClose = false;
        private bool isInspection = false; // 검사 진행 여부

        private AxCVDISPLAYLib.AxCVdisplay[] axDisplayList; // 화면
        private AxCVIMAGELib.AxCVimage[] axImageList;// Image

        private Interop.Common.Progress.SplashThread loadProgress = null;
        private Interop.Common.DB.cSQLServer localdbConn = null;

        private Interop.Common.PLC.Melsec.CMxComponentDirect cMxCom = null;
        private cBGWorkerEvents.PLCEvents eventPLC = null;
        private cBGWorkerEvents.EventHeartBit eventHeartBit = null;
        private System.Timers.Timer tmrHeartBitPLCCheck = null;

        private cDBInfo.InspSaveMain saveMain;
        private cDBInfo.InspSaveSub[] saveSub = null;

        private int bPCHeartBit = 0;
        private int beforPLCHeartBit = -1;
        public bool[] beforPLCBit = new bool[ 30 ];

        private int nInspCnt = 2;

        public frmSample()
        {
            InitializeComponent();

            loadProgress = new Interop.Common.Progress.SplashThread();
            loadProgress.AllStepCnt = 9;
        }
        private void frmSample_Load(object sender, EventArgs e)
        {
            try
            {
                loadProgress.Open();

                // 1 --> Local Database Connecting..
                loadProgress.UpdateProgress( 1, "Local Database Connecting..." );
                if ( true == bIsDelay ) System.Threading.Thread.Sleep( 300 );

#if UseDatabase
                fnDBConnection();
                if ( false == localdbConn.IsConnect() )
                {
                    MessageBox.Show( this, "Don't Connect local DATA BASE\nProgram Exit", "Error Connect to LocalDB", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    bIsStartClose = true;
                    this.Close();
                }
#endif
                // 2 --> PLC Connecting..
                loadProgress.UpdateProgress( 2, "PLC Connecting..." );
                if ( true == bIsDelay ) System.Threading.Thread.Sleep( 300 );

#if UsePLC
                if (false == fnMxCompomponentOpen())
                {
                    MessageBox.Show(this, "Don't Connect PLC\nProgram Exit", "Error Connect to PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bIsStartClose = true;
                    this.Close();
                }
#endif

                // 3 --> UI Construct & Clear ...
                loadProgress.UpdateProgress( 3, "UI Control Initialize..." );
                if ( true == bIsDelay ) System.Threading.Thread.Sleep( 300 );
                fnDisplayControlsClear();

#if UsePLC
                // 4 --> PC 메모리 영역 초기화...
                loadProgress.UpdateProgress( 4, "PLC - PC Area Initialize..." );
                if ( true == bIsDelay ) System.Threading.Thread.Sleep( 300 );
                fnSetPCMemoryClear();
#endif

#if UseCam
                // 5 --> Cam Initialize
                loadProgress.UpdateProgress( 5, "Cam Connecting..." );
                if ( true == bIsDelay ) System.Threading.Thread.Sleep( 300 );

                fnCAM_Initialize();
                fnCamEventPlus();
                mCVBInit();
#endif

                // 6 --> Program Enviroment Load...
                loadProgress.UpdateProgress( 6, "PC Evviroment Loding..." );
                if ( true == bIsDelay ) System.Threading.Thread.Sleep( 300 );

                saveMain = new cDBInfo.InspSaveMain();
                saveSub = new cDBInfo.InspSaveSub[ nInspCnt ];

                // 7 --> Local Timer 및 기타 설정 완료
                loadProgress.UpdateProgress( 7, "Main UI Loading..." );
                if ( true == bIsDelay ) System.Threading.Thread.Sleep( 300 );
                fnSystemEnviromentSettings( true );

                fnDisplayControlsClear();

                tmrSystem.Start();
#if UsePLC
                bgWorkPLC.RunWorkerAsync();
#endif
            }
            catch ( Exception ex )
            {
            }
            finally
            {
                if ( loadProgress != null )
                {
                    loadProgress.Close();
                    loadProgress.Join();
                    loadProgress = null;
                }
            }
        }
        private void frmSample_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = new DialogResult();
            try
            {
                if ( false == bIsStartClose )
                {
                    result = MessageBox.Show( "Really Exit", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
                }
                if ( ( result == DialogResult.No ) && ( false == bIsStartClose ) )
                {
                    e.Cancel = true;
                }
                else
                {
                    tmrSystem.Stop();
                    tmrHeartBitPLCCheck.Stop();
#if UsePLC
                    fnMxCompomponentClose();
#endif
#if UseCam
                    fnCamEventMinus();
#endif
#if UsePLC
                    if ( bgWorkPLC != null && bgWorkPLC.IsBusy )
                    {
                        bgWorkPLC.CancelAsync();
                    }
#endif
#if UseDatabase
                    fnSetDBConnectClose();
                    if ( localdbConn != null )
                    {
                        localdbConn.CloseDataBase();
                        localdbConn = null;
                    }
#endif
                }
            }
            catch ( Exception ex )
            {
            }
        }


        #region [ PC Control Action ]
        private void btnRunInspection_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void btnImageLoad01_Click(object sender, EventArgs e)
        {
            using ( OpenFileDialog open = new OpenFileDialog() )
            {
                open.InitialDirectory = "c:\\";
                open.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*";
                open.FilterIndex = 2;
                open.RestoreDirectory = true;

                if ( open.ShowDialog() == DialogResult.OK )
                {
                    axCVimage1.LoadImage( open.FileName );

                    Interop.Common.CVB.CCVBOverlay.OverlayLabelRemoveAll( axCVdisplay1 );
                    Interop.Common.CVB.CCVBOverlay.OverlayObjectRemoveAll( axCVdisplay1 );

                    axCVdisplay1.Image = axCVimage1.Image;
                    axCVdisplay1.Refresh();
                }
            }

            //string filePath = @"C:\img01.jpg";
            //axCVimage1.LoadImage( filePath );

            //Interop.Common.CVB.CCVBOverlay.OverlayLabelRemoveAll( axCVdisplay1 );
            //Interop.Common.CVB.CCVBOverlay.OverlayObjectRemoveAll( axCVdisplay1 );

            //axCVdisplay1.Image = axCVimage1.Image;
            //axCVdisplay1.Refresh();
        }
        private void btnImageClear01_Click(object sender, EventArgs e)
        {
#if UseCam
            axImageList[ 0 ].Clear( 0, 0 );
            axDisplayList[ 0 ].Image = axImageList[ 0 ].Image;
            axDisplayList[ 0 ].Refresh();
#endif
        }
        private void btnImageLoad02_Click(object sender, EventArgs e)
        {
            using ( OpenFileDialog open = new OpenFileDialog() )
            {
                open.InitialDirectory = "c:\\";
                open.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*";
                open.FilterIndex = 2;
                open.RestoreDirectory = true;

                if ( open.ShowDialog() == DialogResult.OK )
                {
                    axCVimage2.LoadImage( open.FileName );

                    Interop.Common.CVB.CCVBOverlay.OverlayLabelRemoveAll( axCVdisplay2 );
                    Interop.Common.CVB.CCVBOverlay.OverlayObjectRemoveAll( axCVdisplay2 );

                    axCVdisplay2.Image = axCVimage2.Image;
                    axCVdisplay2.Refresh();
                }
            }

            //string filePath = @"C:\img02.jpg";
            //axCVimage2.LoadImage( filePath );

            //Interop.Common.CVB.CCVBOverlay.OverlayLabelRemoveAll( axCVdisplay2 );
            //Interop.Common.CVB.CCVBOverlay.OverlayObjectRemoveAll( axCVdisplay2 );

            //axCVdisplay2.Image = axCVimage2.Image;
            //axCVdisplay2.Refresh();
        }
        private void btnImageClear02_Click(object sender, EventArgs e)
        {
#if UseCam
            axImageList[ 1 ].Clear( 0, 0 );
            axDisplayList[ 1 ].Image = axImageList[ 1 ].Image;
            axDisplayList[ 1 ].Refresh();
#endif
        }
        private void chkPositive01_CheckedChanged(object sender, EventArgs e)
        {
            if ( chkPositive01.Checked == true )
            {
                chkPositive01.Text = "Negative";
            }
            else
            {
                chkPositive01.Text = "Positive";
            }
        }
        private void chkPositive02_CheckedChanged(object sender, EventArgs e)
        {
            if ( chkPositive02.Checked == true )
            {
                chkPositive02.Text = "Negative";
            }
            else
            {
                chkPositive02.Text = "Positive";
            }
        }
        private void tbMin01_Scroll(object sender, EventArgs e)
        {
            Interop.Common.Util.CDelegate.SetText( EdgeThresholdMin01, tbMin01.Value.ToString() );
        }
        private void tbMax01_Scroll(object sender, EventArgs e)
        {
            Interop.Common.Util.CDelegate.SetText( EdgeThresholdMax01, tbMax01.Value.ToString() );
        }
        private void tbMin02_Scroll(object sender, EventArgs e)
        {
            Interop.Common.Util.CDelegate.SetText( EdgeThresholdMin02, tbMin02.Value.ToString() );
        }
        private void tbMax02_Scroll(object sender, EventArgs e)
        {
            Interop.Common.Util.CDelegate.SetText( EdgeThresholdMax02, tbMax02.Value.ToString() );
        }
        private void tmrSystem_Tick(object sender, EventArgs e)
        {
            try
            {
                Interop.Common.Util.CDelegate.SetText( lblSystemTime, System.DateTime.Now.ToString( "yyyy/MM/dd HH:mm:ss" ) );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void tmrDebugPLC_Tick(object sender, EventArgs e)
        {
            int readValue;
            try
            {
                if ( chkPLC_HB.Checked == true )
                {
                    readValue = cMxCom.Word_Read( "D9100" );

                    cMxCom.Word_Write( "D9100", (readValue == 1 ? 0 : 1 ) );
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void bgWorkPLC_DoWork(object sender, DoWorkEventArgs e)
        {
            int plcHeartBit = -1;
            int plcInspStart = -1;

            string ErrorString = string.Empty;
            do
            {
                try
                {
                    // PLC Read
                    plcHeartBit = Convert.ToInt16( fnRead_PLC( cGlobalDefine.ePLCSignal.HEARTBIT ) );
                    plcInspStart = Convert.ToInt16( fnRead_PLC( cGlobalDefine.ePLCSignal.INSP_START ) );

                    eventPLC.Fliker( plcHeartBit );
                    eventPLC.InspStart( plcInspStart );

                    //PC Heart bit
                    eventHeartBit.HeartbitEvent();

                    System.Threading.Thread.Sleep( 30 );
                }
                catch ( Exception ex )
                {
                    System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
                }
            } while ( true );
        }
        #endregion [ PC Control Action ]


        #region [ PLC Debug ]
        private void chkPLC_HB_CheckedChanged(object sender, EventArgs e)
        {
            if ( chkPLC_HB.Checked == true )
            {
                tmrDebugPLC.Interval = 500;
                tmrDebugPLC.Start();
            }
            else
            {
                tmrDebugPLC.Stop();
            }
        }
        private void chkPLC_InspStart_CheckedChanged(object sender, EventArgs e)
        {
            if ( chkPLC_InspStart.Checked == true )
            {
                cMxCom.Word_Write( "D9101", 1 );
            }
            else
            {
                cMxCom.Word_Write( "D9101", 0 );
            }
        }
        private void btnPLC_ModelWrite_Click(object sender, EventArgs e)
        {
            if ( Interop.Common.Util.CUtil.IsNumeric( txtPLC_Model.Text.Trim() ) == false )
            {
                MessageBox.Show( "Only numbers can be entered." );
                return;
            }
            cMxCom.Word_Write( "D9102", Interop.Common.Util.CUtil.mStringToInt( txtPLC_Model.Text.Trim() ) );
        }
        #endregion [ PLC Debug ]










        // Support Functions
        //================================================================================
        private void fnDBConnection()
        {
            try
            {
                if ( localdbConn == null ) localdbConn = new Interop.Common.DB.cSQLServer();

                localdbConn.DB_IP = Environment.MachineName + @"\SQLEXPRESS";
                localdbConn.DB_NAME = AppConfigRead( "LocalDBName" );
                localdbConn.DB_ID = AppConfigRead( "LocalUserID" );
                localdbConn.DB_PW = AppConfigRead( "LocalUserPW" );
                if ( false == string.IsNullOrEmpty( localdbConn.SQLOpen() ) )
                {
                    MessageBox.Show( "DB 연결 오류" );
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        public void fnSetDBConnectClose()
        {
            try
            {
                if ( localdbConn != null ) localdbConn.CloseDataBase();
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private bool fnMxCompomponentOpen()
        {
            bool result = false;
            try
            {
                if ( cMxCom == null )
                {
                    cMxCom = new Interop.Common.PLC.Melsec.CMxComponentDirect();
                }
#if UsePLC
                this.cMxCom.Channel_Open( 0 );
#endif
                if ( true == cMxCom.isOpen )
                {
                    fnSetPCMemoryClear();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
                result = false;
            }
            return result;
        }
        private void fnMxCompomponentClose()
        {
            if ( cMxCom != null && cMxCom.isOpen )
            {
                try
                {
                    cMxCom.Channel_Close();
                }
                catch ( Exception ex )
                {
                    System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
                }
                finally
                {
                    if ( cMxCom != null ) cMxCom = null;
                }
            }
        }
        private void fnDisplayControlsClear()
        {
            try
            {
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private string AppConfigRead(string _keyName)
        {
            string strReturnValue = string.Empty;
            try
            {
                System.Configuration.Configuration currentConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration( System.Configuration.ConfigurationUserLevel.None );
                if ( currentConfig.AppSettings.Settings.AllKeys.Contains( _keyName ) )
                {
                    strReturnValue = currentConfig.AppSettings.Settings[ _keyName ].Value.Trim();
                }
                else
                {
                    strReturnValue = string.Empty;
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
                strReturnValue = string.Empty;
            }
            return strReturnValue;
        }
        private void fnSystemEnviromentSettings(bool _flag)
        {
            try
            {
                if ( true == _flag )
                {
                    if ( tmrHeartBitPLCCheck == null )
                    {
                        tmrHeartBitPLCCheck = new System.Timers.Timer();
                        tmrHeartBitPLCCheck.Elapsed += new System.Timers.ElapsedEventHandler( tmrHeartBitPLCCheck_Elapsed );
                        tmrHeartBitPLCCheck.Interval = 30 * 1000; // 30 초
                    }
                    if ( eventHeartBit == null )
                    {
                        eventHeartBit = new cBGWorkerEvents.EventHeartBit();
                        eventHeartBit.evHeartBitHandler += new cBGWorkerEvents.EventHeartBit.EventHandler( eventHeartBit_evHeartBitHandler );
                    }
                    if ( eventPLC == null )
                    {
                        eventPLC = new cBGWorkerEvents.PLCEvents();
                        eventPLC.evHeartBit += EventPLC_evHeartBit;
                        eventPLC.evInspStart += EventPLC_evInspStart;
                    }
                }
                else
                {
                    tmrHeartBitPLCCheck.Elapsed -= new System.Timers.ElapsedEventHandler( tmrHeartBitPLCCheck_Elapsed );
                    eventHeartBit.evHeartBitHandler -= new cBGWorkerEvents.EventHeartBit.EventHandler( eventHeartBit_evHeartBitHandler );

                    eventPLC.evHeartBit -= EventPLC_evHeartBit;
                    eventPLC.evInspStart -= EventPLC_evInspStart;

                    tmrHeartBitPLCCheck.Stop();
                    tmrHeartBitPLCCheck = null;

                    eventHeartBit = null;
                    eventPLC = null;
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void fnSetPCMemoryClear()
        {
            string _address = string.Empty;
            try
            {
                for ( int idx = 1 ; idx < 3 ; idx++ )
                {
                    _address = string.Format( "D9{0:D3}", idx.ToString() );
                    cMxCom.Word_Write( _address, 0 );
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void fnCAM_Initialize()
        {
            bool bIsOpen = false;
            string driverString = string.Empty;
            try
            {
                driverString = Environment.ExpandEnvironmentVariables( "%CVB%" ) + @"Drivers\GenICam.vin";

                if ( axCVimage1.LoadImage( driverString ) )
                { bIsOpen = true; }
                else
                { MessageBox.Show( "#1 open error" ); }
                if ( bIsOpen == true ) { if ( axCVimage2.LoadImage( driverString ) ) { } else { bIsOpen = false; MessageBox.Show( "#2 open error" ); } }

                if ( bIsOpen == true )
                {
                    axCVgrabber1.CamPort = 0; axCVgrabber1.Image = axCVimage1.Image; axCVimage1.Image = axCVgrabber1.Image;
                    axCVgrabber2.CamPort = 1; axCVgrabber2.Image = axCVimage2.Image; axCVimage2.Image = axCVgrabber2.Image;
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void axCVgrabber_Pos01_ImageUpdated(object sender, System.EventArgs e) { axCVimage1.Image = axCVgrabber1.Image; }
        private void axCVgrabber_Pos02_ImageUpdated(object sender, System.EventArgs e) { axCVimage2.Image = axCVgrabber2.Image; }
        private void fnCamEventPlus()
        {
            try
            {
                axCVgrabber1.ImageUpdated += new System.EventHandler( axCVgrabber_Pos01_ImageUpdated );
                axCVgrabber2.ImageUpdated += new System.EventHandler( axCVgrabber_Pos02_ImageUpdated );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void fnCamEventMinus()
        {
            try
            {
                axCVgrabber1.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos01_ImageUpdated );
                axCVgrabber2.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos02_ImageUpdated );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void mCVBInit()
        {
            axDisplayList = new AxCVDISPLAYLib.AxCVdisplay[ 2 ];
            axImageList = new AxCVIMAGELib.AxCVimage[ cGlobalDefine.InspCamCnt ];

            axDisplayList[ 0 ] = axCVdisplay1;
            axDisplayList[ 1 ] = axCVdisplay2;

            axImageList[ 0 ] = axCVimage1;
            axImageList[ 1 ] = axCVimage2;
        }
        private void fnCVBDisplayClear()
        {
            for ( int idx = 0 ; idx < axDisplayList.Length ; idx++ )
            {
                axDisplayList[ idx ].RemoveAllOverlayObjects();
                axDisplayList[ idx ].Refresh();
            }
        }
        private void fnPLCBitState(cGlobalDefine.ePLCSignal _name, Color _stats)
        {
            switch ( _name )
            {
                case cGlobalDefine.ePLCSignal.HEARTBIT: Interop.Common.Util.CDelegate.SetBackColor( lblPLC_HB, _stats ); break;
                case cGlobalDefine.ePLCSignal.INSP_START: Interop.Common.Util.CDelegate.SetBackColor( lblPLC_InspStart, _stats ); break;
                default:
                    break;
            }
        }
        private void fnPCBitState(cGlobalDefine.ePCSignal _name, Color _stats)
        {
            switch ( _name )
            {
                case cGlobalDefine.ePCSignal.HEARTBIT: Interop.Common.Util.CDelegate.SetBackColor( lblPC_HB, _stats ); break;
                case cGlobalDefine.ePCSignal.INSP_START: Interop.Common.Util.CDelegate.SetBackColor( lblPC_InspStart, _stats ); break;
                case cGlobalDefine.ePCSignal.INSP_COMP: Interop.Common.Util.CDelegate.SetBackColor( lblPC_InspEnd, _stats ); break;
                default: break;
            }
        }
        private string fnRead_PLC(cGlobalDefine.ePLCSignal _target)
        {
            string readValue = string.Empty;
            string address = string.Empty;
            try
            {
                switch ( _target )
                {
                    case cGlobalDefine.ePLCSignal.HEARTBIT: address = AppConfigRead( "PLC_HEARTBIT" ); break;
                    case cGlobalDefine.ePLCSignal.INSP_MODEL: address = AppConfigRead( "PLC_INSP_MODEL" ); break;
                    case cGlobalDefine.ePLCSignal.INSP_START: address = AppConfigRead( "PLC_INSP_START" ); break;
                    default: address = string.Empty; break;
                }
                if ( false == string.IsNullOrEmpty( address ) )
                {
                    readValue = cMxCom.Word_Read( address ).ToString();
                }
                else
                {
                    //log : Wrong Address
                    readValue = string.Empty;
                }
            }
            catch ( Exception ex )
            {
                readValue = string.Empty;
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }

            return readValue;
        }
        private void tmrHeartBitPLCCheck_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                tmrHeartBitPLCCheck.Stop();

                fnPLCBitState( cGlobalDefine.ePLCSignal.HEARTBIT, cGlobalDefine.ColorNG );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void eventHeartBit_evHeartBitHandler(object sender, cBGWorkerEvents.EventArgList e)
        {
            try
            {
                if (bPCHeartBit == 0)
                {
                    bPCHeartBit = 1;
                }
                else
                {
                    bPCHeartBit = 0;
                }

                cMxCom.Word_Write("D9000", bPCHeartBit);
                Interop.Common.Util.CDelegate.SetBackColor( lblPC_HB, bPCHeartBit == 0 ? cGlobalDefine.ColorDefault : cGlobalDefine.ColorOK );
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void EventPLC_evHeartBit(object sender, cBGWorkerEvents.EventArgList e)
        {
            try
            {
                if (beforPLCHeartBit != e.data)
                {
                    tmrHeartBitPLCCheck.Stop(); // 통신 에러 Timer
                    tmrHeartBitPLCCheck.Start();

                    fnPLCBitState(cGlobalDefine.ePLCSignal.HEARTBIT, e.data == 0 ? cGlobalDefine.ColorDefault : cGlobalDefine.ColorOK);
                    beforPLCHeartBit = e.data;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void EventPLC_evInspStart(object sender, cBGWorkerEvents.EventArgList e)
        {
            try
            {

            }
            catch ( Exception ex )
            {

            }
        }








    }
}
