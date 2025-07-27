using MathNet.Numerics.LinearAlgebra;

namespace EulerMethod
{
    // Delegate for derivative function: f(t, y)
    public delegate Vector<double> DerivativeFunc(Matrix<double> state, int index, double time);

    // Result object for both methods
    public sealed class EulerResult
    {
        public double NextTime { get; }
        public Vector<double> NextState { get; }

        public EulerResult(double nextTime, Vector<double> nextState)
        {
            NextTime = nextTime;
            NextState = nextState;
        }
    }

    // ---------------- EXPLICIT EULER ----------------
    public sealed class ExplicitEuler
    {
        public EulerResult Execute(
            DerivativeFunc fc,
            Vector<double> initialCondition,
            double initialTime,
            double h)
        {
            int r = 1;
            int c = initialCondition.Count;

            // Create a 1 x c matrix for the current state
            var previousState = Matrix<double>.Build.Dense(r, c);
            previousState.SetRow(0, initialCondition);

            // Compute next state: y_{i+1} = y_i + h * f(t_i, y_i)
            var tempState = fc(previousState, 0, initialTime);
            var nextState = initialCondition + tempState * h;

            double nextTime = initialTime + h;

            return new EulerResult(nextTime, nextState);
        }
    }

    // ---------------- IMPLICIT EULER ----------------
    public sealed class ImplicitEuler
    {
        private readonly ExplicitEuler _explicitEuler;

        public ImplicitEuler()
        {
            _explicitEuler = new ExplicitEuler();
        }

        private EulerResult GetGuess(
            DerivativeFunc fc,
            Vector<double> initialCondition,
            double initialTime,
            double h)
        {
            // Uses explicit Euler as the predictor
            return _explicitEuler.Execute(fc, initialCondition, initialTime, h);
        }

        public EulerResult Execute(
            DerivativeFunc fc,
            Vector<double> initialCondition,
            double initialTime,
            double h)
        {
            int r = 1;
            int c = initialCondition.Count;

            // Predictor (explicit Euler)
            var guessResult = GetGuess(fc, initialCondition, initialTime, h);

            // Build S_{i+1}^hat as a 1 x c matrix to pass into fc
            var nextStateHat = Matrix<double>.Build.Dense(r, c);
            nextStateHat.SetRow(0, guessResult.NextState);

            // Evaluate derivative at predicted next state
            var tempState = fc(nextStateHat, 0, initialTime);

            // Corrected step: y_{i+1} = y_i + h * f(t_{i+1}, y_{i+1})
            var refinedNextState = initialCondition + tempState * h;

            double nextTime = initialTime + h;

            return new EulerResult(nextTime, refinedNextState);
        }
    }
}
