using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppIsphereTravelers.Views;
using AppIsphereTravelers.Models;
using AppIsphereTravelers.Classes;
using System.Collections.Generic;
using System.Linq;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace AppIsphereTravelers
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            //MainPage = new MainPage();
            var Bd = new clsBd();
            Bd.AbreBanco();
            Bd.Cn.CreateTable<UserLogin>();
            List<UserLogin> Retorno = Bd.Cn.Table<UserLogin>().ToList(); ;

            if (Retorno.Count == 0)
            {
                var login = new LoginPage();
                App.Current.MainPage = new NavigationPage(login);
                //await NavigationService.NavigateAsync("");
            }
            else
            {
                var MinhaPagina = new PrincipalPage();
                MinhaPagina.User = Retorno.FirstOrDefault();
                App.Current.MainPage = new NavigationPage(MinhaPagina);
                MinhaPagina.LoadInf();
            }

        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
