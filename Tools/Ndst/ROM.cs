using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Ndst {
    
    // DS Unit Code.
    public enum UnitCode {
        NDS,
        Reserved,
        NDS_DSI,
        DSI
    }
    // NDS Rom.
    public class ROM {
        public string GameTitle;
        public string GameCode;
        public string MakerCode;
        public UnitCode UnitCode;
        public byte EncryptionSeedSelect;
        public byte DeviceCapacity;
        public ushort Revision;
        public byte Version;
        public byte Flags;
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Arm9EntryAddress;
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Arm9LoadAddress;
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Arm7EntryAddress;
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Arm7LoadAddress;
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint NormalCardControlRegisterSettings;
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint SecureCardControlRegisterSettings;
        public ushort SecureTransferTimeout;
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Arm9Autoload;
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Arm7Autoload;
        public ulong SecureDisable;
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint HeaderSize;
        [JsonIgnore]
        public byte[] NintendoLogo;
        [JsonIgnore]
        public Filesystem Filesystem;
        [JsonIgnore]
        public List<Overlay> Arm9Overlays = new List<Overlay>();
        [JsonIgnore]
        public List<Overlay> Arm7Overlays = new List<Overlay>();
        [JsonIgnore]
        public byte[] Arm9;
        [JsonIgnore]
        public byte[] Arm7;
        [JsonIgnore]
        public byte[] Banner;

        // Banner lengths.
        readonly Dictionary<ushort, uint> BANNER_LENGTHS = new Dictionary<ushort, uint>() {
            { 0x0001, 0x840 },
            { 0x0002, 0x940 },
            { 0x0003, 0x1240 },
            { 0x0103, 0x23C0 },
        };

        // Create a new ROM.
        public ROM(string filePath) {

            // Read the file.
            using (Stream s = new FileStream(filePath, FileMode.Open)) {

                // Read general data.
                BinaryReader r = new BinaryReader(s);
                GameTitle = r.ReadFixedLen(0xC);
                GameCode = r.ReadFixedLen(0x4);
                MakerCode = r.ReadFixedLen(0x2);
                UnitCode = (UnitCode)r.ReadByte();
                EncryptionSeedSelect = r.ReadByte();
                DeviceCapacity = r.ReadByte();
                r.ReadBytes(7);
                Revision = r.ReadUInt16();
                Version = r.ReadByte();
                Flags = r.ReadByte();
                uint arm9Offset = r.ReadUInt32();
                Arm9EntryAddress = r.ReadUInt32();
                Arm9LoadAddress = r.ReadUInt32();
                uint arm9Size = r.ReadUInt32();
                uint arm7Offset = r.ReadUInt32();
                Arm7EntryAddress = r.ReadUInt32();
                Arm7LoadAddress = r.ReadUInt32();
                uint arm7Size = r.ReadUInt32();
                uint fntOffset = r.ReadUInt32();
                uint fntSize = r.ReadUInt32();
                uint fatOffset = r.ReadUInt32();
                uint fatSize = r.ReadUInt32();
                uint arm9OverlayOffset = r.ReadUInt32();
                uint arm9OverlayLength = r.ReadUInt32();
                uint arm7OverlayOffset = r.ReadUInt32();
                uint arm7OverlayLength = r.ReadUInt32();
                NormalCardControlRegisterSettings = r.ReadUInt32();
                SecureCardControlRegisterSettings = r.ReadUInt32();
                uint iconBannerOffset = r.ReadUInt32();
                r.ReadUInt16(); // Secure area CRC.
                SecureTransferTimeout = r.ReadUInt16();
                Arm9Autoload = r.ReadUInt32();
                Arm7Autoload = r.ReadUInt32();
                SecureDisable = r.ReadUInt64();
                uint romSize = r.ReadUInt32() + 0x88;
                HeaderSize = r.ReadUInt32();

                // Read logo.
                r.BaseStream.Position = 0xC0;
                NintendoLogo = r.ReadBytes(0x9C);
                r.ReadUInt16(); // Nintendo logo CRC. It is literally just the CRC of the logo.
                r.ReadUInt16(); // Header CRC. It is the CRC of everything up to this point (0 to 0x15E).

                // Get banner.
                r.BaseStream.Position = iconBannerOffset;
                ushort bannerVersion = r.ReadUInt16();
                r.BaseStream.Position -= 2;
                Banner = r.ReadBytes((int)BANNER_LENGTHS[bannerVersion]);

                // Code binaries.
                r.BaseStream.Position = arm9Offset;
                Arm9 = r.ReadBytes((int)arm9Size);
                r.BaseStream.Position = arm7Offset;
                Arm7 = r.ReadBytes((int)arm7Size);

                // Get overlays.
                Arm9Overlays = Overlay.LoadOverlays(r, arm9OverlayOffset, arm9OverlayLength);
                Arm7Overlays = Overlay.LoadOverlays(r, arm7OverlayOffset, arm7OverlayLength);
                for (int i = 0; i < Arm9Overlays.Count; i++) {
                    r.BaseStream.Position = fatOffset + Arm9Overlays[i].FileId * 8;
                    uint fileStart = r.ReadUInt32();
                    uint fileEnd = r.ReadUInt32();
                    r.BaseStream.Position = fileStart;
                    Arm9Overlays[i].Data = r.ReadBytes((int)(fileEnd - fileStart));
                }
                for (int i = 0; i < Arm7Overlays.Count; i++) {
                    r.BaseStream.Position = fatOffset + Arm7Overlays[i].FileId * 8;
                    uint fileStart = r.ReadUInt32();
                    uint fileEnd = r.ReadUInt32();
                    r.BaseStream.Position = fileStart;
                    Arm7Overlays[i].Data = r.ReadBytes((int)(fileEnd - fileStart));
                }

                // Read filesystem.
                Filesystem = new Filesystem(r, fntOffset, fntSize, fatOffset, fatSize);

            }

        }

        // Save the ROM.
        public void Save(string filePath) {
            
        }

        // Calculate the CRC.
        public ushort CalcCRC(byte[] bytes) {
            uint crc = 0xFFFF;
            uint[] val = {0xC0C1, 0xC181, 0xC301, 0xC601, 0xCC01, 0xD801, 0xF001, 0xA001};
            for (uint i = 0; i < bytes.Length; i++) {
                crc ^= bytes[i];
                for (int j = 0; j < 8; j++) {
                    bool carry = ((crc & 0x1) == 0x1);
                    crc >>= 1;
                    if (carry)
                        crc ^= (val[j] << (7 - j));
                }
            }
            return (ushort)crc;
        }

        // Extract the filesystem. So in order for this to work, an "original filesystem" will be extracted, and a "patch filesystem" will have all the edits and file additions.
        public void Extract(string destFolder) {

            // Create folder if needed.
            Directory.CreateDirectory(destFolder);

            // Extract ROM info.
            Directory.CreateDirectory(destFolder + "/" + "__ROM__");
            System.IO.File.WriteAllText(destFolder + "/" + "__ROM__" + "/" + "header.json", JsonConvert.SerializeObject(this, Formatting.Indented));
            System.IO.File.WriteAllBytes(destFolder + "/" + "__ROM__" + "/" + "nintendoLogo.bin", NintendoLogo);
            System.IO.File.WriteAllBytes(destFolder + "/" + "__ROM__" + "/" + "banner.bin", Banner);

            // Extract code.
            Directory.CreateDirectory(destFolder + "/" + "__ROM__");
            System.IO.File.WriteAllBytes(destFolder + "/" + "__ROM__" + "/" + "arm9.bin", Arm9);
            System.IO.File.WriteAllBytes(destFolder + "/" + "__ROM__" + "/" + "arm7.bin", Arm7);
            System.IO.File.WriteAllText(destFolder + "/" + "__ROM__" + "/" + "arm9Overlays.json", JsonConvert.SerializeObject(Arm9Overlays, Formatting.Indented));
            System.IO.File.WriteAllText(destFolder + "/" + "__ROM__" + "/" + "arm7Overlays.json", JsonConvert.SerializeObject(Arm7Overlays, Formatting.Indented));
            Directory.CreateDirectory(destFolder + "/" + "__ROM__" + "/" + "Arm9");
            Directory.CreateDirectory(destFolder + "/" + "__ROM__" + "/" + "Arm7");
            int cnt = 0;
            foreach (var o in Arm9Overlays) {
                System.IO.File.WriteAllBytes(destFolder + "/" + "__ROM__" + "/" + "Arm9" + "/" + cnt++ + ".bin", o.Data);
            }
            cnt = 0;
            foreach (var o in Arm7Overlays) {
                System.IO.File.WriteAllBytes(destFolder + "/" + "__ROM__" + "/" + "Arm7" + "/" + cnt++ + ".bin", o.Data);
            }
            // TODO!!!

            // Extract files.
            void ExtractFiles(string path, Folder folder) {
                Directory.CreateDirectory(path);
                foreach (var f in folder.Folders) {
                    ExtractFiles(path + "/" + f.Name, f);
                }
                foreach (var f in folder.Files) {
                    System.IO.File.WriteAllBytes(path + "/" + f.Name, f.Data);
                }
            }
            ExtractFiles(destFolder, Filesystem);

        }

        // Pack a ROM.
        public void Pack(string srcFolder, string patchFolder) {

        }

    }

}