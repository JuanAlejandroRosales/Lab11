using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lab11
{
    public class ComplexValidation : Fragment
    {
        public SALLab11.ResultInfo Result { get; set; }

        public override string ToString()
        {
            return $"{Result.Status}\n{Result.Fullname}\n{Result.Token}";
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RetainInstance = true;
        }
    }

}