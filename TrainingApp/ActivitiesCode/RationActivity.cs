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
    [Activity(Label = "RationActivity")]
    public class RationActivity : Activity
    {
        Button btn_back, btn_rationAddProduct, btn_rationNewProduct;

        ListView lv_PiR;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RationLayout);

            btn_rationAddProduct = FindViewById<Button>(Resource.Id.buttonRationAddProduct);
            btn_rationAddProduct.Click += Btn_rationAddProduct_Click;

            btn_rationNewProduct = FindViewById<Button>(Resource.Id.buttonRationNewProduct);
            btn_rationNewProduct.Click += Btn_rationNewProduct_Click;

            btn_back = FindViewById<Button>(Resource.Id.buttonRationBack);
            btn_back.Click += Btn_back_Click;

            lv_PiR = FindViewById<ListView>(Resource.Id.listViewRationProductsInRation);

            LoadPiR();
        }

        private void Btn_rationNewProduct_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewProductActivity));
            StartActivity(intent);
        }

        private void Btn_rationAddProduct_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(ChooseProductActivity));
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            LoadPiR();
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void LoadPiR()
        {
            if (Global.PiR == null) { return; }

            ArrayAdapter<string> lv_PiDAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, Global.PiR.ListToStringList());

            lv_PiR.Adapter = lv_PiDAdapter;
        }
    }
}