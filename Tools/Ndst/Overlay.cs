using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Ndst {

    // An overlay.
    public class Overlay {

        public uint Id { get; set; }

        [JsonIgnore]
        public byte[] Data { get; set; }

        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint RAMAddress { get; set; }

        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint BSSSize { get; set; }

        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint StaticInitStart { get; set; }

        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint StaticInitEnd { get; set; }

        [JsonIgnore]
        public ushort FileId { get; set; }
        
        [JsonConverter(typeof(HexStringJsonConverter))]
        public uint Flags { get; set; } // 0b1 means compressed.

        // Load overlays from a table.
        public static List<Overlay> LoadOverlays(BinaryReader r, uint off, uint len) {
            List<Overlay> ret = new List<Overlay>();
            uint numOverlays = len / 0x20;
            r.BaseStream.Position = off;
            for (uint i = 0; i < numOverlays; i++) {
                Overlay o = new Overlay();
                o.Id = r.ReadUInt32();
                o.RAMAddress = r.ReadUInt32();
                r.ReadUInt32(); // RAM size.
                o.BSSSize = r.ReadUInt32();
                o.StaticInitStart = r.ReadUInt32();
                o.StaticInitEnd = r.ReadUInt32();
                o.FileId = r.ReadUInt16();
                r.ReadUInt16();
                o.Flags = r.ReadUInt32();
                ret.Add(o);
            }
            return ret;
        }
        
    }

}