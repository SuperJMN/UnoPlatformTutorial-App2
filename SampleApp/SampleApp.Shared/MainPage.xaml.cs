﻿using Windows.UI.Xaml.Navigation;
using Zafiro.UI.Infrastructure.Uno.Navigation;

namespace SampleApp
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var message = new NavigationFrameMessage(this.Frame, this.InnerFrame);
            ReactiveUI.MessageBus.Current.SendMessage(message);
        }
    }
}
