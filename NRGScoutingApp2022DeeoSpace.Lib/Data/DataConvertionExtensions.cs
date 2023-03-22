using NRGScoutingApp2022DeeoSpace.Lib.Entities;
using NRGScoutingApp2022DeeoSpace.Lib.Helpers;
using NRGScoutingApp2022DeeoSpace.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRGScoutingApp2022DeeoSpace.Lib.Data
{
    public static class DataConvertionExtensions
    {
        public static MatchEntry? ToMatchEntry(this MatchEntryEntity entity)
        {
            MatchEntry? entry = null;

            if (entity != null && string.IsNullOrEmpty(entity.Data) == false)
            {
                entry = JsonHelper.Deserialize<MatchEntry>(entity.Data);

                if (entry != null)
                    entry.Id = entity.Id;
            }

            return entry;
        }

        public static List<MatchEntry> ToEntries(this List<MatchEntryEntity> entities)
        {
            List<MatchEntry> result = new List<MatchEntry>();

            if (entities != null)
            {
                foreach (MatchEntryEntity entity in entities)
                {
                    MatchEntry? entry = entity.ToMatchEntry();

                    if (entry != null)
                        result.Add(entry);
                }
            }

            return result;
        }

        public static PitScoutEntry? ToPitScoutEntry(this PitScoutEntity entity)
        {
            PitScoutEntry? entry = null;

            if (entity != null && string.IsNullOrEmpty(entity.Data) == false)
            {
                entry = JsonHelper.Deserialize<PitScoutEntry>(entity.Data);

                if (entry != null)
                    entry.Id = entity.Id;
            }

            return entry;
        }

        public static List<PitScoutEntry> ToPitScoutEntries(this List<PitScoutEntity> entities)
        {
            List<PitScoutEntry> result = new List<PitScoutEntry>();

            if (entities != null)
            {
                foreach (PitScoutEntity entity in entities)
                {
                    PitScoutEntry? entry = entity.ToPitScoutEntry();

                    if (entry != null)
                        result.Add(entry);
                }
            }

            return result;
        }
    }
}
