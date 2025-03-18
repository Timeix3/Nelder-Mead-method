add_executable(tests test.cpp)

target_link_libraries(tests gtest gtest_main neldermead)

add_test(NAME NelderMeadTests COMMAND tests)