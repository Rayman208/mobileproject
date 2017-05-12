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
using System.IO;

namespace TrainingApp
{
    class DBTableProducts: DBHelper
    {
        public override bool CreateTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    connection.CreateTable<Product>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return true;
            }
        }
        public override bool DeleteEntity(object entity)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    Product product = (Product)entity;
                    connection.Delete(product);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public override bool InsertIntoTable(object entity)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    connection.Insert((Product)entity);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public override List<string> SelectAllInTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    List<string> stringList = new List<string>();
                    List<Product> products = connection.Table<Product>().ToList();

                    foreach (Product product in products)
                    {
                        string currentProduct = String.Format("{0}", product.Title);

                        stringList.Add(currentProduct);
                    }
                    return stringList;
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public Product GetProductByIndex(int index)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    return connection.Table<Product>().ToList()[index];
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public override bool UpdateEntity(object entity)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    Product product = (Product)entity;
                    connection.Query<Product>("UPDATE Product set Name=?,Proteins=?,Fats=?,Carbohydrates=?,Callas=? Where Id=?", product.Title, product.Proteins, product.Fats, product.Carbohydrates, product.Callas, product.Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }
    }
}