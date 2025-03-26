using System.Runtime.InteropServices;
using System.Text.Json;

namespace NelderMeadUI
{
    public partial class Form1 : Form
    {
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern double evaluateFunctionImport(double[] point, int size, [MarshalAs(UnmanagedType.LPStr)] string function);
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr findFunctionMinimum(int varsCount, double[] startingPoint, [MarshalAs(UnmanagedType.LPStr)] string function);

        private Point[][] triangles;
        private int currentTriangleIndex = -1; // индекс текущего рисуемого треугольника
        private System.Windows.Forms.Timer timer; // таймер для переключения треугольников
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string function = textBox1.Text;
            int varsCount = getVariablesCount(function);
            string[] coordinateString = textBox2.Text.Split(',');
            double[] startingPoint = coordinateString.Select(double.Parse).ToArray();
            //string functionRosenbrock = "(1-x1)^2+100*(x2-x1^2)^2";
            //string functionHimmelblau = "(x1^2+x2-11)^2+(x1+x2^2-7)^2";
            //string function3Variables = "x1^2+x2^2+x3^2";
            IntPtr ptr = findFunctionMinimum(varsCount, startingPoint, function);
            double[] result = new double[varsCount];
            Marshal.Copy(ptr, result, 0, varsCount);
            double res = evaluateFunctionImport(result, varsCount, function);
            for (int i = 0; i < varsCount; i++)
            {
                label1.Text += $"X{i + 1}=" + result[i].ToString() + "\n";
            }
            label1.Text += "F(X)=" + res.ToString();
        }
        private int getVariablesCount(string function)
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
