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
        Button btn_set, btn_back, btn_add, btn_delete, btn_clear;

        EditText et_approachHour, et_approachMin, et_approachSec;
        EditText et_smalRelaxHour, et_smalRelaxMin, et_smalRelaxSec;
        EditText et_bigRelaxHour, et_bigRelaxMin, et_bigRelaxSec;
        EditText et_countRound;

        EditText et_nameParams;

        Spinner sr_TimerParams;
        DBTableTimerParams tableTimerParams;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TimerOptionsLayout);

            btn_set = FindViewById<Button>(Resource.Id.buttonSetTimeOptions);
            btn_set.Click += Btn_setTimerOptions_Click;

            btn_back = FindViewById<Button>(Resource.Id.buttonTimeOptionsBack);
            btn_back.Click += Btn_back_Click;

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

            et_nameParams = FindViewById<EditText>(Resource.Id.editTextTimeOptionsNameParams);
            
            TimeCounter.ApproachTime = new TimeValues();
            TimeCounter.SmallRelaxTime = new TimeValues();
            TimeCounter.BigRelaxTime = new TimeValues();

            sr_TimerParams = FindViewById<Spinner>(Resource.Id.spinnerTimeOptionsTimerParams);
            sr_TimerParams.ItemSelected += Sr_TimerParams_ItemSelected;    

            //экземпляр для работы с БД
            tableTimerParams = new DBTableTimerParams();
            tableTimerParams.CreateTable();

            LoadTimerParams();

            btn_add = FindViewById<Button>(Resource.Id.buttonTimeOptionsAdd);
            btn_add.Click += Btn_add_Click;

            btn_delete = FindViewById<Button>(Resource.Id.buttonTimeOptionsDelete);
            btn_delete.Click += Btn_delete_Click;

            btn_clear = FindViewById<Button>(Resource.Id.buttonTimeOptionsClear);
            btn_clear.Click += Btn_clear_Click;
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            if (Global.ChooseTimerParams == null)
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
                return;
            }
            bool result = tableTimerParams.DeleteEntity(Global.ChooseTimerParams);

            if (result == true)
            {
                Toast.MakeText(this, "Успешно удалено.", ToastLength.Short).Show();

                Global.ChooseTimerParams = null;

                LoadTimerParams();
            }
            else
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
            }
        }

        private void Btn_clear_Click(object sender, EventArgs e)
        {
            et_approachHour.Text = "0";
            et_approachMin.Text = "0";
            et_approachSec.Text = "0";
            et_smalRelaxHour.Text = "0";
            et_smalRelaxMin.Text = "0";
            et_smalRelaxSec.Text = "0";
            et_bigRelaxHour.Text = "0";
            et_bigRelaxMin.Text = "0";
            et_bigRelaxSec.Text = "0";
            et_countRound.Text = String.Empty;
        }

        private void Btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                TimerParams insertTimerParams = new TimerParams()
                {
                    Id = 0,
                    NameParams = et_nameParams.Text,
                    ApproachHour = int.Parse(et_approachHour.Text),
                    ApproachMin = int.Parse(et_approachMin.Text),
                    ApproachSec = int.Parse(et_approachSec.Text),

                    SmalRelaxHour = int.Parse(et_smalRelaxHour.Text),
                    SmalRelaxMin = int.Parse(et_smalRelaxMin.Text),
                    SmalRelaxSec = int.Parse(et_smalRelaxSec.Text),

                    BigRelaxHour = int.Parse(et_bigRelaxHour.Text),
                    BigRelaxMin = int.Parse(et_bigRelaxMin.Text),
                    BigRelaxSec = int.Parse(et_bigRelaxSec.Text),

                    CountRound = int.Parse(et_countRound.Text)
                };
                bool result = tableTimerParams.InsertIntoTable(insertTimerParams);

                if (result == true)
                {
                    Toast.MakeText(this, "Успешно добавлено.", ToastLength.Short).Show();

                    LoadTimerParams();

                    et_nameParams.Text = String.Empty;
                }
                else
                {
                    Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
                }
            }
            catch
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
            }
        }

        private void Sr_TimerParams_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Global.ChooseTimerParams = tableTimerParams.GetTimerParamsByIndex(e.Position);

            if (Global.ChooseTimerParams == null) { return; }

            et_approachHour.Text= Global.ChooseTimerParams.ApproachHour.ToString();
            et_approachMin.Text= Global.ChooseTimerParams.ApproachMin.ToString(); 
            et_approachSec.Text= Global.ChooseTimerParams.ApproachSec.ToString(); ;
            et_smalRelaxHour.Text= Global.ChooseTimerParams.SmalRelaxHour.ToString();
            et_smalRelaxMin.Text= Global.ChooseTimerParams.SmalRelaxMin.ToString(); 
            et_smalRelaxSec.Text= Global.ChooseTimerParams.SmalRelaxSec.ToString(); 
            et_bigRelaxHour.Text= Global.ChooseTimerParams.BigRelaxHour.ToString(); 
            et_bigRelaxMin.Text= Global.ChooseTimerParams.BigRelaxMin.ToString(); 
            et_bigRelaxSec.Text= Global.ChooseTimerParams.BigRelaxSec.ToString(); 
            et_countRound.Text= Global.ChooseTimerParams.CountRound.ToString(); 
        }

        //заполнить спиннер элементами
        private void LoadTimerParams()
        {
            List<string> listTimerParams = tableTimerParams.SelectAllInTable();

            //адаптер для заполнения спиннера элементами
            ArrayAdapter<string> timerParamsAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, listTimerParams);
            timerParamsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            sr_TimerParams.Adapter = timerParamsAdapter;
        }


        private void Btn_back_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Btn_setTimerOptions_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
            }
        }
    }
}