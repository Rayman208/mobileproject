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
    [Activity(Label = "SelectorActivity"/*, MainLauncher = true*/)]
    public class SelectorActivity : Activity
    {
        Button btn_openRation, btn_openTrainingDiary, btn_openTimer, btn_selectorBack;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectorLayout);

            btn_openRation = FindViewById<Button>(Resource.Id.buttonOpenRation);
            btn_openRation.Click += Btn_openRation_Click;

            btn_openTrainingDiary = FindViewById<Button>(Resource.Id.buttonOpenTrainingDiary);

            btn_openTimer = FindViewById<Button>(Resource.Id.buttonOpenTimer);
            btn_openTimer.Click += Btn_openTimer_Click;

            btn_selectorBack = FindViewById<Button>(Resource.Id.buttonSelectorBack);
            btn_selectorBack.Click += Btn_selectorBack_Click;
        }

        private void Btn_openRation_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RationActivity));
            StartActivity(intent);
        }

        private void Btn_selectorBack_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Btn_openTimer_Click(object sender, EventArgs e)
        {
            TimeCounter.TimeSing = WhatsTime.Nothing;

            Intent intent = new Intent(this, typeof(TimerWorkActivity));
            StartActivity(intent);
        }
    }
}