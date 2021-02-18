using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using Plugin.Media;

namespace AppIsphereTravelers.Droid
{//, 
    [Activity(Label = "AppIsphereTravelers", Theme = "@style/MainTheme", Icon = "@mipmap/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    //public class MainActivity : XFormsApplicationDroid
    {
        //private void SetIoc()
        //{
        //    var resolverContainer = new SimpleContainer();

        //    var app = new XFormsAppDroid();

        //    app.Init(this);

        //    var documents = app.AppDataDirectory;
        //    var pathToDatabase = Path.Combine(documents, "xforms.db");

        //    resolverContainer.Register<IDevice>(t => AndroidDevice.CurrentDevice)
        //        .Register<IDisplay>(t => t.Resolve<IDevice>().Display)
        //        .Register<IFontManager>(t => new FontManager(t.Resolve<IDisplay>()))
        //        //.Register<IJsonSerializer, Services.Serialization.JsonNET.JsonSerializer>()
        //        .Register<IJsonSerializer, JsonSerializer>()
        //        .Register<IEmailService, EmailService>()
        //        .Register<IMediaPicker, MediaPicker>()
        //        .Register<ITextToSpeechService, TextToSpeechService>()
        //        .Register<IDependencyContainer>(resolverContainer)
        //        .Register<IXFormsApp>(app)
        //        .Register<ISecureStorage>(t => new KeyVaultStorage(t.Resolve<IDevice>().Id.ToCharArray()));
        //        //.Register<ICacheProvider>(
        //        //    t => new SQLiteSimpleCache(new SQLitePlatformAndroid(),
        //        //        new SQLiteConnectionString(pathToDatabase, true), t.Resolve<IJsonSerializer>()));


        //    Resolver.SetResolver(resolverContainer.GetResolver());
       /// }

        protected override async void OnCreate(Bundle bundle)
        {
            //TabLayoutResource = Resource.Layout.Tabbar;
            //ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            //MobileBarcodeScanner.Initialize(this.Application);

            await CrossMedia.Current.Initialize();

            //MobileBarcodeScanner.Initialize(this.Application);

            //Xamarin.Essentials.Platform.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            var app = new App();

            //MobileBarcodeScanner.Initialize(this.Application);
            LoadApplication(app);
        }
        //private Android.Views.View GetSnackbarAnchorView()
        //{
        //    var a = (Activity)Forms.Context;

        //    //			var v2 = a.FindViewById(Android.Resource.Id.Content);
        //    //			var v3 = v2.RootView;
        //    //			var v =  a.CurrentFocus;
        //    //			return v;

        //    var v3 = a.FindViewById(Android.Resource.Id.Content);
        //    //var v = a.FindViewById(Resource.Id.decor_content_parent);
        //    //var v2 = a.FindViewById(Resource.Id.concontent);
        //    return v3;
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {


        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        //{
        //    if (grantResults[0] == (int)Permission.Granted)
        //    {
        //        SetPermissions.OKResultHandler(requestCode);
        //    }
        //    else
        //    {
        //        SetPermissions.FailedResultHandler(requestCode);
        //    }
        //}

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions)
        //{
        //    base.OnRequestPermissionsResult(requestCode, permissions);
        //}

        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);
        //}


        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        //{
        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}
