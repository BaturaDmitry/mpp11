using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace mpp1.TracerLib
{
    [Serializable]
    public class MethodRes
    {
        [XmlAttribute]
        public string MethodName;
        [XmlAttribute]
        public string ClassName;
        [XmlAttribute]
        public long Time;
        public readonly List<MethodRes> ChildMethods = new List<MethodRes>();

        public void set_time(long time)
        {
            this.Time = time;
        }
        public void set_methodName(string methodName)
        {
            this.MethodName = methodName;
        }
        public void set_className(string className)
        {
            this.ClassName = className;
        }
        public void AddChild(MethodRes childMethod)
        {
            this.ChildMethods.Add(childMethod);
        }
        
    }

    [Serializable]
    public class ThreadRes:ITThreadRes
    {
        [Newtonsoft.Json.JsonIgnore]
        [XmlAttribute]
        public int Id;
        [XmlAttribute]
        public long Time;
        [XmlElement(ElementName = "methods")]
        public List<MethodRes> Methods = new List<MethodRes>();
        public void AddMethod(MethodRes method)
        {
            Methods.Add(method);
        }

        public void AddTime(long time)
        {
            this.Time += time;
        }
        public long get_time()
        {
            return Time;
        }

    }

    [Serializable]
    public class TraceResult
    {
        //[Newtonsoft.Json.JsonProperty("id")]
        public Dictionary<int, ThreadRes> Threads { get; private set; }

        public TraceResult()
        {
            Threads = new Dictionary<int, ThreadRes>();
        }

        public TraceResult(Dictionary<int, ThreadRes> threads)
        {
            this.Threads = threads;
        }
        public void AddThread(int idThread, ThreadRes thread)
        {
            Threads.Add(idThread, thread);
        }
        public ThreadRes GetThreadInfo(int idThread)
        {
            return Threads[idThread];
        }
        public Dictionary<int, ThreadRes> GetResult()
        {
            return Threads;
        }
    }
}