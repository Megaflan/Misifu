using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yarhl.FileSystem;

namespace Misifu.Models
{
    public class DirectoryRootModel
    {        
        public Node RootNode { get; set; }
        public string Name { get; set; }
        public ObservableCollection<DirectoryNodeModel> Nodes { get; private set; }
            = new ObservableCollection<DirectoryNodeModel>();
    }
}
