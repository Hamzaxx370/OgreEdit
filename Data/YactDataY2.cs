using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgreEdit.Data
{
    public class YactDataY2
    {
        public List<CameraInfo> CamInfos = new List<CameraInfo>();
        public List<CharaInfo> CharaInfos = new List<CharaInfo>();
        public List<Effects.DefEffectData> FrameTimes = new List<Effects.DefEffectData>();
        public struct UnknownHdr
        {
            public byte[] Data;
        }
        public struct Header
        {
            public uint CameraInfoPointer;
            public uint CameraInfoCount;
            public uint CharaInfoPointer;
            public uint CharaInfoCount;
            public uint Chunk3Pointer;
            public uint Chunk3Count;
            public uint ArmsInfoPointer;
            public uint ArmsInfoCount;
            public uint ObjectsInfoPointer;
            public uint ObjectsInfoCount;
            public uint EffectsPointer;
            public uint EffectsCount;
            public uint Chunk7Pointer;
            public uint Chunk7Count;
            public uint Chunk8Pointer;
            public uint Chunk8Count;
            public uint CamAnimPointer;
            public uint CamAnimCount;
            public uint Chunk10Pointer;
            public uint Chunk10Count;
            public uint CharaAnimPointer;
            public uint CharaAnimCount;
            public uint StringsPointer;
            public uint StringsCount;
            public uint Chunk13Pointer;
            public uint Chunk13Count;
        }
        public struct ParentData
        {
            public uint Start;
            public uint Count;
        }
        public struct CamCharaInfoEntry
        {
            public uint Unk;
            public float FrameStart;
            public float FrameEnd;
            public float Speed;
            public uint AnimID;
            public uint Unknown1;
            public uint Unknown2;
            public uint Unknown3;
            public byte[] Animation;
        }
        public struct CameraInfo
        {
            public uint Unk;
            public uint EffectIndex;
            public uint EffectCount;
            public uint Pointer;
            public uint Count;
            public List<CamCharaInfoEntry> Info;
            public List<Effects.IEffect> Effects;
        }
        public struct CharaInfo
        {
            public uint NamePtr;
            public string Name;
            public uint Pointer;
            public uint Count;
            public uint EffectIndex;
            public uint EffectCount;
            public List<CamCharaInfoEntry> Info;
            public List<Effects.IEffect> Effects;
        }
        public struct EffectHeaderY2
        {
            public uint NamePtr;
            public string Name;
            public uint Pointer;
        }
        public struct MTBW
        {
            public uint Pointer;
            public byte[] MTBWData;
        }
        public struct OMT
        {
            public uint Pointer;
            public byte[] OMTData;
        }
    }
}
