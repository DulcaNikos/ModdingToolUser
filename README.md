# ModdingToolUser

## Table of Contents
- [Overview](#overview)
- [Folder Structure](#folder-structure)
- [Editor Window Overview](#editor-window-overview)
  - [First Tab: Mod Creation](#first-tab-mod-creation)
  - [Second Tab: Mod Details](#second-tab-mod-details)
- [Asset Hats](#asset-hats)

## Overview
The ModdingToolUser script allows you to create mods for your Unity projects. Developed with Unity version 2022.3.36.f1, this tool may have compatibility issues with other versions. It simplifies the mod creation process by providing a user-friendly interface within the Unity Editor, where you can assign assets, configure mod details, and build mod bundles. This guide will walk you through the setup, usage, and features of the ModdingToolUser tool to help you effectively manage your modding workflow.

## Folder Structure

Upon setup, you will notice several folders in your Assets directory:

![Screenshot 2024-07-16 120022](https://github.com/user-attachments/assets/da7f310b-a907-48cf-b827-bf50567c4bba)

- **Editor**: Contains the tool's script. This script must remain in the Editor folder to function correctly.
- **Mg3D_Hats**: Contains asset hats used for the object prefabs included for testing.
- **ModsPrefabs**: Contains the object prefabs.
- **Scenes**: Contains an empty Scene. 

In the Unity interface, you'll see a tab called "ModdingToolKit" next to the Window tab. Clicking this will open the editor window:

![Screenshot 2024-07-16 115049](https://github.com/user-attachments/assets/78d08ab6-5453-464e-80fc-6ff534583a90) 

## Editor Window Overview

### First Tab: Mod Creation

![Screenshot 2024-07-16 124756](https://github.com/user-attachments/assets/080061b1-5a82-4a3a-b2ad-55b204adf9f9)

- **Asset Field**: Assign the asset you want to turn into a mod.
- **Asset Name**: Displays the name of the assigned asset.
- **Folder Name**: Click "Select Folder" to choose where to save the mod. If it left blank, a folder named "BundlesFolder" will be created in the project files.
- **Bundle Name**: Enter the desired name for the bundle and its folder.
- **Build Target**: Select the platform for the mod. Only one platform can be chosen at a time.
- **Create Bundle**: Click this button to create the mod.

A JSON file will be created in your mod folder, containing configuration and display information.

### Second Tab: Mod Details
![Screenshot 2024-07-16 120926](https://github.com/user-attachments/assets/e624e1a8-f40b-43a5-9362-c80b992cc8f2)

- **Mod Name**: Enter the name of the mod.
- **Description**: Provide a description of the mod.
- **Author Name**: Enter the author's name or nickname.
- **Version**: Specify the version of the mod.
- **Unity Version**: Indicate the Unity version compatible with the game you want to mod.

## Asset Hats

You can find the asset hats [here](https://assetstore.unity.com/packages/3d/hats-pack-urp-163011).
