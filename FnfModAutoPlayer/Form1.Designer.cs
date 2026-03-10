namespace FnfModAutoPlayer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonBrowse = new Button();
            textBoxPath = new TextBox();
            labelTip = new Label();
            buttonStart = new Button();
            buttonStop = new Button();
            labelInfo = new Label();
            SuspendLayout();
            // 
            // buttonBrowse
            // 
            buttonBrowse.Location = new Point(439, 129);
            buttonBrowse.Name = "buttonBrowse";
            buttonBrowse.Size = new Size(150, 46);
            buttonBrowse.TabIndex = 0;
            buttonBrowse.Text = "选择谱面";
            buttonBrowse.UseVisualStyleBackColor = true;
            buttonBrowse.Click += buttonBrowse_Click;
            // 
            // textBoxPath
            // 
            textBoxPath.Location = new Point(504, 308);
            textBoxPath.Name = "textBoxPath";
            textBoxPath.ReadOnly = true;
            textBoxPath.Size = new Size(200, 38);
            textBoxPath.TabIndex = 1;
            // 
            // labelTip
            // 
            labelTip.AutoSize = true;
            labelTip.Location = new Point(154, 281);
            labelTip.Name = "labelTip";
            labelTip.Size = new Size(230, 31);
            labelTip.TabIndex = 2;
            labelTip.Text = "按空格开始自动代打";
            // 
            // buttonStart
            // 
            buttonStart.Enabled = false;
            buttonStart.Location = new Point(306, 84);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(150, 46);
            buttonStart.TabIndex = 3;
            buttonStart.Text = "开始";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // 
            // buttonStop
            buttonStop.Enabled = false;
            buttonStop.Location = new Point(407, 213);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(150, 46);
            buttonStop.TabIndex = 4;
            buttonStop.Text = "停止";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;   // ⭐ 加上这一行

            // 
            // labelInfo
            // 
            labelInfo.AutoSize = true;
            labelInfo.Location = new Point(154, 118);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(134, 31);
            labelInfo.TabIndex = 5;
            labelInfo.Text = "未加载谱面";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelInfo);
            Controls.Add(buttonStop);
            Controls.Add(buttonStart);
            Controls.Add(labelTip);
            Controls.Add(textBoxPath);
            Controls.Add(buttonBrowse);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonBrowse;
        private TextBox textBoxPath;
        private Label labelTip;
        private Button buttonStart;
        private Button buttonStop;
        private Label labelInfo;
    }
}
