using NRGScoutingApp2022DeeoSpace.Lib.Data;
using NRGScoutingApp2022DeeoSpace.Lib.Entities;
using NRGScoutingApp2022DeeoSpace.Lib.Helpers;
using NRGScoutingApp2022DeeoSpace.Lib.Models;
using NRGScoutingApp2022DeepSpace.Properties;

namespace NRGScoutingApp2022DeepSpace.Views.PitScoutViews;

public partial class PitScoutPage : PitScoutBasePage
{
    private PitScoutDatabase database;


    public PitScoutPage(PitScoutDatabase database)
        : base(database)
    {
        InitializeComponent();

        this.database = database;
    }



    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (this.CurrentEntry != null)
        {
            await this.Database.SaveMatchEntry(this.CurrentEntry);

            await this.DisplayAlert("Notice", AppResource.PitScoutEntrySaved, "OK");
        }
    }
}