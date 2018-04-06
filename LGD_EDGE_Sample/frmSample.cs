#define UsePLC
//#define UseCam
//#define UseDatabase

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace LGD_EDGE_Sample
{
    public partial class frmSample : Form
    {
        public const int NUM_CAM = 2;
        public const int NUM_STEPS = 5;

        private bool bIsDelay = true;
        private bool bIsStartClose = false;
        private bool isInspection = false; // 검사 진행 여부

        private AxCVDISPLAYLib.AxCVdisplay[] axDisplayList; // 화면
        private AxCVIMAGELib.AxCVimage[] axImageList;// Image
        private Interop.Common.CVB.cStruct.stEdgeInputData[] settingEdgeInputList;

        private Interop.Common.Progress.SplashThread loadProgress = null;
        private Interop.Common.DB.cSQLServer localdbConn = null;

        private Interop.Common.PLC.Melsec.CMxComponentDirect cMxCom = null;
        private cBGWorkerEvents.PLCEvents eventPLC = null;
        private cBGWorkerEvents.EventHeartBit eventHeartBit = null;
        private System.Timers.Timer tmrHeartBitPLCCheck = null;

        private string sInspectionStartTime = string.Empty;
        private string sInspectionModel = string.Empty;

        private cDBInfo.InspSaveMain saveMain;
        private cDBInfo.InspSaveSub[] saveSub = null;

        private int bPCHeartBit = 0;
        private int beforPLCHeartBit = -1;
        public bool[] beforPLCBit = new bool[30];

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
                loadProgress.UpdateProgress(1, "Local Database Connecting...");
                if (true == bIsDelay) System.Threading.Thread.Sleep(300);

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
                loadProgress.UpdateProgress(2, "PLC Connecting...");
                if (true == bIsDelay) System.Threading.Thread.Sleep(300);

#if UsePLC
                if (false == fnMxCompomponentOpen())
                {
                    MessageBox.Show(this, "Don't Connect PLC\nProgram Exit", "Error Connect to PLC", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bIsStartClose = true;
                    this.Close();
                }
#endif

                // 3 --> UI Construct & Clear ...
                loadProgress.UpdateProgress(3, "UI Control Initialize...");
                if (true == bIsDelay) System.Threading.Thread.Sleep(300);
                fnDisplayControlsClear();

#if UsePLC
                // 4 --> PC 메모리 영역 초기화...
                loadProgress.UpdateProgress(4, "PLC - PC Area Initialize...");
                if (true == bIsDelay) System.Threading.Thread.Sleep(300);
                fnSetPCMemoryClear();
#endif

#if UseCam
                // 5 --> Cam Initialize
                loadProgress.UpdateProgress( 5, "Cam Connecting..." );
                if ( true == bIsDelay ) System.Threading.Thread.Sleep( 300 );

                fnCAM_Initialize();
                fnCamEventPlus();
                //mCVBInit();
#endif
                mCVBInit();

                // 6 --> Program Enviroment Load...
                loadProgress.UpdateProgress(6, "PC Evviroment Loding...");
                if (true == bIsDelay) System.Threading.Thread.Sleep(300);

                saveMain = new cDBInfo.InspSaveMain();
                saveSub = new cDBInfo.InspSaveSub[nInspCnt];

                // 7 --> Local Timer 및 기타 설정 완료
                loadProgress.UpdateProgress(7, "Main UI Loading...");
                if (true == bIsDelay) System.Threading.Thread.Sleep(300);
                fnSystemEnviromentSettings(true);

                fnDisplayControlsClear();

                tmrSystem.Start();
#if UsePLC
                bgWorkPLC.RunWorkerAsync();
#endif
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (loadProgress != null)
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
                if (false == bIsStartClose)
                {
                    result = MessageBox.Show("Please save all the data before exit.", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                }
                if ((result == DialogResult.No) && (false == bIsStartClose))
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
                    if (bgWorkPLC != null && bgWorkPLC.IsBusy)
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
            catch (Exception ex)
            {
                logException("Exit error : " + ex.Message.ToString());
            }
        }


        #region [ PC Control Action ]
        private void btnRunInspection_Click(object sender, EventArgs e)
        {
            try
            {
                // Sample code for edge detection in 2 static images
                //clearing all previous labels
                fnCVBDisplayClear();

                mRunInspectionEdge(axImageList);

                //double x0, y0, x1, y1, x2, y2;
                //x0 = y0 = x1 = y1 = x2 = y2 = 0;
                //iCVCEdge.CvB.Image.TArea scanArea;
                //iCVCEdge.Cvb.Edge.TEdgeResult result;
                //iCVCEdge.Cvb.Edge.TEdgeResult[] resultAll;

                //axCVdisplay.GetSelectedArea(ref x0, ref y0, ref x1, ref y1, ref x2, ref y2);
                //Cvb.Image.SetArea(x0, y0, x1, y1, x2, y2, out scanArea);

                ////finding first edge
                //bool located = Cvb.Edge.TFindFirstEdge(axCVimage.Image, 0, 500, scanArea, System.Convert.ToDouble(m_TrackThreshold.Value.ToString()), checkBoxPositive.Checked, out result);
                ////bool locatedAll = Cvb.Edge.TFindAllEdges(axCVimage.Image, 0, 500, scanArea, System.Convert.ToDouble(m_TrackThreshold.Value.ToString()), checkBoxPositive.Checked, 20, out resultAll);

                //if (located)
                //{
                //    for (int idx = 0; idx < resultAll.Length; idx++)
                //    {
                //        axCVdisplay.AddLabel("X", false, 255, 0, (int)resultAll[idx].x, (int)resultAll[idx].y);
                //    }
                //    axCVdisplay.AddLabel("X", false, 255, 0, (int)result.x, (int)result.y);
                //}

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void btnImageLoad01_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.InitialDirectory = "c:\\";
                open.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*";
                open.FilterIndex = 2;
                open.RestoreDirectory = true;

                if (open.ShowDialog() == DialogResult.OK)
                {
                    axCVimage1.LoadImage(open.FileName);

                    Interop.Common.CVB.CCVBOverlay.OverlayLabelRemoveAll(axCVdisplay1);
                    Interop.Common.CVB.CCVBOverlay.OverlayObjectRemoveAll(axCVdisplay1);

                    axCVdisplay1.Image = axCVimage1.Image;
                    axCVdisplay1.SetSelectedArea(50.0, 340.0, 50.0, 360.0, 590.0, 340.0);
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
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.InitialDirectory = "c:\\";
                open.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*";
                open.FilterIndex = 2;
                open.RestoreDirectory = true;

                if (open.ShowDialog() == DialogResult.OK)
                {
                    axCVimage2.LoadImage(open.FileName);

                    //Interop.Common.CVB.CCVBOverlay.OverlayLabelRemoveAll(axCVdisplay2);
                    //Interop.Common.CVB.CCVBOverlay.OverlayObjectRemoveAll(axCVdisplay2);

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

            if (chkPositive01.Checked == true)
            {
                settingEdgeInputList[0].EdgePositive = Interop.Common.CVB.cEnum.eEdgePositive.Negative;
                chkPositive01.Text = "Negative";
            }
            else
            {
                settingEdgeInputList[0].EdgePositive = Interop.Common.CVB.cEnum.eEdgePositive.Positive;
                chkPositive01.Text = "Positive";
            }
        }
        private void chkPositive02_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPositive02.Checked == true)
            {
                settingEdgeInputList[1].EdgePositive = Interop.Common.CVB.cEnum.eEdgePositive.Negative;
                chkPositive02.Text = "Negative";
            }
            else
            {
                settingEdgeInputList[1].EdgePositive = Interop.Common.CVB.cEnum.eEdgePositive.Positive;
                chkPositive02.Text = "Positive";
            }
        }
        private void tbMin01_Scroll(object sender, EventArgs e)
        {
            settingEdgeInputList[0].ThresholdResult_Min = tbMin01.Value;
            Interop.Common.Util.CDelegate.SetText(EdgeThresholdMin01, tbMin01.Value.ToString());
        }
        private void tbMax01_Scroll(object sender, EventArgs e)
        {
            settingEdgeInputList[0].ThresholdResult_Max = tbMax01.Value;
            Interop.Common.Util.CDelegate.SetText(EdgeThresholdMax01, tbMax01.Value.ToString());
        }
        private void tbMin02_Scroll(object sender, EventArgs e)
        {
            settingEdgeInputList[1].ThresholdResult_Min = tbMin02.Value;
            Interop.Common.Util.CDelegate.SetText(EdgeThresholdMin02, tbMin02.Value.ToString());
        }
        private void tbMax02_Scroll(object sender, EventArgs e)
        {
            settingEdgeInputList[1].ThresholdResult_Max = tbMax02.Value;
            Interop.Common.Util.CDelegate.SetText(EdgeThresholdMax02, tbMax02.Value.ToString());
        }
        private void tmrSystem_Tick(object sender, EventArgs e)
        {
            try
            {
                Interop.Common.Util.CDelegate.SetText(lblSystemTime, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        private void tmrDebugPLC_Tick(object sender, EventArgs e)
        {
            int readValue;
            try
            {
                if (chkPLC_HB.Checked == true)
                {
                    readValue = cMxCom.Word_Read("D9100");

                    cMxCom.Word_Write("D9100", (readValue == 1 ? 0 : 1));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
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
                    plcHeartBit = Convert.ToInt16(fnRead_PLC(cGlobalDefine.ePLCSignal.HEARTBIT));
                    plcInspStart = Convert.ToInt16(fnRead_PLC(cGlobalDefine.ePLCSignal.INSP_START));

                    eventPLC.Fliker(plcHeartBit);
                    eventPLC.InspStart(plcInspStart);

                    //PC Heart bit
                    eventHeartBit.HeartbitEvent();

                    System.Threading.Thread.Sleep(30);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                }
            } while (true);
        }
        #endregion [ PC Control Action ]


        #region [ PLC Debug ]
        private void chkPLC_HB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPLC_HB.Checked == true)
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
            if (chkPLC_InspStart.Checked == true)
            {
                cMxCom.Word_Write("D9101", 1);
            }
            else
            {
                cMxCom.Word_Write("D9101", 0);
            }
        }
        private void btnPLC_ModelWrite_Click(object sender, EventArgs e)
        {
            if (Interop.Common.Util.CUtil.IsNumeric(txtPLC_Model.Text.Trim()) == false)
            {
                MessageBox.Show("Only numbers can be entered.");
                return;
            }
            cMxCom.Word_Write("D9102", Interop.Common.Util.CUtil.mStringToInt(txtPLC_Model.Text.Trim()));
        }
        #endregion [ PLC Debug ]










        // Support Functions
        //================================================================================
        private void fnDBConnection()
        {
            try
            {
                if (localdbConn == null) localdbConn = new Interop.Common.DB.cSQLServer();

                localdbConn.DB_IP = Environment.MachineName + @"\SQLEXPRESS";
                localdbConn.DB_NAME = AppConfigRead("LocalDBName");
                localdbConn.DB_ID = AppConfigRead("LocalUserID");
                localdbConn.DB_PW = AppConfigRead("LocalUserPW");
                if (false == string.IsNullOrEmpty(localdbConn.SQLOpen()))
                {
                    MessageBox.Show("DB 연결 오류");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        public void fnSetDBConnectClose()
        {
            try
            {
                if (localdbConn != null) localdbConn.CloseDataBase();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        private bool fnMxCompomponentOpen()
        {
            bool result = false;
            try
            {
                if (cMxCom == null)
                {
                    cMxCom = new Interop.Common.PLC.Melsec.CMxComponentDirect();
                }
#if UsePLC
                this.cMxCom.Channel_Open(0);
#endif
                if (true == cMxCom.isOpen)
                {
                    fnSetPCMemoryClear();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                result = false;
            }
            return result;
        }
        private void fnMxCompomponentClose()
        {
            if (cMxCom != null && cMxCom.isOpen)
            {
                try
                {
                    cMxCom.Channel_Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                }
                finally
                {
                    if (cMxCom != null) cMxCom = null;
                }
            }
        }

        // Summary:
        //     Clear Display label such as x,y,z coordinate
        //
        // Parameters:
        //   
        private void fnDisplayControlsClear()
        {
            try
            {
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        private string AppConfigRead(string _keyName)
        {
            string strReturnValue = string.Empty;
            try
            {
                System.Configuration.Configuration currentConfig = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                if (currentConfig.AppSettings.Settings.AllKeys.Contains(_keyName))
                {
                    strReturnValue = currentConfig.AppSettings.Settings[_keyName].Value.Trim();
                }
                else
                {
                    strReturnValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                strReturnValue = string.Empty;
            }
            return strReturnValue;
        }
        private void fnSystemEnviromentSettings(bool _flag)
        {
            try
            {
                if (true == _flag)
                {
                    if (tmrHeartBitPLCCheck == null)
                    {
                        tmrHeartBitPLCCheck = new System.Timers.Timer();
                        tmrHeartBitPLCCheck.Elapsed += new System.Timers.ElapsedEventHandler(tmrHeartBitPLCCheck_Elapsed);
                        tmrHeartBitPLCCheck.Interval = 30 * 1000; // 30 초
                    }
                    if (eventHeartBit == null)
                    {
                        eventHeartBit = new cBGWorkerEvents.EventHeartBit();
                        eventHeartBit.evHeartBitHandler += new cBGWorkerEvents.EventHeartBit.EventHandler(eventHeartBit_evHeartBitHandler);
                    }
                    if (eventPLC == null)
                    {
                        eventPLC = new cBGWorkerEvents.PLCEvents();
                        eventPLC.evHeartBit += EventPLC_evHeartBit;
                        eventPLC.evInspStart += EventPLC_evInspStart;
                    }
                }
                else
                {
                    tmrHeartBitPLCCheck.Elapsed -= new System.Timers.ElapsedEventHandler(tmrHeartBitPLCCheck_Elapsed);
                    eventHeartBit.evHeartBitHandler -= new cBGWorkerEvents.EventHeartBit.EventHandler(eventHeartBit_evHeartBitHandler);

                    eventPLC.evHeartBit -= EventPLC_evHeartBit;
                    eventPLC.evInspStart -= EventPLC_evInspStart;

                    tmrHeartBitPLCCheck.Stop();
                    tmrHeartBitPLCCheck = null;

                    eventHeartBit = null;
                    eventPLC = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        private void fnSetPCMemoryClear()
        {
            string _address = string.Empty;
            try
            {
                for (int idx = 1; idx < 3; idx++)
                {
                    _address = string.Format("D9{0:D3}", idx.ToString());
                    cMxCom.Word_Write(_address, 0);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        private void fnCAM_Initialize()
        {
            bool bIsOpen = false;
            string driverString = string.Empty;
            try
            {
                driverString = Environment.ExpandEnvironmentVariables("%CVB%") + @"Drivers\GenICam.vin";

                if (axCVimage1.LoadImage(driverString))
                { bIsOpen = true; }
                else
                { MessageBox.Show("#1 open error"); }
                if (bIsOpen == true) { if (axCVimage2.LoadImage(driverString)) { } else { bIsOpen = false; MessageBox.Show("#2 open error"); } }

                if (bIsOpen == true)
                {
                    axCVgrabber1.CamPort = 0; axCVgrabber1.Image = axCVimage1.Image;
                    axCVgrabber2.CamPort = 1; axCVgrabber2.Image = axCVimage2.Image;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        private void axCVgrabber_Pos01_ImageUpdated(object sender, System.EventArgs e) { axCVimage1.Image = axCVgrabber1.Image; }
        private void axCVgrabber_Pos02_ImageUpdated(object sender, System.EventArgs e) { axCVimage2.Image = axCVgrabber2.Image; }
        private void fnCamEventPlus()
        {
            try
            {
                axCVgrabber1.ImageUpdated += new System.EventHandler(axCVgrabber_Pos01_ImageUpdated);
                axCVgrabber2.ImageUpdated += new System.EventHandler(axCVgrabber_Pos02_ImageUpdated);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        private void fnCamEventMinus()
        {
            try
            {
                axCVgrabber1.ImageUpdated -= new System.EventHandler(axCVgrabber_Pos01_ImageUpdated);
                axCVgrabber2.ImageUpdated -= new System.EventHandler(axCVgrabber_Pos02_ImageUpdated);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        private void mCVBInit()
        {
            axDisplayList = new AxCVDISPLAYLib.AxCVdisplay[2];
            axImageList = new AxCVIMAGELib.AxCVimage[cGlobalDefine.InspCamCnt];

            axDisplayList[0] = axCVdisplay1;
            axDisplayList[1] = axCVdisplay2;

            axImageList[0] = axCVimage1;
            axImageList[1] = axCVimage2;

            settingEdgeInputList = new Interop.Common.CVB.cStruct.stEdgeInputData[NUM_CAM];

            // load default image
            axImageList[0].LoadImage(@"%CVB%\Tutorial\ClassicSwitch001.bmp");
            axImageList[1].LoadImage(@"%CVB%\Tutorial\ClassicSwitch001.bmp");
            axDisplayList[0].SetSelectedArea(50.0, 340.0, 50.0, 360.0, 590.0, 340.0);
            axDisplayList[1].SetSelectedArea(50.0, 340.0, 50.0, 360.0, 590.0, 340.0);
        }

        private void fnCVBDisplayClear()
        {
            for (int idx = 0; idx < axDisplayList.Length; idx++)
            {
                Interop.Common.CVB.CCVBOverlay.OverlayLabelRemoveAll(axDisplayList[idx]);
                //Interop.Common.CVB.CCVBOverlay.OverlayObjectRemoveAll(CVDisplay);
                axDisplayList[idx].RemoveAllOverlayObjects();
                axDisplayList[idx].Refresh();
            }
        }
        private void fnPLCBitState(cGlobalDefine.ePLCSignal _name, Color _stats)
        {
            switch (_name)
            {
                case cGlobalDefine.ePLCSignal.HEARTBIT: Interop.Common.Util.CDelegate.SetBackColor(lblPLC_HB, _stats); break;
                case cGlobalDefine.ePLCSignal.INSP_START: Interop.Common.Util.CDelegate.SetBackColor(lblPLC_InspStart, _stats); break;
                default:
                    break;
            }
        }
        private void fnPCBitState(cGlobalDefine.ePCSignal _name, Color _stats)
        {
            switch (_name)
            {
                case cGlobalDefine.ePCSignal.HEARTBIT: Interop.Common.Util.CDelegate.SetBackColor(lblPC_HB, _stats); break;
                case cGlobalDefine.ePCSignal.INSP_START: Interop.Common.Util.CDelegate.SetBackColor(lblPC_InspStart, _stats); break;
                case cGlobalDefine.ePCSignal.INSP_COMP: Interop.Common.Util.CDelegate.SetBackColor(lblPC_InspEnd, _stats); break;
                default: break;
            }
        }
        private string fnRead_PLC(cGlobalDefine.ePLCSignal _target)
        {
            string readValue = string.Empty;
            string address = string.Empty;
            try
            {
                switch (_target)
                {
                    case cGlobalDefine.ePLCSignal.HEARTBIT: address = AppConfigRead("PLC_HEARTBIT"); break;
                    case cGlobalDefine.ePLCSignal.INSP_MODEL: address = AppConfigRead("PLC_INSP_MODEL"); break;
                    case cGlobalDefine.ePLCSignal.INSP_START: address = AppConfigRead("PLC_INSP_START"); break;
                    default: address = string.Empty; break;
                }
                if (false == string.IsNullOrEmpty(address))
                {
                    readValue = cMxCom.Word_Read(address).ToString();
                }
                else
                {
                    //log : Wrong Address
                    readValue = string.Empty;
                }
            }
            catch (Exception ex)
            {
                readValue = string.Empty;
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }

            return readValue;
        }
        private void tmrHeartBitPLCCheck_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                tmrHeartBitPLCCheck.Stop();

                fnPLCBitState(cGlobalDefine.ePLCSignal.HEARTBIT, cGlobalDefine.ColorNG);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
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
                Interop.Common.Util.CDelegate.SetBackColor(lblPC_HB, bPCHeartBit == 0 ? cGlobalDefine.ColorDefault : cGlobalDefine.ColorOK);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
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
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }
        private void EventPLC_evInspStart(object sender, cBGWorkerEvents.EventArgList e)
        {
            try
            {
                int iAddr = (int)cGlobalDefine.ePLCSignal.INSP_START;


                if (e.data == 1 && beforPLCBit[iAddr] == false)
                {
                    //int isBypass = cMxCom.Word_Read("D9103");
                    //if (isBypass != 1)
                    {

                        fnPLCBitState(cGlobalDefine.ePLCSignal.INSP_START, cGlobalDefine.ColorOK);
                        beforPLCBit[iAddr] = true;

                        logComm(true, "(PLC)검사 시작 On", Interop.Common.Util.cLog.eLogType.COMM);
                        if (true == isInspection)
                        {
                            logComm(true, "검사 도중 PLC에서 검사시작 신호 요청 함.", Interop.Common.Util.cLog.eLogType.COMM);
                        }
                        else
                        {
                            fnCVBDisplayClear();
                            //axImageList[idx].Clear(0, 0);


                            //axCVdisplay1.Image = axImageList[idx].Image;

                            cMxCom.Word_Write("D9002", 0);

                            fnPCBitState(cGlobalDefine.ePCSignal.INSP_START, cGlobalDefine.ColorOK);
                            fnDisplayControlsClear();

                            sInspectionStartTime = System.DateTime.Now.ToString("yyyyMMddHHmmss");
                            sInspectionModel = fnRead_PLC(cGlobalDefine.ePLCSignal.INSP_MODEL);

                            fnGrabImgSet(true);
                            /*
                            Interop.Common.Util.CDelegate.SetText(lblCurrInspTime, sInspectionStartTime);
                            Interop.Common.Util.CDelegate.SetText(lblCurrInspModel, sInspectionModel);

                            //Save DB Init
                            saveMain.Clear();
                            saveMain.dateTime = sInspectionStartTime;
                            saveMain.model = sInspectionModel;
                            for (int idx = 0; idx < saveSub.Length; idx++)
                            {
                                saveSub[idx].Clear();
                                saveSub[idx].dateTime = saveMain.dateTime;
                                saveSub[idx].model = saveMain.model;
                            }

                            fnScanSensor(true);

                            fnPCBitState(cGlobalDefine.ePCSignal.POS_MOVE, cGlobalDefine.ColorOK);
                            this.Invoke(new MethodInvoker(delegate() { pnlLMI.Visible = true; }));

                            string posMove = string.Empty;
                            string posVel = string.Empty;
                            long posValue = -1;
                            long velValue = -1;

                            posMove = Interop.Common.Util.cIni.IniReadValue("INSP_START", "MOVE_POS", sMotionPath);
                            posVel = Interop.Common.Util.cIni.IniReadValue("INSP_START", "MOVE_VEL", sMotionPath);
                            */
#if UsePLC
                            //posValue = (long)Interop.Common.Util.CUtil.StringToFloat(posMove.Trim());
                            //posValue = posValue * 10000;
                            //velValue = (long)Interop.Common.Util.CUtil.StringToFloat(posVel.Trim());
                            //velValue = velValue * 100;

                            //cMxCom.Word2_Write("D9014", posValue);
                            //cMxCom.Word2_Write("D9016", velValue);

                            System.Threading.Thread.Sleep(200);

                            //cMxCom.Word_Write("D9012", 1);
                            cMxCom.Word_Write("D9001", 1);
#endif
                        }
                    }
                    //else
                    //{
                    //   logComm(true, "Bypass  상태에서 검사시작 신호 요청 함.", Interop.Common.Util.cLog.eLogType.COMM);
                    //}
                }
                else if (e.data == 0 && beforPLCBit[iAddr] == true)
                {
                    fnPLCBitState(cGlobalDefine.ePLCSignal.INSP_START, cGlobalDefine.ColorDefault);
                    //fnPCBitState(cGlobalDefine.ePCSignal.POS_MOVE, cGlobalDefine.ColorDefault);
                    fnPCBitState(cGlobalDefine.ePCSignal.INSP_COMP, cGlobalDefine.ColorDefault);
                    beforPLCBit[iAddr] = false;
                    logComm(true, "(PLC)검사시작 Off", Interop.Common.Util.cLog.eLogType.COMM);

                    //cMxCom.Word_Write("D9012", 0);
                    cMxCom.Word_Write("D9001", 0);
                }
            }
            catch (Exception ex)
            {
                logException("EventPLC_evInspStart Error : " + ex.Message.ToString());
            }
        }


        private void logException(string _msg)
        {
            Interop.Common.Util.cLog.FileWrite_Str(_msg, Interop.Common.Util.cLog.eLogType.EXCEPTION);
        }


        private void logMessage(bool _saveFlag, string _msg, Interop.Common.Util.cLog.eLogType _type)
        {
            string saveDate = string.Empty;
            string dispTime = string.Empty;
            string saveMsg = string.Empty;
            try
            {
                saveDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dispTime = System.DateTime.Now.ToString("HH:mm:ss");

                saveMsg = string.Format("[{0}]{1}", saveDate, _msg);
                this.Invoke(new MethodInvoker(delegate()
                {
                    if (lstVision.Items.Count >= 200) lstVision.Items.Clear();

                    lstVision.Items.Add(string.Format("[{0}]{1}", dispTime, _msg));
                }));
                if (_saveFlag == true) Interop.Common.Util.cLog.FileWrite_Str(saveMsg, _type);
            }
            catch (Exception ex)
            {
                logException("Log Data Save Fail : " + ex.Message.ToString());
            }
        }

        private void logComm(bool _saveFlag, string _msg, Interop.Common.Util.cLog.eLogType _type)
        {
            string saveDate = string.Empty;
            string dispTime = string.Empty;
            string saveMsg = string.Empty;

            try
            {
                saveDate = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                dispTime = System.DateTime.Now.ToString("HH:mm:ss");

                saveMsg = string.Format("[{0}]{1}", saveDate, _msg);
                this.Invoke(new MethodInvoker(delegate()
                {
                    if (lstComm.Items.Count >= 200) lstComm.Items.Clear();

                    lstComm.Items.Add(string.Format("[{0}]{1}", dispTime, _msg));
                }));
                if (_saveFlag == true) Interop.Common.Util.cLog.FileWrite_Str(saveMsg, _type);
            }
            catch (Exception ex)
            {
                logException("Comm Data Save Fail : " + ex.Message.ToString());
            }
        }



        // Summary:
        //     Enable/Disable CVB Image snaped event function
        //
        // Parameters:
        //   _flag:
        //     True     Enable
        //     False    Disable 
        private bool fnGrabImgSet(bool _flag)
        {
            bool result = false;
            try
            {
                for (int idx = 0; idx < axImageList.Length; idx++)
                {


                    if (_flag == true)
                    {
                        if (axImageList[idx].Grab == false)
                        {
                            axImageList[idx].Grab = false;
                            System.Threading.Thread.Sleep(500);
                            axImageList[idx].Grab = true;
                            logMessage(true, "LMI Grap = true", Interop.Common.Util.cLog.eLogType.LOG);
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        axImageList[idx].Grab = false;
                        System.Threading.Thread.Sleep(500);
                        logMessage(true, "LMI Grap = false", Interop.Common.Util.cLog.eLogType.LOG);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logException("fnScanSensor Error : " + ex.Message.ToString());
                result = false;
            }
            return result;
        }



        private void mRunInspectionEdge(AxCVIMAGELib.AxCVimage[] _inImageList)
        //(AxCVDISPLAYLib.AxCVdisplay _dsp, Cvb.Image.IMG _inImage, structSub_Camera _resultCamera, structSettingVision_Camera[] _settingCamera, out structSub_Camera _outResult)
        {
            //_outResult = new structSub_Camera();
            //_outResult.clear();
            //_outResult = _resultCamera;

            try
            {

                {
                    //double dPixelmm = dPixelmmX;
                    //bool bIsFeaturesT = this.bIsFeatures;

#if Debug
                    bIsFeaturesT = true;
#endif

                    Interop.Common.CVB.cStruct.stEdgeOutputData[] resultDataList = new Interop.Common.CVB.cStruct.stEdgeOutputData[NUM_CAM];
                    Interop.Common.CVB.cStruct.stCvbArea[] area = new Interop.Common.CVB.cStruct.stCvbArea[NUM_CAM];

                    string errMessage = string.Empty;
                    //CGlobal.eJudge bResult = CGlobal.eJudge.OK;
                    //Color tmpJudgeColor = new Color();

                    //area.Left = _settingCamera[iHoleNo].InspLeft;
                    //area.Top = _settingCamera[iHoleNo].InspTop;
                    //area.Right = _settingCamera[iHoleNo].InspRight;
                    //area.Bottom = _settingCamera[iHoleNo].InspBottom;



                    for (int camIdx = 0; camIdx < axImageList.Length; camIdx++)
                    {
                        double x0, y0, x1, y1, x2, y2;
                        x0 = y0 = x1 = y1 = x2 = y2 = 0;
                        axDisplayList[camIdx].GetSelectedArea(ref x0, ref y0, ref x1, ref y1, ref x2, ref y2);

                        area[camIdx].Left = x0;
                        area[camIdx].Top = y0;
                        area[camIdx].Right = x2;
                        area[camIdx].Bottom = y1;

                        // Run Edge detector every 10 pixels
                        int pixelStep = 3;// (int)(area[camIdx].Bottom - area[camIdx].Top) / NUM_STEPS;
                        if (pixelStep < 2)
                            continue;
                        else
                        {
                            Cvb.Edge.TEdgeResult prevPoint;
                            Interop.Common.CVB.cStruct.stCvbArea tmpArea;
                            int steps = 0;
                            do
                            {

                                //for (int steps = 0; steps < NUM_STEPS; steps++)
                                //{
                                // Store previous point Edge result
                                prevPoint = resultDataList[camIdx].EdgeResult;

                                // Set the new ROI for Edge detector
                                tmpArea.Top = area[camIdx].Top + pixelStep * steps;
                                tmpArea.Bottom = tmpArea.Top + pixelStep;
                                tmpArea.Left = area[camIdx].Left;
                                tmpArea.Right = area[camIdx].Right;
                                Interop.Common.CVB.Util.UtilEdge.RunProcess(axImageList[camIdx].Image,
                                                                            tmpArea,
                                                                            settingEdgeInputList[camIdx].EdgePositive,
                                                                            settingEdgeInputList[camIdx].ThresholdResult_Min,
                                                                            out resultDataList[camIdx],
                                                                            out errMessage);

                                // Label the first point on the edge result
                                //axDisplayList[camIdx].AddLabel(steps.ToString(),
                                //                                false,
                                //                                200,
                                //                                0,
                                //                                (int)resultDataList[camIdx].EdgeResult.x,
                                //                                (int)resultDataList[camIdx].EdgeResult.y);

                                // Draw the edge, possible if there is more than 2 times the Edge detector is ran
                                if (steps >= 1)
                                {
                                    Cvb.Image.PIXELLIST pixelLine = Cvb.Image.CreatePixelList(3);
                                    Cvb.Image.AddPixel(pixelLine, new double[] { resultDataList[camIdx].EdgeResult.x, resultDataList[camIdx].EdgeResult.y });
                                    Cvb.Image.AddPixel(pixelLine, new double[] { prevPoint.x, prevPoint.y });

                                    axDisplayList[camIdx].AddOverlayObjectNET("Line",
                                                                                "My Line",
                                                                                false,
                                                                                false,
                                                                                Cvb.Plugin.ColorToInt32(Color.Red),
                                                                                255,
                                                                                true,
                                                                                10000,
                                                                                pixelLine.ToInt32(),
                                                                                IntPtr.Zero);
                                    axDisplayList[camIdx].Refresh();
                                }
                                steps++;
                            } while (tmpArea.Bottom < area[camIdx].Bottom);
                            //}


                        }

                    }

                    if (string.IsNullOrEmpty(errMessage) && resultDataList.Length > 0)
                    {
                        //moveX = Interop.Common.Util.cZutil.mMoveCalculation((int)_settingCamera[iHoleNo].MasterX, (int)resultDataList[0].CenterX, dPixelmm);
                        //moveY = Interop.Common.Util.cZutil.mMoveCalculation((int)_settingCamera[iHoleNo].MasterY, (int)resultDataList[0].CenterY, dPixelmm);
                        //moveZ = 0;//mMoveCalculation ( ( int )_settingCamera[ inx ].MASTER_Z , ( int )tmpV , sLmiInfo.ZResolution );

                        //userControlCarLine1.mCalCarLine( _areaNo.ToString(), moveX, moveY, moveZ, _settingCamera[ iHoleNo ].OffsetX, _settingCamera[ iHoleNo ].OffsetY, 0, out moveX, out moveY, out moveZ );

                        //float coordinateX = Interop.Common.Util.CUtil.mStringToFloat(Interop.Common.Util.CIni.IniReadValue("X_1_1", "Coordinate").Trim());
                        //float coordinateY = Interop.Common.Util.CUtil.mStringToFloat(Interop.Common.Util.CIni.IniReadValue("Y_1_1", "Coordinate").Trim());

                        //moveX = System.Math.Round(moveX * 100) * 0.01 * (coordinateX != 0 ? coordinateX : 1);
                        //moveY = System.Math.Round(moveY * 100) * 0.01 * (coordinateY != 0 ? coordinateY : 1);
                        //moveZ = System.Math.Round(moveZ * 100) * 0.01;

                        //_outResult.xValue = (float)moveX;
                        //_outResult.yValue = (float)moveY;


                        //// 판정
                        //if ((float)moveX >= _settingCamera[iHoleNo].xValueMin && (float)moveX <= _settingCamera[iHoleNo].xValueMax && (float)moveY >= _settingCamera[iHoleNo].yValueMin && (float)moveY <= (float)_settingCamera[iHoleNo].yValueMax
                        //   )
                        //{
                        //    _outResult.HoleResult = CGlobal.eJudge.OK.ToString();
                        //}
                        //else
                        //{
                        //    _outResult.HoleResult = CGlobal.eJudge.NG.ToString();
                        //}
                    }
                    else
                    {

                        //_outResult.xValue = 0;
                        //_outResult.yValue = 0;
                        //_outResult.HoleResult = CGlobal.eJudge.NG.ToString();
                    }

                    //Console.WriteLine("Area : " + _areaNo + " ,  Hole No : " + iHoleNo + ", Judge : " + _outResult.HoleResult);
                }
            }
            catch (Exception ex)
            {
                string errString = string.Format("mRunInspectionSF 에러  : {0}", ex.ToString());
                // Console.WriteLine ( errString );
                //CLog.FileWrite_Str(errString, CLog.eLogType.EXCEPTION);
            }

        }

        private void axCVgrabber2_ImageUpdated(object sender, EventArgs e)
        {
            axImageList[1].Image = axCVgrabber2.Image;
            axDisplayList[1].Image = axCVgrabber2.Image;
        }

        private void axCVimage2_ImageUpdated(object sender, EventArgs e)
        {
            axCVgrabber2.Image = axImageList[1].Image;
            axDisplayList[1].Image = axImageList[1].Image;
        }

        private void axCVgrabber1_ImageUpdated(object sender, EventArgs e)
        {
            axImageList[0].Image = axCVgrabber1.Image;
            axDisplayList[0].Image = axCVgrabber1.Image;
        }

        private void axCVimage1_ImageUpdated(object sender, EventArgs e)
        {
            axCVgrabber1.Image = axImageList[0].Image;
            axDisplayList[0].Image = axImageList[0].Image;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }




    }
}
