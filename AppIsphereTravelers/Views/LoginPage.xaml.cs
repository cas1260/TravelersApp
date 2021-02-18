using AppIsphereTravelers.Classes;
using AppIsphereTravelers.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace AppIsphereTravelers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public clsTranslator Traducao { get; set; } = new clsTranslator("EN");
        public string IdiomaPadrao { get; set; }
        public LoginPage()
        {
            InitializeComponent();

            cmdlogar.Clicked += async delegate
            {

                await cmdlogar_ClickedAsync1(null, null); ;

            };
            cmdNovo.Clicked += async delegate
            {
                await NovoCadAsync(null, null);
            };

            RefreshTela("EN");

        }

        public void RefreshTela(string idioma)
        {

            txtlogin.Placeholder = Traducao.Get("txtlogin", idioma);
            lblsenha.Text = Traducao.Get("lblsenha", idioma);
            txtSenha.Placeholder = Traducao.Get("txtSenha", idioma);
            cmdlogar.Text = Traducao.Get("cmdlogar", idioma);
            cmdNovo.Text = Traducao.Get("cmdNovo", idioma);
            lblAguarde.Text = Traducao.Get("lblAguarde", idioma);

            IdiomaPadrao = idioma;///
            Traducao.IdiomaPadrao = idioma;
        }

        public Color retornacor()
        {
            return Color.FromRgb(69, 82, 95);
        }

        private async Task cmdlogar_ClickedAsync1(object sender, System.EventArgs e)
        {
            clsDownload d = new clsDownload(Msg);

            if (txtlogin.Text == "")
            {
                await DisplayAlert(Traducao.Get("TituloAtecao"), Traducao.Get("ErroLogin"), "Ok");
                txtlogin.Focus();
                return;
            }
            if (txtSenha.Text == "")
            {
                await DisplayAlert(Traducao.Get("TituloAtecao"), Traducao.Get("ErroSenha"), "Ok");
                txtSenha.Focus();
                return;
            }

            divLoad.IsVisible = true;
            divDados.IsVisible = false;

            UserLogin Retorno = await d.Login(txtlogin.Text, txtSenha.Text);
            //await Navigation.PopModalAsync();
            Retorno.IdiomaPadrao = IdiomaPadrao;
            divLoad.IsVisible = false;
            divDados.IsVisible = true;

            if (Retorno.Sucesso == true)
            {
                // Vou precisar salvar os dados 
                var Bd = new clsBd();
                Bd.AbreBanco();
                Bd.Cn.DropTable<UserLogin>();
                Bd.Cn.CreateTable<UserLogin>();
                Bd.Cn.Insert(Retorno);

                var MinhaPagina = new MainPage();
                MinhaPagina.DadosLogin = Retorno;
                MinhaPagina.Load();
                App.Current.MainPage = new NavigationPage(MinhaPagina);

            }
            else
            {
                await DisplayAlert(Traducao.Get("TituloAtecao"), Traducao.Get("ErroAoLogin"), "ok");
                divLoad.IsVisible = false;
                divDados.IsVisible = true;
                txtSenha.Text = "";
                txtSenha.Focus();
            }

        }

        private async Task cmdlogar_ClickedAsync(object sender, System.EventArgs e)
        {
            await cmdlogar_ClickedAsync1(sender, e);
        }

        private void clickEmergencia(object sender, EventArgs e)
        {

        }

        private async Task NovoCad1(object sender, EventArgs e)
        {
            WebPage Page = new WebPage();
            Page.Tradutor = Traducao;
            Page.Title = Traducao.Get("Cadastra-se");
            Page.SetUrlBrowse("http://www.ispheretravelers.com/my-account/cadastro.php?Origem=app&lang=" + IdiomaPadrao);
            await Navigation.PushAsync(Page);
        }

        private async Task NovoCadAsync(object sender, EventArgs e)
        {
            WebPage Page = new WebPage();
            Page.Tradutor = Traducao;
            Page.Title = Traducao.Get("Cadastra-se");
            Page.SetUrlBrowse("http://www.ispheretravelers.com/my-account/cadastro.php?Origem=app&lang="+IdiomaPadrao);
            await Navigation.PushAsync(Page);
        }

        private void BtnEN_Clicked(object sender, EventArgs e)
        {
            RefreshTela("EN");
        }

        private void BtnPT_Clicked(object sender, EventArgs e)
        {
            RefreshTela("PT");
        }
    }
}