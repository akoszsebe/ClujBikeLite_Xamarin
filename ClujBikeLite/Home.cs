using Android.App;
using Android.Content;
using Android.OS;

using Android.Support.Design.Widget;
using Android.Support.V7.App;
using ClujBikeLite.Fragments;
using System;
using System.Collections.Generic;

namespace ClujBikeLite
{
    [Activity(Label = "ClujBikeLite", MainLauncher = true)]
    public class Home : AppCompatActivity
    {
        BottomNavigationView bottomNavigation;
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Home);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                SetSupportActionBar(toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(false);
                SupportActionBar.SetHomeButtonEnabled(false);

            }

            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);


            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;

            LoadFragment(Resource.Id.menu_all);
        }

        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.menu_all:
                    fragment = AllFragment.NewInstance();
                    break;
                case Resource.Id.menu_favourites:
                    fragment = FavoriteFragment.NewInstance();
                    break;
            }
            if (fragment == null)
                return;

            SupportFragmentManager.BeginTransaction()
               .Replace(Resource.Id.content_frame, fragment)
               .Commit();
        }
    }
}