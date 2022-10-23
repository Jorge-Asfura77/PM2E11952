using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PM2E11952.Views;
using PM2E11952.Models;
using PM2E11952.Converters;
using System.IO;

namespace PM2E11952.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ubicacionesPage : ContentPage
    {
        

        public ubicacionesPage()
        {
            InitializeComponent();
           
        }

        private Ubicaciones sitio;

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            listasitios.ItemsSource = await App.Basedatos02.ObtenerListaUbicaciones();
        
        }

        private async void tlbeliminar_Clicked(object sender, EventArgs e)
        
        {try
            {
                var eliminar = await App.Basedatos02.EliminarUbicacion(sitio);


                if (eliminar != 0)
                {
                    await DisplayAlert("Aviso", "Sitio eliminado", "Hecho");
                    listasitios.ItemsSource = await App.Basedatos02.ObtenerListaUbicaciones();

                }
                else
                {
                    await DisplayAlert("Aviso", "Error", "Hecho");
                }
            }
            catch
            {
                await DisplayAlert("Aviso", "Seleccione sitio a eliminar", "Hecho");
            }
        }

        private async void tlbver_Clicked(object sender, EventArgs args1)
        {
            // El mapa se abrirá en la aplicación Google Maps
            await Map.OpenAsync(sitio.latitud, sitio.longitud, new MapLaunchOptions
            {
                Name = sitio.descripcion,
                NavigationMode = NavigationMode.None
            });

            //try
            //{
            //    await Navigation.PushAsync(new MapPage(sitio.latitud, sitio.longitud, sitio.descripcion));
            //}
            //catch
            //{
            //    await DisplayAlert("Advertencia", "Favor seleccione el sitio donde desea ver en el mapa", "Ok");
            //}
        }

        private async void tlbcompartir_Clicked(object sender, EventArgs args1)
        {
            var lati = sitio.latitud.ToString();
            var longi = sitio.longitud.ToString();
            
            
                await Share.RequestAsync(
                   new ShareTextRequest
                   {
                       Title = "Ubicacion",
                       Text = "Te comparto mi ubicación",
                       Uri = "https://maps.google.com/?q="
                   }
                    );
            
            
        }


        private void listasistios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            sitio = (Ubicaciones)e.Item;

        }
    }
    

    
}