using System;

namespace get_started.control;
public class Program
{
    public static void Main(string[] args)
    {

        IControl control = null;

        if (control == null)
        {
            control = new Control();
        }

        if (control.IsStarted() != true)
        {
            control.Initialize();
            control.Start();
        }

        control.Stop();
    }
}