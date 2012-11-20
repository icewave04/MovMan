using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace movieData
{
    //File manager, reads and writes the Movie List and Data
    public class fileManager
    {
        string file = String.Format("{0}{1}", Directory.GetCurrentDirectory(), @"\movieList.xml");
        string dataFile = String.Format("{0}{1}", Directory.GetCurrentDirectory(), @"\dataList.xml");
        public globalException ge;
        public fileManager()
        {
            this.ge = new globalException();
        }

        public bool readFile()
        {
            return File.Exists(file);
        }

        public bool readData()
        {
            return File.Exists(dataFile);
        }

        #region Read Methods
        public void readData(ref Dictionary<string, data> readData)
        {
            string file = String.Empty;
            string image = String.Empty;
            int year = -1;
            long secondsIn = 0;
            string description = String.Empty;
            List<string> actors = new List<string>();

            using (XmlReader reader = XmlReader.Create(this.dataFile))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name == "File")
                        {
                            file = reader.ReadElementContentAsString();
                            reader.MoveToNextAttribute();
                            if (reader.Name == "Year") //These checks prevent the wrong field being read to the wrong variable
                            {
                                year = Int32.Parse(reader.ReadElementContentAsString());
                            }
                            reader.MoveToNextAttribute();
                            if (reader.Name == "Image") //These checks prevent the wrong field being read to the wrong variable
                            {
                                image = reader.ReadElementContentAsString();
                            }
                            reader.MoveToNextAttribute();
                            if (reader.Name == "Description") //These checks prevent the wrong field being read to the wrong variable
                            {
                                description = reader.ReadElementContentAsString();
                            }
                            reader.MoveToNextAttribute();
                            if (reader.Name == "SecondsIn")
                            {
                                secondsIn = reader.ReadElementContentAsLong();
                            }
                            reader.MoveToNextAttribute();
                            if (reader.Name == "Actor") //These checks prevent the wrong field being read to the wrong variable
                            {
                                while (reader.Name == "Actor")
                                {
                                    actors.Add(reader.ReadElementContentAsString());
                                    reader.MoveToNextAttribute();
                                }
                            }
                            readData.Add(file, new data(year, image, description, actors, secondsIn));

                            file = "";
                            image = "";
                            year = -1;
                            description = "";
                            actors.Clear();
                        }
                    }
                }
            }
        }

        public void readAll(ref List<info> readList)
        {
            string title = String.Empty;
            string file = String.Empty;
            
            genreEnum genre = genreEnum.Unknown;
            ratingEnum rating = ratingEnum.Unknown;
            using (XmlReader reader = XmlReader.Create(this.file))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        if (reader.Name == "Title")
                        {
                            title = reader.ReadElementContentAsString();
                            reader.MoveToNextAttribute();
                            if (reader.Name == "File") //These checks prevent the wrong field being read to the wrong variable
                            {
                                file = reader.ReadElementContentAsString();
                            }
                            reader.MoveToNextAttribute();
                            try
                            {
                                if (reader.Name == "Genre") //These checks prevent the wrong field being read to the wrong variable
                                {
                                    genre = (genreEnum)(Int32.Parse(reader.ReadElementContentAsString()));
                                }
                            }
                            catch (Exception e)
                            {
                                this.ge.GlobalTryCatch(e, "Genre Read In Error");
                                genre = genreEnum.Unknown;
                            }
                            reader.MoveToNextAttribute();

                            try
                            {
                                if (reader.Name == "Rating") //These checks prevent the wrong field being read to the wrong variable
                                {
                                    rating = (ratingEnum)(Int32.Parse(reader.ReadElementContentAsString()));
                                }
                            }
                            catch (Exception e)
                            {
                                this.ge.GlobalTryCatch(e, "Rating Read In Error");
                                rating = ratingEnum.Unknown;
                            }
                            reader.MoveToNextAttribute();

                            readList.Add(new info(title, file, rating, genre));
                            title = "";
                            file = "";
                            genre = genreEnum.Unknown;
                            rating = ratingEnum.Unknown;
                        }
                    }
                }
            }
        }
        #endregion

        #region Write Methods
        public void writeData(Dictionary<string, data> dataList)
        {
            try
            {
                if (File.Exists(dataFile))
                {
                    File.Delete(dataFile);
                }
            }
            catch (IOException e)
            {
                this.ge.GlobalTryCatch(e, "Error writting List File - " + dataFile);
            }
            using (XmlWriter writer = XmlWriter.Create(dataFile))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("MovieData");
                List<string> actorWrite = new List<string>();
                foreach (KeyValuePair<string,data> d in dataList)
                {
                    actorWrite = d.Value.getActors();
                    writer.WriteStartElement("Entry");
                    writer.WriteElementString("File", d.Key);
                    writer.WriteElementString("Year", d.Value.Year.ToString());
                    writer.WriteElementString("Image", d.Value.Image);
                    writer.WriteElementString("Description", d.Value.Description);
                    writer.WriteElementString("SecondsIn", d.Value.SecondsIn.ToString());
                    foreach (string a in actorWrite)
                    {
                        writer.WriteElementString("Actor", a);
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
        }

        public void writeAll(List<info> writeList)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            catch (IOException e)
            {
                this.ge.GlobalTryCatch(e, "Error writting Data File - " + dataFile);
            }
            using (XmlWriter xmlw = XmlWriter.Create(file))
            {
                xmlw.WriteStartDocument();
                xmlw.WriteStartElement("Movies");
                
                foreach (info movie in writeList)
                {
                    xmlw.WriteStartElement("Movie");
                    string rating = ((int)movie.Genre).ToString();
                    xmlw.WriteElementString("Title", movie.getTitle());
                    xmlw.WriteElementString("File", movie.File);
                    xmlw.WriteElementString("Genre", ((int)movie.Genre).ToString());
                    xmlw.WriteElementString("Rating", ((int)movie.Rating).ToString());

                    xmlw.WriteEndElement();
                }

                xmlw.WriteEndElement();
                xmlw.WriteEndDocument();
                xmlw.Close();
            }
        }
        #endregion
    }
}
