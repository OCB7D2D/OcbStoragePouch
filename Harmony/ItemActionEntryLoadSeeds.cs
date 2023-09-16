using HarmonyLib;
using System.Collections.Generic;
using static TypedMetadataValue;

public class ItemActionEntryLoadSeeds : BaseItemActionEntry
{

    // ####################################################################
    // ####################################################################

    readonly XUiC_ItemStack Controller;
    private readonly ItemValue Pouch;
    private readonly ItemStack Stack;

    // ####################################################################
    // ####################################################################

    public ItemActionEntryLoadSeeds(XUiC_ItemStack controller, ItemStack stack, ItemValue pouch) :
        base(controller, "lblContextActionLoadSeeds", "ui_game_symbol_pouch_out", GamepadShortCut.DPadRight)
    {
        Stack = stack;
        Pouch = pouch;
        Controller = controller;
    }

    // ####################################################################
    // ####################################################################

    public override void OnActivated()
    {
        var bag = ItemController.xui.PlayerInventory.Backpack;
        var belt = ItemController.xui.PlayerInventory.Toolbelt;
        object inv = Pouch.GetMetadata("seeds");
        var pouch = new List<ItemStack>();
        if (inv != null) OcbSeedPouch.Decode(inv, pouch);
        HideCollectedItems = true;
        for (int i = 0; i < pouch.Count; i++)
        {
            if (!Controller.xui.PlayerInventory.AddItem(pouch[i], false))
                Controller.xui.PlayerInventory.DropItem(pouch[i]);
        }
        HideCollectedItems = false;
        Pouch.SetMetadata("seeds", "", TypeTag.String);
        bag.SetSlots(bag.GetSlots());
        belt.SetSlots(belt.GetSlots());
        Stack.itemValue = Pouch;
        Controller.ItemStack = Stack;
    }

    // ####################################################################
    // Patch sidebar item counter to just count up and down
    // Otherwise re-seeding can get pretty weird
    // ####################################################################

    static bool HideCollectedItems = false;

    [HarmonyPatch(typeof(XUiC_CollectedItemList), "AddItemStack")]
    static class CollectedItemListAddItemStackPatch
    {
        static bool Prefix() => !HideCollectedItems;
    }

    // ####################################################################
    // ####################################################################

}