# Command create project structure
```
mkdir AspNetCore-DDD
cd AspNetCore-DDD
dotnet new globaljson --sdk-version 3.0.100
dotnet new sln
mkdir src
cd src
mkdir Common
cd Common
dotnet new classlib -f netcoreapp3.0
```

- Open .sln file with Visual Studio
- Right click on Solution > Add > Existing Item > global.json
- Right click on Solution > Add > New Solution Folder > src
- Right click on `src` folder > Add > Existing Project > Common.csproj

# References

```
dotnet new sln -n AspNetCore-DDD
dotnet new classlib -n Common --framework netcoreapp3.0
dotnet new console
dotnet new mstest
dotnet new web (Web/Empty)
dotnet new mvc (Web/MVC)
dotnet new webapp (Web/MVC/Razor Pages)
dotnet new webapi (Web/WebAPI)
dotnet new gitignore
dotnet new globaljson
dotnet new nugetconfig
dotnet new webconfig

-n|--name <OUTPUT_NAME>
The name for the created output. If no name is specified, the name of the current directory is used.

-o|--output <OUTPUT_DIRECTORY>
Location to place the generated output. The default is the current directory.
```
