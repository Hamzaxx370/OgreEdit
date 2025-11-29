using System;
using System.Xml.Linq;

namespace YActLib.ogre2
{
    public class YActHeader
    {
        public byte[] unkData;
        public int p_cameras;
        public int n_cameras;
        public int p_characters;
        public int n_characters;
        public int p_objects;
        public int n_objects;
        public int p_arms;
        public int n_arms;
        public int p_models;
        public int n_models;
        public int p_effects;
        public int n_effects;
        public int p_chunk7;
        public int n_chunk7;
        public int p_chunk8;
        public int n_chunk8;
        public int p_anims1;
        public int n_anims1;
        public int p_mdl;
        public int n_mdl;
        public int p_anims2;
        public int n_anims2;
        public int p_tex;
        public int n_tex;
        public int p_chunk13;
        public int n_chunk13;
    }

    /// <summary>
    /// Most useless enum ever
    /// </summary>
    public enum FileTypes
    {
        mtbw,
        omt,
        ome,
        txb
    }

    public class CYActReaderAndWriter
    {
        public static void WriteYAct(string path, TreeNode YActNode)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                YAct YActData = YActNode.Tag as YAct;
                YActData.Cameras = new();
                YActData.Characters = new List<CYActCharacter>();
                YActData.Objects = new List<CYActObject>();
                YActData.Arms = new List<CYActArm>();
                YActData.ExMotions = new List<ogre1.CYActExMotion>();

                YActHeader Header = new YActHeader();
                writer.Write(new byte[136]);

                List<string> Strings = new();
                List<int> StringPtrs = new();
                List<Common.EFFECT_AUTHORING> Effects = new List<Common.EFFECT_AUTHORING>();
                List<uint> EffectPtrs = new();
                List<Common.YActAnimation> Animations = new List<Common.YActAnimation>();
                List<byte[]> Anims1 = new List<byte[]>();
                List<int> Anims1Ptrs = new();
                List<byte[]> Anims2 = new List<byte[]>();
                List<int> Anims2Ptrs = new();
                List<byte[]> Models = new List<byte[]>();
                List<int> ModelPtrs = new();
                List<byte[]> Textures = new List<byte[]>();
                List<int> TexturesPtrs = new();

                uint EffectCount = 0;
                uint AnimCount;

                int CamOffset = 0;
                int CharaOffset = 0;
                int ObjOffset = 0;
                int ArmOffset = 0;
                int MdlOffset = 0;
                int OmeOffset = 0;
                int TxbOffset = 0;

