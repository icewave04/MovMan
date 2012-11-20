namespace movieData
{
    partial class mainForm
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.newMovieButton = new System.Windows.Forms.Button();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Location = new System.Drawing.Point(1, 111);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1803, 820);
            this.mainPanel.TabIndex = 0;
            // 
            // newMovieButton
            // 
            this.newMovieButton.Location = new System.Drawing.Point(12, 12);
            this.newMovieButton.Name = "newMovieButton";
            this.newMovieButton.Size = new System.Drawing.Size(80, 52);
            this.newMovieButton.TabIndex = 1;
            this.newMovieButton.Text = "New Movie";
            this.newMovieButton.UseVisualStyleBackColor = true;
            this.newMovieButton.Click += new System.EventHandler(this.newMovieButton_Click);
            // 
            // settingsBtn
            // 
            this.settingsBtn.Location = new System.Drawing.Point(98, 12);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(80, 52);
            this.settingsBtn.TabIndex = 2;
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.UseVisualStyleBackColor = true;
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1804, 941);
            this.Controls.Add(this.newMovieButton);
            this.Controls.Add(this.settingsBtn);
            this.Controls.Add(this.mainPanel);
            this.Name = "mainForm";
            this.Text = "Movie Manager - Almost Beta";
            this.ResizeEnd += new System.EventHandler(this.resizeEndEvent_CorrectSizes);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Button newMovieButton;
        private System.Windows.Forms.Button settingsBtn;
    }
}

