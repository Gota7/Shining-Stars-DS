import code
import json
import os
import shutil
import struct

# Variables:
ov_num = 155            # Change this to the overlay number!
code_addr = 0x23ec400   # Change this address for whatever you need for the overlay!

# Delete old newcode.
if os.path.exists("newcode.bin"):
    os.remove("newcode.bin")

# Run make.
os.system("make CODEADDR=" + hex(code_addr))

# Make sure code was generated.
if not os.path.exists("newcode.bin"):
    print("ERR: No new code generated!")
    exit(0)

# Find location of init symbol.
init_loc = -1
symbols = open("newcode.sym", "r")
lines = symbols.readlines()
symbols.close()
for line in lines:
    if "_Z4initv" == line.split(' ')[-1].strip(): # Space to ensure authenticity.
        init_loc = int(line[0:8], 16)
        break
if (init_loc == -1):
    print("ERR: Can not find init function!")
    exit(0)

# Write location.
bin = open("newcode.bin", "ab")
static_init_start = bin.tell() + code_addr
bin.write(struct.pack("<I", init_loc))
ram_size = static_init_end = bin.tell()
static_init_end += code_addr
bin.close()

# Copy overlay and remove ARM9 folder.
shutil.copy("newcode.bin", "../../Shining/__ROM__/Arm9/" + str(ov_num) + ".bin")
if os.path.exists("../../Editor/build/__ROM__/Arm9"):
    shutil.rmtree("../../Editor/build/__ROM__/Arm9")

# Get arm9Overlays.json.
overlaysFile = open("../../Shining/__ROM__/arm9Overlays.json", "r")
overlays = json.loads(overlaysFile.read())
overlaysFile.close()

# Find the target overlay and modify vars.
for overlay in overlays:
    if overlay["Id"] == ov_num:
        overlay["RAMAddress"] = hex(code_addr)
        overlay["RAMSize"] = hex(ram_size)
        overlay["BSSSize"] = hex(0)
        overlay["StaticInitStart"] = hex(static_init_start)
        overlay["StaticInitEnd"] = hex(static_init_end)
        break

# Set arm9Overlays.json.
overlaysFile = open("../../Shining/__ROM__/arm9Overlays.json", "w")
overlaysFile.write(json.dumps(overlays, indent=2))
overlaysFile.close()