using MathNet.Numerics.LinearAlgebra;

public class Euler
{
    // Using forward difference
    double ExplicitEulerMethod(func<double, double> y,
     func<double, double> de_y, double y0, double t0, double delta_t)
    {
        double f = (S(t0 + delta_t) - s0) / delta_t;
        return old_s + delta_t * f;
    }

    Vector<double> ExplicitEulerMethod(func<Vector<double>, Vector<double>> y,
     func<Vector<double>, Vector<double>> de_y, Vector<double> y0, double t0, double delta_t)
    {
        
     }
}