using Freezer.processor;

namespace Freezer;

public static class Program
{

    public static void Main(string[] args)
    {
        var configuration = ArgsResolver.GetConfiguration(args);
        if (configuration == null)
            return;
        var compressor = new ImageCompressor();
        compressor.Compress(configuration);
    }

}