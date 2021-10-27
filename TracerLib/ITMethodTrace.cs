namespace mpp1.TracerLib
{
    public interface ITMethodTrace
    {
        public void StartTrace();
        public void StopTrace();
        public void AddChildResult(MethodRes methodRes);
        public MethodRes GetTraceRes();
    }
}