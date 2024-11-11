if (-not (Get-Command "choco" -ErrorAction SilentlyContinue)) {
    Set-ExecutionPolicy Bypass -Scope Process -Force
    [System.Net.WebClient]::new().DownloadString('https://chocolatey.org/install.ps1') | Invoke-Expression
}

choco install dotnet-8.0-sdk -y
choco install dotnet-8.0-runtime -y

if (Get-Command "dotnet" -ErrorAction SilentlyContinue) {
    Write-Host ".NET Core 8 successfully installed."
    dotnet --list-sdks
    dotnet --list-runtimes

    cd C:/
    dotnet new console -n Lab4 --force
    cd Lab4

    dotnet nuget add source http://192.168.0.103:5000/v3/index.json -n BaGet
    dotnet tool install --global VShevchenko --version 1.0.1

    cd C:/vagrant/project

    $labs = @("Lab1", "Lab2", "Lab3")
    foreach ($lab in $labs) {
        dotnet build Build.proj -p:Solution=$lab -t:Build
        dotnet build Build.proj -p:Solution=$lab -t:Test
        dotnet build Build.proj -p:Solution=$lab -t:Run
    }
} else {
    Write-Host ".NET Core 8 installation failed. Manual intervention required."
}