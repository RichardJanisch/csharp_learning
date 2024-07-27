namespace get_started.control
{
    public interface IControl
    {
        void Initialize();
        void Start();
        void Stop();
        bool IsStarted();
    }
}