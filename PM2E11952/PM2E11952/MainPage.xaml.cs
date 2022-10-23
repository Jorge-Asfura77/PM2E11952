using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PM2E11952.Controller;
using PM2E11952.Models;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Plugin.Media;
using Xamarin.Forms.Xaml;
using System.IO;

namespace PM2E11952
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        string base64ball = "";
        public MainPage()
        {
            InitializeComponent();
            GetLocation();
        }
        private async void btncargarimg_Clicked(object sender, EventArgs e)
        {
            var tomarfoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "miApp",
                Name = "Image.jpg"

            });

            if (tomarfoto != null)
            {
                imagen.Source = ImageSource.FromStream(() => { return tomarfoto.GetStream(); });
            }

            Byte[] imagenByte = null;

            using (var stream = new MemoryStream())
            {
                tomarfoto.GetStream().CopyTo(stream);
                tomarfoto.Dispose();
                imagenByte = stream.ToArray();

                base64ball = Convert.ToBase64String(imagenByte);

            }

        }

        public async void GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "Active el GPS", "Hecho");
                    txtlatitud.Text = "00.0";
                    txtlongitud.Text = "00.0";
                }


                if (location != null)
                {
                    txtlatitud.Text = location.Latitude.ToString();
                    txtlongitud.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {

                // Handle not enabled on device exception
            }
            catch (PermissionException)
            {
                // Handle permission exception
            }
            catch (Exception)
            {
                // Unable to get location
            }
        }

        private async void btn02_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ubicacionesPage());
        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {
            try
            {
                var ubicaciones = new Models.Ubicaciones
                {
                    latitud = Convert.ToDouble(this.txtlatitud.Text),
                    longitud = Convert.ToDouble(this.txtlongitud.Text),
                    descripcion = this.txtdescripcion.Text,
                    base64 = base64ball

                };

                var resultado = await App.Basedatos02.GrabarUbicacion(ubicaciones);


                if (resultado == 1)
                {
                    await DisplayAlert("Operación finalizada", "Datos guardados", "Hecho");
                }
                else
                {
                    await DisplayAlert("Operación finalizada", "Error al guardar", "Hecho");

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Operacion finalizada", ex.Message.ToString(), "OK");

            }
        }
    }
}
