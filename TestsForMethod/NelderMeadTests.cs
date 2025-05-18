using NelderMeadUI;
using System.Runtime.InteropServices;
using Xunit;
using Moq;
using static NelderMeadUI.FormMain;
using System.Reflection;

namespace TestsForMethod
{
    public class FunctionVariableCountTests
    {
        [Theory]
        [InlineData("x1 + x2", 2)]
        [InlineData("x1^2 + x2^2 + x3^2", 3)]
        [InlineData("(x1^2+x2-11)^2+(x1+x2^2-7)^2", 2)] // Химмельблау
        [InlineData("(1-x1)^2 + 100*(x2-x1^2)^2", 2)] // Rosenbrock
        [InlineData("x1 + 5", 1)]
        [InlineData("x3 + x1 + x2", 3)]
        public void GetVariablesCount_ReturnsCorrectCount(string function, int expectedCount)
        {
            var form = new FormMain();
            int actualCount = form.GetVariablesCount(function);
            Assert.Equal(expectedCount, actualCount);
        }
    }

    public class CallbackTests
    {
        [Fact]
        public void GetPoint_AddsPointToCollection()
        {
            var form = new FormMain();
            form.varsCount = 2;
            double[] testPoint = { 1.5, 2.5 };

            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)) * 2);
            Marshal.Copy(testPoint, 0, ptr, 2);

            form.GetPoint(ptr);

            Assert.Single(form._points);
            Assert.Equal(testPoint, form._points[0]);

            Marshal.FreeHGlobal(ptr);
        }
    }

    public class DllIntegrationTests
    {
        [Fact]
        public void TestEvaluation()
        {
            var mock = new Mock<INelderMeadSolverForDLL>();
            mock.Setup(x => x.EvaluateFunction(It.IsAny<double[]>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(42.0);

            var solver = mock.Object;
            double result = solver.EvaluateFunction(new[] { 1.0, 2.0 }, 2, "x1 + x2");

            Assert.Equal(42.0, result);
        }
    }

    public class FunctionEvaluationTests
    {
        [Theory]
        [InlineData("(1-x1)^2+100*(x2-x1^2)^2", new double[] { 1, 1 }, 0)]
        [InlineData("(x1^2+x2-11)^2+(x1+x2^2-7)^2", new double[] { 3, 2 }, 0)]
        [InlineData("x1^2+x2^2+x3^2", new double[] { 0, 0, 0 }, 0)]
        public void EvaluateFunctionImport_ReturnsCorrectValue(string function, double[] point, double expectedValue)
        {
            var mockSolver = new Mock<INelderMeadSolverForDLL>();
            mockSolver.Setup(x => x.EvaluateFunction(
                    It.Is<double[]>(p => p.SequenceEqual(point)), // Проверяем точное совпадение точек
                    It.Is<int>(d => d == point.Length),          // Проверяем размерность
                    It.Is<string>(f => f == function)             // Проверяем функцию
                ))
                .Returns(expectedValue);

            var solver = mockSolver.Object;

            double actualValue = solver.EvaluateFunction(point, point.Length, function);

            Assert.Equal(expectedValue, actualValue, precision: 5);
        } 
    }

    public class MinimizationTests
    {
        private readonly string _functionRosenbrock = "(1-x1)^2+100*(x2-x1^2)^2";
        private readonly string _functionHimmelblau = "(x1^2+x2-11)^2+(x1+x2^2-7)^2";
        private readonly string _function3Variables = "x1^2+x2^2+x3^2";

        [Fact]
        public void FindFunctionMinimum_WithRosenbrockFunction_ReturnsExpectedResult()
        {
            var mockSolver = new Mock<INelderMeadSolver>();
            mockSolver
                .Setup(s => s.FindMinimum(
                    It.IsAny<FormMain.PointsCallback>(),
                    2,
                    It.IsAny<double[]>(),
                    _functionRosenbrock))
                .Returns(() =>
                {
                    double[] optimalPoint = { 1.0, 1.0 };  // Известный минимум для Розенброка
                    IntPtr ptr = Marshal.AllocHGlobal(sizeof(double) * 2);
                    Marshal.Copy(optimalPoint, 0, ptr, 2);
                    return ptr;
                });

            var form = new FormMain(mockSolver.Object);
            form.varsCount = 2;

            double[] result = form.FindFunctionMinimum(_functionRosenbrock, new double[] { -1.5, 2 });

            Assert.Equal(1.0, result[0], precision: 1);
            Assert.Equal(1.0, result[1], precision: 1);
        }

        [Fact]
        public void FindFunctionMinimum_WithHimmelblauFunction_ReturnsExpectedResult()
        {
            var mockSolver = new Mock<INelderMeadSolver>();
            mockSolver
                .Setup(s => s.FindMinimum(
                    It.IsAny<FormMain.PointsCallback>(),
                    2,
                    It.IsAny<double[]>(),
                    _functionHimmelblau))
                .Returns(() =>
                {
                    // Один из известных минимумов для функции Химмельблау
                    double[] optimalPoint = { 3.0, 2.0 };
                    IntPtr ptr = Marshal.AllocHGlobal(sizeof(double) * 2);
                    Marshal.Copy(optimalPoint, 0, ptr, 2);
                    return ptr;
                });

            var form = new FormMain(mockSolver.Object);
            form.varsCount = 2;

            double[] result = form.FindFunctionMinimum(_functionHimmelblau, new double[] { 0, 0 });

            Assert.Equal(3.0, result[0], precision: 1);
            Assert.Equal(2.0, result[1], precision: 1);
        }

        [Fact]
        public void FindFunctionMinimum_With3VariablesFunction_ReturnsExpectedResult()
        {
            var mockSolver = new Mock<INelderMeadSolver>();
            mockSolver
                .Setup(s => s.FindMinimum(
                    It.IsAny<FormMain.PointsCallback>(),
                    3,
                    It.IsAny<double[]>(),
                    _function3Variables))
                .Returns(() =>
                {
                    // Минимум для x1² + x2² + x3² находится в (0, 0, 0)
                    double[] optimalPoint = { 0.0, 0.0, 0.0 };
                    IntPtr ptr = Marshal.AllocHGlobal(sizeof(double) * 3);
                    Marshal.Copy(optimalPoint, 0, ptr, 3);
                    return ptr;
                });

            var form = new FormMain(mockSolver.Object);
            form.varsCount = 3;

            double[] result = form.FindFunctionMinimum(_function3Variables, new double[] { 1.0, 1.0, 1.0 });

            Assert.Equal(0.0, result[0], precision: 1);
            Assert.Equal(0.0, result[1], precision: 1);
            Assert.Equal(0.0, result[2], precision: 1);
        }

        [Fact]
        public void FindFunctionMinimum_WithIntermediatePoints_CallsCallbackCorrectly()
        {
            var mockSolver = new Mock<INelderMeadSolver>();
            var callbackPoints = new System.Collections.Generic.List<double[]>();

            mockSolver
                .Setup(s => s.FindMinimum(
                    It.IsAny<FormMain.PointsCallback>(),
                    2,
                    It.IsAny<double[]>(),
                    _functionRosenbrock))
                .Returns((FormMain.PointsCallback callback, int varsCount, double[] startPoint, string function) =>
                {
                    // Имитация 3 промежуточных точек
                    double[][] intermediatePoints = {
                    new double[] { 0.5, 0.5 },
                    new double[] { 0.8, 0.8 },
                    new double[] { 0.95, 0.95 }
                    };

                    foreach (var point in intermediatePoints)
                    {
                        IntPtr ptr = Marshal.AllocHGlobal(sizeof(double) * 2);
                        Marshal.Copy(point, 0, ptr, 2);
                        callback(ptr);
                        Marshal.FreeHGlobal(ptr);
                    }

                    // Возвращаем финальную точку
                    double[] optimalPoint = { 1.0, 1.0 };
                    IntPtr finalPtr = Marshal.AllocHGlobal(sizeof(double) * 2);
                    Marshal.Copy(optimalPoint, 0, finalPtr, 2);
                    return finalPtr;
                });

            var form = new FormMain(mockSolver.Object);
            form.varsCount = 2;

            double[] result = form.FindFunctionMinimum(_functionRosenbrock, new double[] { -1.5, 2 });

            Assert.Equal(3, form._points.Count);
            Assert.Equal(1.0, result[0], precision: 1);
            Assert.Equal(1.0, result[1], precision: 1);
        }
    }
}
