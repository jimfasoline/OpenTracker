# OpenTracker
An open-source cross-platform tracking app for A Link to the Past Randomizer.

This project is intended to provide a tracker with the following features:
- Mystery seed friendly
- Support for most game modes (No Glitches logic only and no Door shuffle yet)
- Autotracking
- Support for Windows, Linux, and MacOS
- Customizable from the GUI

The following is on my roadmap for future updates:
- Stream Capture view
  - Provide a Window view that is friendly to capture with OBS.
  - Consider NDI support for remote streaming.
  - Allow Stream Capture window to be customized from the GUI.
- Color Themes
  - Provide preset color themes for GUI elements.
- Improve package manager support
  - Host an apt and yum repo containing the app package
  - Add package requirements (.NET Core) added to the .deb and .rpm packages
  - Add package to the AUR
  - Add software to the Apple App Store
  - Add Chocolatey package to community repository for Windows
- Add glitched logic options
  - Add Overworld/Major Glitches logic to the tracker
  - This feature will not be taken on until the v32 graph-based logic is made public, as I will be converting my logic to follow Veetorp's lead.

## Getting Started

### Prerequisites

OpenTracker is a .NET Core 3.1 application.  You will be required to install a .NET Core runtime version 3.1 or greater.  You can find it at this link: https://dotnet.microsoft.com/download/dotnet-core

### Windows

- Download the latest .msi release.
- Follow the install wizard to install the application.
- To run, click the shortcut placed on your desktop or in your Start menu.

NOTE: The Windows MSI has been known to not install properly over an existing install some of the time.  If you run into odd issues, try uninstalling and reinstalling the program to see if that resolves it and report the issue here.

### Linux

If you are running a Debian-based (Ubuntu, Mint, PopOS, etc.) distribution, run the following commands:

```
wget https://github.com/trippsc2/OpenTracker/releases/download/<version>/OpenTracker.<version>.deb
sudo apt install ./OpenTracker.<version>.deb
```

If you are running a RHEL-based (Fedora, etc.) distribution, run the following commands:

```
wget https://github.com/trippsc2/OpenTracker/releases/download/<version>/OpenTracker.<version>.rpm
sudo rpm -i ./OpenTracker.<version>.rpm
```

If you are running a different distribution, run the following commands:

```
wget https://github.com/trippsc2/OpenTracker/releases/download/<version>/OpenTracker.<version>.tar.gz
tar xvzf ./OpenTracker.<version>.tar.gz
```

To run the application, run the OpenTracker binary file.  It will be located in the extracted folder, if you used the tarball.  It will be located in your distribution's X application folder (usually /usr/share), if you installed the package.

### MacOS

Download the OpenTracker.<version>.macOS.zip file from the Releases page.  This contains an .app bundle that can be moved to your Applications folder or run directly.

## How it Works

### General

Some notable differences from other map trackers:

- The tracker assumes that fake flippers is always possible (except for entry to Swamp Palace).  This is because there are so many Splash Deletion setups that the logic would be very complicated.  There is room for development on this.
- Game mode settings can be changed by clicking the Gear icon on the top right of the Items panel and modifying the settings listed.
- The Dungeon Item Placement settings change the number of available checks in a dungeon to the number of non-dungeon items applicible for that mode.  (e.g. If Standard Dungeon Item Placement is set, Palace of Darkness has 5 items.  If Maps/Compasses is set, PoD has 7 items.)
- To allow for mystery seed friendliness, all mode settings can be changed without affecting the tracker state.  Modifying Dungeon Item Placement adds or subtracts remaining items from relevant dungeons when the mode is changed.
- In Retro, you can use the generic key in the Items panel to indicate keys in inventory.  Any keys used in a dungeon should be tracked using the dungeon key icons.  The dungeon logic will take the total of the generic keys and dungeon keys, so to give you a better sense of what you can currently do.
- The Rupee quiver is assumed to be had in Retro mode, but is trackable.

### Autotracking

Autotracking support is available currently.  QUSB2SNES or USB2SNES are required to connect the tracker to your game currently.  I don't plan to add support for direct Lua connectors at this time, as QUSB2SNES can facilitate connections to those emulators already.  If there is enough demand, this may change.

To start Autotracking, have QUSB2SNES or USB2SNES open and connected to your game, then select "Autotracker..." from the Tracker menu.  Click Start on the Autotracker window that pops up.

Some notes about Autotracking:

- Most inventory items are autotracked.
- Small keys are not autotracked.
- Big keys are autotracked.
- The dungeon prize type is not autotracked, but whether or not the dungeon prize has been acquired is autotracked.
- All non-dungeon item locations are autotracked.
- Dungeon item locations (including Hyrule Castle, Agahnim's Tower, and Ganon's Tower) are not autotracked.
- Crystal requirements for GT and Ganon Vulnerability are not autotracked.
- Entrance locations in Entrance Shuffle mode are not autotracked.
- Take Any locations are not autotracked.

### Entrance Shuffle

Enabling the Entrance Shuffle mode will do the following.

- Scales down the size of all map locations to allow for the higher density.
- Stops displaying in-door overworld item locations.
- Starts displaying entrances that can be shuffled.

Each entrance can be marked with a number of images.  The images are of all inventory items, Ganon, Agahnim, and text representing each of the dungeon entrances.  When an image is selected, it will be displayed on the map instead of the square.  This allows for you to use your own system for marking entrance locations for future use and be able to see the markings you made at a glance.

The standard colors still display on each location to indicate whether the location is accessible as an entrance given the current items and exits available.  However, all entrance locations (including those colored as inaccessible) can be cleared by right clicking the location, indicating that it is available as an exit.  This will allow you to determine what locations a new exit provides.

## Development

OpenTracker is developed in the Avalonia framework.  In order to develop using Visual Studio, you'll need to install the Avalonia extensions in the Extensions menu.

OpenTracker follows the Model-View-ViewModel (MVVM) pattern as closely as possible.  The solution is separated into 3 projects.

- OpenTracker - This project contains all GUI-specific code.  All View and ViewModel classes, GUI utility classes/types, and GUI-specific Model classes/types will be a part of this project.
- OpenTracker.Models - This contains all GUI non-specific Model classes/types.  This project can be used to port the project to another UI with reliance on any Avalonia libraries.
- OpenTracker.Setup - This is a Visual Studio Setup project used for creating the .MSI for Windows users.

## License

This project is licensed under the MIT license.

## Acknowledgments

- My wife - Thank you for your help testing and providing feedback.  Thank you for putting up with me pouring free time into this project and this old game.
- Serafina Bui - Thank you for your help with the visual presentation.
- Derian Meyer - Thank you for allowing me to bounce ideas off you and for your help with testing and feedback.
- Sara Meyer - Thank you for your help with testing and providing new Rando player feedback.
- Kyle (Kerigyl) - Thank you for your help with testing and providing feedback.
- KatDevsGames - Thank you for developing ConnectorLib.  It was a great reference for my Autotracker connector code.
- EmoSaru - Thank you for creating EmoTracker and providing inspiration for this app.
