using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using System;
using System.Linq;

namespace Misifu.Views.Windows
{
    public class MainWindow : Window
    {
        public static MainWindow Instance;
        public MainWindow()
        {
            this.InitializeComponent();
            Instance = this;            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
