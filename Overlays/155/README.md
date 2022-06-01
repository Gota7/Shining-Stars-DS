# Build Instructions
1. Run `run.sh`.
2. In the `Shining/__ROM__/Arm9/155.bin`, make a note of the filesize (before adding 0s) and add 4 0s at the end of the file.
3. In `Shining/__ROM__/arm9Overlays.json` set the `RAMSize` to that filesize + 4, `StaticInitStart` to the `RAMAddress` plus that earlier filesize from 2, and `StaticInitEnd` to `StaticInitStart + 4`.