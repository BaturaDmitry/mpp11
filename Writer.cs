using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mpp1
{
    public class Writer
    {
        public void toConsole(string text)
        {
            Console.OpenStandardOutput();
            Console.WriteLine(text+"\n");
        }

        public void toFile(string text, string fileName)
        {
            System.IO.File.WriteAllText(fileName, text);
            Console.WriteLine("Writing to the file was successful.\n");
        }
    }
}