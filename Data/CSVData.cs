using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OgreEdit.Data.YactDataY2;

namespace OgreEdit.Data
{
    public class CSVData
    {
        public List<YAct> Category1 = new List<YAct>();
        public List<YAct> Category2 = new List<YAct>();
        public struct CategoryFileHeader
        {
            public uint Pointer;
            public uint Size;
            public uint Index;
            public string Identifier;
        }
        public struct CategoryYActInfo
        {
            public uint Pointer;
            public uint Count;
        }
        public struct YAct
        {
            public byte[] UnkHdr;
            public uint FileID;
            public uint Size;
            public uint CamInfoPtr;
            public uint CamInfoCount;
            public uint PlayerPointer;
            public uint EnemiesPointer;
            public uint EnemiesCount;
            public uint ObjectsPointer;
            public uint ObjectsCount;
            public uint ArmsPointer;
            public uint ArmsCount;
            public uint UNK4Pointer;
            public uint UNK4Count;
            public uint UNK5Pointer;
            public uint UNK5Count;
            public List<CameraInfo> CamInfos;
            public Player Player;
            public List<Enemy> Enemies;
            public List<Object> Objects;
            public List<Arm> Arms;
            public List<Unk4> UnknownC4;
        }
        public struct Player
        {
            public uint Unknown1;
            public uint Condition;
            public byte[] Unknown2;
            public uint EffectIndex;
            public uint EffectCount;
            public byte[] Unknown3;
            public uint Pointer;
            public uint Count;
            public uint HealthCondition;
            public List<Anim> Info;
            public List<Effects.IEffect> Effects;
        }
        public struct Enemy
        {
            public uint Condition1;
            public byte[] Unknown1;
            public uint Condition2;
            public byte[] Unknown2;
            public uint EffectIndex;
            public uint EffectCount;
            public byte[] Unknown3;
            public uint Pointer;
            public uint Count;
            public uint HealthCondition;
            public List<Anim> Info;
            public List<Effects.IEffect> Effects;
        }
        public struct Object
        {
            public byte[] Unknown1;
            public uint EffectIndex;
            public uint EffectCount;
            public uint Unknown2;
            public uint Pointer;
            public uint Count;
            public byte[] Unknown3;
            public List<Anim> Info;
            public List<Effects.IEffect> Effects;
        }
        public struct Arm
        {
            public byte[] Unknown1;
            public uint Pointer;
            public uint Count;
            public byte[] Unknown2;
            public List<Anim> Info;
            public List<Effects.IEffect> Effects;
        }
        public struct Unk4
        {
            public byte[] Unknown1;
            public uint EffectIndex;
            public uint EffectCount;
            public uint Pointer;
            public uint Count;
            public byte[] Unknown2;
            public List<Anim> Info;
            public List<Effects.IEffect> Effects;
        }
        public struct CameraInfo
        {
            public uint Unk;
            public uint EffectIndex;
            public uint EffectCount;
            public uint Pointer;
            public uint Count;
            public List<Anim> Info;
            public List<Effects.IEffect> Effects;
        }
        public struct Anim
        {
            public uint Unk;
            public float FrameStart;
            public float FrameEnd;
            public float Speed;
            public uint AnimID;
            public uint Unknown1;
            public uint Unknown2;
            public uint Unknown3;
            public byte[] AnimationForYAct;
        }
    }
}
