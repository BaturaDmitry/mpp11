namespace mpp1.WriterLib
{
    public interface ITWriter
    {
        public void ToConsole(string text);
        public void ToFile(string text, string fileName);
    }
}