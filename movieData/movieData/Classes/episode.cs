using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace movieData.Classes
{
    public class episode : info
    {
        //This class will be used with the currently non-developed Season class
        //A Season class will contain a set of episodes.
        //A SeasonSet will contain a set of Season class
        public episode(string title, string file, string seasonTitle, int epNumber):base(title, file)
        {
            
        }

        public episode(string title, string file, ratingEnum rating, genreEnum genre, string seasonTitle, int epNumber)
            : base(title, file, rating, genre)
        {

        }
    }
}
