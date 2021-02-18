using AppIsphereTravelers.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppIsphereTravelers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebPage : ContentPage
    {
        public clsTranslator Tradutor { get; set; }
        public WebPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            browse.Navigating += Browse_Navigating;
            browse.Navigated += Browse_Navigated;
        }


        public void Load()
        {
            LblAguarde.Text = Tradutor.Get("LoadInfor");
        }

        private void Browse_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Result.ToString() == "Success")
            {
                divLoad.IsVisible = false;
                browse.IsVisible = true;
            }
        }

        private void Browse_Navigating(object sender, WebNavigatingEventArgs e)
        {
            //LblAguarde.Text = e.
            //e.Url
        }

        private string _RetornaUrl { get; set; }
        public string RetornaUrl
        {
            get
            {
                return _RetornaUrl;
            }
            set
            {
                _RetornaUrl = value;
            }
        }

        public void SetUrlBrowse(string url)
        {
            browse.Source = url;
            this.Load();
        }
    }
}