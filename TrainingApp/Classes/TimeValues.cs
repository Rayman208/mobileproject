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
    //класс для единицы времени 
    class TimeValues
    {
        private int m_totalsec;//текщуие секунды
        private int m_saveTotalsec;//для восстановления времени
        
        //функция установки времени
        public void SetTime(int hour = 0, int min = 0, int sec = 0)
        {
            //всё превращаем в секунды
            m_totalsec = sec + min * 60 + hour * 3600;
            m_saveTotalsec = m_totalsec;
        }
        //восстановить время
        public void ReturnTime()
        {
            m_totalsec = m_saveTotalsec;
        }

        public void Tick()
        {
            if (m_totalsec > 0)
            {
                m_totalsec--;
            }
        }
        public int GetSec()
        {
            return m_totalsec % 60;
        }
        public int GetMin()
        {
            return (m_totalsec / 60) % 60;
        }
        public int GetHour()
        {
            return m_totalsec / 3600;
        }
        public int GetTotalsec()
        {
            return m_totalsec;
        }

        public string GetTimeString()
        {
            return String.Format("{0:00}:{1:00}:{2:00}",GetHour(),GetMin(),GetSec());
        }
    }
}