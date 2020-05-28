using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Java.Util;
using Newtonsoft.Json;
using Uri = Android.Net.Uri;

namespace SimpleMapDemo
{
    using AndroidUri = Uri;

   //   [Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme")]
    [Activity(Label = "@string/activity_label_mapwithmarkers")]
    public class MainActivity : AppCompatActivity
    {
        public static readonly int RC_INSTALL_GOOGLE_PLAY_SERVICES = 1000;
        public static readonly string TAG = "XamarinMapDemo";










        // This is a list of the examples that will be display in the Main Activity.

        bool isGooglePlayServicesInstalled;
        
        ListView listView;
        string[] items;
        ListView mainList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


                items = new string[] {
                 "London",
                 "New York",
                
                 
             };
                 // Set our view from the "main" layout resource  
                 SetContentView(Resource.Layout.MainActivity);
                 mainList = (ListView)FindViewById<ListView>(Resource.Id.listView);
                 mainList.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, items);

                 mainList.ItemClick += (s, e) => {
                     var t = items[e.Position];
                    // Android.Widget.Toast.MakeText(this, (e.Position+1).ToString(), Android.Widget.ToastLength.Short).Show();
                     Intent intent = new Intent(this, typeof(MapWithMarkersActivity));
                     //   intent.PutExtra("User", JsonConvert.SerializeObject(list));
                     intent.PutExtra("locationCode", e.Position+1);
                     //intent.PutExtra("cordinate",)
                     this.StartActivity(intent);

                 };
                 
            isGooglePlayServicesInstalled = TestIfGooglePlayServicesIsInstalled();

          
        }

       

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (RC_INSTALL_GOOGLE_PLAY_SERVICES == requestCode && resultCode == Result.Ok)
            {
                isGooglePlayServicesInstalled = true;
            }
            else
            {
                Log.Warn(TAG, $"Don't know how to handle resultCode {resultCode} for request {requestCode}.");
            }
        }


        protected override void OnResume()
        {
            base.OnResume();
           // listView.ItemClick += SampleSelected;
        }

        void SampleSelected(object sender, AdapterView.ItemClickEventArgs e)
        {
            var position = e.Position;
            if (position == 0)
            {
                var geoUri = AndroidUri.Parse("geo:42.374260,-71.120824");
                var mapIntent = new Intent(Intent.ActionView, geoUri);
                StartActivity(mapIntent);
                return;
            }
            Dictionary<double, double> list = new Dictionary<double, double>
            {
                { 51.507351, -0.127758}
            };

            Intent intent = new Intent(this, typeof(MapWithMarkersActivity));
        //   intent.PutExtra("User", JsonConvert.SerializeObject(list));
            intent.PutExtra("locationCode", 1);
            //intent.PutExtra("cordinate",)
            this.StartActivity(intent);
           

            

           // var sampleToStart = SampleMetaDataList[position];
           //sampleToStart.Start(this);
        }

        protected override void OnPause()
        {
            //listView.ItemClick -= SampleSelected;
            base.OnPause();
        }


       

        bool TestIfGooglePlayServicesIsInstalled()
        {
            var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info(TAG, "Google Play Services is installed on this device.");
                return true;
            }

            if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error(TAG, "There is a problem with Google Play Services on this device: {0} - {1}", queryResult, errorString);
                var errorDialog = GoogleApiAvailability.Instance.GetErrorDialog(this, queryResult, RC_INSTALL_GOOGLE_PLAY_SERVICES);
                var dialogFrag = new ErrorDialogFragment(errorDialog);

                dialogFrag.Show(FragmentManager, "GooglePlayServicesDialog");
            }

            return false;
        }
    }
}
