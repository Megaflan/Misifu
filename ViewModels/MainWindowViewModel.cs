using Misifu.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Misifu.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<TranslationModel> Translation { get; }

        public MainWindowViewModel()
        {
            Translation = new ObservableCollection<TranslationModel>(TestModel());
        }


        private IEnumerable<TranslationModel> TestModel()
        {
            var testModel = new List<TranslationModel>()
            {
                new TranslationModel()
                {
                    Source = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    Target = "",
                },
                new TranslationModel()
                {
                    Source = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    Target = "",
                },
                new TranslationModel()
                {
                    Source = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                    Target = "",
                }
            };

            return testModel;
        }
    }
}
