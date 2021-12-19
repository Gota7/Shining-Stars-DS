# Shining-Stars-DS
Source code, tools, and assets required to build Shining Stars DS. The ASM build system is hacky atm, but Ndst will eventually support ASM patching but until then there are some extra steps described below to recompile the code (optional).

## Assets
Nintendo's assets and code for SM64DS are not included in this repo. You need a legally dumped european SM64DS ROM in order to build and play the Shining Stars DS ROM.

## Build Instructions
1. Open an SM64DS EUR ROM with the provided SM64DSe (in Editor folder) and extract it to a folder.
2. Use SM64DSe to open the extracted ROM with the extracted folder as the source/ROM folder, `Patch` folder as the patch folder, and the `Convert` folder as the conversion folder.
3. Hit the build button to build the ROM.

## ARM9 Instructions
1. Install DevKitPro.
2. Open a clean EUR ROM with SM64DSe, patch it, and enable NSMBe patching.
3. Place the ROM in `Source/ARM9` and open it with the NSMBe in `Source/ARM9/1Patcher`.
4. Run `make clean` then `make insert`, and after that extract ARM9 and overlay 2.
5. Place the ARM9 in `Patch/__ROM__/` and overlay 2 in `Patch/__ROM__/Arm9/2.bin`.

## MOM Overlay Instructions
1. Install DevKitPro.
2. Open the code compiler in SM64DSe by navigating to ASM Hacking->Compilation->Code Compiler.
3. Set the mode to Overlay in patch settings, and put 155 as the Id.
4. For the folder, select `Source/Overlay`.
5. Hit `Clean`, then `Compile!`.

## Credits
* Gota7 - Helped with everything.
* PieOrDie1 - Made the original Shining Stars assets and romhack for N64.
* Pants64 - ASM hacks.
* Splatterboy - ASM hacks.
* Josh65536 - ASM hacks.
