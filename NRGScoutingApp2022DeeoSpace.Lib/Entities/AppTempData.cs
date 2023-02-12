﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NRGScoutingApp2022DeeoSpace.Lib.Entities
{
    public class AppTempData
    {
        [PrimaryKey]
        public string Key
        {
            get;
            set;
        } = string.Empty;

        public string Data
        {
            get;
            set;
        } = string.Empty;
    }
}
