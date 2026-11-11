#!/usr/bin/env bash
# apt-install.sh - install build packages on GitHub-hosted Ubuntu runners.
#
# Why this exists: GitHub's runners intermittently fail to resolve
# archive.ubuntu.com / security.ubuntu.com / azure.archive.ubuntu.com
# ("Temporary failure resolving ..."), which makes a plain
# `sudo apt-get update && sudo apt-get install ...` step fail and kills the
# whole release build. This happened three times in a row for v1.1.2 while
# other hostnames (e.g. builds.dotnet.microsoft.com) resolved fine.
#
# Strategy: probe DNS for the Ubuntu archives, and if they do not resolve,
# rewrite the apt sources to a mirror that does before updating.
#
# Usage: bash .github/scripts/apt-install.sh <package> [package...]
set -euo pipefail

if [[ $# -eq 0 ]]; then
  echo "usage: $0 <package> [package...]" >&2
  exit 2
fi

probe() { getent hosts "$1" >/dev/null 2>&1; }

echo "== DNS probe =="
for mirror in archive.ubuntu.com security.ubuntu.com azure.archive.ubuntu.com \
              mirrors.kernel.org mirror.rackspace.com mirrors.edge.kernel.org; do
  if probe "$mirror"; then echo "  ok:   $mirror"; else echo "  FAIL: $mirror"; fi
done

if ! probe archive.ubuntu.com || ! probe security.ubuntu.com; then
  FALLBACK=""
  for candidate in mirrors.kernel.org mirror.rackspace.com mirrors.edge.kernel.org; do
    if probe "$candidate"; then FALLBACK="$candidate"; break; fi
  done
  if [[ -n "$FALLBACK" ]]; then
    echo "Ubuntu archives unreachable - rewriting apt sources to http://$FALLBACK/ubuntu"
    sudo sed -i -E \
      "s#https?://(azure\.)?archive\.ubuntu\.com/ubuntu#http://$FALLBACK/ubuntu#g; \
       s#https?://security\.ubuntu\.com/ubuntu#http://$FALLBACK/ubuntu#g" \
      /etc/apt/sources.list /etc/apt/sources.list.d/*.list 2>/dev/null || true
    sudo sed -i -E "s#^URIs: .*#URIs: http://$FALLBACK/ubuntu#" \
      /etc/apt/sources.list.d/*.sources 2>/dev/null || true
  else
    echo "No mirror resolved; trying the default sources anyway" >&2
  fi
fi

sudo apt-get update -o Acquire::Retries=5
sudo apt-get install -y --no-install-recommends "$@"
