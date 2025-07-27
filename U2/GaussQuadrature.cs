using System;

namespace GaussQuadrature
{

    public class Legendre
    {
        double x2p = 1 / Math.Sqrt(3);
        double x2m = -1 / Math.Sqrt(3);
        double w2 = 1;

        double x3n = 0;
        double w3n = 8 / 9;
        double x3p = Math.Sqrt(3 / 5);
        double x3m = -Math.Sqrt(3 / 5);
        double w3f = 5 / 9;

        double x4f1p = Math.Sqrt((3 - 2 * Math.Sqrt(6 / 5)) / 7);
        double x4f1m = -Math.Sqrt((3 - 2 * Math.Sqrt(6 / 5)) / 7);
        double w4p = (18 + Math.Sqrt(30)) / 36;

        double x4f2p = Math.Sqrt((3 + 2 * Math.Sqrt(6 / 5)) / 7);
        double x4f2m = -Math.Sqrt((3 + 2 * Math.Sqrt(6 / 5)) / 7);
        double w4m = (18 - Math.Sqrt(30)) / 36;

        public double SecondDegree(Func<double, double> f, double a, double b, double s)
        {
            return ((b - a) / 2) * (
                f((b - a) / 2 * x2p + (b + a) / 2) * w2 +
                f((b - a) / 2 * x2m + (b + a) / 2) * w2
            );
        }

        public double ThirdDegree(Func<double, double> f, double a, double b, double s)
        {
            return ((b - a) / 2) * (
                f((b - a) / 2 * x3n + (b + a) / 2) * w3n +
                f((b - a) / 2 * x3p + (b + a) / 2) * w3f +
                f((b - a) / 2 * x3m + (b + a) / 2) * w3f
            );
        }

        public double FourthDegree(Func<double, double> f, double a, double b, double s)
        {
            return ((b - a) / 2) * (
                f((b - a) / 2 * x4f1p + (b + a) / 2) * w4p +
                f((b - a) / 2 * x4f1m + (b + a) / 2) * w4p +
                f((b - a) / 2 * x4f2p + (b + a) / 2) * w4m +
                f((b - a) / 2 * x4f2m + (b + a) / 2) * w4m
            );
        }
    }
    public class Hermite
    {
        double x2p = 1 / Math.Sqrt(2);
        double x2m = -1 / Math.Sqrt(2);
        double w2 = Math.Sqrt(Math.PI) / 2;

        double x3n = 0;
        double w3n = 2 * Math.Sqrt(Math.PI) / 3;
        double x3fp = Math.Sqrt(3 / 2);
        double x3fm = -Math.Sqrt(3 / 2);
        double w3f = Math.Sqrt(Math.PI) / 6;

        double x4f1p = Math.Sqrt((3.0 + Math.Sqrt(6)) / 2.0);
        double x4f1m = -Math.Sqrt((3.0 + Math.Sqrt(6)) / 2.0);
        double w4f1 = Math.Sqrt(Math.PI) / (12 + 4.0 * Math.Sqrt(6));
        double x4f2p = Math.Sqrt((3.0 - Math.Sqrt(6)) / 2.0);
        double x4f2m = -Math.Sqrt((3.0 - Math.Sqrt(6)) / 2.0);
        double w4f2 = Math.Sqrt(Math.PI) / (12 - 4.0 * Math.Sqrt(6));

        public double SecondDegree(Func<double, double> f)
        {
            return w2 * (
                f(x2p) +
                f(x2m)
            );
        }
        public double ThirdDegree(Func<double, double> f)
        {
            return w3n * f(x3n) +
                   w3f * (f(x3fp) + f(x3fm));
        }
        public double FourthDegree(Func<double, double> f)
        {
            return w4f1 * (f(x4f1p) + f(x4f1m)) +
                   w4f2 * (f(x4f2p) + f(x4f2m));
        }
    }
    public class Laguerre
    {
        double x2p = 2 + Math.Sqrt(2);
        double w2p = (1 / 4) * (2 - Math.Sqrt(2));
        double x2m = 2 - Math.Sqrt(2);
        double w2m = (1 / 4) * (2 + Math.Sqrt(2));

        double x3f1 = 0.4157745568;
        double w3f1 = 0.7110930099;
        double x3f2 = 2.2942803603;
        double w3f2 = 0.2785177336;
        double x3f3 = 6.2899450829;
        double w3f3 = 0.0103892565;

        double x4f1 = 0.3225476896;
        double w4f1 = 0.6031541043;
        double x4f2 = 1.7457611011;
        double w4f2 = 0.3574186924;
        double x4f3 = 4.5366202969;
        double w4f3 = 0.0388879085;
        double x4f4 = 9.3950709123;
        double w4f4 = 0.0005392945;


        public double SecondDegree(Func<double, double> f)
        {
            return w2p * f(x2p) + w2m * f(x2m);
        }
        double ThirdDegree(Func<double, double> f)
        {
            return w3f1 * f(x3f1) + w3f2 * f(x3f2) + w3f3 * f(x3f3);
        }
        public double FourthDegree(Func<double, double> f)
        {
            return w4f1 * f(x4f1) + w4f2 * f(x4f2) +
                   w4f3 * f(x4f3) + w4f4 * f(x4f4);
        }
    }
    public class Chebyshev
    {
        double x2p = 1 / Math.Sqrt(2);
        double x2m = -1 / Math.Sqrt(2);
        double w2 = Math.PI / 2;

        double x3n = 0;
        double x3p = Math.Sqrt(3) / 2;
        double x3m = -Math.Sqrt(3) / 2;
        double w3 = Math.PI / 3;

        double x4f1p = Math.Sqrt(2 + Math.Sqrt(2)) / 2.0;
        double x4f1m = -Math.Sqrt(2 + Math.Sqrt(2)) / 2.0;
        double x4f2p = Math.Sqrt(2 - Math.Sqrt(2)) / 2.0;
        double x4f2m = -Math.Sqrt(2 - Math.Sqrt(2)) / 2.0;
        double w4 = Math.PI / 4;

        public double SecondDegree(Func<double, double> f)
        {

            return w2 * (
                f(x2p) +
                f(x2m)
            );
        }

        public double ThirdDegree(Func<double, double> f)
        {
            return w3 * (
                f(x3n) +
                f(x3p) +
                f(x3m)
            );
        }
        public double FourthDegree(Func<double, double> f)
        {
            return w4 * (
                f(x4f1p) +
                f(x4f1m) +
                f(x4f2p) +
                f(x4f2m)
            );
        }
    }


}
