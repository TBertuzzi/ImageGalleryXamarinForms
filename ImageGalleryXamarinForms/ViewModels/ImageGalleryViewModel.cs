using System.Windows.Input;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ImageGalleryXamarinForms.Models;
using ImageGalleryXamarinForms.Helpers;

namespace ImageGalleryXamarinForms.ViewModels
{
    public class ImageGalleryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<GalleryClass> GalleryCollection;
        private int _maxColumns;
        private ObservableCollection<GalleryClass> _parentModels;
        private float _tileHeight;

        public event PropertyChangedEventHandler PropertyChanged;


        private void RaisePropertyChanged([CallerMemberName] string propertyname = null)
        {
            if (PropertyChanged != null)
            {
                if (!string.IsNullOrEmpty(propertyname))
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
                }
            }
        }

        public ImageGalleryViewModel()
        {
            _parentModels = new ObservableCollection<GalleryClass>();
            ParentModels = new ObservableCollection<GalleryClass>();
            ItemTapCommand = new Command<GalleryClass>(OnParentTapped);
            MaxColumns = 2;
            TileHeight = 100;
        }

        public ICommand ItemTapCommand { get; private set; }

        public int MaxColumns
        {
            get { return _maxColumns; }
            set
            {
                _maxColumns = value; RaisePropertyChanged();
            }
        }

        public ObservableCollection<GalleryClass> ParentModels
        {
            get { return _parentModels; }
            set
            {
                _parentModels = value;
                RaisePropertyChanged();
            }
        }

        public float TileHeight
        {
            get { return _tileHeight; }
            set { _tileHeight = value; RaisePropertyChanged(); }
        }

        internal void LoadData()
        {
            if (Constants.GalleryCollection != null)
            {
                ParentModels = Constants.GalleryCollection;
            }
        }

        private void OnParentTapped(GalleryClass item)
        {
            Application.Current.MainPage.DisplayAlert("ImageGallery", "Selected " + item.Title, "Ok");
        }
    }
}
