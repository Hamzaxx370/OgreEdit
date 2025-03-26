using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgreEdit.Data
{
    public class YactData
    {
        public byte[] UnknownHdr;
        public List<byte[]> Chunk1 = new List<byte[]>();
        public List<byte[]> Chunk2 = new List<byte[]>();
        public List<Effects.IEffect> Effects = new List<Effects.IEffect>();
        public List<byte[]> OMTs = new List<byte[]>();
        public List<byte[]> MTBWs = new List<byte[]>();
        public struct Header
        {
            public uint FileSize;
            public uint EffectTable;
            public uint EffectTableCount;
            public uint Chunk1;
            public uint Chunk1Count;
            public uint Chunk2;
            public uint Chunk2Count;
            public uint MTBW;
            public uint MTBWCount;
            public uint OME;
            public uint OMECount;
            public uint OMT;
            public uint OMTCount;
        }
        public struct OMT
        {
            public uint Offset;
            public uint Size;
            public byte[] OMTFile; 
        }
        public struct MTBW
        {
            public uint Offset;
            public uint Size;
            public byte[] MTBWFile;
        }
    }
}
