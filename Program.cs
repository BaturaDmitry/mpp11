using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Threading;

namespace mpp1
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
            var serialize = new Serializers();

            string outRes = serialize.toXML(res);            
            writer.toConsole(outRes);
            writer.toFile(outRes, "xml.txt");

            outRes = serialize.toJSON(res);
            writer.toConsole(outRes);

            

        }
    }
}