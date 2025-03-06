#include <iostream>
#include <vector>
#include <algorithm>

using namespace std;

double function(vector<double> point) {
	return point[0] * point[0] + point[0] * point[1] + point[1] * point[1] - 6 * point[0] - 9 * point[1];
}

double functionRosenbrock(vector<double> point) {
	return pow(1 - point[0], 2) + 100 * pow(point[1] - point[0] * point[0], 2);
}

double functionHimmelblau(vector<double> point) {
	return pow(point[0] * point[0] + point[1] - 11, 2) + pow(point[0] + point[1] * point[1] - 7, 2);
}

class element {
public:
	vector<double> point;
	double functionValue;
	element() {
		point = { 0,0 };
		functionValue = 0;
	}
	element(vector<double> p) {
		point = p;
		functionValue = functionRosenbrock(p);
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

vector<element> makeStartSimplex(int dimension, double scale, vector<double> startingPoint) {
	vector<element> elements;
	elements.push_back(element(startingPoint));
	for (int i = 0; i < dimension; i++) {
		vector<double> newPoint(dimension);
		newPoint[i] = scale;
		elements.push_back(element(newPoint));
	}
	return elements;
}

vector<double> operator*(const vector<double>& vec, double scalar) {
	vector<double> result(vec.size());
	for (int i = 0; i < vec.size(); i++) {
		result[i] = vec[i] * scalar;
	}
	return result;
}

vector<double> operator/(const vector<double>& vec, double scalar) {
	vector<double> result(vec.size());
	for (int i = 0; i < vec.size(); i++) {
		result[i] = vec[i] / scalar;
	}
	return result;
}

vector<double> operator+(const vector<double>& vec1, const vector<double>& vec2) {
	vector<double> result(vec1.size());
	for (int i = 0; i < vec1.size(); i++) {
		result[i] = vec1[i] + vec2[i];
	}
	return result;
}

vector<double> operator-(const vector<double>& vec1, const vector<double>& vec2) {
	vector<double> result(vec1.size());
	for (int i = 0; i < vec1.size(); i++) {
		result[i] = vec1[i] - vec2[i];
	}
	return result;
}

vector<double> calculateMassCenter(int dimension, vector<element> elements) {
	vector<double> massCenter(dimension);
	for (int i = 0; i < dimension; i++)
		massCenter = massCenter + elements[i].point / (double)dimension;
	return massCenter;
}

bool compare(const element left, const element right)
{
	return left.functionValue < right.functionValue;
}

bool endCheck(int dimension, double eps, vector<element> elements) {
	double sum = 0;
	for (int i = 1; i < elements.size(); i++)
	{
		sum += pow(elements[i].functionValue - elements.front().functionValue, 2);
	}
	if (sqrt(sum / dimension) <= eps) return true;
	return false;
}

//void print(vector<double> point) {
//    cout << "(";
//    for (int i = 0; i < point.size(); ++i) {
//        cout << point[i];
//        if (i < point.size() - 1) cout << ", ";
//    }
//    std::cout << ")" << endl;
//}

element calculateContraction(vector<element> elements, element reflection, vector<double> massCenter, double contractionCoeff)
{
	element contraction;
	if (elements.back().functionValue <= reflection.functionValue)
		contraction = element(massCenter + (elements.back().point - massCenter) * contractionCoeff);
	else /*if (reflection.functionValue < elements.back().functionValue)*/
		contraction = element(massCenter + (reflection.point - massCenter) * contractionCoeff);
	return contraction;
}

int main()
{
	double reflectionCoeff = 1;
	double contractionCoeff = 0.5;
	double expansionCoeff = 2;
	double scale = 1;
	double eps = 0.001;
	int dimension = 2;
	vector<double> startingPoint = { 0,0 };
	vector<element> elements = makeStartSimplex(dimension, scale, startingPoint);
	/*for (auto var : elements) {
		var.print();
	}*/
	int k = 0;
	for (;;) {
		sort(begin(elements), end(elements), compare);
		/*for (auto var : elements) {
			var.print();
		}*/
		if (endCheck(dimension, eps, elements)) break;
		k++;
		vector<double> massCenter = calculateMassCenter(dimension, elements);
		//cout << "C=";  print(massCenter);
		element reflection = element(massCenter * (1 + reflectionCoeff) - elements.back().point * reflectionCoeff);
		//reflection.print();
		if (elements.front().functionValue <= reflection.functionValue && reflection.functionValue <= elements.at(elements.size() - 2).functionValue) {
			elements.back() = reflection;
		}
		else if (reflection.functionValue < elements.front().functionValue) {
			element expansion = element(massCenter * (1 - expansionCoeff) + reflection.point * expansionCoeff);
			if (expansion.functionValue < reflection.functionValue) elements.back() = expansion;
			else elements.back() = reflection;
		}
		else if (reflection.functionValue > elements.at(elements.size() - 2).functionValue) {
			element contraction = calculateContraction(elements, reflection, massCenter, contractionCoeff);
			if (contraction.functionValue < min(elements.back().functionValue, reflection.functionValue))
				elements.back() = contraction;
			else {
				for (int i = 1; i < elements.size(); i++)
					elements[i] = element(elements[i].point + ((elements.front().point - elements[i].point) / 2.0));
			}
		}
	}
	cout << "Steps: " << k << endl;
	elements.front().print();
}
