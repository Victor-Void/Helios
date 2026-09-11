# Helios Changelog

## v1.1.0

- English-only build: removed 37 translations and the runtime language switcher
  (smaller binary, no translation maintenance)
- udev rules: always exit successfully when sysfs nodes are absent
  (stops non-zero exit-status noise in the journal)
- Power limits: cache only successful writes so a rejected PPT write stays
  retryable instead of being deduplicated away until restart
- Power limits: made the written-value cache thread-safe (FW-attr watcher race)
- GPU temperature: drop sensor glitch readings at or above 125°C
- Display: skip the modeset when the panel is already at the target refresh rate
- Touchpad-toggle detection: match model prefix instead of substring
- Added XG Mobile PCIe power-management hint in Extra Settings

## v1.0.0 — Initial Release

- Performance mode switching (Silent / Balanced / Turbo) via asusd D-Bus
- 8-point custom fan curve editor for CPU and GPU fans
- GPU mode control (Eco / Standard / Ultimate / Optimized)
- Battery charge limit control
- Keyboard backlight brightness
- Aura RGB keyboard mode and colour
- Live CPU / GPU temperature and fan speed monitoring
- Display refresh rate control and panel overdrive
- Fn-Lock remapping and custom hotkeys
- System tray with quick profile switching
- Automatic mode switching on AC / battery
- Material You theming
- Material Symbols icons
- Native AOT single-binary deployment
- AppImage packaging
