using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Socialtap.Code.Controller;

namespace Socialtap.Code.View_.Fragments
{
    public class BarListAdapter : RecyclerView.Adapter
    {
        /// <summary>
        /// Kiekvieno elemento liste reference'ų gavimas
        /// </summary>
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

        /// <summary>
        /// Sukuria naują view'ą BarList fragmentui
        /// </summary>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Layouto užkrovimas
            const int id = Resource.Layout.fragment_bar_list_item;
            var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            return new BarListViewHolder(itemView);
        }

        /// Pakeičia view'o turinį užkraunant/scrollinant
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = MainActivityController.BarsData[position];

            if (!(holder is BarListViewHolder viewHolder)) return;
            viewHolder.Title.Text = item.BarName;
            viewHolder.BarRating.Text = item.AverageRating.ToString();
            viewHolder.AvgBeverageVolume.Text = item.AverageBeverageVolume.ToString();
        }

        public override int ItemCount => MainActivityController.BarsData.Count;
    }
}
