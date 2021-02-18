using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.Forms;
using AppIsphereTravelers.Models;
using AppIsphereTravelers.Classes;
using Plugin.Media;

namespace AppIsphereTravelers.Views
{
    public partial class MainPage : ContentPage
    {
        public UserLogin DadosLogin;
        public clsTranslator Tradutor{get;set;}

        public MainPage()
		{
            CrossMedia.Current.Initialize();
            InitializeComponent();
        }

        public void Load()
        {
            Tradutor = new clsTranslator(DadosLogin.IdiomaPadrao);

            LabelWELCOME1.Text = Tradutor.Get("LabelWELCOME1");
            LabelWELCOME2.Text = Tradutor.Get("LabelWELCOME2");
            LabelWELCOME3.Text = Tradutor.Get("LabelWELCOME3");
            btnHome.Text = Tradutor.Get("btnHome");
        }

        private void ClikcPageInicial(object sender, EventArgs e)
        {
            var Page = new PrincipalPage();
            Page.User = DadosLogin;

            App.Current.MainPage = new NavigationPage(Page);
            Page.LoadInf();
        }

        private async Task ClickAtiveProduto1(object sender, EventArgs e)
        {

            //var scanner = DependencyService.Get<IQrCodeScanningService>();
            //var result = await scanner.ScanAsync();// .ScanAsync();
            //if (!string.IsNullOrEmpty(result))
           // {
                // Sua logica.
           //     var QrCode = result;
           // }
        }

        private void ClickAtiveProduto(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await ClickAtiveProduto1(sender, e);
            });
            x.BeginInvoke(null, null);
        }
    }
}
