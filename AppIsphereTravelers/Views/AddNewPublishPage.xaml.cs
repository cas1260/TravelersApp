using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppIsphereTravelers.Models;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using AppIsphereTravelers.Classes;
using System.Net.Http;
using System.IO;

namespace AppIsphereTravelers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewPublishPage : ContentPage
    {
        public UserLogin User { get; set; }
        public AddNewPublishPage()
        {
            InitializeComponent();
        }
        public void IsLoadForm(bool valor, string LabelPadrao = "Carregandos dados...")
        {
            ContainerDados.IsVisible = !valor;
            ContainerLoad.IsVisible = valor;
            NameLoad.Text = LabelPadrao;
        }


        public async Task Load()
        {
            IsLoadForm(true, "Aguarde, carregado dados");

            var sql = $@"SELECT * FROM `tbl_emergency` WHERE  `idprofile_bw`in (select id_profile from  tbl_profile_bw where idvendas in (select id from tbl_vendas where profile_id =  {User.Id}))";
            var Down = new clsDownload(null);

            var Dados = await Down.DoSql<List<tbl_emergency>>(sql);
            if (Dados == null)
            {
                LoadObj.IsVisible = false;
                NameLoad.Text = "Não ha nenhum anjo cadastrado!";
                return;
            }
            var Qtd = 0;

            StackLayout StackLayoutPai = new StackLayout();


            foreach (var item in Dados)
            {

                if (Qtd == 0)
                {
                    StackLayoutPai = new StackLayout();
                    StackLayoutPai.Orientation = StackOrientation.Horizontal;
                    StackLayoutPai.HorizontalOptions = LayoutOptions.StartAndExpand;
                    StackLayoutPai.VerticalOptions = LayoutOptions.StartAndExpand;
                }

                StackLayoutPai.Children.Add(new Checkbox
                {
                    Anjos = item,
                    idRegistro = item.id_emergency,
                    Text = item.name_emergency,
                    Checked = true
                });

                if (Qtd == 2)
                {
                    Qtd = -1;
                    ViagenComAnjo.Children.Add(StackLayoutPai);
                }
                Qtd++;

            }

            if (Qtd != 0)
            {
                ViagenComAnjo.Children.Add(StackLayoutPai);
            }
            IsLoadForm(false, "Aguarde, carregado dados");

        }


        private async void NovaFoto(object sender, EventArgs e)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
                    {
                        await DisplayAlert("Sem Permissão", "Sem acesso a camera", "OK");
                        return;
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
                    status = results[Permission.Camera];
                }

                if (status == PermissionStatus.Granted)
                {
                    //var results = await Crosscam .Current.GetPositionAsync(10000);
                    // LabelGeolocation.Text = "Lat: " + results.Latitude + " Long: " + results.Longitude;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Sem Permissão", "Sem acesso a camera", "OK");
                    return;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Error: " + ex, "OK");
                return;
            }



            var action = await DisplayActionSheet("Escolha origem da foto", "Cancel", null, "Tirar Foto", "Foto na galeria", "Gravar Video", "Video na galeria");

            var File = "";
            if (action == "Tirar Foto") File = await TirarFoto(sender, e);
            if (action == "Foto na galeria") File = await EscolherFoto(sender, e);
            if (action == "Gravar Video") File = await GravarVideo(sender, e);
            if (action == "Video na galeria") File = await EscolherVideo(sender, e);

            if (File != "")
            {
                Image img = new Image
                {
                    Source = File,
                    HeightRequest = 120,
                    WidthRequest = 120
                };


                Imagens.Children.Add(img);
            }

        }

        private async Task<string> TirarFoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();


            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");

                return "";
            }

       

            var file = await CrossMedia.Current.TakePhotoAsync(
            new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "IsphereImg"
            });

            if (file == null)
                return "";

            /*MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });*/

            return file.Path;
        }

        private async Task<string> EscolherFoto(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Ops", "Galeria de fotos não suportada.", "OK");

                return "";
            }

            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return "";

            return file.Path;

            /*MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });*/
        }

        private async Task<string> GravarVideo(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakeVideoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("Ops", "Nenhuma câmera detectada.", "OK");

                return "";
            }

            var file = await CrossMedia.Current.TakeVideoAsync(
                new StoreVideoOptions
                {
                    SaveToAlbum = true,
                    Directory = "IsphereImg",
                    Quality = VideoQuality.Medium
                });

            if (file == null)
                return "";


            return file.Path;
            /*MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });*/
        }

        private async Task<string> EscolherVideo(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                await DisplayAlert("Ops", "Galeria de videos não suportada.", "OK");

                return "";
            }

            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
                return "";

            return file.Path;

            /*
            MinhaImagem.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;

            });*/
        }

        public void SendPhotoServer(ImageSource caminho)
        {
            var file = caminho.GetValue(FileImageSource.FileProperty);
            var uri = caminho.GetValue(UriImageSource.UriProperty);

            //DisplayAlert("",file, "");

            var url = "http://hallpassapi.jamsocialapps.com/api/profile/UpdateProfilePicture/";


            //  var upfilebytes = File.ReadAllBytes(file);

            //    HttpClient client = new HttpClient();
            //    MultipartFormDataContent content = new MultipartFormDataContent();
            //    ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
            //    StringContent studentIdContent = new StringContent("2123");
            //    content.Add(baContent, "File", "filename.ext");
            //    content.Add(studentIdContent, "StudentId");


            //    //upload MultipartFormDataContent content async and store response in response var
            //    var response =
            //        await client.PostAsync(url, content);

            //    //read response result as a string async into json var
            //    var responsestr = response.Content.ReadAsStringAsync().Result;

            //    //debug
            //    Debug.WriteLine(responsestr);


            //    //read file into upfilebytes array
            //    var upfilebytes = File.ReadAllBytes(file);

            //    //create new HttpClient and MultipartFormDataContent and add our file, and StudentId
            //    HttpClient client = new HttpClient();
            //    MultipartFormDataContent content = new MultipartFormDataContent();
            //    ByteArrayContent baContent = new ByteArrayContent(upfilebytes);
            //    StringContent studentIdContent = new StringContent("2123");
            //    content.Add(baContent, "File", "filename.ext");
            //    content.Add(studentIdContent, "StudentId");


            //    //upload MultipartFormDataContent content async and store response in response var
            //    var response =
            //        await client.PostAsync(url, content);

            //    //read response result as a string async into json var
            //    var responsestr = response.Content.ReadAsStringAsync().Result;

            //    //debug
            //    Debug.WriteLine(responsestr);

            //}
            //    catch (Exception e)
            //    {
            //        //debug
            //        Debug.WriteLine("Exception Caught: " + e.ToString());

            //        return;
            //    }
        }


        public void UploadPhoto()
        {

            IsLoadForm(true, "Aguarde, fazendo upload das imagens");

            foreach (var item in Imagens.Children)
            {
                Image i = (Image)item;
                SendPhotoServer(i.Source);

            }


        }

        public long SalvePublicacao()
        {

            return 0;
        }
        private void Save(object sender, EventArgs e)
        {
            if (Viagem.Text == null || Viagem.Text == "")
            {
                DisplayAlert("Atenção", "É necessario escolher um destino!", "OK");
                return;
            }

            if (ViagenComAnjo.Children.Count == 0)
            {
                DisplayAlert("Atenção", "É necessario escolher um anjo para compartilhar sua viagem!", "OK");
                return;

            }
            var escolheu = false;
            List<tbl_emergency> AnjosSelect = new List<tbl_emergency>();
            foreach (var item1 in ViagenComAnjo.Children)
            {
                var i = (StackLayout)item1;
                foreach (var item2 in i.Children)
                {
                    var chk = (Checkbox)item2;
                    if (chk.Checked == true)
                    {
                        escolheu = true;
                        AnjosSelect.Add(chk.Anjos);
                    }
                }

            }

            if (escolheu == false)
            {
                DisplayAlert("Atenção", "É necessario escolher um anjo para compartilhar sua viagem!", "OK");
                return;
            }

            if (data.Date == null)
            {
                DisplayAlert("Atenção", "É necessario escolher uma data!", "OK");
                return;
            }

            
            UploadPhoto();
        }
    }
}