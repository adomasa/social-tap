using System.Linq;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Socialtap.Code.Controller;

namespace Socialtap.Code.View_.Fragments
{
    public class BarListAdapter : RecyclerView.Adapter
    {
        static readonly string Tag = typeof(BarListAdapter).Name;
        /// <summary>
        /// Gets the reference of each item from the list
        /// </summary>
        private class BarListViewHolder : RecyclerView.ViewHolder
        {
            internal readonly TextView Title;
            internal readonly TextView BarRating;
            internal readonly TextView AvgBeverageVolume;
            internal readonly TextView ReviewCount;

            public BarListViewHolder(View v) : base(v)
            {
                Title = v.FindViewById<TextView>(Resource.Id.barTitle);
                BarRating = v.FindViewById<TextView>(Resource.Id.barRating);
                AvgBeverageVolume = v.FindViewById<TextView>(Resource.Id.avgBeverageVolume);
                ReviewCount = v.FindViewById<TextView>(Resource.Id.reviewCount);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Loads layout
            const int id = Resource.Layout.fragment_bar_list_item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            return new BarListViewHolder(itemView);
        }

        /// Updates view content on load/scroll
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = MainController.barsData.ElementAt(position);

            if (!(holder is BarListViewHolder viewHolder)) return;
            viewHolder.Title.Text = MainController.barsData.ElementAt(position).Key;
            viewHolder.BarRating.Text = item.Value.RateAvg.ToString();
            viewHolder.AvgBeverageVolume.Text = item.Value.BeverageAvg.ToString();
            viewHolder.ReviewCount.Text = item.Value.BarUses.ToString();
        }

        public override int ItemCount => MainController.barsData.Count;
    }
}
