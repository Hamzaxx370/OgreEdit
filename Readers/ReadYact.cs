using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using OgreEdit.Data;
using static OgreEdit.Data.CSVData;

namespace OgreEdit.Readers
{
    public class ReadYact
    {
        public List<uint> EffectPointers = new List<uint>();
        public List<uint> Chunk1Pointers = new List<uint>();
        public List<uint> Chunk2Pointers = new List<uint>();
        public List<uint> MTBWPointers = new List<uint>();
        public List<uint> OMTPointers = new List<uint>();
        public void ReadYactFile(string path,ref YactData Data, ref YAct YActCSV, ref TreeNode CSVNode)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                YactData.Header Header = new YactData.Header
                {
                    FileSize = reader.ReadUInt32(),
                    EffectTable = reader.ReadUInt32(),
                    EffectTableCount = reader.ReadUInt32(),
                    Chunk1 = reader.ReadUInt32(),
                    Chunk1Count = reader.ReadUInt32(),
                    Chunk2 = reader.ReadUInt32(),
                    Chunk2Count = reader.ReadUInt32(),
                    MTBW = reader.ReadUInt32(),
                    MTBWCount = reader.ReadUInt32(),
                    OME = reader.ReadUInt32(),
                    OMECount = reader.ReadUInt32(),
                    OMT = reader.ReadUInt32(),
                    OMTCount = reader.ReadUInt32(),
                };
                
                reader.BaseStream.Seek(Header.Chunk1, SeekOrigin.Begin);
                int e;
                for (e = 0; e < Header.Chunk1Count; e++)
                {
                    Chunk1Pointers.Add(reader.ReadUInt32());
                }
                reader.BaseStream.Seek(Header.Chunk2, SeekOrigin.Begin);
                int f;
                for (f = 0; f < Header.Chunk2Count; f++)
                {
                    Chunk2Pointers.Add(reader.ReadUInt32());
                }
                reader.BaseStream.Seek(Header.OMT, SeekOrigin.Begin);
                int o;
                for (o = 0; o < Header.OMTCount; o++)
                {
                    OMTPointers.Add(reader.ReadUInt32());
                }
                foreach (uint pointer in Chunk1Pointers)
                {
                    reader.BaseStream.Seek(pointer, SeekOrigin.Begin);
                    Data.Chunk1.Add(reader.ReadBytes(32));
                }
                foreach (uint pointer in Chunk2Pointers)
                {
                    reader.BaseStream.Seek(pointer, SeekOrigin.Begin);
                    Data.Chunk2.Add(reader.ReadBytes(32));
                }
                foreach (TreeNode Child in CSVNode.Nodes)
                {
                    if (YActCSV.PlayerPointer != 0)
                    {
                        if (Child.Text == "KIRYU")
                        {
                            int Index = CSVNode.Nodes.IndexOf(Child);
                            reader.BaseStream.Seek(Header.EffectTable + 4 * YActCSV.Player.EffectIndex, SeekOrigin.Begin);
                            for (int i = 0; i < YActCSV.Player.EffectCount; i++)
                            {
                                uint Pointer = reader.ReadUInt32();
                                long returnpos = reader.BaseStream.Position;
                                Effects.IEffect Effect = ReadEffect(Pointer, reader, ref Data);
                                YActCSV.Player.Effects.Add(Effect);
                                reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
                            }
                            for (int i = 0; i < YActCSV.Player.Count; i++)
                            {
                                foreach (TreeNode AnimNode in Child.Nodes)
                                {
                                    if (AnimNode.Tag is Anim)
                                    {
                                        Anim Anim = YActCSV.Player.Info[i];
                                        reader.BaseStream.Seek(Header.OMT + 4 * Anim.AnimID, SeekOrigin.Begin);
                                        uint Pointer = reader.ReadUInt32();
                                        reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                                        uint OMT = reader.ReadUInt32();
                                        uint Size = reader.ReadUInt32();
                                        reader.BaseStream.Seek(OMT, SeekOrigin.Begin);
                                        byte[] OMTFile = reader.ReadBytes((int)Size);
                                        Anim.AnimationForYAct = OMTFile;
                                        YActCSV.Player.Info[i] = Anim;
                                        Child.Nodes[Child.Nodes.IndexOf(AnimNode)].Tag = Anim;
                                    }

                                }


                            }
                        }
                    
                }
                }
                foreach (TreeNode Child in CSVNode.Nodes)
                {
                    if (Child.Tag is CameraInfo Camera)
                    {
                        int CamIndex = YActCSV.CamInfos.IndexOf(Camera);
                        reader.BaseStream.Seek(Header.EffectTable + 4 * Camera.EffectIndex, SeekOrigin.Begin);
                        for (int i = 0; i < Camera.EffectCount; i++)
                        {
                            uint Pointer = reader.ReadUInt32();
                            long returnpos = reader.BaseStream.Position;
                            Effects.IEffect Effect = ReadEffect(Pointer, reader, ref Data);
                            Camera.Effects.Add(Effect);
                            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
                        }
                        for (int i = 0; i < Camera.Info.Count; i++)
                        {
                            foreach (TreeNode AnimNode in Child.Nodes)
                            {
                                if (AnimNode.Tag is Anim)
                                {
                                    Anim Anim = YActCSV.CamInfos[CamIndex].Info[i];
                                    reader.BaseStream.Seek(Header.MTBW + 4 * Anim.AnimID, SeekOrigin.Begin);
                                    uint Pointer = reader.ReadUInt32();
                                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                                    uint OMT = reader.ReadUInt32();
                                    uint Size = reader.ReadUInt32();
                                    reader.BaseStream.Seek(OMT, SeekOrigin.Begin);
                                    byte[] OMTFile = reader.ReadBytes((int)Size);
                                    Anim.AnimationForYAct = OMTFile;
                                    Camera.Info[i] = Anim;
                                    Child.Nodes[Child.Nodes.IndexOf(AnimNode)].Tag = Anim;
                                }
                                
                            }

                        }
                        YActCSV.CamInfos[CamIndex] = Camera;
                        Child.Tag = Camera;
                    }
                }

