using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace Lab11
{
    [Activity(Label = "Lab11", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Complex Data;
        ComplexValidation DataV;
        int Counter = 0;
        protected async override void OnCreate(Bundle bundle)
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnCreate");
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.StartActivity).Click += (sender, e) =>
            {
                var ActivityIntent = new Android.Content.Intent(this, typeof(SecondActivity));
                StartActivity(ActivityIntent);
            };

            // Utilizar FragmentManager para recuperar el Fragmento
            Data = (Complex)this.FragmentManager.FindFragmentByTag("Data");
            if (Data == null)
            {
                // No ha sido almacenado, agregar el fragmento a la Activity
                Data = new Complex();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(Data, "Data");
                FragmentTransaction.Commit();
            }

            // Utilizar FragmentManager para recuperar el Fragmento
            var ValidateText = FindViewById<TextView>(Resource.Id.ValidateText);
            DataV = (ComplexValidation)this.FragmentManager.FindFragmentByTag("DataV");
            if (DataV == null)
            {
                // No ha sido almacenado, agregar el fragmento a la Activity
                DataV = new ComplexValidation();
                var FragmentTransaction = this.FragmentManager.BeginTransaction();
                FragmentTransaction.Add(DataV, "DataV");
                FragmentTransaction.Commit();
                await Validate();
            }
            ValidateText.Text = $"{DataV.ToString()}";

            if (bundle != null)
            {
                Counter = bundle.GetInt("CounterValue", 0);
                Android.Util.Log.Debug("Lab11Log", "Activity A - Recovered Instance State");
            }

            var ClickCounter =
                FindViewById<Button>(Resource.Id.ClicksCounter);
            ClickCounter.Text =
                Resources.GetString(Resource.String.ClicksCounter_Text, Counter);

            ClickCounter.Text += $"\n{Data.ToString()}";

            ClickCounter.Click += (sender, e) =>
            {
                Counter++;
                ClickCounter.Text =
                    Resources.GetString(Resource.String.ClicksCounter_Text, Counter);

                // Modificar con cualquier valor solo para verificar la persistencia
                Data.Real++;
                Data.Imaginary++;
                // Mostrar el valor de los miembros
                ClickCounter.Text += $"\n{Data.ToString()}";

            };

        }

        protected override void OnStart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStart");
            base.OnStart();
        }

        protected override void OnResume()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnResume");
            base.OnResume();
        }

        protected override void OnPause()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnPause");
            base.OnPause();
        }

        protected override void OnStop()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnStop");
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnDestroy");
            base.OnDestroy();
        }

        protected override void OnRestart()
        {
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnRestart");
            base.OnRestart();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("CounterValue", Counter);
            Android.Util.Log.Debug("Lab11Log", "Activity A - OnSaveInstanceState");
            base.OnSaveInstanceState(outState);
        }

        private async System.Threading.Tasks.Task Validate()
        {
            SALLab11.ServiceClient ServiceClient = new SALLab11.ServiceClient();

            string StudentEmail = "jarc_software@hotmail.com";
            string Password = "Jrosales23";

            string myDevice = Android.Provider.Settings.Secure.GetString(
                ContentResolver, Android.Provider.Settings.Secure.AndroidId);

            DataV.Result = await ServiceClient.ValidateAsync(
                StudentEmail, Password, myDevice);
            
        }
    }
}