                // Pad data
                Header.p_cameras = (int)writer.BaseStream.Position;
                foreach (TreeNode CamNode in YActNode.Nodes[0].Nodes)
                {
                    ogre1.CYActCamera Cam = CamNode.Tag as ogre1.CYActCamera;
                    Cam.EffectIndex = EffectCount;
                    Cam.EffectCount = (uint)CamNode.Nodes[1].Nodes.Count;
                    Cam.AnimationCount = (uint)CamNode.Nodes[0].Nodes.Count;
                    Cam.Animations = new List<Common.YActAnimation>();
                    foreach (TreeNode AnimNode in CamNode.Nodes[0].Nodes)
                    {
                        Common.YActAnimation Anim = AnimNode.Tag as Common.YActAnimation;
                        Anim.AnimID = Anims1.Count;
                        Anims1.Add(Anim.Animation);
                        Cam.Animations.Add(Anim);
                    }
                    writer.Write(new byte[32]);
                    foreach (TreeNode EffectNode in CamNode.Nodes[1].Nodes)
                    {
                        Common.EFFECT_AUTHORING Effect = EffectNode.Tag as Common.EFFECT_AUTHORING;
                        if (Effect is Common.YACT_EVENT) // Yakuza 2 YAct Event
                            Strings.Add(Effect.Name);
                        Effects.Add(Effect);
                        EffectCount++;
                    }
                    YActData.Cameras.Add(Cam);
                }
                Header.p_characters = (int)writer.BaseStream.Position;
                foreach (TreeNode CharaNode in YActNode.Nodes[1].Nodes)
                {
                    CYActCharacter Chara = CharaNode.Tag as CYActCharacter;
                    Strings.Add(Chara.Name);
                    Chara.EffectIndex = EffectCount;
                    Chara.EffectCount = (uint)CharaNode.Nodes[1].Nodes.Count;
                    Chara.AnimationCount = (uint)CharaNode.Nodes[0].Nodes.Count;
                    Chara.Animations = new List<Common.YActAnimation>();
                    foreach (TreeNode AnimNode in CharaNode.Nodes[0].Nodes)
                    {
                        Common.YActAnimation Anim = AnimNode.Tag as Common.YActAnimation;
                        Anim.AnimID = Anims2.Count;
                        Anims2.Add(Anim.Animation);
                        Chara.Animations.Add(Anim);
                    }
                    writer.Write(new byte[20]);
                    foreach (TreeNode EffectNode in CharaNode.Nodes[1].Nodes)
                    {
                        Common.EFFECT_AUTHORING Effect = EffectNode.Tag as Common.EFFECT_AUTHORING;
                        if (Effect is Common.YACT_EVENT) // Yakuza 2 YAct Event
                            Strings.Add(Effect.Name);
                        Effects.Add(Effect);
                        EffectCount++;
                    }
                    YActData.Characters.Add(Chara);
                }
                Header.p_objects = (int)writer.BaseStream.Position;
                foreach (TreeNode ObjNode in YActNode.Nodes[2].Nodes)
                {
                    CYActObject Obj = ObjNode.Tag as CYActObject;
                    Strings.Add(Obj.Name);
                    Obj.EffectIndex = EffectCount;
                    Obj.EffectCount = (uint)ObjNode.Nodes[1].Nodes.Count;
                    Obj.AnimationCount = (uint)ObjNode.Nodes[0].Nodes.Count;
                    Obj.Animations = new List<Common.YActAnimation>();
                    foreach (TreeNode AnimNode in ObjNode.Nodes[0].Nodes)
                    {
                        Common.YActAnimation Anim = AnimNode.Tag as Common.YActAnimation;
                        if (Anim.Animation != null)
                        {
                            Anim.AnimID = Anims2.Count;
                            Anims2.Add(Anim.Animation);
                        }
                        else
                        {
                            Anim.AnimID = -1;
                            Anim.Unknown1 = 0xFFFFFFFF;
                        }

                        Obj.Animations.Add(Anim);
                    }
                    writer.Write(new byte[24]);
                    foreach (TreeNode EffectNode in ObjNode.Nodes[1].Nodes)
                    {
                        Common.EFFECT_AUTHORING Effect = EffectNode.Tag as Common.EFFECT_AUTHORING;
                        if (Effect is Common.YACT_EVENT) // Yakuza 2 YAct Event
                            Strings.Add(Effect.Name);
                        Effects.Add(Effect);
                        EffectCount++;
                    }
                    YActData.Objects.Add(Obj);
                }
                Header.p_arms = (int)writer.BaseStream.Position;
                foreach (TreeNode ArmNode in YActNode.Nodes[3].Nodes)
                {
                    CYActArm Arm = ArmNode.Tag as CYActArm;
                    Strings.Add(Arm.Name);
                    Arm.EffectIndex = EffectCount;
                    Arm.EffectCount = (uint)ArmNode.Nodes[1].Nodes.Count;
                    Arm.AnimationCount = (uint)ArmNode.Nodes[0].Nodes.Count;
                    Arm.Animations = new List<Common.YActAnimation>();
                    foreach (TreeNode AnimNode in ArmNode.Nodes[0].Nodes)
                    {
                        Common.YActAnimation Anim = AnimNode.Tag as Common.YActAnimation;
                        if (Anim.Animation != null)
                        {
                            Anim.AnimID = Anims2.Count;
                            Anims2.Add(Anim.Animation);
                        }
                        else
                        {
                            Anim.AnimID = -1;
                            Anim.Unknown1 = 0xFFFFFFFF;
                        }
                        Arm.Animations.Add(Anim);
                    }
                    writer.Write(new byte[24]);
                    foreach (TreeNode EffectNode in ArmNode.Nodes[1].Nodes)
                    {
                        Common.EFFECT_AUTHORING Effect = EffectNode.Tag as Common.EFFECT_AUTHORING;
                        if (Effect is Common.YACT_EVENT) // Yakuza 2 YAct Event
                            Strings.Add(Effect.Name);
                        Effects.Add(Effect);
                        EffectCount++;
                    }
                    YActData.Arms.Add(Arm);
                }
                Header.p_models = (int)writer.BaseStream.Position;
                foreach (TreeNode MdlNode in YActNode.Nodes[4].Nodes)
                {
                    ogre1.CYActExMotion Mesh = MdlNode.Tag as ogre1.CYActExMotion;
                    Mesh.EffectIndex = EffectCount;
                    Mesh.EffectCount = (uint)MdlNode.Nodes[1].Nodes.Count;
                    Mesh.AnimationCount = (uint)MdlNode.Nodes[0].Nodes.Count;
                    Mesh.Animations = new List<Common.YActAnimation>();
                    if (Mesh.OME != null)
                    {
                        Mesh.ModelID = Models.Count;
                        Models.Add(Mesh.OME);
                    }
                    if (Mesh.TXB != null)
                    {
                        Mesh.TextureID = Textures.Count;
                        Textures.Add(Mesh.TXB);
                    }

                    foreach (TreeNode AnimNode in MdlNode.Nodes[0].Nodes)
                    {
                        Common.YActAnimation Anim = AnimNode.Tag as Common.YActAnimation;
                        Anim.AnimID = Anims2.Count;
                        Anims2.Add(Anim.Animation);
                        Mesh.Animations.Add(Anim);
                    }
                    writer.Write(new byte[80]);
                    foreach (TreeNode EffectNode in MdlNode.Nodes[1].Nodes)
                    {
                        Common.EFFECT_AUTHORING Effect = EffectNode.Tag as Common.EFFECT_AUTHORING;
                        if (Effect is Common.YACT_EVENT) // Yakuza 2 YAct Event
                            Strings.Add(Effect.Name);
                        Effects.Add(Effect);
                        EffectCount++;
                    }
                    YActData.ExMotions.Add(Mesh);
                }
                foreach (ogre1.CYActCamera Cam in YActData.Cameras)
                {
                    Cam.AnimationPointer = WriteAnims(writer, Cam.Animations);
                }
                foreach (CYActCharacter Chara in YActData.Characters)
                {
                    Chara.AnimationPointer = WriteAnims(writer, Chara.Animations);
                }
                foreach (CYActObject Obj in YActData.Objects)
                {
                    Obj.AnimationPointer = WriteAnims(writer, Obj.Animations);
                }
                foreach (CYActArm Arm in YActData.Arms)
                {
                    Arm.AnimationPointer = WriteAnims(writer, Arm.Animations);
                }
                foreach (ogre1.CYActExMotion Mdl in YActData.ExMotions)
                {
                    Mdl.AnimationPointer = WriteAnims(writer, Mdl.Animations);
                }

