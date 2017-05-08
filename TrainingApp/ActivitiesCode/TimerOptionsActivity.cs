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
    [Activity(Label = "TimerOptionsActivity")]
    public class TimerOptionsActivity : Activity
    {
        Button btn_setTimerOptions;
        EditText et_approachHour, et_approachMin, et_approachSec;
        EditText et_smalRelaxHour, et_smalRelaxMin, et_smalRelaxSec;
        EditText et_bigRelaxHour, et_bigRelaxMin, et_bigRelaxSec;
        EditText et_countRound;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TimerOptionsLayout);

            btn_setTimerOptions = FindViewById<Button>(Resource.Id.buttonSetTimeOptions);

            btn_setTimerOptions.Click += Btn_setTimerOptions_Click;

            et_approachHour = FindViewById<EditText>(Resource.Id.editTextApproachHour);
            et_approachMin = FindViewById<EditText>(Resource.Id.editTextApproachMin);
            et_approachSec = FindViewById<EditText>(Resource.Id.editTextApproachSec);

            et_smalRelaxHour = FindViewById<EditText>(Resource.Id.editTextSmalRelaxHour);
            et_smalRelaxMin = FindViewById<EditText>(Resource.Id.editTextSmalRelaxMin);
            et_smalRelaxSec = FindViewById<EditText>(Resource.Id.editTextSmalRelaxSec);

            et_bigRelaxHour = FindViewById<EditText>(Resource.Id.editTextBigRelaxHour);
            et_bigRelaxMin = FindViewById<EditText>(Resource.Id.editTextBigRelaxMin);
            et_bigRelaxSec = FindViewById<EditText>(Resource.Id.editTextBigRelaxSec);

            et_countRound = FindViewById<EditText>(Resource.Id.editTextCountRound);

            TimeCounter.ApproachTime = new TimeValues();
            TimeCounter.SmallRelaxTime = new TimeValues();
            TimeCounter.BigRelaxTime = new TimeValues();
        }

        private void Btn_setTimerOptions_Click(object sender, EventArgs e)
        {
            TimeCounter.ApproachTime.SetTime
                (int.Parse(et_approachHour.Text),
                    int.Parse(et_approachMin.Text),
                    int.Parse(et_approachSec.Text));

            TimeCounter.SmallRelaxTime.SetTime
                (int.Parse(et_smalRelaxHour.Text),
                    int.Parse(et_smalRelaxMin.Text),
                    int.Parse(et_smalRelaxSec.Text));


            TimeCounter.BigRelaxTime.SetTime
                (int.Parse(et_bigRelaxHour.Text),
                    int.Parse(et_bigRelaxMin.Text),
                    int.Parse(et_bigRelaxSec.Text));

            TimeCounter.CountRound = int.Parse(et_countRound.Text);

            TimeCounter.TimeSing = WhatsTime.ApproachTime;

            Finish();
        }
    }
}