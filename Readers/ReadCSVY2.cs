using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgreEdit.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OgreEdit.Readers
{
    public class ReadCSVY2
    {
        public void ReadCSV(string path,ref CSVDataY2 Data)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                CSVDataY2.Header Header = new CSVDataY2.Header();
                reader.ReadBytes(16);
                Header.Pointer = reader.ReadUInt32();
                Header.Size = reader.ReadUInt32();
                Header.Unk = reader.ReadUInt32();
                Header.ID = ReadFixedString(reader,4);
                Header.Pointer1 = reader.ReadUInt32();
                Header.Count1 = reader.ReadUInt32();
                Header.Pointer2 = reader.ReadUInt32();
                Header.Count2 = reader.ReadUInt32();
                reader.BaseStream.Seek(Header.Pointer1 + 32, SeekOrigin.Begin);
                for (int e  = 0;e<Header.Count1;e++)
                {
                    CSVDataY2.CSVEntry YAct = ReadCSVEntry(reader);
                    Data.Category1.Add(YAct);
                }
                for (int e = 0; e < Header.Count2; e++)
                {
                    CSVDataY2.CSVEntry YAct = ReadCSVEntry(reader);
                    Data.Category2.Add(YAct);
                }
            }
        }
        private CSVDataY2.CSVEntry ReadCSVEntry(BinaryReader reader)
        {
            int i;
            CSVDataY2.CSVEntry YAct = new CSVDataY2.CSVEntry
            {
                Name = ReadFixedString(reader, 16),
                FileID = reader.ReadUInt32(),
                unk1 = reader.ReadUInt32(),
                unk2 = reader.ReadUInt32(),
                unk3 = reader.ReadUInt32(),
                CharaPtr = reader.ReadUInt32(),
                CharaCount = reader.ReadUInt32(),
                ObjectPtr = reader.ReadUInt32(),
                ObjectCount = reader.ReadUInt32(),
                ArmsPtr = reader.ReadUInt32(),
                ArmsCount = reader.ReadUInt32(),
                HactEventPtr = reader.ReadUInt32(),
                HactEventCount = reader.ReadUInt32(),
                Characters = new List<CSVDataY2.Character>(),
                Objects = new List<CSVDataY2.Object>(),
                Arms = new List<CSVDataY2.Arm>(),
                HactEvents = new List<CSVDataY2.IHactEvent>(),
            };
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(YAct.CharaPtr + 32, SeekOrigin.Begin);
            for (i = 0; i < YAct.CharaCount; i++)
            {
                CSVDataY2.Character Chara = new CSVDataY2.Character
                {
                    NamePtr = reader.ReadUInt32(),
                    Unknown1 = reader.ReadBytes(8),
                    DamageCondition = reader.ReadUInt32(),
                    Unknown2 = reader.ReadBytes(16),
                    Data1Ptr = reader.ReadUInt32(),
                    Data1Count = reader.ReadUInt32(),
                    Unknown3 = reader.ReadUInt32(),
                    Data2Ptr = reader.ReadUInt32(),
                    Data2Count = reader.ReadUInt32(),
                    Unknown4 = reader.ReadUInt32(),
                    Data1 = new List<CSVDataY2.UNKDATA>(),
                    Data2 = new List<CSVDataY2.UNKDATA>()
                };
                long returnposc = reader.BaseStream.Position;
                Chara.Name = ReadName(reader, Chara.NamePtr);
                ReadUNK(reader, Chara.Data1Ptr, Chara.Data1Count, ref Chara.Data1);
                ReadUNK(reader, Chara.Data2Ptr, Chara.Data2Count, ref Chara.Data2);
                reader.BaseStream.Seek(returnposc, SeekOrigin.Begin);
                YAct.Characters.Add(Chara);
            }
            reader.BaseStream.Seek(YAct.ObjectPtr + 32, SeekOrigin.Begin);
            for (i = 0; i < YAct.ObjectCount; i++)
            {
                CSVDataY2.Object Object = new CSVDataY2.Object()
                {
                    NamePtr1 = reader.ReadUInt32(),
                    Unknown1 = reader.ReadBytes(8),
                    NamePtr2 = reader.ReadUInt32(),
                    DataPtr = reader.ReadUInt32(),
                    DataCount = reader.ReadUInt32(),
                    Unknown2 = reader.ReadUInt32(),
                    Data = new List<CSVDataY2.UNKDATA>()
                };
                long returnposo = reader.BaseStream.Position;
                Object.Name1 = ReadName(reader, Object.NamePtr1);
                Object.Name2 = ReadName(reader, Object.NamePtr2);
                ReadUNK(reader, Object.DataPtr, Object.DataCount, ref Object.Data);
                reader.BaseStream.Seek(returnposo, SeekOrigin.Begin);
                YAct.Objects.Add(Object);
            }
            reader.BaseStream.Seek(YAct.ArmsPtr + 32, SeekOrigin.Begin);
            for (i = 0; i < YAct.ArmsCount; i++)
            {
                CSVDataY2.Arm Arm = new CSVDataY2.Arm()
                {
                    NamePtr1 = reader.ReadUInt32(),
                    WeaponCount = reader.ReadUInt32(),
                    NamePtr2 = reader.ReadUInt32(),
                    WeaponPointer = reader.ReadUInt32(),
                };
                Arm.Name1 = ReadName(reader, Arm.NamePtr1);
                Arm.Name2 = ReadName(reader, Arm.NamePtr2);
                if (Arm.WeaponPointer != 0)
                {
                    Arm.ArmName = ReadName(reader, Arm.WeaponPointer);
                }
                YAct.Arms.Add(Arm);
            }
            reader.BaseStream.Seek(YAct.HactEventPtr + 32, SeekOrigin.Begin);
            for (i = 0; i < YAct.HactEventCount; i++)
            {
                uint NamePtr = reader.ReadUInt32();
                string Name = ReadName(reader, NamePtr);
                string EffectName = Name.Substring(0, Name.Length - 1);
                switch (EffectName)
                {
                    case "EFFECT_DAMAGE":
                        reader.ReadUInt32();
                        CSVDataY2.EFFECT_DAMAGE Dmg = new CSVDataY2.EFFECT_DAMAGE
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            DamageVal = reader.ReadUInt32(),
                        };
                        reader.ReadBytes(28);
                        YAct.HactEvents.Add(Dmg);
                        break;
                    default:
                        CSVDataY2.UnknownEffect UNK = new CSVDataY2.UnknownEffect
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            UnkData = reader.ReadBytes(36)
                        };
                        YAct.HactEvents.Add(UNK);
                        break;
                }
            }
            
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return YAct;
        }
        private void ReadUNK(BinaryReader reader,uint ptr,uint count,ref List<CSVDataY2.UNKDATA> Data)
        {
            reader.BaseStream.Seek(ptr + 32, SeekOrigin.Begin);
            for (int i = 0;i<count;i++)
            {
                CSVDataY2.UNKDATA Unk = new CSVDataY2.UNKDATA();
                Unk.Data = reader.ReadBytes(36);
                Data.Add(Unk);
            }
            
        }
        private string ReadName(BinaryReader reader,uint ptr)
        {
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(ptr + 32, SeekOrigin.Begin);
            string Name = ReadString(reader);
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return Name;
        }
        private string ReadFixedString(BinaryReader reader,int Count)
        {
            byte[] Bytes = reader.ReadBytes(Count);
            return Encoding.ASCII.GetString(Bytes).TrimEnd('\0');
        }
        private string ReadString(BinaryReader reader)
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
    }
}
