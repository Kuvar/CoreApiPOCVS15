using System;
using System.Collections.Generic;
using System;
using Xamarin.Forms;

namespace CoreApiPOC.Views
{
    public partial class MainView : MasterDetailPage
    {
        public MainView()
        {
            try
            {
                InitializeComponent();

                masterPage.ListView.ItemSelected += OnItemSelected;

                if (Device.RuntimePlatform == Device.UWP)
                {
                    MasterBehavior = MasterBehavior.Popover;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
