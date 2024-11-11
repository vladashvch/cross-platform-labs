#!/bin/bash

curl -o dotnet-sdk.pkg "https://download.visualstudio.microsoft.com/download/pr/cb2d65e1-ad90-4416-8e6a-3755f92ba39f/f498aca4950a038d6fc55cca75eca630/dotnet-sdk-2.2.207-osx-x64.pkg"
sudo installer -pkg dotnet-sdk.pkg -target /

curl -L -o git-installer.dmg https://sourceforge.net/projects/git-osx-installer/files/git-2.33.0-intel-universal-mavericks.dmg/download
hdiutil attach git-installer.dmg -mountpoint /Volumes/Git
sudo installer -pkg /Volumes/Git/git-2.33.0-intel-universal-mavericks.pkg -target /

sudo git clone https://github.com/vladashvch/cross-platform-labs.git /Users/vagrant/Desktop/Cross-platform


cd /Users/vagrant/Desktop

for lab in Lab1 Lab2 Lab3; do
    sudo dotnet build Build.proj -p:Solution=$lab -t:Build
    sudo dotnet build Build.proj -p:Solution=$lab -t:Test
    sudo dotnet build Build.proj -p:Solution=$lab -t:Run
done

mkdir -p ~/.nuget/NuGet
cat <<EOF > ~/.nuget/NuGet/NuGet.Config
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="BaGet" value="http://192.168.50.62:5000/v3/index.json" />
  </packageSources>
</configuration>
EOF

dotnet tool install --global VShevchenko --version 1.0.1
