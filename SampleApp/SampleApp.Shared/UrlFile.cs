using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SampleApp.ViewModels;

namespace SampleApp
{
    public class UrlFile : ZafiroFile
    {
        private readonly string url;

        public UrlFile(string url)
        {
            this.url = url;
        }

        public override async Task<Stream> OpenForRead()
        {
            HttpResponseMessage openForRead;
            using (var httpClient = new HttpClient(new CorsByPassHandler()))
            {
                openForRead = await httpClient.GetAsync(url);
            }

            return await openForRead.Content.ReadAsStreamAsync();
        }

        public override Task<Stream> OpenForWrite()
        {
            throw new NotSupportedException();
        }

        public override string Name { get; }
    }
}