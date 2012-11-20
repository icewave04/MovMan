using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace movieData
{
    public class data
    {
        private List<string> actors;
        private int year;
        private string image;
        private string description;
        private long secondsIn;

        public data()
        {
            year = -1;
            image = "";
            description = "No Description";
            actors = new List<string>();
            secondsIn = 0;
        }

        public data(int year, string image, string description, long secondsIn)
        {
            this.year = year;
            this.description = description;
            this.image = image;
            this.secondsIn = secondsIn;
            actors = new List<string>();
        }

        public data(int year, string image, string description, List<string> actors, long secondsIn)
        {
            this.year = year;
            this.description = description;
            this.image = image;
            this.secondsIn = secondsIn;
            this.actors = new List<string>();
            foreach(string a in actors){
                this.actors.Add(a);
            }
        }

        public string ToString()
        {
            return String.Format("{0} {1} {2} {3}\n", year, image, description, printActors());
        }

        public string printActors()
        {
            string a = String.Empty;
            foreach (string x in actors)
            {
                a = String.Format("{0} {1}", a, x);
            }
            return a;
        }

        #region Properties
        public int Year
        {
            get
            {
                return year;
            }

            set
            {
                year = value;
            }
        }

        public string Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public long SecondsIn
        {
            get
            {
                return secondsIn;
            }
            set
            {
                secondsIn = value;
            }
        }
#endregion

        public void addActors(List<string> actorsx)
        {
            actors = null;
            this.actors = actorsx.ToList();
        }

        public List<string> getActors()
        {
            return actors;
        }
    }
}
