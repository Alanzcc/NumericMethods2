namespace Derivative
{
    /* FORWARD */
    public class Forward
    {
        private readonly Func<double, double> f;

        public Forward(Func<double, double> f)
        {
            this.f = f;
        }

        // First Derivative
        double FstDe1stO(double x, double h)
        {
            return (f(x + h) - f(x)) / h;
        }
        double FstDe2ndO(double x, double h)
        {
            return (-f(x + 2 * h) + 4 * f(x + h) - 3 * f(x)) / (2 * h);
        }
        double FstDe3rdO(Func<double, double> f, double x, double h)
        {
            return (2.0 * f(x + 3 * h) - 9.0 * f(x + 2 * h) + 18.0 * f(x + h) - 11.0 * f(x)) / (6.0 * h);
        }
        double FstDe4thO(Func<double, double> f, double x, double h)
        {
            return (-3 * f(x + 4 * h) + 16 * f(x + 3 * h) - 36 * f(x + 2 * h) + 48 * f(x + h) - 25 * f(x)) / (12 * h);
        }

        // Second Derivative
        double SndDe1stO(double x, double h)
        {
            return (f(x + 2 * h) - 2 * f(x + h) + f(x)) / (h * h);
        }
        double SndDe2ndO(double x, double h)
        {
            return (-f(x + 3 * h) + 4 * f(x + 2 * h) - 5 * f(x + h) + 2 * f(x)) / (h * h);

        }
        double SndDe3rdO(Func<double, double> f, double x, double h)
        {
            return (11 * f(x + 4 * h) - 56 * f(x + 3 * h) + 114 * f(x + 2 * h) - 104 * f(x + h) + 35 * f(x)) / (12 * h * h);
        }
        double SndDe4thO(Func<double, double> f, double x, double h)
        {
            return (-10 * f(x + 5 * h) + 61 * f(x + 4 * h) - 156 * f(x + 3 * h) + 214 * f(x + 2 * h) - 154 * f(x + h) + 45 * f(x)) / (12 * h * h);
        }

        // Third Derivative
        double TrdDe1stO(double x, double h)
        {
            return (f(x + 3 * h) - 3 * f(x + 2 * h) + 3 * f(x + 1) - f(x)) / (h * h * h);

        }
        double TrdDe2ndO(double x, double h)
        {
            return (-3 * f(x + 4 * h) + 14 * f(x + 3 * h) - 24 * f(x + 2 * h) + 18 * f(x + h) - 5 * f(x)) / (2 * h * h * h);

        }
        double TrdDe3rdO(Func<double, double> f, double x, double h)
        {
            return (7 * f(x + 5 * h) - 47 * f(x + 4 * h) + 122 * f(x + 3 * h) - 154 * f(x + 2 * h) + 95 * f(x + h) - 23 * f(x)) / (4 * h * h * h);
        }
        double TrdDe4thO(Func<double, double> f, double x, double h)
        {
            return (-15 * f(x + 6 * h) + 104 * f(x + 5 * h) - 319 * f(x + 4 * h) + 544 * f(x + 3 * h) - 533 * f(x + 2 * h) + 280 * f(x + h) - 61 * f(x)) / (8 * h * h * h);

        }
    }

    /* BACKWORD */
    public class Backword
    {
        private readonly Func<double, double> f;

        public Backword(Func<double, double> f)
        {
            this.f = f;
        }

        // First Derivative
        double FstDe1stO(double x, double h)
        {
            return (f(x) - f(x - h)) / h;
        }
        double FstDe2ndO(double x, double h)
        {
            return (3 * f(x) - 4 * f(x - h) + f(x - 2 * h)) / (2 * h);
        }
        double FstDe3rdO(Func<double, double> f, double x, double h)
        {
            return (11 * f(x) - 18 * f(x - h) + 9 * f(x - 2 * h) - 2 * f(x - 3 * h)) / (6 * h);
        }
        double FstDe4thO(Func<double, double> f, double x, double h)
        {
            return (25 * f(x) - 48 * f(x - h) + 36 * f(x - 2 * h) - 16 * f(x - 3 * h) + 3 * f(x - 4 * h)) / (12 * h);
        }

        // Second Derivative
        double SndDe1stO(double x, double h)
        {
            return (f(x) - 2 * f(x - h) + f(x - 2 * h)) / (h * h);
        }
        double SndDe2ndO(double x, double h)
        {
            return (2 * f(x) - 5 * f(x - h) + 4 * f(x - 2 * h) - f(x - 3 * h)) / (h * h);
        }
        double SndDe3rdO(Func<double, double> f, double x, double h)
        {
            return (35 * f(x) - 104 * f(x - h) + 114 * f(x - 2 * h) - 56 * f(x - 3 * h) + 11 * f(x - 4 * h)) / (12 * h * h);
        }
        double SndDe4thO(Func<double, double> f, double x, double h)
        {
            return (45 * f(x) - 154 * f(x - h) + 214 * f(x - 2 * h) - 156 * f(x - 3 * h) + 61 * f(x - 4 * h) - 10 * f(x - 5 * h)) / (12 * h * h);
        }

        // Third Derivative
        double TrdDe1stO(double x, double h)
        {
            return f(x) - 3 * f(x - h) + 3 * f(x - 2 * h) - f(x - 3 * h) / (h * h * h);
        }
        double TrdDe2ndO(double x, double h)
        {
            return (5 * f(x) - 18 * f(x - h) + 24 * f(x - 2 * h) - 14 * f(x - 3 * h) + 3 * f(x - 4 * h)) / (2 * h * h * h);
        }
        double TrdDe3rdO(Func<double, double> f, double x, double h)
        {
            return (17 * f(x) - 77 * f(x - h) + 142 * f(x - 2 * h) - 134 * f(x - 3 * h) + 65 * f(x - 4 * h) - 13 * f(x - 5 * h)) / (4 * h * h * h);
        }
        double TrdDe4thO(Func<double, double> f, double x, double h)
        {
            return 49 * f(x) - 232 * f(x - h) + 449 * f(x - 2 * h) - 448 * f(x - 3 * h) + 235 * f(x - 4 * h) - 56 * f(x - 5 * h) + 3 * f(x - 6 * h) / (8 * h * h * h);
        }

    }

    /* CENTRAL */
    public class Central
    {
        private readonly Func<double, double> f;

        public Central(Func<double, double> f)
        {
            this.f = f;
        }

        // First Derivative
        double FstDe2ndO(double x, double h)
        {
            return (f(x + h) - f(x - h)) / (2 * h);

        }
        double FstDe4thO(Func<double, double> f, double x, double h)
        {
            return (-f(x + 2 * h) + 8 * f(x + h) - 8 * f(x - h) + f(x - 2 * h)) / (12 * h);

        }

        // Second Derivative
        double SndDe1stO(double x, double h)
        {
            return 0;
        }
        double SndDe2ndO(double x, double h)
        {
            return (f(x + h) - 2 * f(x) + f(x - h)) / (h * h);

        }
        double SndDe4thO(Func<double, double> f, double x, double h)
        {
            return (-f(x + 2 * h) + 16 * f(x + h) - 30 * f(x) + 16 * f(x - h) - f(x - 2 * h)) / (12 * h * h);
        }

        // Third Derivative
        double TrdDe2ndO(double x, double h)
        {
            return (f(x + 2 * h) + -2 * f(x + h) + 2 * f(x - h) - f(x - 2 * h)) / (2 * h * h * h);
        }
        double TrdDe4thO(Func<double, double> f, double x, double h)
        {
            return (-f(x + 3 * h) + 8 * f(x + 2 * h) - 13 * f(x + h) + 13 * f(x - h) - 8 * f(x - 2 * h) + f(x - 3 * h)) / (8 * h * h * h);


        }


    }
}
