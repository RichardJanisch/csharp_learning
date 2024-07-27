namespace get_started.control;
public class Control : IControl
{
    public void Start()
    {
        Console.WriteLine("Control started");
    }

    public void Stop()
    {
        Console.WriteLine("Control stopped");
    }
}