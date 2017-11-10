using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using Socialtap.Code.Controller;

namespace Socialtap.Code.View_.Fragments
{
    public class LoadingFragment : Fragment
    {
        private View _rootView;
        private Fragment _fragment;
        private bool _status;
        private TextView _errorLabel;
        private ProgressBar _progressBar;

        public static LoadingFragment NewInstance()
        {
            return new LoadingFragment();
        }

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _fragment = BarListFragment.NewInstance();
            // Laukia kol parsiųs duomenis
            _status = await MainController.FetchBarsData();

            if (_status)
            {
                LoadBarListFragment();
            }
            else
            {
                _progressBar.Visibility = ViewStates.Gone;
                _errorLabel.Visibility = ViewStates.Visible;
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _rootView = inflater.Inflate(Resource.Layout.fragment_loading, container, false);
            _errorLabel = _rootView.FindViewById<TextView>(Resource.Id.errorLabel);
            _progressBar = _rootView.FindViewById<ProgressBar>(Resource.Id.loadingBar);

            return _rootView;
        }

        void LoadBarListFragment() => FragmentManager.BeginTransaction()
            .Replace(Resource.Id.content_frame, _fragment)
            .Commit();
    }
}
