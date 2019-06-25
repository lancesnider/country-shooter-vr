# Country Shooter VR for Daydream

### An educational geography game for Daydream VR.

The gameplay is simple - When a country name shows up on the screen, find it and shoot it. You'll be scored on speed and accuracy.

## Getting Started

Some horror stories have convinced me not to use GitHub for prefabs and scenes (yet). This repo is mostly just the `.cs` scripts. Instead of cloning the entire repo, follow these steps.

1. Follow [Google's instructions](https://developers.google.com/vr/develop/unity/get-started-android) for creating a Unity VR project.
2. Download the `country_shooter_vr.unitypackage` from the root of this repo.
3. [Import the package](https://docs.unity3d.com/Manual/AssetPackages.html#ImportingPackages) into Unity

## Architecture

For top-level data (game state, score, currently selected country, etc.), I've chosed a one-direction data flow.

1. All top-level data lives within the Store. This is the single source of truth!
2. When data within the store is updated, events are fired, informing any subscribed element within the scene. For example, if `isPaused` changes to `true`, the gamer timer will know and will stop counting.
3. When something happens within the game (user clicks pause, a bullet hits a country, etc.) an Action is fired. An action is just an action type along with some data (`Action.PAUSE_CLICKED`, `true`). The verifies the data (does `MENU_CLICKED` have a `string` attached?), then routes it to the relevant Store function. All actions live in the same place, making it really easy to see what your player is able to do.
4. The action calls a special function (a Store Function), which is the only thing that's allowed to update the store. No more hunting around to figure out which script is updating data. Once the store function is called, go back to step 1.
