using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using static System.Math;

FiniteDifference fd = new FiniteDifference();
Func<double, double> f = x => (Exp(-x) - Exp(x)) / (Exp(-1) - Exp(1));

Console.WriteLine("Exato");
Console.WriteLine("f(x) = " + f(0.25).ToString() + " at x = 0.25");
Console.WriteLine("f(x) = " + f(0.50).ToString() + " at x = 0.50");
Console.WriteLine("f(x) = " + f(0.75).ToString() + " at x = 0.75 \n");

Console.WriteLine("Aproximado pelo Metodo das Diferencas Finitas");
Vector<double> r =
fd.Single(f, 0, 0, 1, 1, 0.25);
float i = 0.25f;
foreach (var item in r)
{
    Console.WriteLine($"u({i}) = " + item.ToString());
    i += (float)0.25;
}

