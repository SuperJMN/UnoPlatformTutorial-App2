using System.IO;
using System.Threading.Tasks;

namespace SampleApp.Infrastructure
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ReadFully(this Stream stream)
        {
            var buffer = new byte[32768];
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    int read = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (read <= 0)
                    {
                        return ms.ToArray();
                    }

                    await ms.WriteAsync(buffer, 0, read);
                }
            }
        }
    }
}