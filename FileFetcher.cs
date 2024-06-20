namespace Freezer.processor;

public class FileFetcher(Configuration configuration)
{

    private readonly Configuration _configuration = configuration;

    public List<FileStream> GetFiles()
    {

        List<FileStream> files = [];

        foreach (var path in _configuration.FilePaths)
        {
            files.Add( File.Open( path, FileMode.Open));
        }

        return files;
    }

}