using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Socialtap.Model;

namespace Socialtap.Code.BarListRecyclerView
{
    public class BarListAdapter : RecyclerView.Adapter
    {
        private readonly List<BarData> _barsData;

        private class BarListViewHolder : RecyclerView.ViewHolder
        {
            internal readonly TextView Title;
            internal readonly TextView BarRating;
            internal readonly TextView AvgBeverageVolume;

            public BarListViewHolder(View v) : base(v)
            {
                Title = v.FindViewById<TextView>(Resource.Id.barTitle);
                BarRating = v.FindViewById<TextView>(Resource.Id.barRating);
                AvgBeverageVolume = v.FindViewById<TextView>(Resource.Id.avgBeverageVolume);
            }
        }
        
        public BarListAdapter(List<BarData> barsData)
        {
            _barsData = barsData;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup and inflate your layout here
            var id = Resource.Layout.fragment_bar_list_item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            return new BarListViewHolder(itemView);
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _barsData[position];

            // Replace the contents of the view with that element

            if (!(holder is BarListViewHolder viewHolder)) return;
            viewHolder.Title.Text = item.barName;
            viewHolder.BarRating.Text = item.AvgRating.ToString();
            viewHolder.AvgBeverageVolume.Text = item.AvgBeverageVolume.ToString();
        }

        public override int ItemCount => _barsData.Count;
    }
}
