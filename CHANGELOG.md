# Helios Changelog

## v1.1.2

- Fixed: the live `asus-nb-wmi` PPT registers (PL1/PL2/fPPT) could stay stuck
  at the 5 W Windows table floor while `asus-armoury` held the real per-mode
  value. AMD firmware enforces `min(all limits)`, so on dual-backend kernels the
  CPU was hard-capped to 5 W (~530 MHz under load) regardless of the selected
  mode, fan curve or thermal policy. Every mode apply now re-raises a
  floor-level register to the firmware value, validated against that
  attribute's `min_value`/`max_value` first.
- Fixed: the NVIDIA Vulkan ICD and EGL vendor files could stay hidden after a
  return to Standard mode when the transition ended anywhere other than the
  live Eco -> Standard path (deferred to reboot, failed re-enable, crash).
  Startup now reconciles the files against the real `dgpu_disable` state
  instead of trusting the previous switch, so Vulkan/EGL apps can no longer
  silently lose the dGPU.
- Diagnostics: no longer reads the write-only `kbd_rgb_mode`/`kbd_rgb_state`
  sysfs nodes (two spurious EIO errors per run), and no longer reports a
  `tmpfiles.d` file the app does not install.
- Version metadata bumped to 1.1.2 so the self-update check compares against
  the actual release instead of the 1.0.0 fallback.

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
