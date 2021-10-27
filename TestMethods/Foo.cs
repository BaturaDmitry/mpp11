using System.Threading;
using mpp1.TracerLib;

namespace mpp1.TestMethods
{
    public class Foo
    {
        private readonly Bar _bar;
        private readonly ITracer _tracer;

        public Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();

            _bar.InnerMethod();

            _tracer.StopTrace();
        }

        public void NotMyMethod()
        {
            _tracer.StartTrace();
            
            _bar.InnerMethod();
            Thread.Sleep(50);
            _bar.InnerMethod();

            _tracer.StopTrace();
        }
    }
}