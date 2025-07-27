using GaussQuadrature;
using System;
class Exponential
{
    public double Simple(Func<double, double> f, double a, double b)
    {
        Func<double, double> ex = s => Math.Exp(Math.Pow(s, 2));
        Func<double, double> f_dash = s => f(((a + b) / 2 + ((b - a) / 2) * Math.Tanh(s)));
        Func<double, double> dxds = s => ((b - a) / 2) * (1 / Math.Pow(Math.Cosh(s), 2));
        Func<double, double> f_two_dash = s => ex(s) * f_dash(s) * dxds(s);
        Hermite gh = new Hermite();
        return gh.FourthDegree(f_two_dash);

    }
    public double Double(Func<double, double> f, double a, double b)
    {
        Func<double, double> ex = s => Math.Exp(Math.Pow(s, 2));
        Func<double, double> f_dash = s => f((a + b) / 2 + ((b - a) / 2) * Math.Tanh((Math.PI / 2) * Math.Sinh(s)));
        Func<double, double> dxds = s => ((b - a) / 2) * ((Math.PI / 2) * (Math.Cosh(s) /
        Math.Pow(Math.Cosh((Math.PI / 2) * Math.Sinh(s)), 2)));
        Func<double, double> f_two_dash = s => ex(s) * f_dash(s) * dxds(s);
        Hermite gh = new Hermite();
        return gh.FourthDegree(f_two_dash);


    }
}