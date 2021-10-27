using System.Threading;
using mpp1.Serialization;
using mpp1.TestMethods;
using mpp1.TracerLib;
using mpp1.WriterLib;
namespace mpp1.Main
{
    class Program
    {
        static void Main(string[] args)
        {

            var tracer = new Tracer();
            var foo = new Foo(tracer);

            var thread = new Thread(foo.MyMethod);
            thread.Start();
            thread.Join();

            thread = new Thread(foo.NotMyMethod);
            thread.Start();
            foo.NotMyMethod();
            thread.Join();

            var res = tracer.GetTraceResult();

            
            var writer = new Writer();
            var serialize = new SerializersImpl();

            string outRes = serialize.ToXml(res);            
            writer.ToConsole(outRes);
            writer.ToFile(outRes, "xmlFile.txt");

            outRes = serialize.ToJson(res);
            writer.ToConsole(outRes);
            writer.ToFile(outRes, "jsonFile.txt");
        }
    }
}