                Header.p_effects = (int)writer.BaseStream.Position;
                foreach (Common.EFFECT_AUTHORING Effect in Effects)
                {
                    writer.Write(new byte[16]);
                
                }
                Header.p_chunk7 = (int)writer.BaseStream.Position;
                foreach (byte[] Data in YActData.YActChunk1Data)
                {
                    writer.Write(new byte[4]);
                }
                Header.p_chunk8 = (int)writer.BaseStream.Position;
                foreach (byte[] Data in YActData.YActChunk2Data)
                {
                    writer.Write(new byte[4]);
                }
                Header.p_anims1 = (int)writer.BaseStream.Position;
                foreach (byte[] Data in Anims1)
                {
                    writer.Write(new byte[4]);
                }
                Header.p_mdl = (int)writer.BaseStream.Position;
                foreach (byte[] Data in Models)
                {
                    writer.Write(new byte[4]);
                }
                Header.p_anims2 = (int)writer.BaseStream.Position;
                foreach (byte[] Data in Anims2)
                {
                    writer.Write(new byte[4]);
                }
                Header.p_tex = (int)writer.BaseStream.Position;
                foreach (byte[] Data in Textures)
                {
                    writer.Write(new byte[4]);
                }

                foreach (string s in Strings)
                {
                    StringPtrs.Add((int)writer.BaseStream.Position);
                    Utils.WriteString(writer, s, true);
                }
                Utils.AlignData(writer, 16);

