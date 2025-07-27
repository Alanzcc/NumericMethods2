// QR_Householder_Test.cs
using System;
using MathNet.Numerics.LinearAlgebra;

class QRHouseholderTest
{
    static void Main()
    {
        TestQRDecomposition();
        TestHouseholderReduction();
        TestQREigenMethod();
    }

    // ------------------------- QR Decomposition -------------------------
    static void TestQRDecomposition()
    {
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("= QR DECOMPOSITION (Givens-based implementation) =");
        Console.WriteLine("==================================================");

        var A = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            { 12, -51,   4 },
            {  6, 167, -68 },
            { -4,  24, -41 }
        });

        var (Q, R) = QRMethods.QRDecomp(A);

        PrintMatrix("A", A);
        PrintMatrix("Q", Q);
        PrintMatrix("R", R);

        var Arec = Q * R;
        var I = Matrix<double>.Build.DenseIdentity(Q.ColumnCount);

        double recErr = (A - Arec).FrobeniusNorm();
        double orthErr = (Q.TransposeThisAndMultiply(Q) - I).FrobeniusNorm();

        Console.WriteLine("||A - Q*R||_F = " + recErr.ToString("E6"));
        Console.WriteLine("||Q^T Q - I||_F = " + orthErr.ToString("E6"));
    }

    // ------------------------- Householder Reduction -------------------------
    static void TestHouseholderReduction()
    {
        Console.WriteLine();
        Console.WriteLine("=========================================");
        Console.WriteLine("= HOUSEHOLDER (to almost tridiagonal)   =");
        Console.WriteLine("=========================================");

        var A = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            {  4,  1, -2,  2 },
            {  1,  2,  0,  1 },
            { -2,  0,  3, -2 },
            {  2,  1, -2, -1 }
        });

        var res = Householder.HouseholderMethod(A);

        PrintMatrix("A (original)", A);
        PrintMatrix("Tri (after Householder)", res.Tri);
        PrintMatrix("H (accumulated reflectors)", res.H);

        var HTAH = res.H.TransposeThisAndMultiply(A).Multiply(res.H);
        double triErr = (HTAH - res.Tri).FrobeniusNorm();

        Console.WriteLine("||H^T * A * H - Tri||_F = " + triErr.ToString("E6"));
    }

    // ------------------------- QR Eigen (QR iteration) -------------------------
    static void TestQREigenMethod()
    {
        Console.WriteLine();
        Console.WriteLine("==================================");
        Console.WriteLine("= QR ITERATION (Eigenvalues/Vects) =");
        Console.WriteLine("==================================");

        var A = Matrix<double>.Build.DenseOfArray(new double[,]
        {
            {  4,  1, -2,  2 },
            {  1,  2,  0,  1 },
            { -2,  0,  3, -2 },
            {  2,  1, -2, -1 }
        });

        var hh = Householder.HouseholderMethod(A);

        double eps = 1e-12;
        var qrRes = QRMethods.QRMethod(hh.Tri, hh.H, eps);

        Console.WriteLine("\nLambda (final matrix, eigenvalues on diagonal):");
        PrintMatrix("Lambda", qrRes.Lambda);

        Console.WriteLine("\nX (eigenvectors columns):");
        PrintMatrix("X", qrRes.X);

        Console.WriteLine("\nApproximate eigenvalues (diag of Lambda):");
        for (int i = 0; i < qrRes.Lambda.RowCount; i++)
        {
            Console.WriteLine("  lambda_" + (i + 1) + " = " + qrRes.Lambda[i, i].ToString("E16"));
        }

        Console.WriteLine("\nResiduals  ||A * x_i - lambda_i * x_i||_2:");
        for (int i = 0; i < qrRes.X.ColumnCount; i++)
        {
            var xi = qrRes.X.Column(i);
            double lambda = qrRes.Lambda[i, i];
            var r = A * xi - lambda * xi;
            Console.WriteLine("  i=" + (i + 1) + ": " + r.L2Norm().ToString("E6"));
        }
    }

    // ------------------------- Helpers -------------------------
    static void PrintMatrix(string name, Matrix<double> M)
    {
        Console.WriteLine("\n" + name + " (" + M.RowCount + "x" + M.ColumnCount + "):");
        for (int i = 0; i < M.RowCount; i++)
        {
            Console.Write("  ");
            for (int j = 0; j < M.ColumnCount; j++)
            {
                Console.Write(M[i, j].ToString("E6").PadLeft(15) + " ");
            }
            Console.WriteLine();
        }
    }
}
