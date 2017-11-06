using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Socialtap.Code.Controller;
using Socialtap.Code.Model;
using Socialtap.Code.View_.Fragments;

namespace Socialtap.Code.View_
{
    [Activity(Label = "Social-tap", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity
    {
        private const string Tag = "MainActivity";
        private BottomNavigationView _bottomNavigation;

        /// Lango inicializacija
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (savedInstanceState != null) {
                // Statinė atmintis
            }

            // Pagrindinio view'o inicializacija
            SetContentView(Resource.Layout.Main);

            // Atgal mygtukas
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);

            // UI komponentų referencai
            GetReferencesFromLayout();

            // Paspaustas navigacijos langelis
            _bottomNavigation.NavigationItemSelected += NavigationItemSelected;

            // Užkraunamas defaultinis fragmentas atidarius aplikaciją
            LoadFragment(Resource.Id.fragment_review);
        }

        /// <summary>
        /// Kviečiamas, kai paspaudžiama ant viršutinės meniu juostos elemento
        /// </summary>
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

        /// Prideda funkcionalių UI elementų nuorodas 
        private void GetReferencesFromLayout() {
            _bottomNavigation = 
                FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
        }

        /// <summary>
        /// Užkrauna fragmentą layouto dalyje
        /// </summary>
        /// <param name="id">Fragmento id</param>
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
                default:
                    Log.Warn(Tag, "LoadFragment(id) unknown ID");
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

        /// <summary>
        /// Kviečiamas, kai paspaudžiama ant apatinės navigacijos juostos elemento
        /// </summary>
        private void NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }
    }
}

