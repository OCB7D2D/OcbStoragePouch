using System.Collections.Generic;
using static TypedMetadataValue;

public class ItemActionEntryStoreSeeds : BaseItemActionEntry
{

    // ####################################################################
    // ####################################################################

    readonly XUiC_ItemStack Controller;
    private readonly ItemValue Pouch;
    private readonly ItemStack Stack;
    private readonly string PouchName;

    // ####################################################################
    // ####################################################################

    public ItemActionEntryStoreSeeds(XUiC_ItemStack controller, ItemStack stack, ItemValue pouch) :
        base(controller, "lblContextActionStoreSeeds", "ui_game_symbol_pouch_in", GamepadShortCut.DPadLeft)
    {
        Stack = stack;
        Pouch = pouch;
        Controller = controller;
        PouchName = Pouch?.ItemClass?.Name;
    }

    // ####################################################################
    // ####################################################################

    private bool IsBagable(ItemClass item)
    {
        if (!item.IsBlock()) return item.Properties.GetBool(PouchName);
        else return item.GetBlock().Properties.GetBool(PouchName);
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
        ItemStack[] slots = bag.GetSlots();
        for (int i = 0; i < slots.Length; i++)
        {
            ItemStack slot = slots[i];
            var ic = slot?.itemValue?.ItemClass;
            if (ic == null) continue;
            if (!IsBagable(ic)) continue;
            slots[i] = ItemStack.Empty.Clone();
            pouch.Add(slot); // Add to pouch
        }
        var data = OcbSeedPouch.Encode(pouch);
        Pouch.SetMetadata("seeds", data, TypeTag.String);
        bag.SetSlots(bag.GetSlots());
        belt.SetSlots(belt.GetSlots());
        Stack.itemValue = Pouch;
        Controller.ItemStack = Stack;
    }

    // ####################################################################
    // ####################################################################

}