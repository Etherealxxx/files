using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace files.Model
{
    public class AppModel
    {
        public EventHandler FilepathWasChanged;

        private static AppModel instance;
        private AppModel() { }
        public static AppModel Instance()
        {
            if (instance == null)
                instance = new AppModel();
            return instance;
        }
        private string filepath;
        public string FilePath { get=>filepath; set { filepath = value; FilepathWasChanged?.Invoke(this, EventArgs.Empty); } }

        private string selectedFile;
        public string SelectedFile { get => selectedFile; set { selectedFile = value;} }
    }
}
