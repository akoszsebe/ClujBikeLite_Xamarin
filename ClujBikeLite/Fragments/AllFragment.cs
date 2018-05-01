
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using ClujBikeLite.Adapters;
using ClujBikeLite.Communicaton;
using ClujBikeLite.Models;
using System.Collections.Generic;
using System.Threading;

namespace ClujBikeLite.Fragments
{
    public class AllFragment : Fragment
    {

        private ListView mlistview { get; set; }
        private MyStationListAdapter adapter { get; set; }
        private Android.Support.V4.Widget.SwipeRefreshLayout refresher { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public static AllFragment NewInstance()
        {
            var frag1 = new AllFragment { Arguments = new Bundle() };
            return frag1;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignored = base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.Main, null);

            mlistview = view.FindViewById<ListView>(Resource.Id.listViewStations);
            refresher = view.FindViewById<Android.Support.V4.Widget.SwipeRefreshLayout>(Resource.Id.refresher);

            adapter = new MyStationListAdapter(this.Activity,false);
            mlistview.Adapter = adapter;

            ISharedPreferences sharedPref = Activity.GetSharedPreferences("favorite_stations", FileCreationMode.Private);
            ICollection<string> favorite_stations = sharedPref.GetStringSet("favorite_stations", new List<string>());

            refresher.Refresh += delegate
            {
                new Thread(new ThreadStart(() =>
                {
                    List<ListViewItemStation> items;
                    Station[] stations;
                    var data = RestClient.GetStationsData();
                    if (data != null)
                    {
                        stations = data.Data;
                        items = new List<ListViewItemStation>();
                        Activity.RunOnUiThread(() =>
                        {
                            for (int i = 0; i < stations.Length; i++)
                            {
                                if (favorite_stations.Contains(stations[i].StationName))
                                    items.Add(new ListViewItemStation(i, stations[i], true));
                                else
                                    items.Add(new ListViewItemStation(i, stations[i], false));
                            }
                            adapter.AddData(items);
                            adapter.NotifyDataSetChanged();
                            refresher.Refreshing = false;
                        });
                    }
                })).Start();
            };


            new Thread(new ThreadStart(() =>
            {
                List<ListViewItemStation> items;
                Station[] stations;
                var data = RestClient.GetStationsData();
                if (data != null)
                {
                    stations = data.Data;
                    items = new List<ListViewItemStation>();
                    Activity.RunOnUiThread(() =>
                    {
                        for (int i = 0; i < stations.Length; i++)
                        {
                            if (favorite_stations.Contains(stations[i].StationName))
                                items.Add(new ListViewItemStation(i, stations[i], true));
                            else
                                items.Add(new ListViewItemStation(i, stations[i], false));
                        }
                        adapter.AddData(items);
                        adapter.NotifyDataSetChanged();
                    });
                }
            })).Start();


            return view;
        }
    }
}