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
    class Product
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
        public double Callas { get; set; }

        public override string ToString()
        {
            return String.Format("Id={0}\n��������={1}\n�����={2}\n����={3}\n��������={4}\n��������={5}", Id, Title, Proteins, Fats, Carbohydrates, Callas);
        }
    }
}