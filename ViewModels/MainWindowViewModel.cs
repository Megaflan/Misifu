using Avalonia.Controls;
using DynamicData.Binding;
using Misifu.Models;
using Misifu.Views.Windows;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileSystem;
using Yarhl.Media.Text;

namespace Misifu.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public static ObservableCollection<TranslationModel> Translation { get; set; }
        public ObservableCollection<DirectoryRootModel> Directories { get; set; }
        private Po po;

        public MainWindowViewModel()
        {
            Translation = new ObservableCollection<TranslationModel>(TranslationModel());
            Directories = new ObservableCollection<DirectoryRootModel>();
        }

        public async Task OpenFolderEvent()
        {
            var folderDialog = new OpenFolderDialog();
            var result = await folderDialog.ShowAsync(MainWindow.Instance);
            if (result != null)
            {
                folderDialog.Directory = result;
                var folderNode = NodeFactory.FromDirectory(result, "*.po");
                var dirModel = new DirectoryRootModel()
                {
                    RootNode = folderNode,
                    Name = folderNode.Path,                    
                };

                foreach (var node in folderNode.Children)
                {
                    dirModel.Nodes.Add(new DirectoryNodeModel
                    {
                        Name = node.Name,
                        Node = node,
                    });
                }

                Directories.Add(dirModel);
            }            
        }

        public async Task OpenFileEvent()
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filters.Add(new FileDialogFilter()
            {
                Name = "PO format",
                Extensions = new List<string> { "*.po" }
            });
            var result = await fileDialog.ShowAsync(MainWindow.Instance);
            if (result != null)
            {
                try
                {                    
                    var a = NodeFactory.FromFile(result[0]);
                    if (a.Format.GetType().Name == "Po")
                        po = a.GetFormatAs<Po>();
                    else
                        po = a.TransformWith<Binary2Po>().GetFormatAs<Po>();
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
        }

        private IEnumerable<TranslationModel> TranslationModel()
        {
            var model = new List<TranslationModel>();

            return model;
        }
    }
}
