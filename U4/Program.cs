using System;
using MathNet.Numerics.LinearAlgebra;
using EulerMethod;
using RungeKutta;
using PredictorCorrector;

class TestIVP
{
    // Shared derivative function: dy/dt = y
    // Works for both Euler/RungeKutta delegate and PredictorCorrector delegate
    static Vector<double> F(Matrix<double> state, int index, double t)
    {
        // y' = y
        return state.Row(index);
    }

    static void Main()
    {
        // Problem setup
        double t0 = 0.0;
        double tf = 1.0;
        int steps = 10;
        double h = (tf - t0) / steps;

        // initial condition y(0) = 1
        var y0 = Vector<double>.Build.DenseOfArray(new[] { 1.0 });

        Console.WriteLine("ODE: y' = y,  y(0) = 1,  exact: y(t) = exp(t)\n");

        // Euler
        RunEuler("Explicit Euler", new ExplicitEuler(), F, y0, t0, h, steps);
        RunEuler("Implicit Euler", new ImplicitEuler(), F, y0, t0, h, steps);

        // Runge–Kutta
        RunRK2("Runge-Kutta 2 (Heun-like)", new RungeKuttaSecondOrder(), F, y0, t0, h, steps);
        RunRK3("Runge-Kutta 3", new RungeKuttaThirdOrder(), F, y0, t0, h, steps);
        RunRK4("Runge-Kutta 4 (classic)", new RungeKuttaFourthOrder(), F, y0, t0, h, steps);

        // Predictor–Corrector (Adams-Bashforth/Moulton)
        double tol = 1e-12;
        RunPC2("Predictor-Corrector 2nd order", new AdamsBashforth2(), PredictorCorrectorEval, y0, t0, h, steps, tol);
        RunPC3("Predictor-Corrector 3rd order", new AdamsBashforth3(), PredictorCorrectorEval, y0, t0, h, steps, tol);
        RunPC4("Predictor-Corrector 4th order", new AdamsBashforth4(), PredictorCorrectorEval, y0, t0, h, steps, tol);
    }

    // Adapter so we can pass the exact same underlying F to PredictorCorrector,
    // which defines its own (but identical) delegate type.
    static Vector<double> PredictorCorrectorEval(Matrix<double> s, int idx, double t) => F(s, idx, t);

    // -------------------------------------------
    // Euler loops
    // -------------------------------------------
    static void RunEuler(string title, object eulerObj, EulerMethod.DerivativeFunc fc,
                         Vector<double> y0, double t0, double h, int steps)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine(title);
        Console.WriteLine("t\t\tnumeric\t\t\texact\t\t\terror");

        double t = t0;
        var y = y0.Clone();

        // method resolution
        Func<EulerMethod.DerivativeFunc, Vector<double>, double, double, EulerResult> step;
        if (eulerObj is ExplicitEuler ee)
            step = ee.Execute;
        else if (eulerObj is ImplicitEuler ie)
            step = ie.Execute;
        else
            throw new ArgumentException("Unsupported Euler object");

        PrintLine(t, y[0]);

        for (int i = 0; i < steps; i++)
        {
            var res = step(fc, y, t, h);
            y = res.NextState;
            t = res.NextTime;
            PrintLine(t, y[0]);
        }

        Console.WriteLine();
    }

    // -------------------------------------------
    // RK loops
    // -------------------------------------------
    static void RunRK2(string title, RungeKuttaSecondOrder rk, EulerMethod.DerivativeFunc fc,
                       Vector<double> y0, double t0, double h, int steps)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine(title);
        Console.WriteLine("t\t\tnumeric\t\t\texact\t\t\terror");
        double t = t0;
        var y = y0.Clone();

        PrintLine(t, y[0]);

        for (int i = 0; i < steps; i++)
        {
            var res = rk.Execute(fc, y, t, h);
            y = res.State;
            t = res.Time;
            PrintLine(t, y[0]);
        }

        Console.WriteLine();
    }

    static void RunRK3(string title, RungeKuttaThirdOrder rk, EulerMethod.DerivativeFunc fc,
                       Vector<double> y0, double t0, double h, int steps)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine(title);
        Console.WriteLine("t\t\tnumeric\t\t\texact\t\t\terror");
        double t = t0;
        var y = y0.Clone();

        PrintLine(t, y[0]);

        for (int i = 0; i < steps; i++)
        {
            var res = rk.Execute(fc, y, t, h);
            y = res.State;
            t = res.Time;
            PrintLine(t, y[0]);
        }

        Console.WriteLine();
    }

    static void RunRK4(string title, RungeKuttaFourthOrder rk, EulerMethod.DerivativeFunc fc,
                       Vector<double> y0, double t0, double h, int steps)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine(title);
        Console.WriteLine("t\t\tnumeric\t\t\texact\t\t\terror");
        double t = t0;
        var y = y0.Clone();

        PrintLine(t, y[0]);

        for (int i = 0; i < steps; i++)
        {
            var res = rk.Execute(fc, y, t, h);
            y = res.State;
            t = res.Time;
            PrintLine(t, y[0]);
        }

        Console.WriteLine();
    }

    // -------------------------------------------
    // Predictor-Corrector loops
    // -------------------------------------------
    static void RunPC2(string title, AdamsBashforth2 pc, PredictorCorrector.DerivativeFunc fc,
                       Vector<double> y0, double t0, double h, int steps, double tol)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine(title);
        Console.WriteLine("t\t\tnumeric\t\t\texact\t\t\terror");

        double t = t0;
        var y = y0.Clone();

        PrintLine(t, y[0]);

        for (int i = 0; i < steps; i++)
        {
            var res = pc.Execute(fc, y, t, h, tol);
            y = res.State;
            t = res.Time;
            PrintLine(t, y[0]);
        }

        Console.WriteLine();
    }

    static void RunPC3(string title, AdamsBashforth3 pc, PredictorCorrector.DerivativeFunc fc,
                       Vector<double> y0, double t0, double h, int steps, double tol)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine(title);
        Console.WriteLine("t\t\tnumeric\t\t\texact\t\t\terror");

        double t = t0;
        var y = y0.Clone();

        PrintLine(t, y[0]);

        for (int i = 0; i < steps; i++)
        {
            var res = pc.Execute(fc, y, t, h, tol);
            y = res.State;
            t = res.Time;
            PrintLine(t, y[0]);
        }

        Console.WriteLine();
    }

    static void RunPC4(string title, AdamsBashforth4 pc, PredictorCorrector.DerivativeFunc fc,
                       Vector<double> y0, double t0, double h, int steps, double tol)
    {
        Console.WriteLine("==================================================");
        Console.WriteLine(title);
        Console.WriteLine("t\t\tnumeric\t\t\texact\t\t\terror");

        double t = t0;
        var y = y0.Clone();

        PrintLine(t, y[0]);

        for (int i = 0; i < steps; i++)
        {
            var res = pc.Execute(fc, y, t, h, tol);
            y = res.State;
            t = res.Time;
            PrintLine(t, y[0]);
        }

        Console.WriteLine();
    }

    // -------------------------------------------
    // Helpers
    // -------------------------------------------
    static void PrintLine(double t, double y)
    {
        double exact = Math.Exp(t);
        double err = Math.Abs(y - exact);
        Console.WriteLine($"{t:F4}\t\t{y:E8}\t{exact:E8}\t{err:E8}");
    }
}
