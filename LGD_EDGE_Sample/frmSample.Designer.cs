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
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
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
            this.label1 = new System.Windows.Forms.Label();
            this.chkPLC_HB = new System.Windows.Forms.CheckBox();
            this.btnPLC_ModelWrite = new System.Windows.Forms.Button();
            this.txtPLC_Model = new System.Windows.Forms.TextBox();
            this.chkPLC_InspStart = new System.Windows.Forms.CheckBox();
            this.btnImageLoad01 = new System.Windows.Forms.Button();
            this.btnImageClear01 = new System.Windows.Forms.Button();
            this.chkPositive01 = new System.Windows.Forms.CheckBox();
            this.tbMin01 = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMax01 = new System.Windows.Forms.TrackBar();
            this.EdgeThresholdMin01 = new System.Windows.Forms.Label();
            this.EdgeThresholdMax01 = new System.Windows.Forms.Label();
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
            this.label4 = new System.Windows.Forms.Label();
            this.EdgeThresholdMin02 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.EdgeThresholdMax02 = new System.Windows.Forms.Label();
            this.chkPositive02 = new System.Windows.Forms.CheckBox();
            this.tbMin02 = new System.Windows.Forms.TrackBar();
            this.tbMax02 = new System.Windows.Forms.TrackBar();
            this.btnImageLoad02 = new System.Windows.Forms.Button();
            this.btnImageClear02 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnRunInspection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMin01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMax01)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMin02)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMax02)).BeginInit();
            this.SuspendLayout();
            // 
            // axCVdisplay1
            // 
            this.axCVdisplay1.Location = new System.Drawing.Point(1, 24);
            this.axCVdisplay1.Name = "axCVdisplay1";
            this.axCVdisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay1.OcxState")));
            this.axCVdisplay1.Size = new System.Drawing.Size(300, 289);
            this.axCVdisplay1.TabIndex = 0;
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
            this.axCVgrabber1.Location = new System.Drawing.Point(1, 1);
            this.axCVgrabber1.Name = "axCVgrabber1";
            this.axCVgrabber1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber1.OcxState")));
            this.axCVgrabber1.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber1.TabIndex = 1;
            // 
            // axCVimage1
            // 
            this.axCVimage1.Enabled = true;
            this.axCVimage1.Location = new System.Drawing.Point(35, 1);
            this.axCVimage1.Name = "axCVimage1";
            this.axCVimage1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage1.OcxState")));
            this.axCVimage1.Size = new System.Drawing.Size(32, 32);
            this.axCVimage1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(603, 168);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "PLC";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkPLC_HB
            // 
            this.chkPLC_HB.AutoSize = true;
            this.chkPLC_HB.ForeColor = System.Drawing.Color.White;
            this.chkPLC_HB.Location = new System.Drawing.Point(607, 193);
            this.chkPLC_HB.Name = "chkPLC_HB";
            this.chkPLC_HB.Size = new System.Drawing.Size(99, 16);
            this.chkPLC_HB.TabIndex = 4;
            this.chkPLC_HB.Text = "Flicker Active";
            this.chkPLC_HB.UseVisualStyleBackColor = true;
            this.chkPLC_HB.CheckedChanged += new System.EventHandler(this.chkPLC_HB_CheckedChanged);
            // 
            // btnPLC_ModelWrite
            // 
            this.btnPLC_ModelWrite.Location = new System.Drawing.Point(717, 218);
            this.btnPLC_ModelWrite.Name = "btnPLC_ModelWrite";
            this.btnPLC_ModelWrite.Size = new System.Drawing.Size(87, 20);
            this.btnPLC_ModelWrite.TabIndex = 5;
            this.btnPLC_ModelWrite.Text = "Model Write";
            this.btnPLC_ModelWrite.UseVisualStyleBackColor = true;
            this.btnPLC_ModelWrite.Click += new System.EventHandler(this.btnPLC_ModelWrite_Click);
            // 
            // txtPLC_Model
            // 
            this.txtPLC_Model.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPLC_Model.Location = new System.Drawing.Point(607, 217);
            this.txtPLC_Model.Name = "txtPLC_Model";
            this.txtPLC_Model.Size = new System.Drawing.Size(104, 21);
            this.txtPLC_Model.TabIndex = 6;
            this.txtPLC_Model.Text = "Model";
            this.txtPLC_Model.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chkPLC_InspStart
            // 
            this.chkPLC_InspStart.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPLC_InspStart.ForeColor = System.Drawing.Color.Black;
            this.chkPLC_InspStart.Location = new System.Drawing.Point(717, 193);
            this.chkPLC_InspStart.Name = "chkPLC_InspStart";
            this.chkPLC_InspStart.Size = new System.Drawing.Size(87, 20);
            this.chkPLC_InspStart.TabIndex = 4;
            this.chkPLC_InspStart.Text = "Insp Start";
            this.chkPLC_InspStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPLC_InspStart.UseVisualStyleBackColor = true;
            this.chkPLC_InspStart.CheckedChanged += new System.EventHandler(this.chkPLC_InspStart_CheckedChanged);
            // 
            // btnImageLoad01
            // 
            this.btnImageLoad01.Location = new System.Drawing.Point(603, 317);
            this.btnImageLoad01.Name = "btnImageLoad01";
            this.btnImageLoad01.Size = new System.Drawing.Size(100, 31);
            this.btnImageLoad01.TabIndex = 5;
            this.btnImageLoad01.Text = "ImageLoad01";
            this.btnImageLoad01.UseVisualStyleBackColor = true;
            this.btnImageLoad01.Click += new System.EventHandler(this.btnImageLoad01_Click);
            // 
            // btnImageClear01
            // 
            this.btnImageClear01.Location = new System.Drawing.Point(703, 317);
            this.btnImageClear01.Name = "btnImageClear01";
            this.btnImageClear01.Size = new System.Drawing.Size(101, 31);
            this.btnImageClear01.TabIndex = 5;
            this.btnImageClear01.Text = "ImageClear01";
            this.btnImageClear01.UseVisualStyleBackColor = true;
            this.btnImageClear01.Click += new System.EventHandler(this.btnImageClear01_Click);
            // 
            // chkPositive01
            // 
            this.chkPositive01.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPositive01.ForeColor = System.Drawing.Color.Black;
            this.chkPositive01.Location = new System.Drawing.Point(1, 315);
            this.chkPositive01.Name = "chkPositive01";
            this.chkPositive01.Size = new System.Drawing.Size(60, 64);
            this.chkPositive01.TabIndex = 4;
            this.chkPositive01.Text = "Positive";
            this.chkPositive01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPositive01.UseVisualStyleBackColor = true;
            this.chkPositive01.CheckedChanged += new System.EventHandler(this.chkPositive01_CheckedChanged);
            // 
            // tbMin01
            // 
            this.tbMin01.AutoSize = false;
            this.tbMin01.Location = new System.Drawing.Point(146, 315);
            this.tbMin01.Maximum = 255;
            this.tbMin01.Name = "tbMin01";
            this.tbMin01.Size = new System.Drawing.Size(151, 32);
            this.tbMin01.TabIndex = 7;
            this.tbMin01.Scroll += new System.EventHandler(this.tbMin01_Scroll);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(61, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "MIN";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(61, 347);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 32);
            this.label3.TabIndex = 3;
            this.label3.Text = "MAX";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbMax01
            // 
            this.tbMax01.AutoSize = false;
            this.tbMax01.Location = new System.Drawing.Point(146, 347);
            this.tbMax01.Maximum = 255;
            this.tbMax01.Name = "tbMax01";
            this.tbMax01.Size = new System.Drawing.Size(151, 32);
            this.tbMax01.TabIndex = 7;
            this.tbMax01.Scroll += new System.EventHandler(this.tbMax01_Scroll);
            // 
            // EdgeThresholdMin01
            // 
            this.EdgeThresholdMin01.BackColor = System.Drawing.Color.White;
            this.EdgeThresholdMin01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeThresholdMin01.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EdgeThresholdMin01.ForeColor = System.Drawing.Color.Black;
            this.EdgeThresholdMin01.Location = new System.Drawing.Point(100, 315);
            this.EdgeThresholdMin01.Name = "EdgeThresholdMin01";
            this.EdgeThresholdMin01.Size = new System.Drawing.Size(46, 32);
            this.EdgeThresholdMin01.TabIndex = 3;
            this.EdgeThresholdMin01.Text = "0";
            this.EdgeThresholdMin01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EdgeThresholdMax01
            // 
            this.EdgeThresholdMax01.BackColor = System.Drawing.Color.White;
            this.EdgeThresholdMax01.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeThresholdMax01.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EdgeThresholdMax01.ForeColor = System.Drawing.Color.Black;
            this.EdgeThresholdMax01.Location = new System.Drawing.Point(100, 347);
            this.EdgeThresholdMax01.Name = "EdgeThresholdMax01";
            this.EdgeThresholdMax01.Size = new System.Drawing.Size(46, 32);
            this.EdgeThresholdMax01.TabIndex = 3;
            this.EdgeThresholdMax01.Text = "0";
            this.EdgeThresholdMax01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(602, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 22);
            this.label6.TabIndex = 3;
            this.label6.Text = "Status PLC";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPLC_HB
            // 
            this.lblPLC_HB.BackColor = System.Drawing.Color.White;
            this.lblPLC_HB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPLC_HB.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPLC_HB.ForeColor = System.Drawing.Color.Black;
            this.lblPLC_HB.Location = new System.Drawing.Point(602, 46);
            this.lblPLC_HB.Name = "lblPLC_HB";
            this.lblPLC_HB.Size = new System.Drawing.Size(100, 22);
            this.lblPLC_HB.TabIndex = 3;
            this.lblPLC_HB.Text = "Alive Flicker";
            this.lblPLC_HB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPLC_InspStart
            // 
            this.lblPLC_InspStart.BackColor = System.Drawing.Color.White;
            this.lblPLC_InspStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPLC_InspStart.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPLC_InspStart.ForeColor = System.Drawing.Color.Black;
            this.lblPLC_InspStart.Location = new System.Drawing.Point(602, 68);
            this.lblPLC_InspStart.Name = "lblPLC_InspStart";
            this.lblPLC_InspStart.Size = new System.Drawing.Size(100, 22);
            this.lblPLC_InspStart.TabIndex = 3;
            this.lblPLC_InspStart.Text = "Insp Start";
            this.lblPLC_InspStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(704, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 22);
            this.label10.TabIndex = 3;
            this.label10.Text = "Status PC";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPC_HB
            // 
            this.lblPC_HB.BackColor = System.Drawing.Color.White;
            this.lblPC_HB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPC_HB.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPC_HB.ForeColor = System.Drawing.Color.Black;
            this.lblPC_HB.Location = new System.Drawing.Point(704, 46);
            this.lblPC_HB.Name = "lblPC_HB";
            this.lblPC_HB.Size = new System.Drawing.Size(100, 22);
            this.lblPC_HB.TabIndex = 3;
            this.lblPC_HB.Text = "Alive Flicker";
            this.lblPC_HB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPC_InspStart
            // 
            this.lblPC_InspStart.BackColor = System.Drawing.Color.White;
            this.lblPC_InspStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPC_InspStart.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPC_InspStart.ForeColor = System.Drawing.Color.Black;
            this.lblPC_InspStart.Location = new System.Drawing.Point(704, 68);
            this.lblPC_InspStart.Name = "lblPC_InspStart";
            this.lblPC_InspStart.Size = new System.Drawing.Size(100, 22);
            this.lblPC_InspStart.TabIndex = 3;
            this.lblPC_InspStart.Text = "InspStart";
            this.lblPC_InspStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPC_InspEnd
            // 
            this.lblPC_InspEnd.BackColor = System.Drawing.Color.White;
            this.lblPC_InspEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPC_InspEnd.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPC_InspEnd.ForeColor = System.Drawing.Color.Black;
            this.lblPC_InspEnd.Location = new System.Drawing.Point(704, 90);
            this.lblPC_InspEnd.Name = "lblPC_InspEnd";
            this.lblPC_InspEnd.Size = new System.Drawing.Size(100, 22);
            this.lblPC_InspEnd.TabIndex = 3;
            this.lblPC_InspEnd.Text = "InspEnd";
            this.lblPC_InspEnd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPC_TotalJudge
            // 
            this.lblPC_TotalJudge.BackColor = System.Drawing.Color.White;
            this.lblPC_TotalJudge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPC_TotalJudge.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblPC_TotalJudge.ForeColor = System.Drawing.Color.Black;
            this.lblPC_TotalJudge.Location = new System.Drawing.Point(704, 112);
            this.lblPC_TotalJudge.Name = "lblPC_TotalJudge";
            this.lblPC_TotalJudge.Size = new System.Drawing.Size(100, 47);
            this.lblPC_TotalJudge.TabIndex = 3;
            this.lblPC_TotalJudge.Text = "Judge";
            this.lblPC_TotalJudge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSystemTime
            // 
            this.lblSystemTime.BackColor = System.Drawing.Color.White;
            this.lblSystemTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSystemTime.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSystemTime.ForeColor = System.Drawing.Color.Black;
            this.lblSystemTime.Location = new System.Drawing.Point(602, 1);
            this.lblSystemTime.Name = "lblSystemTime";
            this.lblSystemTime.Size = new System.Drawing.Size(202, 22);
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
            this.axCVdisplay2.Location = new System.Drawing.Point(301, 24);
            this.axCVdisplay2.Name = "axCVdisplay2";
            this.axCVdisplay2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVdisplay2.OcxState")));
            this.axCVdisplay2.Size = new System.Drawing.Size(300, 289);
            this.axCVdisplay2.TabIndex = 0;
            // 
            // axCVgrabber2
            // 
            this.axCVgrabber2.Enabled = true;
            this.axCVgrabber2.Location = new System.Drawing.Point(305, 1);
            this.axCVgrabber2.Name = "axCVgrabber2";
            this.axCVgrabber2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVgrabber2.OcxState")));
            this.axCVgrabber2.Size = new System.Drawing.Size(32, 32);
            this.axCVgrabber2.TabIndex = 1;
            // 
            // axCVimage2
            // 
            this.axCVimage2.Enabled = true;
            this.axCVimage2.Location = new System.Drawing.Point(339, 1);
            this.axCVimage2.Name = "axCVimage2";
            this.axCVimage2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axCVimage2.OcxState")));
            this.axCVimage2.Size = new System.Drawing.Size(32, 32);
            this.axCVimage2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(358, 315);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "MIN";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EdgeThresholdMin02
            // 
            this.EdgeThresholdMin02.BackColor = System.Drawing.Color.White;
            this.EdgeThresholdMin02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeThresholdMin02.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EdgeThresholdMin02.ForeColor = System.Drawing.Color.Black;
            this.EdgeThresholdMin02.Location = new System.Drawing.Point(397, 315);
            this.EdgeThresholdMin02.Name = "EdgeThresholdMin02";
            this.EdgeThresholdMin02.Size = new System.Drawing.Size(46, 32);
            this.EdgeThresholdMin02.TabIndex = 3;
            this.EdgeThresholdMin02.Text = "0";
            this.EdgeThresholdMin02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(358, 347);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 32);
            this.label7.TabIndex = 3;
            this.label7.Text = "MAX";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EdgeThresholdMax02
            // 
            this.EdgeThresholdMax02.BackColor = System.Drawing.Color.White;
            this.EdgeThresholdMax02.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EdgeThresholdMax02.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.EdgeThresholdMax02.ForeColor = System.Drawing.Color.Black;
            this.EdgeThresholdMax02.Location = new System.Drawing.Point(397, 347);
            this.EdgeThresholdMax02.Name = "EdgeThresholdMax02";
            this.EdgeThresholdMax02.Size = new System.Drawing.Size(46, 32);
            this.EdgeThresholdMax02.TabIndex = 3;
            this.EdgeThresholdMax02.Text = "0";
            this.EdgeThresholdMax02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkPositive02
            // 
            this.chkPositive02.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkPositive02.ForeColor = System.Drawing.Color.Black;
            this.chkPositive02.Location = new System.Drawing.Point(298, 315);
            this.chkPositive02.Name = "chkPositive02";
            this.chkPositive02.Size = new System.Drawing.Size(60, 64);
            this.chkPositive02.TabIndex = 4;
            this.chkPositive02.Text = "Positive";
            this.chkPositive02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkPositive02.UseVisualStyleBackColor = true;
            this.chkPositive02.CheckedChanged += new System.EventHandler(this.chkPositive02_CheckedChanged);
            // 
            // tbMin02
            // 
            this.tbMin02.AutoSize = false;
            this.tbMin02.Location = new System.Drawing.Point(443, 315);
            this.tbMin02.Maximum = 255;
            this.tbMin02.Name = "tbMin02";
            this.tbMin02.Size = new System.Drawing.Size(151, 32);
            this.tbMin02.TabIndex = 7;
            this.tbMin02.Scroll += new System.EventHandler(this.tbMin02_Scroll);
            // 
            // tbMax02
            // 
            this.tbMax02.AutoSize = false;
            this.tbMax02.Location = new System.Drawing.Point(443, 347);
            this.tbMax02.Maximum = 255;
            this.tbMax02.Name = "tbMax02";
            this.tbMax02.Size = new System.Drawing.Size(151, 32);
            this.tbMax02.TabIndex = 7;
            this.tbMax02.Scroll += new System.EventHandler(this.tbMax02_Scroll);
            // 
            // btnImageLoad02
            // 
            this.btnImageLoad02.Location = new System.Drawing.Point(603, 348);
            this.btnImageLoad02.Name = "btnImageLoad02";
            this.btnImageLoad02.Size = new System.Drawing.Size(100, 31);
            this.btnImageLoad02.TabIndex = 5;
            this.btnImageLoad02.Text = "ImageLoad02";
            this.btnImageLoad02.UseVisualStyleBackColor = true;
            this.btnImageLoad02.Click += new System.EventHandler(this.btnImageLoad02_Click);
            // 
            // btnImageClear02
            // 
            this.btnImageClear02.Location = new System.Drawing.Point(703, 348);
            this.btnImageClear02.Name = "btnImageClear02";
            this.btnImageClear02.Size = new System.Drawing.Size(100, 31);
            this.btnImageClear02.TabIndex = 5;
            this.btnImageClear02.Text = "ImageClear02";
            this.btnImageClear02.UseVisualStyleBackColor = true;
            this.btnImageClear02.Click += new System.EventHandler(this.btnImageClear02_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(1, 1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(300, 22);
            this.label9.TabIndex = 3;
            this.label9.Text = "Cam01";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(301, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(300, 22);
            this.label11.TabIndex = 3;
            this.label11.Text = "Cam02";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRunInspection
            // 
            this.btnRunInspection.Location = new System.Drawing.Point(603, 285);
            this.btnRunInspection.Name = "btnRunInspection";
            this.btnRunInspection.Size = new System.Drawing.Size(200, 31);
            this.btnRunInspection.TabIndex = 5;
            this.btnRunInspection.Text = "ImageLoad01";
            this.btnRunInspection.UseVisualStyleBackColor = true;
            this.btnRunInspection.Click += new System.EventHandler(this.btnRunInspection_Click);
            // 
            // frmSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(804, 381);
            this.Controls.Add(this.tbMax02);
            this.Controls.Add(this.tbMax01);
            this.Controls.Add(this.tbMin02);
            this.Controls.Add(this.tbMin01);
            this.Controls.Add(this.txtPLC_Model);
            this.Controls.Add(this.btnImageClear02);
            this.Controls.Add(this.btnImageClear01);
            this.Controls.Add(this.btnImageLoad02);
            this.Controls.Add(this.btnRunInspection);
            this.Controls.Add(this.btnImageLoad01);
            this.Controls.Add(this.btnPLC_ModelWrite);
            this.Controls.Add(this.chkPLC_InspStart);
            this.Controls.Add(this.chkPositive02);
            this.Controls.Add(this.chkPositive01);
            this.Controls.Add(this.EdgeThresholdMax02);
            this.Controls.Add(this.chkPLC_HB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.EdgeThresholdMax01);
            this.Controls.Add(this.EdgeThresholdMin02);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EdgeThresholdMin01);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPC_TotalJudge);
            this.Controls.Add(this.lblPC_InspEnd);
            this.Controls.Add(this.lblPLC_InspStart);
            this.Controls.Add(this.lblPC_InspStart);
            this.Controls.Add(this.lblPC_HB);
            this.Controls.Add(this.lblPLC_HB);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblSystemTime);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.axCVimage2);
            this.Controls.Add(this.axCVgrabber2);
            this.Controls.Add(this.axCVimage1);
            this.Controls.Add(this.axCVgrabber1);
            this.Controls.Add(this.axCVdisplay2);
            this.Controls.Add(this.axCVdisplay1);
            this.Name = "frmSample";
            this.Text = "Edge Sample";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSample_FormClosing);
            this.Load += new System.EventHandler(this.frmSample_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMin01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMax01)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVdisplay2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVgrabber2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axCVimage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMin02)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMax02)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxCVDISPLAYLib.AxCVdisplay axCVdisplay1;
        private System.ComponentModel.BackgroundWorker bgWorkPLC;
        private System.Windows.Forms.Timer tmrSystem;
        private AxCVGRABBERLib.AxCVgrabber axCVgrabber1;
        private AxCVIMAGELib.AxCVimage axCVimage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkPLC_HB;
        private System.Windows.Forms.Button btnPLC_ModelWrite;
        private System.Windows.Forms.TextBox txtPLC_Model;
        private System.Windows.Forms.CheckBox chkPLC_InspStart;
        private System.Windows.Forms.Button btnImageLoad01;
        private System.Windows.Forms.Button btnImageClear01;
        private System.Windows.Forms.CheckBox chkPositive01;
        private System.Windows.Forms.TrackBar tbMin01;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbMax01;
        private System.Windows.Forms.Label EdgeThresholdMin01;
        private System.Windows.Forms.Label EdgeThresholdMax01;
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
        private AxCVIMAGELib.AxCVimage axCVimage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label EdgeThresholdMin02;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label EdgeThresholdMax02;
        private System.Windows.Forms.CheckBox chkPositive02;
        private System.Windows.Forms.TrackBar tbMin02;
        private System.Windows.Forms.TrackBar tbMax02;
        private System.Windows.Forms.Button btnImageLoad02;
        private System.Windows.Forms.Button btnImageClear02;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnRunInspection;
    }
}

