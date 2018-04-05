using System.Windows.Forms;

namespace Interop.Common.Util
{
	/// <summary>
	/// Form1에 대한 요약 설명입니다.
	/// </summary>
	public class frmPasswordKeyPad : Form {
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnKey0;
		private System.Windows.Forms.Button btnKey9;
		private System.Windows.Forms.Button btnKey8;
		private System.Windows.Forms.Button btnKey7;
		private System.Windows.Forms.Button btnKey6;
		private System.Windows.Forms.Button btnKey5;
		private System.Windows.Forms.Button btnKey4;
		private System.Windows.Forms.Button btnKey3;
		private System.Windows.Forms.Button btnKey2;
		private System.Windows.Forms.Button btnKey1;
		private System.Windows.Forms.TextBox txtInputValue;
		private System.Windows.Forms.Button btnKeyClear;
		private System.Windows.Forms.Button btnKeyBS;
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmPasswordKeyPad() {
			//
			// Windows Form 디자이너 지원에 필요합니다.
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent를 호출한 다음 생성자 코드를 추가합니다.
			//
		}


		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		protected override void Dispose(bool disposing) {
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드
		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent() {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnKeyBS = new System.Windows.Forms.Button();
            this.btnKey0 = new System.Windows.Forms.Button();
            this.btnKey9 = new System.Windows.Forms.Button();
            this.btnKey8 = new System.Windows.Forms.Button();
            this.btnKey7 = new System.Windows.Forms.Button();
            this.btnKey6 = new System.Windows.Forms.Button();
            this.btnKey5 = new System.Windows.Forms.Button();
            this.btnKey4 = new System.Windows.Forms.Button();
            this.btnKey3 = new System.Windows.Forms.Button();
            this.btnKey2 = new System.Windows.Forms.Button();
            this.btnKey1 = new System.Windows.Forms.Button();
            this.btnKeyClear = new System.Windows.Forms.Button();
            this.txtInputValue = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCancel.FlatAppearance.BorderSize = 3;
            this.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCancel.Location = new System.Drawing.Point(137, 398);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(117, 55);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "닫  기";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.White;
            this.btnOK.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnOK.FlatAppearance.BorderSize = 3;
            this.btnOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOK.Location = new System.Drawing.Point(8, 398);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(117, 55);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "확  인";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnKeyBS);
            this.groupBox2.Controls.Add(this.btnKey0);
            this.groupBox2.Controls.Add(this.btnKey9);
            this.groupBox2.Controls.Add(this.btnKey8);
            this.groupBox2.Controls.Add(this.btnKey7);
            this.groupBox2.Controls.Add(this.btnKey6);
            this.groupBox2.Controls.Add(this.btnKey5);
            this.groupBox2.Controls.Add(this.btnKey4);
            this.groupBox2.Controls.Add(this.btnKey3);
            this.groupBox2.Controls.Add(this.btnKey2);
            this.groupBox2.Controls.Add(this.btnKey1);
            this.groupBox2.Controls.Add(this.btnKeyClear);
            this.groupBox2.Location = new System.Drawing.Point(8, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 330);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // btnKeyBS
            // 
            this.btnKeyBS.BackColor = System.Drawing.Color.White;
            this.btnKeyBS.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKeyBS.FlatAppearance.BorderSize = 3;
            this.btnKeyBS.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKeyBS.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKeyBS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyBS.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyBS.Location = new System.Drawing.Point(8, 250);
            this.btnKeyBS.Name = "btnKeyBS";
            this.btnKeyBS.Size = new System.Drawing.Size(72, 72);
            this.btnKeyBS.TabIndex = 36;
            this.btnKeyBS.Text = "BS";
            this.btnKeyBS.UseVisualStyleBackColor = false;
            this.btnKeyBS.Click += new System.EventHandler(this.btnKeyBS_Click);
            // 
            // btnKey0
            // 
            this.btnKey0.BackColor = System.Drawing.Color.White;
            this.btnKey0.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey0.FlatAppearance.BorderSize = 3;
            this.btnKey0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey0.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey0.Location = new System.Drawing.Point(86, 250);
            this.btnKey0.Name = "btnKey0";
            this.btnKey0.Size = new System.Drawing.Size(72, 72);
            this.btnKey0.TabIndex = 32;
            this.btnKey0.Text = "0";
            this.btnKey0.UseVisualStyleBackColor = false;
            this.btnKey0.Click += new System.EventHandler(this.btnKey0_Click);
            // 
            // btnKey9
            // 
            this.btnKey9.BackColor = System.Drawing.Color.White;
            this.btnKey9.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey9.FlatAppearance.BorderSize = 3;
            this.btnKey9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey9.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey9.Location = new System.Drawing.Point(164, 172);
            this.btnKey9.Name = "btnKey9";
            this.btnKey9.Size = new System.Drawing.Size(72, 72);
            this.btnKey9.TabIndex = 31;
            this.btnKey9.Text = "9";
            this.btnKey9.UseVisualStyleBackColor = false;
            this.btnKey9.Click += new System.EventHandler(this.btnKey9_Click);
            // 
            // btnKey8
            // 
            this.btnKey8.BackColor = System.Drawing.Color.White;
            this.btnKey8.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey8.FlatAppearance.BorderSize = 3;
            this.btnKey8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey8.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey8.Location = new System.Drawing.Point(86, 172);
            this.btnKey8.Name = "btnKey8";
            this.btnKey8.Size = new System.Drawing.Size(72, 72);
            this.btnKey8.TabIndex = 30;
            this.btnKey8.Text = "8";
            this.btnKey8.UseVisualStyleBackColor = false;
            this.btnKey8.Click += new System.EventHandler(this.btnKey8_Click);
            // 
            // btnKey7
            // 
            this.btnKey7.BackColor = System.Drawing.Color.White;
            this.btnKey7.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey7.FlatAppearance.BorderSize = 3;
            this.btnKey7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey7.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey7.Location = new System.Drawing.Point(8, 172);
            this.btnKey7.Name = "btnKey7";
            this.btnKey7.Size = new System.Drawing.Size(72, 72);
            this.btnKey7.TabIndex = 29;
            this.btnKey7.Text = "7";
            this.btnKey7.UseVisualStyleBackColor = false;
            this.btnKey7.Click += new System.EventHandler(this.btnKey7_Click);
            // 
            // btnKey6
            // 
            this.btnKey6.BackColor = System.Drawing.Color.White;
            this.btnKey6.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey6.FlatAppearance.BorderSize = 3;
            this.btnKey6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey6.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey6.Location = new System.Drawing.Point(164, 94);
            this.btnKey6.Name = "btnKey6";
            this.btnKey6.Size = new System.Drawing.Size(72, 72);
            this.btnKey6.TabIndex = 28;
            this.btnKey6.Text = "6";
            this.btnKey6.UseVisualStyleBackColor = false;
            this.btnKey6.Click += new System.EventHandler(this.btnKey6_Click);
            // 
            // btnKey5
            // 
            this.btnKey5.BackColor = System.Drawing.Color.White;
            this.btnKey5.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey5.FlatAppearance.BorderSize = 3;
            this.btnKey5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey5.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey5.Location = new System.Drawing.Point(86, 94);
            this.btnKey5.Name = "btnKey5";
            this.btnKey5.Size = new System.Drawing.Size(72, 72);
            this.btnKey5.TabIndex = 27;
            this.btnKey5.Text = "5";
            this.btnKey5.UseVisualStyleBackColor = false;
            this.btnKey5.Click += new System.EventHandler(this.btnKey5_Click);
            // 
            // btnKey4
            // 
            this.btnKey4.BackColor = System.Drawing.Color.White;
            this.btnKey4.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey4.FlatAppearance.BorderSize = 3;
            this.btnKey4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey4.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey4.Location = new System.Drawing.Point(8, 94);
            this.btnKey4.Name = "btnKey4";
            this.btnKey4.Size = new System.Drawing.Size(72, 72);
            this.btnKey4.TabIndex = 26;
            this.btnKey4.Text = "4";
            this.btnKey4.UseVisualStyleBackColor = false;
            this.btnKey4.Click += new System.EventHandler(this.btnKey4_Click);
            // 
            // btnKey3
            // 
            this.btnKey3.BackColor = System.Drawing.Color.White;
            this.btnKey3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey3.FlatAppearance.BorderSize = 3;
            this.btnKey3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey3.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey3.Location = new System.Drawing.Point(164, 16);
            this.btnKey3.Name = "btnKey3";
            this.btnKey3.Size = new System.Drawing.Size(72, 72);
            this.btnKey3.TabIndex = 25;
            this.btnKey3.Text = "3";
            this.btnKey3.UseVisualStyleBackColor = false;
            this.btnKey3.Click += new System.EventHandler(this.btnKey3_Click);
            // 
            // btnKey2
            // 
            this.btnKey2.BackColor = System.Drawing.Color.White;
            this.btnKey2.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey2.FlatAppearance.BorderSize = 3;
            this.btnKey2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey2.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey2.Location = new System.Drawing.Point(86, 16);
            this.btnKey2.Name = "btnKey2";
            this.btnKey2.Size = new System.Drawing.Size(72, 72);
            this.btnKey2.TabIndex = 24;
            this.btnKey2.Text = "2";
            this.btnKey2.UseVisualStyleBackColor = false;
            this.btnKey2.Click += new System.EventHandler(this.btnKey2_Click);
            // 
            // btnKey1
            // 
            this.btnKey1.BackColor = System.Drawing.Color.White;
            this.btnKey1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKey1.FlatAppearance.BorderSize = 3;
            this.btnKey1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKey1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKey1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKey1.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKey1.Location = new System.Drawing.Point(8, 16);
            this.btnKey1.Name = "btnKey1";
            this.btnKey1.Size = new System.Drawing.Size(72, 72);
            this.btnKey1.TabIndex = 23;
            this.btnKey1.Text = "1";
            this.btnKey1.UseVisualStyleBackColor = false;
            this.btnKey1.Click += new System.EventHandler(this.btnKey1_Click);
            // 
            // btnKeyClear
            // 
            this.btnKeyClear.BackColor = System.Drawing.Color.White;
            this.btnKeyClear.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnKeyClear.FlatAppearance.BorderSize = 3;
            this.btnKeyClear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Lime;
            this.btnKeyClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKeyClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeyClear.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnKeyClear.Location = new System.Drawing.Point(164, 250);
            this.btnKeyClear.Name = "btnKeyClear";
            this.btnKeyClear.Size = new System.Drawing.Size(72, 72);
            this.btnKeyClear.TabIndex = 35;
            this.btnKeyClear.Text = "Clr";
            this.btnKeyClear.UseVisualStyleBackColor = false;
            this.btnKeyClear.Click += new System.EventHandler(this.btnKeyClear_Click);
            // 
            // txtInputValue
            // 
            this.txtInputValue.Font = new System.Drawing.Font("굴림", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtInputValue.Location = new System.Drawing.Point(8, 10);
            this.txtInputValue.Name = "txtInputValue";
            this.txtInputValue.PasswordChar = '*';
            this.txtInputValue.Size = new System.Drawing.Size(246, 39);
            this.txtInputValue.TabIndex = 5;
            this.txtInputValue.Text = "1111";
            this.txtInputValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmPasswordKeyPad
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(261, 463);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtInputValue);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(269, 497);
            this.Name = "frmPasswordKeyPad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Password Input";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmPasswordKeyPad_Load);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		//		[STAThread]
		//		static void Main() 
		//		{
		//			Application.Run(new VirtualKeyPad());
		//		}


