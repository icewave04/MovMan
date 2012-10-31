using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace movieData
{
    public partial class editMovie : UserControl
    {
        private List<string> actors;
        private dataManager dm;
        public EventHandler sendEdit = null;
        public EventHandler deleteEntry = null;
        private data movieData;
        private simpleMovie selected;
        private int index;

        public editMovie(dataManager dm)
        {
            InitializeComponent();
            this.dm = dm;
            genreCB.DataSource = Enum.GetValues(typeof(genreEnum));
            ratingCB.DataSource = Enum.GetValues(typeof(ratingEnum));
        }

        public void editItem(simpleMovie editMovie)
        {
            this.index = dm.getIndexOf(editMovie);
            this.selected = dm.getMovie(index);
            titleT.Text = editMovie.getTitle();
            fileLocationT.Text = editMovie.File;
            genreCB.SelectedIndex = (int)editMovie.Genre;
            ratingCB.SelectedIndex = (int)editMovie.Rating;
            movieData = dm.getdata(editMovie.File);
            yearT.Text = movieData.Year.ToString();
            imageT.Text = movieData.Image;
            actors = movieData.getActors();
            actorListBox.DataSource = actors;
            descriptionT.Text = movieData.Description.Replace("\n", "\r\n");
            if (movieData.Image != "")
            {
                    try
                    {
                        Image image = Image.FromFile(@movieData.Image);
                        Rectangle newRect = ImageHandling.GetScaledRectangle(image, imageP.ClientRectangle);
                        imageP.MaximumSize = imageP.Size;


                        imageP.Image = ImageHandling.GetResizedImage(image, newRect);

                    }
                    catch (ArgumentException ae)
                    {
                        dm.ge.GlobalTryCatch(ae, movieData.Image);
                    }
                    catch (FileNotFoundException fnfe)
                    {
                        dm.ge.GlobalTryCatch(fnfe, movieData.Image);
                    }
                }
                else
                {

                }
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
                    if (dm.editMovie(titleT.Text.Trim(), (genreEnum)genreCB.SelectedIndex, (ratingEnum)ratingCB.SelectedIndex, fileLocationT.Text.Trim(), x, imageT.Text.Trim(), descriptionT.Text.Trim(), actors, index, out result))
                    {
                        sendEdit(dm.getMovie(index), new EventArgs());
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

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Really delete entry?", MessageBoxButtons.YesNo);
            String result;
            if (dr == DialogResult.Yes)
            {
                if (dm.removeEntry(index, dm.getMovie(index), out result))
                {
                    MessageBox.Show(result, "Entry Deleted", MessageBoxButtons.OK);
                    deleteEntry(null, null);
                }
                else
                {
                    MessageBox.Show(result, "Unable to Delete Entry" , MessageBoxButtons.OK); 
                }
            }
            else
            {
                //Do nothing
            }
        }
    }
}
