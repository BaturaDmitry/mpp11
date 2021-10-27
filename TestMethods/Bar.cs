using System.Threading;
using mpp1.TracerLib;

namespace mpp1.TestMethods
{
    public class Bar
    {
        private ITracer _tracer;

        public Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(24);
            
            _tracer.StopTrace();
        }
    }
}