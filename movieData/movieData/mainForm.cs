using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace movieData
{
    public partial class mainForm : Form
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        public enum panelAct
        {
            newMovie = 0,
            mainForm = 1,
            viewMovie = 2,
            editMovie = 3,
            editSettings = 4,
        }

        private panelAct ap;

        private dataManager dm;

        private newMovie NewMovie;
        private searchMovie SearchMovie;
        private viewMovie ViewMovie;
        private editMovie EditMovie;
        private editSettings EditSettings;
        public mainForm()
        {
            InitializeComponent();

            try
            {
                if (!Directory.Exists("Errors"))
                {
                    Directory.CreateDirectory("Errors");
                }

                if (!Directory.Exists("Images"))
                {
                    Directory.CreateDirectory("Images");
                }

                if (!Directory.Exists("Images/Movies"))
                {
                    Directory.CreateDirectory("Images/Movies");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Error", err.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            ap = panelAct.mainForm;

            //Data manager
            dm = new dataManager();

            #region Create Forms
            NewMovie = new newMovie(dm);
            SearchMovie = new searchMovie(dm);
            ViewMovie = new viewMovie(dm);
            EditMovie = new editMovie(dm);
            EditSettings = new editSettings(dm);
            #endregion

            #region Setup Forms
            NewMovie.VerticalScroll.Enabled = false;
            ViewMovie.VerticalScroll.Enabled = false;
            EditMovie.VerticalScroll.Enabled = false;
            EditSettings.VerticalScroll.Enabled = false;
            SearchMovie.VerticalScroll.Enabled = false;
            mainPanel.VerticalScroll.Enabled = false;
            this.VerticalScroll.Enabled = false;
            NewMovie.Dock = DockStyle.Bottom;
            EditSettings.Dock = DockStyle.Bottom;
            ViewMovie.Dock = DockStyle.Bottom;
            EditMovie.Dock = DockStyle.Bottom;
            #endregion

            mainPanel.Controls.Add(SearchMovie);

            resizeEndEvent_CorrectSizes(null, null);
            

            #region Delegates
            EditMovie.deleteEntry += delegate(object sender, EventArgs e)
            {
                ap = panelAct.mainForm;
                toggleControls();
                newMovieButton.Text = "New Movie";
                EditMovie.clearAll();
                SearchMovie.searchButton_Click(null, null);
            };

            ViewMovie.actorEvent += delegate(object sender, EventArgs e)
            {
                ap = panelAct.mainForm;
                toggleControls();
                newMovieButton.Text = "New Movie";
                ViewMovie.clearAll();
                SearchMovie.actorSearch((string)sender);
            };

            ViewMovie.viewEdit += delegate(object sender, EventArgs e)
            {
                ap = panelAct.editMovie;
                toggleControls();
                newMovieButton.Text = "Back";
                ViewMovie.clearAll();
                EditMovie.editItem((simpleMovie)sender);
            };

            EditSettings.sendEdit += delegate(object sender, EventArgs e)
            {
                ap = panelAct.mainForm;
                toggleControls();

            };

            EditMovie.sendEdit += delegate(object sender, EventArgs e)
            {
                ap = panelAct.viewMovie;
                toggleControls();
                newMovieButton.Text = "Back";
                ViewMovie.viewItem((simpleMovie)sender);
            };

            SearchMovie.viewItem += delegate(object sender, MouseEventArgs e)
            {
                ap = panelAct.viewMovie;
                toggleControls();
                newMovieButton.Text = "Back";
                ViewMovie.viewItem((simpleMovie)sender);
            };

            NewMovie.movieAdded += delegate
            {
                newMovieButton_Click(null, null);
            };

            #endregion

            this.WindowState = FormWindowState.Maximized;

        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m); 
            if (m.Msg == 0x0112)
            {
                if (m.WParam == new IntPtr(0xF030))
                {
                    this.Height = Screen.FromControl(this).Bounds.Height;
                    this.Width = Screen.FromControl(this).Bounds.Width;
                    resizeEndEvent_CorrectSizes(null, null);
                }
                if (m.WParam == new IntPtr(0xF120))
                {
                    this.Height = 731;
                    this.Width = 1300;
                    resizeEndEvent_CorrectSizes(null, null);
                }
            }
            
        }


        private void toggleControls() //Current enum always reflects current set up
        {
            if (ap == panelAct.mainForm)
            {
                mainPanel.Controls.Remove(NewMovie);
                mainPanel.Controls.Remove(ViewMovie);
                mainPanel.Controls.Remove(EditMovie);
                mainPanel.Controls.Remove(EditSettings);
                mainPanel.Controls.Add(SearchMovie);
            }
            else if (ap == panelAct.newMovie)
            {
                mainPanel.Controls.Remove(SearchMovie);
                mainPanel.Controls.Remove(ViewMovie);
                mainPanel.Controls.Remove(EditSettings);
                mainPanel.Controls.Add(NewMovie);
            }
            else if (ap == panelAct.viewMovie)
            {
                settingsBtn.Hide();
                mainPanel.Controls.Remove(SearchMovie);
                mainPanel.Controls.Remove(EditMovie);
                mainPanel.Controls.Remove(EditSettings);
                mainPanel.Controls.Add(ViewMovie);
            }
            else if (ap == panelAct.editMovie)
            {
                settingsBtn.Hide();
                mainPanel.Controls.Remove(ViewMovie);
                mainPanel.Controls.Remove(EditSettings);
                mainPanel.Controls.Add(EditMovie);
            }
            else if (ap == panelAct.editSettings)
            {
                mainPanel.Controls.Remove(NewMovie);
                mainPanel.Controls.Remove(ViewMovie);
                mainPanel.Controls.Remove(EditMovie);
                mainPanel.Controls.Remove(SearchMovie);
                mainPanel.Controls.Add(EditSettings);
            }
        }

        private void newMovieButton_Click(object sender, EventArgs e) //New Movie button changes according to active panel
        {
            if (ap == panelAct.mainForm)
            {
                settingsBtn.Hide();
                newMovieButton.Text = "Cancel";
                ap = panelAct.newMovie;
                toggleControls();
            }
            else if(ap == panelAct.newMovie)
            {
                settingsBtn.Show();
                NewMovie.clearAll();
                newMovieButton.Text = "New Movie";
                ap = panelAct.mainForm;
                toggleControls();
            }
            else if (ap == panelAct.viewMovie)
            {
                settingsBtn.Show();
                newMovieButton.Text = "New Movie";
                ap = panelAct.mainForm;
                toggleControls();
            }
            else if (ap == panelAct.editMovie)
            {
                settingsBtn.Show();
                newMovieButton.Text = "New Movie";
                ap = panelAct.mainForm;
                toggleControls();
            }
            else if (ap == panelAct.editSettings)
            {
                settingsBtn.Show();
                newMovieButton.Text = "New Movie";
                ap = panelAct.mainForm;
                toggleControls();
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            ap = panelAct.editSettings;
            newMovieButton.Text = "Cancel";
            EditSettings.updateCount();
            toggleControls();
        }

        private void resizeEndEvent_CorrectSizes(object sender, EventArgs e)
        {
            //Adjust height by 50 to accomidate windows
            if (this.Height < 731)
            {
                this.Height = 731;
            }

            if (this.Width < 1300)
            {
                this.Width = 1300;
            }

            this.Height = (int)(this.Width / 1.77777777777778);
            mainPanel.Height = this.Height - 50;
            mainPanel.Width = this.Width - 120;
            mainPanel.Location = new Point(newMovieButton.Width + 5, 2);
            //SearchMovie.Size = mainPanel.Size;
            int x = mainPanel.Width-20;
            int y = mainPanel.Height-20;
            SearchMovie.resize(x,y);
            ViewMovie.resize(x, y);
            
            newMovieButton.Location = new Point(newMovieButton.Location.X, mainPanel.Size.Height - (newMovieButton.Size.Height * 2));
            settingsBtn.Location = new Point(newMovieButton.Location.X, newMovieButton.Location.Y - newMovieButton.Size.Height);
            mainPanel.VerticalScroll.Enabled = false;
            SearchMovie.VerticalScroll.Enabled = false;
        }
    }
}
