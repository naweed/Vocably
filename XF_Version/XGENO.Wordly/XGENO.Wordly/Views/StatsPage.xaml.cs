using System;
using System.Collections.Generic;
using Plugin.Segmented.Control;
using Xamarin.Forms;
using XGENO.Wordly.Models;
using XGENO.Wordly.ViewModels;

namespace XGENO.Wordly.Views
{
    public partial class StatsPage : ViewBase<StatsPageViewModel>
    {
        private int currentSelectedSegment = 0;

        public StatsPage() : base()
        {
            InitializeComponent();
        }

        async void SegmentedControl_OnSegmentSelected(System.Object sender, Plugin.Segmented.Event.SegmentSelectEventArgs e)
        {
            if (currentSelectedSegment == e.NewValue)
                return;

            currentSelectedSegment = e.NewValue;

            switch(currentSelectedSegment)
            {
                case 0:
                    StatsView.Opacity = 0;
                    StatsView.IsVisible = true;

                    await GamesView.FadeTo(0, Constants.SmallDuration);
                    await StatsView.FadeTo(1, Constants.SmallDuration);

                    GamesView.IsVisible = false;
                    break;
                case 1:
                    GamesView.Opacity = 0;
                    GamesView.IsVisible = true;

                    await StatsView.FadeTo(0, Constants.SmallDuration);
                    await GamesView.FadeTo(1, Constants.SmallDuration);

                    StatsView.IsVisible = false;
                    break;
            }


        }
    }
}
