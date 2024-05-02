﻿using SpellEditor.Sources.Controls;
using System.Collections.Generic;

namespace SpellEditor.Sources.DBC
{
    class TotemCategory : AbstractDBC, IBoxContentProvider
    {
        public List<DBCBoxContainer> Lookups = new List<DBCBoxContainer>();

        public TotemCategory()
        {
            ReadDBCFile(Config.Config.DbcDirectory + "\\TotemCategory.dbc");
        }

        public override void LoadGraphicUserInterface()
        {
            Lookups.Add(new DBCBoxContainer(0, "None", 0));

            int boxIndex = 1;
            for (uint i = 0; i < Header.RecordCount; ++i)
            {
                var record = Body.RecordMaps[i];
                string name = GetAllLocaleStringsForField("Name", record);
                uint id = (uint)record["ID"];

                Lookups.Add(new DBCBoxContainer(id, name, boxIndex));

                ++boxIndex;
            }

            // In this DBC we don't actually need to keep the DBC data now that
            // we have extracted the lookup tables. Nulling it out may help with
            // memory consumption.
            CleanStringsMap();
            CleanBody();
        }

        public List<DBCBoxContainer> GetAllBoxes()
        {
            return Lookups;
        }

        public int UpdateTotemCategoriesSelection(uint ID)
        {
            var match = Lookups.Find((entry) => entry.ID == ID);
            return match != null ? match.ComboBoxIndex : 0;
        }
    };
}
