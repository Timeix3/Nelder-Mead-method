using System.Runtime.InteropServices;
using System.Text.Json;

namespace NelderMeadUI
{
    public partial class FormMain : Form
    {
        public delegate void PointsCallback(IntPtr pointPtr);
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern double evaluateFunctionImport(double[] point, int size, [MarshalAs(UnmanagedType.LPStr)] string function);
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr findFunctionMinimum(PointsCallback callback, int varsCount, double[] startingPoint, [MarshalAs(UnmanagedType.LPStr)] string function);
        
        private Point[][] triangles;
        private int currentTriangleIndex = -1; // индекс текущего рисуемого треугольника
        private System.Windows.Forms.Timer timer; // таймер для переключения треугольников
        public FormMain()
        {
            InitializeComponent();
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
        }
        List<double[]> points = new();
        private void GetPoint(IntPtr pointPtr)
        {
            double[] point = new double[varsCount];
            Marshal.Copy(pointPtr, point, 0, varsCount);
            points.Add(point);
        }
        private int GetVariablesCount(string function)
        {
            int varsCount = 1;
            for (int i = 0; i < function.Length; i++)
                if (function[i] == 'x' && varsCount < (function[i + 1] - '0')) varsCount = function[i + 1] - '0';
            return varsCount;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            triangles = LoadTriangles("triangles.json");

            timer = new System.Windows.Forms.Timer
            {
                Interval = 200
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            pictureBox1.Paint += DrawingPanel_Paint;
        }

        private Point[][] LoadTriangles(string filePath)
        {
            if (!File.Exists(filePath))
                return null;

      
            string json = File.ReadAllText(filePath);

            return JsonSerializer.Deserialize<Point[][]>(json);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTriangleIndex++;
            if (currentTriangleIndex >= triangles.Length)
            {
                timer.Stop();
            }
            else
            {
                pictureBox1.Invalidate();
            }            
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            // Получаем текущий треугольник
            Point[] currentTriangle = triangles[currentTriangleIndex];

            e.Graphics.DrawPolygon(Pens.Black, currentTriangle); // Обводим треугольник
        }
    }
}
