using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using Plugin.Media;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Map = Xamarin.Essentials.Map;
using PM2E11952.Models;

namespace PM2E11952.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {

        Double lati, longi;
        String mapdescripcion;

        public MapPage(double latitud, double longitud, string descripcion)
        {
            InitializeComponent();

            lati = latitud;
            longi = longitud;
            mapdescripcion = descripcion;
        }

        private async void btnCompartir_Clicked(object sender, EventArgs e)
        {
            
        }

        private void toolmenu_Clicked(object sender, EventArgs e)
        {

        }

        protected async void OnAppearing()
        {
            base.OnAppearing();

            //await Map.OpenAsync(lati, longi, new MapLaunchOptions
            //{
            //    Name = mapdescripcion,
            //    NavigationMode = NavigationMode.None
            //});

            //MapLaunchOptions options = new MapLaunchOptions { Name = "Mi posición actual" };
            //await Map.OpenAsync(lati, longi, options);


            //Pin ubicacion = new Pin();
            //ubicacion.Label = mapdescripcion.ToString();
            //ubicacion.Type = PinType.Place;
            //ubicacion.Position = new Position(longi, lati);
            //mapa.Pins.Add(ubicacion);
            //mapa.IsShowingUser = true;
            //mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(longi,
            //lati), Distance.FromMeters(500.0)));
        }

    }
}