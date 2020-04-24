# Mount &amp; Blade II: Bannerlord - Yet Another Party Organiser (YAPO)

[Nexus Link](https://www.nexusmods.com/mountandblade2bannerlord/mods/539)

### Another party organisation mod to add to your ever expanding collection!

**IMPORTANT** If you used a version prior to 1.2.0, UIExtenderLibModule is a deprecated mod. Make sure you remove this mod from your load order. The library is included with this mod.

**IMPORTANT** To ensure compatibility between Harmony versions across different modules, it is advised to load ModLib as the first Module after the official modules. If this is not done, the game may error on startup as an older version of harmony might be loaded first

## Dependencies

| Dependency | Mandatory | Notes |
|------------|:---------:|-------|
| [ModLib](https://www.nexusmods.com/mountandblade2bannerlord/mods/592) | ✓ | Used to provide settings and other goodies |
| [Save Missing Module Fix](https://www.nexusmods.com/mountandblade2bannerlord/mods/282) | ✗ | Recommended as it ensures saves can be loaded when you unload this mod having saved a game with it enabled |

Make sure that the dependent mods are enabled and listed above this mod within to launcher.

## Features

### Sorting

- Separate Main Party and Other Party sorting
- Sorting modes:
  - Alphabetical (Name)
  - Type (Infantry, Ranged, Cavalry etc)
  - Group (Formation group)
  - Tier
  - Culture
  - Count
- Two sorting modes can be selected through two drop downs
  - Sorting is treated as Sort By -> Then By
  - Button to set opposite sorting direction between the Sort By -> Then By Modes, to allow more granular sorting rules
- Button to place upgradable troops at the top of the list
- Buttons will disable themselves if functionality is not available (e.g. cannot set opposite sorting if no Then By mode is selected)
- Sorting configuration is saved to your save file

### Party Actions

- Upgrade single-path upgradable troops
- Recruit recruitable prisoners - sort order is respected, party limit is respected but can be overridden by holding CTRL
- Buttons will disable themselves if functionality is not available (e.g. no troops to upgrade)

### Hotkeys

- LCTRL + LSHIFT + A - Sort Ascending (Both Sides)
- LCTRL + LSHIFT + D - Sort Descending (Both Sides)
- U - Upgrade Troops
- R - Recruit Prisoners

### Options

Options can be found via the main menu option:
`Mod Options -> Yet Another Party Organiser`

| Option | Default | Description |
|--------|---------|-------------|
| Enable Auto Sorting | On | Sorts troops and prisoners upon opening the party screen |

### Planned Features

- Anything that fits within the scope of party organisation that anyone may suggest! Feel free to create feature requests!
- Configurable group sort order (sort cavalry after archers for example)
- Configurable upgrade rules
- Handling of multi-path upgradable troops
- Configurable main party prisoner and other party troops/prisoner (after battle) recruitment rules

## Contributing

This mod is open source, so please feel free to explore it. It isn't perfect but we are working on it.

If you want to help don't hesitate to get into contact and have a look at our [contribution guide](https://github.com/tbeswick96/BannerlordYetAnotherPartyOrganiser/wiki/Contributing).
