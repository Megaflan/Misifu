using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Misifu.Models;
using Misifu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.IO;
using Yarhl.Media.Text;

namespace Misifu.Views
{    
    public class TranslationEditorView : UserControl
    {
        private TreeView dirTreeView;
        private DataGrid mainDataGrid;
        private TextBox sourceBox;
        private TextBox targetBox;
        private TextBlock statusBox;
        private TranslationModel translationModel;
        public TranslationEditorView()
        {
            this.InitializeComponent();
            dirTreeView = this.FindControl<TreeView>("dirTreeView");
            mainDataGrid = this.FindControl<DataGrid>("mainDataGrid");
            sourceBox = this.FindControl<TextBox>("sourceBox");
            targetBox = this.FindControl<TextBox>("targetBox");
            statusBox = this.FindControl<TextBlock>("statusBox");
            dirTreeView.SelectionChanged += DirTreeView_SelectionChanged;
            mainDataGrid.SelectionChanged += MainDataGrid_SelectionChanged;
            targetBox.LostFocus += TargetBox_LostFocus;
            targetBox.KeyUp += TargetBox_KeyUp;
            
        }

        private void TargetBox_KeyUp(object sender, Avalonia.Input.KeyEventArgs e)
        {
            var sourceCharCount = sourceBox.Text.Length;
            var targetCharCount = targetBox.Text.Length;
            var sourceWordCount = sourceBox.Text.Split(' ').Length;
            var targetWordCount = targetBox.Text.Split(' ').Length;
            statusBox.Text = "Source: " + sourceCharCount + " | " + sourceWordCount + "    Target: " + targetCharCount + " | " + targetWordCount;
        }

        private void TargetBox_LostFocus(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            translationModel.Target = targetBox.Text;
            MainWindowViewModel.Translation[translationModel.ModelID] = translationModel;
        }

        private void MainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                translationModel = (TranslationModel)mainDataGrid.SelectedItem;
                if (translationModel != null)
                {
                    sourceBox.Text = translationModel.Source;
                    targetBox.Text = translationModel.Target;
                }

                var sourceCharCount = sourceBox.Text.Length;
                var targetCharCount = targetBox.Text.Length;
                var sourceWordCount = sourceBox.Text.Split(' ').Length;
                var targetWordCount = targetBox.Text.Split(' ').Length;
                statusBox.Text = "Source: " + sourceCharCount + " | " + sourceWordCount + "    Target: " + targetCharCount + " | " + targetWordCount;
            }
            catch (Exception)
            {
            }
        }

        private void DirTreeView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {                
                DirectoryNodeModel a = (DirectoryNodeModel)dirTreeView.SelectedItem;
                if (a.Node.Format.GetType().Name == "Po")
                    MainWindowViewModel.PoNode = a.Node.GetFormatAs<Po>();
                else
                    MainWindowViewModel.PoNode = a.Node.TransformWith<Binary2Po>().GetFormatAs<Po>();
                MainWindowViewModel.Translation.Clear();
                var c = 0;
                foreach (var entry in MainWindowViewModel.PoNode.Entries)
                {
                    MainWindowViewModel.Translation.Add(new TranslationModel
                    {
                        ModelID = c,
                        Source = entry.Original,
                        Target = entry.Text,
                    });
                    c++;
                }
                MainWindowViewModel.FileInfo = a.Node.Tags["FileInfo"];
            }
            catch (Exception)
            {
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
