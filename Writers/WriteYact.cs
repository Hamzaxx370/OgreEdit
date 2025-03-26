using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgreEdit.Data;
using static OgreEdit.Data.CSVData;

namespace OgreEdit.Writers
{
    internal class WriteYact
    {
        uint EffectCount = 0;
        int AnimCount = 0;
        int CamCount = 0;
        private List<Effects.IEffect> Chara = new List<Effects.IEffect>();
        private List<Effects.IEffect> Cam = new List<Effects.IEffect>();
        private List<uint> EffectPointers = new List<uint> ();
        private List<uint> Chunk1Pointers = new List<uint>();
        private List<uint> Chunk2Pointers = new List<uint>();
        private List<uint> MTBWPointers = new List<uint>();
        private List<uint> OMTPointers = new List<uint>();
        public void WriteNewYact(YactData Data, ref YAct YAct,ref TreeNode CSVNode)
        {
            using (SaveFileDialog OpenFile = new SaveFileDialog())
            {
                
                OpenFile.Filter = "PS2 YAct(*.bin) |*.bin";
                OpenFile.Title = "Export YAct";
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    string path = OpenFile.FileName;
                    using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                    {
                        ReOrderEffects(ref YAct,ref CSVNode,ref Data);
                        writer.Write((uint)0);
                        writer.Write((uint)64);
                        writer.Write((uint)Cam.Count+Chara.Count);
                        writer.Write((uint)0);
                        writer.Write((uint)Data.Chunk1.Count);
                        writer.Write((uint)0);
                        writer.Write((uint)Data.Chunk2.Count);
                        writer.Write((uint)0);
                        writer.Write((uint)Data.MTBWs.Count);
                        writer.Write((double)0);
                        writer.Write((uint)0);
                        writer.Write((uint)Data.OMTs.Count);
                        writer.Write(new byte[12]);
                        writer.Write(new byte[4* (Cam.Count + Chara.Count)]);
                        long Chunk1TablePos = writer.BaseStream.Position;
                        writer.Write(new byte[4 * Data.Chunk1.Count]);
                        long Chunk2TablePos = writer.BaseStream.Position;
                        writer.Write(new byte[4 * Data.Chunk2.Count]);
                        long MTBWTablePos = writer.BaseStream.Position;
                        writer.Write(new byte[4 * Data.MTBWs.Count]);
                        long OMTTablePos = writer.BaseStream.Position;
                        writer.Write(new byte[4 * Data.OMTs.Count]);
                        long EndTablePos = writer.BaseStream.Position;
                        AlignData(writer, 16);
                        WriteEffects(writer);
                        foreach (byte[] Chunk1 in Data.Chunk1)
                        {
                            uint Pointer = (uint)writer.BaseStream.Position;
                            Chunk1Pointers.Add(Pointer);
                            writer.Write(Chunk1);
                        }
                        foreach (byte[] Chunk2 in Data.Chunk2)
                        {
                            uint Pointer = (uint)writer.BaseStream.Position;
                            Chunk2Pointers.Add(Pointer);
                            writer.Write(Chunk2);
                        }
                        foreach (byte[] MTBW in Data.MTBWs)
                        {
                            uint Pointer = (uint)writer.BaseStream.Position;
                            MTBWPointers.Add(Pointer);
                            writer.Write(Pointer + 16);
                            writer.Write((uint)MTBW.Length);
                            writer.Write(new byte[8]);
                            writer.Write(MTBW);
                        }
                        foreach (byte[] OMT in Data.OMTs)
                        {
                            uint Pointer = (uint)writer.BaseStream.Position;
                            OMTPointers.Add(Pointer);
                            writer.Write(Pointer + 16);
                            writer.Write((uint)OMT.Length);
                            writer.Write(new byte[8]);
                            writer.Write(OMT);
                        }
                        uint FileSize = (uint)writer.BaseStream.Position;
                        writer.Seek(0,SeekOrigin.Begin);
                        writer.Write(FileSize);
                        writer.Seek(12, SeekOrigin.Begin);
                        writer.Write((uint)Chunk1TablePos);
                        writer.Write((uint)Chunk1Pointers.Count);
                        writer.Seek(20, SeekOrigin.Begin);
                        writer.Write((uint)Chunk2TablePos);
                        writer.Write((uint)Chunk2Pointers.Count);
                        writer.Seek(28, SeekOrigin.Begin);
                        writer.Write((uint)MTBWTablePos);
                        writer.Write((uint)MTBWPointers.Count);
                        writer.Seek(36, SeekOrigin.Begin);
                        writer.Write((uint)OMTTablePos);
                        writer.Seek(44, SeekOrigin.Begin);
                        writer.Write((uint)OMTTablePos);
                        writer.Write((uint)OMTPointers.Count);
                        writer.Seek(52, SeekOrigin.Begin);
                        writer.Write((uint)EndTablePos);
                        writer.Seek((int)Chunk1TablePos, SeekOrigin.Begin);
                        foreach (uint Pointer in Chunk1Pointers)
                        {
                            writer.Write(Pointer);
                        }
                        writer.Seek((int)Chunk2TablePos, SeekOrigin.Begin);
                        foreach (uint Pointer in Chunk2Pointers)
                        {
                            writer.Write(Pointer);
                        }
                        writer.Seek(64, SeekOrigin.Begin);
                        foreach (uint Pointer in EffectPointers)
                        {
                            writer.Write(Pointer);
                        }
                        writer.Seek((int)OMTTablePos, SeekOrigin.Begin);
                        foreach (uint Pointer in OMTPointers)
                        {
                            writer.Write(Pointer);
                        }
                        writer.Seek((int)MTBWTablePos, SeekOrigin.Begin);
                        foreach (uint Pointer in MTBWPointers)
                        {
                            writer.Write(Pointer);
                        }
                    }
                }
            }
        }
        private void ReOrderEffects(ref YAct YAct,ref TreeNode CSVNode,ref YactData Data)
        {
            YAct YActNodeTag = new YAct();
            if (CSVNode.Tag is YAct Tag)
            {
                YActNodeTag = Tag;
            }
            for (int n = 0;n<CSVNode.Nodes.Count;n++)
            {
                TreeNode Node = CSVNode.Nodes[n];
                switch (Node.Tag)
                {
                    case Player Player:
                        Player.Effects.Clear();
                        foreach (TreeNode Child in Node.Nodes)
                        {
                            if (Child.Tag is Effects.IEffect Effect)
                            {
                                Player.Effects.Add(Effect);
                            }
                        }
                        if (YAct.PlayerPointer == 0)
                        {
                            break;
                        }
                        Player.EffectIndex = EffectCount;
                        Player.EffectCount = (uint)Player.Effects.Count;
                        foreach (Effects.IEffect Effect in Player.Effects)
                        {
                            Chara.Add(Effect);
                            EffectCount++;
                        }
                        for (int e = 0; e < Node.Nodes.Count; e++)
                        {
                            TreeNode Child = Node.Nodes[e];
                            if (Child.Tag is Anim Anim)
                            {
                                int AIndex = Player.Info.IndexOf(Anim);
                                Anim.AnimID = (uint)AnimCount;
                                Data.OMTs.Add(Anim.AnimationForYAct);
                                Child.Tag = Anim;
                                Player.Info[AIndex] = Anim;
                                AnimCount++;
                            }
                        }
                        Node.Tag = Player;
                        YActNodeTag.Player = Player;
                        break;
                    case Enemy Enemy:
                        int EneInx = YActNodeTag.Enemies.IndexOf(Enemy);
                        Enemy.Effects.Clear();
                        foreach (TreeNode Child in Node.Nodes)
                        {
                            if (Child.Tag is Effects.IEffect Effect)
                            {
                                Enemy.Effects.Add(Effect);
                            }
                        }
                        Enemy.EffectIndex = EffectCount;
                        Enemy.EffectCount = (uint)Enemy.Effects.Count;
                        foreach (Effects.IEffect Effect in Enemy.Effects)
                        {
                            Chara.Add(Effect);
                            EffectCount++;
                        }
                        for (int e = 0; e < Node.Nodes.Count; e++)
                        {
                            TreeNode Child = Node.Nodes[e];
                            if (Child.Tag is Anim Anim)
                            {
                                int AIndex = Enemy.Info.IndexOf(Anim);
                                Anim.AnimID = (uint)AnimCount;
                                Data.OMTs.Add(Anim.AnimationForYAct);
                                Child.Tag = Anim;
                                Enemy.Info[AIndex] = Anim;
                                AnimCount++;
                            }
                        }
                        Node.Tag = Enemy;
                        YActNodeTag.Enemies[EneInx] = Enemy;
                        break;
                    case CSVData.Object Object:
                        int ObjInx = YActNodeTag.Objects.IndexOf(Object);
                        Object.Effects.Clear();
                        foreach (TreeNode Child in Node.Nodes)
                        {
                            if (Child.Tag is Effects.IEffect Effect)
                            {
                                Object.Effects.Add(Effect);
                            }
                        }
                        Object.EffectIndex = EffectCount;
                        Object.EffectCount = (uint)Object.Effects.Count;
                        foreach (Effects.IEffect Effect in Object.Effects)
                        {
                            Chara.Add(Effect);
                            EffectCount++;
                        }
                        for (int e = 0; e < Node.Nodes.Count; e++)
                        {
                            TreeNode Child = Node.Nodes[e];
                            if (Child.Tag is Anim Anim)
                            {
                                int AIndex = Object.Info.IndexOf(Anim);
                                Anim.AnimID = (uint)AnimCount;
                                Data.OMTs.Add(Anim.AnimationForYAct);
                                Child.Tag = Anim;
                                Object.Info[AIndex] = Anim;
                                AnimCount++;
                            }
                        }
                        Node.Tag = Object;
                        YActNodeTag.Objects[ObjInx] = Object;
                        break;
                    case Arm Arm:
                        int ArmInx = YActNodeTag.Arms.IndexOf(Arm);
                        
                        for (int e = 0; e < Node.Nodes.Count; e++)
                        {
                            TreeNode Child = Node.Nodes[e];
                            if (Child.Tag is Anim Anim)
                            {
                                int AIndex = Arm.Info.IndexOf(Anim);
                                Anim.AnimID = (uint)AnimCount;
                                Data.OMTs.Add(Anim.AnimationForYAct);
                                Child.Tag = Anim;
                                Arm.Info[AIndex] = Anim;
                                AnimCount++;
                            }
                        }
                        Node.Tag = Arm;
                        YActNodeTag.Arms[ArmInx] = Arm;
                        break;
                    case Unk4 Model:
                        int MdlInx = YActNodeTag.UnknownC4.IndexOf(Model);
                        Model.Effects.Clear();
                        foreach (TreeNode Child in Node.Nodes)
                        {
                            if (Child.Tag is Effects.IEffect Effect)
                            {
                                Model.Effects.Add(Effect);
                            }
                        }
                        Model.EffectIndex = EffectCount;
                        Model.EffectCount = (uint)Model.Effects.Count;
                        foreach (Effects.IEffect Effect in Model.Effects)
                        {
                            Chara.Add(Effect);
                            EffectCount++;
                        }
                        for (int e = 0; e < Node.Nodes.Count; e++)
                        {
                            TreeNode Child = Node.Nodes[e];
                            if (Child.Tag is Anim Anim)
                            {
                                int AIndex = Model.Info.IndexOf(Anim);
                                Anim.AnimID = (uint)AnimCount;
                                Data.OMTs.Add(Anim.AnimationForYAct);
                                Child.Tag = Anim;
                                Model.Info[AIndex] = Anim;
                                AnimCount++;
                            }
                        }
                        Node.Tag = Model;
                        YActNodeTag.UnknownC4[MdlInx] = Model;
                        break;
                    case CameraInfo Camera:
                        int CamInx = YActNodeTag.CamInfos.IndexOf(Camera);
                        Camera.Effects.Clear();
                        foreach (TreeNode Child in Node.Nodes)
                        {
                            if (Child.Tag is Effects.IEffect Effect)
                            {
                                Camera.Effects.Add(Effect);
                            }
                        }
                        Camera.EffectIndex = EffectCount;
                        Camera.EffectCount = (uint)Camera.Effects.Count;
                        foreach (Effects.IEffect Effect in Camera.Effects)
                        {
                            Cam.Add(Effect);
                            EffectCount++;
                        }
                        for (int e = 0; e < Node.Nodes.Count; e++)
                        {
                            TreeNode Child = Node.Nodes[e];
                            if (Child.Tag is Anim Anim)
                            {
                                int AIndex = Camera.Info.IndexOf(Anim);
                                Anim.AnimID = (uint)CamCount;
                                Data.MTBWs.Add(Anim.AnimationForYAct);
                                Child.Tag = Anim;
                                Camera.Info[AIndex] = Anim;
                                CamCount++;
                            }
                        }
                        Node.Tag = Camera;
                        YActNodeTag.CamInfos[CamInx] = Camera;
                        break;
                }
                YAct = YActNodeTag;
                CSVNode.Tag = YActNodeTag;
            }
        }
        private void AlignData(BinaryWriter writer,int Alignment)
        {
            long currentpos = writer.BaseStream.Position;
            int padding = (int)(Alignment - currentpos % Alignment) % Alignment;
            writer.Write(new byte[padding]);
        }
        private void WriteEffects(BinaryWriter writer)
        {
            foreach (Effects.IEffect effect in Chara)
            {
                uint Ptr = (uint)writer.BaseStream.Position;
                EffectPointers.Add(Ptr);
                writer.Write(effect.ParentID);
                writer.Write(effect.FrameStart);
                writer.Write(effect.FrameEnd);
                writer.Write(effect.Speed);
                if (effect is Effects.Damage Dmg)
                {
                    writer.Write(new byte[28]);
                    writer.Write((uint)4);
                    writer.Write(new byte[28]);
                    writer.Write(Dmg.DamageVal);
                    writer.Write(new byte[16]);
                }
                else if(effect is Effects.SoundCue Cue)
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
                else if(effect is Effects.ParticleNormal PTCL)
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
                else if(effect is Effects.ParticleTrail Trail)
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
                else if(effect is Effects.Loop Loop)
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
            }
            foreach (Effects.IEffect effect in Cam)
            {
                uint Ptr = (uint)writer.BaseStream.Position;
                EffectPointers.Add(Ptr);
                writer.Write(effect.ParentID);
                writer.Write(effect.FrameStart);
                writer.Write(effect.FrameEnd);
                writer.Write(effect.Speed);
                if (effect is Effects.CtrlVibration Vib)
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
