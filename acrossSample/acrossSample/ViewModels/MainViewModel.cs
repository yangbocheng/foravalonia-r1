using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;
using Serilog;
using Splat;

namespace acrossSample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // public MainViewModel()
        // {
        //     //todo 这里还没有传入 viewmodel
        //     // GoNext = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute())
        // }
        //
        // public RoutingState Router { get; } = new RoutingState();
        // public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }
        // public ReactiveCommand<Unit, IRoutableViewModel?> GoBack => Router.NavigateBack;
        
        public MainViewModel()
        {
            NextCommand = ReactiveCommand.Create<string>((str) =>
            {
                onNext();
            });

            PreviewCommand = ReactiveCommand.Create<string>((str) =>
            {
                onPreview();
            });
            
            doran();
        }

        private Bitmap _selectImage;

        public Bitmap SelectImage
        {
            get => _selectImage;
            set => this.RaiseAndSetIfChanged(ref _selectImage, value);
        }

        public ReactiveCommand<string,Unit> NextCommand { get; }
        public ReactiveCommand<string,Unit> PreviewCommand { get; }

        private int tmpi = 0;
        private void doran()
        {
            {
                var li = new List<string>() { "11.PNG", "22.PNG", "33.PNG" };
                var one = li[(tmpi++)%li.Count];
                var src = $"avares://acrossSample/Assets/png/{one}";
                var loader = AvaloniaLocator.Current.GetService<IAssetLoader>();
                SelectImage = new Bitmap(loader.Open(new Uri(src)));
            }
        }
        public void onNext()
        {
            Console.WriteLine("onNext");
            doran();
        }

        public void onPreview()
        {
            Console.WriteLine("onPreview");
            doran();
        }
    }
}