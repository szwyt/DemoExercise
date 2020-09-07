namespace ClProject.SerialPortUtility
{
    partial class SerialPortCommunication
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
            this.gbHint = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbGetValue = new System.Windows.Forms.TextBox();
            this.gbHint.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbHint
            // 
            this.gbHint.Controls.Add(this.tbGetValue);
            this.gbHint.Controls.Add(this.btnSave);
            this.gbHint.Controls.Add(this.btnStart);
            this.gbHint.Location = new System.Drawing.Point(12, 12);
            this.gbHint.Name = "gbHint";
            this.gbHint.Size = new System.Drawing.Size(481, 196);
            this.gbHint.TabIndex = 10;
            this.gbHint.TabStop = false;
            this.gbHint.Text = "赣A27244";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
          
            this.btnSave.Enabled = false;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(337, 141);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnSave.Size = new System.Drawing.Size(122, 42);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
          
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(206, 141);
            this.btnStart.Margin = new System.Windows.Forms.Padding(0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.btnStart.Size = new System.Drawing.Size(122, 42);
            this.btnStart.TabIndex = 18;
            this.btnStart.Text = "开始接收";
            this.btnStart.UseVisualStyleBackColor = false;
         
            // 
            // tbGetValue
            // 
            this.tbGetValue.BackColor = System.Drawing.Color.Black;
            this.tbGetValue.Font = new System.Drawing.Font("微软雅黑", 60F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbGetValue.ForeColor = System.Drawing.Color.White;
            this.tbGetValue.Location = new System.Drawing.Point(17, 30);
            this.tbGetValue.Multiline = true;
            this.tbGetValue.Name = "tbGetValue";
            this.tbGetValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tbGetValue.Size = new System.Drawing.Size(445, 108);
            this.tbGetValue.TabIndex = 11;
            this.tbGetValue.Text = "0";
            // 
            // SerialPortCommunication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 217);
            this.Controls.Add(this.gbHint);
            this.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "SerialPortCommunication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "称重";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SerialPortCommunication_FormClosed);
            this.Load += new System.EventHandler(this.SerialPort_Load);
            this.gbHint.ResumeLayout(false);
            this.gbHint.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbHint;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbGetValue;
    }
}