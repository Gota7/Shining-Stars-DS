using System;
using System.IO;
using Newtonsoft.Json;

namespace Ndst {

    // Extension method helper.
    public static class Helper {

        // Read a null terminated string.
        public static string ReadNullTerminated(this BinaryReader r) {
            string ret = "";
            char c = r.ReadChar();
            while (c != 0) {
                ret += c;
                c = r.ReadChar();
            }
            return ret;
        }

        // Read a fixed length string.
        public static string ReadFixedLen(this BinaryReader r, uint len) {
            string ret = "";
            for (uint i = 0; i < len; i++) {
                char c = r.ReadChar();
                if (c != 0) {
                    ret += c;
                } else {
                    return ret;
                }
            }
            return ret;
        }
        
    }

    public sealed class HexStringJsonConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(uint).Equals(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteValue($"0x{value:x}");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            var str = reader.ReadAsString();
            if (str == null || !str.StartsWith("0x"))
                throw new JsonSerializationException();
            return Convert.ToUInt32(str);
        }
        
    }

}