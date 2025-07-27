// Test.cs
using System;
using System.Collections.Generic;
using Derivative;

class Test
{
    static void Main()
    {
        // function and exact derivatives
        Func<double, double> f = Math.Sin;
        Func<double, double> d1 = Math.Cos;          // first derivative
        Func<double, double> d2 = x => -Math.Sin(x); // second derivative
        Func<double, double> d3 = x => -Math.Cos(x); // third derivative

        var forward = new Forward(f);
        var back = new Backword(f);
        var central = new Central(f);

        double x0 = 1.2345;
        double[] hs = { 1e-1, 1e-2, 1e-3, 1e-4, 1e-5 };

        // --------- FORWARD ----------
        Banner("FORWARD");

        PrintGroup("First derivative", new List<(string, Func<double, double, double>)>
        {
            ("FstDe1stO", forward.FstDe1stO),
            ("FstDe2ndO", forward.FstDe2ndO),
            ("FstDe3rdO", forward.FstDe3rdO),
            ("FstDe4thO", forward.FstDe4thO),
        }, d1, x0, hs);

        Divider();

        PrintGroup("Second derivative", new List<(string, Func<double, double, double>)>
        {
            ("SndDe1stO", forward.SndDe1stO),
            ("SndDe2ndO", forward.SndDe2ndO),
            ("SndDe3rdO", forward.SndDe3rdO),
            ("SndDe4thO", forward.SndDe4thO),
        }, d2, x0, hs);

        Divider();

        PrintGroup("Third derivative", new List<(string, Func<double, double, double>)>
        {
            ("TrdDe1stO", forward.TrdDe1stO),
            ("TrdDe2ndO", forward.TrdDe2ndO),
            ("TrdDe3rdO", forward.TrdDe3rdO),
            ("TrdDe4thO", forward.TrdDe4thO),
        }, d3, x0, hs);

        // --------- BACKWARD ----------
        Banner("BACKWARD");

        PrintGroup("First derivative", new List<(string, Func<double, double, double>)>
        {
            ("FstDe1stO", back.FstDe1stO),
            ("FstDe2ndO", back.FstDe2ndO),
            ("FstDe3rdO", back.FstDe3rdO),
            ("FstDe4thO", back.FstDe4thO),
        }, d1, x0, hs);

        Divider();

        PrintGroup("Second derivative", new List<(string, Func<double, double, double>)>
        {
            ("SndDe1stO", back.SndDe1stO),
            ("SndDe2ndO", back.SndDe2ndO),
            ("SndDe3rdO", back.SndDe3rdO),
            ("SndDe4thO", back.SndDe4thO),
        }, d2, x0, hs);

        Divider();

        PrintGroup("Third derivative", new List<(string, Func<double, double, double>)>
        {
            ("TrdDe1stO", back.TrdDe1stO),
            ("TrdDe2ndO", back.TrdDe2ndO),
            ("TrdDe3rdO", back.TrdDe3rdO),
            ("TrdDe4thO", back.TrdDe4thO),
        }, d3, x0, hs);

        // --------- CENTRAL ----------
        Banner("CENTRAL");

        PrintGroup("First derivative", new List<(string, Func<double, double, double>)>
        {
            ("FstDe2ndO", central.FstDe2ndO),
            ("FstDe4thO", central.FstDe4thO),
        }, d1, x0, hs);

        Divider();

        PrintGroup("Second derivative", new List<(string, Func<double, double, double>)>
        {
            ("SndDe2ndO", central.SndDe2ndO),
            ("SndDe4thO", central.SndDe4thO),
        }, d2, x0, hs);

        Divider();

        PrintGroup("Third derivative", new List<(string, Func<double, double, double>)>
        {
            ("TrdDe2ndO", central.TrdDe2ndO),
            ("TrdDe4thO", central.TrdDe4thO),
        }, d3, x0, hs);
    }

    static void Banner(string title)
    {
        Console.WriteLine();
        Console.WriteLine(new string('=', title.Length + 8));
        Console.WriteLine($"=== {title} ===");
        Console.WriteLine(new string('=', title.Length + 8));
    }

    static void Divider()
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 60));
        Console.WriteLine();
    }

    static void PrintGroup(
        string subtitle,
        List<(string name, Func<double, double, double> num)> methods,
        Func<double, double> exact,
        double x0,
        double[] hs)
    {
        Console.WriteLine($"\n-- {subtitle} --");
        foreach (var (name, method) in methods)
        {
            Console.WriteLine($"\n{name}:");
            foreach (var h in hs)
            {
                double numerical = method(x0, h);
                double expected = exact(x0);
                Console.WriteLine($"   h={h:e2} -> num={numerical:E10}  exact={expected:E10}");
            }
        }
    }
}
