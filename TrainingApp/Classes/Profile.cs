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

using SQLite;

namespace TrainingApp
{
    class Profile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int Purpose { get; set; }
        public int CountTrainings { get; set; }

        public override string ToString()
        {
            return String.Format("Id={0}\nНазвание={1}\nРост={2}\nВес={3}\nВозраст={4}\nПол={5}\nЦель={6}\nТренировок в неделю={7}", Id, Title, Height, Weight, Age, Sex==1?"Мужской":"Женский", Purpose == 0 ? "Сушка" : "Набор веса", CountTrainings);
        }
    }
}