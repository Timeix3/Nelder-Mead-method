cmake_minimum_required(VERSION 3.27)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

project(NelderMeadOptimization)

include(FetchContent)

FetchContent_Declare(
    googletest
    GIT_REPOSITORY https://github.com/google/googletest.git
    GIT_TAG v1.16.0  # Вы можете указать другую версию
)

FetchContent_MakeAvailable(googletest)

add_subdirectory(src)

include_directories(${PROJECT_SOURCE_DIR}/src)

enable_testing()
add_subdirectory(test)