// See https://aka.ms/new-console-template for more information
// Example from https://learn.microsoft.com/en-us/dotnet/standard/commandline/get-started-tutorial
// ./CommandLineDemo.Scl --help 
// ./CommandLineDemo.Scl --version
// ./CommandLineDemo.Scl --file CommandLineDemo.Scl.deps.json
// ./CommandLineDemo.Scl --file CommandLineDemo.Scl.runtimeconfig.json

using System.CommandLine;

var fileOption = new Option<FileInfo?>(
    name: "--file",
    description: "The file to read and display on the console.");

var rootCommand = new RootCommand("Sample app for System.CommandLine");
rootCommand.AddOption(fileOption);

rootCommand.SetHandler((file) => 
    { 
        ReadFile(file!); 
    },
    fileOption);

return await rootCommand.InvokeAsync(args);

static void ReadFile(FileInfo file)
{
    File.ReadLines(file.FullName).ToList()
        .ForEach(line => Console.WriteLine(line));
}