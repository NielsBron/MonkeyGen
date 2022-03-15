project=RayDot
framework=netcoreapp3.1
# config=Debug
config=Release

all: run
	
build:
	dotnet build ${project}.csproj --configuration "${config}"

run: build
	dotnet exec bin/${config}/${framework}/${project}.dll

clean:
	dotnet clean ${project}.csproj --configuration "${config}"
