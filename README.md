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

```bash
gh release download --repo Victor-Void/Helios \
  --pattern 'helios' \
  --pattern 'Helios-x86_64.AppImage' \
  --pattern '90-helios.rules'

chmod +x helios Helios-x86_64.AppImage

# System-wide install
sudo cp helios /opt/helios/helios
sudo chmod +x /opt/helios/helios
sudo ln -sf /opt/helios/helios /usr/local/bin/helios
sudo cp 90-helios.rules /etc/udev/rules.d/90-helios.rules
sudo udevadm control --reload-rules && sudo udevadm trigger
```

Or run the AppImage directly:

```bash
./Helios-x86_64.AppImage
```

## Uninstall

```bash
sudo rm -f /opt/helios/helios /usr/local/bin/helios
sudo rm -f /etc/udev/rules.d/90-helios.rules
sudo systemctl stop helios-gpu-boot.service 2>/dev/null || true
sudo systemctl disable helios-gpu-boot.service 2>/dev/null || true
sudo rm -f /etc/systemd/system/helios-gpu-boot.service
sudo rm -rf /usr/local/lib/helios /etc/helios
sudo systemctl daemon-reload
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
