using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace movieData
{
    public class Settings
    {
        private string path = String.Format("{0}{1}", Directory.GetCurrentDirectory(), @"\settings.xml");

        private string VLC;
        private bool vlcDefault;
        public Settings()
        {

            //Sets defaults
            VLC = "\"" + Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\VideoLAN\VLC\vlc.exe" + "\"";
            vlcDefault = false;


            readSettings();
        }

        public void readSettings()
        {
            string temp;
            try
            {
                using (XmlReader reader = XmlReader.Create(this.path))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            if (reader.Name == "OpenWith")
                            {
                                if ((temp = reader.ReadElementContentAsString()) != null)
                                {
                                    VLC = temp;
                                }
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                //First Run Detected
            }
        }

        public void writeSettings()
        {
            if(File.Exists(path)){
                File.Delete(path);
            }
            using (XmlWriter writer = XmlWriter.Create(path))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Settings");
                if (vlcDefault)
                {
                    writer.WriteElementString("OpenWith", "");
                }
                else
                {
                    writer.WriteElementString("OpenWith", VLC);
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }

        public void updateSettings()
        {

        }

        public string vlc
        {
            get
            {
                return this.VLC;
            }
            set
            {
                this.VLC = value;
            }
        }
    }
}
