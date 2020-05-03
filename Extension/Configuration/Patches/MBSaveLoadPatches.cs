﻿using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.Core;
using YAPO.Global;

// ReSharper disable InconsistentNaming
// ReSharper disable ParameterTypeCanBeEnumerable.Global
// ReSharper disable RedundantAssignment
// ReSharper disable UnusedType.Global
// ReSharper disable UnusedMember.Global

namespace YAPO.Configuration.Patches
{
    public class MBSaveLoadPatches
    {
        [HarmonyPatch(typeof(MBSaveLoad), "LoadSaveGameData")]
        public static class MBSaveLoadLoadSaveGameDataPatch
        {
            public static void Postfix(string saveName)
            {
                States.CurrentSaveId = saveName;
                Task unused = Task.Run(LoadAsync);
            }

            private static void LoadAsync()
            {
                SorterConfigurationManager.Instance.LoadConfigurations();
                (States.PartySorterConfiguration, States.OtherSorterConfiguration) =
                    SorterConfigurationManager.Instance.GetConfiguration(States.CurrentSaveId);
            }
        }

        [HarmonyPatch(typeof(MBSaveLoad), "SaveGame")]
        public static class MBSaveLoadSaveGamePatch
        {
            public static void Postfix(string saveName)
            {
                States.CurrentSaveId = saveName.Replace(".tmp", "");
                Task unused = Task.Run(SaveAsync);
            }

            private static void SaveAsync()
            {
                SorterConfigurationManager.Instance.SaveConfigurations();
            }
        }
    }
}
