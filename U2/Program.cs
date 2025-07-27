// Test.cs
using System;
using System.Reflection;
using GaussQuadrature;
using NewtonCotes;

class Test
{
    static void Main()
    {
        Console.WriteLine("Gauss-Legendre Tests:");
        TestLegendre();

        Console.WriteLine("\nGauss-Hermite Tests:");
        TestHermite();

        Console.WriteLine("\nGauss-Laguerre Tests:");
        TestLaguerre();

        Console.WriteLine("\nGauss-Chebyshev Tests:");
        TestChebyshev();

        Console.WriteLine("\nExponential Transform Tests:");
        TestExponential();

        Console.WriteLine("\nNewton-Cotes Closed Tests:");
        TestNewtonCotesClosed();

        Console.WriteLine("\nNewton-Cotes Open Tests:");
        TestNewtonCotesOpen();
    }

    // =========================
    //  Gauss–Legendre ([-1,1])
    // =========================
    static void TestLegendre()
    {
        var leg = new Legendre();

        Func<double, double> f2 = x => x * x * x + x * x;
        double result2 = CallNonPublic(leg, "SecondDegree", f2, -1.0, 1.0, 0.0);
        Console.WriteLine($"Legendre.SecondDegree: {result2} (expected ~0.6667)");

        Func<double, double> f3 = x => Math.Pow(x, 5) + Math.Pow(x, 4);
        double result3 = CallNonPublic(leg, "ThirdDegree", f3, -1.0, 1.0, 0.0);
        Console.WriteLine($"Legendre.ThirdDegree: {result3} (expected ~0.4)");

        Func<double, double> f4 = x => Math.Pow(x, 7) + Math.Pow(x, 6);
        double result4 = CallNonPublic(leg, "FourthDegree", f4, -1.0, 1.0, 0.0);
        Console.WriteLine($"Legendre.FourthDegree: {result4} (expected ~0.2857)");
    }

    // =========================
    //  Gauss–Hermite (R, weight e^{-x^2})
    // =========================
    static void TestHermite()
    {
        var herm = new Hermite();
        Func<double, double> f = x => 1.0;
        double expected = Math.Sqrt(Math.PI);

        Console.WriteLine($"Hermite.SecondDegree: {CallNonPublic(herm, "SecondDegree", f)} (expected ~{expected})");
        Console.WriteLine($"Hermite.ThirdDegree: {CallNonPublic(herm, "ThirdDegree", f)} (expected ~{expected})");
        Console.WriteLine($"Hermite.FourthDegree: {CallNonPublic(herm, "FourthDegree", f)} (expected ~{expected})");
    }

    // =========================
    //  Gauss–Laguerre ([0,∞), weight e^{-x})
    // =========================
    static void TestLaguerre()
    {
        var lag = new Laguerre();
        Func<double, double> f = x => 1.0;

        Console.WriteLine($"Laguerre.SecondDegree: {CallNonPublic(lag, "SecondDegree", f)} (expected ~1.0)");
        Console.WriteLine($"Laguerre.ThirdDegree: {CallNonPublic(lag, "ThirdDegree", f)} (expected ~1.0)");
        Console.WriteLine($"Laguerre.FourthDegree: {CallNonPublic(lag, "FourthDegree", f)} (expected ~1.0)");
    }

    // =========================
    //  Gauss–Chebyshev ([-1,1], weight 1/sqrt(1-x^2))
    // =========================
    static void TestChebyshev()
    {
        var cheb = new Chebyshev();
        Func<double, double> f = x => 1.0;
        double expected = Math.PI;

        Console.WriteLine($"Chebyshev.SecondDegree: {CallNonPublic(cheb, "SecondDegree", f)} (expected ~{expected})");
        Console.WriteLine($"Chebyshev.ThirdDegree: {CallNonPublic(cheb, "ThirdDegree", f)} (expected ~{expected})");
        Console.WriteLine($"Chebyshev.FourthDegree: {CallNonPublic(cheb, "FourthDegree", f)} (expected ~{expected})");
    }

    // =========================
    //  Exponential transformations
    // =========================
    static void TestExponential()
    {
        var exp = new Exponential();
        Func<double, double> f = x => x * x;

        Console.WriteLine($"Exponential.Simple: {CallNonPublic(exp, "Simple", f, 0.0, 1.0)} (expected ~0.3333)");
        Console.WriteLine($"Exponential.Double: {CallNonPublic(exp, "Double", f, 0.0, 1.0)} (expected ~0.3333)");
    }

    // =========================
    //  Newton–Cotes (Closed)
    // =========================
    static void TestNewtonCotesClosed()
    {
        var closed = new Closed();
        Func<double, double> f = x => x * x;

        Console.WriteLine($"Closed.Trapezoidal: {CallNonPublic(closed, "Trapezoidal", f, 0.0, 1.0, 1000)} (expected ~0.3333)");
        Console.WriteLine($"Closed.Simpson: {CallNonPublic(closed, "Simpson", f, 0.0, 1.0, 1000)} (expected ~0.3333)");
        Console.WriteLine($"Closed.ThreeEighthsSimpson: {CallNonPublic(closed, "ThreeEighthsSimpson", f, 0.0, 1.0, 999)} (expected ~0.3333)");
        Console.WriteLine($"Closed.Boole: {CallNonPublic(closed, "Boole", f, 0.0, 1.0, 1000)} (expected ~0.3333)");
    }

    // =========================
    //  Newton–Cotes (Open)
    // =========================
    static void TestNewtonCotesOpen()
    {
        var open = new Open();
        Func<double, double> f = x => x * x;
        double eps = 1e-6;

        Console.WriteLine($"Open.FirstDegree: {CallNonPublic(open, "FirstDegree", f, 0.0, 1.0, eps)} (expected ~0.3333)");
        Console.WriteLine($"Open.SecondDegree: {CallNonPublic(open, "SecondDegree", f, 0.0, 1.0, eps)} (expected ~0.3333)");
        Console.WriteLine($"Open.ThirdDegree: {CallNonPublic(open, "ThirdDegree", f, 0.0, 1.0, eps)} (expected ~0.3333)");
        Console.WriteLine($"Open.FourthDegree: {CallNonPublic(open, "FourthDegree", f, 0.0, 1.0, eps)} (expected ~0.3333)");
    }

    // Reflection helper
    static double CallNonPublic(object instance, string method, params object[] args)
    {
        var mi = instance.GetType().GetMethod(method,
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (mi == null)
            throw new MissingMethodException(instance.GetType().FullName, method);
        return (double)mi.Invoke(instance, args);
    }
}
