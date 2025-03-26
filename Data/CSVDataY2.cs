using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgreEdit.Data
{
    public class CSVDataY2
    {
        public List<CSVEntry> Category1 = new List<CSVEntry>();
        public List<CSVEntry> Category2 = new List<CSVEntry>();
        public interface IHactEvent
        {
            public uint NamePtr { get; set; }
            public string Name { get; set; }
        }
        public struct Header
        {
            public uint Pointer;
            public uint Size;
            public uint Unk;
            public string ID;
            public uint Pointer1;
            public uint Count1;
            public uint Pointer2;
            public uint Count2;
        }
        public struct CSVEntry
        {
            public string Name;
            public uint FileID;
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public uint CharaPtr;
            public uint CharaCount;
            public uint ObjectPtr;
            public uint ObjectCount;
            public uint ArmsPtr;
            public uint ArmsCount;
            public uint HactEventPtr;
            public uint HactEventCount;
            public List<Character> Characters;
            public List<Object> Objects;
            public List<Arm> Arms;
            public List<IHactEvent> HactEvents;
        }
        public struct Character
        {
            public uint NamePtr;
            public string Name;
            public byte[] Unknown1;
            public uint DamageCondition;
            public byte[] Unknown2;
            public uint Data1Ptr;
            public uint Data1Count;
            public uint Unknown3;
            public uint Data2Ptr;
            public uint Data2Count;
            public uint Unknown4;
            public List<UNKDATA> Data1;
            public List<UNKDATA> Data2;
        }
        public struct Object
        {
            public uint NamePtr1;
            public string Name1;
            public byte[] Unknown1;
            public uint NamePtr2;
            public string Name2;
            public uint DataPtr;
            public uint DataCount;
            public uint Unknown2;
            public List<UNKDATA> Data;
        }
        public struct Arm
        {
            public uint NamePtr1;
            public uint WeaponCount;
            public string Name1;
            public uint NamePtr2;
            public string Name2;
            public uint WeaponPointer;
            public string ArmName;
        }
        public struct EFFECT_DAMAGE : IHactEvent
        {
            public uint NamePtr { get; set; }
            public string Name { get; set; }
            public uint DamageVal;

        }
        public struct EFFECT_RENDA : IHactEvent
        {
            public uint NamePtr { get; set; }
            public string Name { get; set; }
            public uint Unknown1;
            public uint Unknown2;
            public uint Unknown3;
            public uint Button;
        }
        public struct UnknownEffect : IHactEvent
        {
            public uint NamePtr { get; set; }
            public string Name { get; set; }
            public byte[] UnkData;
        }
        public struct UNKDATA
        {
            public byte[] Data;
        }
    }
}
