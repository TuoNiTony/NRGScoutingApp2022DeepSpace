using NRGScoutingApp2022DeeoSpace.Lib.Entities;
using NRGScoutingApp2022DeeoSpace.Lib.Helpers;
using NRGScoutingApp2022DeeoSpace.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace NRGScoutingApp2022DeeoSpace.Lib.Data
{
    public static class InitDataExtensions
    {
        public async static Task<MatchEntryDatabase> InitTeamsFromResourceAsync(this MatchEntryDatabase database)
        {
            List<Team>? teams = JsonHelper.LoadEmbededData<List<Team>>("Teams");

            await database.Connection.DeleteAllAsync<Team>();

            if (teams != null)
                await database.Connection.InsertAllAsync(teams);

            return await Task.FromResult(database);
        }

        public async static Task<PitScoutDatabase> PSInitTeamsFromResourceAsync(this PitScoutDatabase database)
        {
            List<Team>? teams = JsonHelper.LoadEmbededData<List<Team>>("Teams");

            await database.Connection.DeleteAllAsync<Team>();

            if (teams != null)
                await database.Connection.InsertAllAsync(teams);

            return await Task.FromResult(database);
        }

        public async static Task<MatchEntryDatabase> InitEntriesFromResourceAsync(this MatchEntryDatabase database)
        {
            List<MatchEntry>? entries = JsonHelper.LoadEmbededData<List<MatchEntry>>("MatchEntrySample");

            await database.Connection.DeleteAllAsync<MatchEntryEntity>();

            if (entries != null)
                await database.Connection.InsertAllAsync(entries.ConvertAll(entry => new MatchEntryEntity(entry)));

            return database;
        }

        public async static Task<PitScoutDatabase> PSInitEntriesFromResourceAsync(this PitScoutDatabase database)
        {
            List<PitScoutEntry>? entries = JsonHelper.LoadEmbededData<List<PitScoutEntry>>("PitScoutSample");

            await database.Connection.DeleteAllAsync<PitScoutEntity>();

            if (entries != null)
                await database.Connection.InsertAllAsync(entries.ConvertAll(entry => new PitScoutEntity(entry)));

            return database;
        }


        private static string LoadStringFromResource(string path)
        {
            string result = string.Empty;

            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path))
            {
                if (stream != null)
                {
                    using StreamReader reader = new StreamReader(stream);
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}
