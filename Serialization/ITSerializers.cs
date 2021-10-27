using mpp1.TracerLib;

namespace mpp1.Serialization
{
    public interface ITSerializers
    {
        public string ToXml(TraceResult result);
        public string ToJson(TraceResult result);
    }
}