namespace Freezer.processor;

public static class ArgsResolver
{

    public static Configuration? GetConfiguration(string[] args)
    {
        string lastOption = "";

        ushort limitSize = 0;
        byte quality = 75;
        List<string> filePaths = [];
        string outputDir = "./";


        foreach (var arg in args)
        {
            if (arg.Equals("-s") || arg.Equals("-f") || arg.Equals("-o") || arg.Equals("-q"))
            {
                lastOption = arg;
                continue;
            }

            switch (lastOption)
            {
                case "-s":
                    if (limitSize != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("-s only accepts one argument!");
                        Console.ResetColor();
                        return null;
                    }
                    if (!ushort.TryParse(arg, out limitSize))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("-s must be an unsigned integer!");
                        Console.ResetColor();
                    }
                    break;
                case "-f":
                    if (File.Exists(arg))
                    {
                        filePaths.Add(arg);
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{arg} was not found!");
                        Console.ResetColor();
                        return null;
                    }
                case "-o":
                    if (!outputDir.Equals("./"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"-o only accepts one argument!");
                        Console.ResetColor();
                        return null;
                    }
                    if (Directory.Exists(arg))
                    {
                        outputDir = arg;
                    }
                    else
                    {
                        Directory.CreateDirectory(arg);
                        outputDir = arg;
                    }
                    break;
                case "-q":
                    if (!byte.TryParse(arg, out quality))
                    {            
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Quality value is not an 8-bit integer value!");
                        Console.ResetColor();
                        return null;
                    }
                    if (quality < 10 || quality > 100)
                    {            
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Quality value must between 10 and 100!");
                        Console.ResetColor();
                    }
                    break;
            }

        }

        if (filePaths.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No files were given!");
            Console.ResetColor();
            return null;
        }

        if (limitSize == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"No limit size given!");
            Console.ResetColor();
            return null;
        }

        if (limitSize < 8)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"The size given is too low!");
            Console.ResetColor();
            return null;
        }

        return new Configuration
        {
            FilePaths = filePaths,
            LimitResolutionSize = limitSize,
            Quality = quality,
            outputDir = outputDir
        };
    }

}