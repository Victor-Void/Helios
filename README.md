# Helios

Lightweight ASUS laptop control utility for Linux.

Performance modes, custom fan curves, GPU mode switching, battery charge
limits, Aura RGB, keyboard backlight, display control, and more —
running as a single, self-contained binary.

Built for ASUS ROG, TUF, Vivobook, Zenbook, and ProArt laptops.

## Features

- **Performance modes** — Silent, Balanced, Turbo (via asusd D-Bus)
- **Fan curves** — 8-point custom fan curve editor for CPU and GPU fans
- **GPU modes** — Eco, Standard, Ultimate, Optimized (auto-switching)
- **Battery** — charge limit control and health preservation
- **Keyboard** — backlight brightness, Aura RGB mode and colour
- **Display** — refresh rate switching, panel overdrive, MiniLED multi-zone
- **Monitoring** — live CPU / GPU temperature and fan speed readouts
- **Automation** — auto mode switching on AC / battery
- **Hotkeys** — Fn-Lock remapping, custom keybinds, brightness shortcuts
- **System tray** — quick profile switching without opening the window
- **Audio** — microphone noise reduction, parametric EQ, reverb (PipeWire)
- **Handheld** — ROG Ally controller bindings and game-mode support

## Install

### Quick install (recommended)

```bash
curl -sL https://raw.githubusercontent.com/Victor-Void/Helios/master/install/install.sh | sudo bash
```

### AppImage

Download `Helios-x86_64.AppImage` from the [latest release](https://github.com/Victor-Void/Helios/releases/latest):

```bash
chmod +x Helios-x86_64.AppImage
./Helios-x86_64.AppImage
```

### Manual

```bash
curl -sL https://github.com/Victor-Void/Helios/releases/latest/download/helios -o helios
chmod +x helios
sudo ./install/install.sh  # installs udev rules for hardware access
./helios
```

## Uninstall

```bash
curl -sL https://raw.githubusercontent.com/Victor-Void/Helios/master/install/install.sh | sudo bash -s -- --uninstall
```

## Build

Requires [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).

```bash
# Native AOT single-binary build
./build.sh

# AppImage
./build-appimage.sh
```

## Requirements

- Linux kernel 6.2+
- `asusd` (asusctl) daemon for performance profiles and fan curves
- Hardware with ASUS WMI / asus-armoury firmware interface

## License

[GPL-3.0](LICENSE)

Helios is a modified version of g-helper-linux (utajum/g-helper-linux),
which is a modified version of g-helper (seerge/g-helper).

## Credits

- [g-helper](https://github.com/seerge/g-helper) by seerge — original Windows tool
- [g-helper-linux](https://github.com/utajum/g-helper-linux) by utajum — Linux port foundation
- [asusctl](https://gitlab.com/asus-linux/asusctl) — asusd daemon for hardware interface
- Material Design Icons by Google — icon glyphs
