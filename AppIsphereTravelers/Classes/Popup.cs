//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Text;
//using Xamarin.Forms;

//namespace AppIsphereTravelers.Classes
//{
//    class Popup : AbsoluteLayout
//    {
//        public Popup(View content, double popupWidth, double popupHeight)
//        {

//            SetLayoutFlags(this, AbsoluteLayoutFlags.All);

//            var shaded = new AbsoluteLayout { Opacity = 0.5, BackgroundColor = Color.Black };
//            var tapShaded = new TapGestureRecognizer();
//            tapShaded.Tapped += OnShadedTapped;
//            shaded.GestureRecognizers.Add(tapShaded);

//            var sv = new ScrollView { Content = content };

//            this.Children.Add(shaded, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
//            this.Children.Add(sv, new Rectangle(0.5, 0.5, popupWidth, popupHeight), AbsoluteLayoutFlags.PositionProportional);
//            this.PropertyChanged += OnPropChanged;

//        }

//        public void OnPropChanged(object sender, PropertyChangedEventArgs e)
//        {
//            if (e.PropertyName == "ShowPopup")
//            {
//                if (ShowPopup)
//                    Show();
//                else
//                    Hide();
//            }
//        }

//        public void Show()
//        {
//            SetLayoutBounds(this, new Rectangle(0, 0, 1, 1));
//        }

//        public void Hide()
//        {
//            SetLayoutBounds(this, new Rectangle(0, 0, 0, 0));
//        }



//        public static BindableProperty ShowPopupProperty =
//            BindableProperty.Create<Popup, bool>(p => p.ShowPopup, default(bool), BindingMode.TwoWay);

//        public bool ShowPopup
//        {
//            get { return (bool)GetValue(ShowPopupProperty); }
//            set { SetValue(ShowPopupProperty, value); }
//        }


//        /*
//        public static BindableProperty ShadedTappedCommandProperty = 
//            BindableProperty.Create<Popup, object> (p => p.OnShadedTappedCommand, default(object));

//        public ICommand OnShadedTappedCommand
//        {
//            get { return (ICommand) GetValue(ShadedTappedCommandProperty); }
//            set { SetValue(ShadedTappedCommandProperty, value); }
//        }*/

//        public void OnShadedTapped(object sender, EventArgs e)
//        {
//            SetValue(ShowPopupProperty, false);
//            //Hide ();

//            /*
//            if (OnShadedTappedCommand != null) {
//                OnShadedTappedCommand.Execute (e);
//            }*/
//        }
//    }
//}
