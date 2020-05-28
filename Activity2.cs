using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace SimpleMapDemo
{
    [Activity(Label = "History")]



    public class Info : AppCompatActivity
    {
        Button next;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout2);
            next = (Button)FindViewById<Button>(Resource.Id.button1);
            next.Click += (s, e) => {
                StartActivity(typeof(MainActivity));
                Finish();
            };

               
        }
    }
}