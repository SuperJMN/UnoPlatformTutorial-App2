using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using SampleApp.ViewModels;

namespace SampleApp
{
    public class WasmFilePicker : IFilePicker
    {
        public WasmFilePicker()
        {
            MessageBus.Current.Listen<UrlMessage>()
                .Subscribe(message => Url = message.Url);
        }

        public string Url { get; set; }

        public IObservable<ZafiroFile> Pick(string title, string[] extensions)
        {
            return Observable.Return(new UrlFile(Url));
        }

        public IObservable<ZafiroFile> PickSave(string title, KeyValuePair<string, IList<string>>[] extensions)
        {
            throw new NotImplementedException();
        }
    }
}