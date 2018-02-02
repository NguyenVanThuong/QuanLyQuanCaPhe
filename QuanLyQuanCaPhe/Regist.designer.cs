namespace QuanLyQuanCaPhe
{
    partial class Regist
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtUSname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRegist = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtUSname);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 30);
            this.panel1.TabIndex = 0;
            // 
            // txtUSname
            // 
            this.txtUSname.Location = new System.Drawing.Point(73, 4);
            this.txtUSname.Name = "txtUSname";
            this.txtUSname.Size = new System.Drawing.Size(183, 20);
            this.txtUSname.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tài khoản";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtPassword);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(13, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(259, 30);
            this.panel2.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(73, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(183, 20);
            this.txtPassword.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mật khẩu";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtName);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(13, 85);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(259, 30);
            this.panel3.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(73, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(183, 20);
            this.txtName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tên ";
            // 
            // btnRegist
            // 
            this.btnRegist.Location = new System.Drawing.Point(194, 121);
            this.btnRegist.Name = "btnRegist";
            this.btnRegist.Size = new System.Drawing.Size(75, 23);
            this.btnRegist.TabIndex = 4;
            this.btnRegist.Text = "Đăng ký";
            this.btnRegist.UseVisualStyleBackColor = true;
            this.btnRegist.Click += new System.EventHandler(this.btnRegist_Click);
            // 
            // Regist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 163);
            this.Controls.Add(this.btnRegist);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Regist";
            this.Text = "Regist";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtUSname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRegist;
    }
}