                Common.BaseReaderAndWriter.WriteEffects(writer, Effects, EffectPtrs);

                Utils.AlignData(writer, 16);

                List<int> C1Ptrs = new();
                List<int> C2Ptrs = new();
                foreach (byte[] Data in YActData.YActChunk1Data)
                {
                    C1Ptrs.Add((int)writer.BaseStream.Position);
                    writer.Write(Data);
                }

                Utils.AlignData(writer, 16);

                foreach (byte[] Data in YActData.YActChunk2Data)
                {
                    C2Ptrs.Add((int)writer.BaseStream.Position);
                    writer.Write(Data);
                }

                foreach (byte[] Data in Anims1)
                {
                    Utils.AlignData(writer, 16);
                    Anims1Ptrs.Add((int)writer.BaseStream.Position);
                    writer.Write((int)(writer.BaseStream.Position + 16));
                    writer.Write((int)Data.Length);
                    writer.Write(new byte[8]);
                    writer.Write(Data);
                }

                foreach (byte[] Data in Models)
                {
                    Utils.AlignData(writer, 16);
                    ModelPtrs.Add((int)writer.BaseStream.Position);
                    writer.Write((int)(writer.BaseStream.Position + 16));
                    writer.Write((int)Data.Length);
                    writer.Write(new byte[8]);
                    writer.Write(Data);
                }
                foreach (byte[] Data in Anims2)
                {
                    Utils.AlignData(writer, 16);
                    Anims2Ptrs.Add((int)writer.BaseStream.Position);
                    writer.Write((int)(writer.BaseStream.Position + 16));
                    writer.Write((int)Data.Length);
                    writer.Write(new byte[8]);
                    writer.Write(Data);
                }
                foreach (byte[] Data in Textures)
                {
                    Utils.AlignData(writer, 16);
                    TexturesPtrs.Add((int)writer.BaseStream.Position);
                    writer.Write((int)(writer.BaseStream.Position + 16));
                    writer.Write((int)Data.Length);
                    writer.Write(new byte[8]);
                    writer.Write(Data);
                }

                writer.Seek(0, SeekOrigin.Begin);
                writer.Write(YActData.Header.unkData);
                writer.Write(Header.p_cameras);
                writer.Write((int)YActData.Cameras.Count);
                writer.Write(Header.p_characters);
                writer.Write((int)YActData.Characters.Count);
                writer.Write(Header.p_objects);
                writer.Write((int)YActData.Objects.Count);
                writer.Write(Header.p_arms);
                writer.Write((int)YActData.Arms.Count);
                writer.Write(Header.p_models);
                writer.Write((int)YActData.ExMotions.Count);
                writer.Write(Header.p_effects);
                writer.Write((int)Effects.Count);
                writer.Write(Header.p_chunk7);
                writer.Write((int)YActData.YActChunk1Data.Count);
                writer.Write(Header.p_chunk8);
                writer.Write((int)YActData.YActChunk2Data.Count);
                writer.Write(Header.p_anims1);
                writer.Write((int)Anims1.Count);
                writer.Write(Header.p_mdl);
                writer.Write((int)Models.Count);
                writer.Write(Header.p_anims2);
                writer.Write((int)Anims2.Count);
                writer.Write(Header.p_tex);
                writer.Write((int)Textures.Count);
                writer.Write(Header.p_effects);
                writer.Write((int)0);

