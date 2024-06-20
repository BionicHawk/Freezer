namespace Freezer.processor;

public record Configuration {
    public required ushort LimitResolutionSize;
    public required ushort Quality;
    public required List<string> FilePaths = [];
    public string outputDir = "./";

}