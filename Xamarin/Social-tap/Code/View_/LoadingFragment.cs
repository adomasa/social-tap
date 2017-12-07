using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Socialtap.Code.Controller;
using Socialtap.Code.Controller.Interfaces;

namespace Socialtap.Code.View_.Fragments
{
    public class LoadingFragment : Fragment
    {

        private const string _targetFragment = "TARGET_FRAGMENT";
        private View _rootView;
        private Fragment _fragment;
        private TextView _errorLabel;
        private ProgressBar _progressBar;

        public static LoadingFragment NewInstance(int fragment_id)
        {
            // Saves target fragment for OnCreate() method
            LoadingFragment fragment = new LoadingFragment();
            Bundle args = new Bundle();
            args.PutInt(_targetFragment, fragment_id);
            fragment.Arguments = args;
            return fragment;
        }

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            int fragmentId = Arguments.GetInt(_targetFragment);
            IMainController controller = MainController.GetInstance(Activity);

            bool request_success;
            switch (fragmentId) {
                case Resource.Id.fragment_home:
                    _fragment = HomeFragment.NewInstance();
                    request_success = await controller.FetchStatistics();
                    break;
                case Resource.Id.fragment_bar_list:
                    _fragment = BarListFragment.NewInstance();
                    request_success = await controller.FetchBarsData();
                    if (MainController.barsData == null) {
                        DisplayError();
                        return;
                    }
                    if (MainController.barsData.Count == 0) 
                    {
                        _errorLabel.Text = GetString(Resource.String.bar_list_empty);
                        DisplayError();
                        return;
                    }
                    break;
                default:
                    Log.Error(Tag, $"LoadingFragment onCreate(): " +
                              "Unknown fragmentId");
                    DisplayError();
                    return;
            }

            if (request_success)
            {
                FragmentManager.BeginTransaction()
                               .Replace(Resource.Id.content_frame, _fragment)
                               .Commit();
            }
            else
            {
                DisplayError();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _rootView = inflater.Inflate(Resource.Layout.fragment_loading, container, false);
            _errorLabel = _rootView.FindViewById<TextView>(Resource.Id.errorLabel);
            _progressBar = _rootView.FindViewById<ProgressBar>(Resource.Id.loadingBar);

            return _rootView;
        }

        /// <summary>
        /// Displays the loading error.
        /// </summary>
        void DisplayError() {
            _progressBar.Visibility = ViewStates.Gone;
            _errorLabel.Visibility = ViewStates.Visible;
        }
    }
}
