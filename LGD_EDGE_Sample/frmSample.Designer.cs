namespace LGD_EDGE_Sample
{
    partial class frmSample
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSample));
            this.axCVdisplay1 = new AxCVDISPLAYLib.AxCVdisplay();
            this.bgWorkPLC = new System.ComponentModel.BackgroundWorker();
            this.tmrSystem = new System.Windows.Forms.Timer(this.components);
            this.axCVgrabber1 = new AxCVGRABBERLib.AxCVgrabber();
            this.axCVimage1 = new AxCVIMAGELib.AxCVimage();
            this.label6 = new System.Windows.Forms.Label();
            this.lblPLC_HB = new System.Windows.Forms.Label();
            this.lblPLC_InspStart = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPC_HB = new System.Windows.Forms.Label();
            this.lblPC_InspStart = new System.Windows.Forms.Label();
            this.lblPC_InspEnd = new System.Windows.Forms.Label();
            this.lblPC_TotalJudge = new System.Windows.Forms.Label();
            this.lblSystemTime = new System.Windows.Forms.Label();
            this.tmrDebugPLC = new System.Windows.Forms.Timer(this.components);
            this.axCVdisplay2 = new AxCVDISPLAYLib.AxCVdisplay();
            this.axCVgrabber2 = new AxCVGRABBERLib.AxCVgrabber();
            this.axCVimage2 = new AxCVIMAGELib.AxCVimage();
            this.label9 = new System.Windows.Forms.Label();
            this.btnRunInspection = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lstComm = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.lstVision = new System.Windows.Forms.ListBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.tblLayoutsub = new System.Windows.Forms.TableLayoutPanel();
            this.tblLayoutCam = new System.Windows.Forms.TableLayoutPanel();
            this.axCVdisplay3 = new AxCVDISPLAYLib.AxCVdisplay();
            this.axCVdisplay4 = new AxCVDISPLAYLib.AxCVdisplay();
            this.axCVdisplay5 = new AxCVDISPLAYLib.AxCVdisplay();
            this.axCVdisplay7 = new AxCVDISPLAYLib.AxCVdisplay();
            this.axCVdisplay6 = new AxCVDISPLAYLib.AxCVdisplay();
            this.axCVdisplay8 = new AxCVDISPLAYLib.AxCVdisplay();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblCamRet01 = new System.Windows.Forms.Label();
            this.tableLayoutPanel20 = new System.Windows.Forms.TableLayoutPanel();
            this.label23 = new System.Windows.Forms.Label();
            this.lblCamRet02 = new System.Windows.Forms.Label();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.label25 = new System.Windows.Forms.Label();
            this.lblCamRet03 = new System.Windows.Forms.Label();
            this.tableLayoutPanel22 = new System.Windows.Forms.TableLayoutPanel();
            this.label27 = new System.Windows.Forms.Label();
            this.lblCamRet04 = new System.Windows.Forms.Label();
            this.tableLayoutPanel23 = new System.Windows.Forms.TableLayoutPanel();
            this.label18 = new System.Windows.Forms.Label();
            this.lblCamRet05 = new System.Windows.Forms.Label();
            this.tableLayoutPanel24 = new System.Windows.Forms.TableLayoutPanel();
            this.label20 = new System.Windows.Forms.Label();
            this.lblCamRet06 = new System.Windows.Forms.Label();
            this.tableLayoutPanel25 = new System.Windows.Forms.TableLayoutPanel();
            this.label29 = new System.Windows.Forms.Label();
            this.lblCamRet07 = new System.Windows.Forms.Label();
            this.tableLayoutPanel26 = new System.Windows.Forms.TableLayoutPanel();
            this.label31 = new System.Windows.Forms.Label();
            this.lblCamRet08 = new System.Windows.Forms.Label();
            this.tblLayoutMenu = new System.Windows.Forms.TableLayoutPanel();
            this.radManualMod = new System.Windows.Forms.RadioButton();
            this.radAutoMod = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.chkPositive01 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.EdgeThresholdMin01 = new System.Windows.Forms.Label();
            this.EdgeThresholdMax01 = new System.Windows.Forms.Label();
            this.tbMin01 = new System.Windows.Forms.TrackBar();
            this.tbMax01 = new System.Windows.Forms.TrackBar();
            this.chkPositive02 = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.EdgeThresholdMin02 = new System.Windows.Forms.Label();
            this.EdgeThresholdMax02 = new System.Windows.Forms.Label();
            this.tbMin02 = new System.Windows.Forms.TrackBar();
            this.tbMax02 = new System.Windows.Forms.TrackBar();
            this.btnAutoMode = new System.Windows.Forms.Button();
            this.btnModelSettings = new System.Windows.Forms.Button();
            this.axCVgrabber3 = new AxCVGRABBERLib.AxCVgrabber();
            this.axCVimage3 = new AxCVIMAGELib.AxCVimage();
            this.axCVgrabber4 = new AxCVGRABBERLib.AxCVgrabber();
            this.axCVimage4 = new AxCVIMAGELib.AxCVimage();
            this.axCVgrabber5 = new AxCVGRABBERLib.AxCVgrabber();
            this.axCVimage5 = new AxCVIMAGELib.AxCVimage();
            this.axCVgrabber6 = new AxCVGRABBERLib.AxCVgrabber();
            this.axCVgrabber7 = new AxCVGRABBERLib.AxCVgrabber();
            this.axCVgrabber8 = new AxCVGRABBERLib.AxCVgrabber();
            this.axCVimage6 = new AxCVIMAGELib.AxCVimage();
            this.axCVimage7 = new AxCVIMAGELib.AxCVimage();
            this.axCVimage8 = new AxCVIMAGELib.AxCVimage();
            this.tblLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label22 = new System.Windows.Forms.Label();
            this.pnlMenuMarker = new System.Windows.Forms.Panel();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.chkPLC_HB = new System.Windows.Forms.CheckBox();
            this.chkPLC_InspStart = new System.Windows.Forms.CheckBox();
            this.btnPLC_ModelWrite = new System.Windows.Forms.Button();
            this.txtPLC_Model = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tblLayoutsub.SuspendLayout();
            this.tblLayoutCam.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay8)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel20.SuspendLayout();
            this.tableLayoutPanel21.SuspendLayout();
            this.tableLayoutPanel22.SuspendLayout();
            this.tableLayoutPanel23.SuspendLayout();
            this.tableLayoutPanel24.SuspendLayout();
            this.tableLayoutPanel25.SuspendLayout();
            this.tableLayoutPanel26.SuspendLayout();
            this.tblLayoutMenu.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMin01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMax01)).BeginInit();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbMin02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMax02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage8)).BeginInit();
            this.tblLayoutMain.SuspendLayout();
            this.tableLayoutPanel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.SuspendLayout();
            // 
            // axCVdisplay1
            // 
            this.axCVdisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axCVdisplay1.Location = new System.Drawing.Point(3, 34);
            this.axCVdisplay1.Name = "axCVdisplay1";
            this.axCVdisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay1.OcxState")));
            this.axCVdisplay1.Size = new System.Drawing.Size(259, 333);
            this.axCVdisplay1.TabIndex = 0;
            this.axCVdisplay1.DblClick += new System.EventHandler(this.axCVdisplay1_DblClick);
            // 
            // bgWorkPLC
            // 
            this.bgWorkPLC.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorkPLC_DoWork);
            // 
            // tmrSystem
            // 
            this.tmrSystem.Tick += new System.EventHandler(this.tmrSystem_Tick);
            // 
            // axCVgrabber1
            // 
            this.axCVgrabber1.Enabled = true;
            this.axCVgrabber1.Location = new System.Drawing.Point(430, 2);
            this.axCVgrabber1.Name = "axCVgrabber1";
            this.axCVgrabber1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber1.OcxState")));
            this.axCVgrabber1.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber1.TabIndex = 1;
            this.axCVgrabber1.ImageUpdated += new System.EventHandler(this.axCVgrabber1_ImageUpdated);
            // 
            // axCVimage1
            // 
            this.axCVimage1.Enabled = true;
            this.axCVimage1.Location = new System.Drawing.Point(430, 40);
            this.axCVimage1.Name = "axCVimage1";
            this.axCVimage1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage1.OcxState")));
            this.axCVimage1.Size = new System.Drawing.Size(32, 32);
            this.axCVimage1.TabIndex = 2;
            this.axCVimage1.ImageUpdated += new System.EventHandler(this.axCVimage1_ImageUpdated);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.label6, 2);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 33);
            this.label6.TabIndex = 3;
            this.label6.Text = "PLC";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPLC_HB
            // 
            this.lblPLC_HB.BackColor = System.Drawing.Color.White;
            this.lblPLC_HB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPLC_HB.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPLC_HB.ForeColor = System.Drawing.Color.Black;
            this.lblPLC_HB.Location = new System.Drawing.Point(84, 33);
            this.lblPLC_HB.Name = "lblPLC_HB";
            this.lblPLC_HB.Size = new System.Drawing.Size(15, 12);
            this.lblPLC_HB.TabIndex = 3;
            this.lblPLC_HB.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblPLC_InspStart
            // 
            this.lblPLC_InspStart.BackColor = System.Drawing.Color.White;
            this.lblPLC_InspStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPLC_InspStart.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPLC_InspStart.ForeColor = System.Drawing.Color.Black;
            this.lblPLC_InspStart.Location = new System.Drawing.Point(84, 66);
            this.lblPLC_InspStart.Name = "lblPLC_InspStart";
            this.lblPLC_InspStart.Size = new System.Drawing.Size(15, 12);
            this.lblPLC_InspStart.TabIndex = 3;
            this.lblPLC_InspStart.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.label10, 2);
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(105, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 33);
            this.label10.TabIndex = 3;
            this.label10.Text = "PC";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPC_HB
            // 
            this.lblPC_HB.BackColor = System.Drawing.Color.White;
            this.lblPC_HB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPC_HB.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPC_HB.ForeColor = System.Drawing.Color.Black;
            this.lblPC_HB.Location = new System.Drawing.Point(188, 33);
            this.lblPC_HB.Name = "lblPC_HB";
            this.lblPC_HB.Size = new System.Drawing.Size(15, 12);
            this.lblPC_HB.TabIndex = 3;
            this.lblPC_HB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPC_InspStart
            // 
            this.lblPC_InspStart.BackColor = System.Drawing.Color.White;
            this.lblPC_InspStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPC_InspStart.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPC_InspStart.ForeColor = System.Drawing.Color.Black;
            this.lblPC_InspStart.Location = new System.Drawing.Point(188, 66);
            this.lblPC_InspStart.Name = "lblPC_InspStart";
            this.lblPC_InspStart.Size = new System.Drawing.Size(15, 12);
            this.lblPC_InspStart.TabIndex = 3;
            this.lblPC_InspStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPC_InspEnd
            // 
            this.lblPC_InspEnd.BackColor = System.Drawing.Color.White;
            this.lblPC_InspEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPC_InspEnd.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPC_InspEnd.ForeColor = System.Drawing.Color.Black;
            this.lblPC_InspEnd.Location = new System.Drawing.Point(188, 99);
            this.lblPC_InspEnd.Name = "lblPC_InspEnd";
            this.lblPC_InspEnd.Size = new System.Drawing.Size(15, 12);
            this.lblPC_InspEnd.TabIndex = 3;
            this.lblPC_InspEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPC_TotalJudge
            // 
            this.lblPC_TotalJudge.BackColor = System.Drawing.Color.White;
            this.lblPC_TotalJudge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPC_TotalJudge.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPC_TotalJudge.ForeColor = System.Drawing.Color.Black;
            this.lblPC_TotalJudge.Location = new System.Drawing.Point(188, 132);
            this.lblPC_TotalJudge.Name = "lblPC_TotalJudge";
            this.lblPC_TotalJudge.Size = new System.Drawing.Size(15, 13);
            this.lblPC_TotalJudge.TabIndex = 3;
            this.lblPC_TotalJudge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSystemTime
            // 
            this.lblSystemTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.lblSystemTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel2.SetColumnSpan(this.lblSystemTime, 4);
            this.lblSystemTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSystemTime.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSystemTime.ForeColor = System.Drawing.Color.White;
            this.lblSystemTime.Location = new System.Drawing.Point(3, 165);
            this.lblSystemTime.Name = "lblSystemTime";
            this.lblSystemTime.Size = new System.Drawing.Size(200, 35);
            this.lblSystemTime.TabIndex = 3;
            this.lblSystemTime.Text = "0000/00/00 00:00:00";
            this.lblSystemTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tmrDebugPLC
            // 
            this.tmrDebugPLC.Tick += new System.EventHandler(this.tmrDebugPLC_Tick);
            // 
            // axCVdisplay2
            // 
            this.axCVdisplay2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axCVdisplay2.Location = new System.Drawing.Point(268, 34);
            this.axCVdisplay2.Name = "axCVdisplay2";
            this.axCVdisplay2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay2.OcxState")));
            this.axCVdisplay2.Size = new System.Drawing.Size(259, 333);
            this.axCVdisplay2.TabIndex = 0;
            this.axCVdisplay2.DblClick += new System.EventHandler(this.axCVdisplay2_DblClick);
            // 
            // axCVgrabber2
            // 
            this.axCVgrabber2.Enabled = true;
            this.axCVgrabber2.Location = new System.Drawing.Point(468, 2);
            this.axCVgrabber2.Name = "axCVgrabber2";
            this.axCVgrabber2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber2.OcxState")));
            this.axCVgrabber2.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber2.TabIndex = 1;
            this.axCVgrabber2.ImageUpdated += new System.EventHandler(this.axCVgrabber2_ImageUpdated);
            // 
            // axCVimage2
            // 
            this.axCVimage2.Enabled = true;
            this.axCVimage2.Location = new System.Drawing.Point(468, 40);
            this.axCVimage2.Name = "axCVimage2";
            this.axCVimage2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage2.OcxState")));
            this.axCVimage2.Size = new System.Drawing.Size(32, 32);
            this.axCVimage2.TabIndex = 2;
            this.axCVimage2.ImageUpdated += new System.EventHandler(this.axCVimage2_ImageUpdated);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(174, 25);
            this.label9.TabIndex = 3;
            this.label9.Text = "Cam01";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRunInspection
            // 
            this.btnRunInspection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnRunInspection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRunInspection.FlatAppearance.BorderSize = 0;
            this.btnRunInspection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRunInspection.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunInspection.ForeColor = System.Drawing.Color.White;
            this.btnRunInspection.Image = ((System.Drawing.Image)(resources.GetObject("btnRunInspection.Image")));
            this.btnRunInspection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRunInspection.Location = new System.Drawing.Point(3, 471);
            this.btnRunInspection.Name = "btnRunInspection";
            this.btnRunInspection.Size = new System.Drawing.Size(206, 77);
            this.btnRunInspection.TabIndex = 5;
            this.btnRunInspection.Text = "Edge Finder";
            this.btnRunInspection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRunInspection.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRunInspection.UseVisualStyleBackColor = false;
            this.btnRunInspection.Click += new System.EventHandler(this.btnRunInspection_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 33);
            this.label5.TabIndex = 8;
            this.label5.Text = "Insp Start";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(3, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 33);
            this.label8.TabIndex = 9;
            this.label8.Text = "Alive Flicker";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(105, 132);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 33);
            this.label12.TabIndex = 10;
            this.label12.Text = "Judge";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(105, 99);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(77, 33);
            this.label13.TabIndex = 11;
            this.label13.Text = "Insp. End";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(105, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 33);
            this.label14.TabIndex = 12;
            this.label14.Text = "Insp. Start";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(105, 33);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 33);
            this.label15.TabIndex = 13;
            this.label15.Text = "Alive Flicker";
            // 
            // lstComm
            // 
            this.lstComm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstComm.FormattingEnabled = true;
            this.lstComm.ItemHeight = 12;
            this.lstComm.Location = new System.Drawing.Point(3, 3);
            this.lstComm.Name = "lstComm";
            this.lstComm.Size = new System.Drawing.Size(522, 132);
            this.lstComm.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.groupBox1.Controls.Add(this.tableLayoutPanel16);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(0, 741);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1062, 158);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logs";
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 2;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel16.Controls.Add(this.lstComm, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.lstVision, 1, 0);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 1;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(1056, 138);
            this.tableLayoutPanel16.TabIndex = 16;
            // 
            // lstVision
            // 
            this.lstVision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstVision.FormattingEnabled = true;
            this.lstVision.ItemHeight = 12;
            this.lstVision.Location = new System.Drawing.Point(531, 3);
            this.lstVision.Name = "lstVision";
            this.lstVision.Size = new System.Drawing.Size(522, 132);
            this.lstVision.TabIndex = 15;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(3, 393);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(206, 72);
            this.btnExit.TabIndex = 16;
            this.btnExit.Text = "Exit";
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // tblLayoutsub
            // 
            this.tblLayoutsub.ColumnCount = 2;
            this.tblLayoutsub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.01587F));
            this.tblLayoutsub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.98413F));
            this.tblLayoutsub.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLayoutsub.Controls.Add(this.tblLayoutCam, 0, 0);
            this.tblLayoutsub.Controls.Add(this.groupBox1, 0, 1);
            this.tblLayoutsub.Controls.Add(this.tblLayoutMenu, 1, 0);
            this.tblLayoutsub.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutsub.Location = new System.Drawing.Point(0, 125);
            this.tblLayoutsub.Margin = new System.Windows.Forms.Padding(0);
            this.tblLayoutsub.Name = "tblLayoutsub";
            this.tblLayoutsub.RowCount = 2;
            this.tblLayoutsub.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutsub.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tblLayoutsub.Size = new System.Drawing.Size(1280, 899);
            this.tblLayoutsub.TabIndex = 17;
            // 
            // tblLayoutCam
            // 
            this.tblLayoutCam.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(64)))));
            this.tblLayoutCam.ColumnCount = 4;
            this.tblLayoutCam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayoutCam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayoutCam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayoutCam.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblLayoutCam.Controls.Add(this.axCVdisplay1, 0, 1);
            this.tblLayoutCam.Controls.Add(this.axCVdisplay2, 1, 1);
            this.tblLayoutCam.Controls.Add(this.axCVdisplay3, 2, 1);
            this.tblLayoutCam.Controls.Add(this.axCVdisplay4, 3, 1);
            this.tblLayoutCam.Controls.Add(this.axCVdisplay5, 0, 3);
            this.tblLayoutCam.Controls.Add(this.axCVdisplay7, 2, 3);
            this.tblLayoutCam.Controls.Add(this.axCVdisplay6, 1, 3);
            this.tblLayoutCam.Controls.Add(this.axCVdisplay8, 3, 3);
            this.tblLayoutCam.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tblLayoutCam.Controls.Add(this.tableLayoutPanel20, 1, 0);
            this.tblLayoutCam.Controls.Add(this.tableLayoutPanel21, 2, 0);
            this.tblLayoutCam.Controls.Add(this.tableLayoutPanel22, 3, 0);
            this.tblLayoutCam.Controls.Add(this.tableLayoutPanel23, 0, 2);
            this.tblLayoutCam.Controls.Add(this.tableLayoutPanel24, 1, 2);
            this.tblLayoutCam.Controls.Add(this.tableLayoutPanel25, 2, 2);
            this.tblLayoutCam.Controls.Add(this.tableLayoutPanel26, 3, 2);
            this.tblLayoutCam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutCam.Location = new System.Drawing.Point(0, 0);
            this.tblLayoutCam.Margin = new System.Windows.Forms.Padding(0);
            this.tblLayoutCam.Name = "tblLayoutCam";
            this.tblLayoutCam.RowCount = 4;
            this.tblLayoutCam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.2F));
            this.tblLayoutCam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.8F));
            this.tblLayoutCam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.2F));
            this.tblLayoutCam.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.8F));
            this.tblLayoutCam.Size = new System.Drawing.Size(1062, 741);
            this.tblLayoutCam.TabIndex = 0;
            // 
            // axCVdisplay3
            // 
            this.axCVdisplay3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axCVdisplay3.Location = new System.Drawing.Point(533, 34);
            this.axCVdisplay3.Name = "axCVdisplay3";
            this.axCVdisplay3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay3.OcxState")));
            this.axCVdisplay3.Size = new System.Drawing.Size(259, 333);
            this.axCVdisplay3.TabIndex = 4;
            // 
            // axCVdisplay4
            // 
            this.axCVdisplay4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axCVdisplay4.Location = new System.Drawing.Point(798, 34);
            this.axCVdisplay4.Name = "axCVdisplay4";
            this.axCVdisplay4.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay4.OcxState")));
            this.axCVdisplay4.Size = new System.Drawing.Size(261, 333);
            this.axCVdisplay4.TabIndex = 5;
            // 
            // axCVdisplay5
            // 
            this.axCVdisplay5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axCVdisplay5.Location = new System.Drawing.Point(3, 404);
            this.axCVdisplay5.Name = "axCVdisplay5";
            this.axCVdisplay5.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay5.OcxState")));
            this.axCVdisplay5.Size = new System.Drawing.Size(259, 334);
            this.axCVdisplay5.TabIndex = 5;
            // 
            // axCVdisplay7
            // 
            this.axCVdisplay7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axCVdisplay7.Location = new System.Drawing.Point(533, 404);
            this.axCVdisplay7.Name = "axCVdisplay7";
            this.axCVdisplay7.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay7.OcxState")));
            this.axCVdisplay7.Size = new System.Drawing.Size(259, 334);
            this.axCVdisplay7.TabIndex = 5;
            // 
            // axCVdisplay6
            // 
            this.axCVdisplay6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axCVdisplay6.Location = new System.Drawing.Point(268, 404);
            this.axCVdisplay6.Name = "axCVdisplay6";
            this.axCVdisplay6.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay6.OcxState")));
            this.axCVdisplay6.Size = new System.Drawing.Size(259, 334);
            this.axCVdisplay6.TabIndex = 5;
            // 
            // axCVdisplay8
            // 
            this.axCVdisplay8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axCVdisplay8.Location = new System.Drawing.Point(798, 404);
            this.axCVdisplay8.Name = "axCVdisplay8";
            this.axCVdisplay8.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay8.OcxState")));
            this.axCVdisplay8.Size = new System.Drawing.Size(261, 334);
            this.axCVdisplay8.TabIndex = 5;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.66824F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.33175F));
            this.tableLayoutPanel4.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblCamRet01, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(259, 25);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // lblCamRet01
            // 
            this.lblCamRet01.AutoSize = true;
            this.lblCamRet01.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(182)))), ((int)(((byte)(51)))));
            this.lblCamRet01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCamRet01.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCamRet01.Location = new System.Drawing.Point(183, 0);
            this.lblCamRet01.Name = "lblCamRet01";
            this.lblCamRet01.Size = new System.Drawing.Size(73, 25);
            this.lblCamRet01.TabIndex = 4;
            this.lblCamRet01.Text = "OK";
            this.lblCamRet01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel20
            // 
            this.tableLayoutPanel20.ColumnCount = 2;
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.66824F));
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.33175F));
            this.tableLayoutPanel20.Controls.Add(this.label23, 0, 0);
            this.tableLayoutPanel20.Controls.Add(this.lblCamRet02, 1, 0);
            this.tableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel20.Location = new System.Drawing.Point(268, 3);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            this.tableLayoutPanel20.RowCount = 1;
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel20.Size = new System.Drawing.Size(259, 25);
            this.tableLayoutPanel20.TabIndex = 6;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(3, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(174, 25);
            this.label23.TabIndex = 3;
            this.label23.Text = "Cam02";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCamRet02
            // 
            this.lblCamRet02.AutoSize = true;
            this.lblCamRet02.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(182)))), ((int)(((byte)(51)))));
            this.lblCamRet02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCamRet02.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCamRet02.Location = new System.Drawing.Point(183, 0);
            this.lblCamRet02.Name = "lblCamRet02";
            this.lblCamRet02.Size = new System.Drawing.Size(73, 25);
            this.lblCamRet02.TabIndex = 4;
            this.lblCamRet02.Text = "OK";
            this.lblCamRet02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel21
            // 
            this.tableLayoutPanel21.ColumnCount = 2;
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.66824F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.33175F));
            this.tableLayoutPanel21.Controls.Add(this.label25, 0, 0);
            this.tableLayoutPanel21.Controls.Add(this.lblCamRet03, 1, 0);
            this.tableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel21.Location = new System.Drawing.Point(533, 3);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 1;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(259, 25);
            this.tableLayoutPanel21.TabIndex = 6;
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(3, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(174, 25);
            this.label25.TabIndex = 3;
            this.label25.Text = "Cam03";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCamRet03
            // 
            this.lblCamRet03.AutoSize = true;
            this.lblCamRet03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(182)))), ((int)(((byte)(51)))));
            this.lblCamRet03.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCamRet03.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCamRet03.Location = new System.Drawing.Point(183, 0);
            this.lblCamRet03.Name = "lblCamRet03";
            this.lblCamRet03.Size = new System.Drawing.Size(73, 25);
            this.lblCamRet03.TabIndex = 4;
            this.lblCamRet03.Text = "OK";
            this.lblCamRet03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel22
            // 
            this.tableLayoutPanel22.ColumnCount = 2;
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.66824F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.33175F));
            this.tableLayoutPanel22.Controls.Add(this.label27, 0, 0);
            this.tableLayoutPanel22.Controls.Add(this.lblCamRet04, 1, 0);
            this.tableLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel22.Location = new System.Drawing.Point(798, 3);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            this.tableLayoutPanel22.RowCount = 1;
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel22.Size = new System.Drawing.Size(261, 25);
            this.tableLayoutPanel22.TabIndex = 6;
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label27.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label27.ForeColor = System.Drawing.Color.White;
            this.label27.Location = new System.Drawing.Point(3, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(175, 25);
            this.label27.TabIndex = 3;
            this.label27.Text = "Cam04";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCamRet04
            // 
            this.lblCamRet04.AutoSize = true;
            this.lblCamRet04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(182)))), ((int)(((byte)(51)))));
            this.lblCamRet04.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCamRet04.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCamRet04.Location = new System.Drawing.Point(184, 0);
            this.lblCamRet04.Name = "lblCamRet04";
            this.lblCamRet04.Size = new System.Drawing.Size(74, 25);
            this.lblCamRet04.TabIndex = 4;
            this.lblCamRet04.Text = "OK";
            this.lblCamRet04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel23
            // 
            this.tableLayoutPanel23.ColumnCount = 2;
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.66824F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.33175F));
            this.tableLayoutPanel23.Controls.Add(this.label18, 0, 0);
            this.tableLayoutPanel23.Controls.Add(this.lblCamRet05, 1, 0);
            this.tableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel23.Location = new System.Drawing.Point(3, 373);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            this.tableLayoutPanel23.RowCount = 1;
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel23.Size = new System.Drawing.Size(259, 25);
            this.tableLayoutPanel23.TabIndex = 6;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(3, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(174, 25);
            this.label18.TabIndex = 3;
            this.label18.Text = "Cam05";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCamRet05
            // 
            this.lblCamRet05.AutoSize = true;
            this.lblCamRet05.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(182)))), ((int)(((byte)(51)))));
            this.lblCamRet05.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCamRet05.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCamRet05.Location = new System.Drawing.Point(183, 0);
            this.lblCamRet05.Name = "lblCamRet05";
            this.lblCamRet05.Size = new System.Drawing.Size(73, 25);
            this.lblCamRet05.TabIndex = 4;
            this.lblCamRet05.Text = "OK";
            this.lblCamRet05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel24
            // 
            this.tableLayoutPanel24.ColumnCount = 2;
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.66824F));
            this.tableLayoutPanel24.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.33175F));
            this.tableLayoutPanel24.Controls.Add(this.label20, 0, 0);
            this.tableLayoutPanel24.Controls.Add(this.lblCamRet06, 1, 0);
            this.tableLayoutPanel24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel24.Location = new System.Drawing.Point(268, 373);
            this.tableLayoutPanel24.Name = "tableLayoutPanel24";
            this.tableLayoutPanel24.RowCount = 1;
            this.tableLayoutPanel24.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel24.Size = new System.Drawing.Size(259, 25);
            this.tableLayoutPanel24.TabIndex = 6;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(3, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(174, 25);
            this.label20.TabIndex = 3;
            this.label20.Text = "Cam06";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCamRet06
            // 
            this.lblCamRet06.AutoSize = true;
            this.lblCamRet06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(182)))), ((int)(((byte)(51)))));
            this.lblCamRet06.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCamRet06.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCamRet06.Location = new System.Drawing.Point(183, 0);
            this.lblCamRet06.Name = "lblCamRet06";
            this.lblCamRet06.Size = new System.Drawing.Size(73, 25);
            this.lblCamRet06.TabIndex = 4;
            this.lblCamRet06.Text = "OK";
            this.lblCamRet06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel25
            // 
            this.tableLayoutPanel25.ColumnCount = 2;
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.66824F));
            this.tableLayoutPanel25.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.33175F));
            this.tableLayoutPanel25.Controls.Add(this.label29, 0, 0);
            this.tableLayoutPanel25.Controls.Add(this.lblCamRet07, 1, 0);
            this.tableLayoutPanel25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel25.Location = new System.Drawing.Point(533, 373);
            this.tableLayoutPanel25.Name = "tableLayoutPanel25";
            this.tableLayoutPanel25.RowCount = 1;
            this.tableLayoutPanel25.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel25.Size = new System.Drawing.Size(259, 25);
            this.tableLayoutPanel25.TabIndex = 6;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(3, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(174, 25);
            this.label29.TabIndex = 3;
            this.label29.Text = "Cam07";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCamRet07
            // 
            this.lblCamRet07.AutoSize = true;
            this.lblCamRet07.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(182)))), ((int)(((byte)(51)))));
            this.lblCamRet07.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCamRet07.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCamRet07.Location = new System.Drawing.Point(183, 0);
            this.lblCamRet07.Name = "lblCamRet07";
            this.lblCamRet07.Size = new System.Drawing.Size(73, 25);
            this.lblCamRet07.TabIndex = 4;
            this.lblCamRet07.Text = "OK";
            this.lblCamRet07.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel26
            // 
            this.tableLayoutPanel26.ColumnCount = 2;
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.66824F));
            this.tableLayoutPanel26.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.33175F));
            this.tableLayoutPanel26.Controls.Add(this.label31, 0, 0);
            this.tableLayoutPanel26.Controls.Add(this.lblCamRet08, 1, 0);
            this.tableLayoutPanel26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel26.Location = new System.Drawing.Point(798, 373);
            this.tableLayoutPanel26.Name = "tableLayoutPanel26";
            this.tableLayoutPanel26.RowCount = 1;
            this.tableLayoutPanel26.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel26.Size = new System.Drawing.Size(261, 25);
            this.tableLayoutPanel26.TabIndex = 6;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(3, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(175, 25);
            this.label31.TabIndex = 3;
            this.label31.Text = "Cam08";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCamRet08
            // 
            this.lblCamRet08.AutoSize = true;
            this.lblCamRet08.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(182)))), ((int)(((byte)(51)))));
            this.lblCamRet08.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCamRet08.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblCamRet08.Location = new System.Drawing.Point(184, 0);
            this.lblCamRet08.Name = "lblCamRet08";
            this.lblCamRet08.Size = new System.Drawing.Size(74, 25);
            this.lblCamRet08.TabIndex = 4;
            this.lblCamRet08.Text = "OK";
            this.lblCamRet08.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblLayoutMenu
            // 
            this.tblLayoutMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.tblLayoutMenu.ColumnCount = 1;
            this.tblLayoutMenu.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblLayoutMenu.Controls.Add(this.radManualMod, 0, 1);
            this.tblLayoutMenu.Controls.Add(this.radAutoMod, 0, 0);
            this.tblLayoutMenu.Controls.Add(this.tableLayoutPanel2, 0, 7);
            this.tblLayoutMenu.Controls.Add(this.tableLayoutPanel19, 0, 6);
            this.tblLayoutMenu.Controls.Add(this.btnRunInspection, 0, 5);
            this.tblLayoutMenu.Controls.Add(this.btnExit, 0, 4);
            this.tblLayoutMenu.Controls.Add(this.btnAutoMode, 0, 2);
            this.tblLayoutMenu.Controls.Add(this.btnModelSettings, 0, 3);
            this.tblLayoutMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutMenu.Location = new System.Drawing.Point(1065, 3);
            this.tblLayoutMenu.Name = "tblLayoutMenu";
            this.tblLayoutMenu.RowCount = 8;
            this.tblLayoutsub.SetRowSpan(this.tblLayoutMenu, 2);
            this.tblLayoutMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.979073F));
            this.tblLayoutMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.979073F));
            this.tblLayoutMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.77728F));
            this.tblLayoutMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblLayoutMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.748115F));
            this.tblLayoutMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.351433F));
            this.tblLayoutMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.23379F));
            this.tblLayoutMenu.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.62443F));
            this.tblLayoutMenu.Size = new System.Drawing.Size(212, 893);
            this.tblLayoutMenu.TabIndex = 16;
            // 
            // radManualMod
            // 
            this.radManualMod.AutoSize = true;
            this.radManualMod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radManualMod.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radManualMod.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.radManualMod.Location = new System.Drawing.Point(3, 56);
            this.radManualMod.Name = "radManualMod";
            this.radManualMod.Size = new System.Drawing.Size(206, 47);
            this.radManualMod.TabIndex = 20;
            this.radManualMod.TabStop = true;
            this.radManualMod.Text = "Manual Mode";
            this.radManualMod.UseVisualStyleBackColor = true;
            this.radManualMod.CheckedChanged += new System.EventHandler(this.radManualMod_CheckedChanged);
            // 
            // radAutoMod
            // 
            this.radAutoMod.AutoSize = true;
            this.radAutoMod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radAutoMod.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.radAutoMod.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.radAutoMod.Location = new System.Drawing.Point(3, 3);
            this.radAutoMod.Name = "radAutoMod";
            this.radAutoMod.Size = new System.Drawing.Size(206, 47);
            this.radAutoMod.TabIndex = 20;
            this.radAutoMod.TabStop = true;
            this.radAutoMod.Text = "Autol Mode";
            this.radAutoMod.UseVisualStyleBackColor = true;
            this.radAutoMod.CheckedChanged += new System.EventHandler(this.radAutoMod_CheckedChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.02439F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.2439F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.756098F));
            this.tableLayoutPanel2.Controls.Add(this.lblPLC_InspStart, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPLC_HB, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label12, 2, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblPC_HB, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label15, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label13, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.label14, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSystemTime, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPC_InspStart, 3, 2);
            this.tableLayoutPanel2.Controls.Add(this.lblPC_TotalJudge, 3, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblPC_InspEnd, 3, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 690);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(206, 200);
            this.tableLayoutPanel2.TabIndex = 20;
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 2;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.25773F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.74227F));
            this.tableLayoutPanel19.Controls.Add(this.chkPositive01, 0, 0);
            this.tableLayoutPanel19.Controls.Add(this.tableLayoutPanel5, 1, 0);
            this.tableLayoutPanel19.Controls.Add(this.chkPositive02, 0, 1);
            this.tableLayoutPanel19.Controls.Add(this.tableLayoutPanel6, 1, 1);
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(3, 554);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 2;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(206, 130);
            this.tableLayoutPanel19.TabIndex = 18;
            // 
            // chkPositive01
            // 
            this.chkPositive01.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPositive01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPositive01.ForeColor = System.Drawing.Color.Black;
            this.chkPositive01.Location = new System.Drawing.Point(3, 3);
            this.chkPositive01.Name = "chkPositive01";
            this.chkPositive01.Size = new System.Drawing.Size(46, 59);
            this.chkPositive01.TabIndex = 4;
            this.chkPositive01.Text = "Positive";
            this.chkPositive01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPositive01.UseVisualStyleBackColor = true;
            this.chkPositive01.CheckedChanged += new System.EventHandler(this.chkPositive01_CheckedChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.60736F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.56442F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.21473F));
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.EdgeThresholdMin01, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.EdgeThresholdMax01, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.tbMin01, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.tbMax01, 2, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(55, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(148, 59);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "MIN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "MAX";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EdgeThresholdMin01
            // 
            this.EdgeThresholdMin01.BackColor = System.Drawing.Color.White;
            this.EdgeThresholdMin01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeThresholdMin01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EdgeThresholdMin01.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EdgeThresholdMin01.ForeColor = System.Drawing.Color.Black;
            this.EdgeThresholdMin01.Location = new System.Drawing.Point(44, 0);
            this.EdgeThresholdMin01.Name = "EdgeThresholdMin01";
            this.EdgeThresholdMin01.Size = new System.Drawing.Size(18, 29);
            this.EdgeThresholdMin01.TabIndex = 3;
            this.EdgeThresholdMin01.Text = "0";
            this.EdgeThresholdMin01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EdgeThresholdMax01
            // 
            this.EdgeThresholdMax01.BackColor = System.Drawing.Color.White;
            this.EdgeThresholdMax01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeThresholdMax01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EdgeThresholdMax01.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EdgeThresholdMax01.ForeColor = System.Drawing.Color.Black;
            this.EdgeThresholdMax01.Location = new System.Drawing.Point(44, 29);
            this.EdgeThresholdMax01.Name = "EdgeThresholdMax01";
            this.EdgeThresholdMax01.Size = new System.Drawing.Size(18, 30);
            this.EdgeThresholdMax01.TabIndex = 3;
            this.EdgeThresholdMax01.Text = "0";
            this.EdgeThresholdMax01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbMin01
            // 
            this.tbMin01.AutoSize = false;
            this.tbMin01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMin01.Location = new System.Drawing.Point(68, 3);
            this.tbMin01.Maximum = 255;
            this.tbMin01.Name = "tbMin01";
            this.tbMin01.Size = new System.Drawing.Size(77, 23);
            this.tbMin01.TabIndex = 7;
            this.tbMin01.Scroll += new System.EventHandler(this.tbMin01_Scroll);
            // 
            // tbMax01
            // 
            this.tbMax01.AutoSize = false;
            this.tbMax01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMax01.Location = new System.Drawing.Point(68, 32);
            this.tbMax01.Maximum = 255;
            this.tbMax01.Name = "tbMax01";
            this.tbMax01.Size = new System.Drawing.Size(77, 24);
            this.tbMax01.TabIndex = 7;
            this.tbMax01.Scroll += new System.EventHandler(this.tbMax01_Scroll);
            // 
            // chkPositive02
            // 
            this.chkPositive02.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPositive02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPositive02.ForeColor = System.Drawing.Color.Black;
            this.chkPositive02.Location = new System.Drawing.Point(3, 68);
            this.chkPositive02.Name = "chkPositive02";
            this.chkPositive02.Size = new System.Drawing.Size(46, 59);
            this.chkPositive02.TabIndex = 4;
            this.chkPositive02.Text = "Positive";
            this.chkPositive02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPositive02.UseVisualStyleBackColor = true;
            this.chkPositive02.CheckedChanged += new System.EventHandler(this.chkPositive02_CheckedChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 3;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.65854F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.46342F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.87805F));
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.EdgeThresholdMin02, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.EdgeThresholdMax02, 1, 1);
            this.tableLayoutPanel6.Controls.Add(this.tbMin02, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.tbMax02, 2, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(55, 68);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(148, 59);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 29);
            this.label4.TabIndex = 3;
            this.label4.Text = "MIN";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 30);
            this.label7.TabIndex = 3;
            this.label7.Text = "MAX";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EdgeThresholdMin02
            // 
            this.EdgeThresholdMin02.BackColor = System.Drawing.Color.White;
            this.EdgeThresholdMin02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeThresholdMin02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EdgeThresholdMin02.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EdgeThresholdMin02.ForeColor = System.Drawing.Color.Black;
            this.EdgeThresholdMin02.Location = new System.Drawing.Point(45, 0);
            this.EdgeThresholdMin02.Name = "EdgeThresholdMin02";
            this.EdgeThresholdMin02.Size = new System.Drawing.Size(18, 29);
            this.EdgeThresholdMin02.TabIndex = 3;
            this.EdgeThresholdMin02.Text = "0";
            this.EdgeThresholdMin02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EdgeThresholdMax02
            // 
            this.EdgeThresholdMax02.BackColor = System.Drawing.Color.White;
            this.EdgeThresholdMax02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeThresholdMax02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EdgeThresholdMax02.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EdgeThresholdMax02.ForeColor = System.Drawing.Color.Black;
            this.EdgeThresholdMax02.Location = new System.Drawing.Point(45, 29);
            this.EdgeThresholdMax02.Name = "EdgeThresholdMax02";
            this.EdgeThresholdMax02.Size = new System.Drawing.Size(18, 30);
            this.EdgeThresholdMax02.TabIndex = 3;
            this.EdgeThresholdMax02.Text = "0";
            this.EdgeThresholdMax02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbMin02
            // 
            this.tbMin02.AutoSize = false;
            this.tbMin02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMin02.Location = new System.Drawing.Point(69, 3);
            this.tbMin02.Maximum = 255;
            this.tbMin02.Name = "tbMin02";
            this.tbMin02.Size = new System.Drawing.Size(76, 23);
            this.tbMin02.TabIndex = 7;
            this.tbMin02.Scroll += new System.EventHandler(this.tbMin02_Scroll);
            // 
            // tbMax02
            // 
            this.tbMax02.AutoSize = false;
            this.tbMax02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMax02.Location = new System.Drawing.Point(69, 32);
            this.tbMax02.Maximum = 255;
            this.tbMax02.Name = "tbMax02";
            this.tbMax02.Size = new System.Drawing.Size(76, 24);
            this.tbMax02.TabIndex = 7;
            this.tbMax02.Scroll += new System.EventHandler(this.tbMax02_Scroll);
            // 
            // btnAutoMode
            // 
            this.btnAutoMode.FlatAppearance.BorderSize = 0;
            this.btnAutoMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoMode.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoMode.ForeColor = System.Drawing.Color.White;
            this.btnAutoMode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAutoMode.Location = new System.Drawing.Point(3, 109);
            this.btnAutoMode.Name = "btnAutoMode";
            this.btnAutoMode.Size = new System.Drawing.Size(206, 67);
            this.btnAutoMode.TabIndex = 0;
            this.btnAutoMode.Text = "Auto Mode";
            this.btnAutoMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAutoMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAutoMode.UseVisualStyleBackColor = true;
            this.btnAutoMode.Click += new System.EventHandler(this.btnAutoMode_Click);
            // 
            // btnModelSettings
            // 
            this.btnModelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnModelSettings.FlatAppearance.BorderSize = 0;
            this.btnModelSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModelSettings.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModelSettings.ForeColor = System.Drawing.Color.White;
            this.btnModelSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnModelSettings.Image")));
            this.btnModelSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModelSettings.Location = new System.Drawing.Point(3, 295);
            this.btnModelSettings.Name = "btnModelSettings";
            this.btnModelSettings.Size = new System.Drawing.Size(206, 92);
            this.btnModelSettings.TabIndex = 0;
            this.btnModelSettings.Text = "Model Settings";
            this.btnModelSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnModelSettings.UseVisualStyleBackColor = true;
            this.btnModelSettings.Click += new System.EventHandler(this.btnModelSettings_Click);
            this.btnModelSettings.MouseEnter += new System.EventHandler(this.btnModelSettings_MouseEnter);
            this.btnModelSettings.MouseLeave += new System.EventHandler(this.btnModelSettings_MouseLeave);
            // 
            // axCVgrabber3
            // 
            this.axCVgrabber3.Enabled = true;
            this.axCVgrabber3.Location = new System.Drawing.Point(506, 2);
            this.axCVgrabber3.Name = "axCVgrabber3";
            this.axCVgrabber3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber3.OcxState")));
            this.axCVgrabber3.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber3.TabIndex = 1;
            this.axCVgrabber3.ImageUpdated += new System.EventHandler(this.axCVgrabber3_ImageUpdated);
            // 
            // axCVimage3
            // 
            this.axCVimage3.Enabled = true;
            this.axCVimage3.Location = new System.Drawing.Point(506, 40);
            this.axCVimage3.Name = "axCVimage3";
            this.axCVimage3.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage3.OcxState")));
            this.axCVimage3.Size = new System.Drawing.Size(32, 32);
            this.axCVimage3.TabIndex = 2;
            this.axCVimage3.ImageUpdated += new System.EventHandler(this.axCVimage3_ImageUpdated);
            // 
            // axCVgrabber4
            // 
            this.axCVgrabber4.Enabled = true;
            this.axCVgrabber4.Location = new System.Drawing.Point(544, 2);
            this.axCVgrabber4.Name = "axCVgrabber4";
            this.axCVgrabber4.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber4.OcxState")));
            this.axCVgrabber4.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber4.TabIndex = 1;
            this.axCVgrabber4.ImageUpdated += new System.EventHandler(this.axCVgrabber4_ImageUpdated);
            // 
            // axCVimage4
            // 
            this.axCVimage4.Enabled = true;
            this.axCVimage4.Location = new System.Drawing.Point(544, 40);
            this.axCVimage4.Name = "axCVimage4";
            this.axCVimage4.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage4.OcxState")));
            this.axCVimage4.Size = new System.Drawing.Size(32, 32);
            this.axCVimage4.TabIndex = 2;
            this.axCVimage4.ImageUpdated += new System.EventHandler(this.axCVimage4_ImageUpdated);
            // 
            // axCVgrabber5
            // 
            this.axCVgrabber5.Enabled = true;
            this.axCVgrabber5.Location = new System.Drawing.Point(582, 2);
            this.axCVgrabber5.Name = "axCVgrabber5";
            this.axCVgrabber5.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber5.OcxState")));
            this.axCVgrabber5.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber5.TabIndex = 1;
            this.axCVgrabber5.ImageUpdated += new System.EventHandler(this.axCVgrabber5_ImageUpdated);
            // 
            // axCVimage5
            // 
            this.axCVimage5.Enabled = true;
            this.axCVimage5.Location = new System.Drawing.Point(582, 40);
            this.axCVimage5.Name = "axCVimage5";
            this.axCVimage5.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage5.OcxState")));
            this.axCVimage5.Size = new System.Drawing.Size(32, 32);
            this.axCVimage5.TabIndex = 2;
            this.axCVimage5.ImageUpdated += new System.EventHandler(this.axCVimage5_ImageUpdated);
            // 
            // axCVgrabber6
            // 
            this.axCVgrabber6.Enabled = true;
            this.axCVgrabber6.Location = new System.Drawing.Point(620, 2);
            this.axCVgrabber6.Name = "axCVgrabber6";
            this.axCVgrabber6.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber6.OcxState")));
            this.axCVgrabber6.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber6.TabIndex = 1;
            this.axCVgrabber6.ImageUpdated += new System.EventHandler(this.axCVgrabber6_ImageUpdated);
            // 
            // axCVgrabber7
            // 
            this.axCVgrabber7.Enabled = true;
            this.axCVgrabber7.Location = new System.Drawing.Point(658, 2);
            this.axCVgrabber7.Name = "axCVgrabber7";
            this.axCVgrabber7.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber7.OcxState")));
            this.axCVgrabber7.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber7.TabIndex = 1;
            this.axCVgrabber7.ImageUpdated += new System.EventHandler(this.axCVgrabber7_ImageUpdated);
            // 
            // axCVgrabber8
            // 
            this.axCVgrabber8.Enabled = true;
            this.axCVgrabber8.Location = new System.Drawing.Point(696, 2);
            this.axCVgrabber8.Name = "axCVgrabber8";
            this.axCVgrabber8.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber8.OcxState")));
            this.axCVgrabber8.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber8.TabIndex = 1;
            this.axCVgrabber8.ImageUpdated += new System.EventHandler(this.axCVgrabber8_ImageUpdated);
            // 
            // axCVimage6
            // 
            this.axCVimage6.Enabled = true;
            this.axCVimage6.Location = new System.Drawing.Point(620, 40);
            this.axCVimage6.Name = "axCVimage6";
            this.axCVimage6.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage6.OcxState")));
            this.axCVimage6.Size = new System.Drawing.Size(32, 32);
            this.axCVimage6.TabIndex = 2;
            this.axCVimage6.ImageUpdated += new System.EventHandler(this.axCVimage6_ImageUpdated);
            // 
            // axCVimage7
            // 
            this.axCVimage7.Enabled = true;
            this.axCVimage7.Location = new System.Drawing.Point(658, 40);
            this.axCVimage7.Name = "axCVimage7";
            this.axCVimage7.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage7.OcxState")));
            this.axCVimage7.Size = new System.Drawing.Size(32, 32);
            this.axCVimage7.TabIndex = 2;
            this.axCVimage7.ImageUpdated += new System.EventHandler(this.axCVimage7_ImageUpdated);
            // 
            // axCVimage8
            // 
            this.axCVimage8.Enabled = true;
            this.axCVimage8.Location = new System.Drawing.Point(696, 40);
            this.axCVimage8.Name = "axCVimage8";
            this.axCVimage8.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage8.OcxState")));
            this.axCVimage8.Size = new System.Drawing.Size(32, 32);
            this.axCVimage8.TabIndex = 2;
            this.axCVimage8.ImageUpdated += new System.EventHandler(this.axCVimage8_ImageUpdated);
            // 
            // tblLayoutMain
            // 
            this.tblLayoutMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.tblLayoutMain.ColumnCount = 1;
            this.tblLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblLayoutMain.Controls.Add(this.tableLayoutPanel18, 0, 0);
            this.tblLayoutMain.Controls.Add(this.tblLayoutsub, 0, 1);
            this.tblLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tblLayoutMain.Name = "tblLayoutMain";
            this.tblLayoutMain.RowCount = 2;
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.23958F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.76041F));
            this.tblLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblLayoutMain.Size = new System.Drawing.Size(1280, 1024);
            this.tblLayoutMain.TabIndex = 18;
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 3;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.46625F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.3642F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.24804F));
            this.tableLayoutPanel18.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.pictureBox2, 2, 0);
            this.tableLayoutPanel18.Controls.Add(this.label22, 1, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(1274, 119);
            this.tableLayoutPanel18.TabIndex = 18;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(241, 113);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1069, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(202, 113);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Font = new System.Drawing.Font("Century Gothic", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(250, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(813, 119);
            this.label22.TabIndex = 2;
            this.label22.Text = "LCD Inspection";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMenuMarker
            // 
            this.pnlMenuMarker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(58)))), ((int)(((byte)(0)))));
            this.pnlMenuMarker.Location = new System.Drawing.Point(1061, 103);
            this.pnlMenuMarker.Name = "pnlMenuMarker";
            this.pnlMenuMarker.Size = new System.Drawing.Size(5, 68);
            this.pnlMenuMarker.TabIndex = 19;
            this.pnlMenuMarker.Visible = false;
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 1;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel13, 0, 1);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(744, 4);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 2;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.97403F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74.02597F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(213, 89);
            this.tableLayoutPanel12.TabIndex = 2;
            this.tableLayoutPanel12.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel12.SetColumnSpan(this.label1, 4);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "PLC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.63768F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.36232F));
            this.tableLayoutPanel13.Controls.Add(this.chkPLC_HB, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.chkPLC_InspStart, 1, 0);
            this.tableLayoutPanel13.Controls.Add(this.btnPLC_ModelWrite, 1, 1);
            this.tableLayoutPanel13.Controls.Add(this.txtPLC_Model, 0, 1);
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 26);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 2;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.33333F));
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.66667F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(207, 60);
            this.tableLayoutPanel13.TabIndex = 4;
            // 
            // chkPLC_HB
            // 
            this.chkPLC_HB.AutoSize = true;
            this.chkPLC_HB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPLC_HB.ForeColor = System.Drawing.Color.White;
            this.chkPLC_HB.Location = new System.Drawing.Point(3, 3);
            this.chkPLC_HB.Name = "chkPLC_HB";
            this.chkPLC_HB.Size = new System.Drawing.Size(44, 16);
            this.chkPLC_HB.TabIndex = 4;
            this.chkPLC_HB.Text = "Flicker Active";
            this.chkPLC_HB.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.chkPLC_HB.UseVisualStyleBackColor = true;
            // 
            // chkPLC_InspStart
            // 
            this.chkPLC_InspStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPLC_InspStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPLC_InspStart.ForeColor = System.Drawing.Color.Black;
            this.chkPLC_InspStart.Location = new System.Drawing.Point(53, 3);
            this.chkPLC_InspStart.Name = "chkPLC_InspStart";
            this.chkPLC_InspStart.Size = new System.Drawing.Size(151, 16);
            this.chkPLC_InspStart.TabIndex = 4;
            this.chkPLC_InspStart.Text = "Insp Start";
            this.chkPLC_InspStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPLC_InspStart.UseVisualStyleBackColor = true;
            // 
            // btnPLC_ModelWrite
            // 
            this.btnPLC_ModelWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPLC_ModelWrite.Location = new System.Drawing.Point(53, 25);
            this.btnPLC_ModelWrite.Name = "btnPLC_ModelWrite";
            this.btnPLC_ModelWrite.Size = new System.Drawing.Size(151, 32);
            this.btnPLC_ModelWrite.TabIndex = 5;
            this.btnPLC_ModelWrite.Text = "Model Write";
            this.btnPLC_ModelWrite.UseVisualStyleBackColor = true;
            // 
            // txtPLC_Model
            // 
            this.txtPLC_Model.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPLC_Model.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPLC_Model.Location = new System.Drawing.Point(3, 25);
            this.txtPLC_Model.Name = "txtPLC_Model";
            this.txtPLC_Model.Size = new System.Drawing.Size(44, 21);
            this.txtPLC_Model.TabIndex = 6;
            this.txtPLC_Model.Text = "Model";
            this.txtPLC_Model.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.pnlMenuMarker);
            this.Controls.Add(this.tableLayoutPanel12);
            this.Controls.Add(this.tblLayoutMain);
            this.Controls.Add(this.axCVgrabber1);
            this.Controls.Add(this.axCVimage8);
            this.Controls.Add(this.axCVgrabber5);
            this.Controls.Add(this.axCVgrabber8);
            this.Controls.Add(this.axCVimage4);
            this.Controls.Add(this.axCVgrabber2);
            this.Controls.Add(this.axCVimage1);
            this.Controls.Add(this.axCVgrabber7);
            this.Controls.Add(this.axCVimage7);
            this.Controls.Add(this.axCVgrabber4);
            this.Controls.Add(this.axCVimage5);
            this.Controls.Add(this.axCVimage2);
            this.Controls.Add(this.axCVimage3);
            this.Controls.Add(this.axCVgrabber6);
            this.Controls.Add(this.axCVgrabber3);
            this.Controls.Add(this.axCVimage6);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1920, 1080);
            this.MinimumSize = new System.Drawing.Size(1280, 768);
            this.Name = "frmSample";
            this.Text = "Edge Detection Sample";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSample_FormClosing);
            this.Load += new System.EventHandler(this.frmSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tblLayoutsub.ResumeLayout(false);
            this.tblLayoutCam.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay8)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel20.ResumeLayout(false);
            this.tableLayoutPanel20.PerformLayout();
            this.tableLayoutPanel21.ResumeLayout(false);
            this.tableLayoutPanel21.PerformLayout();
            this.tableLayoutPanel22.ResumeLayout(false);
            this.tableLayoutPanel22.PerformLayout();
            this.tableLayoutPanel23.ResumeLayout(false);
            this.tableLayoutPanel23.PerformLayout();
            this.tableLayoutPanel24.ResumeLayout(false);
            this.tableLayoutPanel24.PerformLayout();
            this.tableLayoutPanel25.ResumeLayout(false);
            this.tableLayoutPanel25.PerformLayout();
            this.tableLayoutPanel26.ResumeLayout(false);
            this.tableLayoutPanel26.PerformLayout();
            this.tblLayoutMenu.ResumeLayout(false);
            this.tblLayoutMenu.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel19.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbMin01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMax01)).EndInit();
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbMin02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMax02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage8)).EndInit();
            this.tblLayoutMain.ResumeLayout(false);
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxCVDISPLAYLib.AxCVdisplay axCVdisplay1;
        private System.ComponentModel.BackgroundWorker bgWorkPLC;
        private System.Windows.Forms.Timer tmrSystem;
        private AxCVGRABBERLib.AxCVgrabber axCVgrabber1;
        private AxCVIMAGELib.AxCVimage axCVimage1;
        private AxCVIMAGELib.AxCVimage axCVimage2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPLC_HB;
        private System.Windows.Forms.Label lblPLC_InspStart;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPC_HB;
        private System.Windows.Forms.Label lblPC_InspStart;
        private System.Windows.Forms.Label lblPC_InspEnd;
        private System.Windows.Forms.Label lblPC_TotalJudge;
        private System.Windows.Forms.Label lblSystemTime;
        private System.Windows.Forms.Timer tmrDebugPLC;
        private AxCVDISPLAYLib.AxCVdisplay axCVdisplay2;
        private AxCVGRABBERLib.AxCVgrabber axCVgrabber2;
        private AxCVIMAGELib.AxCVimage axImageListaxCVimage2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnRunInspection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ListBox lstComm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstVision;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TableLayoutPanel tblLayoutsub;
        private System.Windows.Forms.TableLayoutPanel tblLayoutCam;
        private AxCVDISPLAYLib.AxCVdisplay axCVdisplay3;
        private AxCVDISPLAYLib.AxCVdisplay axCVdisplay4;
        private AxCVDISPLAYLib.AxCVdisplay axCVdisplay5;
        private AxCVDISPLAYLib.AxCVdisplay axCVdisplay7;
        private AxCVDISPLAYLib.AxCVdisplay axCVdisplay6;
        private AxCVDISPLAYLib.AxCVdisplay axCVdisplay8;
        private AxCVGRABBERLib.AxCVgrabber axCVgrabber3;
        private AxCVIMAGELib.AxCVimage axCVimage3;
        private AxCVGRABBERLib.AxCVgrabber axCVgrabber4;
        private AxCVIMAGELib.AxCVimage axCVimage4;
        private AxCVGRABBERLib.AxCVgrabber axCVgrabber5;
        private AxCVIMAGELib.AxCVimage axCVimage5;
        private AxCVGRABBERLib.AxCVgrabber axCVgrabber6;
        private AxCVGRABBERLib.AxCVgrabber axCVgrabber7;
        private AxCVGRABBERLib.AxCVgrabber axCVgrabber8;
        private AxCVIMAGELib.AxCVimage axCVimage6;
        private AxCVIMAGELib.AxCVimage axCVimage7;
        private AxCVIMAGELib.AxCVimage axCVimage8;
        private System.Windows.Forms.TableLayoutPanel tblLayoutMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblCamRet01;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel20;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblCamRet02;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel21;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblCamRet03;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel22;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblCamRet04;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel23;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblCamRet05;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel24;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblCamRet06;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel25;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblCamRet07;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel26;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label lblCamRet08;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.TableLayoutPanel tblLayoutMenu;
        private System.Windows.Forms.Button btnModelSettings;
        private System.Windows.Forms.Button btnAutoMode;
        private System.Windows.Forms.Panel pnlMenuMarker;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.CheckBox chkPositive01;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label EdgeThresholdMin01;
        private System.Windows.Forms.Label EdgeThresholdMax01;
        private System.Windows.Forms.TrackBar tbMin01;
        private System.Windows.Forms.TrackBar tbMax01;
        private System.Windows.Forms.CheckBox chkPositive02;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label EdgeThresholdMin02;
        private System.Windows.Forms.Label EdgeThresholdMax02;
        private System.Windows.Forms.TrackBar tbMin02;
        private System.Windows.Forms.TrackBar tbMax02;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.CheckBox chkPLC_HB;
        private System.Windows.Forms.CheckBox chkPLC_InspStart;
        private System.Windows.Forms.Button btnPLC_ModelWrite;
        private System.Windows.Forms.TextBox txtPLC_Model;
        private System.Windows.Forms.RadioButton radManualMod;
        private System.Windows.Forms.RadioButton radAutoMod;
    }
}

