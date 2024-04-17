using files.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace files.ViewModel
{
    public class FolderViewModel
    {

        public FolderViewModel() {
            model.FilepathWasChanged += SetFiles;
        }

        private string selectedItem;
        public string SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                model.SelectedFile = selectedItem;
            }
        }
        public RelayCommand SelectNewPath
        {
            get => new RelayCommand(() => NewPathSelected());
        }

        private ObservableCollection<string> files = new ObservableCollection<string>();

        AppModel model = AppModel.Instance();

        public ObservableCollection<string> Files
        {
            get => files;
            set { files = value; OnPropertyChanged(nameof(files)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void SetFiles(object sender, EventArgs e)
        {
            Files.Clear();
            string[] directiries = Directory.GetDirectories(model.FilePath);
            foreach (string d in directiries)
            {
                Files.Add(d);
            }
            string[] fls = Directory.GetFiles(model.FilePath);
            foreach (string f in fls) 
            {
                Files.Add(f);
            }
        }

        private void NewPathSelected( ) 
        {
            if (Directory.Exists(SelectedItem))
                model.FilePath = SelectedItem;
            else if (File.Exists(SelectedItem))
                Process.Start(SelectedItem);
        }


    }
}
