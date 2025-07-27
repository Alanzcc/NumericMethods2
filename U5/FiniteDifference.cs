using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

class FiniteDifference
{
    //Funcao a ser calculada com os valores de dirichlet e 
    // u"(x) + 7*u'(x) - u(x) = 2
    // u(0) = 10 e u(2) = 1
    // Δx = 0.1

    // Derivadas centrais de segunda ordem
    // -2
    // f(x)
    // (f(x + h) - f(x - h)) / (2 * h);
    //  (f(x + h) - 2 * f(x) + f(x - h)) / (h * h)

    // Divisao das constantes que multiplicam cada celula
    // f(x+h) ( 1 / (h*h) + 7 / (2 * h) ) 
    // f(x) ( -2 / (h * h) + 1)
    // f(x-h) ( 1 / (h * h) - 7 / (2 * h) )
    // -2

    public Vector<double> Single(double init_cond_x, double init_cond_y, double end_cond_x, double end_cond_y, double h)
    {


        // end_cond_x > init_cond_x
        int n = (int)((end_cond_x - init_cond_x) / h); // qtde de pontos que vamos aproximar
        Matrix<double> A = new DenseMatrix(n, n);
        Vector<double> b = Vector<double>.Build.Dense(n, 2);
        b[0] = 0;
        for (int i = 1; i < n; i++)
        {
            if (i - 1 == 0)
            {
                b[i] += -init_cond_y * (1 / (h * h) - 7 / (2 * h));

                Vector<double> newARow = Vector<double>.Build.Dense(n);
                newARow[i] = -(2 / (h * h) + 1);
                newARow[i + 1] = 1 / (h * h) + 7 / (2 * h);
                A.SetRow(i, newARow);
            }
            else if (i + 1 == n)
            {
                b[i] += -end_cond_y * (1 / (h * h) + 7 / (2 * h));

                Vector<double> newARow = Vector<double>.Build.Dense(n);
                newARow[i - 1] = 1 / (h * h) - 7 / (2 * h);
                newARow[i] = -(2 / (h * h) + 1);
                A.SetRow(i, newARow);
            }
            else
            {
                Vector<double> newARow = Vector<double>.Build.Dense(n);
                newARow[i - 1] = 1 / (h * h) - 7 / (2 * h);
                newARow[i] = -(2 / (h * h) + 1);
                newARow[i + 1] = 1 / (h * h) + 7 / (2 * h);
                A.SetRow(i, newARow);
            }
        }
        Console.WriteLine(b);
        Console.WriteLine(A);
        return A.Solve(b);
    }
}

