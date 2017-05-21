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

using SQLite;//подключение sql

namespace TrainingApp
{
    class TimerParams
    {
        [PrimaryKey, AutoIncrement]//свойства элемента ИД
        public int Id { set; get; }
        public string NameParams { set; get; }
        public int ApproachHour {set; get;}
        public int ApproachMin {set; get;}
        public int ApproachSec {set; get;}
        public int SmalRelaxHour {set; get;}
        public int SmalRelaxMin {set; get;}
        public int SmalRelaxSec {set; get;}
        public int BigRelaxHour {set; get;}
        public int BigRelaxMin {set; get;}
        public int BigRelaxSec {set; get;}
        public int CountRound {set; get;}
    }
}