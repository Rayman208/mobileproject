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

namespace TrainingApp
{
    static class Global
    {
        public static Profile ChooseProfile { set; get; }
        public static ProductsInRation PiR { set; get; }
    }
}