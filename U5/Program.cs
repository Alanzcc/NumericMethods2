using MathNet.Numerics.LinearAlgebra;
using static System.Math;


Func<double, double> f = x => (Exp(-x) - Exp(x)) / (Exp(-1) - Exp(1));
FiniteDifference fd = new FiniteDifference();
Vector<double> r = fd.Single(0, 10, 2, 1, 0.1);
double x = 0.0;
foreach (var item in r)
{
    if (!double.IsNaN(item))
    {
        x += 0.1;
        Console.WriteLine($"Os valores de u({x}) sao: " + item);
        continue;
    }
}

