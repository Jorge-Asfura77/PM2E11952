using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;



namespace PM2E11952.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ubicacionesPage : ContentPage
    {
        public ubicacionesPage()
        {
            InitializeComponent();
           
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var listaubicaciones = await App.Basedatos02.ObtenerListaUbicaciones();
            lstUbicaciones.ItemsSource = listaubicaciones;
            
        }

        private async void tlbeliminar_Clicked(object sender, EventArgs e)
        
        {


            var ubicacion = new Models.Ubicaciones
            {
                codigo = Convert.ToInt32(this.txtcodigo.Text),
                latitud = Convert.ToDouble(this.txtlatitud.Text),
                longitud = Convert.ToDouble(this.txtlongitud.Text),
                descripcion = this.txtdescripcion.Text,
            };

            if (await App.Basedatos02.EliminarUbicacion(ubicacion) != 0)
                await DisplayAlert("Operación finalizada", "Datos eliminados", "Hecho");
            else
                await DisplayAlert("Operación finalizada", "Error al eliminar datos", "Hecho");

        }

        public async void lstUbicaciones_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Models.Ubicaciones item = (Models.Ubicaciones)e.Item;

            this.txtcodigo.Text = Convert.ToString( item.codigo);
            this.txtlatitud.Text = Convert.ToString(item.latitud);
            this.txtlongitud.Text = Convert.ToString(item.longitud);
            this.txtdescripcion.Text = Convert.ToString(item.descripcion);
        }

        private async void tlbver_Clicked(object sender, EventArgs args1)
        {
            double lati = double.Parse(txtlatitud.Text);
            double longi = double.Parse(txtlongitud.Text);

            await DisplayAlert(txtlatitud.Text, txtlongitud.Text, "Cancelar");

            //await Map.OpenAsync(lati, longi, new MapLaunchOptions
            //{
            //    Name = "Hola",
	           // NavigationMode = NavigationMode.None
            //});
            //var page = new Views.MapPage();
            //await Navigation.PushAsync(page);

            //MapLaunchOptions options = new MapLaunchOptions { Name = "Mi posición actual" };
            //await Map.OpenAsync(lati, longi, options);
        }

        private async void tlbcompartir_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Compartiendo imagen", "Espere...", "Cancelar");
        }
    }
    

    
}