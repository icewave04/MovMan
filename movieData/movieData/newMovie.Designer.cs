namespace movieData
{
    partial class newMovie
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
            this.descriptionL = new System.Windows.Forms.Label();
            this.actorsL = new System.Windows.Forms.Label();
            this.titleT = new System.Windows.Forms.TextBox();
            this.fileLocationT = new System.Windows.Forms.TextBox();
            this.yearT = new System.Windows.Forms.TextBox();
            this.imageT = new System.Windows.Forms.TextBox();
            this.actorsT = new System.Windows.Forms.TextBox();
            this.descriptionT = new System.Windows.Forms.TextBox();
            this.actorListBox = new System.Windows.Forms.ListBox();
            this.arrow = new System.Windows.Forms.Label();
            this.addActor = new System.Windows.Forms.Button();
            this.removeActor = new System.Windows.Forms.Button();
            this.fileBrowse = new System.Windows.Forms.Button();
            this.imageBrowse = new System.Windows.Forms.Button();
            this.genreCB = new System.Windows.Forms.ComboBox();
            this.ratingCB = new System.Windows.Forms.ComboBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.autofill = new System.Windows.Forms.Button();
            this.imageSearchBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // titleL
            // 
            this.titleL.AutoSize = true;
            this.titleL.Location = new System.Drawing.Point(76, 4);
            this.titleL.Name = "titleL";
            this.titleL.Size = new System.Drawing.Size(30, 13);
            this.titleL.TabIndex = 0;
            this.titleL.Text = "Title:";
            // 
            // fileL
            // 
            this.fileL.AutoSize = true;
            this.fileL.Location = new System.Drawing.Point(36, 25);
            this.fileL.Name = "fileL";
            this.fileL.Size = new System.Drawing.Size(70, 13);
            this.fileL.TabIndex = 1;
            this.fileL.Text = "File Location:";
            // 
            // genreL
            // 
            this.genreL.AutoSize = true;
            this.genreL.Location = new System.Drawing.Point(67, 46);
            this.genreL.Name = "genreL";
            this.genreL.Size = new System.Drawing.Size(39, 13);
            this.genreL.TabIndex = 2;
            this.genreL.Text = "Genre:";
            // 
            // ratingL
            // 
            this.ratingL.AutoSize = true;
            this.ratingL.Location = new System.Drawing.Point(65, 67);
            this.ratingL.Name = "ratingL";
            this.ratingL.Size = new System.Drawing.Size(41, 13);
            this.ratingL.TabIndex = 3;
            this.ratingL.Text = "Rating:";
            // 
            // yearL
            // 
            this.yearL.AutoSize = true;
            this.yearL.Location = new System.Drawing.Point(71, 88);
            this.yearL.Name = "yearL";
            this.yearL.Size = new System.Drawing.Size(32, 13);
            this.yearL.TabIndex = 4;
            this.yearL.Text = "Year:";
            // 
            // imageL
            // 
            this.imageL.AutoSize = true;
            this.imageL.Location = new System.Drawing.Point(64, 109);
            this.imageL.Name = "imageL";
            this.imageL.Size = new System.Drawing.Size(39, 13);
            this.imageL.TabIndex = 5;
            this.imageL.Text = "Image:";
            // 
            // descriptionL
            // 
            this.descriptionL.AutoSize = true;
            this.descriptionL.Location = new System.Drawing.Point(43, 275);
            this.descriptionL.Name = "descriptionL";
            this.descriptionL.Size = new System.Drawing.Size(63, 13);
            this.descriptionL.TabIndex = 6;
            this.descriptionL.Text = "Description:";
            // 
            // actorsL
            // 
            this.actorsL.AutoSize = true;
            this.actorsL.Location = new System.Drawing.Point(63, 139);
            this.actorsL.Name = "actorsL";
            this.actorsL.Size = new System.Drawing.Size(40, 13);
            this.actorsL.TabIndex = 7;
            this.actorsL.Text = "Actors:";
            // 
            // titleT
            // 
            this.titleT.Location = new System.Drawing.Point(109, 0);
            this.titleT.Name = "titleT";
            this.titleT.Size = new System.Drawing.Size(163, 20);
            this.titleT.TabIndex = 8;
            // 
            // fileLocationT
            // 
            this.fileLocationT.Location = new System.Drawing.Point(109, 22);
            this.fileLocationT.Name = "fileLocationT";
            this.fileLocationT.Size = new System.Drawing.Size(502, 20);
            this.fileLocationT.TabIndex = 9;
            // 
            // yearT
            // 
            this.yearT.Location = new System.Drawing.Point(109, 85);
            this.yearT.Name = "yearT";
            this.yearT.Size = new System.Drawing.Size(75, 20);
            this.yearT.TabIndex = 10;
            // 
            // imageT
            // 
            this.imageT.Location = new System.Drawing.Point(109, 106);
            this.imageT.Name = "imageT";
            this.imageT.Size = new System.Drawing.Size(505, 20);
            this.imageT.TabIndex = 11;
            // 
            // actorsT
            // 
            this.actorsT.Location = new System.Drawing.Point(109, 132);
            this.actorsT.Name = "actorsT";
            this.actorsT.Size = new System.Drawing.Size(212, 20);
            this.actorsT.TabIndex = 12;
            this.actorsT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.pressEnter);
            // 
            // descriptionT
            // 
            this.descriptionT.Location = new System.Drawing.Point(109, 275);
            this.descriptionT.Multiline = true;
            this.descriptionT.Name = "descriptionT";
            this.descriptionT.Size = new System.Drawing.Size(505, 210);
            this.descriptionT.TabIndex = 13;
            // 
            // actorListBox
            // 
            this.actorListBox.FormattingEnabled = true;
            this.actorListBox.Location = new System.Drawing.Point(404, 132);
            this.actorListBox.Name = "actorListBox";
            this.actorListBox.Size = new System.Drawing.Size(210, 121);
            this.actorListBox.TabIndex = 14;
            // 
            // arrow
            // 
            this.arrow.AutoSize = true;
            this.arrow.Location = new System.Drawing.Point(343, 139);
            this.arrow.Name = "arrow";
            this.arrow.Size = new System.Drawing.Size(16, 13);
            this.arrow.TabIndex = 15;
            this.arrow.Text = "->";
            // 
            // addActor
            // 
            this.addActor.Location = new System.Drawing.Point(109, 167);
            this.addActor.Name = "addActor";
            this.addActor.Size = new System.Drawing.Size(75, 23);
            this.addActor.TabIndex = 16;
            this.addActor.Text = "Add";
            this.addActor.UseVisualStyleBackColor = true;
            this.addActor.Click += new System.EventHandler(this.addActor_Click);
            // 
            // removeActor
            // 
            this.removeActor.Location = new System.Drawing.Point(246, 167);
            this.removeActor.Name = "removeActor";
            this.removeActor.Size = new System.Drawing.Size(75, 23);
            this.removeActor.TabIndex = 17;
            this.removeActor.Text = "Remove";
            this.removeActor.UseVisualStyleBackColor = true;
            this.removeActor.Click += new System.EventHandler(this.removeActor_Click);
            // 
            // fileBrowse
            // 
            this.fileBrowse.Location = new System.Drawing.Point(641, 20);
            this.fileBrowse.Name = "fileBrowse";
            this.fileBrowse.Size = new System.Drawing.Size(75, 23);
            this.fileBrowse.TabIndex = 18;
            this.fileBrowse.Text = "Browse";
            this.fileBrowse.UseVisualStyleBackColor = true;
            this.fileBrowse.Click += new System.EventHandler(this.fileBrowse_Click);
            // 
            // imageBrowse
            // 
            this.imageBrowse.Location = new System.Drawing.Point(641, 104);
            this.imageBrowse.Name = "imageBrowse";
            this.imageBrowse.Size = new System.Drawing.Size(75, 23);
            this.imageBrowse.TabIndex = 19;
            this.imageBrowse.Text = "Browse";
            this.imageBrowse.UseVisualStyleBackColor = true;
            this.imageBrowse.Click += new System.EventHandler(this.imageBrowse_Click);
            // 
            // genreCB
            // 
            this.genreCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.genreCB.FormattingEnabled = true;
            this.genreCB.Location = new System.Drawing.Point(109, 43);
            this.genreCB.Name = "genreCB";
            this.genreCB.Size = new System.Drawing.Size(163, 21);
            this.genreCB.TabIndex = 20;
            // 
            // ratingCB
            // 
            this.ratingCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ratingCB.FormattingEnabled = true;
            this.ratingCB.Location = new System.Drawing.Point(109, 64);
            this.ratingCB.Name = "ratingCB";
            this.ratingCB.Size = new System.Drawing.Size(163, 21);
            this.ratingCB.TabIndex = 21;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(197, 491);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 22;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(109, 491);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 23;
            this.addButton.Text = "Add Movie";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // autofill
            // 
            this.autofill.Location = new System.Drawing.Point(539, 491);
            this.autofill.Name = "autofill";
            this.autofill.Size = new System.Drawing.Size(75, 23);
            this.autofill.TabIndex = 24;
            this.autofill.Text = "Auto Fill";
            this.autofill.UseVisualStyleBackColor = true;
            this.autofill.Click += new System.EventHandler(this.autofill_Click);
            // 
            // imageSearchBtn
            // 
            this.imageSearchBtn.Location = new System.Drawing.Point(446, 491);
            this.imageSearchBtn.Name = "imageSearchBtn";
            this.imageSearchBtn.Size = new System.Drawing.Size(87, 23);
            this.imageSearchBtn.TabIndex = 25;
            this.imageSearchBtn.Text = "Image Search";
            this.imageSearchBtn.UseVisualStyleBackColor = true;
            this.imageSearchBtn.Click += new System.EventHandler(this.imageSearchBtn_Click);
            // 
            // newMovie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageSearchBtn);
            this.Controls.Add(this.autofill);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.ratingCB);
            this.Controls.Add(this.genreCB);
            this.Controls.Add(this.imageBrowse);
            this.Controls.Add(this.fileBrowse);
            this.Controls.Add(this.removeActor);
            this.Controls.Add(this.addActor);
            this.Controls.Add(this.arrow);
            this.Controls.Add(this.actorListBox);
            this.Controls.Add(this.descriptionT);
            this.Controls.Add(this.actorsT);
            this.Controls.Add(this.imageT);
            this.Controls.Add(this.yearT);
            this.Controls.Add(this.fileLocationT);
            this.Controls.Add(this.titleT);
            this.Controls.Add(this.actorsL);
            this.Controls.Add(this.descriptionL);
            this.Controls.Add(this.imageL);
            this.Controls.Add(this.yearL);
            this.Controls.Add(this.ratingL);
            this.Controls.Add(this.genreL);
            this.Controls.Add(this.fileL);
            this.Controls.Add(this.titleL);
            this.Name = "newMovie";
            this.Size = new System.Drawing.Size(1620, 533);
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
        private System.Windows.Forms.Label descriptionL;
        private System.Windows.Forms.Label actorsL;
        private System.Windows.Forms.TextBox titleT;
        private System.Windows.Forms.TextBox fileLocationT;
        private System.Windows.Forms.TextBox yearT;
        private System.Windows.Forms.TextBox imageT;
        private System.Windows.Forms.TextBox actorsT;
        private System.Windows.Forms.TextBox descriptionT;
        private System.Windows.Forms.ListBox actorListBox;
        private System.Windows.Forms.Label arrow;
        private System.Windows.Forms.Button addActor;
        private System.Windows.Forms.Button removeActor;
        private System.Windows.Forms.Button fileBrowse;
        private System.Windows.Forms.Button imageBrowse;
        private System.Windows.Forms.ComboBox genreCB;
        private System.Windows.Forms.ComboBox ratingCB;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button autofill;
        private System.Windows.Forms.Button imageSearchBtn;
    }
}
