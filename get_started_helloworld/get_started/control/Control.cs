namespace get_started.control;
public class Control : IControl
{
    private bool _initialized = false;
    private bool _started = false;

    public void Initialize()
    {
        if (!_initialized)
        {
            Console.WriteLine("Control initialized");
            _initialized = true;
        }
    }
    public void Start()
    {
        Console.WriteLine("Control started");
        _started = true;
    }

    public bool IsStarted()
    {
        if (_started)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Stop()
    {
        Console.WriteLine("Control stopped");
    }
}