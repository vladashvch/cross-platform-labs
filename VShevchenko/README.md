# Бібліотека класів для лабораторної роботи №3 (Lab03)

Дана бібліотека класів містить реалізацію алгоритму для перевірки процедур на потенційну рекурсивність шляхом виявлення циклів у викликах процедур.

## Кроки для підключення Nuget пакету "VShevchenko"

1. Впевніться, що втановлено nuget
    - Введіть у консоль команду `nuget`

    У разі повідомлення про відсутність, завантажте NuGet.exe:
    - Помістіть його в папку, у якій воно зберігатиметься. Наприклад: C:\Users\\{ folder_name }\NuGet
    - Додайти шлях до папки: у Environment variable ->  Path -> додайте шлях до nuget.exe і збережіть.

2. Pack using dotnet CLI

    - Вікрийте загальний solution.
    - Перейдіть у проєкт "VShevchenko" `cd VShevchenko`
    - Введіть `dotnet build`
    - Введіть `dotnet pack`

    Це створить файл .nupkg у bin/Release.

3. Release the package locally

    - Відкрийте командний рядок як адміністратор у місці розташування файлу .nupkg ( тобто \VShevchenko\bin\Release).
    - Введіть `nuget add VShevchenko.1.1.0.nupkg -Source "C:\\Program Files (x86)\\Microsoft SDKs\\NuGetPackages\\"`

4. Use your local NuGet package

    - Використайте консоль диспетчера пакетів (Package Manager Console)
    - Перейдіть у проєкт "Lab03"
    - Інсталюйте локальний пакет NuGet за допомогою консолі.

    `Install-Package VShevchenko`
