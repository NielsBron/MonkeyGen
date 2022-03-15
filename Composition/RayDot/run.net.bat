@ECHO OFF

SET project="RayDot"
SET framework="netcoreapp3.1"
SET config="Release"

dotnet build %project%.csproj --configuration "%config%"
dotnet exec bin/%config%/%framework%/%project%.dll
dotnet clean %project%.csproj --configuration "%config%"

@REM PAUSE
