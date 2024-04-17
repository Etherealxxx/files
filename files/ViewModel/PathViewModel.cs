using files.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace files.ViewModel
{
    public class PathViewModel : INotifyPropertyChanged
    {
        public PathViewModel() {
            model.FilepathWasChanged += SetFilePath;
        }

        public RelayCommand Back
        {
            get => new RelayCommand(() => FilepathBack());
        }
        public RelayCommand Remove
        {
            get => new RelayCommand(() => RemoveFile());
        }
        public RelayCommand CreateDirectory
        {
            get => new RelayCommand(() => CreateNewDirectory());
        }
        public RelayCommand CreateFile
        {
            get => new RelayCommand(() => CreateNewFile());
        }
        public RelayCommand Rename
        {
            get => new RelayCommand(() => RenameFile());
        }

        AppModel model = AppModel.Instance();

        private string filepath;
        public string FilePath
        {
            get => filepath;
            set { filepath = value; OnPropertyChanged(nameof(FilePath)); }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void SetFilePath (object sender, EventArgs e)
        {
            FilePath = model.FilePath;
        }

        private void FilepathBack()
        {
            if (model.FilePath == null) { return; }
            model.FilePath = model.FilePath.Remove(model.FilePath.LastIndexOf(@"\"));
        }

        private void CreateNewDirectory()
        {
            if (model.FilePath == null) { return; }
            InputBox inputBox = new InputBox();
            inputBox.ShowDialog();

            if ((bool)inputBox.DialogResult)
            {
                Directory.CreateDirectory(model.FilePath + @"\" + inputBox.InputText);
            }
            model.FilePath = model.FilePath;

        }

        private void CreateNewFile()
        {
            if (model.FilePath == null) { return; }

            InputBox inputBox = new InputBox();
            inputBox.ShowDialog();

            if ((bool)inputBox.DialogResult)
            {
                File.Create(model.FilePath + @"\" + inputBox.InputText);
            }
            model.FilePath = model.FilePath;
        }

        private void RemoveFile()
        {
            if (model.SelectedFile == null) { return; }

            if (File.Exists(model.SelectedFile))
            {
                File.Delete(model.SelectedFile);
            }
            else if (Directory.Exists(model.SelectedFile))
            {
                Directory.Delete(model.SelectedFile);
            }
            model.FilePath = model.FilePath;
        }

        private void RenameFile()
        {
            if (model.SelectedFile == null) { return; }

            if (File.Exists(model.SelectedFile))
            {
                File.Move(model.SelectedFile, NewName_Rename());
            }
            else if (Directory.Exists(model.SelectedFile))
            {
                Directory.Move(model.SelectedFile, NewName_Rename());
            }
            model.FilePath = model.FilePath;
        }

        private string NewName_Rename()
        {
            InputBox inputBox = new InputBox();
            inputBox.ShowDialog();
            if ((bool)inputBox.DialogResult)
            {
                return model.SelectedFile.Remove(model.SelectedFile.LastIndexOf(@"\")) + @"\"+ inputBox.InputText;
            }
            return null;
        }
    }
}
