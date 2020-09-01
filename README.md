------------------------------------------------------------------------
NexusHQ README
------------------------------------------------------------------------
Author: https://github.com/NealSpellman

Platform: Tested on Windows 10

Version: 1.0

Date: 9/1/20

This is a simple tool made with Unity to visualize LEGO Universe's drop tables.
It doesn't have every activity in the game, but it does have most of the enemies in the game.
It also supports adding in item icons, but we don't include them in the repository.
(I don't want to step on any toes by including tons of original assets)

![Preview Picture](https://i.imgur.com/96IVgPB.png)

------------------------------------------------------------------------
Information
------------------------------------------------------------------------
If you do want to add in icons, create "StreamingAssets" within your Assets folder and then an "ItemIcons" folder.
Once you've done that, you can add in any item's icons you want from your local copy of LEGO Universe.
Name each .dds file from the game like this: (itemID).dds, so for example 3040.dds would add in an image for Red Imaginite.

Otherwise, this app wasn't built with a very nice scaling UI.
If you'd like to take on the work of fixing up the UI, then by all means! This is an open source project.

------------------------------------------------------------------------
Adding activities/loot tables
------------------------------------------------------------------------
Now, maybe you've found more activities or just want to check out what models/bricks drop from each place.

For adding activities, check out activities.json. Should be easy enough to understand. (add in a new activity with a name and LootMatrixIndex)
I've carefully curated the LootTable.json file to specifically focus on "rare" items and gear.
If you'd like to change that, feel free to use other tools or check out CDClient in the LEGO Universe game files to find other relevant tables.
Once you've added in / changed a LootTable entry that's linked to an activity's LootMatrixIndex, you should be able to verify it by hitting play in Unity.

I've included a fully converted object table, but the rest you'll have to manually add.
