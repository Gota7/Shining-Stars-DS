fireflower.exe
del Shining.nds
nds-build.exe build_rules.txt Shining.nds
copy /Y "data/arm9.bin" "../Shining/__ROM__/arm9.bin"
copy /Y "data/overlay9/overlay9_2.bin" "../Shining/__ROM__/Arm9/2.bin"
copy /Y "data/overlay9/overlay9_7.bin" "../Shining/__ROM__/Arm9/7.bin"
copy /Y "data/overlay9/overlay9_100.bin" "../Shining/__ROM__/Arm9/100.bin"
del /s /q "../Editor/build/__ROM__"
