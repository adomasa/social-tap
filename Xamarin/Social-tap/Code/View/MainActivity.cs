using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Socialtap.Code.Fragments;
using Socialtap.Model;

namespace Socialtap.Code
{
    [Activity(Label = "Social-tap", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity
    {
        public static string TAG = "MainActivity";
        BottomNavigationView bottomNavigation;

        /// Lango inicializacija
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            MainActivityController.BarsData = new List<BarData>();
            if (savedInstanceState != null) {
                // Statinė atmintis
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Atgal mygtukas
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);

            // UI komponentų referencai
            GetReferencesFromLayout();

            // Paspaustas navigacijos langelis
            bottomNavigation.NavigationItemSelected += NavigationItemSelected;

            // Load the first fragment on creation
            LoadFragment(Resource.Id.fragment_review);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        /// Prideda funkcionalių UI objektų nuorodas 
        private void GetReferencesFromLayout() {
            bottomNavigation = 
                FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
        }

        private void LoadFragment(int id)
        {
            Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.fragment_review:
                    fragment = ReviewFragment.NewInstance();
                    SupportActionBar.SetTitle(Resource.String.review_fragment_title);
                    break;
                case Resource.Id.fragment_bar_list:
                    fragment = BarListFragment.NewInstance();
                    SupportActionBar.SetTitle(Resource.String.bar_list_fragment_title);
                    break;
                case Resource.Id.fragment_map:
                    SupportActionBar.SetTitle(Resource.String.map_fragment_title);
                    // ..
                    break;               
            }

            if (fragment == null)
            {
                return;
            }
                            
            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.content_frame, fragment)
                .Commit();
        }

        private void NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
    }
}

