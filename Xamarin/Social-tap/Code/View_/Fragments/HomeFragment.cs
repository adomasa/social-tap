using Android.App;
using Android.OS;
using Android.Views;

namespace Socialtap.Code.View_.Fragments
{
    public class HomeFragment : Fragment
    {
        private View _rootView;

        public static HomeFragment NewInstance()
        {
            return new HomeFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _rootView = inflater.Inflate(Resource.Layout.fragment_home, container, false);


            return _rootView;
        }
    }
}
