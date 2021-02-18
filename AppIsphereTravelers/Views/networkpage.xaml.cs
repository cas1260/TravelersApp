using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppIsphereTravelers.Models;

namespace AppIsphereTravelers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class networkpage : ContentPage
    {
        public UserLogin User { get; set; }

        public networkpage()
        {
            InitializeComponent();
        }

        public void Load(string Url)
        {
            browse.Source = Url;
        }
        private async Task Addnew(object sender, EventArgs e)
        {
            var Pg = new AddNewPublishPage();
            Pg.Title = "Adicionar novo";
            Pg.User = User;
            await Navigation.PushAsync(Pg);

            await Pg.Load();
        }


        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Action x = (async () =>
            {
                await Addnew(sender, e);
            });
            x.BeginInvoke(null, null);
        }
    }
}