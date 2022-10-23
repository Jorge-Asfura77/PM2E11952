using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E11952.Controller;
using PM2E11952.Models;
using System.IO;

namespace PM2E11952
{
    public partial class App : Application
    {


        static BaseSQLite basedatos;

        public static BaseSQLite Basedatos02
        {
            get
            {

                if (basedatos == null)
                {
                    basedatos = new BaseSQLite(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PM2E11952.db3"));
                }

                return basedatos;

            }


        }

        

        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            MainPage = new NavigationPage(new MainPage());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
