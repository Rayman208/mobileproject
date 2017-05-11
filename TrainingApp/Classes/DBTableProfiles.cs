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
    class DBTableProfiles : DBHelper
    {
        public override bool CreateTable()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    connection.CreateTable<Profile>();
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
                    Profile profile = (Profile)entity;
                    connection.Delete(profile);
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
                    connection.Insert((Profile)entity);
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
                    List<Profile> profiles = connection.Table<Profile>().ToList();

                    foreach (Profile profile in profiles)
                    {
                        string currentProfile = String.Format("{0}", profile.Title);

                        stringList.Add(currentProfile);
                    }
                    return stringList;
                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public Profile GetProfileByIndex(int index)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(Path.Combine(folder, dbName)))
                {
                    return connection.Table<Profile>().ToList()[index];
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
                    Profile profile = (Profile)entity;
                    connection.Query<Profile>("UPDATE Profile set Title=?,Height=?,Weight=?,Age=?,Sex=?,Purpose=?,CountTrainings=? Where Id=?", profile.Title, profile.Height, profile.Weight, profile.Age, profile.Sex, profile.Purpose, profile.CountTrainings, profile.Id);
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