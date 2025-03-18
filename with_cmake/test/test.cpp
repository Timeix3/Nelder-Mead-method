#include <gtest/gtest.h>
#include "neldermead.h"

// Тест для метода findFunctionMinimum
TEST(NelderMeadTest, FindFunctionMinimumBasicTest)
{
  int varsCount = 2;
  vector<double> startingPoint = {1.0, 1.0};
  string function = "x1^2 + x2^2";

  element result = findFunctionMinimum(varsCount, startingPoint, function);

  ASSERT_NEAR(result.functionValue, 0.0, 0.001);
  ASSERT_NEAR(result.point[0], 0.0, 0.001);
  ASSERT_NEAR(result.point[1], 0.0, 0.001);
}

TEST(NelderMeadTest, FindFunctionMinimumBasicTest2)
{
  ASSERT_EQ(-1, 1);
}

TEST(NelderMeadTest, FindFunctionMinimumBasicTest3)
{
  ASSERT_EQ(-1, 2);
}
