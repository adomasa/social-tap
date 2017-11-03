using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Socialtap.Model;

namespace Socialtap.Code.BarListRecyclerView
{
    public class BarListAdapter : RecyclerView.Adapter
    {
        readonly List<BarData> barsData;

        public class ViewHolder : RecyclerView.ViewHolder
        {
            public TextView Title;
            public TextView BarRating;
            public TextView AvgBeverageVolume;

            public ViewHolder(View v) : base(v)
            {
                Title = v.FindViewById<TextView>(Resource.Id.barTitle);
                BarRating = v.FindViewById<TextView>(Resource.Id.barRating);
                AvgBeverageVolume = v.FindViewById<TextView>(Resource.Id.avgBeverageVolume);
            }
        }
        
        public BarListAdapter(List<BarData> barsData)
        {
            this.barsData = barsData;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup and inflate your layout here
            var id = Resource.Layout.fragment_bar_list_item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            return new ViewHolder(itemView);
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = barsData[position];

            // Replace the contents of the view with that element

            if (holder is ViewHolder viewHolder)
            {
                viewHolder.Title.Text = item.barName;
                viewHolder.BarRating.Text = item.avgRating.ToString();
                viewHolder.AvgBeverageVolume.Text = item.avgBeverageVolume.ToString();
            }
        }

        public override int ItemCount => barsData.Count;
    }
}
