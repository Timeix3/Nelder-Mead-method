using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WinForms;

[assembly: InternalsVisibleTo("TestsForMethod")]

namespace NelderMeadUI
{
    public partial class FormMain : Form
    {
        public delegate void PointsCallback(IntPtr pointPtr);
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern double evaluateFunction(double[] point, int size, [MarshalAs(UnmanagedType.LPStr)] string function);
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr findFunctionMinimum(PointsCallback callback, int varsCount, double[] startingPoint, [MarshalAs(UnmanagedType.LPStr)] string function);

        private readonly FormsPlot _formsPlot;

        private readonly System.Windows.Forms.Timer _timer;

        internal readonly List<double[]> _points = new();

        private int _currentTriangleIndex;

        public int varsCount;

        public FormMain()
        {
            InitializeComponent();
            _formsPlot = new FormsPlot() { Dock = DockStyle.Fill };
            panel.Controls.Add(_formsPlot);
            _points = new List<double[]>();
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100;
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_currentTriangleIndex < _points.Count - 2)
            {
                DrawPath();
                _currentTriangleIndex += 3;
            }
            else
            {
                _timer.Stop();
                _timer.Dispose();
                ToggleEditing(true);
            }
        }

        private void DrawPath()
        {
            _formsPlot.Plot.Clear();
            for (int i = 0; i < _currentTriangleIndex; i += 3)
            {
                DrawTriangle(_points.GetRange(i, 3).SelectMany(x => x).ToList(),
                    new Triangle(ScottPlot.Color.FromARGB(838926080), 2));
            }
            DrawTriangle(_points.GetRange(_currentTriangleIndex, 3).SelectMany(x => x).ToList(),
                    new Triangle(ScottPlot.Colors.Red, 3));
            _formsPlot.Refresh();
        }

        private void ToggleEditing(bool availability)
        {
            textBoxFunction.Enabled = availability;
            textBoxPoint.Enabled = availability;
            buttonStart.Enabled = availability;
        }

        public record Triangle(ScottPlot.Color BorderColor, int LineWidth);

        private void DrawTriangle(List<double> trianglePoints, Triangle triangle)
        {
            double[] dataX = { trianglePoints[0], trianglePoints[2], trianglePoints[4], trianglePoints[0] };
            double[] dataY = { trianglePoints[1], trianglePoints[3], trianglePoints[5], trianglePoints[1] };
            var scatter = _formsPlot.Plot.Add.Scatter(dataX, dataY);
            scatter.Color = triangle.BorderColor;
            scatter.LineWidth = triangle.LineWidth;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            _points.Clear();
            _currentTriangleIndex = 0;
            labelResult.Text = "";
            string function = textBoxFunction.Text;
            varsCount = GetVariablesCount(function);
            double[] startingPoint;
            if (!ValidatePoint(out startingPoint)) return;
            if (!GetResult(function, startingPoint)) return;
            if (varsCount == 2)
            {
                ToggleEditing(false);
                _timer.Start();
            }
        }

        private bool GetResult(string function, double[] startingPoint)
        {
            PointsCallback callback = null;
            if (varsCount == 2) callback = new PointsCallback(GetPoint);
            IntPtr ptr = 0;
            double[] resultPoint = new double[varsCount];
            double resultValue;
            try
            {
                ptr = findFunctionMinimum(callback, varsCount, startingPoint, function);
                Marshal.Copy(ptr, resultPoint, 0, varsCount);
                resultValue = evaluateFunction(resultPoint, varsCount, function);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid expression", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            ShowResult(resultPoint, resultValue);
            return true;
        }

        private void ShowResult(double[] resultPoint, double resultValue)
        {
            for (int i = 0; i < varsCount; i++)
            {
                labelResult.Text += $"X{i + 1}=" + resultPoint[i].ToString() + "\n";
            }
            labelResult.Text += "F(X)=" + resultValue.ToString();
        }

        private bool ValidatePoint(out double[] startingPoint)
        {
            string[] coordinateString = textBoxPoint.Text.Split(',');
            try
            {
                startingPoint = coordinateString.Select(double.Parse).ToArray();
                if (startingPoint.Length != varsCount) throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid point", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                startingPoint = null;
                return false;
            }
            return true;
        }

        internal void GetPoint(IntPtr pointPtr)
        {
            double[] point = new double[varsCount];
            Marshal.Copy(pointPtr, point, 0, varsCount);
            _points.Add(point);
        }

        internal int GetVariablesCount(string function)
        {
            int varsCount = 1;
            for (int i = 0; i < function.Length; i++)
                if (function[i] == 'x' && varsCount < (function[i + 1] - '0')) varsCount = function[i + 1] - '0';
            return varsCount;
        }

        public interface INelderMeadSolverForDLL
        {
            double EvaluateFunction(double[] point, int dimensions, string function);
        }

        public interface INelderMeadSolver
        {
            IntPtr FindMinimum(PointsCallback callback, int varsCount, double[] startPoint, string function);
        }

        private readonly INelderMeadSolver _solver;

        public FormMain(INelderMeadSolver solver = null)
        {
            _solver = solver ?? new DefaultSolver();                                         
        }

        public double[] FindFunctionMinimum(string function, double[] startingPoint)
        {
            PointsCallback callback = GetPoint;
            IntPtr ptr = _solver.FindMinimum(callback, startingPoint.Length, startingPoint, function);
            double[] result = new double[startingPoint.Length];
            Marshal.Copy(ptr, result, 0, startingPoint.Length);
            return result;
        }
        
        public class DefaultSolver : INelderMeadSolver
        {
            public IntPtr FindMinimum(PointsCallback callback, int varsCount, double[] startPoint, string function)
            {
                return findFunctionMinimum(callback, varsCount, startPoint, function);
            }

            [DllImport("NelderMeadDll.dll")]
            private static extern IntPtr findFunctionMinimum(PointsCallback callback, int varsCount, double[] startPoint, string function);
        }
    }
}
