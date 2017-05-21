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
    class Profile
    {
        [PrimaryKey, AutoIncrement]//свойства элемента ИД
        public int Id { get; set; }//ид профиля
        public string Title { get; set; }//название профиля
        public double Height { get; set; }//рост
        public double Weight { get; set; }//вес
        public int Age { get; set; }//возраст
        public int Sex { get; set; }//пол
        public int Purpose { get; set; }//цель
        public int CountTrainings { get; set; }//кол-во тренировок

        //перевод в строку всех параметров
        public override string ToString()
        {
            return String.Format("Название={0}\nРост={1}\nВес={2}\nВозраст={3}\nПол={4}\nЦель={5}\nТренировок в неделю={6}",Title, Height, Weight, Age, Sex==1?"Мужской":"Женский", Purpose == 0 ? "Сушка" : "Набор веса", CountTrainings);
        }
    }
}