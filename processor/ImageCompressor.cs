using ImageMagick;

namespace Freezer.processor;

public class ImageCompressor
{

    public void Compress(Configuration config)
    {
        foreach (var path in config.FilePaths)
        {
           var image = new MagickImage(path); 
           var height = image.Height;
           var width = image.Width;

            var ratio = width / height;

            if (width > height)
            {
                width = config.LimitResolutionSize;
                height = config.LimitResolutionSize / ratio;
                image.Resize(width, height);
            }
            else
            {
                width = config.LimitResolutionSize;
                height = ratio * config.LimitResolutionSize;
                image.Resize(width, height);
            }
            
            image.Quality = config.Quality;
            var filename = Path.GetFileName(path);
            image.Write(Path.Combine(config.outputDir, filename));
            image.Dispose();
        }

        Console.WriteLine("Done!");

    }

}