                writer.Seek(Header.p_cameras, SeekOrigin.Begin);
                foreach (ogre1.CYActCamera Cam in YActData.Cameras)
                {
                    writer.Write(Cam.Unk);
                    writer.Write(Cam.EffectIndex);
                    writer.Write(Cam.EffectCount);
                    writer.Write(Cam.AnimationPointer);
                    writer.Write(Cam.AnimationCount);
                    writer.Write(new byte[12]);
                }
                writer.Seek(Header.p_characters, SeekOrigin.Begin);
                foreach (CYActCharacter Chara in YActData.Characters)
                {
                    writer.Write((int)StringPtrs[Strings.IndexOf(Chara.Name)]);
                    writer.Write(Chara.AnimationPointer);
                    writer.Write(Chara.AnimationCount);
                    writer.Write(Chara.EffectIndex);
                    writer.Write(Chara.EffectCount);
                }
                writer.Seek(Header.p_objects, SeekOrigin.Begin);
                foreach (CYActObject Obj in YActData.Objects)
                {
                    writer.Write((int)StringPtrs[Strings.IndexOf(Obj.Name)]);
                    writer.Write(Obj.Unk);
                    writer.Write(Obj.EffectIndex);
                    writer.Write(Obj.EffectCount);
                    writer.Write(Obj.AnimationPointer);
                    writer.Write(Obj.AnimationCount);
                }
                writer.Seek(Header.p_arms, SeekOrigin.Begin);
                foreach (CYActArm Arm in YActData.Arms)
                {
                    writer.Write((int)StringPtrs[Strings.IndexOf(Arm.Name)]);
                    writer.Write(Arm.Unk);
                    writer.Write(Arm.EffectIndex);
                    writer.Write(Arm.EffectCount);
                    writer.Write(Arm.AnimationPointer);
                    writer.Write(Arm.AnimationCount);
                }
                writer.Seek(Header.p_models, SeekOrigin.Begin);
                foreach (ogre1.CYActExMotion Mdl in YActData.ExMotions)
                {
                    writer.Write(Mdl.ModelID);
                    writer.Write(Mdl.TextureID);
                    writer.Write(Mdl.Unknown1);
                    writer.Write(Mdl.EffectIndex);
                    writer.Write(Mdl.EffectCount);
                    writer.Write(Mdl.AnimationPointer);
                    writer.Write(Mdl.AnimationCount);
                    writer.Write(Mdl.Unknown2);
                }
                int i = 0;
                writer.Seek(Header.p_effects, SeekOrigin.Begin);
                foreach (Common.EFFECT_AUTHORING Effect in Effects)
                {
                    if (Effect is Common.YACT_EVENT)
                        writer.Write((int)StringPtrs[Strings.IndexOf(Effect.Name)]);
                    else
                        writer.Write((int)0);

                    writer.Write(new byte[8]);
                    writer.Write(EffectPtrs[i]);
                    i++;
                }

