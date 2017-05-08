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
        Button btn_openRation, btn_openTrainingDiary, btn_openTimer;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SelectorLayout);

            btn_openRation = FindViewById<Button>(Resource.Id.buttonOpenRation);

            btn_openTrainingDiary = FindViewById<Button>(Resource.Id.buttonOpenTrainingDiary);

            btn_openTimer = FindViewById<Button>(Resource.Id.buttonOpenTimer);
            btn_openTimer.Click += Btn_openTimer_Click;
        }

        private void Btn_openTimer_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(TimerWorkActivity));
            StartActivity(intent);
        }
    }
}