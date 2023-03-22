using NRGScoutingApp2022DeeoSpace.Lib.Data;
using NRGScoutingApp2022DeeoSpace.Lib.Helpers;
using NRGScoutingApp2022DeeoSpace.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NRGScoutingApp2022DeepSpace.Views.PitScoutViews
{
    public class PitScoutBasePage : ContentPage
    {
        private PitScoutDatabase database;

        public PitScoutBasePage(PitScoutDatabase database)
        {
            this.database = database;
        }

        protected PitScoutDatabase Database
        {
            get
            {
                return this.database;
            }
        }

        protected PitScoutEntry CurrentEntry
        {
            get
            {
                return this.BindingContext as PitScoutEntry;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            PitScoutEntry entry = await this.database.GetAppTempDataAsync<PitScoutEntry>(PitScoutConstants.TempPitScoutKey);

            this.BindingContext = entry;
        }

        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            if (this.CurrentEntry != null)
                await this.database.SaveAppTempData(PitScoutConstants.TempPitScoutKey, this.CurrentEntry);
        }

        protected void OnBackToMainPage(object sender, EventArgs e)
        {
            App.Current.MainPage = new AppShell();
        }
    }
}