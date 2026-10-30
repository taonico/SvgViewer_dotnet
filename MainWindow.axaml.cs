using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform.Storage;
using System.IO;
using System.Linq;

namespace SvgViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddHandler(DragDrop.DropEvent, OnDrop);
        }

        private async void OnDrop(object? sender, DragEventArgs e)
        {
            var files = e.DataTransfer.TryGetFiles();
            if (files != null)
            {
                var file = files.OfType<IStorageFile>().FirstOrDefault();
                if (file != null)
                {
                    await using var stream = await file.OpenReadAsync();
                    using var reader = new StreamReader(stream);

                    SvgViewer.Source = await reader.ReadToEndAsync();
                }
            }
        }
    }
}