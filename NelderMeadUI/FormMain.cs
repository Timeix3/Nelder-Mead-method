using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.Json;
using ScottPlot;
using ScottPlot.WinForms;

namespace NelderMeadUI
{
    public partial class FormMain : Form
    {
        public delegate void PointsCallback(IntPtr pointPtr);
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern double evaluateFunctionImport(double[] point, int size, [MarshalAs(UnmanagedType.LPStr)] string function);
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr findFunctionMinimum(PointsCallback callback, int varsCount, double[] startingPoint, [MarshalAs(UnmanagedType.LPStr)] string function);

        private readonly FormsPlot _formsPlot;

        private readonly System.Windows.Forms.Timer _timer;

        private readonly List<double[]> _points = new();

        private int _currentTriangleIndex;

        public FormMain()
        {
            InitializeComponent();

            _formsPlot = new FormsPlot() { Dock = DockStyle.Fill };

            panel1.Controls.Add(_formsPlot);

            _points = new List<double[]>();

            _currentTriangleIndex = 0;

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 100;
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_currentTriangleIndex < _points.Count - 2)
            {
                List<double> triangleList =
                [
                    .. _points[_currentTriangleIndex],
                    .. _points[_currentTriangleIndex + 1],
                    .. _points[_currentTriangleIndex + 2],
                ];

                DrawTriangle([.. triangleList]);
                _currentTriangleIndex+=3;
            }
            else
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }
        private void DrawTriangle(double[] trianglePoints)
        {
            _formsPlot.Plot.Clear();

            double[] dataX = { trianglePoints[0], trianglePoints[2], trianglePoints[4], trianglePoints[0] };
            double[] dataY = { trianglePoints[1], trianglePoints[3], trianglePoints[5], trianglePoints[1] };

            _formsPlot.Plot.Add.Scatter(dataX, dataY);

            _formsPlot.Refresh();
        }

        int varsCount;
        private void ButtonStart_Click(object sender, EventArgs e)
        {
            labelResult.Text = "";
            string function = textBoxFunction.Text;
            varsCount = GetVariablesCount(function);
            string[] coordinateString = textBoxPoint.Text.Split(',');
            double[] startingPoint;
            try
            {
                startingPoint = coordinateString.Select(double.Parse).ToArray();
                if (startingPoint.Length != varsCount) throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid point", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //string functionRosenbrock = "(1-x1)^2+100*(x2-x1^2)^2";
            //string functionHimmelblau = "(x1^2+x2-11)^2+(x1+x2^2-7)^2";
            //string function3Variables = "x1^2+x2^2+x3^2";
            PointsCallback callback = new PointsCallback(GetPoint);
            IntPtr ptr = 0;
            double[] result = new double[varsCount];
            double res;
            try
            {
                ptr = findFunctionMinimum(callback, varsCount, startingPoint, function);
                Marshal.Copy(ptr, result, 0, varsCount);
                res = evaluateFunctionImport(result, varsCount, function);
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid expression", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < varsCount; i++)
            {
                labelResult.Text += $"X{i + 1}=" + result[i].ToString() + "\n";
            }
            labelResult.Text += "F(X)=" + res.ToString();

            if (varsCount == 2)
            {
                _timer.Start();
            }
        }
        
        private void GetPoint(IntPtr pointPtr)
        {
            double[] point = new double[varsCount];
            Marshal.Copy(pointPtr, point, 0, varsCount);
            _points.Add(point);
        }
        private int GetVariablesCount(string function)
        {
            int varsCount = 1;
            for (int i = 0; i < function.Length; i++)
                if (function[i] == 'x' && varsCount < (function[i + 1] - '0')) varsCount = function[i + 1] - '0';
            return varsCount;
        }
    }
}
