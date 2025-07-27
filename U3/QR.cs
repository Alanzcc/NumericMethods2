using System;
using MathNet.Numerics.LinearAlgebra;

public static class QRMethods
{
    public class QRResult
    {
        public required Matrix<double> Lambda { get; set; }  // Autovalores na diagonal
        public required Matrix<double> X { get; set; }       // Autovetores
    }

    public static Matrix<double> NewIdentityMatrix(int rows, int cols)
    {
        var identity = Matrix<double>.Build.Dense(rows, cols, 0.0);
        int min = Math.Min(rows, cols);
        for (int i = 0; i < min; i++)
        {
            identity[i, i] = 1.0;
        }
        return identity;
    }

    public static (Matrix<double> Q, Matrix<double> R) QRDecomp(Matrix<double> A)
    {
        int n = A.RowCount;
        int m = A.ColumnCount;

        var R = A.Clone();
        var Q = NewIdentityMatrix(n, m);

        for (int j = 0; j < m; j++)
        {
            for (int i = n - 1; i > j; i--)
            {
                double a = R[i - 1, j];
                double b = R[i, j];

                double r = Math.Sqrt(a * a + b * b);
                if (Math.Abs(r) < 1e-15)
                    continue;

                double cos = a / r;
                double sin = -b / r;

                var G = NewIdentityMatrix(n, n);
                G[i - 1, i - 1] = cos;
                G[i, i] = cos;
                G[i - 1, i] = -sin;
                G[i, i - 1] = sin;

                R = G * R;
                Q = Q * G.Transpose();
            }
        }

        return (Q, R);
    }

    public static QRResult QRMethod(Matrix<double> T, Matrix<double> H, double epsilon)
    {
        int n = T.RowCount;
        var A = T.Clone();
        var X = H.Clone();
        double error = double.MaxValue;

        int maxIterations = 1000;
        int iteration = 0;

        while (error > epsilon && iteration < maxIterations)
        {
            var (Q, R) = QRDecomp(A);
            A = R * Q;
            X = X * Q;

            error = 0.0;
            for (int j = 0; j < n - 1; j++)
            {
                error += Math.Abs(A[j + 1, j]);
            }

            iteration++;
        }

        if (iteration == maxIterations)
        {
            Console.WriteLine("?? QRMethod: Máximo de iterações atingido sem convergência.");
        }

        return new QRResult
        {
            Lambda = A,
            X = X
        };
    }
}
