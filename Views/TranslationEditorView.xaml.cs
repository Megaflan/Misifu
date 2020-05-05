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
        private TranslationModel entry;
        private Po po;
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
                entry = (TranslationModel)mainDataGrid.SelectedItem;
                sourceBox.Text = entry.Source;
                targetBox.Text = entry.Target;                
            }
            catch (Exception)
            {
            }
        }

        private void DirTreeView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                entry = null; //Dispose a la selección del MainDataGrid
                DirectoryNodeModel a = (DirectoryNodeModel)dirTreeView.SelectedItem;
                if (a.Node.Format.GetType().Name == "Po")
                    po = a.Node.GetFormatAs<Po>();
                else
                    po = a.Node.TransformWith<Binary2Po>().GetFormatAs<Po>();
                MainWindowViewModel.Translation.Clear();
                foreach (var entry in po.Entries)
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
