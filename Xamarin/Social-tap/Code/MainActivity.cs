using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using Socialtap.Code.View_.Fragments;
using Fragment = Android.App.Fragment;

namespace Socialtap.Code
{
    [Activity(Label = "Social-tap", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity
    {
        private const string Tag = "MainActivity";
        private BottomNavigationView _bottomNavigation;
        private ScrollView _contentView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            // Hide actionbar item
            SupportActionBar.SetDisplayHomeAsUpEnabled(false);

            GetReferencesFromLayout();

            _bottomNavigation.NavigationItemSelected += NavigationItemSelected;

            // Load default fragment
            LoadFragment(Resource.Id.fragment_home);
        }

        /// <summary>
        /// Called on top action bar item click
        /// </summary>
        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    switch (item.ItemId)
        //    {
        //        case Android.Resource.Id.Home:
        //            Finish();
        //            return true;

        //        default:
        //            return base.OnOptionsItemSelected(item);
        //    }
        //}

        /// Add references from UI layout
        private void GetReferencesFromLayout() {
            _bottomNavigation = 
                FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            _contentView = FindViewById<ScrollView>(Resource.Id.content_frame);
        }

        /// <summary>
        /// Loads fragment inside activity
        /// </summary>
        /// <param name="id">Fragment id</param>
        private void LoadFragment(int id)
        {
            Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.fragment_home:
                    fragment = LoadingFragment.NewInstance(Resource.Id.fragment_home);
                    SupportActionBar
                        .SetTitle(Resource.String.home_fragment_title);
                    break;
                case Resource.Id.fragment_review:
                    fragment = ReviewFragment.NewInstance();
                    SupportActionBar
                        .SetTitle(Resource.String.review_fragment_title);
                    break;
                case Resource.Id.fragment_bar_list:
                    fragment = LoadingFragment.NewInstance(Resource.Id.fragment_bar_list);
                    SupportActionBar
                        .SetTitle(Resource.String.bar_list_fragment_title);
                    break;
                default:
                    Log.Warn(Tag, $"LoadFragment unknown ID: {id}");
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
        /// Called on bottom navigation item tap
        /// </summary>
        private void NavigationItemSelected(object sender, 
                                            BottomNavigationView
                                            .NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        /// <summary>
        /// Reports the state of the add bar review.
        /// </summary>
        /// <param name="status">If set to <c>true</c> status.</param>
        public void ReportAddBarReviewState(bool status)
        {
            var reportContent = 
                status ? GetString(Resource.String.request_success) : GetString(Resource.String.request_failed);
            Snackbar.Make (_contentView, 
                       reportContent, 
                       Snackbar.LengthShort).Show(); 
        }
    }
}

