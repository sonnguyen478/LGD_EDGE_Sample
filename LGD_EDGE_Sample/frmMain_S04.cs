#define UsePLC
//#define UseCam
//#define UseDatabase
#define INSPECTION_STEP04

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace LGD_EDGE_Sample
{
    public partial class frmMain_S04 : Form
    {
        public const int NUM_CAM = 4;
        public const int NUM_STEPS = 5;

        private bool bIsDelay = true;
        private bool bIsStartClose = false;
        private bool isInspection = false; // 검사 진행 여부

        private AxCVDISPLAYLib.AxCVdisplay[] axDisplayList; // 화면
        private AxCVIMAGELib.AxCVimage[] axImageList;// Image
        private AxCVGRABBERLib.AxCVgrabber[] axGrabberList;
        private Interop.Common.CVB.cStruct.stEdgeInputData[] settingEdgeInputList;

        private Interop.Common.Progress.SplashThread loadProgress = null;
        private Interop.Common.DB.cSQLServer localdbConn = null;
        private cGlobalDefine.stProdCount stProdCnt;

        private Interop.Common.PLC.Melsec.CMxComponentDirect cMxCom = null;
        private cBGWorkerEvents.PLCEvents eventPLC = null;
        private cBGWorkerEvents.EventHeartBit eventHeartBit = null;
        private System.Timers.Timer tmrHeartBitPLCCheck = null;

        private string sInspectionStartTime = string.Empty;
        private string sInspectionModel = string.Empty;

        private cDBInfo.InspSaveMain saveMain;
        private cDBInfo.InspSaveSub[] saveSub = null;
        private cDBInfo.InspSettingEdge[] settingModel = null;

        private int bPCHeartBit = 0;
        private int beforPLCHeartBit = -1;
        public bool[] beforPLCBit = new bool[30];

        private int nInspCnt = 2;

        public frmMain_S04()
        {
            InitializeComponent();

            btnModelSettings.Image = (Image)(new Bitmap(btnModelSettings.Image, new Size(32, 32)));
            //btnManualMode.Image = (Image)(new Bitmap(btnManualMode.Image, new Size(32, 32)));
            //btnAutoMode.Image = (Image)(new Bitmap(btnAutoMode.Image, new Size(32, 32)));
            //btnAutoMode1.Image = (Image)(new Bitmap(btnAutoMode1.Image, new Size(32, 32)));
            btnSnapImg.Image = (Image)(new Bitmap(btnSnapImg.Image, new Size(32, 32)));
            btnExit.Image = (Image)(new Bitmap(btnExit.Image, new Size(32, 32)));
            btnRunInspection.Image = (Image)(new Bitmap(btnRunInspection.Image, new Size(32, 32)));


            loadProgress = new Interop.Common.Progress.SplashThread();
            loadProgress.AllStepCnt = 9;
        }
        private void frmMain_S04_Load(object sender, EventArgs e)
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

                //mCVBInit();
                fnCAM_Initialize();
#endif
                mCVBInit();

                // 6 --> Program Enviroment Load...
                loadProgress.UpdateProgress(6, "PC Enviroment Loding...");
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
        private void frmMain_S04_FormClosing(object sender, FormClosingEventArgs e)
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
                // Move the marker color to the correspondent button in GUI
                pnlMenuMarker.Height = btnRunInspection.Height;
                Control tmp = tblLayoutMain.GetControlFromPosition(0, 1);   // Get location of each table layout, The coordinate of table layout is reference coodrinate
                Control tmp1 = tblLayoutsub.GetControlFromPosition(1, 0);   // Due to the button is in reference coordinate with the table layout
                pnlMenuMarker.Top = tblLayoutMain.Top + tmp.Top + tmp1.Top + btnRunInspection.Top;
                pnlMenuMarker.Left = tblLayoutMain.Left + tmp.Left + tmp1.Left + btnRunInspection.Left - pnlMenuMarker.Width;
                pnlMenuMarker.Visible = true;
                // Sample code for edge detection in 2 static images
                //clearing all previous labels
                fnCVBDisplayClear();

                mRunInspection(axImageList);
                //if (chkPLC_InspStart.Checked == true)
                //{
                //    cMxCom.Word_Write("D9101", 1);
                //}
                //else
                //{
                //    cMxCom.Word_Write("D9101", 0);
                //}


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
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
                //if (chkPLC_HB.Checked == true)
                //{
                //    readValue = cMxCom.Word_Read("D9100");

                //    cMxCom.Word_Write("D9100", (readValue == 1 ? 0 : 1));
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        // Summary:
        //     Read PLC status and run the inspection if needed
        //
        // Parameters:
        //   sender, e:
        //     Run all the time, sleep 300ms after running
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

                    System.Threading.Thread.Sleep(300);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                }
            } while (true);
        }
        #endregion [ PC Control Action ]


        #region [ PLC Debug ]
        //private void chkPLC_HB_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chkPLC_HB.Checked == true)
        //    {
        //        tmrDebugPLC.Interval = 500;
        //        tmrDebugPLC.Start();
        //    }
        //    else
        //    {
        //        tmrDebugPLC.Stop();
        //    }
        //}


        #endregion [ PLC Debug ]



        #region [GUI fearture action]
        private void btnModelSettings_Click(object sender, EventArgs e)
        {
            // Move the marker color to the correspondent button in GUI
            pnlMenuMarker.Height = btnModelSettings.Height;
            pnlMenuMarker.Top = btnModelSettings.Top;
            Control tmp = tblLayoutMain.GetControlFromPosition(0, 1);   // Get location of each table layout, The coordinate of table layout is reference coodrinate
            Control tmp1 = tblLayoutsub.GetControlFromPosition(1, 0);   // Due to the button is in reference coordinate with the table layout
            pnlMenuMarker.Top = tblLayoutMain.Top + tmp.Top + tmp1.Top + btnModelSettings.Top;
            pnlMenuMarker.Left = tblLayoutMain.Left + tmp.Left + tmp1.Left + btnModelSettings.Left - pnlMenuMarker.Width;
            pnlMenuMarker.Visible = true;

            // Load Model Setting form
            try
            {
#if INSPECTION_STEP04
                using (frmModelSettings modelSet = new frmModelSettings(this, localdbConn))
                {
                    // Turn off the Main Inspection form
                    // to focus on the Model Settings only
                    this.Visible = false;
                    modelSet.ShowDialog(this);
                }
#endif
            }
            catch (Exception ex)
            {
                logException("btnModelSetting_Click Error : " + ex.Message.ToString());
            }
        }

        private void btnSnapImg_Click(object sender, EventArgs e)
        {
            try
            {
                for (int camIdx = 0; camIdx < NUM_CAM; camIdx++)
                {
                    axImageList[camIdx].Snap();
                }
            }
            catch (Exception ex)
            {
                logException("btnSnapImg_Click Error : " + ex.Message.ToString());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Move the marker color to the correspondent button in GUI
            pnlMenuMarker.Height = btnExit.Height;
            Control tmp = tblLayoutMain.GetControlFromPosition(0, 1);   // Get location of each table layout, The coordinate of table layout is reference coodrinate
            Control tmp1 = tblLayoutsub.GetControlFromPosition(1, 0);   // Due to the button is in reference coordinate with the table layout
            pnlMenuMarker.Top = tblLayoutMain.Top + tmp.Top + tmp1.Top + btnExit.Top;
            pnlMenuMarker.Left = tblLayoutMain.Left + tmp.Left + tmp1.Left + btnExit.Left - pnlMenuMarker.Width;
            pnlMenuMarker.Visible = true;
            this.Close();
        }

        private void axCVgrabber1_ImageUpdated(object sender, EventArgs e)
        {
            try
            {
                axImageList[0].Image = axGrabberList[0].Image;
                axDisplayList[0].Image = axGrabberList[0].Image;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void axCVimage1_ImageUpdated(object sender, EventArgs e)
        {
            try
            {
                axGrabberList[0].Image = axImageList[0].Image;
                axDisplayList[0].Image = axImageList[0].Image;

                // Run inspection depends on Auto/Manual mode
                if (radAutoMod.Checked == true)
                    mRunInspection(axImageList);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void axCVgrabber2_ImageUpdated(object sender, EventArgs e)
        {
            try
            {
                axImageList[1].Image = axGrabberList[1].Image;
                axDisplayList[1].Image = axGrabberList[1].Image;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void axCVimage2_ImageUpdated(object sender, EventArgs e)
        {
            try
            {
                axGrabberList[1].Image = axImageList[1].Image;
                axDisplayList[1].Image = axImageList[1].Image;

                // Run inspection depends on Auto/Manual mode
                if (radAutoMod.Checked == true)
                    mRunInspection(axImageList);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void axCVgrabber3_ImageUpdated(object sender, EventArgs e)
        {
            try
            {
                axImageList[2].Image = axGrabberList[2].Image;
                axDisplayList[2].Image = axGrabberList[2].Image;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void axCVimage3_ImageUpdated(object sender, EventArgs e)
        {
            try
            {
                axGrabberList[2].Image = axImageList[2].Image;
                axDisplayList[2].Image = axImageList[2].Image;

                // Run inspection depends on Auto/Manual mode
                if (radAutoMod.Checked == true)
                    mRunInspection(axImageList);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void axCVgrabber4_ImageUpdated(object sender, EventArgs e)
        {
            try
            {
                axImageList[3].Image = axGrabberList[3].Image;
                axDisplayList[3].Image = axGrabberList[3].Image;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void axCVimage4_ImageUpdated(object sender, EventArgs e)
        {
            try
            {
                axGrabberList[3].Image = axImageList[3].Image;
                axDisplayList[3].Image = axImageList[3].Image;

                // Run inspection depends on Auto/Manual mode
                if (radAutoMod.Checked == true)
                    mRunInspection(axImageList);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void axCVdisplay1_DblClick(object sender, EventArgs e)
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
        }

        private void axCVdisplay2_DblClick(object sender, EventArgs e)
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
        }

        private void radAutoMod_CheckedChanged(object sender, EventArgs e)
        {
            //radManualMod.Checked = false;
            btnRunInspection.Enabled = false;
            btnSnapImg.Enabled = false;

            // enable the grabbing image automaticcaly
            fnGrabImgSet(true);
        }

        private void radManualMod_CheckedChanged(object sender, EventArgs e)
        {
            //radAutoMod.Checked = false;
            btnRunInspection.Enabled = true;
            btnSnapImg.Enabled = true;

            // enable the grabbing image automaticcaly
            fnGrabImgSet(false);
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            cMxCom.Word_Write("D9102", cbModel.SelectedIndex);
        }

        #endregion [GUI fearture action]


        #region [Inspection function]
        private void mRunInspection(AxCVIMAGELib.AxCVimage[] _inImageList)
        {
            try
            {
                for (int camIdx = 0; camIdx < axImageList.Length; camIdx++)
                {
                    string currDate = System.DateTime.Now.ToString("yyyyMMdd");
                    string fileSavePath = System.Windows.Forms.Application.StartupPath + @"D:\\Data_Images\\" + saveMain.model + "\\" + currDate;
                    if (!System.IO.Directory.Exists(fileSavePath)) System.IO.Directory.CreateDirectory(fileSavePath);
                    _inImageList[camIdx].SaveImage(fileSavePath + "\\" + currDate + "\\" + saveMain.dateTime + "_cam" + camIdx.ToString() + ".jpg");
                }

                fnGrabImgSet(false);
                logMessage(true, " #SNAP Grap = false, img Snaped", Interop.Common.Util.cLog.eLogType.LOG);

                fnPCBitState(cGlobalDefine.ePCSignal.INSP_START, cGlobalDefine.ColorOK);

                mRunInspectionEdge(_inImageList);
                fnGrabImgSet(false);

                mRunInspection_Complete();

                cMxCom.Word_Write_Str("D9003", "OK");
                cMxCom.Word_Write("D9002", 1);

                fnPCBitState(cGlobalDefine.ePCSignal.INSP_START, cGlobalDefine.ColorDefault);
                fnPCBitState(cGlobalDefine.ePCSignal.INSP_COMP, cGlobalDefine.ColorOK);
            }
            catch (Exception ex)
            {
                logException("axCVimage_Snap_ImageSnaped Error : " + ex.Message.ToString());
            }
        }

        private void mRunInspectionEdge(AxCVIMAGELib.AxCVimage[] _inImageList)
        {

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

        // Summary:
        //     Update counting inspection products
        //     Save data to SQL server
        //
        // Parameters:
        //   
        private void mRunInspection_Complete()
        {
            try
            {
                stProdCnt.nTotal++;
                if (saveMain.finalResult == "OK")
                {
                    stProdCnt.nOK++;
                }
                else
                {
                    stProdCnt.nNG++;
                }

#if UsePLC
                //cMxCom.Word_Write_Str( "D9003", saveMain.totalResult );
                cMxCom.Word_Write_Str("D9003", "OK");
                cMxCom.Word_Write("D9002", 1);
#endif
                //PC 신호 초기화 타이머 구동
                //2초후에 D9001, D9002, D9003 0 으로 리셋
                //tmrPLCReset.Start();

                //fnProdCntWrite(stProdCnt);
                //fnProdCntDisplay();

                //Interop.Common.Util.CDelegate.SetText(lblTotalResult, saveMain.totalResult);
                //Interop.Common.Util.CDelegate.SetBackColor(lblTotalResult, (saveMain.totalResult == "OK" ? Color.PaleGreen : Color.Red));

                // Store current inspection history to the SQL database
                if (localdbConn.IsConnect() == false) localdbConn.SQLOpen();
                localdbConn.SQLExecute(cDBInfo.saveHistoryMain(saveMain));
                for (int camIdx = 0; camIdx < NUM_CAM; camIdx++)
                {
                    localdbConn.SQLExecute(cDBInfo.saveHistorySub(saveSub[camIdx]));
                }
                if (localdbConn.IsConnect() == true) localdbConn.CloseDataBase();
                logMessage(true, "Local History Save Complete", Interop.Common.Util.cLog.eLogType.LOG);

#if UseRemoteDB
                if ( remotedbConn.IsConnect() == false ) remotedbConn.SQLOpen();
                remotedbConn.SQLExecute( cDBInfo.saveHistoryMain( saveMain ) );
                remotedbConn.SQLExecute( cDBInfo.saveHistorySub( saveSub ) );
                if ( remotedbConn.IsConnect() == true ) remotedbConn.CloseDataBase();
                logMessage( true, "Remote History Save Complete", Interop.Common.Util.cLog.eLogType.LOG );
#endif

                axCVdisplay1.Refresh();
                isInspection = false;
                //fnDisplayHDDSpace();
                //logMessage(true, "검사 완료 : " + saveMain.totalResult, Interop.Common.Util.cLog.eLogType.LOG);
            }
            catch (Exception ex)
            {
                logException("mRunInspection_Complete Error : " + ex.Message.ToString());
            }
        }

        #endregion [Inspection function]


        #region [Database function]
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

        #endregion [Database function]

        #region [Log function]
        private void logException(string _msg)
        {
            Interop.Common.Util.cLog.FileWrite_Str(_msg, Interop.Common.Util.cLog.eLogType.EXCEPTION);
            string dispTime = System.DateTime.Now.ToString("HH:mm:ss");
            if (listException.Items.Count >= 200) listException.Items.Clear();

            listException.Items.Add(string.Format("[{0}]{1}", dispTime, _msg));
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
                    if (listVision.Items.Count >= 200) listVision.Items.Clear();
                    listVision.Items.Add(string.Format("[{0}]{1}", dispTime, _msg));
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
                    if (listCom.Items.Count >= 200) listCom.Items.Clear();

                    listCom.Items.Add(string.Format("[{0}]{1}", dispTime, _msg));
                }));
                if (_saveFlag == true) Interop.Common.Util.cLog.FileWrite_Str(saveMsg, _type);
            }
            catch (Exception ex)
            {
                logException("Comm Data Save Fail : " + ex.Message.ToString());
            }
        }

        #endregion [Log function]

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

                if (axImageList[0].LoadImage(driverString))
                { bIsOpen = true; }
                else
                { MessageBox.Show("#1 open error"); }
                if (bIsOpen == true) { if (axImageList[1].LoadImage(driverString)) { } else { bIsOpen = false; MessageBox.Show("#2 open error"); } }
                if (bIsOpen == true) { if (axImageList[2].LoadImage(driverString)) { } else { bIsOpen = false; MessageBox.Show("#2 open error"); } }
                if (bIsOpen == true) { if (axImageList[3].LoadImage(driverString)) { } else { bIsOpen = false; MessageBox.Show("#2 open error"); } }
                if (bIsOpen == true) { if (axImageList[4].LoadImage(driverString)) { } else { bIsOpen = false; MessageBox.Show("#2 open error"); } }
                if (bIsOpen == true) { if (axImageList[5].LoadImage(driverString)) { } else { bIsOpen = false; MessageBox.Show("#2 open error"); } }
                if (bIsOpen == true) { if (axImageList[6].LoadImage(driverString)) { } else { bIsOpen = false; MessageBox.Show("#2 open error"); } }
                if (bIsOpen == true) { if (axImageList[7].LoadImage(driverString)) { } else { bIsOpen = false; MessageBox.Show("#2 open error"); } }

                if (bIsOpen == true)
                {
                    axGrabberList[0].CamPort = 0; axGrabberList[0].Image = axImageList[0].Image;
                    axGrabberList[1].CamPort = 1; axGrabberList[1].Image = axImageList[1].Image;
                    axGrabberList[2].CamPort = 2; axGrabberList[2].Image = axImageList[2].Image;
                    axGrabberList[3].CamPort = 3; axGrabberList[3].Image = axImageList[3].Image;
                    axGrabberList[4].CamPort = 4; axGrabberList[4].Image = axImageList[4].Image;
                    axGrabberList[5].CamPort = 5; axGrabberList[5].Image = axImageList[5].Image;
                    axGrabberList[6].CamPort = 6; axGrabberList[6].Image = axImageList[6].Image;
                    axGrabberList[7].CamPort = 7; axGrabberList[7].Image = axImageList[7].Image;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        private void mCVBInit()
        {
            axDisplayList = new AxCVDISPLAYLib.AxCVdisplay[cGlobalDefine.InspCamCnt];
            axImageList = new AxCVIMAGELib.AxCVimage[cGlobalDefine.InspCamCnt];
            axGrabberList = new AxCVGRABBERLib.AxCVgrabber[cGlobalDefine.InspCamCnt];

            axDisplayList[0] = axCVdisplay1;
            axDisplayList[1] = axCVdisplay2;
            axDisplayList[2] = axCVdisplay3;
            axDisplayList[3] = axCVdisplay4;

            axImageList[0] = axCVimage1;
            axImageList[1] = axCVimage2;
            axImageList[2] = axCVimage3;
            axImageList[3] = axCVimage4;

            axGrabberList[0] = axCVgrabber1;
            axGrabberList[1] = axCVgrabber2;
            axGrabberList[2] = axCVgrabber3;
            axGrabberList[3] = axCVgrabber4;

            settingEdgeInputList = new Interop.Common.CVB.cStruct.stEdgeInputData[2 * NUM_CAM];

            // load default image
            axImageList[0].LoadImage(@"D:\SONNGUYEN\07_Projects\02_LGD_VN_LCDInspection\00_Docs\Image_Sample\04_CG_OCA1_TOUCH_OCA2_BA_BACK(IR)\CG_OCA1_TOUCH_OCA2_BA_BACK(IR)1.bmp");
            axDisplayList[0].SetSelectedArea(50.0, 340.0, 50.0, 360.0, 590.0, 340.0);
            axImageList[1].LoadImage(@"D:\SONNGUYEN\07_Projects\02_LGD_VN_LCDInspection\00_Docs\Image_Sample\04_CG_OCA1_TOUCH_OCA2_BA_BACK(IR)\CG_OCA1_TOUCH_OCA2_BA_BACK(IR)2.bmp");
            axDisplayList[1].SetSelectedArea(50.0, 340.0, 50.0, 360.0, 590.0, 340.0);
            axImageList[2].LoadImage(@"D:\SONNGUYEN\07_Projects\02_LGD_VN_LCDInspection\00_Docs\Image_Sample\04_CG_OCA1_TOUCH_OCA2_BA_BACK(IR)\CG_OCA1_TOUCH_OCA2_BA_BACK(IR)3.bmp");
            axDisplayList[2].SetSelectedArea(50.0, 340.0, 50.0, 360.0, 590.0, 340.0);
            axImageList[3].LoadImage(@"D:\SONNGUYEN\07_Projects\02_LGD_VN_LCDInspection\00_Docs\Image_Sample\04_CG_OCA1_TOUCH_OCA2_BA_BACK(IR)\CG_OCA1_TOUCH_OCA2_BA_BACK(IR)4.bmp");
            axDisplayList[3].SetSelectedArea(50.0, 340.0, 50.0, 360.0, 590.0, 340.0);


            // Adding Model
            cbModel.Items.Clear();
            cbModel.Items.Add(string.Format("{0}", Interop.Common.Util.CGlobal.eModel.LGD_E61_CG_OCA1.ToString()));
            cbModel.Items.Add(string.Format("{0}", Interop.Common.Util.CGlobal.eModel.LGD_E61_CG_OCA1_TOUCH.ToString()));
            cbModel.Items.Add(string.Format("{0}", Interop.Common.Util.CGlobal.eModel.LGD_E61_CG_OCA1_TOUCH_OCA2.ToString()));
            cbModel.Items.Add(string.Format("{0}", Interop.Common.Util.CGlobal.eModel.LGD_E61_CG_OCA1_TOUCH_OCA2_BA.ToString()));
            cbModel.Items.Add(string.Format("{0}", Interop.Common.Util.CGlobal.eModel.LGD_E61_CG_OCA1_TOUCH_OCA2_BA_METAL.ToString()));
            cbModel.Items.Add(string.Format("{0}", Interop.Common.Util.CGlobal.eModel.LGD_E61_CG_OCA1_TOUCH_OCA2_BA_METAL_X758.ToString()));
            cbModel.SelectedIndex = 3;

            //for (int camIdx = 0; camIdx < NUM_CAM; camIdx++)
            //{
            //    localdbConn.SQLExecute(cDBInfo.fnGetModelSettingData(settingModel[camIdx].model, camIdx, settingModel[camIdx].inspSide));
            //    settingEdgeInputList[2 * camIdx].EdgePositive = (settingModel[camIdx].posNegEdge == "POSITIVE") ? Interop.Common.CVB.cEnum.eEdgePositive.Positive
            //                                                                                                    : Interop.Common.CVB.cEnum.eEdgePositive.Negative;
            //    settingEdgeInputList[2 * camIdx].ThresholdResult_Min = settingModel[camIdx].thresholdMin;

            //    settingEdgeInputList[2 * camIdx + 1].EdgePositive = (settingModel[camIdx].posNegEdge == "POSITIVE") ? Interop.Common.CVB.cEnum.eEdgePositive.Positive
            //                                                                                                    : Interop.Common.CVB.cEnum.eEdgePositive.Negative;
            //    settingEdgeInputList[2 * camIdx + 1].ThresholdResult_Min = settingModel[camIdx].thresholdMax;
            //    //settingModel[camIdx].posNegEdge == 

            //}

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
                            logMessage(true, "Camera Grap = true", Interop.Common.Util.cLog.eLogType.LOG);
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
                        logMessage(true, "Camera Grap = false", Interop.Common.Util.cLog.eLogType.LOG);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                logException("fnGrabImgSet Error : " + ex.Message.ToString());
                result = false;
            }
            return result;
        }

        // Summary:
        //     Loading model, inspection area, acceptable range from SQL server
        //
        // Parameters:
        //  _model:
        //     Model number
        private void fnLoadModelSettings(string _model)
        {
            // Load a setting in the database
            DataTable makeData = new DataTable();
            try
            {
                for (int camIdx = 0; camIdx < NUM_CAM; camIdx++)
                {
                    makeData = localdbConn.GetSelectTable(cDBInfo.fnGetModelSettingData(settingModel[camIdx].model, camIdx));
                    if (makeData.Rows.Count > 0)
                    {
                        settingModel[camIdx].inner.threshold = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][2].ToString());
                        settingModel[camIdx].inner.edgePolarity = makeData.Rows[0][3].ToString();
                        settingModel[camIdx].inner.inspAreaTopLeft_X = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][4].ToString());
                        settingModel[camIdx].inner.inspAreaTopLeft_Y = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][5].ToString());
                        settingModel[camIdx].inner.inspAreaBotRight_X = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][6].ToString());
                        settingModel[camIdx].inner.inspAreaBotRight_Y = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][7].ToString());
                        settingModel[camIdx].outer.threshold = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][8].ToString());
                        settingModel[camIdx].outer.edgePolarity = makeData.Rows[0][9].ToString();
                        settingModel[camIdx].outer.inspAreaTopLeft_X = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][10].ToString());
                        settingModel[camIdx].outer.inspAreaTopLeft_Y = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][11].ToString());
                        settingModel[camIdx].outer.inspAreaBotRight_X = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][12].ToString());
                        settingModel[camIdx].outer.inspAreaBotRight_Y = Interop.Common.Util.CUtil.mStringToInt(makeData.Rows[0][13].ToString());

                    }
                    else
                    {
                        MessageBox.Show(this, "The setting for required model doesn't exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                logException("fnLoadModelSettings Error : " + ex.Message.ToString());
            }
            finally
            {
                makeData = null;
            }
        }

        // Summary:
        //     Remove all labels and objects in the display box
        //
        // Parameters:
        //   
        private void fnCVBDisplayClear()
        {
            for (int idx = 0; idx < axDisplayList.Length; idx++)
            {
                Interop.Common.CVB.CCVBOverlay.OverlayLabelRemoveAll(axDisplayList[idx]);
                Interop.Common.CVB.CCVBOverlay.OverlayObjectRemoveAll(axDisplayList[idx]);
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
                logComm(true, "Sensor error - PLC incorrect behavior.", Interop.Common.Util.cLog.eLogType.COMM);

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

                            // Loading the setting for the current model
                            settingModel = new cDBInfo.InspSettingEdge[NUM_CAM];
                            fnLoadModelSettings(cbModel.Text.Trim());

                            // Initialize the history data table
                            saveMain.Clear();
                            saveMain.dateTime = sInspectionStartTime;
                            saveMain.model = sInspectionModel;
                            for (int idx = 0; idx < saveSub.Length; idx++)
                            {
                                saveSub[idx].Clear();
                                saveSub[idx].dateTime = saveMain.dateTime;
                                saveSub[idx].model = saveMain.model;
                            }


                            fnGrabImgSet(true);
                            /*
                            Interop.Common.Util.CDelegate.SetText(lblCurrInspTime, sInspectionStartTime);
                            Interop.Common.Util.CDelegate.SetText(lblCurrInspModel, sInspectionModel);

                            fnScanSensor(true);


                            */
#if UsePLC

                            System.Threading.Thread.Sleep(200);

                            cMxCom.Word_Write("D9001", 1);
#endif
                        }
                    }
                }
                else if (e.data == 0 && beforPLCBit[iAddr] == true)
                {
                    fnPLCBitState(cGlobalDefine.ePLCSignal.INSP_START, cGlobalDefine.ColorDefault);
                    fnPCBitState(cGlobalDefine.ePCSignal.INSP_COMP, cGlobalDefine.ColorDefault);
                    beforPLCBit[iAddr] = false;
                    logComm(true, "(PLC)검사시작 Off", Interop.Common.Util.cLog.eLogType.COMM);

                    cMxCom.Word_Write("D9001", 0);
                }
            }
            catch (Exception ex)
            {
                logException("EventPLC_evInspStart Error : " + ex.Message.ToString());
            }
        }

    }
}
