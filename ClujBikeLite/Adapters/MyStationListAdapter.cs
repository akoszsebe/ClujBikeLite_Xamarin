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

        public MyStationListAdapter(Activity context)
        {
            this.activity = context;
            this.context = context;
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
            var tvhousename = view.FindViewById<TextView>(Resource.Id.);
            var tvactivedevicenumber = view.FindViewById<TextView>(Resource.Id.tvActiveDevicesNumber);
            var tvValue = view.FindViewById<TextView>(Resource.Id.tvValue);
            var ivsettings = view.FindViewById<ImageView>(Resource.Id.imageviewUserHouseSettings);
            var ivhouseicon = view.FindViewById<ImageView>(Resource.Id.imageviewHouseColoricon);

            tvactivedevicenumber.Text = context.Resources.GetString(Resource.String.textview_active) + " " + itemList[position].activedevices.ToString() + " " + context.Resources.GetString(Resource.String.textview_device);
            tvValue.Text = itemList[position].sumwat + "W";
            ivhouseicon.SetImageResource(HouseSelector.GetIconId(itemList[position].sumwat));

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

        public ListViewItemStation(long id, Station station)
        {
            this.Id = id;
            this.station = station;
            
        }
    }
}