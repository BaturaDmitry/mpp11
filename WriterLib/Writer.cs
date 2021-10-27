using System;

namespace mpp1.WriterLib
{
    public class Writer:ITWriter
    {
        private string Path = "C:\\Users\\37529\\RiderProjects\\mpp1\\mpp1\\XmlAndJson\\";
        public void ToConsole(string text)
        {
            Console.OpenStandardOutput();
            Console.WriteLine(text+"\n");
        }

        public void ToFile(string text, string fileName)
        {
            System.IO.File.WriteAllText(Path+fileName, text);
            Console.WriteLine("Writing to the file was successful.\n");
        }
    }
}