using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using SampleApp.Services;
using TestApp.Shared;
using Zafiro.UI.Infrastructure.Uno;

namespace SampleApp.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private float angle;
        private readonly ObservableAsPropertyHelper<byte[]> destination;
        private readonly ObservableAsPropertyHelper<byte[]> source;
        private readonly ObservableAsPropertyHelper<bool> isLoading;

        public MainViewModel(IBitmapService bitmapService, IFilePicker filePicker, IDialogService dialogService)
        {
            BrowseFile = ReactiveCommand
                .CreateFromObservable(() => Pick(filePicker));
            Rotate = ReactiveCommand.CreateFromTask(() => bitmapService.Create(Source, Angle), BrowseFile.Any());
            Rotate.ShowExceptions(dialogService);

            source = BrowseFile.ToProperty(this, x => x.Source);
            destination = Rotate.ToProperty(this, x => x.Destination);
            Angle = 90f;

            isLoading = Rotate.IsExecuting.ToProperty(this, x => x.IsLoading);

            this.WhenAnyValue(x => x.Url)
                .Subscribe(s => MessageBus.Current.SendMessage(new UrlMessage(s)));
        }

        private IObservable<byte[]> Pick(IFilePicker filePicker)
        {
            return filePicker.Pick("Select an image", new[] {".png", ".jpg"})
                .Where(file => file != null)
                .SelectMany(x => Observable.FromAsync(() => ToBytes(x)));
        }

        public bool IsLoading => isLoading.Value;

        public ReactiveCommand<Unit, byte[]> BrowseFile { get; }

        private async Task<byte[]> ToBytes(ZafiroFile zafiroFile)
        {
            using (var stream = await zafiroFile.OpenForRead())
            {
                return await stream.ReadFully();
            }
        }
        
        public ReactiveCommand<Unit, byte[]> Rotate { get; }

        public float Angle
        {
            get => angle;
            set => this.RaiseAndSetIfChanged(ref angle, value);
        }

        public byte[] Source => source.Value;

        public byte[] Destination => destination.Value;

        private string url;

        public string Url
        {
            get => url;
            set => this.RaiseAndSetIfChanged(ref url, value);
        }
    }
}