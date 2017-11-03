using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Socialtap.Code.BarListRecyclerView;
using Socialtap.Model;

namespace Socialtap.Code.Fragments
{
    public class BarListFragment : Fragment
    {
        RecyclerView recyclerView;
        RecyclerView.LayoutManager layoutManager;
        BarListAdapter adapter;
        List<BarData> barsData;
        View rootView;

        public static BarListFragment NewInstance()
        {
            return new BarListFragment();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            barsData = new List<BarData>
            {
                new BarData("Bar name", 5, "Comment is here.", 7),
                new BarData("Bar name", 5, "Comment is here.", 7),
                new BarData("Bar name", 5, "Comment is here.", 7),
                new BarData("Bar name", 5, "Comment is here.", 7)
            };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            rootView = inflater.Inflate(Resource.Layout.fragment_bar_list, container, false);
            recyclerView = rootView.FindViewById<RecyclerView>(Resource.Id.list);
            
            layoutManager = new LinearLayoutManager(Activity);
            recyclerView.SetLayoutManager(layoutManager);
                
            adapter = new BarListAdapter(barsData);

            recyclerView.SetAdapter(adapter);
            
            return rootView;
        }
    }
}
