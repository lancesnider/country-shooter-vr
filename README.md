# Country Shooter VR for Daydream

### An educational geography game for Daydream VR.

The gameplay is simple - When a country name shows up on the screen, find it and shoot it. You'll be scored on speed and accuracy.

## Getting Started

Some horror stories have convinced me not to use GitHub for prefabs and scenes (yet). This repo is mostly just the `.cs` scripts. Instead of cloning the entire repo, follow these steps.

1. Follow [Google's instructions](https://developers.google.com/vr/develop/unity/get-started-android) for creating a Unity VR project.
2. Download the `country_shooter_vr.unitypackage` from the root of this repo.
3. [Import the package](https://docs.unity3d.com/Manual/AssetPackages.html#ImportingPackages) into Unity

## Architecture

In this game, data only flows in one direction and all top-level game data lives in the Store. The Store is the single source of truth. By inspecting the store, you should know everything that's happening within the game.

1. Player Interaction - When a player does something (selects a menu item, clicks pause, etc.) and action is fired.
2. Action - All actions live in the same place, making it really easy to see what the user is able to do. Each action is just an action type (`MENU_CLICKED`) along with the relevant data (which menu item got clicked).

- It verifies the data (does `MENU_CLICKED` have a `string` attached?).
- Then routes it to the relevant Store function.

3. Store function - These are the only functions that are allowed to modify the store!
4. The Store - When any bit of data in the store changes, an event is fired and the subscribers update accordingly.
