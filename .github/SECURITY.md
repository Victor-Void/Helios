# Security Policy

## Reporting a Vulnerability

Helios is a local hardware control utility. It does not transfer data to
networked systems. However, bugs related to hardware access (particularly
the privileged `gpu-helper` binary and the boot-time GPU switching service)
are security-relevant.

To report a vulnerability privately, open an issue on the repository and
mark it as confidential, or contact the maintainer directly.

## Supported Versions

Only the latest release is actively supported. Older releases or
development branches receive no security updates.

## Privileged Components

- `gpu-helper` — setuid binary; required for GPU PCI-level operations.
  Conforms to a strict allow-list of operations.
- `helios-gpu-boot.service` — early-boot systemd oneshot; applies
  pending GPU mode changes before the display manager starts.
- udev rules — grant `plugdev` group access to ASUS WMI sysfs nodes.
