using files.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
namespace files.ViewModel
{
    public class DrivesViewModel : INotifyPropertyChanged
    {
        public DrivesViewModel() { FindAllDrivers(); }
                
        private ObservableCollection<string> drivers = new ObservableCollection<string>();

        AppModel model = AppModel.Instance();

        public ObservableCollection<string> Drivers
        {
            get => drivers;
            set { drivers = value; OnPropertyChanged(nameof(Drivers)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void FindAllDrivers()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in allDrives)
            {
                Drivers.Add(drive.Name);
            }
        }

        private string selectedItem;
        public string SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                model.FilePath = selectedItem;
                OnPropertyChanged(nameof(Drivers));
            }
        }
    }
}
