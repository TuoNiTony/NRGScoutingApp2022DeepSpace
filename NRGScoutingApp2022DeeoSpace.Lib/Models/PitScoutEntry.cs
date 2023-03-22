using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRGScoutingApp2022DeeoSpace.Lib.Models
{
    public class PitScoutEntry
    {
        internal int Id { get; set; }

        public int TeamNum { get; set; }

        public string TeamName { get; set; } = string.Empty;

        public List<PitNote>? Questions
        {
            get;
            set;
        } = new List<PitNote>();
    }
}
