using NRGScoutingApp2022DeeoSpace.Lib.Helpers;
using NRGScoutingApp2022DeeoSpace.Lib.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRGScoutingApp2022DeeoSpace.Lib.Entities
{
    public class PitScoutEntity
    {
        public PitScoutEntity()
        {

        }

        public PitScoutEntity(PitScoutEntry entry)
        {
            this.Id = entry.Id;
            this.TeamNum = entry.TeamNum;
            this.TeamName = entry.TeamName;

            this.Data = JsonHelper.Serialize(entry);
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int TeamNum { get; set; }

        public int MatchNum { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public string Data { get; set; } = string.Empty;
    }
}
