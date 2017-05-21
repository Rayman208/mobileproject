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
    //варианты периодов таймера
    enum WhatsTime { ApproachTime, SmallRelaxTime, BigRelaxTime, Nothing };

    static class TimeCounter
    {
        public static TimeValues ApproachTime { set; get; }//время подхода
        public static TimeValues SmallRelaxTime { set; get; }//малый отдых
        public static TimeValues BigRelaxTime { set; get; }//большой отдых
        public static int CountRound { set; get; }//кол-во кругов
        public static WhatsTime TimeSing { set; get; }//какой сейчас период таймера
    }
}