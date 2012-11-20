namespace movieData
{
    partial class editSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openL = new System.Windows.Forms.Label();
            this.openWithDir = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.stats = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openL
            // 
            this.openL.AutoSize = true;
            this.openL.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.openL.Location = new System.Drawing.Point(17, 20);
            this.openL.Name = "openL";
            this.openL.Size = new System.Drawing.Size(196, 26);
            this.openL.TabIndex = 0;
            this.openL.Text = "Open Movies With:";
            // 
            // openWithDir
            // 
            this.openWithDir.AutoSize = true;
            this.openWithDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.openWithDir.Location = new System.Drawing.Point(139, 20);
            this.openWithDir.Name = "openWithDir";
            this.openWithDir.Size = new System.Drawing.Size(70, 26);
            this.openWithDir.TabIndex = 1;
            this.openWithDir.Text = "label1";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.button1.Location = new System.Drawing.Point(22, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.button2.Location = new System.Drawing.Point(20, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "Commit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(139, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tested Programs: VLC, Windows Media Player ";
            // 
            // stats
            // 
            this.stats.AutoSize = true;
            this.stats.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.stats.Location = new System.Drawing.Point(17, 359);
            this.stats.Name = "stats";
            this.stats.Size = new System.Drawing.Size(62, 26);
            this.stats.TabIndex = 5;
            this.stats.Text = "Stats";
            // 
            // editSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.stats);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.openWithDir);
            this.Controls.Add(this.openL);
            this.Name = "editSettings";
            this.Size = new System.Drawing.Size(1109, 385);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label openL;
        private System.Windows.Forms.Label openWithDir;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label stats;
    }
}
