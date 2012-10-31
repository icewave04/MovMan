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
    public partial class editSettings : UserControl
    {
        private dataManager dm;
        public EventHandler sendEdit = null;
        public editSettings(dataManager dm)
        {
            InitializeComponent();
            this.dm = dm;
            openWithDir.Text = dm.Settings.vlc;
            stats.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fname = String.Empty;
            OpenFileDialog odf = new OpenFileDialog();
            odf.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            odf.Filter = "Program Files|*.exe|All Files|*.*";

            DialogResult result;
            result = odf.ShowDialog();

            if (result == DialogResult.OK)
            {
                openWithDir.Text = odf.FileName;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dm.Settings.vlc = "\"" + openWithDir.Text.Trim() + "\"";
            dm.Settings.writeSettings();
            sendEdit(null, null);
        }

        public void updateCount()
        {
            stats.Text = String.Format("{0}/{1} {2}", dm.movieListSize, dm.movieListSize, "movies currently being managed");
        }
    }
}
