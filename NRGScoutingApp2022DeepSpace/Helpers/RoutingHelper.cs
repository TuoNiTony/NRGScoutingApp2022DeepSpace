using NRGScoutingApp2022DeeoSpace.Lib.Data;
using NRGScoutingApp2022DeepSpace.Views;

namespace NRGScoutingApp2022DeepSpace.Helpers
{
    internal static class Registers
    {
        public static void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(Views.MatchEntryDetailPage), typeof(Views.MatchEntryDetailPage));
            Routing.RegisterRoute(nameof(Views.TeamSelectorPage), typeof(Views.TeamSelectorPage));

            Routing.RegisterRoute(nameof(Views.MatchEntryViews.MatchTimerPage), typeof(Views.MatchEntryViews.MatchTimerPage));
            Routing.RegisterRoute(nameof(Views.MatchEntryViews.MatchEventsPage), typeof(Views.MatchEntryViews.MatchEventsPage));
            Routing.RegisterRoute(nameof(Views.MatchEntryViews.MatchInfoPage), typeof(Views.MatchEntryViews.MatchInfoPage));
        }

        public static void RegisterServices(this MauiAppBuilder builder)
        {
            if (builder != null)
            {
                builder.Services
                    .AddSingleton<MatchEntryDatabase>()
                    .AddSingleton<PitScoutDatabase>()
                    .AddTransient<AllMatchEntriesPage>()
                    .AddTransient<MatchEntryDetailPage>()
                    .AddTransient<TeamSelectorPage>()
                    .AddTransient<Views.MatchEntryViews.MatchTimerPage>()
                    .AddTransient<Views.MatchEntryViews.MatchEventsPage>()
                    .AddTransient<Views.MatchEntryViews.MatchInfoPage>()
                    .AddTransient<Views.PitScoutViews.AllPitScoutEntriesPage>()
                    .AddTransient<Views.PitScoutViews.PitScoutPage>();
            }
        }
    }
}
