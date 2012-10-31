using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace movieData
{
    public class simpleMovie : IComparable, IComparer
    {
        private string title;
        private genreEnum genre;
        private ratingEnum rating;
        private string file;
        private bool hasThe;
        //Image, Year, Description and Actors will be in a seperate data structure

        public simpleMovie(string title, string file)
        {
            this.title = title;
            this.file = file;
            genre = genreEnum.Unknown;
            rating = ratingEnum.Unknown;
            hasThe = theSubstringCheck();
        }

        public simpleMovie(string title, string file, ratingEnum rating, genreEnum genre)
        {
            this.title = title;
            this.file = file;
            this.rating = rating;
            this.genre = genre;
            hasThe = theSubstringCheck();
        }

        public genreEnum Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
            }
        }

        public ratingEnum Rating
        {
            get
            {
                return rating;
            }
            set
            {
                rating = value;
            }
        }

        public string File
        {
            get
            {
                return file;
            }
            set
            {
                file = value;
            }
        }


        public void setTitle(string title)
        {
            this.title = title;
            this.hasThe = theSubstringCheck();
        }

        public string getTitle()
        {
            return title;
        }

        private bool theSubstringCheck()
        {
            try
            {
                if (title.Substring(0, 4).ToLower().Equals("the "))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                //This exception is not system critical and does not require reporting
                return false;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3}\n", title,file,(int)genre,(int)rating);
        }

        public int CompareTo(object obj)
        {
            if (this.hasThe)
            {
                if ((obj as simpleMovie).hasThe)
                {
                    return title.Substring(4).ToLower().CompareTo((obj as simpleMovie).title.Substring(4).ToLower());
                }
                return title.Substring(4).ToLower().CompareTo((obj as simpleMovie).title.ToLower());
            }
            else
            {
                if ((obj as simpleMovie).hasThe)
                {
                    return title.ToLower().CompareTo((obj as simpleMovie).title.Substring(4).ToLower());
                }
                return title.ToLower().CompareTo((obj as simpleMovie).title.ToLower());
            }
        }

        public int Compare(object x, object y)
        {
            if ((x as simpleMovie).getTitle() == (y as simpleMovie).getTitle()) { return 0; }
            if (x == null) { return -1; }
            if (y == null) { return 1; }

            return (x as simpleMovie).getTitle().CompareTo((y as simpleMovie).getTitle());
        }
    }
}
