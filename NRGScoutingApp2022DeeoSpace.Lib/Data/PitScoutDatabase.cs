using NRGScoutingApp2022DeeoSpace.Lib.Entities;
using NRGScoutingApp2022DeeoSpace.Lib.Helpers;
using NRGScoutingApp2022DeeoSpace.Lib.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Environment;

namespace NRGScoutingApp2022DeeoSpace.Lib.Data
{
    public class PitScoutDatabase
    {
        private SQLiteAsyncConnection? conection;

        public PitScoutDatabase(string path)
        {
            this.conection = new SQLiteAsyncConnection(path);
        }

        public PitScoutDatabase()
        {

        }

        public SQLiteAsyncConnection Connection
        {
            get
            {
                if (this.conection == null)
                    throw new NullReferenceException("Conection has not been initialized");

                return this.conection;
            }
        }

        private async Task Init()
        {
            if (this.conection == null)
            {
                this.conection = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData), PitScoutConstants.LocalDatabaseFileName));
                await CreateAllTablesAsync();
                await this.PSInitTeamsFromResourceAsync();
                await this.PSInitEntriesFromResourceAsync(); //  This is for mock data
            }
        }

        public async Task<List<PitScoutEntry>> GetAllPitScoutEntriesAsync()
        {
            await this.Init();

            List<PitScoutEntity> entities = await this.Connection.Table<PitScoutEntity>().ToListAsync();

            return entities.ToPitScoutEntries();
        }

        public async Task<PitScoutEntry?> GetPitScoutEntryByIdAsync(int id)
        {
            await this.Init();

            PitScoutEntity entity = await this.Connection.Table<PitScoutEntity>()
                            .Where(e => e.Id == id)
                            .FirstOrDefaultAsync();

            PitScoutEntry? result = null;

            if (entity != null)
                result = entity.ToPitScoutEntry();

            return result;
        }

        public async Task SaveMatchEntry(PitScoutEntry entry)
        {
            PitScoutEntity entity = new PitScoutEntity(entry);

            if (entity.Id == 0)
            {
                await this.Connection.InsertAsync(entity);
                entry.Id = entity.Id;
            }
            else
            {
                await this.Connection.UpdateAsync(entity);
            }
        }

        public async Task DeleteAllPitScoutEntriesAsync()
        {
            await this.Connection.DeleteAllAsync<PitScoutEntity>();
        }

        public async Task DeletePitScoutEntriesAsync(int id)
        {
            await this.Connection.DeleteAsync<PitScoutEntity>(id);
        }

        public async Task<List<Team>> GetTeamsAsync(string criteria = "")
        {
            await this.Init();

            return await this.Connection.Table<Team>().ToListAsync();
        }

        public async Task<Team> GetTeamByNumAsync(int teamNum)
        {
            await this.Init();

            return await this.Connection.Table<Team>()
                            .Where(t => t.TeamNum == teamNum)
                            .FirstOrDefaultAsync();
        }

        public async Task<T?> GetAppTempDataAsync<T>(string key)
        {
            await this.Init();

            AppTempData tempData = await this.Connection.Table<AppTempData>()
                                    .Where(t => t.Key == key)
                                    .FirstOrDefaultAsync();

            T? result = default(T);

            if (tempData != null && string.IsNullOrEmpty(tempData.Data) == false)
                result = JsonHelper.Deserialize<T>(tempData.Data);

            return result;
        }

        public async Task SaveAppTempData<T>(string key, T data)
        {
            AppTempData tempData = new AppTempData()
            {
                Key = key,
                Data = JsonHelper.Serialize(data)
            };

            await this.Connection.InsertOrReplaceAsync(tempData);
        }

        public async Task<PitScoutDatabase> CreateAllTablesAsync()
        {
            await this.Connection.CreateTableAsync<Team>();
            await this.Connection.CreateTableAsync<AppTempData>();
            await this.Connection.CreateTableAsync<MatchEntryEntity>();

            // Remove the constraint of table MatchEntryEntity
            // await this.Connection.CreateIndexAsync("MatchEntryEntity", new string[] { "TeamNum", "MatchNum", "Side" }, true);

            return this;
        }
    }
}
