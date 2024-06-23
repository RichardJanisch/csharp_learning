using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Willkommen zum einfachen Konsolenrechner!");

        Console.Write("Gib die erste Zahl ein: ");
        double num1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Gib die zweite Zahl ein: ");
        double num2 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Wähle eine Operation ( +, -, *, / ): ");
        string operation = Console.ReadLine();

        double result = 0;

        switch (operation)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "*":
                result = num1 * num2;
                break;
            case "/":
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                else
                {
                    Console.WriteLine("Division durch Null ist nicht erlaubt.");
                    return;
                }
                break;
            default:
                Console.WriteLine("Ungültige Operation.");
                return;
        }

        Console.WriteLine($"Das Ergebnis ist: {result}");
    }
}
