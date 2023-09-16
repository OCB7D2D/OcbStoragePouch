using System.Collections.Generic;
using static InvBaseItem;
using static TypedMetadataValue;

public class ItemActionEntryStoreSeeds : BaseItemActionEntry
{

    XUiC_ItemStack Controller;
    private readonly ItemValue Pouch;
    private readonly ItemStack Stack;

    public ItemActionEntryStoreSeeds(XUiC_ItemStack controller, ItemStack stack, ItemValue pouch) :
        base(controller, "lblContextActionStoreSeeds", "ui_game_symbol_pouch_in", GamepadShortCut.DPadLeft)
    {
        Stack = stack;
        Pouch = pouch;
        Controller = controller;
    }

    private bool IsBagable(ItemValue iv)
    {
        // Support custom property to mark anything to bag
        if (iv.ItemClass.Properties.GetBool("PlantSeed")) return true;
        // Support vanilla naming schema for seeds
        var name = iv.ItemClass.Name;
        if (!name.EndsWith("1")) return false;
        if (!name.StartsWith("planted")) return false;
        return true;
    }

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
            if (slot == null) continue;
            if (slot.itemValue == null) continue;
            if (slot.itemValue.ItemClass == null) continue;
            if (!IsBagable(slot.itemValue)) continue;
            slots[i] = ItemStack.Empty.Clone();
            pouch.Add(slot); // Add to pouch
        }
        Pouch.SetMetadata("seeds", OcbSeedPouch.Encode(pouch), TypeTag.String);
        bag.SetSlots(bag.GetSlots());
        belt.SetSlots(belt.GetSlots());
        Stack.itemValue = Pouch;
        Controller.ItemStack = Stack;
    }

}