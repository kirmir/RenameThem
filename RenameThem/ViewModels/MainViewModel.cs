using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using CS.MVVM.Commands;

namespace RenameThem.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string _renameDirectory;
        private string _renamePattern;
        private RelayCommand _renameCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel(string directory = null)
        {
            _renameDirectory = directory;
            _renamePattern = "{fn}.{ext}";
            _renameCommand = new RelayCommand(rename, canRename);
        }

        public string RenameDirectory
        {
            get { return _renameDirectory; }
            set
            {
                if (_renameDirectory != value)
                {
                    _renameDirectory = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string RenamePattern
        {
            get { return _renamePattern; }
            set
            {
                if (_renamePattern != value)
                {
                    _renamePattern = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand RenameCommand
        {
            get { return _renameCommand; }
            set
            {
                if (_renameCommand != value)
                {
                    _renameCommand = value;
                    RaisePropertyChanged();
                }
            }
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void rename(object obj)
        {
            var files = Directory.GetFiles(RenameDirectory, "*.*", SearchOption.TopDirectoryOnly);
            foreach (var fullPath in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(fullPath);
                var extension = Path.GetExtension(fullPath);
                var directory = Path.GetDirectoryName(fullPath);

                var newFileName = RenamePattern.Replace("{fn}", fileName).Replace("{ext}", extension);
                // ReSharper disable once AssignNullToNotNullAttribute
                var newFullPath = Path.Combine(directory, newFileName);

                File.Move(fullPath, newFullPath);
            }
        }

        private bool canRename(object obj)
        {
            return !string.IsNullOrEmpty(_renameDirectory) && !string.IsNullOrEmpty(_renamePattern);
        }
    }
}
