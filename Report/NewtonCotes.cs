using MathNet.Numerics;
namespace NewtonCotes
{
    // AS formulas fechadas sao todas utilizando as versoes compostas das regras para melhores aproximacoes.
    class Closed
    {
        double Trapezoidal(Func<double, double> f, double a, double b, int n)
        {
            double s = f(a) + f(b);
            double h = (b - a) / n;
            for (int step = 1; step < n; step++)
            {
                s += f(a + step * h);
            }

            return s * h;
        }
        double Simpson(Func<double, double> f, double a, double b, int n)
        {
            double s = f(a) + f(b);
            double h = (b - a) / n;

            for (int step = 1; step < n; step++)
            {
                if (step % 2 != 0)
                {
                    s += 4 * f(a + step * h);
                }
                else
                    s += 2 * f(a + step * h);
            }
            return s * (h / 3);
        }
        double ThreeEighthsSimpson(Func<double, double> f, double a, double b, int n)
        {
            double s = f(a) + f(b);
            double h = (b - a) / n;

            for (int step = 1; step < n; step++)
            {
                if (step % 3 == 0)
                {
                    s += 2 * f(a + step * h);
                }
                else
                    s += 3 * f(a + step * h);
            }
            return s * ((3 * h) / 8);
        }
        double Boole(Func<double, double> f, double a, double b, int n)
        {
            double s = 7 * f(a) + 7 * f(b);
            double h = (b - a) / n;

            for (int step = 1; step < n; step++)
            {
                if (step % 2 != 0)
                {
                    s += 32 * f(a + step * h);
                }
                else if (step % 4 == 0)
                    s += 14 * f(a + step * h);
                else
                    s += 12 * f(a + step * h);
            }
            return s * ((2 * h) / 45);
        }
    }

    // Nas formulas abertas foi usado recursao com medicao de erro para obter a composicao e, assim, melhores aproximacoes
    class Open
    {
        double FirstDegree(Func<double, double> f, double a, double b, double e)
        {
            double h = (b - a) / 2;
            double integral = 2 * h * f(a + h);

            double mid = (a + b) / 2;

            double hl = (mid - a) / 2;
            double integralfh = 2 * hl * f(a + hl);

            double hr = (b - mid) / 2;
            double integralsh = 2 * hr * f(mid + hr);

            double sum = integralfh + integralsh;
            if (Math.Abs(integral - sum) < e) { return sum; }

            double ne = e / 2;
            double leftSide = FirstDegree(f, a, mid, ne);
            double rightSide = FirstDegree(f, mid, b, ne);
            return leftSide + rightSide;

        }
        double SecondDegree(Func<double, double> f, double a, double b, double e)
        {
            double h = (b - a) / 3;
            double integral = 1.5 * h * (f(a + h) + f(a + 2 * h));

            double mid = (a + b) / 2;

            double hl = (mid - a) / 3;
            double integralfh = 1.5 * hl * (f(a + hl) + f(a + 2 * hl));


            double hr = (b - mid) / 3;
            double integralsh = 1.5 * hr * (f(mid + hr) + f(a + 2 * hr));

            double sum = integralfh + integralsh;
            if (Math.Abs(integral - sum) < e) { return sum; }

            double ne = e / 2;
            double leftSide = FirstDegree(f, a, mid, ne);
            double rightSide = FirstDegree(f, mid, b, ne);
            return leftSide + rightSide;
        }
        double ThirdDegree(Func<double, double> f, double a, double b, double e)
        {
            double h = (b - a) / 4;
            double integral = (4 / 3) * h * (2 * f(a + h) - f(a + 2 * h) + 2 * f(a + 3 * h));

            double mid = (a + b) / 2;

            double hl = (mid - a) / 4;
            double integralfh = (4 / 3) * hl * (2 * f(a + hl) - f(a + 2 * hl) + 2 * f(a + 3 * hl));

            double hr = (b - mid) / 4;
            double integralsh = (4 / 3) * hr * (2 * f(mid + hr) - f(mid + 2 * (hr)) + 2 * f(mid + 3 * hr));

            double sum = integralfh + integralsh;
            if (Math.Abs(integral - sum) < e) { return sum; }

            double ne = e / 2;
            double leftSide = FirstDegree(f, a, mid, ne);
            double rightSide = FirstDegree(f, mid, b, ne);
            return leftSide + rightSide;
        }
        double FourthDegree(Func<double, double> f, double a, double b, double e)
        {
            double h = (b - a) / 5;
            double integral = (5 / 24) * h * (2 * f(a + h) - f(a + 2 * h) + 2 * f(a + 3 * h));

            double mid = (a + b) / 2;

            double hl = (mid - a) / 5;
            double integralfh = (5 / 24) * hl * (11 * f(a + hl) + f(a + 2 * hl) + f(a + 3 * hl) + 11 * f(a + 4 * hl));

            double hr = (b - mid) / 5;
            double integralsh = (5 / 24) * hr * (11 * f(mid + hr / 5) + f(mid + 2 * hr) + 2 * f(mid + 3 * hr));

            double sum = integralfh + integralsh;
            if (Math.Abs(integral - sum) < e) { return sum; }

            double ne = e / 2;
            double leftSide = FirstDegree(f, a, mid, ne);
            double rightSide = FirstDegree(f, mid, b, ne);
            return leftSide + rightSide;
        }
    }

}