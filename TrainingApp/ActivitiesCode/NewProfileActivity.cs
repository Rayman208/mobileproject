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
    [Activity(Label = "NewProfileActivity")]
    public class NewProfileActivity : Activity
    {
        EditText et_title, et_height, et_weight, et_age, et_countTrainings;
        RadioButton rb_man, rb_woman, rb_loss, rb_gain;
        Button btn_add, btn_back, btn_edit, btn_delete;

        DBTableProfiles tableProfiles;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewProfileLayout);

            et_title = FindViewById<EditText>(Resource.Id.editTextPofileTitle);
            et_height = FindViewById<EditText>(Resource.Id.editTextPofileHeight);
            et_weight = FindViewById<EditText>(Resource.Id.editTextPofileWeight);
            et_age = FindViewById<EditText>(Resource.Id.editTextPofileAge);
            et_countTrainings = FindViewById<EditText>(Resource.Id.editTextPofileCountTraining);

            rb_man = FindViewById<RadioButton>(Resource.Id.radioButtonPofileMan);
            rb_woman = FindViewById<RadioButton>(Resource.Id.radioButtonPofileWoman);
            rb_loss = FindViewById<RadioButton>(Resource.Id.radioButtonPofileLoss);
            rb_gain = FindViewById<RadioButton>(Resource.Id.radioButtonPofileGain);

            btn_add = FindViewById<Button>(Resource.Id.buttonPofileAdd);
            btn_add.Click += Btn_Add_Click;

            btn_back = FindViewById<Button>(Resource.Id.buttonPofileBack);
            btn_back.Click += Btn_Back_Click;

            btn_edit = FindViewById<Button>(Resource.Id.buttonProfileEdit);
            btn_edit.Click += Btn_Edit_Click;

            btn_delete = FindViewById<Button>(Resource.Id.buttonProfileDelete);
            btn_delete.Click += Btn_delete_Click;

            tableProfiles = new DBTableProfiles();
            tableProfiles.CreateTable();
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            if (Global.ChooseProfile == null)
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
                return;
            }
            bool result = tableProfiles.DeleteEntity(Global.ChooseProfile);

            if (result == true)
            {
                Toast.MakeText(this, "Успешно удалено. Вернитесь назад", ToastLength.Short).Show();
                Global.ChooseProfile = null;
            }
            else
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
            }
        }

        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                Profile editProfile = new Profile()
                {
                    Id = Global.ChooseProfile.Id,
                    Title = et_title.Text,
                    Weight = double.Parse(et_weight.Text.Replace('.', ',')),
                    Height = double.Parse(et_height.Text.Replace('.', ',')),
                    Age = int.Parse(et_age.Text),
                    CountTrainings = int.Parse(et_countTrainings.Text),
                    Purpose = rb_loss.Checked == true ? 0 : 1,
                    Sex = rb_man.Checked == true ? 1 : 0
                };
                bool result = tableProfiles.UpdateEntity(editProfile);

                et_title.Text = String.Empty;
                et_height.Text = String.Empty;
                et_weight.Text = String.Empty;
                et_age.Text = String.Empty;
                et_countTrainings.Text = String.Empty;

                rb_man.Checked = true;
                rb_loss.Checked = true;

                if (result == true)
                {
                    Toast.MakeText(this, "Успешно отредактированно. Вернитесь назад", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
                }
            }
            catch
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
            }
        }

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                Profile insertProfile = new Profile()
                {
                    Id = 0,
                    Title = et_title.Text,
                    Weight = double.Parse(et_weight.Text.Replace('.', ',')),
                    Height = double.Parse(et_height.Text.Replace('.', ',')),
                    Age = int.Parse(et_age.Text),
                    CountTrainings = int.Parse(et_countTrainings.Text),
                    Purpose = rb_loss.Checked == true ? 0 : 1,
                    Sex = rb_man.Checked == true ? 1 : 0
                };
                bool result = tableProfiles.InsertIntoTable(insertProfile);

                et_title.Text = String.Empty;
                et_height.Text = String.Empty;
                et_weight.Text = String.Empty;
                et_age.Text = String.Empty;
                et_countTrainings.Text = String.Empty;

                rb_man.Checked = true;
                rb_loss.Checked = true;

                if (result == true)
                {
                    Toast.MakeText(this, "Успешно добавлено. Вернитесь назад", ToastLength.Short).Show();
                }
                else
                {
                    Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
                }
            }
            catch
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
            }
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}