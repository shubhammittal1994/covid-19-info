using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace SimpleMapDemo
{
    [Activity(Label = "@string/activity_label_mapwithmarkers")]
    public class MapWithMarkersActivity : AppCompatActivity, IOnMapReadyCallback
    {



        List<LatLng> londonCleLatLng = new List<LatLng>() {
            new LatLng(51.511307, -0.150532),new LatLng(51.525492, -0.126484)
        };
        List<LatLng> londonPleLatLng = new List<LatLng>() {
            new LatLng(51.507546,-0.147784),new LatLng(51.510452,-0.122705)
        };
        List<LatLng> cityenters = new List<LatLng>() {
            new LatLng(51.507351,-0.127758), new LatLng(40.712776, -74.005974)
        };

        List<LatLng> newCleLatLng = new List<LatLng>() {
            new LatLng(40.724468, -73.976562),new LatLng(40.730452,-74.007826)
        };

        List<LatLng> newPleLatLng = new List<LatLng>() {
            new LatLng(40.709896, -74.009887),new LatLng(40.717183,-73.987213)
        };






        Button animateToLocationButton;
        GoogleMap googleMap;
        int name;


        public void OnMapReady(GoogleMap map)
        {
            googleMap = map;

            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.UiSettings.MyLocationButtonEnabled = false;
            AddMarkersToMap();
            animateToLocationButton.Click += AnimateToPasschendaele;
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.MapLayout);

            var mapFragment = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);

            animateToLocationButton = FindViewById<Button>(Resource.Id.animateButton);
            animateToLocationButton.Click += AnimateToPasschendaele;
             name = Intent.GetIntExtra("locationCode",0);

            SetupZoomInButton();
            SetupZoomOutButton();
        }


        void AnimateToPasschendaele(object sender, EventArgs e)
        {
            // Move the camera to the PasschendaeleLatLng Memorial in Belgium.
            var builder = CameraPosition.InvokeBuilder();
            builder.Target(cityenters[name-1]);
            builder.Zoom(15);
           // builder.Bearing(155);
            //builder.Tilt(65);
            var cameraPosition = builder.Build();

            // AnimateCamera provides a smooth, animation effect while moving
            // the camera to the the position.
            googleMap.AnimateCamera(CameraUpdateFactory.NewCameraPosition(cameraPosition));
        }

        void AddMarkersToMap()
        {

            if (name == 1) {
                for (int i = 0; i < londonPleLatLng.Count; i++)
                {
                    dataSet("Treatment Centre", londonPleLatLng[i], londonCleLatLng[i]);
                }
                  
            }
            else if (name==2) {
                for (int i = 0; i < londonPleLatLng.Count; i++)
                    dataSet("Treatment Centre", newPleLatLng[i], newCleLatLng[i]);


            }
            var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(cityenters[name - 1], 15);
            googleMap.MoveCamera(cameraUpdate);
        }


        void dataSet(string title,LatLng ltlg,LatLng centers) {
            var vimyMarker = new MarkerOptions();
            vimyMarker.SetPosition(ltlg)
                      .SetTitle(title)
                      .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueCyan));
            googleMap.AddMarker(vimyMarker);


            var passchendaeleMarker = new MarkerOptions();
            passchendaeleMarker.SetPosition(centers)
                               .SetTitle("covid patients");
            googleMap.AddMarker(passchendaeleMarker);

            // We create an instance of CameraUpdate, and move the map to it.
        //    var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(ltlg, 15);
          //  googleMap.MoveCamera(cameraUpdate);
        }


        int pData=0;
        int cData = 0;
        void SetupZoomInButton()
        {


            var zoomInButton = FindViewById<Button>(Resource.Id.zoomInButton);
            zoomInButton.Click += (sender, e) => {

                if (name == 1)
                {

                    if (pData == 1)
                    {
                      
                        var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(londonPleLatLng[pData], 15);
                        googleMap.MoveCamera(cameraUpdate);
                        pData = 0;
                    }
                    else {
                        var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(londonPleLatLng[pData], 15);
                        googleMap.MoveCamera(cameraUpdate);
                        pData = 1;
                    }
                    

                }
                else if (name == 2)
                {

                    if (pData == 1)
                    {
                        
                        var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(newPleLatLng[pData], 15);
                        googleMap.MoveCamera(cameraUpdate);
                        pData = 0;
                    }
                    else
                    {
                        var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(newPleLatLng[pData], 15);
                        googleMap.MoveCamera(cameraUpdate);
                        pData = 1;
                    }



                }


            };
        }

        void SetupZoomOutButton()
        {
            var zoomOutButton = FindViewById<Button>(Resource.Id.zoomOutButton);
            zoomOutButton.Click += (sender, e) => {
                if (name == 1)
                {

                    if (cData == 1)
                    {
                       
                        var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(londonCleLatLng[cData], 15);
                        googleMap.MoveCamera(cameraUpdate);
                        cData = 0;
                    }
                    else
                    {
                        var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(londonCleLatLng[cData], 15);
                        googleMap.MoveCamera(cameraUpdate);
                        cData = 1;
                    }


                }
                else if (name == 2)
                {

                    if (cData == 1)
                    {

                        var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(newCleLatLng[cData], 15);
                        googleMap.MoveCamera(cameraUpdate);
                        cData = 0;
                    }
                    else
                    {
                        var cameraUpdate = CameraUpdateFactory.NewLatLngZoom(newCleLatLng[cData], 15);
                        googleMap.MoveCamera(cameraUpdate);
                        cData = 1;
                    }



                }

            };
        }

    }
}
