using Derivative;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Security.Cryptography;

class FiniteDifference
{

    public Vector<double> Single(Func<double, double> f, double init_cond_x, double init_cond_y, double end_cond_x, double end_cond_y, double h)
    {
        // u"(x) + 7*u'(x) - u(x) = 2
        // u(0) = 10 e u(2) = 1
        // Δx = 0.1

        // end_cond_x > init_cond_x
        int n = (int)((end_cond_x - init_cond_x) / h); // qtde de pontos que vamos aproximar

        Central cen = new Central(f);
        Matrix<double> A = new DenseMatrix(n + 1, 3);
        Vector<double> b = Vector<double>.Build.Dense(n + 1, 999);
        b[0] = init_cond_y; // Condicao de contorno inicial
        b[n] = end_cond_y; // Condicao de contorno final
        for (int i = 1; i < n; i++)
        {
            //  f(x - h) * (1 / (h * h) - 1 / (2 * h)) +
            //  f(x) * (-2 / (h * h) - 1) +
            //  f(x + 1) * (1 / (h * h) + 1 / (2 * h));
            if (i - 1 == 0)
            {
                b[i] = init_cond_y * -1 / (h * h);

                Vector<double> newARow = DenseVector
                    .OfArray(new double[] {
                        -(2 / (h * h) + 1),
                        1 / (h * h),
                        0
                    });
                A.SetRow(i, newARow);
            }
            else if (i + 1 == n)
            {
                b[i] = end_cond_y * -1 / (h * h);

                Vector<double> newARow = DenseVector
                    .OfArray(new double[] {
                        0,
                        1 / (h * h),
                        -(2 / (h * h) + 1)
                    });
                A.SetRow(i, newARow);
            }
            else
            {
                b[i] = 0;

                Vector<double> newARow = DenseVector
                    .OfArray(new double[] {
                        1 / (h * h),
                        -(2 / (h * h) + 1),
                        1 / (h * h)
                    });
                A.SetRow(i, newARow);
            }
        }
        return A.Solve(b);
    }
}