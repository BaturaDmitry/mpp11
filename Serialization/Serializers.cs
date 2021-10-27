using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using mpp1.TracerLib;
using Newtonsoft.Json;

namespace mpp1.Serialization
{
    public class Record
    {
        [XmlIgnore]
        [JsonIgnore]
        public int Id;

        [XmlElement(ElementName = "Thread")]
        public ThreadRes Res;

        public Record(int key, ThreadRes res)
        {
            Id = key;
            this.Res = res;
        }
        public Record()
        {
        }
    }

    public class SerializersImpl:ITSerializers
    {

        public string ToXml(TraceResult result)
        {
            List<Record> records = new List<Record>();
            foreach (int key in result.Threads.Keys)
            {
                records.Add(new Record(key, result.Threads[key]));
            }

            XmlSerializer formatter = new XmlSerializer(typeof(List<Record>));
            var stringWriter = new StringWriter();
            formatter.Serialize(stringWriter, records);
            Console.WriteLine(stringWriter.ToString());
            return stringWriter.ToString();
        }
        public string ToJson(TraceResult result)
        {
            List<Record> records = new List<Record>();
            foreach (int key in result.Threads.Keys)
            {
                records.Add(new Record(key, result.Threads[key]));
            }
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return json;
        }

    }
}