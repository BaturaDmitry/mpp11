using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json.Serialization;

namespace mpp1
{
    [Serializable]
    public class MethodRes
    {
        [XmlAttribute]
        public string methodName;
        [XmlAttribute]
        public string className;
        [XmlAttribute]
        public long time;
        public readonly List<MethodRes> childMethods = new List<MethodRes>();

        public void set_time(long time)
        {
            this.time = time;
        }
        public void set_methodName(string methodName)
        {
            this.methodName = methodName;
        }
        public void set_className(string className)
        {
            this.className = className;
        }
        public void addChild(MethodRes childMethod)
        {
            this.childMethods.Add(childMethod);
        }


    }

    [Serializable]
    public class ThreadRes
    {
        [Newtonsoft.Json.JsonIgnore]
        [XmlAttribute]
        public int id;
        [XmlAttribute]
        public long time;
        [XmlElement(ElementName = "methods")]
        public List<MethodRes> methods = new List<MethodRes>();


        public void addMethod(MethodRes method)
        {
            methods.Add(method);
        }

        public void addTime(long time)
        {
            this.time += time;
        }
    }

    [Serializable]
    public class TraceResult
    {
        //[Newtonsoft.Json.JsonProperty("id")]
        public Dictionary<int, ThreadRes> threads { get; private set; }

        public TraceResult()
        {
            threads = new Dictionary<int, ThreadRes>();
        }

        public TraceResult(Dictionary<int, ThreadRes> threads)
        {
            this.threads = threads;
        }
        public void addThread(int ID_Thread, ThreadRes thread)
        {
            threads.Add(ID_Thread, thread);
        }
        public ThreadRes getThreadInfo(int ID_Thread)
        {
            return threads[ID_Thread];
        }
        public Dictionary<int, ThreadRes> getResult()
        {
            return threads;
        }
    }
}