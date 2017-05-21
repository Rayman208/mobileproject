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
    //глобавльный класс чтобы хранить данные между активити
    static class Global
    {
        public static Profile ChooseProfile { set; get; }
        public static Product ChooseProduct { set; get; }
        public static TimerParams ChooseTimerParams { set; get; }
        public static List<ProductInRation> ProductsInRation { set; get; }
    }
}