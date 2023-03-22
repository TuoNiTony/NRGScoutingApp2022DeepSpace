using NRGScoutingApp2022DeeoSpace.Lib.Data;
using NRGScoutingApp2022DeeoSpace.Lib.Entities;
using NRGScoutingApp2022DeeoSpace.Lib.Helpers;
using NRGScoutingApp2022DeeoSpace.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NRGScoutingApp2022DeepSpace.Views.PitScoutViews;

[QueryProperty(nameof(TeamNum), nameof(TeamNum))]
public partial class AllPitScoutEntriesPage : ContentPage
{
    private PitScoutDatabase database;

    public AllPitScoutEntriesPage()
    {
        InitializeComponent();
    }

    public AllPitScoutEntriesPage(PitScoutDatabase database)
    {
        InitializeComponent();

        this.database = database;
    }


    public int TeamNum
    {
        get;
        set;
    } = -1;

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if (this.TeamNum >= 0)
            await this.SaveTempMatchEntrAndSwitchMainPageAsync(this.TeamNum);
        else
            await this.BindDataAsync();
    }

    private async Task BindDataAsync()
    {
        this.pitScoutCollection.ItemsSource = await this.database.GetAllPitScoutEntriesAsync();
    }

    private async Task SaveTempMatchEntrAndSwitchMainPageAsync(int teamNum)
    {
        Team team = await this.database.GetTeamByNumAsync(teamNum);

        if (team != null)
        {
            PitScoutEntry entry = new PitScoutEntry()
            {
                TeamNum = teamNum,
                TeamName = team.TeamName
            };

            await this.database.SaveAppTempData(PitScoutConstants.TempPitScoutKey, entry);

            App.Current.MainPage = new PitScoutPage(database);
        }
    }

    private async void Add_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(TeamSelectorPage)}?Target=..");
    }

    private async void pitScoutCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count != 0)
        {
            PitScoutEntry entry = (PitScoutEntry)e.CurrentSelection[0];

            // Should navigate to "MatchEntryDetail?MatchId=0"
            //await Shell.Current.GoToAsync($"{nameof(MatchEntryDetailPage)}?MatchEntryId={entry.Id}");

            // Unselect the UI
            this.pitScoutCollection.SelectedItem = null;
        }
    }

    private async void DeleteAll_Clicked(object sender, EventArgs e)
    {
        if (await this.DisplayAlert("Confirm", "Do you want to delete all matches ? Data CANNOT be recovered.", "YES", "NO"))
        {
            await this.database.DeleteAllPitScoutEntriesAsync();
            await this.BindDataAsync();
        }
    }
}
