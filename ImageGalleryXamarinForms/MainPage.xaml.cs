using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageGalleryXamarinForms.Helpers;
using ImageGalleryXamarinForms.Models;
using ImageGalleryXamarinForms.ViewModels;
using Xamarin.Forms;

namespace ImageGalleryXamarinForms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ImageGalleryViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            Title = "Custom Gallery";
            _viewModel = new ImageGalleryViewModel();
            this.BindingContext = _viewModel;
            NavigationPage.SetHasBackButton(this, false);
            Constants.GalleryCollection = new ObservableCollection<GalleryClass>();
            SearchEntry.TextChanged += (sender, e) => SearchProjects(SearchEntry.Text);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadData();
            if (Constants.GalleryCollection != null && Constants.GalleryCollection.Count > 0)
                customGrid.ItemsSource = Constants.GalleryCollection;
        }

        void UploadClicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new UploadPage());
        }

        public void SearchProjects(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                _viewModel.ParentModels = Constants.GalleryCollection;
            }
            else
            {
                var collection = Constants.GalleryCollection;
                //var _collection = Constants.GalleryCollection.Where(x => (x.Title.ToLower().Contains(filter.ToLower())));
                var _collection = collection.Where(x => (x.Title.ToLower().Contains(filter.ToLower())));
                _viewModel.ParentModels = new ObservableCollection<GalleryClass>(_collection);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}
