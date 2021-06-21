using System.Linq;
using System.Reflection;
using UnityEngine;
using MelodicLib.lib.scripts;
using System.Collections.Generic;
using static MelodicLib.lib.data.ExportHandler;
using MelodicLib.lib.data;

namespace MelodicLib.lib
{
    class MelodicDeposits
    {
        public void InitDeposits()
        {
            depositsurface = Resources.FindObjectsOfTypeAll<DepositLocationSurface>();
            depositunderground = Resources.FindObjectsOfTypeAll<DepositLocationUnderground>();

            foreach (KeyValuePair<Deposit, string> dict in melodicDeposits)
            {
                CreateDeposit(dict.Key.underground, dict.Key.percent_replace, dict.Key.output_name, dict.Key.yields[0], dict.Key.yields[1], dict.Key.replaced_item);
            }

            MelodicLog.Log($"[{MelodicLib.modName} | Deposits]: Deposits Loaded...");
        }
        public void CreateDeposit(bool Underground, int PercentageToReplace, string outputname, float minyield, float maxyield, string ItemToReplace)
        {

            if (Underground)
            {
                foreach (DepositLocationUnderground underground in depositunderground)
                {
                    if (Random.Range(0, 100) <= PercentageToReplace)
                    {
                        if ((ItemToReplace != null && underground.Ore == GetItem(ItemToReplace)) || ItemToReplace == null)
                        {
                            underground.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(underground, GetItem(outputname));
                        }
                    }
                }
            }
            if (!Underground)
            {
                foreach (DepositLocationSurface surface in depositsurface)
                {
                    if (Random.Range(0, 100) <= PercentageToReplace)
                    {
                        if ((ItemToReplace != null && surface.Ore == GetItem(ItemToReplace)) || ItemToReplace == null)
                        {
                            surface.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(surface, GetItem(outputname));
                        }
                    }
                }
            }
            MelodicLog.Log($"[{MelodicLib.modName} | Deposits]: Deposit Replacing " + ItemToReplace + " has been replaced with " + outputname);
        }
        private ItemDefinition GetItem(string itemname)
        {
            ItemDefinition item = GameResources.Instance.Items.FirstOrDefault(s => s.name == itemname);
            if (item == null)
            {
                MelodicLog.Log($"ERROR: [{MelodicLib.modName} | Deposits]: Item is null, name: " + itemname + ". Replacing with NullItem");

                Debug.LogError($"[{MelodicLib.modName} | Deposits]: Item is null, name: " + itemname + ". Replacing with NullItem");
                return GameResources.Instance.Items.FirstOrDefault(s => s.name == "NullItem");
            }
            return item;

        }
        private DepositLocationSurface[] depositsurface;
        private DepositLocationUnderground[] depositunderground;
        private static readonly FieldInfo OreField = typeof(DepositLocation).GetField("m_ore", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}
