namespace DB_Scheme_Extract
{
    partial class ExtractForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.result = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.Label();
            this.generate = new System.Windows.Forms.Button();
            this.exportPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.userId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataSource = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.resultTxt = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(503, 313);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.message);
            this.tabPage1.Controls.Add(this.generate);
            this.tabPage1.Controls.Add(this.exportPath);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.password);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.userId);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.dataSource);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(495, 287);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Location = new System.Drawing.Point(6, 15);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(104, 13);
            this.result.TabIndex = 11;
            this.result.Text = "Success write to file:";
            // 
            // message
            // 
            this.message.AutoSize = true;
            this.message.Location = new System.Drawing.Point(22, 166);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(0, 13);
            this.message.TabIndex = 9;
            // 
            // generate
            // 
            this.generate.Location = new System.Drawing.Point(25, 256);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(117, 23);
            this.generate.TabIndex = 8;
            this.generate.Text = "Generate All Objects";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // exportPath
            // 
            this.exportPath.Location = new System.Drawing.Point(111, 121);
            this.exportPath.Name = "exportPath";
            this.exportPath.Size = new System.Drawing.Size(251, 20);
            this.exportPath.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Export File Path";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(111, 87);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(251, 20);
            this.password.TabIndex = 5;
            this.password.Text = "password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password";
            // 
            // userId
            // 
            this.userId.Location = new System.Drawing.Point(111, 53);
            this.userId.Name = "userId";
            this.userId.Size = new System.Drawing.Size(251, 20);
            this.userId.TabIndex = 3;
            this.userId.Text = "MS9DJA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "User ID";
            // 
            // dataSource
            // 
            this.dataSource.Location = new System.Drawing.Point(111, 21);
            this.dataSource.Name = "dataSource";
            this.dataSource.Size = new System.Drawing.Size(251, 20);
            this.dataSource.TabIndex = 1;
            this.dataSource.Text = "localhost/extdev";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Source";
            this.label1.Click += new System.EventHandler(this.generate_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(495, 223);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // resultTxt
            // 
            this.resultTxt.Location = new System.Drawing.Point(9, 46);
            this.resultTxt.Multiline = true;
            this.resultTxt.Name = "resultTxt";
            this.resultTxt.Size = new System.Drawing.Size(406, 44);
            this.resultTxt.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.result);
            this.panel1.Controls.Add(this.resultTxt);
            this.panel1.Location = new System.Drawing.Point(16, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 103);
            this.panel1.TabIndex = 13;
            // 
            // ExtractForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 325);
            this.Controls.Add(this.tabControl1);
            this.Name = "ExtractForm";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox exportPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dataSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button generate;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.TextBox resultTxt;
        private System.Windows.Forms.Panel panel1;

    }
}

