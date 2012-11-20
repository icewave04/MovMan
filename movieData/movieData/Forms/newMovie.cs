using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;

namespace movieData
{
    public partial class newMovie : UserControl
    {
        private List<string> actors;
        private dataManager DataManager;
        public EventHandler movieAdded = null;
        public EventHandler autoFill = null;
        private Thread autoFiller;
        private Imdb imdb;
        private Thread thisThread;

        
        public newMovie(dataManager DataManager)
        {
            InitializeComponent();
            this.DataManager = DataManager;
            actors = new List<string>();
            genreCB.DataSource = Enum.GetValues(typeof(genreEnum));
            genreCB.SelectedItem = genreEnum.Unknown;
            ratingCB.DataSource = Enum.GetValues(typeof(ratingEnum));
            ratingCB.SelectedItem = ratingEnum.Unknown;
            thisThread = Thread.CurrentThread;
        }

        private void fileBrowse_Click(object sender, EventArgs e)
        {
            string fname = String.Empty;
            OpenFileDialog odf = new OpenFileDialog();
            odf.InitialDirectory = Directory.GetCurrentDirectory();
            odf.Filter = "*.avi;*.mp4;*.mpeg;*.mkv|*.avi;*.mp4;*.mpeg;*.mkv|All Files|*.*";

            DialogResult result;
            result = odf.ShowDialog();

            if (result == DialogResult.OK)
            {
                fname = odf.FileName;
            }
            fileLocationT.Text = fname;
        }

        private void imageBrowse_Click(object sender, EventArgs e)
        {
            string fname = String.Empty;
            OpenFileDialog odf = new OpenFileDialog();
            odf.InitialDirectory = Directory.GetCurrentDirectory();
            odf.Filter = "*.jpg;*.jpeg;*.gif;*.png;|*.jpg;*.jpeg;*.gif;*.png;|All Files|*.*";

            DialogResult result;
            result = odf.ShowDialog();

            if (result == DialogResult.OK)
            {
                fname = odf.FileName;
            }
            imageT.Text = fname;
        }

        private bool imageDownload()
        {
            try
            {
                using (WebClient Client = new WebClient())
                {
                    Client.DownloadFile(imageT.Text.Trim(), String.Format(@"images\movies\{0}", String.Format("{0}{1}{2}", titleT.Text.Trim().Replace(" ", ""), yearT.Text.Trim(), genreCB.Text)));
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        private void addActor_Click(object sender, EventArgs e)
        {
            if (actorsT.Text.Trim() != "")
            {
                actors.Add(actorsT.Text.Trim());
                actorsT.Text = "";
                actorListBox.DataSource = null;
                actorListBox.DataSource = actors;
            }
        }

        private void removeActor_Click(object sender, EventArgs e)
        {
            int index = actorListBox.SelectedIndex;

            try
            {
                actors.RemoveAt(index);
            }
            catch
            {
            }

            actorListBox.DataSource = null;
            actorListBox.DataSource = actors;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            clearAll();
        }

        public void clearAll()
        {
            titleT.Text = "";
            fileLocationT.Text = "";
            yearT.Text = "";
            imageT.Text = "";
            descriptionT.Text = "";
            actors.RemoveRange(0, actors.Count);
            actorListBox.DataSource = null;
            actorListBox.DataSource = actors;
            actorsT.Text = "";
            genreCB.SelectedItem = genreEnum.Unknown;
            ratingCB.SelectedItem = ratingEnum.Unknown;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            string result = String.Empty;
            if (titleT.Text.Trim() != "")
            {
                if (fileLocationT.Text.Trim() != "")
                {
                    int x = -1;
                    Int32.TryParse(yearT.Text.Trim(), out x);
                    if (imageDownload())
                    {
                        if (DataManager.addMovie(titleT.Text.Trim(), (genreEnum)genreCB.SelectedIndex, (ratingEnum)ratingCB.SelectedIndex, fileLocationT.Text.Trim(), x, String.Format(@"images\movies\{0}", String.Format("{0}{1}{2}", titleT.Text.Trim().Replace(" ", ""), yearT.Text.Trim(), genreCB.Text)), descriptionT.Text.Trim(), actors, out result))
                        {
                            movieAdded(this, new EventArgs());
                        }
                    }
                }
            }
        }

        private void pressEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                addActor_Click(this, new EventArgs());
                e.Handled = true;
            }
        }

        private void autofill_Click(object sender, EventArgs e)
        {
            if (autoFiller == null || !autoFiller.IsAlive)
            {
                autoFiller = new Thread(autoFillThread);
                autoFiller.SetApartmentState(ApartmentState.STA);
                
                autoFiller.Start();
            }
        }


        private void autoFillThreadValueUpdate()
        {
            titleT.Text = imdb.Title;
            genreCB.SelectedItem = (genreEnum)Enum.Parse(typeof(genreEnum), Regex.Replace(imdb.Genre, @"\W", ""));
            yearT.Text = imdb.Year;
            descriptionT.Text = imdb.Plot;
            actors = imdb.getActors;
            actorListBox.DataSource = null;
            actorListBox.DataSource = actors;
        }

        void autoFillThread()
        {
            string query = titleT.Text;
            if (query != "")
            {
                imdb = null;
                imdb = new Imdb();
                imdb.Search(query);

                this.Invoke((MethodInvoker)delegate
                {
                    autoFillThreadValueUpdate();
                });
            }
        }

        private void imageSearchBtn_Click(object sender, EventArgs e)
        {
            if (titleT.Text.Trim() != "")
            {
                string target = String.Format(@"http://images.google.com/images?q={0}%20{1}&tbs=isz:l", this.titleT.Text.Trim().Replace(" ", "%20"), "front%20cover");
                Process.Start(target); //Lauches default browser
            }

        }

    }
}
