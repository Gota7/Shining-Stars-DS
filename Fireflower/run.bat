fireflower.exe
del Shining.nds
nds-build.exe build_rules.txt Shining.nds
copy /Y "data/arm9.bin" "../Shining/__ROM__/arm9.bin"
del /s /q "../Editor/build/__ROM__"
