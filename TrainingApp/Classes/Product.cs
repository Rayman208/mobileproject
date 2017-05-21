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
            return String.Format("Название={0}\nБелки={1}\nЖиры={2}\nУглеводы={3}\nКаллории={4}",Title, Proteins, Fats, Carbohydrates, Callas);
        }
    }
}