#include <windows.h>
#include <iostream>
#include <vector>

void print(std::vector<double> point) {
	std::cout << "X=(";
	for (int i = 0; i < point.size(); i++) {
		std::cout << point[i];
		if (i < point.size() - 1) std::cout << ", ";
	}
	std::cout << ")";
}

int main()
{
	int varsCount = 2;
	std::vector<double> startingPoint = { 0,0 };
	std::string functionRosenbrock = "(1-x1)^2+100*(x2-x1^2)^2";
	std::string functionHimmelblau = "(x1^2+x2-11)^2+(x1+x2^2-7)^2";
	std::string function3Variables = "x1^2+x2^2+x3^2";
	std::string chosenFunction = functionRosenbrock;
	//getline(std::cin, chosenFunction);

	HMODULE NelderMeadLib;
	NelderMeadLib = LoadLibrary(L"NelderMeadDll.dll");
	if (NelderMeadLib == NULL) std::cout << "dll not found";

	typedef std::vector<double>(WINAPI* findFunctionMinimum)(int varsCount, std::vector<double> startingPoint, std::string function);
	findFunctionMinimum functionMinimum;
	functionMinimum = (findFunctionMinimum)GetProcAddress(NelderMeadLib, "findFunctionMinimum");
	if (functionMinimum == nullptr) std::cout << "functionMinimum not found";
	std::vector<double> point = functionMinimum(varsCount, startingPoint, chosenFunction);

	typedef double(WINAPI* evaluateFunction)(std::vector<double> point, std::string function);
	evaluateFunction functionEvaluate;
	functionEvaluate = (evaluateFunction)GetProcAddress(NelderMeadLib, "evaluateFunction");
	if (functionEvaluate == nullptr) std::cout << "functionEvaluate not found";
	double value = functionEvaluate(point, chosenFunction);

	print(point);
	std::cout << " F(X)=" << value << std::endl;
	FreeLibrary(NelderMeadLib);
}
