#include <windows.h>
#include <iostream>
#include <vector>

void print(double* point, int size) {
	std::cout << "X=(";
	for (int i = 0; i < size; i++) {
		std::cout << point[i];
		if (i < size - 1) std::cout << ", ";
	}
	std::cout << ")";
}

int main()
{
	int varsCount = 2;
	std::vector<double> startingPoint2 = { 0,0 };
	std::string functionRosenbrock = "(1-x1)^2+100*(x2-x1^2)^2";
	std::string functionHimmelblau = "(x1^2+x2-11)^2+(x1+x2^2-7)^2";
	std::string function3Variables = "x1^2+x2^2+x3^2";
	std::string chosenFunctionstr = functionRosenbrock;

	char* chosenFunction = chosenFunctionstr.data();
	double* startingPoint = startingPoint2.data();
	HMODULE NelderMeadLib;
	NelderMeadLib = LoadLibrary(L"NelderMeadDll.dll");
	if (NelderMeadLib == NULL) std::cout << "dll not found";

	typedef double*(WINAPI* findFunctionMinimum)(int varsCount, double* startingPoint, char* function);
	findFunctionMinimum functionMinimum;
	functionMinimum = (findFunctionMinimum)GetProcAddress(NelderMeadLib, "findFunctionMinimum");
	if (functionMinimum == nullptr) std::cout << "functionMinimum not found";
	double* point = functionMinimum(varsCount, startingPoint, functionRosenbrock.data());

	typedef double(WINAPI* evaluateFunctionImport)(double* point, int size, char* function);
	evaluateFunctionImport functionEvaluate;
	functionEvaluate = (evaluateFunctionImport)GetProcAddress(NelderMeadLib, "evaluateFunctionImport");
	if (functionEvaluate == nullptr) std::cout << "functionEvaluate not found";
	double value = functionEvaluate(point, varsCount, functionRosenbrock.data());

	print(point, varsCount);
	std::cout << " F(X)=" << value << std::endl;
	FreeLibrary(NelderMeadLib);
}
