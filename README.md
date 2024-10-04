# Cross-Platform Development


Лабораторні роботи з дисципліни "Кросплатформне програмування"

Студентки групи ІПЗ-34мс **Шевченко Влади**

Обраний варіант для виконання: **Варіант 17**

---

Laboratory Work on subject Cross-Platform App Development by **Vlada Shevchenko** IPZ-34ms **Variant №17**

## Navigation

- [Laboratory work №1](https://github.com/vladashvch/cross-platform-labs/tree/master/Lab01)
  - [Tests](https://github.com/vladashvch/cross-platform-labs/tree/master/Lab01.Tests)

- [Laboratory work №2](https://github.com/vladashvch/cross-platform-labs/tree/master/Lab02)
  - [Tests](https://github.com/vladashvch/cross-platform-labs/tree/master/Lab02.Tests)

- [Laboratory work №3](https://github.com/vladashvch/cross-platform-labs/tree/master/Lab03)
  - [Class Library](https://github.com/vladashvch/cross-platform-labs/tree/master/VShevchenko)
  - [Tests](https://github.com/vladashvch/cross-platform-labs/tree/master/Lab03.Tests)

## Usage

Щоб швидко запустити певний проєкт, використовуйте msbuild файл "MSBuild.proj".

*Quick commands to start all Labs 1-3 at once:*

```
# Build all Labs 1-3
msbuild MSBuild.proj /t:Build

# Run all Labs 1-3
msbuild MSBuild.proj /t:Run

# Build all tests for Labs 1-3
msbuild MSBuild.proj /t:BuildTests

# Run all tests for Labs 1-3
msbuild MSBuild.proj /t:RunTests
```
