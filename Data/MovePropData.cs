using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgreEdit.Data
{
    public class MovePropData
    {
        public List<Property> PropData = new List<Property>();
        public List<Effects.IEffect> Effects = new List<Effects.IEffect>();
        public struct Property
        {
            public uint Type;
            public float FrameStart;
            public float FrameEnd;
            public uint HitBox;
        }
        public struct Header
        {
            public uint PropertyOffset;
            public uint PropertyCount;
            public uint EffectsOffset;
            public uint EffectsCount;
        }
    }
}
