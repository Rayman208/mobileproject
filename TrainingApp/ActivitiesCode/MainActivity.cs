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
        Button btn_newProfile, btn_startWork, btn_Exit;
        Spinner sr_profiles;

        DBTableProfiles tableProfiles;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.MainLayout);

            btn_newProfile = FindViewById<Button>(Resource.Id.buttonNewProfile);
            btn_newProfile.Click += Btn_newProfile_Click;

            btn_startWork = FindViewById<Button>(Resource.Id.buttonStartWork);
            btn_startWork.Click += Btn_startWork_Click;

            btn_Exit = FindViewById<Button>(Resource.Id.buttonExit);
            btn_Exit.Click += Btn_Exit_Click;

            sr_profiles = FindViewById<Spinner>(Resource.Id.spinnerProfiles);
            sr_profiles.ItemSelected += Sr_profiles_ItemSelected;


            tableProfiles = new DBTableProfiles();
            tableProfiles.CreateTable();

            LoadProfiles();
        }

        private void Sr_profiles_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Global.ChooseProfile = tableProfiles.GetProfileByIndex(e.Position);

            Toast.MakeText(this, Global.ChooseProfile.ToString(), ToastLength.Long).Show();
        }

        private void LoadProfiles()
        {
            List<string> profiles = tableProfiles.SelectAllInTable();

            ArrayAdapter<string> profilesAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, profiles);
            profilesAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            sr_profiles.Adapter = profilesAdapter;

        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            Process.KillProcess(Process.MyPid());
        }

        private void Btn_newProfile_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewProfileActivity));
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            LoadProfiles();
        }

        private void Btn_startWork_Click(object sender, EventArgs e)
        {
            //if (Global.ChooseProfile==null)
            //{
            //    Toast.MakeText(this, "Вначале, выберите или создайте профиль", ToastLength.Long).Show();
            //    return;
            //}

            Intent intent = new Intent(this, typeof(SelectorActivity));
            StartActivity(intent);
        }
    }
}

