namespace ljm4
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button6 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.address = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.email = new System.Windows.Forms.TextBox();
            this.phone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.name = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.num = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.sex = new System.Windows.Forms.TextBox();
            this.imgName = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.addnum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dorm2 = new System.Windows.Forms.RadioButton();
            this.dorm1 = new System.Windows.Forms.RadioButton();
            this.major = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dormtxt = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Female = new System.Windows.Forms.RadioButton();
            this.Male = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(383, 389);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(99, 41);
            this.button6.TabIndex = 59;
            this.button6.Text = "닫기";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(75, 149);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(165, 21);
            this.dateTimePicker2.TabIndex = 4;
            this.dateTimePicker2.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            // 
            // address
            // 
            this.address.Location = new System.Drawing.Point(75, 327);
            this.address.Name = "address";
            this.address.Size = new System.Drawing.Size(407, 21);
            this.address.TabIndex = 13;
            this.address.TextChanged += new System.EventHandler(this.address_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LightGray;
            this.pictureBox1.Location = new System.Drawing.Point(255, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(227, 209);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 55;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 182);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 61;
            this.label9.Text = "성      별";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 330);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 53;
            this.label8.Text = "주      소";
            // 
            // button7
            // 
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Location = new System.Drawing.Point(255, 283);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(228, 38);
            this.button7.TabIndex = 58;
            this.button7.Text = "이미지 넣기";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button4
            // 
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Location = new System.Drawing.Point(255, 389);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(99, 41);
            this.button4.TabIndex = 60;
            this.button4.Text = "신청";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(75, 235);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(165, 21);
            this.email.TabIndex = 9;
            this.email.TextChanged += new System.EventHandler(this.email_TextChanged);
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(75, 208);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(165, 21);
            this.phone.TabIndex = 8;
            this.phone.TextChanged += new System.EventHandler(this.phone_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 51;
            this.label3.Text = "학      번";
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(75, 122);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(165, 21);
            this.name.TabIndex = 3;
            this.name.TextChanged += new System.EventHandler(this.name_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 49;
            this.label4.Text = "이      름";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 54;
            this.label5.Text = "생년월일";
            // 
            // num
            // 
            this.num.Location = new System.Drawing.Point(75, 68);
            this.num.Name = "num";
            this.num.Size = new System.Drawing.Size(165, 21);
            this.num.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 211);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 50;
            this.label6.Text = "전화번호";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 238);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 52;
            this.label7.Text = "메일주소";
            // 
            // sex
            // 
            this.sex.Enabled = false;
            this.sex.Location = new System.Drawing.Point(59, 400);
            this.sex.Name = "sex";
            this.sex.ReadOnly = true;
            this.sex.Size = new System.Drawing.Size(10, 21);
            this.sex.TabIndex = 64;
            this.sex.Visible = false;
            this.sex.TextChanged += new System.EventHandler(this.sex_TextChanged);
            // 
            // imgName
            // 
            this.imgName.Enabled = false;
            this.imgName.Location = new System.Drawing.Point(43, 400);
            this.imgName.Name = "imgName";
            this.imgName.ReadOnly = true;
            this.imgName.Size = new System.Drawing.Size(10, 21);
            this.imgName.TabIndex = 63;
            this.imgName.Visible = false;
            this.imgName.TextChanged += new System.EventHandler(this.imgName_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(75, 400);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(10, 21);
            this.textBox1.TabIndex = 65;
            this.textBox1.Visible = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // addnum
            // 
            this.addnum.Location = new System.Drawing.Point(75, 298);
            this.addnum.Name = "addnum";
            this.addnum.Size = new System.Drawing.Size(165, 21);
            this.addnum.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 301);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 67;
            this.label1.Text = "우편번호";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 271);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 70;
            this.label10.Text = "기숙사\r\n";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dorm2
            // 
            this.dorm2.AutoSize = true;
            this.dorm2.Location = new System.Drawing.Point(169, 270);
            this.dorm2.Name = "dorm2";
            this.dorm2.Size = new System.Drawing.Size(59, 16);
            this.dorm2.TabIndex = 11;
            this.dorm2.TabStop = true;
            this.dorm2.Text = "미신청";
            this.dorm2.UseVisualStyleBackColor = true;
            this.dorm2.CheckedChanged += new System.EventHandler(this.dorm2_CheckedChanged);
            // 
            // dorm1
            // 
            this.dorm1.AutoSize = true;
            this.dorm1.Location = new System.Drawing.Point(98, 270);
            this.dorm1.Name = "dorm1";
            this.dorm1.Size = new System.Drawing.Size(47, 16);
            this.dorm1.TabIndex = 10;
            this.dorm1.TabStop = true;
            this.dorm1.Text = "신청";
            this.dorm1.UseVisualStyleBackColor = true;
            this.dorm1.CheckedChanged += new System.EventHandler(this.dorm1_CheckedChanged);
            // 
            // major
            // 
            this.major.Location = new System.Drawing.Point(75, 95);
            this.major.Name = "major";
            this.major.Size = new System.Drawing.Size(165, 21);
            this.major.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 74;
            this.label2.Text = "학      과";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 352);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 24);
            this.label11.TabIndex = 75;
            this.label11.Text = "현장실습\r\n선택";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(75, 354);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(407, 20);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dormtxt
            // 
            this.dormtxt.Enabled = false;
            this.dormtxt.Location = new System.Drawing.Point(91, 400);
            this.dormtxt.Name = "dormtxt";
            this.dormtxt.ReadOnly = true;
            this.dormtxt.Size = new System.Drawing.Size(10, 21);
            this.dormtxt.TabIndex = 77;
            this.dormtxt.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Female);
            this.panel1.Controls.Add(this.Male);
            this.panel1.Location = new System.Drawing.Point(75, 176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 26);
            this.panel1.TabIndex = 5;
            // 
            // Female
            // 
            this.Female.AutoSize = true;
            this.Female.Location = new System.Drawing.Point(94, 5);
            this.Female.Name = "Female";
            this.Female.Size = new System.Drawing.Size(47, 16);
            this.Female.TabIndex = 7;
            this.Female.TabStop = true;
            this.Female.Text = "여자";
            this.Female.UseVisualStyleBackColor = true;
            this.Female.CheckedChanged += new System.EventHandler(this.Female_CheckedChanged_1);
            // 
            // Male
            // 
            this.Male.AutoSize = true;
            this.Male.Location = new System.Drawing.Point(23, 5);
            this.Male.Name = "Male";
            this.Male.Size = new System.Drawing.Size(47, 16);
            this.Male.TabIndex = 6;
            this.Male.TabStop = true;
            this.Male.Text = "남자";
            this.Male.UseVisualStyleBackColor = true;
            this.Male.CheckedChanged += new System.EventHandler(this.Male_CheckedChanged_1);
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(503, 452);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dormtxt);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.major);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dorm2);
            this.Controls.Add(this.dorm1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addnum);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.sex);
            this.Controls.Add(this.imgName);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.address);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.email);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.num);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.MaximizeBox = false;
            this.Name = "Form4";
            this.Text = "현장실습 신청";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox address;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox sex;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.TextBox imgName;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox num;
        private System.Windows.Forms.TextBox addnum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton dorm2;
        private System.Windows.Forms.RadioButton dorm1;
        private System.Windows.Forms.TextBox major;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox dormtxt;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton Female;
        private System.Windows.Forms.RadioButton Male;
    }
}