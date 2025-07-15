using System;
using MathNet.Numerics.LinearAlgebra;

public static class Householder
{

    public static Matrix<double> NewIdentityMatrix(int rows, int cols)
    {
        var identity = Matrix<double>.Build.Dense(rows, cols);
        int min = Math.Min(rows, cols);
        for (int i = 0; i < min; i++)
            identity[i, i] = 1.0;
        return identity;
    }
    public class HouseholderResult
    {
        public required Matrix<double> Tri { get; set; }
        public required Matrix<double> H { get; set; }
    }

    public static Matrix<double> HouseholderMatrix(Matrix<double> A, int i)
    {
        int n = A.RowCount;
        var I = NewIdentityMatrix(n, n);

        var w = Vector<double>.Build.Dense(n);
        var wLinha = Vector<double>.Build.Dense(n);

        var col = A.Column(i);
        for (int j = i + 1; j < n; j++)
        {
            w[j] = col[j];
        }

        double Lw = w.L2Norm();

        if (i + 1 < n)
            wLinha[i + 1] = Lw;

        var N = w - wLinha;
        double normN = N.L2Norm();

        Vector<double> n_vec = normN != 0 ? N / normN : Vector<double>.Build.Dense(n);

        var outerProd = n_vec.OuterProduct(n_vec);  // n * n^T
        var H = I - outerProd.Multiply(2.0);

        return H;
    }

    public static HouseholderResult HouseholderMethod(Matrix<double> A)
    {
        int n = A.RowCount;
        var H = NewIdentityMatrix(n, n);
        var A_old = A.Clone();

        for (int i = 0; i < n - 2; i++)
        {
            var Hi = HouseholderMatrix(A_old, i);

            var temp = A_old.Multiply(Hi);
            A_old = Hi.Transpose().Multiply(temp);

            H = H.Multiply(Hi);
        }

        return new HouseholderResult
        {
            Tri = A_old,
            H = H
        };
    }
}

