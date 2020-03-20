using System.Threading.Tasks;

namespace SampleApp.Services
{
    public interface IBitmapService
    {
        Task<byte[]> Create(byte[] imageBytes, float rotation);
    }
}