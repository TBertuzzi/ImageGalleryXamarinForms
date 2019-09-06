using System;
using System.Collections.Generic;
using ImageGalleryXamarinForms.Helpers;
using ImageGalleryXamarinForms.Models;
using Plugin.Media;
using Xamarin.Forms;

namespace ImageGalleryXamarinForms
{
    public partial class UploadPage : ContentPage
    {
        string _filePath;
        public UploadPage()
        {
            InitializeComponent();
            Title = "ImageGallery";
            DateTimeEntry.Text = DateTime.Now.ToString();
            UploadButton.Clicked += async delegate
            {
                if (string.IsNullOrEmpty(TittleEntry.Text))
                {
                    await DisplayAlert("ImageGallery", "Titulo Obrigatorio", "OK");
                    return;
                }
                var gallery = new GalleryClass
                {
                    Title = TittleEntry.Text,
                    Path = _filePath,
                    Created = DateTime.Now
                };
                Constants.GalleryCollection.Add(gallery);
                await Application.Current.MainPage.Navigation.PopAsync();
            };
            BrowseButton.Clicked += delegate
            {
                PickImage();
            };
        }

        async void PickImage()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("ImageGallery", "Necessita de persmissao", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });
            if (file == null)
                return;
            image.Source = file.Path;
            _filePath = file.Path;
        }
    }
}
