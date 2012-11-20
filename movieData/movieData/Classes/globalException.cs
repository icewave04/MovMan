using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace movieData
{
    //Manages the recording of all exceptions
    public class globalException
    {
        string path = String.Format("{0}{1}{2}", Directory.GetCurrentDirectory(),@"\Errors\", @String.Format("\\{0}{1}{2}{3}.txt", "errorFile_", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year));
        public globalException()
        {
        }

        public void GlobalTryCatch(Exception e, object o)
        {
            File.AppendAllText(path, String.Format("{0}\n{1}\n{2}\n{3}\n\n", System.DateTime.Now, e.Message, e.StackTrace, o.ToString()));
        }
    }
}
