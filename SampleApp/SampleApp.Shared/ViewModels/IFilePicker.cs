using System;
using System.Collections.Generic;

namespace SampleApp.ViewModels
{
    public interface IFilePicker
    {
        IObservable<ZafiroFile> Pick(string title, string[] extensions);
        IObservable<ZafiroFile> PickSave(string title, KeyValuePair<string, IList<string>>[] extensions);
    }
}