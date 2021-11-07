using System.Collections.Generic;
using System.IO;

namespace Ndst {

    // Folder.
    public class Folder {
        public string Name = "";
        public ushort Id;
        public ushort FirstFileId;
        public List<Folder> Folders = new List<Folder>();
        public List<File> Files = new List<File>();

        public override string ToString() => "0x" + Id.ToString("X") + " - " + Name;
    }

    // File.
    public class File {
        public string Name;
        public ushort Id;
        public byte[] Data;

        public override string ToString() => "0x" + Id.ToString("X") + " - " + Name;
    }

    // NDS Filesystem.
    // ID Convention: IDs start at 0, and first name the ARM9 overlays, then the ARM7 overlays. Then they go for files in the root folder, then recursively go through the subfolders.
    // Folder IDs start at 0 with the root, and recursively increase for each sub folder.
    // Both IDs are alphabetical.
    public class Filesystem : Folder {

        // Create a new filesystem.
        public Filesystem(BinaryReader r, uint fntOff, uint fntSize, uint fatOff, uint fatSize) {

            // Read a folder.
            Folder ReadFolder(ushort id) {

                // Get folder info.
                r.BaseStream.Position = fntOff + 8 * id;
                Folder ret = new Folder();
                ret.Id = id;
                uint off = r.ReadUInt32();
                ret.FirstFileId = r.ReadUInt16();
                r.ReadUInt16(); // Parent ID. Root folder is total number of folders.
                r.BaseStream.Position = fntOff + off;

                // Data.
                byte control = r.ReadByte();
                ushort currId = ret.FirstFileId;
                while (control != 0) {
                    bool folder = (control & 0x80) != 0;
                    control &= 0x7F;
                    string name = new string(r.ReadChars(control));
                    if (folder) {
                        ushort subId = (ushort)(r.ReadUInt16() & 0xFFF);
                        long bakPos = r.BaseStream.Position;
                        Folder newFolder = ReadFolder(subId);
                        r.BaseStream.Position = bakPos;
                        newFolder.Name = name;
                        ret.Folders.Add(newFolder);
                    } else {
                        long bakPos = r.BaseStream.Position;
                        r.BaseStream.Position = fatOff + currId * 8;
                        uint startOff = r.ReadUInt32();
                        uint endOff = r.ReadUInt32();
                        r.BaseStream.Position = startOff;
                        byte[] fileData = r.ReadBytes((int)(endOff - startOff));
                        r.BaseStream.Position = bakPos;
                        ret.Files.Add(new File() { Name = name, Id = currId++, Data = fileData });
                    }
                    control = r.ReadByte();
                }
                
                // Finish.
                return ret;

            }

            // Read root.
            var root = ReadFolder(0);
            Folders = root.Folders;
            Files = root.Files;
            FirstFileId = root.FirstFileId;

        }

    }

}