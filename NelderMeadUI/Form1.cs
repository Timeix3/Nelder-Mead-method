using System.Runtime.InteropServices;

namespace NelderMeadUI
{
    public partial class Form1 : Form
    {
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern double evaluateFunctionImport(double[] point, int size, [MarshalAs(UnmanagedType.LPStr)] string function);
        [DllImport("NelderMeadDll.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr findFunctionMinimum(int varsCount, double[] startingPoint, [MarshalAs(UnmanagedType.LPStr)] string function);
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string function = textBox1.Text;
            int varsCount = (int)numericUpDown1.Value;
            string[] coordinateString = textBox2.Text.Split(',');
            double[] startingPoint = coordinateString.Select(double.Parse).ToArray();
            //string functionRosenbrock = "(1-x1)^2+100*(x2-x1^2)^2";
            //string functionHimmelblau = "(x1^2+x2-11)^2+(x1+x2^2-7)^2";
            //string function3Variables = "x1^2+x2^2+x3^2";
            IntPtr ptr = findFunctionMinimum(varsCount, startingPoint, function);
            double[] result = new double[varsCount];
            Marshal.Copy(ptr, result, 0, varsCount);
            double res = evaluateFunctionImport(result, varsCount, function);
            for(int i = 0; i < varsCount; i++)
            {
                label1.Text += $"X{i+1}=" + result[i].ToString() + "\n";
            }
            label1.Text += "F(X)=" + res.ToString();
        }
    }
}
