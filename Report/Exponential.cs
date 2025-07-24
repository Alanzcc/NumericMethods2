using Gauss.Hermite;
class Exponential
{
    double Simple(Func<double, double> f, double a, double b)
    {
        double ex = Math.Exp(Math.Pow(s, 2));
        double f_dash = f(((a + b) / 2 + ((b - a) / 2) * Math.Tanh(s)));
        double dxds = ((b - a) / 2) * (1 / Math.Pow(Math.Cosh(s), 2));
        Func<double, double> f_two_dash = s => ex * f_dash * dxds;
        Hermite gh = new Hermite();
        return gh.FourthDegree(f_two_dash);

    }
    double Double(Func<double, double> f, double a, double b)
    {
        double ex = Math.Exp(Math.Pow(s, 2));
        double f_dash = f((a + b) / 2 + ((b - a) / 2) * Math.Tanh((Math.PI / 2) * Math.Sinh(s)));
        double dxds = ((b - a) / 2) * ((Math.PI / 2) * (Math.Cosh(s) /
        Math.Pow(Math.Cosh((Math.PI / 2) * Math.Sinh(s)), 2)));
        Func<double, double> f_two_dash = s => ex * f_dash * dxds;
        Hermite gh = new Hermite();
        return gh.FourthDegree(f_two_dash);


    }
}