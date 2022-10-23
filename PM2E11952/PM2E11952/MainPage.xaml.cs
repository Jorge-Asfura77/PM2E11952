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

using PM2E11952.Views;
using PM2E11952.Models;
using PM2E11952.Controller;

namespace PM2E11952
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetLocation();
        }

        byte[] imageToSave;
        private async void btncargarimg_Clicked(object sender, EventArgs e)
        {
            try
            {

                var takepic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "PhotoApp",
                    Name = DateTime.Now.ToString() + "_foto.jpg",
                    SaveToAlbum = true
                });

                if (takepic != null)
                {
                    imageToSave = null;
                    MemoryStream memoryStream = new MemoryStream();

                    takepic.GetStream().CopyTo(memoryStream);
                    imageToSave = memoryStream.ToArray();

                    img.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });
                }
                txtdescripcion.Focus();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo guardar la fotografía", "Hecho");
            }
        }

        public async void GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    await DisplayAlert("Error", "GPS desactivado", "Hecho");
                    txtlatitud.Text = "00.0";
                    txtlongitud.Text = "00.0";
                }


                if (location != null)
                {
                    txtlatitud.Text = location.Latitude.ToString();
                    txtlongitud.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Aviso", "GPS no soportado" + fnsEx, "Hecho");
            }
            catch (FeatureNotEnabledException)
            {
                await DisplayAlert("Aviso", "GPS desactivado, activelo y vuelva a ingresar", "Hecho");
                System.Diagnostics.Process.GetCurrentProcess().Kill();

            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Aviso", "Conceda los permiso de uso de GPS" + pEx, "Hecho");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", "Ubicacion no detectada" + ex, "Hecho");
            }
        }

        private async void btn02_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ubicacionesPage());
        }

        private async void btn03_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void btnguardar_Clicked(object sender, EventArgs e)
        {

            if (imageToSave == null)
            {
                await DisplayAlert("Datos incompletos", "Ingrese imagen", "Hecho");
            }
            else if (txtdescripcion.Text == null)
            {
                await DisplayAlert("Datos incompletos", "Ingrese descripcion", "Hecho");
            }
            else
            {
                var Ubicaciones = new Ubicaciones { imagen = imageToSave, latitud = Convert.ToDouble(this.txtlatitud.Text), longitud = Convert.ToDouble(this.txtlongitud.Text), descripcion = txtdescripcion.Text };
                var resultado = await App.Basedatos02.GrabarUbicacion(Ubicaciones);

                if (resultado != 0)
                {
                    await DisplayAlert("Operación finalizada", "Datos guardados", "Hecho");
                    txtdescripcion.Text = "";
                    img.Source = "anadir.png";
                    imageToSave = null;

                }
                else
                {
                    await DisplayAlert("Operación finalizada", "Error al guardar", "Hecho");
                }


                await Navigation.PopAsync();
            }
        }
    }
}
