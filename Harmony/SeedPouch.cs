using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;

public class OcbSeedPouch : IModApi
{
    // ####################################################################
    // ####################################################################

    public static void Decode(object inv, List<ItemStack> seeds)
    {
        if (!(inv is string data)) return;
        foreach (var pairs in data.Split(';'))
        {
            var pair = pairs.Split(",", 2);
            if (pair.Length != 2) continue;
            var iv = new ItemValue(int.Parse(pair[0]));
            seeds.Add(new ItemStack(iv, int.Parse(pair[1])));
        }
    }

    public static string Encode(List<ItemStack> seeds)
    {
        return string.Join(';', seeds.ConvertAll(
            s => $"{s.itemValue.type},{s.count}"));
    }

    // ####################################################################
    // ####################################################################

    public void InitMod(Mod mod)
    {
        // if (GameManager.IsDedicatedServer) return;
        Log.Out("OCB Harmony Patch: " + GetType().ToString());
        Harmony harmony = new Harmony(GetType().ToString());
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }

    // ####################################################################
    // ####################################################################

    // Patch to add pin option into action list
    [HarmonyPatch(typeof(XUiC_ItemActionList))]
    [HarmonyPatch("SetCraftingActionList")]
    public class XUiC_ItemActionList_SetCraftingActionList
    {
        static void Postfix(XUiC_ItemActionList __instance,
            List<BaseItemActionEntry> ___itemActionEntries,
            XUiC_RecipeCraftCount ___craftCountControl,
            XUiController itemController)
        {
            if (itemController is XUiC_ItemStack stackController)
            {
                ItemStack itemStack1 = stackController?.ItemStack;
                ItemValue itemValue1 = itemStack1?.itemValue;
                ItemClass itemClass = itemValue1?.ItemClass;
                if (!itemClass.Properties.Contains("StoragePouch")) return;
                __instance.AddActionListEntry(new ItemActionEntryStoreSeeds(
                    stackController, itemStack1, itemValue1));
                __instance.AddActionListEntry(new ItemActionEntryLoadSeeds(
                    stackController, itemStack1, itemValue1));
                // Remove scrap option to avoid unfortunate mishaps
                ___itemActionEntries.RemoveAll(x => x is ItemActionEntryScrap);
            }
        }
    }

    // ####################################################################
    // ####################################################################

}
