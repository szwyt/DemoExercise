:@echo off
color B
rd  .\publish\   /s /q
dotnet restore

dotnet build

cd ./MongoDb

dotnet publish -c Release -o ./publish

echo "Release Success"
pause