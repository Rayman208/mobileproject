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
using System.Timers;

namespace TrainingApp
{
    [Activity(Label = "TimerWorkActivity")]
    public class TimerWorkActivity : Activity
    {
        bool isWorking;
        Timer timer;

        Button btn_options, btn_startStop, btn_back, btn_reset;
        TextView tv_whatTime, tv_time, tv_round;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.TimerWorkLayout);

            isWorking = false;
        
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            
            tv_round = FindViewById<TextView>(Resource.Id.textViewRound);
            tv_time = FindViewById<TextView>(Resource.Id.textViewTime);
            tv_whatTime = FindViewById<TextView>(Resource.Id.textViewWhatTime);

            btn_options = FindViewById<Button>(Resource.Id.buttonTimerOptions);
            btn_options.Click += Btn_timerOptions_Click;

            btn_startStop = FindViewById<Button>(Resource.Id.buttonTimerStartStop);
            btn_startStop.Click += Btn_timerStartStop_Click;

            btn_back = FindViewById<Button>(Resource.Id.buttonTimerBack);
            btn_back.Click += Btn_back_Click;

            btn_reset = FindViewById<Button>(Resource.Id.buttonTimerReset);
            btn_reset.Click += Btn_reset_Click;
        }

        private void Btn_reset_Click(object sender, EventArgs e)
        {
            TimeCounter.TimeSing = WhatsTime.Nothing;
            tv_time.Text = "Время";
            tv_whatTime.Text = "Период";
            tv_round.Text = "Осталось кругов";

            timer.Stop();
            isWorking = false;
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            switch (TimeCounter.TimeSing)
            {
                case WhatsTime.ApproachTime:
                    if (TimeCounter.ApproachTime.GetTotalsec() > 0)
                    {
                        TimeCounter.ApproachTime.Tick();
                        
                        RunOnUiThread(() =>
                        {
                            tv_time.Text = TimeCounter.ApproachTime.GetTimeString();
                            tv_whatTime.Text = "ПЕРИОД ПОДХОДА";
                            tv_round.Text = "Осталось кругов: "+TimeCounter.CountRound.ToString();
                        });
                    }
                    else
                    {
                        TimeCounter.TimeSing = WhatsTime.SmallRelaxTime;
                    }
                    break;
                case WhatsTime.SmallRelaxTime:
                    if (TimeCounter.SmallRelaxTime.GetTotalsec() > 0)
                    {
                        TimeCounter.SmallRelaxTime.Tick();

                        RunOnUiThread(() =>
                        {
                            tv_time.Text = TimeCounter.SmallRelaxTime.GetTimeString();
                            tv_whatTime.Text = "ПЕРИОД МАЛОГО ОТДЫХА";
                            tv_round.Text = "Осталось кругов: " + TimeCounter.CountRound.ToString();
                        });
                    }
                    else
                    {
                        TimeCounter.CountRound--;
                        if (TimeCounter.CountRound == 0)
                        {
                            TimeCounter.TimeSing = WhatsTime.BigRelaxTime;
                        }
                        else
                        {
                            TimeCounter.TimeSing = WhatsTime.ApproachTime;

                            TimeCounter.ApproachTime.ReturnTime();
                            TimeCounter.SmallRelaxTime.ReturnTime();
                        }
                    }
                    break;
                case WhatsTime.BigRelaxTime:
                    if (TimeCounter.BigRelaxTime.GetTotalsec() > 0)
                    {
                        TimeCounter.BigRelaxTime.Tick();

                        RunOnUiThread(() =>
                        {
                            tv_time.Text = TimeCounter.BigRelaxTime.GetTimeString();
                            tv_whatTime.Text = "ПЕРИОД БОЛЬШОГО ОТДЫХА";
                            tv_round.Text = "КРУГИ ОКОНЧЕНЫ";
                        });
                    }
                    else
                    {
                        timer.Stop();
                        isWorking = false;
                        TimeCounter.TimeSing = WhatsTime.Nothing;
                        RunOnUiThread(() =>
                        {
                            Toast.MakeText(this, "ЗАКОНЧЕН СЕТ", ToastLength.Long).Show();

                            tv_time.Text = "Время";
                            tv_whatTime.Text = "Период";
                            tv_round.Text = "Осталось кругов";
                        });
                    }
                    break;
            }
        }

        private void Btn_timerStartStop_Click(object sender, EventArgs e)
        {
            if (TimeCounter.TimeSing == WhatsTime.Nothing)
            {
                Toast.MakeText(this, "Вначале, настройте таймер", ToastLength.Long).Show();
                return;
            }

            if (isWorking == false)
            {
                isWorking = true;
                timer.Start();
            }
            else
            {
                isWorking = false;
                timer.Stop();
            }
        }

        private void Btn_timerOptions_Click(object sender, EventArgs e)
        {
            //намерение 
            Intent intent = new Intent(this, typeof(TimerOptionsActivity));
            //this - кто является родителем вызываемого активити
            //typeof(TimerOptionsActivity) - какое активити мы вызываем

            StartActivity(intent);//запуск намерения
        }
    }
}