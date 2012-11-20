using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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

        private panelAct currentPanel;
        private panelAct lastPanel;

        private dataManager DataManager;

        private newMovie NewMovie;
        private searchMovie SearchMovie;
        private viewMovie ViewMovie;
        private editMovie EditMovie;
        private editSettings EditSettings;

        private float ratioX;
        private float ratioY;

        public mainForm()
        {
            InitializeComponent();
            //Maximize Window and adjust sizes
            this.WindowState = FormWindowState.Maximized;
            ratioX = (1700 / Screen.FromControl(this).WorkingArea.Width);
            ratioY = (820 / Screen.FromControl(this).WorkingArea.Height);

            //panel enums, used to control what panel is active or was active
            currentPanel = panelAct.mainForm;
            lastPanel = panelAct.editSettings;
            //Data manager
            DataManager = new dataManager();

            #region Create Forms
            NewMovie = new newMovie(DataManager);
            SearchMovie = new searchMovie(DataManager);
            ViewMovie = new viewMovie(DataManager);
            EditMovie = new editMovie(DataManager);
            EditSettings = new editSettings(DataManager);
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

            
            

            #region Delegates
            EditMovie.deleteEntry += delegate(object sender, EventArgs e) //EditMovie>Delete Button
            {
                toggleControls(panelAct.mainForm);
                newMovieButton.Text = "New Movie";
                EditMovie.clearAll();
                SearchMovie.searchButton_Click(null, null);
            };

            ViewMovie.actorEvent += delegate(object sender, EventArgs e) //ViewMovie>Double Click Actor
            {
                SearchMovie.actorSearch((string)sender);
                toggleControls(panelAct.mainForm);
                newMovieButton.Text = "New Movie";
                ViewMovie.clearAll();
                
            };

            ViewMovie.viewEdit += delegate(object sender, EventArgs e) //ViewMovie>Edit Button
            {
                EditMovie.editItem((info)sender);
                toggleControls(panelAct.editMovie);
            };

            EditSettings.sendEdit += delegate(object sender, EventArgs e) //EditSettings>Commit Button
            {
                
                toggleControls(lastPanel);

            };

            EditMovie.sendEdit += delegate(object sender, EventArgs e) //EditMovie>Send Edit Button
            {
                ViewMovie.viewItem((info)sender);
                toggleControls(panelAct.viewMovie);
                newMovieButton.Text = "Back";
                
            };

            SearchMovie.viewItem += delegate(object sender, MouseEventArgs e) //SearchMovie>Double Click Title
            {
                ViewMovie.viewItem((info)sender);
                toggleControls(panelAct.viewMovie);
                newMovieButton.Text = "Back";
                
            };

            NewMovie.movieAdded += delegate //NewMovie>Movie Was Added Statement (Called from Add Movie Button)
            {
                newMovieButton_Click(null, null);
            };

            #endregion

            //Stage is set, this adjusts sizes of all panels that require resizing
            resizeEndEvent_CorrectSizes(null, null);
        }

        //This override method handles window maximise and restore as these are system calls
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


        private void toggleControls(panelAct current) //Current enum always reflects current set up
        {
            if (currentPanel != current) //Check if panel has actually changed
            {
                lastPanel = currentPanel;
                currentPanel = current;

                //Remove the last panel
                if (lastPanel == panelAct.editMovie)
                {
                    mainPanel.Controls.Remove(EditMovie);
                }
                else if (lastPanel == panelAct.editSettings)
                {
                    mainPanel.Controls.Remove(EditSettings);
                }
                else if (lastPanel == panelAct.mainForm)
                {
                    mainPanel.Controls.Remove(SearchMovie);
                }
                else if (lastPanel == panelAct.newMovie)
                {
                    mainPanel.Controls.Remove(NewMovie);
                }
                else if (lastPanel == panelAct.viewMovie)
                {
                    mainPanel.Controls.Remove(ViewMovie);
                }

                //Add the new panel
                if (currentPanel == panelAct.editMovie)
                {
                    mainPanel.Controls.Add(EditMovie);
                    newMovieButton.Text = "Cancel";
                }
                else if (currentPanel == panelAct.editSettings)
                {
                    mainPanel.Controls.Add(EditSettings);
                    newMovieButton.Text = "Cancel";
                }
                else if (currentPanel == panelAct.mainForm)
                {
                    mainPanel.Controls.Add(SearchMovie);
                    newMovieButton.Text = "New Movie";
                }
                else if (currentPanel == panelAct.newMovie)
                {
                    mainPanel.Controls.Add(NewMovie);
                    newMovieButton.Text = "Search";
                }
                else if (currentPanel == panelAct.viewMovie)
                {
                    mainPanel.Controls.Add(ViewMovie);
                    newMovieButton.Text = "Back";
                }
            }
        }

        private void newMovieButton_Click(object sender, EventArgs e) //New Movie button changes according to active panel
        {
            //Check that current is viewMovie and last is edit (to prevent locks) or currentPanel = lastPanel (also to prevent locks) or if currentPanel == viewMovie
            if ((currentPanel == panelAct.viewMovie && lastPanel == panelAct.editMovie) || currentPanel == lastPanel || currentPanel == panelAct.viewMovie)
            {
                toggleControls(panelAct.mainForm); //Always return to main if any of the above are satisfied
            }
            else if (currentPanel == panelAct.editMovie)
            {
                toggleControls(panelAct.viewMovie); //You can only edit via the view movie panel
            }
            else if (currentPanel == panelAct.newMovie) //New Movie button is a "Back" button while on the new movie panel
            {
                toggleControls(panelAct.mainForm);
            }
            else if (currentPanel != panelAct.mainForm)
            {
                toggleControls(lastPanel); //All other panels can just go back
            }
            else
            {
                toggleControls(panelAct.newMovie); //All other cases can go to the new movie panel (should only occur on the main panel)
            }
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            EditSettings.updateCount();
            toggleControls(panelAct.editSettings);
        }

        private void resizeEndEvent_CorrectSizes(object sender, EventArgs e)
        {
            
            //Adjust height by 50 to accomidate windows
            /*
            if (this.Height < 820)
            {
                this.Height = 820;
            }
            */

            if (this.Width < 1450)
            {
                this.Width = 1450;
            }

            this.Height = (int)(this.Width / 1.77777777777778);


            mainPanel.Height = (int)(this.Height * 0.88);
            mainPanel.Width = this.Width;
            mainPanel.Location = new Point(0, newMovieButton.Height+20);
            //Debug Main Panel Color
            //mainPanel.BackColor = Color.Black;
            //SearchMovie.Size = mainPanel.Size;
            int x = mainPanel.Width-20;
            int y = mainPanel.Height-20;
            SearchMovie.resize(x,y);
            ViewMovie.resize(x, y);
            
            //newMovieButton.Location = new Point(newMovieButton.Location.X, mainPanel.Size.Height - (newMovieButton.Size.Height * 2));
            //settingsBtn.Location = new Point(newMovieButton.Location.X, newMovieButton.Location.Y - newMovieButton.Size.Height);
            mainPanel.VerticalScroll.Enabled = false;
            SearchMovie.VerticalScroll.Enabled = false;
        }
    }
}
