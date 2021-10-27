using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace mpp1.TracerLib
{

    class MethodTrace:ITMethodTrace
    {
        private readonly Stopwatch _stopwatch;
        private readonly MethodRes _method = new MethodRes();

        public MethodTrace()
        {
            this._stopwatch = new Stopwatch();

            StackTrace trace = new StackTrace();
            var oneFrame = trace.GetFrame(3);
            _method.set_methodName(oneFrame.GetMethod().Name);
            _method.set_className(oneFrame.GetMethod().ReflectedType.Name);
        }

        public void StartTrace()
        {
            _stopwatch.Start();
        }

        public void StopTrace()
        {
            _stopwatch.Stop();
            var time = _stopwatch.ElapsedMilliseconds;
            _method.set_time(time);
        }

        public void AddChildResult(MethodRes methodRes)
        {
            this._method.AddChild(methodRes);
        }
        public MethodRes GetTraceRes()
        {
            return _method;
        }

    }

    class ThreadTrace:ITThreadTrace
    {
        private ThreadRes _thread;
        private Stack<MethodTrace> _stackList = new Stack<MethodTrace>();
        
        public ThreadTrace(int id)
        {
            this._thread = new ThreadRes();
            this._thread.Id = id;
        }

        public void StartTrace()
        {
            var newMethodTrace = new MethodTrace();
            _stackList.Push(newMethodTrace);
            newMethodTrace.StartTrace();
        }

        public void StopTrace()
        {
            var lastTrace = _stackList.Pop();
            lastTrace.StopTrace();
            var method = lastTrace.GetTraceRes();



            if (_stackList.Count > 0)
            {
                var newLastTracer = _stackList.Peek();
                newLastTracer.AddChildResult(method);
            }
            else
            {
                _thread.AddMethod(method);
                _thread.AddTime(method.Time);
            }
            
        }
        public ThreadRes GetTraceRes()
        {
            return _thread;
        }
    }

    public class Tracer : ITracer
    {
        private Dictionary<int, ThreadTrace> _threadObjects = new Dictionary<int, ThreadTrace>();

        
        public void StartTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            ThreadTrace thread;
            if (_threadObjects.ContainsKey(threadId))
            {
                thread = _threadObjects[threadId];
            }
            else
            {
   
                thread = new ThreadTrace(threadId);
                _threadObjects[threadId] = thread;
            }

            thread.StartTrace();

        }
        
        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            if (_threadObjects.ContainsKey(threadId))
            {
                _threadObjects[threadId].StopTrace();
            }

        }
        
        public TraceResult GetTraceResult()
        {
            
            var values = _threadObjects.Values;

            var results = new Dictionary<int, ThreadRes>();
            foreach (ThreadTrace value in values)
            {
                var threadResult = value.GetTraceRes();
                results.Add(threadResult.Id, threadResult);
            }

            var result = new TraceResult(results);

            return result;
        }
    }

}