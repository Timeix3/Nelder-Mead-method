add_library(neldermead neldermead.cpp tinyexpr.cpp)

add_executable(optimization main.cpp)

target_link_libraries(optimization neldermead)