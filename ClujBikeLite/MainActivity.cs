using Android.App;
using Android.Widget;
using Android.OS;
using System.Threading;
using ClujBikeLite.Communicaton;
using ClujBikeLite.Adapters;
using System.Collections.Generic;
using ClujBikeLite.Models;

namespace ClujBikeLite
{
    [Activity(Label = "ClujBikeLite", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private ListView mlistview { get; set; }
        private MyStationListAdapter adapter { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button button_sendReq = FindViewById<Button>(Resource.Id.button_sendReq);
            ListView listViev_forResponse = FindViewById<ListView>(Resource.Id.listViewStations);


            button_sendReq.Click += delegate
            {
                new Thread(new ThreadStart(() =>
                {
                    List<ListViewItemStation> items;
                    Station[] stations = null;
                    while (stations == null)
                    {
                        stations = RestClient.GetStationsData().Data;
                        Thread.Sleep(1000);
                    }
                    items = new List<ListViewItemStation>();
                    RunOnUiThread(() =>
                    {
                        for (int i = 0; i < stations.Length; i++)
                        {
                            items.Add(new ListViewItemStation(i, stations[i]));
                        }
                        adapter.AddData(items);
                        adapter.NotifyDataSetChanged();
                    });
                })).Start();
            };


            adapter = new MyStationListAdapter(this);
            mlistview.Adapter = adapter;
        }
    }
}

