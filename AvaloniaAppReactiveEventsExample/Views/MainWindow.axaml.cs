using Avalonia.Controls;
using Avalonia.Input;
using System.Diagnostics;
using System.Reactive.Linq;
using System;

namespace AvaloniaAppReactiveEventsExample.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var codes = new[]
            {
                Key.Up,
                Key.Up,
                Key.Down,
                Key.Down,
                Key.Left,
                Key.Right,
                Key.Left,
                Key.Right,
                Key.A,
                Key.B
            };

            // convert the array into an sequence
            var koanmi = codes.ToObservable();

            this.Events().KeyUp

                // we want the keycode
                .Select(x => x.Key)
                .Do(key => TextEvent.Text = $"{key} was pressed.")

                // get the last ten keys
                .Window(10)

                // compare to known konami code sequence
                .SelectMany(x => x.SequenceEqual(koanmi))
                .Do(isMatch => Debug.WriteLine(isMatch))

                // where we match
                .Where(x => x)
                .Do(x => TextEvent.Text = "Konami sequence")
                .Subscribe(x => Debug.WriteLine(x));
        }
    }
}