using AppIsphereTravelers.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppIsphereTravelers.Classes
{
    public class Checkbox : Button
    {
        public Checkbox()
        {
            base.Image = "Assets/newcbu.png";
            base.Clicked += new EventHandler(OnClicked);
            base.BackgroundColor = Color.Transparent;
            base.BorderWidth = 0;
            base.HorizontalOptions = LayoutOptions.Start;
            base.VerticalOptions = LayoutOptions.Start;
            //base.Gravity = GravityFlags.Left;
        }

        public long idRegistro { get; set; }

        public tbl_emergency Anjos { get; set; }

        public static BindableProperty CheckedProperty = BindableProperty.Create(
            propertyName: "Checked",
            returnType: typeof(Boolean?),
            declaringType: typeof(Checkbox),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: CheckedValueChanged);

        public Boolean? Checked
        {
            get
            {
                if (GetValue(CheckedProperty) == null)
                {
                    return null;
                }
                return (Boolean)GetValue(CheckedProperty);
            }
            set
            {
                SetValue(CheckedProperty, value);
                OnPropertyChanged();
                RaiseCheckedChanged();
            }
        }

        private static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && (Boolean)newValue == true)
                ((Checkbox)bindable).Image = "checkedimage.png";
            else
                ((Checkbox)bindable).Image = "uckeck.png";
        }

        public event EventHandler CheckedChanged;
        private void RaiseCheckedChanged()
        {
            CheckedChanged?.Invoke(this, EventArgs.Empty);
        }


        public void OnClicked(object sender, EventArgs e)
        {
            Checked = !Checked;
        }

    }
}
