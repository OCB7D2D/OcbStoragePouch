using System.Collections.Generic;
using static TypedMetadataValue;

public class ItemActionEntryLoadSeeds : BaseItemActionEntry
{

    XUiC_ItemStack Controller;
    private readonly ItemValue Pouch;
    private readonly ItemStack Stack;

    public ItemActionEntryLoadSeeds(XUiC_ItemStack controller, ItemStack stack, ItemValue pouch) :
        base(controller, "lblContextActionLoadSeeds", "ui_game_symbol_pouch_out", GamepadShortCut.DPadRight)
    {
        Stack = stack;
        Pouch = pouch;
        Controller = controller;
    }

    public override void OnActivated()
    {
        var bag = ItemController.xui.PlayerInventory.Backpack;
        var belt = ItemController.xui.PlayerInventory.Toolbelt;
        object inv = Pouch.GetMetadata("seeds");
        var pouch = new List<ItemStack>();
        if (inv != null) OcbSeedPouch.Decode(inv, pouch);
        for (int i = 0; i < pouch.Count; i++)
        {
            if (!Controller.xui.PlayerInventory.AddItem(pouch[i], false))
                Controller.xui.PlayerInventory.DropItem(pouch[i]);
        }
        Pouch.SetMetadata("seeds", "", TypeTag.String);
        bag.SetSlots(bag.GetSlots());
        belt.SetSlots(belt.GetSlots());
        Stack.itemValue = Pouch;
        Controller.ItemStack = Stack;
    }

}