                writer.Seek(Header.p_chunk7, SeekOrigin.Begin);
                foreach (uint ptr in C1Ptrs)
                {
                    writer.Write(ptr);
                }
                writer.Seek(Header.p_chunk8, SeekOrigin.Begin);
                foreach (uint ptr in C2Ptrs)
                {
                    writer.Write(ptr);
                }
                writer.Seek(Header.p_anims1, SeekOrigin.Begin);
                foreach (uint ptr in Anims1Ptrs)
                {
                    writer.Write(ptr);
                }
                writer.Seek(Header.p_anims2, SeekOrigin.Begin);
                foreach (uint ptr in Anims2Ptrs)
                {
                    writer.Write(ptr);
                }
                writer.Seek(Header.p_mdl, SeekOrigin.Begin);
                foreach (uint ptr in ModelPtrs)
                {
                    writer.Write(ptr);
                }
                writer.Seek(Header.p_tex, SeekOrigin.Begin);
                foreach (uint ptr in TexturesPtrs)
                {
                    writer.Write(ptr);
                }

            }
        }
        public static uint WriteAnims(BinaryWriter writer, List<Common.YActAnimation> Anims)
        {
            uint Position = (uint)writer.BaseStream.Position;
            foreach (Common.YActAnimation Anim in Anims)
            {
                writer.Write(Anim.Unk);
                writer.Write(Anim.FrameStart);
                writer.Write(Anim.FrameEnd);
                writer.Write(Anim.Speed);
                writer.Write(Anim.AnimID);
                writer.Write(Anim.Unknown1);
                writer.Write(Anim.Unknown2);
                writer.Write(Anim.Unknown3);
            }
            return Position;
        }
        public static byte[] GetFile(BinaryReader reader, YActHeader Header, int Type, int Index)
        {
            int Pointer = 0;
            if (Type == 0)
            {
                Pointer = Header.p_anims1; //mtbw
            }
            else if (Type == 1)
            {
                Pointer = Header.p_anims2; //omt
            }
            else if (Type == 2)
            {
                Pointer = Header.p_mdl; //ome
            }
            else if (Type == 3)
            {
                Pointer = Header.p_tex; //txb
            }

            reader.BaseStream.Seek(Pointer + (4 * Index), SeekOrigin.Begin);
            int IPointer = reader.ReadInt32();
            reader.BaseStream.Seek(IPointer, SeekOrigin.Begin);
            int FPointer = reader.ReadInt32();
            int FSize = reader.ReadInt32();
            reader.BaseStream.Seek(FPointer, SeekOrigin.Begin);
            return reader.ReadBytes(FSize);
        }

        public static void GetAnimation(BinaryReader reader, YActHeader header, TreeNode Node, int Type)
        {
            int offset = reader.ReadInt32();
            int count = reader.ReadInt32();
            long Pos = reader.BaseStream.Position;
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                Common.YActAnimation Anim = new Common.YActAnimation
                {
                    Unk = reader.ReadUInt32(),
                    FrameStart = reader.ReadSingle(),
                    FrameEnd = reader.ReadSingle(),
                    Speed = reader.ReadSingle(),
                    AnimID = reader.ReadInt32(),
                    Unknown1 = reader.ReadUInt32(),
                    Unknown2 = reader.ReadUInt32(),
                    Unknown3 = reader.ReadUInt32(),
                };
                Anim.Animation = GetFile(reader, header, Type, Anim.AnimID);
                TreeNode AnimNode = new TreeNode
                {
                    Text = "Animation",
                    Tag = Anim
                };
                Node.Nodes.Add(AnimNode);
            }
            reader.BaseStream.Seek(Pos, SeekOrigin.Begin);
        }

        public static string GetName(BinaryReader reader)
        {
            int Pointer = reader.ReadInt32();
            long Pos = reader.BaseStream.Position;
            if (Pointer == 0)
                return "";
            reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
            string Name = Utils.ReadString(reader);
            reader.BaseStream.Seek(Pos, SeekOrigin.Begin);
            return Name;
        }

        public static void GetEffect(BinaryReader reader, TreeNode Node)
        {
            string Name = GetName(reader);
            reader.ReadBytes(8);
            TreeNode ENode = Common.BaseReaderAndWriter.ReadEffect(reader.ReadUInt32(), reader, 1, Node);
            if (Name != "")
            {
                ENode.Text = Name;
                Common.EFFECT_AUTHORING Effect = ENode.Tag as Common.EFFECT_AUTHORING;
                Effect.Name = Name;
            }
        }

        public static void GetCamera(BinaryReader reader, TreeNode YActCamNode, YActHeader header)
        {
            reader.BaseStream.Seek(header.p_cameras, SeekOrigin.Begin);
            for (int i = 0; i < header.n_cameras; i++)
            {
                ogre1.CYActCamera Camera = new ogre1.CYActCamera
                {
                    Unk = reader.ReadUInt32(),
                    EffectIndex = reader.ReadUInt32(),
                    EffectCount = reader.ReadUInt32(),
                };
                TreeNode CameraNode = new TreeNode()
                {
                    Text = $"CAMERA{i}",
                    Tag = Camera
                };

                CameraNode.Nodes.Add(new TreeNode
                {
                    Text = "Animations"
                });

                CameraNode.Nodes.Add(new TreeNode
                {
                    Text = "Effects"
                });

                YActCamNode.Nodes.Add(CameraNode);
                GetAnimation(reader, header, CameraNode.Nodes[0], (int)FileTypes.mtbw);
                reader.ReadBytes(12);
                long Pos = reader.BaseStream.Position;
                for (uint e = Camera.EffectIndex; e < Camera.EffectIndex + Camera.EffectCount; e++)
                {
                    reader.BaseStream.Seek(header.p_effects + (16 * e), SeekOrigin.Begin);
                    GetEffect(reader, CameraNode.Nodes[1]);
                }
                reader.BaseStream.Seek(Pos, SeekOrigin.Begin);
            }
        }

        public static void GetChara(BinaryReader reader, TreeNode YActCharaNode, YActHeader header)
        {
            reader.BaseStream.Seek(header.p_characters, SeekOrigin.Begin);
            for (int i = 0; i < header.n_characters; i++)
            {
                string Name = GetName(reader);
                foreach (TreeNode Node in YActCharaNode.Nodes)
                {
                    if (Name == Node.Text)
                    {
                        GetAnimation(reader, header, Node.Nodes[0], (int)FileTypes.omt);
                        int EffectST = reader.ReadInt32();
                        int EffectC = reader.ReadInt32();
                        long Pos = reader.BaseStream.Position;
                        for (int e = EffectST; e < EffectST + EffectC; e++)
                        {
                            reader.BaseStream.Seek(header.p_effects + (16 * e), SeekOrigin.Begin);
                            GetEffect(reader,Node.Nodes[1]);
                        }
                        reader.BaseStream.Seek(Pos, SeekOrigin.Begin);
                    }
                }
            }
        }

        public static void GetObject(BinaryReader reader, TreeNode YActObjNode, YActHeader header)
        {
            reader.BaseStream.Seek(header.p_objects, SeekOrigin.Begin);
            for (int i = 0; i < header.n_objects; i++)
            {
                string Name = GetName(reader);
                foreach (TreeNode Node in YActObjNode.Nodes)
                {
                    if (Name == Node.Text)
                    {
                        CYActObject Object = Node.Tag as CYActObject;
                        Object.Unk = reader.ReadInt32();
                        int EffectST = reader.ReadInt32();
                        int EffectC = reader.ReadInt32();
                        GetAnimation(reader, header, Node.Nodes[0], (int)FileTypes.omt);
                        long Pos = reader.BaseStream.Position;
                        for (int e = EffectST; e < EffectST + EffectC; e++)
                        {
                            reader.BaseStream.Seek(header.p_effects + (16 * e), SeekOrigin.Begin);
                            GetEffect(reader, Node.Nodes[1]);
                        }
                        reader.BaseStream.Seek(Pos, SeekOrigin.Begin);
                    }
                }
            }
        }

        public static void GetArm(BinaryReader reader, TreeNode YActArmNode, YActHeader header)
        {
            reader.BaseStream.Seek(header.p_arms, SeekOrigin.Begin);
            for (int i = 0; i < header.n_arms; i++)
            {
                string Name = GetName(reader);
                foreach (TreeNode Node in YActArmNode.Nodes)
                {
                    if (Name == Node.Text)
                    {
                        CYActArm Arm = Node.Tag as CYActArm;
                        Arm.Unk = reader.ReadInt32();
                        int EffectST = reader.ReadInt32();
                        int EffectC = reader.ReadInt32();
                        GetAnimation(reader, header, Node.Nodes[0], (int)FileTypes.omt);
                        long Pos = reader.BaseStream.Position;
                        for (int e = EffectST; e < EffectST + EffectC; e++)
                        {
                            reader.BaseStream.Seek(header.p_effects + (16 * e), SeekOrigin.Begin);
                            GetEffect(reader, Node.Nodes[1]);
                        }
                        reader.BaseStream.Seek(Pos, SeekOrigin.Begin);
                    }
                }
            }
        }

        public static void GetModel(BinaryReader reader, TreeNode YActMdlNode, YActHeader header)
        {
            reader.BaseStream.Seek(header.p_models, SeekOrigin.Begin);
            for (int i = 0; i < header.n_models; i++)
            {
                ogre1.CYActExMotion Model = new ogre1.CYActExMotion
                {
                    ModelID = reader.ReadInt32(),
                    TextureID = reader.ReadInt32(),
                    Unknown1 = reader.ReadInt32(),
                    EffectIndex = reader.ReadUInt32(),
                    EffectCount = reader.ReadUInt32(),
                };

                TreeNode ModelNode = new TreeNode()
                {
                    Text = $"ExMotion{i}",
                    Tag = Model
                };

                ModelNode.Nodes.Add(new TreeNode
                {
                    Text = "Animations"
                });

                ModelNode.Nodes.Add(new TreeNode
                {
                    Text = "Effects"
                });

                YActMdlNode.Nodes.Add(ModelNode);
                GetAnimation(reader, header, ModelNode.Nodes[0], (int)FileTypes.omt);
                Model.Unknown2 = reader.ReadBytes(52);
                long Pos = reader.BaseStream.Position;
                if (Model.ModelID != -1)
                    Model.OME = GetFile(reader, header, (int)FileTypes.ome, Model.ModelID);
                if (Model.TextureID != -1)
                    Model.TXB = GetFile(reader, header, (int)FileTypes.txb, Model.TextureID);
                
                for (uint e = Model.EffectIndex; e < Model.EffectIndex + Model.EffectCount; e++)
                {
                    reader.BaseStream.Seek(header.p_effects + (16 * e), SeekOrigin.Begin);
                    GetEffect(reader, ModelNode.Nodes[1]);
                }
                reader.BaseStream.Seek(Pos, SeekOrigin.Begin);
            }
        }

        public static void ReadYAct(string path, TreeNode YActNode)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                YActHeader Header = new YActHeader
                {
                    unkData = reader.ReadBytes(32),
                    p_cameras = reader.ReadInt32(),
                    n_cameras = reader.ReadInt32(),
                    p_characters = reader.ReadInt32(),
                    n_characters = reader.ReadInt32(),
                    p_objects = reader.ReadInt32(),
                    n_objects = reader.ReadInt32(),
                    p_arms = reader.ReadInt32(),
                    n_arms = reader.ReadInt32(),
                    p_models = reader.ReadInt32(),
                    n_models = reader.ReadInt32(),
                    p_effects = reader.ReadInt32(),
                    n_effects = reader.ReadInt32(),
                    p_chunk7 = reader.ReadInt32(),
                    n_chunk7 = reader.ReadInt32(),
                    p_chunk8 = reader.ReadInt32(),
                    n_chunk8 = reader.ReadInt32(),
                    p_anims1 = reader.ReadInt32(),
                    n_anims1 = reader.ReadInt32(),
                    p_mdl = reader.ReadInt32(),
                    n_mdl = reader.ReadInt32(),
                    p_anims2 = reader.ReadInt32(),
                    n_anims2 = reader.ReadInt32(),
                    p_tex = reader.ReadInt32(),
                    n_tex = reader.ReadInt32(),
                    p_chunk13 = reader.ReadInt32(),
                    n_chunk13 = reader.ReadInt32(),
                };

                YAct Y = YActNode.Tag as YAct;
                Y.Header = Header;
                Y.YActChunk1Data = new List<byte[]>();
                Y.YActChunk2Data = new List<byte[]>();

                reader.BaseStream.Seek(Header.p_chunk7, SeekOrigin.Begin);
                for (int i = 0; i < Header.n_chunk7; i++)
                {
                    int Pointer = reader.ReadInt32();
                    long pos = reader.BaseStream.Position;
                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                    Y.YActChunk1Data.Add(reader.ReadBytes(32));
                    reader.BaseStream.Seek(pos, SeekOrigin.Begin);
                }

                reader.BaseStream.Seek(Header.p_chunk8, SeekOrigin.Begin);
                for (int i = 0; i < Header.n_chunk8; i++)
                {
                    int Pointer = reader.ReadInt32();
                    long pos = reader.BaseStream.Position;
                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                    Y.YActChunk2Data.Add(reader.ReadBytes(32));
                    reader.BaseStream.Seek(pos, SeekOrigin.Begin);
                }

                GetCamera(reader, YActNode.Nodes[0], Header);
                GetChara(reader, YActNode.Nodes[1], Header);
                GetObject(reader, YActNode.Nodes[2], Header);
                GetArm(reader, YActNode.Nodes[3], Header);
                GetModel(reader, YActNode.Nodes[4], Header);
            }
        }
    }
}