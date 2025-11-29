namespace YActLib.Common
{
    /// <summary>
    /// Animation entries used by Yakuza 1 Play Data
    /// and later in Yakuza 2 YAct files
    /// </summary>
    public class YActAnimation
    {
        public uint Unk;
        public float FrameStart;
        public float FrameEnd;
        public float Speed;
        public int AnimID;
        public uint Unknown1;
        public uint Unknown2;
        public uint Unknown3;
        public byte[] Animation;
    }
    /// <summary>
    /// Data shared between all YAct Entities
    /// </summary>
    public class CYActEntityBase
    {
        public uint AnimationPointer { get; set; }
        public uint AnimationCount { get; set; }
        public uint EffectIndex { get; set; }
        public uint EffectCount { get; set; }
        public List<YActAnimation> Animations;
        public List<EFFECT_AUTHORING> Effects;
        public void InitLists()
        {
            Animations = new List<YActAnimation>();
            Effects = new List<EFFECT_AUTHORING>();
        }
    }
    public class CActHumanBase : CYActEntityBase
    {
        public uint HealthCondition;
    }
}