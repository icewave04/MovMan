namespace movieData
{
    partial class searchMovie
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
            this.searchButton = new System.Windows.Forms.Button();
            this.searchT = new System.Windows.Forms.TextBox();
            this.resultTree = new System.Windows.Forms.TreeView();
            this.resultInfoT = new System.Windows.Forms.TextBox();
            this.searchCB = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(959, 747);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            this.searchButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.doSearch);
            // 
            // searchT
            // 
            this.searchT.Location = new System.Drawing.Point(3, 749);
            this.searchT.Name = "searchT";
            this.searchT.Size = new System.Drawing.Size(950, 20);
            this.searchT.TabIndex = 2;
            this.searchT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.enterPress);
            // 
            // resultTree
            // 
            this.resultTree.BackColor = System.Drawing.SystemColors.Control;
            this.resultTree.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultTree.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.resultTree.Location = new System.Drawing.Point(13, 14);
            this.resultTree.Name = "resultTree";
            this.resultTree.Size = new System.Drawing.Size(487, 543);
            this.resultTree.TabIndex = 3;
            this.resultTree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.selectItem);
            // 
            // resultInfoT
            // 
            this.resultInfoT.BackColor = System.Drawing.SystemColors.Control;
            this.resultInfoT.Location = new System.Drawing.Point(823, 14);
            this.resultInfoT.Multiline = true;
            this.resultInfoT.Name = "resultInfoT";
            this.resultInfoT.ReadOnly = true;
            this.resultInfoT.Size = new System.Drawing.Size(367, 543);
            this.resultInfoT.TabIndex = 6;
            // 
            // searchCB
            // 
            this.searchCB.FormattingEnabled = true;
            this.searchCB.Items.AddRange(new object[] {
            "Actor Only Search",
            "Specific Actor Only Search",
            "Detailed Search",
            "Advanced Search"});
            this.searchCB.Location = new System.Drawing.Point(1040, 705);
            this.searchCB.Name = "searchCB";
            this.searchCB.Size = new System.Drawing.Size(150, 64);
            this.searchCB.TabIndex = 9;
            this.searchCB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prompt);
            // 
            // searchMovie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchCB);
            this.Controls.Add(this.resultInfoT);
            this.Controls.Add(this.resultTree);
            this.Controls.Add(this.searchT);
            this.Controls.Add(this.searchButton);
            this.Name = "searchMovie";
            this.Size = new System.Drawing.Size(1219, 780);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox searchT;
        private System.Windows.Forms.TreeView resultTree;
        private System.Windows.Forms.TextBox resultInfoT;
        private System.Windows.Forms.CheckedListBox searchCB;
    }
}