		private void inputValue(string keyValue) {
			txtInputValue.Text += keyValue;
		}

		private void btnKey1_Click(object sender, System.EventArgs e) {
			inputValue("1");
		}

		private void btnKey2_Click(object sender, System.EventArgs e) {
			inputValue("2");
		}

		private void btnKey3_Click(object sender, System.EventArgs e) {
			inputValue("3");
		}

		private void btnKey4_Click(object sender, System.EventArgs e) {
			inputValue("4");
		}

		private void btnKey5_Click(object sender, System.EventArgs e) {
			inputValue("5");
		}

		private void btnKey6_Click(object sender, System.EventArgs e) {
			inputValue("6");
		}

		private void btnKey7_Click(object sender, System.EventArgs e) {
			inputValue("7");
		}

		private void btnKey8_Click(object sender, System.EventArgs e) {
			inputValue("8");
		}

		private void btnKey9_Click(object sender, System.EventArgs e) {
			inputValue("9");
		}

		private void btnKey0_Click(object sender, System.EventArgs e) {
			inputValue("0");
		}

		private void btnKeyClear_Click(object sender, System.EventArgs e) {
			txtInputValue.Text = "";
		}

		private void btnKeyBS_Click(object sender, System.EventArgs e) {
			if (txtInputValue.Text.Length > 0) {
				txtInputValue.Text = txtInputValue.Text.Substring(0, txtInputValue.Text.Length - 1);
			}
		}

		private void btnOK_Click(object sender, System.EventArgs e) {
			if (txtInputValue.Text.Trim().Equals(this.iniPw)) {
				DialogResult = DialogResult.OK;
				this.Close();
			} else {
				MessageBox.Show("\r\n새로 입력하세요!!!\r\n", "패스워드 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnKeyClear_Click(null, null);
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}

 
        private string iniPw = string.Empty;

        public string returnValue;


        private void frmPasswordKeyPad_Load(object sender, System.EventArgs e)
        {
            Interop.Common.Util.cIni.ReadAdmin(ref iniPw);
            txtInputValue.Text = "";


            txtInputValue.Text = iniPw;

        }  
	}
}
