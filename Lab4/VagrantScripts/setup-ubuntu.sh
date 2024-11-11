#!/bin/bash

apt-get update
apt-get install -y wget apt-transport-https unzip

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
dpkg -i packages-microsoft-prod.deb
apt-get update
apt-get install -y dotnet-sdk-8.0
mkdir -p ~/.nuget/NuGet

dotnet nuget remove source BaGet
dotnet nuget add source http://192.168.0.103:5000/v3/index.json -n BaGet

cd /home/vagrant
dotnet new console -n Lab4 --force
cd Lab4

dotnet tool install --global VShevchenko --version 1.0.0

cd /home/vagrant/project

for lab in Lab1 Lab2 Lab3; do
    sudo dotnet build Build.proj -p:Solution=$lab -t:Build
    sudo dotnet build Build.proj -p:Solution=$lab -t:Run
    sudo dotnet build Build.proj -p:Solution=$lab -t:Test
done
