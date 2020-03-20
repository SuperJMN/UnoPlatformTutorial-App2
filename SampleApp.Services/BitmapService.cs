using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace SampleApp.Services
{
    public class BitmapService : IBitmapService
    {
        public Task<byte[]> Create(byte[] imageBytes, float rotation)
        {
            var stream = new MemoryStream(imageBytes);
            var image = SixLabors.ImageSharp.Image.Load(stream);
            image.Mutate(x => x.Rotate(rotation));

            var outputStream = new MemoryStream();
            image.Save(outputStream, new PngEncoder());
            return Task.FromResult(outputStream.GetBuffer());
        }
    }
}