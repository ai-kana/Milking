build:
	dotnet build -c Release
	cp ./bin/Release/netstandard2.1/Milking.dll ~/U3DS/Servers/OpenModTest/OpenMod/plugins/Milking.dll
