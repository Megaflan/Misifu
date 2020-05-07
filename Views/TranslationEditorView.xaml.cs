using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Misifu.Models;
using Misifu.ViewModels;
using System;
using System.Collections.Generic;
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
        private TranslationModel translationModel;
        public TranslationEditorView()
        {
            this.InitializeComponent();
            dirTreeView = this.FindControl<TreeView>("dirTreeView");
            mainDataGrid = this.FindControl<DataGrid>("mainDataGrid");
            sourceBox = this.FindControl<TextBox>("sourceBox");
            targetBox = this.FindControl<TextBox>("targetBox");
            dirTreeView.SelectionChanged += DirTreeView_SelectionChanged;
            mainDataGrid.SelectionChanged += MainDataGrid_SelectionChanged;
            
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
                foreach (var entry in MainWindowViewModel.PoNode.Entries)
                {
                    MainWindowViewModel.Translation.Add(new TranslationModel
                    {
                        Source = entry.Original,
                        Target = entry.Text,
                    });
                }
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
