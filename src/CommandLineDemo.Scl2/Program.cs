// See https://aka.ms/new-console-template for more information
// Example from https://learn.microsoft.com/en-us/dotnet/standard/commandline/get-started-tutorial

// ./CommandLineDemo.Scl --help 
// ./CommandLineDemo.Scl --version
// ./CommandLineDemo.Scl --file CommandLineDemo.Scl.deps.json
// ./CommandLineDemo.Scl --file CommandLineDemo.Scl.runtimeconfig.json

// ./CommandLineDemo.Scl2 --file CommandLineDemo.Scl2.deps.json
// ./CommandLineDemo.Scl2 --file CommandLineDemo.Scl2.runtimeconfig.json
// ./CommandLineDemo.Scl2 --help 
// ./CommandLineDemo.Scl2 read --help 

// --delay 0
// --delay ten

// --fgcolor Green

// --light-mode
// --light-mode true
// --light-mode false

// IsRequired
// parseArgument
// isDefault
// alias
// arrays
// arguments

using System.CommandLine;
using System.Globalization;

var fileOption = new Option<FileInfo?>(
    name: "--file",
    description: "The file to read and display on the console."
    // isDefault: true,
    // parseArgument: result =>
    // {
    //     // if (result.Tokens.Count == 0)
    //     // {
    //     //     return new FileInfo("CommandLineDemo.Scl.runtimeconfig.json");
    //     // }
    //     string? filePath = result.Tokens.Single().Value;
    //     if (!File.Exists(filePath))
    //     {
    //         result.ErrorMessage = "File does not exist";
    //         return null;
    //     }
    //     else
    //     {
    //         return new FileInfo(filePath);
    //     }
    // }
)
{
    // IsRequired = true
};
// fileOption.AddAlias("-f");

var delayOption = new Option<int>(
    name: "--delay",
    description: "Delay between lines, specified as milliseconds per character in a line.",
    getDefaultValue: () => 42);

var fgcolorOption = new Option<ConsoleColor>(
    name: "--fgcolor",
    description: "Foreground color of text displayed on the console.",
    getDefaultValue: () => ConsoleColor.White);

var lightModeOption = new Option<bool>(
    name: "--light-mode",
    description: "Background color of text displayed on the console: default is black, light mode is white.");

// var bottomLineArgument = new Argument<string>(
//     name: "bottom-line",
//     description: "Text for printing a the end.")
// {
//     
// };

// var linesOption = new Option<int[]>(
//     name: "--lines",
//     description: "Number of lines to print")
// {
//     AllowMultipleArgumentsPerToken = true
// };

var rootCommand = new RootCommand("Sample app for System.CommandLine");

var readCommand = new Command("read", "Read and display the file.")
{
    fileOption,
    delayOption,
    fgcolorOption,
    lightModeOption,
    // linesOption
};
// readCommand.AddAlias("print");
// readCommand.AddArgument(bottomLineArgument);

readCommand.SetHandler(async (file, delay, fgcolor, lightMode) =>
    {
        await ReadFile(file!, delay, fgcolor, lightMode);
    },
    fileOption, delayOption, fgcolorOption, lightModeOption);

rootCommand.AddCommand(readCommand);


return await rootCommand.InvokeAsync(args);

static async Task ReadFile(
    FileInfo file, int delay, ConsoleColor fgColor, bool lightMode)
{
    Console.BackgroundColor = lightMode ? ConsoleColor.White : ConsoleColor.Black;
    Console.ForegroundColor = fgColor;
    var lines = File.ReadLines(file.FullName).ToArray();

    // if (lineNumbers?.Any() == true)
    // {
    //     var linesNumbersHash = new HashSet<int>(lineNumbers.Distinct());
    //     lines = lines.Select((l, i) => new {Index = i, Value = l})
    //         .Where(x => linesNumbersHash.Contains(x.Index))
    //         .Select(x => x.Value)
    //         .ToArray();
    // }
    
    // if (!string.IsNullOrEmpty(bottomLine))
    // {
    //     lines = lines.Concat(new string[] {bottomLine}).ToArray();
    // }
    
    foreach (var line in lines)
    {
        Console.WriteLine(line);
        await Task.Delay(delay * line.Length);
    };
}