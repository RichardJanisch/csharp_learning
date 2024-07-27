using System;

namespace get_started.control;
public class Program
{
    public static void Main(string[] args)
    {
        IControl control = new Control();
        control.Start();
        control.Stop();
    }
}