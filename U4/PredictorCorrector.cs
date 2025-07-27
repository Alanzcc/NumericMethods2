// PredictorCorrector.cs
using System;
using MathNet.Numerics.LinearAlgebra;

namespace PredictorCorrector
{
    // Keep the same signature you used in your Euler/Runge-Kutta ports
    // fc(stateMatrix, whichRow, time) -> derivative vector
    public delegate Vector<double> DerivativeFunc(Matrix<double> state, int index, double time);

    public sealed class PredictorCorrectorResult
    {
        public double Time { get; }
        public Vector<double> State { get; }

        public PredictorCorrectorResult(double time, Vector<double> state)
        {
            Time = time;
            State = state;
        }
    }

    internal static class PcUtils
    {
        // Build a 1xN matrix holding the vector as row 0, to respect the DerivativeFunc signature
        public static Vector<double> Eval(DerivativeFunc fc, Vector<double> state, double time)
        {
            var m = Matrix<double>.Build.Dense(1, state.Count);
            m.SetRow(0, state);
            return fc(m, 0, time);
        }

        public static double RelError(Vector<double> prev, Vector<double> curr)
        {
            double prevNorm = prev.L2Norm();
            double currNorm = curr.L2Norm();
            if (currNorm == 0.0) return prevNorm == 0.0 ? 0.0 : double.PositiveInfinity;
            return Math.Abs(currNorm - prevNorm) / currNorm;
        }
    }

    // =========================
    // Adams–Bashforth 2nd order (PC2)
    // =========================
    public sealed class AdamsBashforth2
    {
        private const int MaxIterations = 100;

        public PredictorCorrectorResult Execute(
            DerivativeFunc fc,
            Vector<double> initial,
            double startTime,
            double stepSize,
            double errorTolerance)
        {
            // In the Go code, stepSize (h) is halved internally
            double h = stepSize / 2.0;

            // States S_i, S_{i+1}, S_{i+2}
            var S0 = initial.Clone();

            // S_{i+1} via simple Euler predictor (you can swap by RK2 if you want)
            var F0 = PcUtils.Eval(fc, S0, startTime);
            var S1 = S0 + h * F0;

            // Prediction: S_{i+2}^hat = S_{i+1} + h/2 * ( -F_i + 3 F_{i+1} )
            var F1 = PcUtils.Eval(fc, S1, startTime + h);
            var S2hat = S1 + (h / 2.0) * (-F0 + 3.0 * F1);

            // Correction loop: S_{i+2} = S_{i+1} + h/2 * ( F_{i+1} + F_{i+2} )
            var S2 = S2hat.Clone();
            double nextTime = startTime + 2.0 * h;
            for (int iter = 0; iter < MaxIterations; iter++)
            {
                var F2 = PcUtils.Eval(fc, S2, nextTime);
                var corrected = S1 + (h / 2.0) * (F1 + F2);

                double err = PcUtils.RelError(S2, corrected);
                S2 = corrected;
                if (err < errorTolerance) break;
            }

            return new PredictorCorrectorResult(nextTime, S2);
        }
    }

    // =========================
    // 3rd order Predictor–Corrector (PC3)
    // =========================
    public sealed class AdamsBashforth3
    {
        private const int MaxIterations = 100;

        public PredictorCorrectorResult Execute(
            DerivativeFunc fc,
            Vector<double> initial,
            double startTime,
            double stepSize,
            double errorTolerance)
        {
            // In the Go code, h is divided by 3
            double h = stepSize / 3.0;

            // Initialization with simple Euler
            var S0 = initial.Clone();
            var F0 = PcUtils.Eval(fc, S0, startTime);
            var S1 = S0 + h * F0;

            var F1tmp = PcUtils.Eval(fc, S1, startTime + h);
            var S2 = S1 + h * F1tmp;

            // Prediction: S_{i+3}^hat = S_{i+2} + h/12 * ( 5F_i - 16F_{i+1} + 23F_{i+2} )
            var F1 = PcUtils.Eval(fc, S1, startTime + h);
            var F2 = PcUtils.Eval(fc, S2, startTime + 2.0 * h);
            var S3hat = S2 + (h / 12.0) * (5.0 * F0 - 16.0 * F1 + 23.0 * F2);

            // Correction: S_{i+3} = S_{i+2} + h/12 * ( -F_{i+1} + 8F_{i+2} + 5F_{i+3} )
            var S3 = S3hat.Clone();
            double nextTime = startTime + 3.0 * h;
            for (int iter = 0; iter < MaxIterations; iter++)
            {
                var F3 = PcUtils.Eval(fc, S3, nextTime);
                var corrected = S2 + (h / 12.0) * (-F1 + 8.0 * F2 + 5.0 * F3);

                double err = PcUtils.RelError(S3, corrected);
                S3 = corrected;
                if (err < errorTolerance) break;
            }

            return new PredictorCorrectorResult(nextTime, S3);
        }
    }

    // =========================
    // 4th order Predictor–Corrector (PC4)
    // =========================
    public sealed class AdamsBashforth4
    {
        private const int MaxIterations = 100;

        public PredictorCorrectorResult Execute(
            DerivativeFunc fc,
            Vector<double> initial,
            double startTime,
            double stepSize,
            double errorTolerance)
        {
            // In the Go code, h is divided by 4
            double h = stepSize / 4.0;

            // Initialization with simple Euler 
            var S0 = initial.Clone();
            var F0 = PcUtils.Eval(fc, S0, startTime);
            var S1 = S0 + h * F0;

            var F1tmp = PcUtils.Eval(fc, S1, startTime + h);
            var S2 = S1 + h * F1tmp;

            var F2tmp = PcUtils.Eval(fc, S2, startTime + 2.0 * h);
            var S3 = S2 + h * F2tmp;

            // Prediction: S_{i+4}^hat = S_{i+3} + h/24 * ( -9F_i + 37F_{i+1} - 59F_{i+2} + 55F_{i+3} )
            var F1 = PcUtils.Eval(fc, S1, startTime + h);
            var F2 = PcUtils.Eval(fc, S2, startTime + 2.0 * h);
            var F3 = PcUtils.Eval(fc, S3, startTime + 3.0 * h);

            var S4hat = S3 + (h / 24.0) * (-9.0 * F0 + 37.0 * F1 - 59.0 * F2 + 55.0 * F3);

            // Correction: S_{i+4} = S_{i+3} + h/24 * ( 9F_{i+4} + 19F_{i+3} - 5F_{i+2} + F_{i+1} )
            var S4 = S4hat.Clone();
            double nextTime = startTime + 4.0 * h;
            for (int iter = 0; iter < MaxIterations; iter++)
            {
                var F4 = PcUtils.Eval(fc, S4, nextTime);
                var corrected = S3 + (h / 24.0) * (9.0 * F4 + 19.0 * F3 - 5.0 * F2 + F1);

                double err = PcUtils.RelError(S4, corrected);
                S4 = corrected;
                if (err < errorTolerance) break;
            }

            return new PredictorCorrectorResult(nextTime, S4);
        }
    }
}
