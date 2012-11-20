namespace movieData
{
    partial class viewMovie
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
            this.titleL = new System.Windows.Forms.Label();
            this.fileL = new System.Windows.Forms.Label();
            this.genreL = new System.Windows.Forms.Label();
            this.ratingL = new System.Windows.Forms.Label();
            this.yearL = new System.Windows.Forms.Label();
            this.imageL = new System.Windows.Forms.Label();
            this.actors = new System.Windows.Forms.ListBox();
            this.description = new System.Windows.Forms.RichTextBox();
            this.imageP = new System.Windows.Forms.PictureBox();
            this.editMovieBtn = new System.Windows.Forms.Button();
            this.vlcBtn = new System.Windows.Forms.Button();
            this.moviePanel = new System.Windows.Forms.Panel();
            this.secondsIn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.imageP)).BeginInit();
            this.SuspendLayout();
            // 
            // titleL
            // 
            this.titleL.AutoSize = true;
            this.titleL.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleL.Location = new System.Drawing.Point(7, 14);
            this.titleL.Name = "titleL";
            this.titleL.Size = new System.Drawing.Size(118, 42);
            this.titleL.TabIndex = 0;
            this.titleL.Text = "label1";
            // 
            // fileL
            // 
            this.fileL.AutoSize = true;
            this.fileL.Location = new System.Drawing.Point(11, 579);
            this.fileL.Name = "fileL";
            this.fileL.Size = new System.Drawing.Size(35, 13);
            this.fileL.TabIndex = 1;
            this.fileL.Text = "label2";
            // 
            // genreL
            // 
            this.genreL.AutoSize = true;
            this.genreL.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.genreL.Location = new System.Drawing.Point(7, 88);
            this.genreL.Name = "genreL";
            this.genreL.Size = new System.Drawing.Size(79, 29);
            this.genreL.TabIndex = 2;
            this.genreL.Text = "label3";
            // 
            // ratingL
            // 
            this.ratingL.AutoSize = true;
            this.ratingL.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.ratingL.Location = new System.Drawing.Point(7, 101);
            this.ratingL.Name = "ratingL";
            this.ratingL.Size = new System.Drawing.Size(79, 29);
            this.ratingL.TabIndex = 3;
            this.ratingL.Text = "label4";
            // 
            // yearL
            // 
            this.yearL.AutoSize = true;
            this.yearL.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.yearL.Location = new System.Drawing.Point(7, 114);
            this.yearL.Name = "yearL";
            this.yearL.Size = new System.Drawing.Size(79, 29);
            this.yearL.TabIndex = 4;
            this.yearL.Text = "label5";
            // 
            // imageL
            // 
            this.imageL.AutoSize = true;
            this.imageL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.imageL.Location = new System.Drawing.Point(7, 127);
            this.imageL.Name = "imageL";
            this.imageL.Size = new System.Drawing.Size(60, 24);
            this.imageL.TabIndex = 5;
            this.imageL.Text = "label6";
            // 
            // actors
            // 
            this.actors.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.actors.FormattingEnabled = true;
            this.actors.ItemHeight = 24;
            this.actors.Location = new System.Drawing.Point(10, 154);
            this.actors.Name = "actors";
            this.actors.Size = new System.Drawing.Size(243, 244);
            this.actors.TabIndex = 6;
            this.actors.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.searchActor);
            // 
            // description
            // 
            this.description.BackColor = System.Drawing.SystemColors.Control;
            this.description.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.description.Location = new System.Drawing.Point(10, 422);
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Size = new System.Drawing.Size(577, 154);
            this.description.TabIndex = 7;
            this.description.Text = "";
            // 
            // imageP
            // 
            this.imageP.Location = new System.Drawing.Point(654, 0);
            this.imageP.Name = "imageP";
            this.imageP.Size = new System.Drawing.Size(966, 777);
            this.imageP.TabIndex = 8;
            this.imageP.TabStop = false;
            // 
            // editMovieBtn
            // 
            this.editMovieBtn.Location = new System.Drawing.Point(10, 608);
            this.editMovieBtn.Name = "editMovieBtn";
            this.editMovieBtn.Size = new System.Drawing.Size(75, 47);
            this.editMovieBtn.TabIndex = 9;
            this.editMovieBtn.Text = "Edit Movie";
            this.editMovieBtn.UseVisualStyleBackColor = true;
            this.editMovieBtn.Click += new System.EventHandler(this.editMovieBtn_Click);
            // 
            // vlcBtn
            // 
            this.vlcBtn.Location = new System.Drawing.Point(512, 608);
            this.vlcBtn.Name = "vlcBtn";
            this.vlcBtn.Size = new System.Drawing.Size(75, 47);
            this.vlcBtn.TabIndex = 10;
            this.vlcBtn.Text = "Play";
            this.vlcBtn.UseVisualStyleBackColor = true;
            this.vlcBtn.Click += new System.EventHandler(this.vlcBtn_Click);
            // 
            // moviePanel
            // 
            this.moviePanel.Location = new System.Drawing.Point(654, 0);
            this.moviePanel.Name = "moviePanel";
            this.moviePanel.Size = new System.Drawing.Size(966, 777);
            this.moviePanel.TabIndex = 11;
            // 
            // secondsIn
            // 
            this.secondsIn.AutoSize = true;
            this.secondsIn.Location = new System.Drawing.Point(11, 592);
            this.secondsIn.Name = "secondsIn";
            this.secondsIn.Size = new System.Drawing.Size(35, 13);
            this.secondsIn.TabIndex = 12;
            this.secondsIn.Text = "label1";
            // 
            // viewMovie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.secondsIn);
            this.Controls.Add(this.moviePanel);
            this.Controls.Add(this.vlcBtn);
            this.Controls.Add(this.editMovieBtn);
            this.Controls.Add(this.imageP);
            this.Controls.Add(this.description);
            this.Controls.Add(this.actors);
            this.Controls.Add(this.imageL);
            this.Controls.Add(this.yearL);
            this.Controls.Add(this.ratingL);
            this.Controls.Add(this.genreL);
            this.Controls.Add(this.fileL);
            this.Controls.Add(this.titleL);
            this.Name = "viewMovie";
            this.Size = new System.Drawing.Size(1620, 780);
            ((System.ComponentModel.ISupportInitialize)(this.imageP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleL;
        private System.Windows.Forms.Label fileL;
        private System.Windows.Forms.Label genreL;
        private System.Windows.Forms.Label ratingL;
        private System.Windows.Forms.Label yearL;
        private System.Windows.Forms.Label imageL;
        private System.Windows.Forms.ListBox actors;
        private System.Windows.Forms.RichTextBox description;
        private System.Windows.Forms.PictureBox imageP;
        private System.Windows.Forms.Button editMovieBtn;
        private System.Windows.Forms.Button vlcBtn;
        private System.Windows.Forms.Panel moviePanel;
        private System.Windows.Forms.Label secondsIn;


    }
}
