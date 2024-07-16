# ModdingToolUser

## Overview
The ModdingToolUser script allows you to create mods for your Unity projects. Developed with Unity version 2022.3.36.f1, this tool may have compatibility issues with other versions. It simplifies the mod creation process by providing a user-friendly interface within the Unity Editor, where you can assign assets, configure mod details, and build mod bundles. This guide will walk you through the setup, usage, and features of the ModdingToolUser tool to help you effectively manage your modding workflow.

First of all you will notice this inside your Assets folder.

![Screenshot 2024-07-16 120022](https://github.com/user-attachments/assets/da7f310b-a907-48cf-b827-bf50567c4bba)

- Inside editor folder is the script of the tool. The script must be inside the Editor folder otherwise will not work.
- The Mg3D_Hats is a folder that contains asset hats that im using for the object prefabs that i've prepared for you to test the tool.
- ModsPreafbs contains the object prefabs.
- And the Scenes folder contains a empty Scene. 

Above next to the Window tab you will notice a tab called ModdingToolKit if you press it will open an editor window.
![Screenshot 2024-07-16 115049](https://github.com/user-attachments/assets/78d08ab6-5453-464e-80fc-6ff534583a90) 

### First tab of the editor window 
![Screenshot 2024-07-16 124756](https://github.com/user-attachments/assets/080061b1-5a82-4a3a-b2ad-55b204adf9f9)

- For the first field you will assign the asset that you want to make it mod.
- Bellow at the AssetName will be displayed the name of the asset you have assigned.
- For the Folder Name you can click on Select Folder and chooce where to save the mod or you can do nothing and a folder called BundlesFolder will be created inside the project files.
- On the Bundle Name you can type the name that you want the bundle and the bundle folder to be called.
- On Build Target Dropdown you can chooce on which platform the mod will work, you can chooce only one at the time.
- And for Create Bundle button when you press it , it will create the mod.

A json file within your mod folder will be created and will contain information that will be used for configuration and for displaying.

### Second tab of the editor window 
![Screenshot 2024-07-16 120926](https://github.com/user-attachments/assets/e624e1a8-f40b-43a5-9362-c80b992cc8f2)

- The name of the mod.
- A description about the mod.
- The author name or a nickname.
- The version of the mod.
- And the unity version that the game you want to add the mod is.

You can find the asset hats [here](https://assetstore.unity.com/packages/3d/hats-pack-urp-163011).
