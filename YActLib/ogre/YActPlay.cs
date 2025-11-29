using YActLib.Common;

namespace YActLib.ogre1
{
    /// <summary>
    /// A category header in Yakuza 1.
    /// Category 1 YActs are always loaded,
    /// while Category 2 YActs need to be called by YAct set files
    /// </summary>
    public class CategoryHeaderY1
    {
        public uint Pointer;
        public uint Size;
        public uint Index;
        public string Identifier;
        public uint Count;
    }
    /// <summary>
    /// A YActPlayData.bin entry
    /// </summary>
    public class PlayDataY1
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
        public uint ModelPointer;
        public uint ModelCount;
        public uint UNK5Pointer;
        public uint UNK5Count;
        public List<CYActCamera> Cameras;
        public CYActPlayer Player;
        public List<CYActEnemy> Enemies;
        public List<CYActObject> Objects;
        public List<CYActArm> Arms;
        public List<CYActExMotion> ExMotions;
        public List<byte[]> AnimFiles;
        public List<byte[]> ModelFiles;
        public List<byte[]> TextureFiles;
        public List<byte[]> YActChunk1Data; //Present in YAct files,
        public List<byte[]> YActChunk2Data; //no clue what they are so theyre left here
        public void InitEntityLists()
        {
            Cameras = new List<CYActCamera>();
            Enemies = new List<CYActEnemy>();
            Objects = new List<CYActObject>();
            Arms = new List<CYActArm>();
            ExMotions = new List<CYActExMotion>();
            AnimFiles = new List<byte[]>();
            ModelFiles = new List<byte[]>();
        }
    }
    /// <summary>
    /// Camera definition data
    /// </summary>
    public class CYActCamera : CYActEntityBase
    {
        public uint Unk = 0;
    }
    /// <summary>
    /// Player definition and condition data
    /// </summary>
    public class CYActPlayer : CActHumanBase
    {
        public uint Unknown1;
        public uint Condition;
        public byte[] Unknown2;
        public byte[] Unknown3;
    }
    /// <summary>
    /// Enemy definition and condition data
    /// </summary>
    public class CYActEnemy : CActHumanBase
    {
        public uint Condition1;
        public byte[] Unknown1;
        public uint Condition2;
        public byte[] Unknown2;
        public byte[] Unknown3;
    }
    /// <summary>
    /// No clue how this works,
    /// but it can use models provided in the yact apparently
    /// </summary>
    public class CYActObject : CYActEntityBase
    {
        public byte[] Unknown1;
        public uint Unknown2;
        public byte[] Unknown3;
    }
    /// <summary>
    /// Objects and arms possibly use this sometimes
    /// </summary>
    public class CYActExMotion : CYActEntityBase
    {
        public int ModelID = -1;
        public int TextureID = -1;
        public int Unknown1 = -1;
        public byte[] Unknown2 = new byte[52];
        public byte[] OME = null;
        public byte[] TXB = null;
    }
    /// <summary>
    /// Same as Objects,
    /// no clue how it parents to the character
    /// </summary>
    public class CYActArm : CYActEntityBase
    {
        public byte[] Unknown1;
        public byte[] Unknown2;
    }
    /// <summary>
    /// File writer
    /// </summary>
    public class CPlayDataWriterY1
    {
        public static void WriteFile(string path, TreeView TreeView1)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                TreeNode Category1 = (TreeNode)TreeView1.Nodes[0];
                TreeNode Category2 = (TreeNode)TreeView1.Nodes[1];
                writer.Write((uint)2);
                writer.Write(new byte[12]);
                writer.Write((uint)48);
                writer.Write((uint)0);
                writer.Write((uint)1);
                Utils.WriteString(writer, "TCAY", false);
                writer.Write((uint)0);
                writer.Write((uint)0);
                writer.Write((uint)2);
                Utils.WriteString(writer, "TCAY", false);
                writer.Write((uint)0x10);
                writer.Write(Category1.Nodes.Count);
                writer.Write(new byte[8]);
                for (int i = 0; i < Category1.Nodes.Count; i++)
                {
                    TreeNode Node = (TreeNode)Category1.Nodes[i];
                    PlayDataY1 PlayData = Node.Tag as PlayDataY1;
                    WritePlayData(writer, RetreiveYActData(Node));
                }
                uint end1 = (uint)writer.BaseStream.Position;
                uint start2 = (uint)writer.BaseStream.Position;
                writer.Write((uint)0x10);
                writer.Write(Category2.Nodes.Count);
                writer.Write(new byte[8]);
                for (int i = 0; i < Category2.Nodes.Count; i++)
                {
                    TreeNode Node = (TreeNode)Category2.Nodes[i];
                    PlayDataY1 PlayData = Node.Tag as PlayDataY1;
                    WritePlayData(writer, RetreiveYActData(Node));
                }
                uint end2 = (uint)writer.BaseStream.Position;
                writer.BaseStream.Seek(20, SeekOrigin.Begin);
                writer.Write(end1 - 48);
                writer.BaseStream.Seek(32, SeekOrigin.Begin);
                writer.Write(start2);
                writer.Write(end2 - start2);
            }
        }
        public static PlayDataY1 RetreiveYActData(TreeNode Node)
        {
            uint EffectIndex = 0;
            int ModelIndex = 0;
            int AnimIndex = 0;
            int CharaAnimIndex = 0;
            int CamAnimIndex = 0;
            PlayDataY1 PlayData = Node.Tag as PlayDataY1;
            PlayData.InitEntityLists();
            for (int i = 0; i < Node.Nodes.Count; i++)
            {
                TreeNode EntityNode = (TreeNode)Node.Nodes[i];
                switch (EntityNode.Tag)
                {
                    case CYActEntityBase Entity:
                        Entity.InitLists();
                        uint EffectCount = 0;

                        foreach (TreeNode Child in EntityNode.Nodes)
                        {
                            if (Child.Tag is YActAnimation Anim)
                            {

                                Entity.Animations.Add(Anim);
                            }
                            else if (Child.Tag is EFFECT_AUTHORING EFFECT)
                            {
                                Entity.Effects.Add(EFFECT);


                            }
                        }

                        switch (Entity)
                        {
                            case CYActPlayer Player:
                                PlayData.Player = Player;
                                break;
                            case CYActCamera Camera:
                                PlayData.Cameras.Add(Camera);
                                break;
                            case CYActEnemy Enemy:
                                PlayData.Enemies.Add(Enemy);
                                break;
                            case CYActObject Object:
                                PlayData.Objects.Add(Object);
                                break;
                            case CYActArm Arm:
                                PlayData.Arms.Add(Arm);
                                break;
                            case CYActExMotion Model:
                                if (Model.ModelID != -1 && Model.TextureID != -1)
                                {

                                }
                                PlayData.ExMotions.Add(Model);
                                break;

                        }
                        break;
                }
            }

            return PlayData;
        }
        public static void WriteAnimation(BinaryWriter writer, YActAnimation Anim)
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
        public static void WritePlayData(BinaryWriter writer, PlayDataY1 YAct)
        {
            List<uint> AnimPtrs = new List<uint>();
            List<uint> AnimPtrPos = new List<uint>();
            uint Start = (uint)writer.BaseStream.Position;
            writer.Write(YAct.UnkHdr);
            writer.Write(YAct.FileID);
            writer.Write((uint)0);
            writer.Write((uint)128);
            writer.Write((uint)YAct.Cameras.Count);
            writer.Write(new byte[48]);
            foreach (CYActCamera Cam in YAct.Cameras)
            {
                writer.Write(Cam.Unk);
                writer.Write(Cam.EffectIndex);
                writer.Write(Cam.EffectCount);
                AnimPtrPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Cam.Animations.Count);
                writer.Write(new byte[12]);
            }
            uint PlayerPos = (uint)writer.BaseStream.Position - Start;
            if (YAct.PlayerPointer != 0)
            {
                writer.Write(YAct.Player.Unknown1);
                writer.Write(YAct.Player.Condition);
                writer.Write(YAct.Player.Unknown2);
                writer.Write(YAct.Player.EffectIndex);
                writer.Write(YAct.Player.EffectCount);
                writer.Write(YAct.Player.Unknown3);
                AnimPtrPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)YAct.Player.Animations.Count);
                writer.Write(YAct.Player.HealthCondition);
            }
            uint EnemyPos = (uint)writer.BaseStream.Position - Start;
            foreach (CYActEnemy Enemy in YAct.Enemies)
            {
                writer.Write(Enemy.Condition1);
                writer.Write(Enemy.Unknown1);
                writer.Write(Enemy.Condition2);
                writer.Write(Enemy.Unknown2);
                writer.Write(Enemy.EffectIndex);
                writer.Write(Enemy.EffectCount);
                AnimPtrPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Enemy.Animations.Count);
                writer.Write(Enemy.HealthCondition);
            }
            uint ObjectPos = (uint)writer.BaseStream.Position - Start;
            foreach (CYActObject Object in YAct.Objects)
            {
                writer.Write(Object.Unknown1);
                writer.Write(Object.EffectIndex);
                writer.Write(Object.EffectCount);
                writer.Write(Object.Unknown2);
                AnimPtrPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Object.Animations.Count);
                writer.Write(Object.Unknown3);
            }
            uint ModelPos = (uint)writer.BaseStream.Position - Start;
            foreach (CYActExMotion Model in YAct.ExMotions)
            {
                writer.Write(Model.ModelID);
                writer.Write(Model.TextureID);
                writer.Write(Model.Unknown1);
                writer.Write(Model.EffectIndex);
                writer.Write(Model.EffectCount);
                AnimPtrPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Model.Animations.Count);
                writer.Write(Model.Unknown2);
            }
            uint ArmPos = (uint)writer.BaseStream.Position - Start;
            foreach (CYActArm Arm in YAct.Arms)
            {
                writer.Write(Arm.Unknown1);
                writer.Write(Arm.EffectIndex);
                writer.Write(Arm.EffectCount);
                AnimPtrPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Arm.Animations.Count);
                writer.Write(Arm.Unknown2);
            }
            uint C5Pos = (uint)writer.BaseStream.Position - Start;
            foreach (CYActCamera Cam in YAct.Cameras)
            {
                AnimPtrs.Add((uint)writer.BaseStream.Position - Start);

                foreach (YActAnimation Anim in Cam.Animations)
                {
                    WriteAnimation(writer, Anim);
                }
            }
            if (YAct.PlayerPointer != 0)
            {
                AnimPtrs.Add((uint)writer.BaseStream.Position - Start);
                foreach (YActAnimation Anim in YAct.Player.Animations)
                {
                    WriteAnimation(writer, Anim);
                }
            }
            foreach (CYActEnemy Enemy in YAct.Enemies)
            {

                AnimPtrs.Add((uint)writer.BaseStream.Position - Start);

                foreach (YActAnimation Anim in Enemy.Animations)
                {
                    WriteAnimation(writer, Anim);
                }
            }
            foreach (CYActObject Enemy in YAct.Objects)
            {

                AnimPtrs.Add((uint)writer.BaseStream.Position - Start);

                foreach (YActAnimation Anim in Enemy.Animations)
                {
                    WriteAnimation(writer, Anim);
                }
            }
            foreach (CYActExMotion Enemy in YAct.ExMotions)
            {

                AnimPtrs.Add((uint)writer.BaseStream.Position - Start);

                foreach (YActAnimation Anim in Enemy.Animations)
                {
                    WriteAnimation(writer, Anim);
                }
            }
            foreach (CYActArm Enemy in YAct.Arms)
            {

                AnimPtrs.Add((uint)writer.BaseStream.Position - Start);

                foreach (YActAnimation Anim in Enemy.Animations)
                {
                    WriteAnimation(writer, Anim);
                }
            }
            uint EntrySize = (uint)writer.BaseStream.Position - Start;
            int p = 0;
            foreach (uint Pointer in AnimPtrPos)
            {
                writer.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                writer.Write(AnimPtrs[p]);
                p++;
            }
            writer.BaseStream.Seek(Start + 68, SeekOrigin.Begin);
            writer.Write(EntrySize);
            writer.BaseStream.Seek(Start + 80, SeekOrigin.Begin);
            if (YAct.PlayerPointer != 0)
            {
                writer.Write(PlayerPos);
            }
            else
            {
                writer.Write((uint)0);
            }
            writer.BaseStream.Seek(Start + 84, SeekOrigin.Begin);
            writer.Write(EnemyPos);
            writer.Write((uint)YAct.Enemies.Count);
            writer.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
            writer.Write(ObjectPos);
            writer.Write((uint)YAct.Objects.Count);
            writer.BaseStream.Seek(Start + 108, SeekOrigin.Begin);
            writer.Write(ArmPos);
            writer.Write((uint)YAct.Arms.Count);
            writer.BaseStream.Seek(Start + 100, SeekOrigin.Begin);
            writer.Write(ModelPos);
            writer.Write((uint)YAct.ExMotions.Count);
            writer.BaseStream.Seek(Start + 116, SeekOrigin.Begin);
            writer.Write(EntrySize);
            writer.BaseStream.Seek(Start + EntrySize, SeekOrigin.Begin);
        }
    }
    /// <summary>
    /// File reader
    /// </summary>
    public class CPlayDataReaderY1
    {
        public static void ReadYActPlayDataFile(string path, TreeView TreeView1)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                uint CategoryCount = reader.ReadUInt32();
                reader.ReadBytes(12);
                CategoryHeaderY1 C1 = ReadCategoryHeader(reader);
                TreeNode C1Node = new TreeNode { Text = "Category 1" };
                TreeView1.Nodes.Add(C1Node);
                CategoryHeaderY1 C2 = ReadCategoryHeader(reader);
                TreeNode C2Node = new TreeNode { Text = "Category 2" };
                TreeView1.Nodes.Add(C2Node);
                reader.BaseStream.Seek(C1.Pointer, SeekOrigin.Begin);
                reader.ReadBytes(4);
                C1.Count = reader.ReadUInt32();
                reader.ReadBytes(8);
                for (int i = 0; i < C1.Count; i++)
                {
                    ReadPlayData(reader, C1Node);
                }
                reader.BaseStream.Seek(C2.Pointer, SeekOrigin.Begin);
                reader.ReadBytes(4);
                C2.Count = reader.ReadUInt32();
                reader.ReadBytes(8);
                for (int i = 0; i < C2.Count; i++)
                {
                    ReadPlayData(reader, C2Node);
                }
            }
        }
        /// <summary>
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>
        /// Yakuza 1 Category Header
        /// </returns>
        public static CategoryHeaderY1 ReadCategoryHeader(BinaryReader reader)
        {
            CategoryHeaderY1 c = new CategoryHeaderY1()
            {
                Pointer = reader.ReadUInt32(),
                Size = reader.ReadUInt32(),
                Index = reader.ReadUInt32(),
                Identifier = Utils.ReadFixedString(reader, 4)
            };
            return c;

        }
        public static void ReadAnimation(BinaryReader reader, CYActEntityBase Entity, long Start, TreeNode Node)
        {
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(Start + Entity.AnimationPointer, SeekOrigin.Begin);
            for (int i = 0; i < Entity.AnimationCount; i++)
            {
                YActAnimation Anim = new YActAnimation()
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
                TreeNode AnimNode = new TreeNode
                {
                    Text = "Animation",
                    Tag = Anim
                };
                Node.Nodes.Add(AnimNode);
            }
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
        }

        /// <summary>
        /// Reads Play Data and populates the treeview
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>
        /// </returns>
        public static void ReadPlayData(BinaryReader reader, TreeNode CategoryNode)
        {
            long Start = reader.BaseStream.Position;
            PlayDataY1 PlayData = new PlayDataY1()
            {
                UnkHdr = reader.ReadBytes(64),
                FileID = reader.ReadUInt32(),
                Size = reader.ReadUInt32(),
                CamInfoPtr = reader.ReadUInt32(),
                CamInfoCount = reader.ReadUInt32(),
                PlayerPointer = reader.ReadUInt32(),
                EnemiesPointer = reader.ReadUInt32(),
                EnemiesCount = reader.ReadUInt32(),
                ObjectsPointer = reader.ReadUInt32(),
                ObjectsCount = reader.ReadUInt32(),
                ModelPointer = reader.ReadUInt32(),
                ModelCount = reader.ReadUInt32(),
                ArmsPointer = reader.ReadUInt32(),
                ArmsCount = reader.ReadUInt32(),
                UNK5Pointer = reader.ReadUInt32(),
                UNK5Count = reader.ReadUInt32(),
            };
            TreeNode YActNode = new TreeNode()
            {
                Text = PlayData.FileID.ToString(),
                Tag = PlayData
            };
            if (PlayData.PlayerPointer != 0)
            {
                reader.BaseStream.Seek(Start + PlayData.PlayerPointer, SeekOrigin.Begin);
                CYActPlayer Player = new CYActPlayer()
                {
                    Unknown1 = reader.ReadUInt32(),
                    Condition = reader.ReadUInt32(),
                    Unknown2 = reader.ReadBytes(28),
                    EffectIndex = reader.ReadUInt32(),
                    EffectCount = reader.ReadUInt32(),
                    Unknown3 = reader.ReadBytes(8),
                    AnimationPointer = reader.ReadUInt32(),
                    AnimationCount = reader.ReadUInt32(),
                    HealthCondition = reader.ReadUInt32(),
                };
                TreeNode PlayerNode = new TreeNode()
                {
                    Text = "KIRYU",
                    Tag = Player
                };
                ReadAnimation(reader, Player, Start, PlayerNode);
                YActNode.Nodes.Add(PlayerNode);
            }
            reader.BaseStream.Seek(Start + PlayData.EnemiesPointer, SeekOrigin.Begin);
            for (int e = 0; e < PlayData.EnemiesCount; e++)
            {
                CYActEnemy Enemy = new CYActEnemy
                {
                    Condition1 = reader.ReadUInt32(),
                    Unknown1 = reader.ReadBytes(28),
                    Condition2 = reader.ReadUInt32(),
                    Unknown2 = reader.ReadBytes(8),
                    EffectIndex = reader.ReadUInt32(),
                    EffectCount = reader.ReadUInt32(),
                    AnimationPointer = reader.ReadUInt32(),
                    AnimationCount = reader.ReadUInt32(),
                    HealthCondition = reader.ReadUInt32(),
                };
                TreeNode EnemyNode = new TreeNode()
                {
                    Text = $"ENEMY{e}",
                    Tag = Enemy
                };
                ReadAnimation(reader, Enemy, Start, EnemyNode);
                YActNode.Nodes.Add(EnemyNode);
            }
            reader.BaseStream.Seek(Start + PlayData.ObjectsPointer, SeekOrigin.Begin);
            for (int e = 0; e < PlayData.ObjectsCount; e++)
            {
                CYActObject Object = new CYActObject
                {
                    Unknown1 = reader.ReadBytes(24),
                    EffectIndex = reader.ReadUInt32(),
                    EffectCount = reader.ReadUInt32(),
                    Unknown2 = reader.ReadUInt32(),
                    AnimationPointer = reader.ReadUInt32(),
                    AnimationCount = reader.ReadUInt32(),
                    Unknown3 = reader.ReadBytes(20),
                };
                TreeNode ObjectNode = new TreeNode()
                {
                    Text = $"OBJECT{e}",
                    Tag = Object
                };
                ReadAnimation(reader, Object, Start, ObjectNode);
                YActNode.Nodes.Add(ObjectNode);
            }
            reader.BaseStream.Seek(Start + PlayData.ModelPointer, SeekOrigin.Begin);
            for (int e = 0; e < PlayData.ModelCount; e++)
            {
                CYActExMotion Model = new CYActExMotion
                {
                    ModelID = reader.ReadInt32(),
                    TextureID = reader.ReadInt32(),
                    Unknown1 = reader.ReadInt32(),
                    EffectIndex = reader.ReadUInt32(),
                    EffectCount = reader.ReadUInt32(),
                    AnimationPointer = reader.ReadUInt32(),
                    AnimationCount = reader.ReadUInt32(),
                    Unknown2 = reader.ReadBytes(52),
                };
                TreeNode ModelNode = new TreeNode()
                {
                    Text = $"MODEL{e}",
                    Tag = Model
                };
                ReadAnimation(reader, Model, Start, ModelNode);
                YActNode.Nodes.Add(ModelNode);
            }
            reader.BaseStream.Seek(Start + PlayData.ArmsPointer, SeekOrigin.Begin);
            for (int e = 0; e < PlayData.ArmsCount; e++)
            {
                CYActArm Arm = new CYActArm
                {
                    Unknown1 = reader.ReadBytes(16),
                    EffectIndex = reader.ReadUInt32(),
                    EffectCount = reader.ReadUInt32(),
                    AnimationPointer = reader.ReadUInt32(),
                    AnimationCount = reader.ReadUInt32(),
                    Unknown2 = reader.ReadBytes(32),
                };
                TreeNode ArmNode = new TreeNode()
                {
                    Text = $"ARM{e}",
                    Tag = Arm
                };
                ReadAnimation(reader, Arm, Start, ArmNode);
                YActNode.Nodes.Add(ArmNode);
            }
            reader.BaseStream.Seek(Start + PlayData.CamInfoPtr, SeekOrigin.Begin);
            for (int e = 0; e < PlayData.CamInfoCount; e++)
            {
                CYActCamera Camera = new CYActCamera
                {
                    Unk = reader.ReadUInt32(),
                    EffectIndex = reader.ReadUInt32(),
                    EffectCount = reader.ReadUInt32(),
                    AnimationPointer = reader.ReadUInt32(),
                    AnimationCount = reader.ReadUInt32(),
                };
                reader.ReadBytes(12);
                TreeNode CameraNode = new TreeNode()
                {
                    Text = $"CAMERA{e}",
                    Tag = Camera
                };
                ReadAnimation(reader, Camera, Start, CameraNode);
                YActNode.Nodes.Add(CameraNode);
            }
            CategoryNode.Nodes.Add(YActNode);
            reader.BaseStream.Seek(Start + PlayData.Size, SeekOrigin.Begin);
        }
    }
}
