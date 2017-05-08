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
    enum WhatsTime { ApproachTime, SmallRelaxTime, BigRelaxTime, Nothing };

    static class TimeCounter
    {
        public static TimeValues ApproachTime { set; get; }
        public static TimeValues SmallRelaxTime { set; get; }
        public static TimeValues BigRelaxTime { set; get; }
        public static int CountRound { set; get; }
        public static WhatsTime TimeSing { set; get; }
    }
}