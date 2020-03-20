﻿using System.IO;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Pickers;
using Plugin.FilePicker.Abstractions;
using SampleApp.ViewModels;

namespace SampleApp
{
    public class CrossPlatformFile : ZafiroFile
    {
        private readonly FileData data;

        public CrossPlatformFile(FileData data)
        {
            this.data = data;
        }

        public override Task<Stream> OpenForRead()
        {
            var stream = data.GetStream();
            stream.Seek(0, SeekOrigin.Begin);
            return Task.FromResult(stream);
        }

        public override Task<Stream> OpenForWrite()
        {
            var stream = data.GetStream();
            return Task.FromResult(stream);
        }

        public override string Name => data.FileName;
    }
}