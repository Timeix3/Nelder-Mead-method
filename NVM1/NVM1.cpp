#include "neldermead.h"

int main()
{
	int varsCount = 2;
	vector<double> startingPoint = { 0,0 };
	//string function;
	//getline(cin, function);
	string functionRosenbrock = "(1-x1)^2+100*(x2-x1^2)^2";
	string functionHimmelblau = "(x1^2+x2-11)^2+(x1+x2^2-7)^2";
	string function3Variables = "x1^2+x2^2+x3^2";
	element result = findFunctionMinimum(varsCount, startingPoint, functionRosenbrock);
	result.print();
}
