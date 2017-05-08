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

        Button btn_timerOptions, btn_timerStartStop;
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

            btn_timerOptions = FindViewById<Button>(Resource.Id.buttonTimerOptions);
            btn_timerOptions.Click += Btn_timerOptions_Click;

            btn_timerStartStop = FindViewById<Button>(Resource.Id.buttonTimerStartStop);
            btn_timerStartStop.Click += Btn_timerStartStop_Click;
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
                            tv_whatTime.Text = "ÏÅÐÈÎÄ ÏÎÄÕÎÄÀ";
                            tv_round.Text = "Îñòàëîñü êðóãîâ: "+TimeCounter.CountRound.ToString();
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
                            tv_whatTime.Text = "ÏÅÐÈÎÄ ÌÀËÎÃÎ ÎÒÄÛÕÀ";
                            tv_round.Text = "Îñòàëîñü êðóãîâ: " + TimeCounter.CountRound.ToString();
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
                            tv_whatTime.Text = "ÏÅÐÈÎÄ ÁÎËÜØÎÃÎ ÎÒÄÛÕÀ";
                            tv_round.Text = "ÊÐÓÃÈ ÎÊÎÍ×ÅÍÛ";
                        });
                    }
                    else
                    {
                        timer.Stop();
                        RunOnUiThread(() =>
                        {
                            Toast.MakeText(this, "ÇÀÊÎÍ×ÅÍ ÑÅÒ", ToastLength.Long).Show();

                            tv_time.Text = "Âðåìÿ";
                            tv_whatTime.Text = "ÏÅÐÈÎÄ";
                            tv_round.Text = "Îñòàëîñü êðóãîâ: ";
                        });
                    }
                    break;
            }
        }

        private void Btn_timerStartStop_Click(object sender, EventArgs e)
        {
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
            Intent intent = new Intent(this, typeof(TimerOptionsActivity));
            StartActivity(intent);
        }
    }
}