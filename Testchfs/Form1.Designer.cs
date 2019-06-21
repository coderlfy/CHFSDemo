namespace Testchfs
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnlogin = new System.Windows.Forms.Button();
            this.btngetfiles = new System.Windows.Forms.Button();
            this.btnupload = new System.Windows.Forms.Button();
            this.btnExist = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(87, 81);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(196, 23);
            this.btnlogin.TabIndex = 0;
            this.btnlogin.Text = "登陆";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // btngetfiles
            // 
            this.btngetfiles.Location = new System.Drawing.Point(87, 138);
            this.btngetfiles.Name = "btngetfiles";
            this.btngetfiles.Size = new System.Drawing.Size(196, 23);
            this.btngetfiles.TabIndex = 1;
            this.btngetfiles.Text = "获取文件列表";
            this.btngetfiles.UseVisualStyleBackColor = true;
            this.btngetfiles.Click += new System.EventHandler(this.btngetfiles_Click);
            // 
            // btnupload
            // 
            this.btnupload.Location = new System.Drawing.Point(87, 251);
            this.btnupload.Name = "btnupload";
            this.btnupload.Size = new System.Drawing.Size(196, 23);
            this.btnupload.TabIndex = 2;
            this.btnupload.Text = "上传文件";
            this.btnupload.UseVisualStyleBackColor = true;
            this.btnupload.Click += new System.EventHandler(this.btnupload_Click);
            // 
            // btnExist
            // 
            this.btnExist.Location = new System.Drawing.Point(87, 195);
            this.btnExist.Name = "btnExist";
            this.btnExist.Size = new System.Drawing.Size(196, 23);
            this.btnExist.TabIndex = 3;
            this.btnExist.Text = "文件是否存在";
            this.btnExist.UseVisualStyleBackColor = true;
            this.btnExist.Click += new System.EventHandler(this.btnExist_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 410);
            this.Controls.Add(this.btnExist);
            this.Controls.Add(this.btnupload);
            this.Controls.Add(this.btngetfiles);
            this.Controls.Add(this.btnlogin);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btngetfiles;
        private System.Windows.Forms.Button btnupload;
        private System.Windows.Forms.Button btnExist;
    }
}

