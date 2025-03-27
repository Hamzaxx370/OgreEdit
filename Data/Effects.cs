using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OgreEdit.Data
{
    public class Effects
    {
        public struct DefEffectData
        {
            public uint ParentID;
            public float FrameStart;
            public float FrameEnd;
            public float Speed;
        }

        public struct ScreenShakeProp : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint Flag;
            public uint Intensity;
        }
        public struct ParticleNormalProp : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint BoneNumber;
            public uint ptclID;
            public float PTCLParam1;
            public float PTCLParam2;
            public uint Unknown;
            public byte[] Flags;
        }
        public struct ParticleTrailProp : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint BoneNumber;
            public RGBA8 RGBA1;
            public RGBA8 RGBA2;
            public float TrailParam1;
            public float TrailParam2;
            public float TrailParam3;
            public uint Unknown1;
            public uint Unknown2;
            public byte[] Flags;
        }

        public interface IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public virtual IEffect Clone()
            {
                return (IEffect)MemberwiseClone();
            }
        }
        public struct RGBA8
        {
            public byte Red;
            public byte Green;
            public byte Blue;
            public byte Alpha;
        };

        public struct HactEvent : IEffect
        {
            public string Name;
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint BoneNumber;
        }

        public struct SoundCue : IEffect
        {
            public uint ParentID { get;set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint BoneNumber;
            public float SoundSpeed;
            public ushort ContainerID;
            public ushort VoiceID;
        }
        public struct ParticleNormal : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint BoneNumber;
            public uint ptclID;
            public float PTCLParam1;
            public float PTCLParam2;
            public uint Unknown;
            public byte[] Flags;
        }
        public struct ParticleTrail : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint BoneNumber;
            public RGBA8 RGBA1;
            public RGBA8 RGBA2;
            public float TrailParam1;
            public float TrailParam2;
            public float TrailParam3;
            public uint Unknown1;
            public uint Unknown2;
            public byte[] Flags;
        }

        public struct Damage : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint DamageVal;
        }
        public struct Loop : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint MaxLoopNum;
            public int Flag;
        }
        public struct NormalBranch : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public int ID;
        }

        public struct Dead1 : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public int ID;
        }

        public struct Dead2 : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public int ID;
        }

        public struct CounterBranch : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public int Unknown1;
            public int Unknown2;
        }

        public struct Finish : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
        }

        public struct CounterUp : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }

            public int Unknown;
        }

        public struct CounterReset : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }

            public int Unknown;
        }

        public struct ChangeFinishStatus : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }

            public int Status;
        }
        public struct ButtonTiming : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }

            public uint Button;

            public int ID;
        }
        public struct ButtonSpam : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public ushort Button;
            public ushort Count;
            public int ID;
        }
        public struct ScreenFlash : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint Unknown1;
            public float Unknown2;
            public RGBA8 RGBA;
        }

        public struct AfterImage : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint Unknown1;
            public float Unknown2;
            public float Param1;
            public float Param2;
            public float Scale;
            public RGBA8 RGBA;

        }

        public struct CtrlVibration : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint Vibration1;
            public uint Vibration2;
        }
        public struct UnknownEffect : IEffect
        {
            public uint ParentID { get; set; }
            public float FrameStart { get; set; }
            public float FrameEnd { get; set; }
            public float Speed { get; set; }
            public uint BoneNumber;
            public byte[] UnknownData; 
        }

    }
}
