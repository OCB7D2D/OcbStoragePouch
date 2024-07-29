# OCB Storage Pouch  - 7 Days to Die (V1.0) Addon

Mod to store all seeds in a pouch to free up used inventory slots.

You need to disable EAC to use this mod!

[![GitHub CI Compile Status][5]][4]

## Description

The pouch can be used to store all seeds you gather on your scavenging hunts.
The briefcase can be used to store all the Magazines you come across.
Usage is a bit convoluted, as I really just put this together in
two evenings. Select the pouch in your inventory and activate one of the
two item actions that will be presented (bag or unpack items). Only items
in your backpack will be bagged (items in your toolbar will stay there).
When unpacking the pouch, any item that will not fit your inventory, will
be spilled on the ground. I may improve these behaviors in the future if
people think this is a useful mod!

## Compatibility with other Mods

Other mods that add seeds will either need to use the same name schema
as vanilla or a specific config. This can either be done in the other
mods directly (`<property name="OcbSeedPouch" value="True"/>`) or by
extending the rule-set in this mod in `Config/block.xml`. In later case,
you must ensure that this mod loads after any mod that adds new items
you want to be able to collect in the seed pouch. This can be achieved
by renaming either mod to enforce the correct load order. For details,
See how the seeds pouch and the magazine briefcase are set up.

## How to unlock

I've put the seed pouch behind pack mule perk level 2 (bag lady),
and the briefcase behind pack mule perk level 2 (neat and tidy),
mainly in order to make that perk branch a little more useful ;)

### Download and Install

End-Users are encouraged to download my mods from [NexusMods][3].  
Every download there helps me to buy stuff for mod development.

Otherwise please use one of the [official releases][2] here.  
Only clone or download the repo if you know what you do!

## Changelog

### Version 0.1.0

- First compatibility with V1.0 (exp)
- Add briefcase for magazines
- Rename to OcbStoragePouch

### Version 0.0.2

- Add compatibility for War3zuk FarmLife

### Version 0.0.1

- Initial version

[1]: https://github.com/OCB7D2D/OcbStoragePouch
[2]: https://github.com/OCB7D2D/OcbStoragePouch/releases
[3]: https://www.nexusmods.com/7daystodie/mods/3700
[4]: https://github.com/OCB7D2D/OcbStoragePouch/actions/workflows/ci.yml
[5]: https://github.com/OCB7D2D/OcbStoragePouch/actions/workflows/ci.yml/badge.svg
