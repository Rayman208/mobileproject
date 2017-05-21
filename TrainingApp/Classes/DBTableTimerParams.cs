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
    class DBTableTimerParams:DBHelper
    {
        public override bool CreateTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    connection.CreateTable<TimerParams>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }
        public override bool DeleteEntity(object entity)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    TimerParams timerParams = (TimerParams)entity;
                    connection.Delete(timerParams);
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
                    connection.Insert((TimerParams)entity);
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
                    List<TimerParams> listTimerParams = connection.Table<TimerParams>().ToList();

                    foreach (TimerParams timerParams in listTimerParams)
                    {
                        string currentTimerParams = String.Format("{0}", timerParams.NameParams);

                        stringList.Add(currentTimerParams);
                    }
                    return stringList;
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public TimerParams GetTimerParamsByIndex(int index)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    return connection.Table<TimerParams>().ToList()[index];
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public override bool UpdateEntity(object entity)
        {
            return false;
        }
    }
}