using NRGScoutingApp2022DeeoSpace.Lib.Data;
using NRGScoutingApp2022DeeoSpace.Lib.Helpers;
using NRGScoutingApp2022DeeoSpace.Lib.Models;

namespace NRGScoutingApp2022DeepSpace.Views;

[QueryProperty(nameof(PitScoutEntryId), nameof(PitScoutEntryId))]
public partial class PitScoutEntryDetailPage : ContentPage
{
    private PitScoutDatabase database;

    public PitScoutEntryDetailPage(PitScoutDatabase database)
    {
        InitializeComponent();

        this.database = database;
    }

    public int PitScoutEntryId
    {
        get;
        set;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        PitScoutEntry entry = await this.database.GetPitScoutEntryByIdAsync(this.PitScoutEntryId);

        if (entry != null)
            this.pitScoutData.Text = JsonHelper.Serialize(entry);
    }

    private async void Back_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");

    }
    private async void Delete_Clicked(object sender, EventArgs e)
    {
        if (await this.DisplayAlert("Confirm", "Do you want to delete this entry ? Data CANNOT be recovered.", "YES", "NO"))
        {
            await this.database.DeletePitScoutEntriesAsync(this.PitScoutEntryId);
            await Shell.Current.GoToAsync("..");
        }
    }
}