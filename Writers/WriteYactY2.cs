using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgreEdit.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OgreEdit.Writers
{
    public class WriteYactY2
    {
        private List<Effects.IEffect> Cam = new List<Effects.IEffect>();
        private List<Effects.IEffect> Chara = new List<Effects.IEffect>();
        private List<long> EffectStrings = new List<long>();
        private List<long> CharaNamePtrs = new List<long>();
        private List<long> CamNamePtrs = new List<long>();
        private List<long> EffectNamePtrs = new List<long>();
        private List<uint> EffectPointers = new List<uint>();
        private List<long> CamPtrs = new List<long>();
        private List<long> CamInfoPtrs = new List<long>();
        private List<long> Chunk1Ptrs = new List<long>();
        private List<long> Chunk2Ptrs = new List<long>();
        private List<long> CharaInfoPtrs = new List<long>();
        private List<long> OMTPtrs = new List<long>();
        private List<long> MTBWPtrs = new List<long>();
        private List<YactDataY2.ParentData> ParentsChara = new List<YactDataY2.ParentData>();
        private List<YactDataY2.ParentData> ParentsCam = new List<YactDataY2.ParentData>();
        public void WriteYact(ref YactDataY2 DataY2, ref YactData DataY1,TreeNode YActNode)
        {
            uint EffectCount = ReOrderEffects(ref DataY2.CharaInfos, ref DataY2.CamInfos,ref DataY1,YActNode);
            using (SaveFileDialog OpenFile = new SaveFileDialog())
            {
                OpenFile.Filter = "PS2 YAct(*.bin) |*.bin";
                OpenFile.Title = "Export YAct";
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    string path = OpenFile.FileName;
                    using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                    {
                        writer.Write(DataY1.UnknownHdr);
                        writer.Write(new byte[104]);
                        int i = 0;
                        uint CamPos = (uint)writer.BaseStream.Position;
                        foreach(YactDataY2.CameraInfo Cam in DataY2.CamInfos)
                        {
                            writer.Write(0);
                            writer.Write(Cam.EffectIndex);
                            writer.Write(Cam.Effects.Count);
                            CamInfoPtrs.Add(writer.BaseStream.Position);
                            writer.Write((uint)0);
                            writer.Write(Cam.Count);
                            writer.Write(new byte[12]);
                            i++;
                        }
                        int e = 0;
                        uint CharaPos = (uint)writer.BaseStream.Position;
                        foreach (YactDataY2.CharaInfo Chara in DataY2.CharaInfos)
                        {
                            CharaNamePtrs.Add(writer.BaseStream.Position);
                            writer.Write(0);
                            CharaInfoPtrs.Add(writer.BaseStream.Position);
                            writer.Write((uint)0);
                            writer.Write(Chara.Count);
                            writer.Write(Chara.EffectIndex);
                            writer.Write(Chara.Effects.Count);
                            e++;
                        }
                        int c = 0;
                        foreach (YactDataY2.CameraInfo Cam in DataY2.CamInfos)
                        {
                            uint ptr = (uint)writer.BaseStream.Position;
                            writer.BaseStream.Seek(CamInfoPtrs[c], SeekOrigin.Begin);
                            writer.Write(ptr);
                            c++;
                            writer.BaseStream.Seek(ptr, SeekOrigin.Begin);
                            foreach (YactDataY2.CamCharaInfoEntry Info in Cam.Info)
                            {
                                writer.Write(Info.Unk);
                                writer.Write(Info.FrameStart);
                                writer.Write(Info.FrameEnd);
                                writer.Write(Info.Speed);
                                writer.Write(Info.AnimID);
                                writer.Write(Info.Unknown1);
                                writer.Write(Info.Unknown2);
                                writer.Write(Info.Unknown3);
                            }
                        }
                        int o = 0;
                        foreach (YactDataY2.CharaInfo Chara in DataY2.CharaInfos)
                        {
                            uint ptr = (uint)writer.BaseStream.Position;
                            writer.BaseStream.Seek(CharaInfoPtrs[o], SeekOrigin.Begin);
                            writer.Write(ptr);
                            o++;
                            writer.BaseStream.Seek(ptr, SeekOrigin.Begin);
                            foreach (YactDataY2.CamCharaInfoEntry Info in Chara.Info)
                            {
                                writer.Write(Info.Unk);
                                writer.Write(Info.FrameStart);
                                writer.Write(Info.FrameEnd);
                                writer.Write(Info.Speed);
                                writer.Write(Info.AnimID);
                                writer.Write(Info.Unknown1);
                                writer.Write(Info.Unknown2);
                                writer.Write(Info.Unknown3);
                            }
                        }
                        uint EffectsPtr = (uint)writer.BaseStream.Position;
                        foreach (Effects.IEffect Effect in DataY1.Effects)
                        {
                            EffectNamePtrs.Add(writer.BaseStream.Position);
                            writer.Write(new byte[16]);
                        }
                        uint Chunk1Ptr = (uint)writer.BaseStream.Position;
                        foreach (byte[] C1 in DataY1.Chunk1)
                        {
                            writer.Write(new byte[4]);
                        }
                        uint Chunk2Ptr = (uint)writer.BaseStream.Position;
                        foreach (byte[] C2 in DataY1.Chunk2)
                        {
                            writer.Write(new byte[4]);
                        }
                        uint CamDataPtr = (uint)writer.BaseStream.Position;
                        foreach (YactDataY2.CameraInfo Cam in DataY2.CamInfos)
                        {
                            foreach (YactDataY2.CamCharaInfoEntry Info in Cam.Info)
                            {
                                writer.Write(new byte[4]);
                            }
                        }
                        uint CharaDataPtr = (uint)writer.BaseStream.Position;
                        foreach (YactDataY2.CharaInfo Chara in DataY2.CharaInfos)
                        {
                            foreach (YactDataY2.CamCharaInfoEntry Info in Chara.Info)
                            {
                                writer.Write(new byte[4]);
                            }
                        }
                        uint StringsPtr = (uint)writer.BaseStream.Position;
                        int ParentInx = 0;
                        foreach (YactDataY2.CharaInfo Charac in DataY2.CharaInfos)
                        {
                            int returnpos = (int)writer.BaseStream.Position;
                            writer.Seek((int)CharaNamePtrs[DataY2.CharaInfos.IndexOf(Charac)],SeekOrigin.Begin);
                            writer.Write(returnpos);
                            writer.Seek(returnpos, SeekOrigin.Begin);
                            WriteString(writer,Charac.Name);
                            if (Charac.Name == "KIRYU")
                            {
                                writer.Write((ushort)58);
                            }
                            int EffectInx = 0;
                            foreach (Effects.IEffect Effect in Charac.Effects)
                            {
                                if (Effect is Effects.HactEvent HE)
                                {
                                    
                                    uint StringPos = (uint)writer.BaseStream.Position;
                                        
                                    writer.BaseStream.Seek(StringPos, SeekOrigin.Begin);
                                    WriteString(writer, HE.Name);
                                    uint UNKPos = (uint)writer.BaseStream.Position;
                                    writer.BaseStream.Seek(EffectsPtr + 16 * DataY1.Effects.IndexOf(HE), SeekOrigin.Begin);
                                    writer.Write(StringPos);
                                    writer.Write((uint)0);
                                        
                                    writer.BaseStream.Seek(UNKPos, SeekOrigin.Begin);
                                    if (HE.Name == "HG_CHK_00")
                                    {
                                        writer.Write((ushort)58);
                                    }
                                    
                                    
                                }
                                EffectInx++;
                            }
                            
                            ParentInx++;
                        }
                        
                        AlignData(writer, 16);
                        WriteEffects(writer,DataY1);
                        foreach (byte [] C1 in DataY1.Chunk1)
                        {
                            Chunk1Ptrs.Add(writer.BaseStream.Position);
                            writer.Write(C1);
                        }
                        foreach (byte[] C2 in DataY1.Chunk2)
                        {
                            Chunk2Ptrs.Add(writer.BaseStream.Position);
                            writer.Write(C2);
                        }
                        
                        foreach (byte[] MTBW in DataY1.MTBWs)
                        {
                            MTBWPtrs.Add(writer.BaseStream.Position);
                            writer.Write((uint)writer.BaseStream.Position + 16);
                            writer.Write((uint)MTBW.Length);
                            AlignData(writer, 16);
                            writer.Write(MTBW);
                            AlignData(writer, 16);
                        }
                        foreach (byte[] OMT in DataY1.OMTs)
                        {
                            OMTPtrs.Add(writer.BaseStream.Position);
                            writer.Write((uint)writer.BaseStream.Position + 16);
                            writer.Write((uint)OMT.Length);
                            AlignData(writer, 16);
                            writer.Write(OMT);
                            AlignData(writer, 16);  
                        }
                        i = 0;
                        foreach(uint ptr in EffectPointers)
                        {
                            writer.BaseStream.Seek(EffectsPtr+16*i+12,SeekOrigin.Begin);
                            writer.Write(ptr);
                            i++;
                        }
                        writer.Seek((int)Chunk1Ptr, SeekOrigin.Begin);
                        foreach (uint Ptr in Chunk1Ptrs)
                        {
                            writer.Write(Ptr);
                        }
                        writer.Seek((int)Chunk2Ptr, SeekOrigin.Begin);
                        foreach (uint Ptr in Chunk2Ptrs)
                        {
                            writer.Write(Ptr);
                        }
                        writer.Seek((int)CamDataPtr, SeekOrigin.Begin);
                        foreach (uint ptr in MTBWPtrs)
                        {
                            writer.Write(ptr);
                        }
                        writer.Seek((int)CharaDataPtr, SeekOrigin.Begin);
                        foreach (uint ptr in OMTPtrs)
                        {
                            writer.Write(ptr);
                        }
                        writer.Seek(32, SeekOrigin.Begin);
                        writer.Write(CamPos);
                        writer.Write(DataY2.CamInfos.Count);
                        writer.Write(CharaPos);
                        writer.Write(DataY2.CharaInfos.Count);
                        writer.Write(new byte[24]);
                        writer.Write(EffectsPtr);
                        writer.Write(EffectCount);
                        writer.Write(Chunk1Ptr);
                        writer.Write((uint)DataY1.Chunk1.Count);
                        writer.Write(Chunk2Ptr);
                        writer.Write((uint)DataY1.Chunk2.Count);
                        writer.Write(CamDataPtr);
                        writer.Write(DataY1.MTBWs.Count);
                        writer.Write(new byte[8]);
                        writer.Write(CharaDataPtr);
                        writer.Write(DataY1.OMTs.Count);
                        writer.Write(StringsPtr);
                        writer.Seek((int)Chunk1Ptr,SeekOrigin.Begin);
                        
                    }
                }
            }
        }
        private void WriteString(BinaryWriter writer,string name)
        {
            writer.Write(Encoding.ASCII.GetBytes(name));
            writer.Write((byte)0);
        }
        private void AlignData(BinaryWriter writer, int Alignment)
        {
            long currentpos = writer.BaseStream.Position;
            int padding = (int)(Alignment - currentpos % Alignment) % Alignment;
            writer.Write(new byte[padding]);
        }
        private uint ReOrderEffects(ref List<YactDataY2.CharaInfo>Charas , ref List<YactDataY2.CameraInfo> Cams,ref YactData Data,TreeNode YActNode)
        {
            uint Index = 0;
            List<YactDataY2.CharaInfo> CharasN = new List<YactDataY2.CharaInfo>();
            List<YactDataY2.CameraInfo> CamsN = new List<YactDataY2.CameraInfo>();
            foreach (TreeNode Child in YActNode.Nodes)
            {
                switch (Child.Tag)
                {
                    case YactDataY2.CharaInfo Chara:
                        int CIndex = Charas.IndexOf(Chara);
                        YactDataY2.CharaInfo CharaN = Charas[CIndex];
                        CharaN.Effects.Clear();
                        foreach (TreeNode ChildE in Child.Nodes)
                        {
                            if (ChildE.Tag is Effects.IEffect Effect)
                            {
                                CharaN.Effects.Add(Effect);
                            }
                        }
                        Charas[CIndex] = CharaN;
                        break;
                    case YactDataY2.CameraInfo Cam:
                        int CaIndex = Cams.IndexOf(Cam);
                        YactDataY2.CameraInfo CamN = Cams[CaIndex];
                        CamN.Effects.Clear();
                        foreach (TreeNode ChildE in Child.Nodes)
                        {
                            if (ChildE.Tag is Effects.IEffect Effect)
                            {
                                CamN.Effects.Add(Effect);
                            }
                        }
                        Cams[CaIndex] = CamN;
                        break;
                }
            }
            foreach (YactDataY2.CharaInfo Chara in Charas)
            {
                var copy = Chara;
                copy.EffectIndex = Index;
                CharasN.Add(copy);
                foreach (Effects.IEffect Effect in Chara.Effects)
                {
                    Index++;
                    Data.Effects.Add(Effect);
                }
            }
            foreach (YactDataY2.CameraInfo Cam in Cams)
            {
                var copy = Cam;
                copy.EffectIndex = Index;
                CamsN.Add(copy);
                foreach (Effects.IEffect Effect in Cam.Effects)
                {
                    Index++;
                    Data.Effects.Add(Effect);
                }
            }
            Charas = CharasN;
            Cams = CamsN;
            return Index;
        }
        private void WriteEffects(BinaryWriter writer,YactData Data)
        {
            foreach (Effects.IEffect effect in Data.Effects)
            {
                uint Ptr = (uint)writer.BaseStream.Position;
                EffectPointers.Add(Ptr);
                writer.Write(effect.ParentID);
                writer.Write(effect.FrameStart);
                writer.Write(effect.FrameEnd);
                writer.Write(effect.Speed);
                if (effect is Effects.HactEvent HE)
                {
                    writer.Write(new byte[28]);
                    writer.Write((uint)4);
                    writer.Write(new byte[48]);
                }
                else if (effect is Effects.Damage Dmg)
                {
                    writer.Write(new byte[28]);
                    writer.Write((uint)4);
                    writer.Write(new byte[28]);
                    writer.Write(Dmg.DamageVal);
                    writer.Write(new byte[16]);
                }
                else if (effect is Effects.SoundCue Cue)
                {
                    writer.Write(Cue.BoneNumber);
                    writer.Write(new byte[24]);
                    writer.Write((uint)20);
                    writer.Write(new byte[8]);
                    writer.Write((float)1);
                    writer.Write(Cue.ContainerID);
                    writer.Write(Cue.VoiceID);
                    writer.Write(new byte[32]);
                }
                else if (effect is Effects.ParticleNormal PTCL)
                {
                    writer.Write(PTCL.BoneNumber);
                    writer.Write(new byte[12]);
                    writer.Write(PTCL.PTCLParam1);
                    writer.Write((uint)0);
                    writer.Write(PTCL.PTCLParam2);
                    writer.Write((uint)1);
                    writer.Write(new byte[12]);
                    writer.Write(PTCL.ptclID);
                    writer.Write(new byte[12]);
                    writer.Write(PTCL.Unknown);
                    writer.Write(new byte[12]);
                    writer.Write(PTCL.Flags);
                }
                else if (effect is Effects.ParticleTrail Trail)
                {
                    writer.Write(Trail.BoneNumber);
                    writer.Write(new byte[24]);
                    writer.Write((uint)1);
                    writer.Write(new byte[4]);
                    writer.Write(Trail.TrailParam1);
                    writer.Write(Trail.TrailParam2);
                    writer.Write((uint)0);
                    writer.Write(Trail.RGBA1.Blue);
                    writer.Write(Trail.RGBA1.Green);
                    writer.Write(Trail.RGBA1.Red);
                    writer.Write(Trail.RGBA1.Alpha);
                    writer.Write(Trail.RGBA2.Blue);
                    writer.Write(Trail.RGBA2.Green);
                    writer.Write(Trail.RGBA2.Red);
                    writer.Write(Trail.RGBA2.Alpha);
                    writer.Write(Trail.TrailParam3);
                    writer.Write(Trail.Unknown1);
                    writer.Write(new byte[8]);
                    writer.Write(Trail.Unknown2);
                    writer.Write(Trail.Flags);
                }
                else if (effect is Effects.Loop Loop)
                {
                    writer.Write(new byte[28]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)6);
                    writer.Write(new byte[12]);
                    writer.Write(Loop.MaxLoopNum);
                    writer.Write(new byte[16]);
                }
                else if (effect is Effects.LoopEnd LoopE)
                {
                    writer.Write(new byte[28]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)9);
                    writer.Write(new byte[12]);
                    writer.Write(LoopE.Unknown);
                    writer.Write(new byte[16]);
                }
                else if (effect is Effects.ButtonTiming Button)
                {
                    writer.Write(new byte[28]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)8);
                    writer.Write(new byte[28]);
                    writer.Write(Button.Button);
                }
                else if (effect is Effects.ButtonSpam ButtonS)
                {
                    writer.Write(new byte[28]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)8);
                    writer.Write(new byte[28]);
                    writer.Write(ButtonS.Button);
                    writer.Write(ButtonS.Count);
                }
                else if (effect is Effects.UnknownEffect Unk)
                {
                    writer.Write(Unk.BoneNumber);
                    writer.Write(Unk.UnknownData);
                }
                else if (effect is Effects.CtrlVibration Vib)
                {
                    writer.Write(new byte[44]);
                    writer.Write((uint)0x4C);
                    writer.Write(new byte[12]);
                    writer.Write(Vib.Vibration1);
                    writer.Write(new byte[12]);
                    writer.Write(Vib.Vibration2);
                }
                else if (effect is Effects.ScreenFlash ScrFlsh)
                {
                    writer.Write(new byte[16]);
                    writer.Write(ScrFlsh.Unknown1);
                    writer.Write(new byte[12]);
                    writer.Write(ScrFlsh.Unknown2);
                    writer.Write(ScrFlsh.RGBA.Blue);
                    writer.Write(ScrFlsh.RGBA.Green);
                    writer.Write(ScrFlsh.RGBA.Red);
                    writer.Write(ScrFlsh.RGBA.Alpha);
                    writer.Write(new byte[4]);
                    writer.Write((uint)0x31);
                    writer.Write(new byte[32]);
                }
                else if (effect is Effects.AfterImage AImage)
                {
                    writer.Write(new byte[16]);
                    writer.Write(AImage.Unknown1);
                    writer.Write(new byte[12]);
                    writer.Write(AImage.Unknown2);
                    writer.Write(AImage.Param1);
                    writer.Write(AImage.Param2);
                    writer.Write((uint)0x2B);
                    writer.Write(AImage.Scale);
                    writer.Write(new byte[4]);
                    writer.Write(AImage.RGBA.Blue);
                    writer.Write(AImage.RGBA.Green);
                    writer.Write(AImage.RGBA.Red);
                    writer.Write(AImage.RGBA.Alpha);
                    writer.Write(new byte[20]);
                }
            }
            
        }
    }
}
