using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CookingBook.Views;
using CookingBook.Data;
using System.IO;

namespace CookingBook
{
    public partial class App : Application
    {
        static Database database;

        public static Database Database
        {
            get
            {
                if(database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Recipes.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
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
