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
    [Activity(Label = "AddProductActivity")]
    public class NewProductActivity : Activity
    {
        EditText et_title, et_proteins, et_fats, et_carbohydrates, et_calls;
        Button btn_add, btn_edit, btn_delete, btn_back;

        DBTableProducts tableProducts;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewProductLayout);

            et_title = FindViewById<EditText>(Resource.Id.editTextNewProductTitle);
            et_proteins = FindViewById<EditText>(Resource.Id.editTextNewProductProteins);
            et_fats = FindViewById<EditText>(Resource.Id.editTextNewProductFats);
            et_carbohydrates = FindViewById<EditText>(Resource.Id.editTextNewProductCarbohydrates);
            et_calls = FindViewById<EditText>(Resource.Id.editTextNewProductCalls);

            tableProducts = new DBTableProducts();
            tableProducts.CreateTable();

            btn_back = FindViewById<Button>(Resource.Id.buttonNewProductBack);
            btn_back.Click += Btn_back_Click;

            btn_add = FindViewById<Button>(Resource.Id.buttonNewProductAdd);
            btn_add.Click += Btn_add_Click;

            btn_delete = FindViewById<Button>(Resource.Id.buttonNewProductDelete);
            btn_delete.Click += Btn_delete_Click;

            btn_edit = FindViewById<Button>(Resource.Id.buttonNewProductEdit);
            btn_edit.Click += Btn_edit_Click;
        }

        private void Btn_delete_Click(object sender, EventArgs e)
        {
            if (Global.ChooseProduct == null)
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
                return;
            }
            bool result = tableProducts.DeleteEntity(Global.ChooseProduct);

            if (result == true)
            {
                Toast.MakeText(this, "Успешно удалено. Вернитесь назад", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
            }
        }

        private void Btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                Product editProduct = new Product()
                {
                    Id = Global.ChooseProduct.Id,
                    Title = et_title.Text,
                    Proteins = double.Parse(et_proteins.Text.Replace('.', ',')),
                    Fats = double.Parse(et_fats.Text.Replace('.', ',')),
                    Carbohydrates = double.Parse(et_carbohydrates.Text.Replace('.', ',')),
                    Callas = double.Parse(et_calls.Text.Replace('.', ','))
                };
                bool result = tableProducts.UpdateEntity(editProduct);

                et_title.Text = String.Empty;
                et_proteins.Text = String.Empty;
                et_fats.Text = String.Empty;
                et_carbohydrates.Text = String.Empty;
                et_calls.Text = String.Empty;

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

        private void Btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                Product insertProduct = new Product()
                {
                    Id = 0,
                    Title = et_title.Text,
                    Proteins = double.Parse(et_proteins.Text.Replace('.', ',')),
                    Fats = double.Parse(et_fats.Text.Replace('.', ',')),
                    Carbohydrates = double.Parse(et_carbohydrates.Text.Replace('.', ',')),
                    Callas = double.Parse(et_calls.Text.Replace('.', ','))
                };
                bool result = tableProducts.InsertIntoTable(insertProduct);

                et_title.Text = String.Empty;
                et_proteins.Text = String.Empty;
                et_fats.Text = String.Empty;
                et_carbohydrates.Text = String.Empty;
                et_calls.Text = String.Empty;
                
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

        private void Btn_back_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}