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
    class ProductsInRation
    {
        public List<ProductInRation> List { set; get; }

        public List<string> ListToStringList()
        {
            List<string> stringList = new List<string>();

            foreach (ProductInRation product in List)
            {
                string currentProfile = String.Format("{0}\n{1}", product.ToString(),product.Weight);
                stringList.Add(currentProfile);
            }
            return stringList;
        }
    }
}