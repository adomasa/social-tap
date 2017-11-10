using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;

namespace Socialtap.Code.View_.Fragments
{
    public class BarListFragment : Fragment
    {
        private RecyclerView _recyclerView;
        private RecyclerView.LayoutManager _layoutManager;
        private BarListAdapter _adapter;
        private View _rootView;

        public static BarListFragment NewInstance()
        {
            return new BarListFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _rootView = inflater.Inflate(Resource.Layout.fragment_bar_list, container, false);
            _recyclerView = _rootView.FindViewById<RecyclerView>(Resource.Id.list);
            _layoutManager = new LinearLayoutManager(Activity);
            _recyclerView.SetLayoutManager(_layoutManager);
            _adapter = new BarListAdapter();
            _recyclerView.SetAdapter(_adapter);
            
            return _rootView;
        }

        //public void NotifyChangesToAdapter()
        //{
        //    _adapter.NotifyDataSetChanged();
        //}
    }
}
