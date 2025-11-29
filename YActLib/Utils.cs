using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace YActLib
{
    public class Utils
    {
        public static uint TryParse32(string text)
        {
            uint result = 0;
            if (uint.TryParse(text, out uint Value))
            {
                result = Value;
            }
            return result;
        }

        public static int TryParseS32(string text)
        {
            int result = 0;
            if (int.TryParse(text, out int Value))
            {
                result = Value;
            }
            return result;
        }

        public static ushort TryParse16(string text)
        {
            ushort result = 0;
            if (ushort.TryParse(text, out ushort Value))
            {
                result = Value;
            }
            return result;
        }

        public static short TryParseS16(string text)
        {
            short result = 0;
            if (short.TryParse(text, out short Value))
            {
                result = Value;
            }
            return result;
        }

        public static byte TryParse8(string text)
        {
            byte result = 0;
            if (byte.TryParse(text, out byte Value))
            {
                result = Value;
            }
            return result;
        }

        public static float TryParseFP(string text)
        {
            float result = 0;
            if (float.TryParse(text, out float Value))
            {
                result = Value;
            }
            return result;
        }
        public static string ReadFixedString(BinaryReader reader, int Count)
        {
            byte[] Bytes = reader.ReadBytes(Count);
            return Encoding.ASCII.GetString(Bytes).TrimEnd('\0');
        }
        public static string ReadString(BinaryReader reader)
        {
            var Result = new StringBuilder();

            while (true)
            {
                char chara = reader.ReadChar();
                if (chara == '\0')
                {
                    break;
                }
                else
                {
                    Result.Append(chara);
                }
            }
            return Result.ToString();
        }
        public static void AlignData(BinaryWriter writer, int Alignment)
        {
            long currentpos = writer.BaseStream.Position;
            int padding = (int)(Alignment - currentpos % Alignment) % Alignment;
            if (padding != Alignment)
                writer.Write(new byte[padding]);
        }
        public static void WriteString(BinaryWriter writer, string name, bool Pad)
        {
            writer.Write(Encoding.ASCII.GetBytes(name));
            if (Pad)
            {
                writer.Write((byte)0);
            }
        }
        public static void WriteFixedString(BinaryWriter writer, string s, int length)
        {
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
            writer.Write(bytes);
            for (int i = bytes.Length; i < length; i++)
            {
                writer.Write((byte)0);
            }
        }
        public static void ReadFloatArray(BinaryReader reader, ref float[] Something, int Count)
        {
            Something = new float[Count];
            for (int i = 0; i < Count; i++)
            {
                Something[i] = reader.ReadSingle();
            }
        }
        public static void ReadIntArray(BinaryReader reader, ref int[] Something, int Count)
        {
            Something = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                Something[i] = reader.ReadInt32();
            }
        }
    }
}