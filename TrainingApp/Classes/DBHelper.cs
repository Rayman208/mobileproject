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
        protected string folder;//папка для хранения бд
        protected string dbName;//имя бд
        protected SQLiteConnection connection;//соединение с бд

        protected DBHelper()
        {
            connection = null;
            dbName = "training_app.db";
            folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);//получение системного пути для хранения бд
           // Console.WriteLine("DBfolder = {0}",folder);
        }
        //функции переопределны в других классах DBTableProfiles & DBTableProducts
        abstract public bool CreateTable();
        abstract public bool InsertIntoTable(object entity);
        abstract public List<string> SelectAllInTable();
        abstract public bool UpdateEntity(object entity);
        abstract public bool DeleteEntity(object entity);
    }
}