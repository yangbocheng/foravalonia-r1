using System.Reactive.Linq;
using System.Reactive.Subjects;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Splat;

namespace acrossSample.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            
            var textBlock = new TextBlock();
            var in1 = new TextBox();

            ListBox bl = new ListBox();
            
            // Locator.CurrentMutable
            

        }

        private void Border_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as Control);
        }
    }
}