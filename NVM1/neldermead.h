#pragma once
#include <iostream>
#include <algorithm>
#include <vector>
#include "tinyexpr.h"

using namespace std;

double evaluateFunction(vector<double> point, string function);

class element {
public:
	vector<double> point;
	double functionValue;
	element() : point({ 0, 0 }), functionValue(0) {}
	element(vector<double> p, string function) {
		point = p;
		functionValue = evaluateFunction(p, function);
	}
	void print() {
		cout << "X=(";
		for (int i = 0; i < point.size(); ++i) {
			cout << point[i];
			if (i < point.size() - 1) cout << ", ";
		}
		std::cout << ")" << " F(X)=" << functionValue << endl;
	}
};

vector<element> makeStartSimplex(int varsCount, double scale, vector<double> startingPoint, string expression);
vector<double> operator*(const vector<double>& vec, double scalar);
vector<double> operator/(const vector<double>& vec, double scalar);
vector<double> operator+(const vector<double>& vec1, const vector<double>& vec2);
vector<double> operator-(const vector<double>& vec1, const vector<double>& vec2);
vector<double> calculateMassCenter(vector<element> elements);
bool compare(const element a, const element b);
bool endCheck(double eps, vector<element> elements);
element calculateContraction(vector<element> elements, element reflection, vector<double> massCenter, double contractionCoeff, string expression);
element findFunctionMinimum(int varsCount, vector<double> startingPoint, string expression);