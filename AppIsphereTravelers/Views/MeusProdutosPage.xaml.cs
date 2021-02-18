using AppIsphereTravelers.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppIsphereTravelers.Classes;
using ZXing.Net.Mobile.Forms;
//using ZXing.Net.Mobile.Forms;

namespace AppIsphereTravelers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeusProdutosPage : ContentPage
    {
        public UserLogin User { get; set; }
        public clsTranslator Tradutor { get; set; }
        public MeusProdutosPage()
        {
            //BindingContext = this;
            InitializeComponent();
        }
        public string WordCount { get { return Tradutor.Get("contentPai"); } }
        public string left(string texto, int qtd)
        {
            if (qtd >= texto.Length)
            {
                return texto;
            }
            return texto.Substring(0, qtd);
        }
        public string CodHash(double numero)
        {
            numero = (numero * 1.1279023198) * 2.271238103;
            numero = Math.Round(numero, 14);
            return numero.ToString().Replace(".", "").Replace(",", "").Trim().Substring(0, 14);
        }

        public void LoadDadosNoWait()
        {
            Action Run = (async () =>
            {
                await LoadDados();
            });
            Run.BeginInvoke(null, null);
        }

        public string GetHashUser
        {
            get
            {
                return $"iduser={User.Id}&hash_auth={CodHash(Convert.ToDouble(User.Id))}";
            }
        }

        public async Task LoadDados()
        {
            //&lang=" + Tradutor.IdiomaPadrao
            Tradutor = new clsTranslator(User.IdiomaPadrao);
            LoadInfor.Text = Tradutor.Get("LoadInfor");
            Title = Tradutor.Get("contentPai");
            //var stackLayoutGeral = new StackLayout { IsVisible = true };
            try
            {

                me.IsVisible = false;
                FrameProdutos.IsVisible = false;
                FrameProdutos.Children.Clear();
                //FrameProdutos.Children.Add(stackLayoutGeral);

                LoadPageObjecto.IsVisible = true;

                var sql = $"select * from tbl_vendas where profile_id = {User.Id}";
                var Down = new clsDownload(null);
                var Dados = await Down.DoSql<List<tbl_vendas>>(sql);
                //var numprodutos = 0;

                FrameProdutos.Children.Add(new Label
                {
                    Text = Tradutor.Get("contentPai"),
                    FontSize = 18
                }) ;

                foreach (var item in Dados)
                {
                    var idvendas = item.id;

                    var LabelBotao = Tradutor.Get("Detalhes");
                    var LabelBotaoReport = Tradutor.Get("Relatorio");
                    var linkReport = $"report.php?idvendas={idvendas}&hash={CodHash(Convert.ToDouble(idvendas))}&lang=" + Tradutor.IdiomaPadrao;

                    if (item.ativo == "0")
                    {
                        LabelBotao = Tradutor.Get("Ativar");
                    }
                    var link = "";
                    var CodigoHash = CodHash(Convert.ToDouble(item.id));
                    var Img = "";
                    if (left(item.codigo, 2) == "BW")
                    {
                        link = $"auth.php?{GetHashUser}&ir=edit.php&idvendas={item.id}&hash={CodigoHash}&lang=" + Tradutor.IdiomaPadrao;
                        Img = "https://www.ispheretravelers.com/my-account/img/Pulseira.jpg";
                    }
                    if (left(item.codigo, 2) == "BP")
                    {
                        link = $"auth.php?{GetHashUser}&ir=pet.php&idvendas={item.id}&hash={CodigoHash}&lang=" + Tradutor.IdiomaPadrao;
                        Img = "https://www.ispheretravelers.com/my-account/img/pet.jpg";
                    }
                    if (left(item.codigo, 2) == "BH")
                    {
                        link = $"auth.php?{GetHashUser}&ir=tag.php&idvendas={item.id}&hash={CodigoHash}&lang=" + Tradutor.IdiomaPadrao;
                    }
                    if (left(item.codigo, 2) == "BT")
                    {
                        link = $"auth.php?{GetHashUser}&ir=tagv.php&idvendas={item.id}&&hash={CodigoHash}&lang=" + Tradutor.IdiomaPadrao;
                        Img = "https://www.ispheretravelers.com/my-account/img/BT.jpg";
                    }

                    var stackLayoutPai = new StackLayout { IsVisible = true };
                    stackLayoutPai.Orientation = StackOrientation.Vertical;

                    stackLayoutPai.Children.Add(new Image
                    {
                        //WidthRequest = 120,
                        //HeightRequest = 120,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        //
                        Source = $"{Img}"
                    });

                    var stackLayoutFilho1 = new StackLayout { IsVisible = true };
                    stackLayoutFilho1.Orientation = StackOrientation.Vertical;
                    stackLayoutFilho1.WidthRequest = 100;

                    stackLayoutFilho1.Children.Add(new Label
                    {
                        Text = item.nomeproduto,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Fill,
                        FontSize = 20
                    });
                    stackLayoutFilho1.Children.Add(new Label
                    {
                        Text = item.ativo == "0" ? Tradutor.Get("NaoAtivo") : item.nomealternativo,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Fill,
                    });
                    stackLayoutFilho1.Children.Add(new Label
                    {
                        Text = item.ativo == "0" ? "--------" : item.codigo,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.Fill,
                    });

                    stackLayoutPai.Children.Add(stackLayoutFilho1);

                    var stackLayoutFilho2 = new StackLayout { IsVisible = true };
                    stackLayoutFilho2.Orientation = StackOrientation.Horizontal;
                    stackLayoutFilho2.VerticalOptions = LayoutOptions.CenterAndExpand;
                    stackLayoutFilho2.HorizontalOptions = LayoutOptions.Fill;
                    stackLayoutFilho2.Margin = new Thickness(0, 0, 10, 10);

                    Button Detalhes = new Button { Text = LabelBotao, IsVisible = true };

                    if (item.ativo == "0")
                    {
                        Detalhes.Clicked += async (object sender, EventArgs e) =>
                        {
                            await Active_Clicked(Convert.ToInt32(idvendas));
                        };

                    }
                    else
                    {
                        Detalhes.Clicked += async (object sender, EventArgs e) =>
                        {
                            await Detalhes_Clicked(Convert.ToInt32(idvendas), $"https://www.ispheretravelers.com/my-account/{link}", item.nomeproduto);
                        };
                    }
                    stackLayoutFilho2.Children.Add(Detalhes);

                    if (item.ativo != "0")
                    {
                        var Relatorio = new Button { Text = LabelBotaoReport, IsVisible = true };
                        stackLayoutFilho2.Children.Add(Relatorio);

                        Relatorio.Clicked += async (object sender, EventArgs e) =>
                        {
                            
                            await Detalhes_Clicked(Convert.ToInt32(idvendas), $"https://www.ispheretravelers.com/my-account/{linkReport}", Tradutor.Get("Relatorio"));
                        };

                        stackLayoutPai.Children.Add(stackLayoutFilho2);
                    }
                    /*
                    var Card = new CardView.CardView
                    {
                        IsVisible=true, 
                        CardViewHasShadow = true,
                        CardViewInnerFrameOutlineColor = Color.FromRgb(230, 230, 230),
                        //CardViewHeightRequest = 10,
                        CardViewInnerFrameOutlineColorThickness = 1,
                        //HeightRequest = 170
                    };
                    Card.VerticalOptions = LayoutOptions.CenterAndExpand;
                    Card.HorizontalOptions = LayoutOptions.Center;
                    Card.CardViewContent = stackLayoutPai;
                    */

                    var f = new Frame();

                    f.IsClippedToBounds = true;
                    //f.HeightRequest = 120;
                    f.VerticalOptions = LayoutOptions.CenterAndExpand;
                    f.HorizontalOptions = LayoutOptions.Center;
                    f.Margin = new Thickness(5, 5, 5, 5);
                    f.CornerRadius = 15;
                    f.IsVisible = true;
                    f.Content = stackLayoutPai;
                    /**/

                    FrameProdutos.Children.Add(f);
                    //FrameProdutos
                }
                me.IsVisible = true;
                FrameProdutos.IsVisible = true;
                //FrameProdutos.Children.Add(stackLayoutGeral);
            }
            catch (Exception e)
            {
                await DisplayAlert("Erro", e.Message, "OK");
            }
            LoadPageObjecto.IsVisible = false;
        }

        private async Task Detalhes_Clicked(long IdVendas, string Url, string NomeProduto)
        {
            WebPage Page = new WebPage();
            Page.Tradutor = Tradutor;
            Page.Title = NomeProduto;
            Page.SetUrlBrowse(Url);
            await Navigation.PushAsync(Page);
        }
        private async Task Active_Clicked(long IdVendas)
        {


            /*var scanner = DependencyService.Get<IQrCodeScanningService>();
            var result = await scanner.ScanAsync();
            if (!string.IsNullOrEmpty(result))
            {
                // Sua logica.
                var QrCode = result;
            }*/

            //ZXing.


            var scanPage = new ZXingScannerPage();
            //scanPage = new ZXingScannerPage(customOverlay: customOverlay);
            scanPage.Title = "Leitor de QrCode";
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

                    string Url = $"&origem=app&idUsuario={User.Id}&lang=" + Tradutor.IdiomaPadrao;

                    if (result.Text.ToLower().IndexOf("ispheretravelers.com") == -1)
                    {
                        await DisplayAlert(Tradutor.Get("TituloAtecao"), Tradutor.Get("Qr"), "OK");
                    }
                    else
                    {
                        me.IsVisible = false;
                        FrameProdutos.IsVisible = false;
                        LoadPageObjecto.IsVisible = true;
                        Url = result.Text + Url;
                        Url = Url.Replace("http://", "https://");
                        clsDownload d = new clsDownload(null);
                        clsResuladoAtivacao Retorno = await d.DownloadPage<clsResuladoAtivacao>(Url, "", Tradutor.Get("Logar"));
                        if (Retorno.resposta == "ok")
                        {
                            await DisplayAlert(Tradutor.Get("Parabens"), Tradutor.Get("ProdutoAtivo"), "OK");
                            await LoadDados();
                        }
                    }

                    //
                });
            };

            // Navigate to our scanner page
            await Navigation.PushModalAsync(scanPage);

        }
    }
}