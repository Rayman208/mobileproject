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
        Button btn_back, btn_rationNewProduct, btn_selectedProduct;
        ListView lv_productsInRation;

        Spinner sr_selectProduct;
        EditText et_productWeight;

        TextView tv_proteinsIs, tv_fatsIs, tv_carbohydratesIs, tv_callsIs,
                 tv_proteinsNeed, tv_fatsNeed, tv_carbohydratesNeed, tv_callsNeed;

        DBTableProducts tableProducts;
        int selectedIndex;
        int deleteIndex;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RationLayout);

            btn_selectedProduct = FindViewById<Button>(Resource.Id.buttonChooseSelectedProduct);
            btn_selectedProduct.Click += Btn_selectedProduct_Click;

            et_productWeight = FindViewById<EditText>(Resource.Id.editTextChooseProductWeight);

            sr_selectProduct = FindViewById<Spinner>(Resource.Id.spinnerChooseSelectProduct);
            sr_selectProduct.ItemSelected += Sr_selectProduct_ItemSelected;

            tableProducts = new DBTableProducts();
            tableProducts.CreateTable();
            LoadProducts();

            selectedIndex = -1;
        
            btn_rationNewProduct = FindViewById<Button>(Resource.Id.buttonRationNewProduct);
            btn_rationNewProduct.Click += Btn_rationNewProduct_Click;

            btn_back = FindViewById<Button>(Resource.Id.buttonRationBack);
            btn_back.Click += Btn_back_Click;

            lv_productsInRation = FindViewById<ListView>(Resource.Id.listViewRationProductsInRation);
            

            tv_proteinsIs = FindViewById<TextView>(Resource.Id.textViewRationProteinsIs);
            tv_fatsIs = FindViewById<TextView>(Resource.Id.textViewRationFatsIs);
            tv_carbohydratesIs = FindViewById<TextView>(Resource.Id.textViewRationCarbohydratesIs);
            tv_callsIs = FindViewById<TextView>(Resource.Id.textViewRationCallsIs);

            
            tv_proteinsNeed = FindViewById<TextView>(Resource.Id.textViewRationProteinsNeed);
            tv_fatsNeed = FindViewById<TextView>(Resource.Id.textViewRationFatsNeed);
            tv_carbohydratesNeed = FindViewById<TextView>(Resource.Id.textViewRationCarbohydratesNeed);
            tv_callsNeed = FindViewById<TextView>(Resource.Id.textViewRationCallsNeed);

            CalculateNeedFoodParameters();
         }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);
            if (v.Id == Resource.Id.listViewRationProductsInRation)
            {
                menu.Add(Menu.None, 0, 0, "Удалить запись");
            }
            if (v.Id == Resource.Id.listViewRationProductsInRation)
            {
                var info = (AdapterView.AdapterContextMenuInfo)menuInfo;
                deleteIndex = info.Position;
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            Global.ProductsInRation.RemoveAt(deleteIndex);
            LoadProductsInRation();
            CalculateIsFoodParameters();

            return true;
        }


        private void Sr_selectProduct_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            selectedIndex = e.Position;
            Global.ChooseProduct = tableProducts.GetProductByIndex(e.Position);
        }
        private void Btn_selectedProduct_Click(object sender, EventArgs e)
        {
            try
            {
                Global.ProductsInRation.Add(new ProductInRation()
                {
                    Product = tableProducts.GetProductByIndex(selectedIndex),
                    Weight = double.Parse(et_productWeight.Text)
                });

                selectedIndex = -1;
                et_productWeight.Text = String.Empty;

                LoadProductsInRation();
                CalculateIsFoodParameters();

                Toast.MakeText(this, "Успешно добавлено.", ToastLength.Short).Show();
            }
            catch
            {
                Toast.MakeText(this, "Неудача. Попробуйте снова", ToastLength.Short).Show();
            }
        }

        private void LoadProducts()
        {
            List<string> products = tableProducts.SelectAllInTable();

            ArrayAdapter<string> productsAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, products);
            productsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            sr_selectProduct.Adapter = productsAdapter;
        }
        
        private void Btn_rationNewProduct_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(NewProductActivity));
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            LoadProducts();
        }

        private void Btn_back_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void LoadProductsInRation()
        {            
            ArrayAdapter<string> lv_PiDAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, PirListToStringList());

            lv_productsInRation.Adapter = lv_PiDAdapter;

            RegisterForContextMenu(lv_productsInRation);
        }

        private List<string> PirListToStringList()
        {
            List<string> pir = new List<string>();
            foreach (ProductInRation item in Global.ProductsInRation)
            {
                pir.Add(String.Format("{0}\nВес={1}", item.Product.ToString(), item.Weight));
            }
            return pir;
        }

        private void CalculateIsFoodParameters()
        {
            double totalProteins = 0;
            double totalFats = 0;
            double totalCarbohydrates = 0;
            double totalCallas = 0;

            foreach (ProductInRation item in Global.ProductsInRation)
            {
                totalProteins += item.Product.Proteins / 100.0 * item.Weight;
                totalFats += item.Product.Fats / 100.0 * item.Weight;
                totalCarbohydrates += item.Product.Carbohydrates / 100.0 * item.Weight;

                totalCallas += item.Product.Callas / 100.0 * item.Weight;
            }
            tv_proteinsIs.Text = totalProteins.ToString();
            tv_fatsIs.Text = totalFats.ToString();
            tv_carbohydratesIs.Text = totalCarbohydrates.ToString();
            tv_callsIs.Text = totalCallas.ToString();
        }
        private void CalculateNeedFoodParameters()
        {
            double calls = 0, proteins = 0, fats = 0, carbohydrates=0;
            double koeff = Global.ChooseProfile.Purpose == 0 ? 1.1 : 1.9;

            if (Global.ChooseProfile.Sex == 1)
            {
                calls = 66 + (13.7 * Global.ChooseProfile.Weight) + (5 * Global.ChooseProfile.Height) - (6.76 * Global.ChooseProfile.Age) * koeff;
            }
            else
            {
                calls = 65.5 + (9.6 * Global.ChooseProfile.Weight) + (1.8 * Global.ChooseProfile.Height) - (4.7 * Global.ChooseProfile.Age) * koeff;
            }

            if (Global.ChooseProfile.Purpose == 1)
            {
                proteins = calls * 0.25;
                carbohydrates = calls * 0.6;
                fats = calls * 0.15;
            }
            else
            {
                proteins = calls * 0.45;
                carbohydrates = calls * 0.4;
                fats = calls * 0.15;
            }

            tv_proteinsNeed.Text = proteins.ToString();
            tv_fatsNeed.Text = fats.ToString();
            tv_carbohydratesNeed.Text = carbohydrates.ToString();
            tv_callsNeed.Text = calls.ToString();
        }
    }
}