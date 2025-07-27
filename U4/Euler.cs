using System;
using MathNet.Numerics.LinearAlgebra;

public class Euler
{
    // Using forward difference
    double ExplicitEulerMethod(Func<double, double> y, double y0, double t0, double delta_t)
    {
        double de_y = (y(t0 + delta_t) - y0) / delta_t;
        return y0 + delta_t * de_y;
    }

    Vector<double> ExplicitEulerMethod(Func<double, Vector<double>> y, Vector<double> y0, double t0, double delta_t)
    {

        Vector<double> tmp = y(t0 + delta_t);
        if (tmp.Count != y0.Count) { throw new IndexOutOfRangeException("The function's vector has a different amount of components than y0"); }
        Vector<double> de_y = (y(t0 + delta_t) - y0) / delta_t;
        return y0 + delta_t * de_y;
    }
}