                int EN = 0;
                
                foreach (Enemy Enemy in YActCSV.Enemies)
                {
                    var encopy = Enemy;
                    int Index = -1;
                    TreeNode EnemyNode = new TreeNode
                    {
                        Tag = 0
                    };
                    foreach (TreeNode Child in CSVNode.Nodes)
                    {
                        if (Child.Text == "ENEMY" + EN.ToString())
                        {
                            Index = CSVNode.Nodes.IndexOf(Child);
                            EnemyNode = Child;
                        }
                    }
                    reader.BaseStream.Seek(Header.EffectTable + 4 * Enemy.EffectIndex, SeekOrigin.Begin);
                    for (int i = 0;i<Enemy.EffectCount;i++)
                    {
                        uint Pointer = reader.ReadUInt32();
                        long returnpos = reader.BaseStream.Position;
                        Effects.IEffect Effect = ReadEffect(Pointer, reader, ref Data);
                        Enemy.Effects.Add(Effect);
                        reader.BaseStream.Seek(returnpos,SeekOrigin.Begin);
                    }
                    for (int i = 0;i<Enemy.Info.Count;i++)
                    {
                        foreach (TreeNode AnimNode in EnemyNode.Nodes)
                            {
                                if (AnimNode.Tag is Anim)
                                {
                                    Anim Anim = Enemy.Info[i];
                                    reader.BaseStream.Seek(Header.OMT + 4 * Anim.AnimID, SeekOrigin.Begin);
                                    uint Pointer = reader.ReadUInt32();
                                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                                    uint OMT = reader.ReadUInt32();
                                    uint Size = reader.ReadUInt32();
                                    reader.BaseStream.Seek(OMT, SeekOrigin.Begin);
                                    byte[] OMTFile = reader.ReadBytes((int)Size);
                                    Anim.AnimationForYAct = OMTFile;
                                    Enemy.Info[i] = Anim;
                                    EnemyNode.Nodes[EnemyNode.Nodes.IndexOf(AnimNode)].Tag = Anim;
                                }
                                
                            }
                    }
                    EnemyNode.Tag = Enemy;
                    EN++;
                }
                EN = 0;
                foreach (CSVData.Object Object in YActCSV.Objects)
                {
                    int Index = -1;
                    TreeNode EnemyNode = new TreeNode
                    {
                        Tag = 0
                    };
                    foreach (TreeNode Child in CSVNode.Nodes)
                    {
                        if (Child.Text == "OBJECT" + EN.ToString())
                        {
                            Index = CSVNode.Nodes.IndexOf(Child);
                            EnemyNode = Child;
                        }
                    }
                    reader.BaseStream.Seek(Header.EffectTable + 4 * Object.EffectIndex, SeekOrigin.Begin);
                    for (int i = 0; i < Object.EffectCount; i++)
                    {
                        uint Pointer = reader.ReadUInt32();
                        long returnpos = reader.BaseStream.Position;
                        Effects.IEffect Effect = ReadEffect(Pointer, reader, ref Data);
                        Object.Effects.Add(Effect);
                        reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
                    }
                    for (int i = 0; i < Object.Info.Count; i++)
                    {
                        foreach (TreeNode AnimNode in EnemyNode.Nodes)
                        {
                            if (AnimNode.Tag is Anim)
                            {
                                Anim Anim = Object.Info[i];
                                reader.BaseStream.Seek(Header.OMT + 4 * Anim.AnimID, SeekOrigin.Begin);
                                uint Pointer = reader.ReadUInt32();
                                reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                                uint OMT = reader.ReadUInt32();
                                uint Size = reader.ReadUInt32();
                                reader.BaseStream.Seek(OMT, SeekOrigin.Begin);
                                byte[] OMTFile = reader.ReadBytes((int)Size);
                                Anim.AnimationForYAct = OMTFile;
                                Object.Info[i] = Anim;
                                EnemyNode.Nodes[EnemyNode.Nodes.IndexOf(AnimNode)].Tag = Anim;
                            }

                        }
                    }
                    EnemyNode.Tag = Object;
                    EN++;
                }
                EN = 0;
                foreach (Arm Object in YActCSV.Arms)
                {
                    int Index = -1;
                    TreeNode EnemyNode = new TreeNode
                    {
                        Tag = 0
                    };
                    foreach (TreeNode Child in CSVNode.Nodes)
                    {
                        if (Child.Text == "ARM" + EN.ToString())
                        {
                            Index = CSVNode.Nodes.IndexOf(Child);
                            EnemyNode = Child;
                        }
                    }
                    
                    for (int i = 0; i < Object.Info.Count; i++)
                    {
                        foreach (TreeNode AnimNode in EnemyNode.Nodes)
                        {
                            if (AnimNode.Tag is Anim)
                            {
                                Anim Anim = Object.Info[i];
                                reader.BaseStream.Seek(Header.OMT + 4 * Anim.AnimID, SeekOrigin.Begin);
                                uint Pointer = reader.ReadUInt32();
                                reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                                uint OMT = reader.ReadUInt32();
                                uint Size = reader.ReadUInt32();
                                reader.BaseStream.Seek(OMT, SeekOrigin.Begin);
                                byte[] OMTFile = reader.ReadBytes((int)Size);
                                Anim.AnimationForYAct = OMTFile;
                                Object.Info[i] = Anim;
                                EnemyNode.Nodes[EnemyNode.Nodes.IndexOf(AnimNode)].Tag = Anim;
                            }

                        }
                    }
                    EnemyNode.Tag = Object;
                    EN++;
                }
                EN = 0;
                foreach (Unk4 Object in YActCSV.UnknownC4)
                {
                    int Index = -1;
                    TreeNode EnemyNode = new TreeNode
                    {
                        Tag = 0
                    };
                    foreach (TreeNode Child in CSVNode.Nodes)
                    {
                        if (Child.Text == "MODEL" + EN.ToString())
                        {
                            Index = CSVNode.Nodes.IndexOf(Child);
                            EnemyNode = Child;
                        }
                    }
                    reader.BaseStream.Seek(Header.EffectTable + 4 * Object.EffectIndex, SeekOrigin.Begin);
                    for (int i = 0; i < Object.EffectCount; i++)
                    {
                        uint Pointer = reader.ReadUInt32();
                        long returnpos = reader.BaseStream.Position;
                        Effects.IEffect Effect = ReadEffect(Pointer, reader, ref Data);
                        Object.Effects.Add(Effect);
                        reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
                    }
                    for (int i = 0; i < Object.Info.Count; i++)
                    {
                        Anim Anim = Object.Info[i];
                        reader.BaseStream.Seek(Header.OMT + 4 * Anim.AnimID, SeekOrigin.Begin);
                        uint Pointer = reader.ReadUInt32();
                        reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                        uint OMT = reader.ReadUInt32();
                        uint Size = reader.ReadUInt32();
                        reader.BaseStream.Seek(OMT, SeekOrigin.Begin);
                        byte[] OMTFile = reader.ReadBytes((int)Size);
                        Anim.AnimationForYAct = OMTFile;
                        Object.Info[i] = Anim;
                        for (int n = 0; n < EnemyNode.Nodes.Count; n++)
                        {
                            if (EnemyNode.Nodes[n].Text == "Animation")
                            {
                                EnemyNode.Nodes[n].Tag = Anim;
                            }
                        }
                    }
                    EnemyNode.Tag = Object;
                    EN++;
                }
                foreach (uint pointer in EffectPointers)
                {
                    reader.BaseStream.Seek(pointer, SeekOrigin.Begin);
                    ReadEffect(pointer, reader,ref Data);
                }
            }
        }
        public Effects.IEffect ReadEffect(uint Start, BinaryReader reader,ref YactData Data)
        {
            reader.BaseStream.Seek(Start + 44, SeekOrigin.Begin);
            uint EffectType = reader.ReadUInt32();
            reader.BaseStream.Seek(Start + 60, SeekOrigin.Begin);
            uint EffectSubType = reader.ReadUInt32();
            reader.BaseStream.Seek(Start, SeekOrigin.Begin);
            Effects.IEffect Effect;
            switch (EffectType)
            {
                case 0:
                    switch(EffectSubType)
                    {
                        case 0x31:
                        case 0x29:
                            Effects.ScreenFlash ScrFlsh = new Effects.ScreenFlash
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 32, SeekOrigin.Begin);
                            ScrFlsh.Unknown1 = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 48, SeekOrigin.Begin);
                            ScrFlsh.Unknown2 = reader.ReadSingle();
                            ScrFlsh.RGBA.Blue = reader.ReadByte();
                            ScrFlsh.RGBA.Green = reader.ReadByte();
                            ScrFlsh.RGBA.Red = reader.ReadByte();
                            ScrFlsh.RGBA.Alpha = reader.ReadByte();
                            Data.Effects.Add(ScrFlsh);
                            Effect = ScrFlsh;
                            break;
                        case 0X2B:
                            Effects.AfterImage AImage = new Effects.AfterImage
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 32, SeekOrigin.Begin);
                            AImage.Unknown1 = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 48, SeekOrigin.Begin);
                            AImage.Unknown2 = reader.ReadSingle();
                            AImage.Param1 = reader.ReadSingle();
                            AImage.Param2 = reader.ReadSingle();
                            reader.BaseStream.Seek(Start + 64, SeekOrigin.Begin);
                            AImage.Scale = reader.ReadSingle();
                            reader.BaseStream.Seek(Start + 72, SeekOrigin.Begin);
                            AImage.RGBA.Blue = reader.ReadByte();
                            AImage.RGBA.Green = reader.ReadByte();
                            AImage.RGBA.Red = reader.ReadByte();
                            AImage.RGBA.Alpha = reader.ReadByte();
                            Data.Effects.Add(AImage);
                            Effect = AImage;
                            break;
                        case 0x4C:
                            Effects.CtrlVibration Vibration = new Effects.CtrlVibration
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Vibration.Vibration1 = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            Vibration.Vibration2 = reader.ReadUInt32();
                            Data.Effects.Add(Vibration);
                            Effect = Vibration;
                            break;
                        default:
                            Effects.UnknownEffect DefEffect0 = new Effects.UnknownEffect
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneNumber = reader.ReadUInt32(),
                                UnknownData = reader.ReadBytes(76)
                            };
                            Data.Effects.Add(DefEffect0);
                            Effect = DefEffect0;
                            break;

                    }
                    break;
                case 1:
                    switch(EffectSubType)
                    {
                        case 0:
                            Effects.ParticleTrail PTCLTrl = new Effects.ParticleTrail
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneNumber = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 52, SeekOrigin.Begin);
                            PTCLTrl.TrailParam1 = reader.ReadSingle();
                            PTCLTrl.TrailParam2 = reader.ReadSingle();
                            reader.BaseStream.Seek(Start + 64, SeekOrigin.Begin);
                            PTCLTrl.RGBA1.Blue = reader.ReadByte();
                            PTCLTrl.RGBA1.Green = reader.ReadByte();
                            PTCLTrl.RGBA1.Red = reader.ReadByte();
                            PTCLTrl.RGBA1.Alpha = reader.ReadByte();
                            PTCLTrl.RGBA2.Blue = reader.ReadByte();
                            PTCLTrl.RGBA2.Green = reader.ReadByte();
                            PTCLTrl.RGBA2.Red = reader.ReadByte();
                            PTCLTrl.RGBA2.Alpha = reader.ReadByte();
                            PTCLTrl.TrailParam3 = reader.ReadSingle();
                            PTCLTrl.Unknown1 = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 88, SeekOrigin.Begin);
                            PTCLTrl.Unknown2 = reader.ReadUInt32();
                            PTCLTrl.Flags = reader.ReadBytes(4);
                            Data.Effects.Add(PTCLTrl);
                            Effect = PTCLTrl;
                            break;
                        default:
                            Effects.ParticleNormal PTCLNrml = new Effects.ParticleNormal
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneNumber = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 32, SeekOrigin.Begin);
                            PTCLNrml.PTCLParam1 = reader.ReadSingle();
                            reader.BaseStream.Seek(Start + 40, SeekOrigin.Begin);
                            PTCLNrml.PTCLParam2 = reader.ReadSingle();
                            reader.BaseStream.Seek(Start + 60, SeekOrigin.Begin);
                            PTCLNrml.ptclID = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            PTCLNrml.Unknown = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            PTCLNrml.Flags = reader.ReadBytes(4);
                            Data.Effects.Add(PTCLNrml);
                            Effect = PTCLNrml;
                            break;
                    }
                    break;
                case 4:
                    switch(EffectSubType)
                    {
                        default:
                            Effects.UnknownEffect DefEffect4 = new Effects.UnknownEffect
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneNumber = reader.ReadUInt32(),
                                UnknownData = reader.ReadBytes(76)
                            };
                            Data.Effects.Add(DefEffect4);
                            Effect = DefEffect4;
                            break;
                        case 0:
                            Effects.Damage Damage = new Effects.Damage
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Damage.DamageVal = reader.ReadUInt32();
                            Data.Effects.Add(Damage);
                            Effect = Damage;
                            break;
                        case 6:
                            Effects.Loop Loop = new Effects.Loop
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Loop.MaxLoopNum = reader.ReadUInt32();
                            Data.Effects.Add(Loop);
                            Effect = Loop;
                            break;
                        case 7:
                            Effects.ButtonSpam Spam = new Effects.ButtonSpam
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Spam.ID = reader.ReadInt32();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            Spam.Button = reader.ReadUInt16();
                            Spam.Count = reader.ReadUInt16();
                            Data.Effects.Add(Spam);
                            Effect = Spam;
                            break;
                        case 8:
                            Effects.ButtonTiming Timing = new Effects.ButtonTiming
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Timing.ID = reader.ReadInt16();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            Timing.Button = reader.ReadUInt16();
                            Data.Effects.Add(Timing);
                            Effect = Timing;
                            break;
                        case 9:
                            Effects.NormalBranch LoopEnd = new Effects.NormalBranch
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            LoopEnd.ID = reader.ReadInt32();
                            Data.Effects.Add(LoopEnd);
                            Effect = LoopEnd;
                            break;
                        case 13:
                            Effects.CounterBranch CounterBranch = new Effects.CounterBranch
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            CounterBranch.Unknown1 = reader.ReadInt32();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            CounterBranch.Unknown2 = reader.ReadInt32();
                            Data.Effects.Add(CounterBranch);
                            Effect = CounterBranch;
                            break;
                        case 10:
                            Effects.Finish Finish = new Effects.Finish
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            Data.Effects.Add(Finish);
                            Effect = Finish;
                            break;
                        case 11:
                            Effects.CounterUp CounterUp = new Effects.CounterUp
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            CounterUp.Unknown = reader.ReadInt32();
                            Data.Effects.Add(CounterUp);
                            Effect = CounterUp;
                            break;
                        case 12:
                            Effects.CounterReset CounterReset = new Effects.CounterReset
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            CounterReset.Unknown = reader.ReadInt32();
                            Data.Effects.Add(CounterReset);
                            Effect = CounterReset;
                            break;
                        case 18:
                            Effects.ChangeFinishStatus CFinish = new Effects.ChangeFinishStatus
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            CFinish.Status = reader.ReadInt32();
                            Data.Effects.Add(CFinish);
                            Effect = CFinish;
                            break;
                        case 16:
                            Effects.Dead1 Dead = new Effects.Dead1
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Dead.ID = reader.ReadInt32();
                            Data.Effects.Add(Dead);
                            Effect = Dead;
                            break;
                        case 17:
                            Effects.Dead2 Dead2 = new Effects.Dead2
                            {
                                ParentID = reader.ReadUInt32(),
                                FrameStart = reader.ReadSingle(),
                                FrameEnd = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Dead2.ID = reader.ReadInt32();
                            Data.Effects.Add(Dead2);
                            Effect = Dead2;
                            break;
                    }
                    break;
                case 20:
                case 21:
                    Effects.SoundCue Cue = new Effects.SoundCue
                    {
                        ParentID = reader.ReadUInt32(),
                        FrameStart = reader.ReadSingle(),
                        FrameEnd = reader.ReadSingle(),
                        Speed = reader.ReadSingle(),
                        BoneNumber = reader.ReadUInt32(),
                    };
                    reader.BaseStream.Seek(Start + 56, SeekOrigin.Begin);
                    Cue.SoundSpeed = reader.ReadSingle();
                    Cue.ContainerID = reader.ReadUInt16();
                    Cue.VoiceID = reader.ReadUInt16();
                    Data.Effects.Add(Cue);
                    Effect = Cue;
                    break;
                default:
                    Effects.UnknownEffect DefEffect = new Effects.UnknownEffect
                    {
                        ParentID = reader.ReadUInt32(),
                        FrameStart = reader.ReadSingle(),
                        FrameEnd = reader.ReadSingle(),
                        Speed = reader.ReadSingle(),
                        BoneNumber = reader.ReadUInt32(),
                        UnknownData = reader.ReadBytes(76)
                    };
                    Data.Effects.Add(DefEffect);
                    Effect = DefEffect;
                    break;
            }
            return Effect;
        }
    }
}
