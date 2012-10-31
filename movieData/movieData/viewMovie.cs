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
using System.Threading;

namespace movieData
{
    public partial class viewMovie : UserControl
    {
        private simpleMovie view;
        private data Data;
        private dataManager dm;
        private List<string> actor;
        public EventHandler viewEdit = null;
        public EventHandler actorEvent = null;
        private int buffer = 10;
        int pid = -1;

        public viewMovie(dataManager dm)
        {
            InitializeComponent();
            this.dm = dm;
            actor = new List<string>();
            moviePanel.Hide(); //Currently, this panel is inactive
            secondsIn.Hide(); //Currently, this value will always be zero
            imageL.Hide();
        }

        public void resize(int x, int y)
        {
            editMovieBtn.Location = new Point(buffer, y - editMovieBtn.Height-buffer);
            vlcBtn.Location = new Point(editMovieBtn.Location.X + vlcBtn.Width + buffer, editMovieBtn.Location.Y);
            secondsIn.Location = new Point(editMovieBtn.Location.X, editMovieBtn.Location.Y - secondsIn.Height - buffer);
            fileL.Location = new Point(secondsIn.Location.X, secondsIn.Location.Y - fileL.Height - buffer);
            description.Location = new Point(fileL.Location.X, fileL.Location.Y - description.Height - buffer);
            actors.Location = new Point(description.Location.X, description.Location.Y - actors.Height - buffer);
            imageL.Location = new Point(actors.Location.X, actors.Location.Y - imageL.Height - buffer);
            yearL.Location = new Point(imageL.Location.X, imageL.Location.Y - yearL.Height - buffer);
            ratingL.Location = new Point(yearL.Location.X, yearL.Location.Y - ratingL.Height - buffer);
            genreL.Location = new Point(ratingL.Location.X, ratingL.Location.Y - genreL.Height - buffer);
            titleL.Location = new Point(genreL.Location.X, genreL.Location.Y - titleL.Height - (buffer * 2));
            imageP.Size = new Size(x - description.Width - buffer, y - (buffer * 2));
            imageP.Location = new Point(description.Width + (buffer * 2), y - imageP.Height - buffer);
            DoImage(imageL.Text, false);

            /*
            titleL.Location = new Point(buffer, buffer);
            genreL.Location = new Point(titleL.Location.X, titleL.Location.Y+titleL.Size.Height+buffer);
            ratingL.Location = new Point(titleL.Location.X,genreL.Location.Y + genreL.Size.Height + buffer);
            yearL.Location = new Point(titleL.Location.X,ratingL.Location.Y + ratingL.Size.Height + buffer);
            imageL.Location = new Point(titleL.Location.X,yearL.Location.Y + ratingL.Size.Height + buffer);
            actors.Location = new Point(titleL.Location.X,imageL.Location.Y + yearL.Size.Height + (buffer * 2));
            description.Location = new Point(titleL.Location.X, actors.Location.Y + actors.Size.Height + buffer);
            fileL.Location = new Point(titleL.Location.X, description.Location.Y + description.Size.Height + buffer);
            editMovieBtn.Location = new Point(titleL.Location.X, fileL.Location.Y + fileL.Size.Height + buffer);
            vlcBtn.Location = new Point(description.Size.Width - vlcBtn.Size.Width, editMovieBtn.Location.Y);
            imageP.Location = new Point(description.Size.Width + buffer, titleL.Location.Y);
            imageP.Size = new Size(x - description.Size.Width, y);
            */
            this.Height = y;
            this.Width = x;
        }

        public void viewItem(simpleMovie view)
        {
            this.view = view;
            titleL.Text = view.getTitle();
            fileL.Text = view.File;
            genreL.Text = view.Genre.ToString();
            ratingL.Text = view.Rating.ToString();

            if ((this.Data = dm.getdata(view.File)) != null)
            {
                yearL.Text = Data.Year.ToString();
                imageL.Text = Data.Image;
                description.Text = Data.Description;
                actor = null;
                actor = Data.getActors();
                actors.DataSource = null;
                actors.DataSource = actor;
                secondsIn.Text = String.Format("{0}{1}", "Time: ", Data.SecondsIn * 1000);
                string imageFile = Data.Image;
                if (imageFile == "")
                {
                    imageFile = @"noImage.png";

                }

                DoImage(imageFile, false);
                
            }
            else
            {
                yearL.Text = "No Year";
                imageL.Text = "No Image";
                description.Text = "No Description";
                actor.Clear();
                actor.Add("No Actors");
                actors.DataSource = null;
                actors.DataSource = actor;
            }
        }

        //Handles the different image inputs and manages image related errors (and replaces image with image related to error)
        private void DoImage(string thisImage, bool error)
        {
            try
            {
                Image image = Image.FromFile(@thisImage);
                Rectangle newRect = ImageHandling.GetScaledRectangle(image, imageP.ClientRectangle);


                imageP.Image = ImageHandling.GetResizedImage(image, newRect);

            }
            catch (ArgumentException ae)
            {
                dm.ge.GlobalTryCatch(ae, thisImage);
                if (error)
                {
                    DoImage(@"Images\bigError.png", true);
                }
                else
                {
                    DoImage(@"Images\argumentError.png", true);
                }
            }
            catch (FileNotFoundException fnfe)
            {
                dm.ge.GlobalTryCatch(fnfe, thisImage);
                if (error)
                {
                    DoImage(@"Images\bigError.png", true);
                }
                else
                {
                    DoImage(@"Images\fileNotFound.png", true);
                }
            }
        }

        public void clearAll()
        {
            view = null;
            Data = null;
            titleL.Text = String.Empty;
            fileL.Text = String.Empty;
            genreL.Text = String.Empty;
            ratingL.Text = String.Empty;
            yearL.Text = String.Empty;
            imageL.Text = String.Empty;
            description.Text = String.Empty;
        }

        private void editMovieBtn_Click(object sender, EventArgs e)
        {
            viewEdit(view, e);
        }

        private void searchActor(object sender, MouseEventArgs e)
        {
            string actorSearch = actors.SelectedItem.ToString();
            actorEvent(actorSearch, e);
        }

        //Plays the movie, error checking not invovled. If the file is not openable, VLC will return the appropriate error
        private void vlcBtn_Click(object sender, EventArgs e)
        {
            //LivVlc code, inactive due to limited funcionality. 
            /*
            if (playMovie != null && playMovie.MovieStatus != null && playMovie.MovieStatus == true)
            {
                playMovie.Dispose();
            }
            else
            {
                playMovie = new movieForm(Data, dm, view);
                
            }
            */

            //Default, external VLC is the best choice despite lack of features i can implement it DOES work
            try
            {
                if (pid != -1)
                {
                    if (Process.GetProcessById(pid) != null && !Process.GetProcessById(pid).HasExited)
                    {
                        Process.GetProcessById(pid).Kill();
                    }
                }
            }
            catch (Exception ep)
            {
                //This is usually a null exception, so should be fine
            }
            Process runVlc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            //startInfo.FileName = "\"" + Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + program + "\"" + "\"" + view.File + "\"";
            startInfo.FileName = String.Format("\"{0}\" \"{1}\"", dm.Settings.vlc, view.File);
            Console.WriteLine(startInfo.FileName);
            runVlc.StartInfo = startInfo;
            try
            {
                runVlc.Start();
                pid = runVlc.Id;
            }
            catch (Exception f)
            {
                //Keep calm and carry on
            }
        }
    }
}
