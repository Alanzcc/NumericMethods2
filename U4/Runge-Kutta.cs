// RungeKuttaMethods.cs
using System;
using MathNet.Numerics.LinearAlgebra;
using EulerMethod;

namespace RungeKutta
{
    public sealed class RungeKuttaResult
    {
        public double Time { get; }
        public Vector<double> State { get; }

        public RungeKuttaResult(double time, Vector<double> state)
        {
            Time = time;
            State = state;
        }
    }

    // ---------------- SECOND ORDER (uses Implicit Euler as predictor) ----------------
    public sealed class RungeKuttaSecondOrder
    {
        private readonly ImplicitEuler _implicitEuler = new ImplicitEuler();

        private RungeKuttaResult GetGuess(
            DerivativeFunc fc,
            Vector<double> y0,
            double t0,
            double h)
        {
            var emRes = _implicitEuler.Execute(fc, y0, t0, h);
            return new RungeKuttaResult(emRes.NextTime, emRes.NextState);
        }

        public RungeKuttaResult Execute(
            DerivativeFunc fc,
            Vector<double> y0,
            double t0,
            double h)
        {
            int c = y0.Count;

            // y_hat at t+h using implicit Euler
            var guess = GetGuess(fc, y0, t0, h);

            // Build a 2xN matrix to pass both states to fc
            var states = Matrix<double>.Build.Dense(2, c);
            states.SetRow(0, y0);
            states.SetRow(1, guess.State);

            var f0 = fc(states, 0, t0);
            var f1 = fc(states, 1, t0 + h);

            var y1 = y0 + (f0 + f1) * (h / 2.0);
            return new RungeKuttaResult(t0 + h, y1);
        }
    }

    // ---------------- THIRD ORDER ----------------
    public sealed class RungeKuttaThirdOrder
    {
        private readonly ExplicitEuler _explicitEuler = new ExplicitEuler();

        private RungeKuttaResult GetGuess(
            DerivativeFunc fc,
            Vector<double> y0,
            double t0,
            double h)
        {
            var emRes = _explicitEuler.Execute(fc, y0, t0, h);
            return new RungeKuttaResult(emRes.NextTime, emRes.NextState);
        }

        public RungeKuttaResult Execute(
            DerivativeFunc fc,
            Vector<double> y0,
            double t0,
            double h)
        {
            int c = y0.Count;

            // States: y_i, y_{i+1/2}, y_{i+1}
            var states = Matrix<double>.Build.Dense(3, c);
            states.SetRow(0, y0);

            var guess = new RungeKuttaResult(t0, y0);
            double time = t0;

            // Two half-steps to get y_{i+1/2} and y_{i+1}
            for (int i = 1; i <= 2; i++)
            {
                guess = GetGuess(fc, guess.State, time, h / 2.0);
                states.SetRow(i, guess.State);
                time += h / 2.0;
            }

            var f0 = fc(states, 0, t0);
            var fHalf = fc(states, 1, t0 + h / 2.0) * 4.0;
            var f1 = fc(states, 2, t0 + h);

            var y1 = y0 + (f0 + fHalf + f1) * (h / 6.0);
            return new RungeKuttaResult(t0 + h, y1);
        }
    }

    // ---------------- FOURTH ORDER (classic RK4) ----------------
    public sealed class RungeKuttaFourthOrder
    {
        public RungeKuttaResult Execute(
            DerivativeFunc fc,
            Vector<double> y0,
            double t0,
            double h)
        {
            int n = y0.Count;

            var states = Matrix<double>.Build.Dense(4, n);
            states.SetRow(0, y0);

            var k1 = fc(states, 0, t0) * h;

            var w1 = y0 + k1 * 0.5;
            states.SetRow(1, w1);

            var k2 = fc(states, 1, t0 + h / 2.0) * h;

            var w2 = y0 + k2 * 0.5;
            states.SetRow(2, w2);

            var k3 = fc(states, 2, t0 + h / 2.0) * h;

            var w3 = y0 + k3;
            states.SetRow(3, w3);

            var k4 = fc(states, 3, t0 + h) * h;

            var y1 = y0 + (k1 + 2.0 * k2 + 2.0 * k3 + k4) * (1.0 / 6.0);
            return new RungeKuttaResult(t0 + h, y1);
        }
    }
}
