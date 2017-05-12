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
    [Activity(Label = "ChooseProductActivity")]
    public class ChooseProductActivity : Activity
    {
        Button btn_selectedProduct, btn_back;
        Spinner sr_selectProduct;

        EditText et_productWeight;

        DBTableProducts tableProducts;

        int selectedIndex;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChooseProductLayout);

            btn_selectedProduct = FindViewById<Button>(Resource.Id.buttonChooseSelectedProduct);
            btn_selectedProduct.Click += Btn_selectedProduct_Click;

            et_productWeight = FindViewById<EditText>(Resource.Id.editTextChooseProductWeight);

            sr_selectProduct = FindViewById<Spinner>(Resource.Id.spinnerChooseSelectProduct);
            sr_selectProduct.ItemSelected += Sr_selectProduct_ItemSelected;

            tableProducts = new DBTableProducts();
            tableProducts.CreateTable();

            selectedIndex = -1;

            btn_back = FindViewById<Button>(Resource.Id.buttonChooseBack);
            btn_back.Click += Btn_back_Click;
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Sr_selectProduct_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            selectedIndex = e.Position;
        }

        private void Btn_selectedProduct_Click(object sender, EventArgs e)
        {
            if (selectedIndex == -1) { return; }

            Global.PiR.List.Add(new ProductInRation()
            {
                Product = tableProducts.GetProductByIndex(selectedIndex),
                Weight = double.Parse(et_productWeight.Text)
            });

            selectedIndex = -1;
            et_productWeight.Text = String.Empty;
        }

        private void LoadProducts()
        {
            List<string> products = tableProducts.SelectAllInTable();

            ArrayAdapter<string> productsAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, products);
            productsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            sr_selectProduct.Adapter = productsAdapter;
        }
    }
}