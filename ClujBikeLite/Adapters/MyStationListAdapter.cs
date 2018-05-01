using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using ClujBikeLite.Models;
using System.Collections.Generic;

namespace ClujBikeLite.Adapters
{
    class MyStationListAdapter: BaseAdapter
    {

        List<ListViewItemStation> itemList;
        Context context;
        Activity activity;
        bool favorite;

        public MyStationListAdapter(Activity context, bool favorite)
        {
            this.activity = context;
            this.context = context;
            this.favorite = favorite;
            itemList = new List<ListViewItemStation>();
        }

        public override int Count
        {
            get { return itemList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return itemList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            LayoutInflater inflater = (LayoutInflater)context
                .GetSystemService(Context.LayoutInflaterService);
            var view = inflater.Inflate(Resource.Layout.stationListViewItem, parent, false);
            var tvstationname = view.FindViewById<TextView>(Resource.Id.tvStationName);
            var tvstationAddress = view.FindViewById<TextView>(Resource.Id.tvStationAddress);
            var tvactivedevicenumber = view.FindViewById<TextView>(Resource.Id.tvOcuppiedSpotsNumber);
            var tvValue = view.FindViewById<TextView>(Resource.Id.tvValue);
            var ivsettings = view.FindViewById<ImageView>(Resource.Id.imageviewAddToFavourites);
            var img_favorite = view.FindViewById<ImageView>(Resource.Id.imageviewAddToFavourites);
            var imageviewActive = view.FindViewById<ImageView>(Resource.Id.imageviewActive);

            if (itemList[position].is_favorite)
            {
                img_favorite.SetImageResource(Resource.Drawable.favouriteselected_48);
            }
            else
            {
                img_favorite.SetImageResource(Resource.Drawable.favourite_50);
            }


           

            ListViewItemStation station = itemList[position];
            if (station.station.Status != "Functionala" && station.station.StatusType != "Online")
            {
                imageviewActive.SetImageResource(Resource.Drawable.in_active);
            }

            if (favorite)
            { 
                img_favorite.SetImageResource(Resource.Drawable.favouriteselected_48);
                img_favorite.Click += delegate
                {
                    ISharedPreferences sharedPref = activity.GetSharedPreferences("favorite_stations", FileCreationMode.Private);
                    ICollection<string> favorite_stations = sharedPref.GetStringSet("favorite_stations", new List<string>());
                    ISharedPreferencesEditor editor = sharedPref.Edit();
                    favorite_stations.Remove(station.station.StationName);
                    editor.PutStringSet("favorite_stations", favorite_stations);
                    editor.Commit();
                    itemList.Remove(station);
                    this.NotifyDataSetChanged();
                };
            }
            else
            {
                img_favorite.Click += delegate
                {
                    ISharedPreferences sharedPref = activity.GetSharedPreferences("favorite_stations", FileCreationMode.Private);
                    ICollection<string> favorite_stations = sharedPref.GetStringSet("favorite_stations", new List<string>());
                    ISharedPreferencesEditor editor = sharedPref.Edit();
                    favorite_stations.Add(station.station.StationName);
                    editor.PutStringSet("favorite_stations", favorite_stations);
                    editor.Commit();
                    itemList.Find(x => x.station == station.station).is_favorite = true;
                    this.NotifyDataSetChanged();
                };
            }

            tvstationname.Text = itemList[position].station.StationName;
            tvstationAddress.Text = itemList[position].station.Address;
            tvactivedevicenumber.Text = "Bikes "+itemList[position].station.OcuppiedSpots.ToString();
            tvValue.Text = "Parking "+itemList[position].station.EmptySpots.ToString();

            return view;
        }

        public void AddData(List<ListViewItemStation> items)
        {
            itemList.Clear();
            itemList.AddRange(items);
        }



    }


    class ListViewItemStation
    {
        public long Id { get; set; }
        public Station station { get; set; }
        public bool is_favorite { get; set; }

        public ListViewItemStation(long id, Station station,bool is_favorite)
        {
            this.Id = id;
            this.station = station;
            this.is_favorite = is_favorite;
            
        }
    }
}