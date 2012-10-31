using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace movieData
{
    public class dataManager
    {
        private List<simpleMovie> movieList;
        private Dictionary<string, data> movieData;
        private fileManager fm;
        private Settings settings;
        public globalException ge;
        private bool sorted = false;

        public dataManager()
        {
            ge = new globalException();
            movieList = new List<simpleMovie>();
            movieData = new Dictionary<string, data>();
            fm = new fileManager();
            settings = new Settings();
            //Implement threading at a later date
            if (fm.readFile())
            {
                fm.readAll(ref movieList);
                Console.WriteLine(String.Format("{0} entires have been added via file", movieList.Count));
                sorted = true;
            }
            if (fm.readData())
            {
                fm.readData(ref movieData);
            }


            quickSort(ref movieList);
            
            //populateAndSort();
            //outputAll();
            
        }

        #region Properties
        public fileManager FileManager
        {
            get
            {
                return this.fm;
            }
        }

        public Dictionary<string, data> Data
        {
            get
            {
                return this.movieData;
            }
        }

        public simpleMovie getMovie(int i)
        {
            return this.movieList[i];
        }

        public Settings Settings
        {
            get
            {
                return settings;
            }
        }

        public int movieListSize
        {
            get
            {
                return movieList.Count;
            }
        }
        #endregion

        //Deletes an entry from list and dictionary, writes modified data to file
        public bool removeEntry(int i, simpleMovie s, out string result)
        {
            try
            {
                if (movieList[i] != null)
                {
                    if (movieList[i] == s)
                    {
                        movieData.Remove(movieList[i].File);
                        movieList.RemoveAt(i);

                        writeList();
                        result = "Entry Removed";
                        return true;
                    }
                    else
                    {
                        result = "Entry was not a match to index given";
                        return false;
                    }
                }
                else
                {
                    result = "Entry Not Found";
                    return false;
                }
            }
            catch (ArgumentOutOfRangeException a)
            {
                result = "Out Of Bounds Exception";
                this.ge.GlobalTryCatch(a, s);
                return false;
            }
            catch (Exception e)
            {
                this.ge.GlobalTryCatch(e, s);
                result = "An error occured";
                return false;
            }
        }

        //Gets the specified data
        public data getdata(string file)
        {
            try
            {
                return movieData[file];
            }
            catch (KeyNotFoundException k)
            {
                this.ge.GlobalTryCatch(k, file);
                return null;
            }
        }

        #region The Search Algorithms
        public void performActorOnlySearch(string p, bool b, ref List<simpleMovie> resultList)
        {
            string[] words = p.ToLower().Split(' ');
        
            int count = 0;
            int max = words.Length;
            bool broken = false;
            if (words.Length > 1)
            {
                foreach (simpleMovie a in movieList)
                {
                    
                    count = 0;
                    for(int j = 0; j < words.Length; j++)
                    {
                        List<string> actors = movieData[a.File].getActors();
                        for (int i = 0; i < actors.Count; i++)
                        {
                            try
                            {
                                if (actors[i].ToLower().Contains(words[j].ToLower()))
                                {
                                    count++;
                                    i = actors.Count;
                                    if (!b)
                                    {
                                        resultList.Add(a);
                                        broken = true;
                                        break;
                                    }
                                }
                            }
                            catch (IndexOutOfRangeException e)
                            {
                                this.ge.GlobalTryCatch(e, p);
                            }
                        }
                        if (broken)
                        {
                            broken = false;
                            break;
                        }
                        actors = null;
                    }
                    if (b && count == max)
                    {
                        resultList.Add(a);
                        
                    }
                    
                }
            }
            else
            {
                performAdvancedSearch(p, ref resultList);
            }
        }

        public void performDetailedAdvancedSearch(string p, ref List<simpleMovie> resultList)
        {
            string[] words = p.ToLower().Split(' ');
            int count = 0;
            int max;
            if (words.Length < 3)
            {
                max = words.Length;
            }
            else
            {
                max = words.Length / 2;
            }
            if (words.Length > 1)
            {
                data d;
                foreach (simpleMovie a in movieList)
                {
                    d = movieData[a.File];
                    count = 0;
                    foreach (string word in words)
                    {
                        if (d.ToString().Contains(word))
                        {
                            //resultList.Add(a);
                            count++;
                        }
                    }
                    if (count == max)
                    {
                        resultList.Add(a);

                    }
                }
            }
            else
            {
                performAdvancedSearch(p, ref resultList);
            }
        }

        public void performAdvancedSearch(string p, ref List<simpleMovie> resultList)
        {
            data d;
            foreach (simpleMovie a in movieList)
            {
                d = movieData[a.File];

                Regex reg = new Regex(String.Format("^[\\w]*{0}{1}[\\w]*$", p, "{1}"), RegexOptions.IgnoreCase);
                if (reg.IsMatch(d.ToString()))
                {
                    resultList.Add(a);
                }
                else if (d.ToString().Contains(p))
                {
                    resultList.Add(a);
                }
            }
        }

        public void performDetailedSearch(string p, ref List<simpleMovie> resultList)
        {
            string[] words = p.ToLower().Split(' ');
            if (words.Length > 1)
            {
                foreach (simpleMovie a in movieList)
                {
                    foreach (string word in words)
                    {
                        if (a.getTitle().ToLower().Contains(word))
                        {
                            resultList.Add(a);
                            break;
                        }
                    }
                }
            }
        }

        public void performRegexSearch(string p, ref List<simpleMovie> resultList)
        {
            p = p.ToLower();
            try
            {
                if (p.Length == 1)
                {
                    foreach (simpleMovie a in movieList)
                    {
                        if (p[0] == a.getTitle().ToLower()[0])
                        {
                            resultList.Add(a);
                        }
                    }
                }
                else
                {
                    Regex reg = new Regex(String.Format("^[\\w]*{0}{1}[\\w]*$", p, "{1}"), RegexOptions.IgnoreCase);

                    foreach (simpleMovie a in movieList)
                    {
                        if (a.getTitle().ToLower() != p)
                        {
                            if (a.getTitle().ToLower().Contains(p))
                            {
                                resultList.Add(a);
                            }
                            else if (reg.IsMatch(a.getTitle().ToLower()))
                            {
                                resultList.Add(a);
                            }
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException a)
            {
                this.ge.GlobalTryCatch(a, p);
            }

        }

        public simpleMovie performSearch(string search)
        {
            simpleMovie x = new simpleMovie(search, search);
            try
            {
                if (sorted && movieList.Count > 5) //If the list is sorted perform binary search
                {
                    simpleMovie s = movieList[movieList.BinarySearch(x)];
                    if (s != null)
                    {
                        return s;
                    }
                }
                else //Else this, only searches for exact matches
                {
                    simpleMovie s = movieList.Find(
                        delegate(simpleMovie sm)
                        { 
                            return sm.getTitle().ToLower() == x.getTitle().ToLower();
                        }
                    );

                    if (s != null)
                    {
                        return s;
                    }
                }
            }
            catch (ArgumentOutOfRangeException a)
            {
                this.ge.GlobalTryCatch(a, search);
            }
            return null;
        }
        #endregion

        //Call tp fm to write data
        private void writeList()
        {
            fm.writeAll(movieList);
            fm.writeData(movieData);
        }

        public int getListSize()
        {
            return movieList.Count;
        }

        public int getIndexOf(simpleMovie x)
        {
            return movieList.IndexOf(x);
        }

        //Edits an existing movie and its data
        public bool editMovie(string title, genreEnum genre, ratingEnum rating, string file, int year, string image, string description, List<string> actors, int index, out string result)
        {
            if (title.Trim() != "")
            {
                if (file.Trim() != "")
                {//Genre and Rating can never be null
                    movieList[index].setTitle(title);
                    movieList[index].Genre = genre;
                    movieList[index].Rating = rating;
                    movieData[movieList[index].File].Year = year;
                    movieData[movieList[index].File].Image = image;
                    movieData[movieList[index].File].Description = description;
                    movieData[movieList[index].File].addActors(actors);
                    result = "File Edited";
                    writeList();
                    Console.WriteLine(movieList[index]);
                    return true;
                }
                else
                {
                    result = "File field invalid";
                    return false;
                }
            }
            else
            {
                result = "Title field invalid";
                return false;
            }
        }

        //Adds a new movie and its data
        public bool addMovie(string title, genreEnum genre, ratingEnum rating, string file, int year, string image, string description, List<string> actors, out string result)
        {
            if (title.Trim() != "")
            {
                if (file.Trim() != "")
                {//Genre and Rating can never be null
                    if (!movieData.ContainsKey(file))
                    {
                        movieList.Add(new simpleMovie(title, file, rating, genre));
                        movieData.Add(file, new data(year, image, description, 0));
                        if (actors.Count != 0)
                        {
                            movieData[file].addActors(actors);
                        }
                        result = "Movie added";
                        sorted = false;
                        writeList();
                        Console.WriteLine(movieList.Last());
                        return true;
                    }
                    else
                    {
                        result = "File already exists";
                        return false;
                    }
                }
                else
                {
                    result = "File field invalid";
                    return false;
                }
            }
            else
            {
                result = "Title field invalid";
                return false;
            }
        }

        //Testing method, used to test speed and accuracy
        private void populateAndSort()
        {
            bool hax = false;
            int X = 100000;
            bool sort = false;
            for (int i = 0; i < X; i++)
            {
                string x = Char.ConvertFromUtf32(122-(i % 26));
                movieList.Add(new simpleMovie(x+"title"+i, i+"file"+x, ratingEnum.G, genreEnum.Romance));
                movieData.Add(i + "file" + x, new data(i, i + x + "image" + x, "description" + i + x + i + x, 0));
                if (hax && i > X) //Used to stress test file output system, forces a write after every entry after X
                {
                    if (sort) //Additional stress test, forces a sort on every entry after X, then writes
                    {
                        quickSort(ref movieList);
                    }
                    writeList();
                }
            }
            
            quickSort(ref movieList);
            writeList();
            //Do not use this loop if i > 1000, produces inaccurate time results as most time is spent outputting this loop
            //outputAll();
            
        }

        private void outputAll()
        {
            foreach (simpleMovie print in movieList)
            {
                Console.Write(print.ToString());
                try
                {
                    Console.Write(movieData[print.File].ToString());
                }
                catch (KeyNotFoundException e)
                {
                    Console.Write("Error, Key Not Found. No data recorded for this entry.\n");
                    this.ge.GlobalTryCatch(e, "Output All");
                }
            }
        }

        //Culls sorted by performing a comparision on i and i+1, if they are a match it removes i+1 and reduces i to maintain position in the list
        public void cullSorted<T>(ref List<T> sortList) where T: simpleMovie
        {
            for (int i = 0; i < sortList.Count-1; i++)
            {
                if (sortList[i] == sortList[i + 1])
                {
                    sortList.RemoveAt(i + 1);
                    i--;
                }
            }
        }

        #region Quick Sort - Median of Three
        private void swap<T>(int i, int j, List<T> sortList) where T : simpleMovie
        {
            T temp = sortList[i];
            sortList[i] = sortList[j];
            sortList[j] = temp;
        }

        public void quickSort<T>(ref List<T> sortList) where T : simpleMovie
        {
            int left = 0;
            int right = sortList.Count-1;

            quickQuickSort(left, right, sortList);
            sorted = true;
        }

        private void quickQuickSort<T>(int left, int right, List<T> sortList) where T : simpleMovie
        {
            if (left < right)
            {
                int pivotIndex = partition(left, right, sortList);
                quickQuickSort(left, pivotIndex - 1, sortList);
                quickQuickSort(pivotIndex + 1, right, sortList);
            }
            else
            {
                return;
            }
        }

        private int partition<T>(int left, int right, List<T> sortList) where T : simpleMovie
        {
            int mid = medianOf3(left, right, sortList);
            T pivot = sortList[mid];
            swap(mid, right, sortList);

            while (left < right)
            {
                while (left < right && sortList[left].CompareTo(pivot) < 0)
                {
                    left++;
                }
                if (left < right)
                {
                    swap(left, right, sortList);
                    right--;
                }

                while (right > left && sortList[right].CompareTo(pivot) > 0)
                {
                    right--;
                }
                if (right > left)
                {
                    swap(left, right, sortList);
                    left++;
                }
            }

            return left;
        }

        private int medianOf3<T>(int left, int right, List<T> sortList) where T : simpleMovie
        {
            int mid = (left + right) / 2;
            if (sortList[left].CompareTo(sortList[mid]) < 0)
            {
                swap(left, mid, sortList);
            }
            if (sortList[left].CompareTo(sortList[right]) < 0)
            {
                swap(left, right, sortList);
            }
            if (sortList[mid].CompareTo(sortList[right]) < 0)
            {
                swap(mid, right, sortList);
            }
            return mid;
        }
        #endregion

        public int percent(int x, int y)
        {
            return ((x / y) * 100);
        }
    }
}
