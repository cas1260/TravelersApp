using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppIsphereTravelers.Models;
using AppIsphereTravelers.Classes;
using System.Threading;
using ZXing.Net.Mobile.Forms;
using Plugin.Media;

namespace AppIsphereTravelers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrincipalPage : ContentPage
    {
        public clsTranslator Tradutor{get;set;}
        public PrincipalPage()
        {
            InitializeComponent();

            

        }
        public UserLogin User = new UserLogin();
        public Color CorSuperior
        {
            get
            {
                return Color.FromRgb(35, 52, 71);
            }
        }
        public Color CorInferior
        {
            get
            {
                return Color.FromRgb(27, 45, 64);
            }
        }

   
        public void LoadInf()
        {
            //http://www.ispheretravelers.com/my-account/photo/
            foto.Source = "https://www.ispheretravelers.com/my-account/" + User.photo_profile;
            nome.Text = User.name_profile;

            Tradutor = new clsTranslator(User.IdiomaPadrao);
            // Perfil
            lblPerfil.Text = Tradutor.Get("lblPerfil");
            TapGestureRecognizer cmdPerfil = new TapGestureRecognizer();
            cmdPerfil.Tapped += async (s, e) => {
                await ClickMeuPerfil1(s, e); 
            };

            lblPerfil.GestureRecognizers.Add(cmdPerfil);

            //Produtos
            lblProdutos.Text = Tradutor.Get("lblProdutos");
            TapGestureRecognizer cmdProduto = new TapGestureRecognizer();
            cmdProduto.Tapped += async (s, e) => {
                await ClickMeusProdutos(s, e);
            };

            lblProdutos.GestureRecognizers.Add(cmdProduto);


            //Alerta de Medicamento
            lblAlertaMedicado.Text = Tradutor.Get("lblAlertaMedicado");
            TapGestureRecognizer cmdAlertaMedicado = new TapGestureRecognizer();
            cmdAlertaMedicado.Tapped += async (s, e) => {
                await ClickAlertaMerdicamentos1(s, e);
            };

            lblAlertaMedicado.GestureRecognizers.Add(cmdAlertaMedicado);

            //Rede Social
            MinhaRede.Text = Tradutor.Get("MinhaRede");
            TapGestureRecognizer cmdNetWork = new TapGestureRecognizer();
            cmdNetWork.Tapped += async (s, e) => {
                await ClickMinhaRedeSocial1(s, e);
            };

            MinhaRede.GestureRecognizers.Add(cmdNetWork);

            //Bk de viagem
            BkpViagem.Text = Tradutor.Get("BkpViagem");
            TapGestureRecognizer cmdBkViagem = new TapGestureRecognizer();
            cmdBkViagem.Tapped += async (s, e) => {
                await ClickBackViagem1(s, e);
            };

            BkpViagem.GestureRecognizers.Add(cmdBkViagem);

            //Sair
            lblSair.Text = Tradutor.Get("lblSair");
            TapGestureRecognizer cmdSair = new TapGestureRecognizer();
            cmdSair.Tapped += async (s, e) => {
                await ClickSair1(s, e);
            };
            lblSair.GestureRecognizers.Add(cmdSair);

            TapGestureRecognizer cmdClickQrCode1 = new TapGestureRecognizer();
            cmdClickQrCode1.Tapped += async (s, e) => {
                await ClickQrCode1(s, e);
            };
            lblSair.GestureRecognizers.Add(cmdClickQrCode1);
            

            //nome1.Text = User.email_profile + " / " + User.birthday_profile;
        }
        private async System.Threading.Tasks.Task TapGestureRecognizer_TappedAsync(object sender, System.EventArgs e)
        {
            //PrincipalPageViewModel Model = (PrincipalPageViewModel)this.BindingContext;
            //await Model.ChamaMeuPerfil();

            //var Perfil = new MeuPerfilPage();
            //await Navigation.PushAsync(Perfil);


        }
        //private async System.Threading.Tasks.Task TapGestureRecognizer_MeusProdutosAsync(object sender, System.EventArgs e)
        //{
        //    var Prod = new MeusProdutosPage();
        //    await Navigation.PushAsync(Prod);
        //}
        public WebPage AbreBrowse(string url, string titulo)
        {
            WebPage Page = new WebPage();
            Page.Tradutor = Tradutor;
            Page.Title = titulo;
            Page.SetUrlBrowse(url);
            return Page;
            // await Prism.Navigation.PushAsync(Page);
            //http://www.ispheretravelers.com/my-account/profile.php
        }

        public string CodHash(double numero)
        {
            numero = (numero * 1.1279023198) * 2.271238103;
            return numero.ToString().Replace(".", "").Replace(",", "").Trim().Substring(0, 14);
        }


        private async Task ClickMeuPerfil1(object sender, System.EventArgs e)
        {

            //WebPage Page = new WebPage();
            //Page.RetornaUrl = $"http://www.ispheretravelers.com/my-account/profile.php?iduser={User.Id}&hash={CodHash(}";
            //Page.Title = "Meu Perfil";
            //Page.SetUrlBrowse($"https://www.ispheretravelers.com/my-account/profile.php?iduser={User.Id}");
            //await Navigation.PushAsync(Page);
            //User.Id = 25;
            var hash = CodHash(User.Id);
            await Navigation.PushAsync(AbreBrowse($"https://www.ispheretravelers.com/my-account/auth.php?iduser={User.Id}&hash_auth={CodHash(User.Id)}&ir=profile.php&lang=" + Tradutor.IdiomaPadrao, Tradutor.Get("lblPerfil")));

        }

        private async Task ClickMeusProdutos(object sender, System.EventArgs e)
        {
            var Page = new MeusProdutosPage();
            Page.User = User;
            Page.Title = Tradutor.Get("lblProdutos"); //"Meus Produtos";
            await Navigation.PushAsync(Page);
            await Page.LoadDados();

        }

        private async Task ClickAlertaMerdicamentos1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(AbreBrowse($"http://www.ispheretravelers.com/my-account/auth.php?iduser={User.Id}&hash_auth={CodHash(User.Id)}&ir=edit.php&sessao=med&lang=" + Tradutor.IdiomaPadrao, Tradutor.Get("lblAlertaMedicado"))); // "Alerta de Medicamentos"));// ;;
        }

        private async Task ClickMinhaRedeSocial1(object sender, System.EventArgs e)
        {
            var Pg = new networkpage();
            Pg.Title = Tradutor.Get("MinhaRede"); // "Rede Social";
            Pg.User = User;
            var LinkUrl = $"http://www.ispheretravelers.com/privatenetwork/developers?email={User.email_profile}&username=User-{User.Id}&name={User.name_profile}&hash_auth={CodHash(User.Id)}&lang=" + Tradutor.IdiomaPadrao;
            //Pg.Load($"http://www.ispheretravelers.com/my-account/auth.php?iduser={User}&hash_auth={CodHash(User.Id)}&email={User.email_profile}");
            Pg.Load(LinkUrl);
            await Navigation.PushAsync(Pg);


        }

        private async Task ClickBackViagem1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(AbreBrowse($"http://www.ispheretravelers.com/my-account/backup.php?iduser={User.Id}&lang=" + Tradutor.IdiomaPadrao, Tradutor.Get("BkpViagem")));// "Backup de Viagem"));
        }

        private async Task ClickQrCode1(object sender, System.EventArgs e)
        {
            //await Navigation.PushAsync(AbreBrowse($"http://www.ispheretravelers.com/my-account/?iduser={User.Id}&lang=" + Tradutor.IdiomaPadrao, "QrCode"));
            await AbreQRAsync();
        }


        public async Task AbreQRAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Erro", Tradutor.Get("ErroCamera"), "OK");
                return;
            }


            var scanPage = new ZXingScannerPage();
            
            //scanPage = new ZXingScannerPage(customOverlay: customOverlay);
            scanPage.Title = Tradutor.Get("LeitorQR");
            //scanPage.AutoFocus();

            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;
                scanPage.AutoFocus(0, 500);

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();

                    string Url = $"&lang=" + Tradutor.IdiomaPadrao;

                    if (result.Text.ToLower().IndexOf("ispheretravelers.com") == -1)
                    {
                        await DisplayAlert(Tradutor.Get("TituloAtecao"), Tradutor.Get("Qr"), "OK");
                    }
                    else
                    {
                        Url = result.Text.ToLower() + Url;
                        await Navigation.PushAsync(AbreBrowse(Url, Tradutor.Get("LeitorQR")));
                    }

                    //
                });
            };

            // Navigate to our scanner page
            await Navigation.PushAsync (scanPage);
        }



        private async Task ClickMrz1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(AbreBrowse($"http://www.ispheretravelers.com/my-account/?iduser={User.Id}&lang=" + Tradutor.IdiomaPadrao, "Mrz"));
        }

        private async Task ClickScanner1(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(AbreBrowse($"http://www.ispheretravelers.com/my-account/?iduser={User.Id}&lang=" + Tradutor.IdiomaPadrao, "Scanner"));
        }

        private async Task ClickSair1(object sender, System.EventArgs e)
        {
            var msg = await DisplayAlert(Tradutor.Get("TituloAtecao"), Tradutor.Get("ConfirmSair"), Tradutor.Get("sim"), Tradutor.Get("nao"));
            if (msg == true)
            {
                var Bd = new clsBd();
                Bd.AbreBanco();
                Bd.Cn.DropTable<UserLogin>();
                Bd.Cn.CreateTable<UserLogin>();
                Bd = null;
                /*try
                {

                    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    Thread.CurrentThread.Abort();
                }
                catch (Exception)
                {
                    Thread.CurrentThread.Abort();
                }


                await Navigation.PopAsync();
                await Navigation.PopToRootAsync();
                System.Environment.Exit(0);

                */
                var login = new LoginPage();
                App.Current.MainPage = new NavigationPage(login);
            }
        }

        private void ClickMeuPerfil(object sender, System.EventArgs e)
        {
            var hash = CodHash(User.Id);
            _ = Navigation.PushAsync(AbreBrowse($"https://www.ispheretravelers.com/my-account/auth.php?iduser={User.Id}&hash_auth={CodHash(User.Id)}&ir=profile.php&lang=" + Tradutor.IdiomaPadrao, Tradutor.Get("lblPerfil")));// "Meu Perfil"));

            //AsyncCallback callBack = new AsyncCallbackClickMeuPerfil1);
            //async delegate
            // {

            //  await (sender, e);
            //};

        }

        private void ClickMeusProdutos1(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await ClickMeusProdutos(sender, e);
            });
            x.BeginInvoke(null, null);
        }

        private void ClickAlertaMerdicamentos(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await ClickAlertaMerdicamentos1(sender, e);
            });
            x.BeginInvoke(null, null);
        }

        private void ClickMinhaRedeSocial(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await ClickMinhaRedeSocial1(sender, e);
            });
            x.BeginInvoke(null, null);
        }

        private void ClickBackViagem(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await ClickBackViagem1(sender, e);
            });
            x.BeginInvoke(null, null);
        }

        private void ClickQrCode(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await ClickQrCode1(sender, e);
            });
            x.BeginInvoke(null, null);
        }

        private void ClickMrz(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await ClickMrz1(sender, e);
            });
            x.BeginInvoke(null, null);
        }

        private void ClickScanner(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await ClickScanner1(sender, e);
            });
            x.BeginInvoke(null, null);
        }

        private void ClickSair(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await ClickSair1(sender, e);
            });
            x.BeginInvoke(null, null);

        }

    }
}