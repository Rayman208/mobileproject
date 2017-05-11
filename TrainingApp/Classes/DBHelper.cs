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
    abstract class DBHelper
    {
        protected string folder;
        protected string dbName;
        protected SQLiteConnection connection;

        protected DBHelper()
        {
            connection = null;
            dbName = "training_app.db";
            folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        }

        abstract public bool CreateTable();
        abstract public bool InsertIntoTable(object entity);
        abstract public List<string> SelectAllInTable();
        abstract public bool UpdateEntity(object entity);
        abstract public bool DeleteEntity(object entity);
    }
}