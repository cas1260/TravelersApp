using System;
using System.Collections.Generic;
using System.Text;
using AppIsphereTravelers.Views;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppIsphereTravelers.Classes
{
    class clsFuncao
    {
        public WebPage AbreBrowse(string url, string titulo)
        {
            WebPage Page = new WebPage();
            Page.Title = titulo;
            Page.SetUrlBrowse ( url);
            return Page;
           // await Prism.Navigation.PushAsync(Page);
            //http://www.ispheretravelers.com/my-account/profile.php
        }
    }
}
