using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace MelodicLib.lib {
    public class MelodicDeposits : IHasData {
        private readonly IMelodicLib view;

        public MelodicDeposits(IMelodicLib view) {
            this.view = view;
        }

        public void Init() {
            depositsurface     = Resources.FindObjectsOfTypeAll<DepositLocationSurface>();
            depositunderground = Resources.FindObjectsOfTypeAll<DepositLocationUnderground>();

            foreach (var dict in view.exportHandler.melodicDeposits) {
                CreateDeposit(dict.Key.underground, dict.Key.percent_replace, dict.Key.output_name, dict.Key.yields[0], dict.Key.yields[1], dict.Key.replaced_item);
            }

            view.melodicLog.LogToMod($"[{view.modName} | Deposits]: Deposits Loaded...");
        }

        public void CreateDeposit(bool Underground, int PercentageToReplace, string outputname, float minyield, float maxyield, string ItemToReplace) {
            if (Underground) {
                foreach (var underground in depositunderground) {
                    if (Random.Range(0, 100) <= PercentageToReplace) {
                        if ((ItemToReplace != null && underground.Ore == GetItem(ItemToReplace)) || ItemToReplace == null) {
                            underground.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(underground, GetItem(outputname));
                        }
                    }
                }
            }
            if (!Underground) {
                foreach (var surface in depositsurface) {
                    if (Random.Range(0, 100) <= PercentageToReplace) {
                        if ((ItemToReplace != null && surface.Ore == GetItem(ItemToReplace)) || ItemToReplace == null) {
                            surface.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(surface, GetItem(outputname));
                        }
                    }
                }
            }
            view.melodicLog.LogToMod($"[{view.modName} | Deposits]: Deposit Replacing " + ItemToReplace + " has been replaced with " + outputname);
        }

        private ItemDefinition GetItem(string itemname) {
            var item = GameResources.Instance.Items.FirstOrDefault(s => s.name == itemname);
            if (item == null) {
                view.melodicLog.LogToMod($"ERROR: [{view.modName} | Deposits]: Item is null, name: " + itemname + ". Replacing with NullItem");

                Debug.LogError($"[{view.modName} | Deposits]: Item is null, name: " + itemname + ". Replacing with NullItem");
                return GameResources.Instance.Items.FirstOrDefault(s => s.name == "NullItem");
            }
            return item;
        }

        private                 DepositLocationSurface[]     depositsurface;
        private                 DepositLocationUnderground[] depositunderground;
        private static readonly FieldInfo                    OreField = typeof(DepositLocation).GetField("m_ore", BindingFlags.NonPublic | BindingFlags.Instance);
    }
}