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
    [Activity(Label = "TrainingApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Button btn_newProfile, btn_startWork;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.MainLayout);

            btn_newProfile = FindViewById<Button>(Resource.Id.buttonNewProfile);
            btn_startWork = FindViewById<Button>(Resource.Id.buttonStartWork);
            btn_startWork.Click += Btn_startWork_Click;
        }

        private void Btn_startWork_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SelectorActivity));
            StartActivity(intent);
        }
    }
}

