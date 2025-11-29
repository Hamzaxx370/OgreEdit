using System;
using System.Text;

namespace YActLib.ogre2
{
    /// <summary>
    /// Special effect type used for PlayData
    /// </summary>
    public class CYActEvent
    {
        public string Name = "EFFECT_";
        public uint NamePtr;
        public uint EventType;
    }

    /// <summary>
    /// Extended entity data used for PlayData
    /// </summary>
    public class CYActPlayEntity : Common.CYActEntityBase
    {
        public string Name = "ENTITY";
        public uint NamePtr;
    }

    /// <summary>
    /// Conditions used for every YActPlayEntity
    /// </summary>
    public class CYActCondition
    {
        public int Type;
    }

    public class Header
    {
        public uint Pointer;
        public uint Size;
        public uint Unk;
        public string ID;
        public uint Pointer1;
        public uint Count1;
        public uint Pointer2;
        public uint Count2;
    }
    public class YAct
    {
        public string Name;
        public uint FileID;
        public uint unk1;
        public uint unk2;
        public uint unk3;
        public uint CharaPtr;
        public uint CharaCount;
        public uint ObjectPtr;
        public uint ObjectCount;
        public uint ArmsPtr;
        public uint ArmsCount;
        public uint HactEventPtr;
        public uint HactEventCount;
        public List<ogre1.CYActCamera> Cameras = new();
        public List<CYActCharacter> Characters = new List<CYActCharacter>();
        public List<CYActObject> Objects = new List<CYActObject>();
        public List<CYActArm> Arms = new List<CYActArm>();
        public List<CYActEvent> HactEvents = new List<CYActEvent>();
        public List<ogre1.CYActExMotion> ExMotions = new List<ogre1.CYActExMotion>();

        public YActHeader Header; // For the weird identifier
        public List<byte[]> YActChunk1Data = new List<byte[]>(); //Present in YAct files,
        public List<byte[]> YActChunk2Data = new List<byte[]>(); //no clue what they are so theyre left here
    }

    /// <summary>
    /// Used for both players and enemies
    /// </summary>
    public class CYActCharacter : CYActPlayEntity
    {
        public byte[] Unknown1 = { 1, 0, 0, 0, 0, 0, 0, 0};
        public uint StatusConditions = 4097;
        public int Unknown2 = 0;
        public uint ArmsIDPtr;
        public byte[] Unknown3 = { 0, 0, 0, 0, 40, 0, 0, 0 };
        public uint ConditionPtr1;
        public uint ConditionCount1;
        public uint Unknown4 = 0;
        public uint ConditionPtr2;
        public uint ConditionCount2;
        public uint Unknown5 = 0;

        public string ArmsID = "";
        public List<CYActCondition> Conditions1;
        public List<CYActCondition> Conditions2;
    }

    /// <summary>
    /// used for stage objects mostly
    /// </summary>
    public class CYActObject : CYActPlayEntity
    {
        public byte[] Unknown1 = { 255, 255, 255, 255, 0, 0, 0, 0 };
        public uint ArmIDPtr;
        public string ArmID = "";
        public uint ConditionPtr;
        public uint ConditionCount;
        public uint Unknown2 = 0;
        public List<CYActCondition> Conditions;

        //YAct Varaibles

        public int Unk = -1;
    }

    /// <summary>
    /// used for parenting and spawning arms
    /// </summary>
    public class CYActArm : CYActPlayEntity
    {
        public uint WeaponCategory = 0;
        public string ParentName = "ENTITY";
        public uint ParentPtr;
        public uint ArmIDPtr;
        public string ArmID = "";

        //YAct Varaibles

        public int Unk = -1;
    }

    // everything here is mostly identical to base effects from Common.Effects.cs

    public class EFFECT_DAMAGE : CYActEvent
    {
        public uint DamageVal = 200;

        public EFFECT_DAMAGE() { EventType = 0; }

    }
    public class EFFECT_RENDA : CYActEvent
    {
        public int ID = 0;

        public uint Button = 1;

        public uint Count = 6;

        public byte[] IDs = { 255, 255, 255, 255 };

        public EFFECT_RENDA() { EventType = 7; }
    }

    public class EFFECT_TIMING_OK : CYActEvent
    {
        public int ID = 0;

        public uint Button = 1;

        public byte[] IDs = { 255, 255, 255, 255 };

        public EFFECT_TIMING_OK() { EventType = 8; }
    }

    public class EFFECT_FINISH_STATUS : CYActEvent
    {
        public uint Status = 0;

        public byte[] IDs = { 255, 255, 255, 255 };

        public EFFECT_FINISH_STATUS() { EventType = 17; }
    }
    public class EFFECT_TIMING_NG : CYActEvent
    {
        public int ID = 0;

        public uint Button = 1;

        public byte[] IDs = { 255, 255, 255, 255 };

        public EFFECT_TIMING_NG() { EventType = 9; }
    }
    public class EFFECT_FINISH : CYActEvent
    {
        public byte[] IDs = { 255, 255, 255, 255 };

        public EFFECT_FINISH() { EventType = 11; }
    }

    public class EFFECT_DEAD : CYActEvent
    {
        public int ID = 0;

        public EFFECT_DEAD() { EventType = 16; }
    }
    public class EFFECT_NORMAL_BRANCH : CYActEvent
    {
        public byte[] IDs = { 255, 255, 255, 255 };

        public EFFECT_NORMAL_BRANCH() { EventType = 10; }
    }

    public class HG_USE : CYActEvent
    {
        public uint HeatLoss = 4000;

        public byte[] IDs = { 255, 255, 255, 255 };

        public HG_USE() { EventType = 20; }
    }

    public class HG_CHK : CYActEvent
    {
        public int ID = 0;

        public uint Unknown = 1;

        public HG_CHK() { EventType = 19; }
    }
    public class EFFECT_LOOP : CYActEvent
    {
        public uint MaxLoopNum = 10;

        public byte[] IDs = { 255, 255, 255, 255 };

        public EFFECT_LOOP() { EventType = 6; }
    }
    public class EFFECT_YACT_BRANCH : CYActEvent
    {
        public string YAct = "YACT_";

        public byte[] IDs = { 255, 255, 255, 255 };
        
        public byte[] Unknown = { };

        public EFFECT_YACT_BRANCH() { EventType = 21; }
    }
    public class EFFECT_ARMS_NAME : CYActEvent
    {
        public EFFECT_ARMS_NAME() { EventType = 31; }
    }
    public class EFFECT_RELEASE_ARMS : CYActEvent
    {
        public EFFECT_RELEASE_ARMS() { EventType = 2; }
    }
    public class EFFECT_CATCH_ARMS : CYActEvent
    {
        public int ArmsID = 0;
        public EFFECT_CATCH_ARMS() { EventType = 3; }
    }
    public class EFFECT_LOAD_ARMS : CYActEvent
    {
        public byte[] IDs = { 255, 255, 255, 255 };
        public EFFECT_LOAD_ARMS() { EventType = 34; }
    }
    public class UnknownEffect : CYActEvent
    {
        public byte[] UnkData;
    }

    /// <summary>
    /// Checks max/min range for parameters <br/>
    /// if base type is 1: Value is the maximum <br/>
    /// if base type is 2: Value is the minimum <br/>
    /// if check type is 0: Value is used for health <br/>
    /// if check type is 1: Value is possibly used for distance
    /// </summary>
    public class CRangeCondition : CYActCondition
    {
        public int Value = 19;
        public int CheckType = 1;
        public CRangeCondition() { Type = 1; }
    }

    public class CUnkCondition : CYActCondition
    {
        public byte[] Unk;
    }

    /// <summary>
    /// checks distance and angles relative to a target YActPlayEntity
    /// </summary>
    public class CRelationCondition : CYActCondition
    {
        public float CharacterAngle = 0.0f;
        public float CharacterArc = 45.0f;
        public float TargetAngle = 90.0f;
        public float TargetArc = 50.0f;
        public float MaxDistance = 30;
        public float MinDistance = 5;
        public string Target = "";
        public CRelationCondition() { Type = 6; }
    }

    public class CYActPlayReaderAndWriter
    {
        List<string> Strings = new List<string>();
        List<int> StringOffsets = new List<int>();
        int CharaOffset = 0;
        int CharaCount = 0;
        int ObjOffset = 0;
        int ObjCount = 0;
        int ArmOffset = 0;
        int ArmCount = 0;
        int EventOffset = 0;
        int EventCount = 0;
        int ConditionOffset = 0;
        int ConditionCount = 0;
        int YActCount = 0;
        
        public void WriteCSV(string path, TreeView MainTree)
        {

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write((int)1);
                writer.Write(new byte[12]);
                writer.Write((int)(32));
                writer.Write((int)(0));
                writer.Write((int)(1));
                Utils.WriteString(writer, "TCAY", false);
                writer.Write((int)(16));
                writer.Write((int)MainTree.Nodes[0].Nodes.Count);
                writer.Write((int)((MainTree.Nodes[0].Nodes.Count * 64) + 16));
                writer.Write((int)MainTree.Nodes[1].Nodes.Count);
                List<YAct> C1YActs = CollectYActs(MainTree.Nodes[0]);
                List<YAct> C2YActs = CollectYActs(MainTree.Nodes[1]);
                for (int i = 0; i < C1YActs.Count; i++)
                {
                    writer.Write(new byte[64]);
                }
                for (int i = 0; i < C2YActs.Count; i++)
                {
                    writer.Write(new byte[64]);
                }
                CharaOffset = (int)writer.BaseStream.Position;
                for (int i = 0; i < CharaCount; i++)
                {
                    writer.Write(new byte[56]);
                }
                ObjOffset = (int)writer.BaseStream.Position;
                for (int i = 0; i < ObjCount; i++)
                {
                    writer.Write(new byte[28]);
                }
                ArmOffset = (int)writer.BaseStream.Position;
                for (int i = 0; i < ArmCount; i++)
                {
                    writer.Write(new byte[16]);
                }
                EventOffset = (int)writer.BaseStream.Position;
                for (int i = 0; i < EventCount; i++)
                {
                    writer.Write(new byte[40]);
                }
                ConditionOffset = (int)writer.BaseStream.Position;
                for (int i = 0; i < ConditionCount; i++)
                {
                    writer.Write(new byte[36]);
                }
                Strings = Strings.Distinct().ToList();
                foreach (string s in Strings)
                {
                    if (s == null || s == "")
                    {
                        StringOffsets.Add((int)0);
                        continue;
                    }
                    StringOffsets.Add((int)(writer.BaseStream.Position - 32));
                    Utils.WriteString(writer, s, true);
                }
                CharaCount = 0;
                ObjCount = 0;
                ArmCount = 0;
                EventCount = 0;
                ConditionCount = 0;

                writer.Seek(48, SeekOrigin.Begin);
                foreach (YAct y in C1YActs)
                {
                    WriteYAct(writer, y);
                }
                foreach (YAct y in C2YActs)
                {
                    WriteYAct(writer, y);
                }
            }
        }

        public void WriteYAct(BinaryWriter writer, YAct YAct)
        {
            writer.Seek(48 + (64 * YActCount), SeekOrigin.Begin);
            Utils.WriteString(writer, YAct.Name, false);
            Utils.AlignData(writer, 16);
            writer.Write(YAct.FileID);
            writer.Write(YAct.unk1);
            writer.Write(YAct.unk2);
            writer.Write(YAct.unk3);
            WriteChara(writer, YAct.Characters);
            WriteObj(writer, YAct.Objects);
            WriteArm(writer, YAct.Arms);
            WriteEvent(writer, YAct.HactEvents);
            YActCount++;
        }
        public void WriteChara(BinaryWriter writer, List<CYActCharacter> Characters)
        {
            long returnpos = writer.BaseStream.Position;
            writer.BaseStream.Seek(CharaOffset + (CharaCount * 56), SeekOrigin.Begin);
            int offset = (int)(writer.BaseStream.Position - 32);

            foreach (CYActCharacter Chara in Characters)
            {
                WriteName(writer,Chara.Name);
                writer.Write(Chara.Unknown1);
                writer.Write(Chara.StatusConditions);
                writer.Write(Chara.Unknown2);
                WriteName(writer, Chara.ArmsID);
                writer.Write(Chara.Unknown3);
                WriteConditions(writer, Chara.Conditions1);
                writer.Write(Chara.Unknown4);
                WriteConditions(writer, Chara.Conditions2);
                writer.Write(Chara.Unknown5);
                CharaCount++;
            }

            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            writer.Write(offset);
            writer.Write((int)Characters.Count);
        }

        public void WriteObj(BinaryWriter writer, List<CYActObject> Objects)
        {
            long returnpos = writer.BaseStream.Position;
            writer.BaseStream.Seek(ObjOffset + (ObjCount * 28), SeekOrigin.Begin);
            int offset = (int)(writer.BaseStream.Position - 32);

            foreach (CYActObject Obj in Objects)
            {
                WriteName(writer, Obj.Name);
                writer.Write(Obj.Unknown1);
                WriteName(writer, Obj.ArmID);
                WriteConditions(writer, Obj.Conditions);
                writer.Write(Obj.Unknown2);
                ObjCount++;
            }

            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            writer.Write(offset);
            writer.Write((int)Objects.Count);
        }

        public void WriteArm(BinaryWriter writer, List<CYActArm> Arms)
        {
            long returnpos = writer.BaseStream.Position;
            writer.BaseStream.Seek(ArmOffset + (ArmCount * 16), SeekOrigin.Begin);
            int offset = (int)(writer.BaseStream.Position - 32);

            foreach (CYActArm Arm in Arms)
            {
                WriteName(writer, Arm.Name);
                writer.Write(Arm.WeaponCategory);
                WriteName(writer, Arm.ParentName);
                WriteName(writer, Arm.ArmID);
                ArmCount++;
            }

            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            writer.Write(offset);
            writer.Write((int)Arms.Count);
        }

        public void WriteEvent(BinaryWriter writer, List<CYActEvent> Events)
        {
            long returnpos = writer.BaseStream.Position;
            writer.BaseStream.Seek(EventOffset + (EventCount * 40), SeekOrigin.Begin);
            int offset = (int)(writer.BaseStream.Position - 32);

            foreach (CYActEvent Event in Events)
            {
                WriteName(writer, Event.Name);
                writer.Write(Event.EventType);
                switch (Event)
                {
                    case EFFECT_DAMAGE Dmg:
                        writer.Write(Dmg.DamageVal);
                        writer.Write(new byte[28]);
                        break;
                    case EFFECT_LOOP Loop:
                        writer.Write(Loop.MaxLoopNum);
                        writer.Write(new byte[12]);
                        writer.Write(Loop.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case EFFECT_RENDA Renda:
                        writer.Write(Renda.ID);
                        writer.Write(Renda.Count);
                        writer.Write(Renda.Button);
                        writer.Write(new byte[4]);
                        writer.Write(Renda.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case EFFECT_TIMING_OK Timing1:
                        writer.Write(Timing1.ID);
                        writer.Write(Timing1.Button);
                        writer.Write(new byte[8]);
                        writer.Write(Timing1.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case EFFECT_TIMING_NG Timing2:
                        writer.Write(Timing2.ID);
                        writer.Write(Timing2.Button);
                        writer.Write(new byte[8]);
                        writer.Write(Timing2.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case EFFECT_DEAD Dead:
                        writer.Write(Dead.ID);
                        writer.Write(new byte[28]);
                        break;
                    case EFFECT_NORMAL_BRANCH NBranch:
                        writer.Write(new byte[16]);
                        writer.Write(NBranch.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case EFFECT_FINISH_STATUS FinishS:
                        writer.Write(FinishS.Status);
                        writer.Write(new byte[12]);
                        writer.Write(FinishS.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case EFFECT_FINISH Finish:
                        writer.Write(new byte[16]);
                        writer.Write(Finish.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case HG_USE Use:
                        writer.Write(Use.HeatLoss);
                        writer.Write(new byte[12]);
                        writer.Write(Use.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case HG_CHK Chk:
                        writer.Write(Chk.ID);
                        writer.Write(Chk.Unknown);
                        writer.Write(new byte[24]);
                        break;
                    case EFFECT_YACT_BRANCH YBranch:
                        long pos = writer.BaseStream.Position;
                        Utils.WriteString(writer, YBranch.YAct, false);
                        if (writer.BaseStream.Position - pos < 16)
                            writer.Write(new byte[16 - (writer.BaseStream.Position - pos)]);
                        writer.Write(YBranch.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case EFFECT_ARMS_NAME:
                        writer.Write(new byte[32]);
                        break;
                    case EFFECT_RELEASE_ARMS:
                        writer.Write(new byte[32]);
                        break;
                    case EFFECT_CATCH_ARMS Arm:
                        writer.Write(Arm.ArmsID);
                        writer.Write(new byte[28]);
                        break;
                    case EFFECT_LOAD_ARMS Arm:
                        writer.Write(new byte[16]);
                        writer.Write(Arm.IDs);
                        writer.Write(new byte[12]);
                        break;
                    case UnknownEffect Unk:
                        writer.Write(Unk.UnkData);
                        break;
                }
                EventCount++;
            }

            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            writer.Write(offset);
            writer.Write((int)Events.Count);
        }

        public void WriteConditions(BinaryWriter writer, List<CYActCondition> Conditions)
        {
            long returnpos = writer.BaseStream.Position;
            writer.BaseStream.Seek(ConditionOffset + (ConditionCount * 36), SeekOrigin.Begin);
            int offset = (int)(writer.BaseStream.Position - 32);

            foreach (CYActCondition Cnd in Conditions)
            {
                ConditionCount++;
                switch (Cnd)
                {
                    case CRelationCondition R:
                        writer.Write(R.Type);
                        writer.Write((ushort)((R.CharacterAngle / 360) * 65536));
                        writer.Write((ushort)((R.CharacterArc / 360) * 65536));
                        writer.Write((ushort)((R.TargetAngle / 360) * 65536));
                        writer.Write((ushort)((R.TargetArc / 360) * 65536));
                        writer.Write(R.MaxDistance);
                        writer.Write(R.MinDistance);
                        long pos = writer.BaseStream.Position;
                        Utils.WriteString(writer, R.Target, false);
                        if (writer.BaseStream.Position - pos < 16)
                            writer.Write(new byte[16 - (writer.BaseStream.Position - pos)]);
                        break;
                    case CRangeCondition R:
                        writer.Write(R.Type);
                        writer.Write(R.Value);
                        writer.Write(R.CheckType);
                        writer.Write(new byte[24]);
                        break;
                    case CUnkCondition U:
                        writer.Write(U.Type);
                        writer.Write(U.Unk);
                        break;

                }
            }

            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            writer.Write(offset);
            writer.Write((int)Conditions.Count);
        }

        public void WriteName(BinaryWriter writer, string Name)
        {
            if (Name != "" && Name != null)
                writer.Write((int)(StringOffsets[Strings.IndexOf(Name)]));
            else
                writer.Write((int)0);
        }

        public List<YAct> CollectYActs(TreeNode CNode)
        {
            List<YAct> YActs = new List<YAct>();
            foreach (TreeNode YNode in CNode.Nodes)
            {
                YAct Data = YNode.Tag as YAct;
                CharaCount += YNode.Nodes[1].Nodes.Count;
                ObjCount += YNode.Nodes[2].Nodes.Count;
                ArmCount += YNode.Nodes[3].Nodes.Count;
                EventCount += YNode.Nodes[5].Nodes.Count;
                Data.Characters = new List<CYActCharacter>();
                foreach (TreeNode CharaNode in YNode.Nodes[1].Nodes)
                {
                    CYActCharacter Character = CharaNode.Tag as CYActCharacter;
                    Strings.Add(Character.Name);
                    Strings.Add(Character.ArmsID);
                    ConditionCount += CharaNode.Nodes[2].Nodes.Count;
                    ConditionCount += CharaNode.Nodes[3].Nodes.Count;
                    Character.Conditions1 = new List<CYActCondition>();
                    Character.Conditions2 = new List<CYActCondition>();
                    foreach (TreeNode ConditionNode in CharaNode.Nodes[2].Nodes)
                    {
                        Character.Conditions1.Add(ConditionNode.Tag as CYActCondition);
                    }
                    foreach (TreeNode ConditionNode in CharaNode.Nodes[3].Nodes)
                    {
                        Character.Conditions2.Add(ConditionNode.Tag as CYActCondition);
                    }
                    Data.Characters.Add(Character);
                }
                Data.Objects = new List<CYActObject>();
                foreach (TreeNode ObjNode in YNode.Nodes[2].Nodes)
                {
                    CYActObject Object = ObjNode.Tag as CYActObject;
                    Strings.Add(Object.Name);
                    Strings.Add(Object.ArmID);
                    ConditionCount += ObjNode.Nodes[2].Nodes.Count;
                    Object.Conditions = new List<CYActCondition>();
                    foreach (TreeNode ConditionNode in ObjNode.Nodes[2].Nodes)
                    {
                        Object.Conditions.Add(ConditionNode.Tag as CYActCondition);
                    }
                    Data.Objects.Add(Object);
                }
                Data.Arms = new List<CYActArm>();
                foreach (TreeNode ArmNode in YNode.Nodes[3].Nodes)
                {
                    CYActArm Arm = ArmNode.Tag as CYActArm;
                    Strings.Add(Arm.Name);
                    Strings.Add(Arm.ParentName);
                    Strings.Add(Arm.ArmID);
                    Data.Arms.Add(Arm);
                }
                Data.HactEvents = new List<CYActEvent>();
                foreach (TreeNode EventNode in YNode.Nodes[5].Nodes)
                {
                    CYActEvent Event = EventNode.Tag as CYActEvent;
                    Strings.Add(Event.Name);
                    Data.HactEvents.Add(Event);
                }
                YActs.Add(Data);
            }
            return YActs;
        }

        public static void ReadCSV(string path, TreeView MainTree)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                Header Header = new Header();
                reader.ReadBytes(16);
                Header.Pointer = reader.ReadUInt32();
                Header.Size = reader.ReadUInt32();
                Header.Unk = reader.ReadUInt32();
                Header.ID = Utils.ReadFixedString(reader, 4);
                Header.Pointer1 = reader.ReadUInt32();
                Header.Count1 = reader.ReadUInt32();
                Header.Pointer2 = reader.ReadUInt32();
                Header.Count2 = reader.ReadUInt32();
                reader.BaseStream.Seek(Header.Pointer1 + 32, SeekOrigin.Begin);

                MainTree.Nodes.Add(new TreeNode
                {
                    Text = "Category 1"
                });

                MainTree.Nodes.Add(new TreeNode
                {
                    Text = "Category 2"
                });

                for (int e = 0; e < Header.Count1; e++)
                {
                    ReadCSVEntry(reader, MainTree.Nodes[0]);
                }
                for (int e = 0; e < Header.Count2; e++)
                {
                    ReadCSVEntry(reader, MainTree.Nodes[1]);
                }
            }
        }
        private static void ReadCSVEntry(BinaryReader reader, TreeNode CategoryNode)
        {
            int i;
            YAct CurrentYAct = new YAct
            {
                Name = Utils.ReadFixedString(reader, 16),
                FileID = reader.ReadUInt32(),
                unk1 = reader.ReadUInt32(),
                unk2 = reader.ReadUInt32(),
                unk3 = reader.ReadUInt32(),
                CharaPtr = reader.ReadUInt32(),
                CharaCount = reader.ReadUInt32(),
                ObjectPtr = reader.ReadUInt32(),
                ObjectCount = reader.ReadUInt32(),
                ArmsPtr = reader.ReadUInt32(),
                ArmsCount = reader.ReadUInt32(),
                HactEventPtr = reader.ReadUInt32(),
                HactEventCount = reader.ReadUInt32(),
            };
            long returnpos = reader.BaseStream.Position;

            TreeNode YActNode = new TreeNode
            {
                Text = CurrentYAct.Name,
                Tag = CurrentYAct
            };

            TreeNode CameraCollection = new TreeNode
            {
                Text = "Cameras"
            };

            TreeNode CharacterCollection = new TreeNode
            {
                Text = "Characters",
            };

            TreeNode ObjectCollection =new TreeNode
            {
                Text = "Objects",
            };

            TreeNode ArmCollection = new TreeNode
            {
                Text = "Arms",
            };

            TreeNode ExMotionCollection = new TreeNode
            {
                Text = "ExMotions",
            };

            TreeNode EventCollection = new TreeNode
            {
                Text = "YAct Events"
            };

            YActNode.Nodes.Add(CameraCollection);
            YActNode.Nodes.Add(CharacterCollection);
            YActNode.Nodes.Add(ObjectCollection);
            YActNode.Nodes.Add(ArmCollection);
            YActNode.Nodes.Add(ExMotionCollection);
            YActNode.Nodes.Add(EventCollection);

            reader.BaseStream.Seek(CurrentYAct.CharaPtr + 32, SeekOrigin.Begin);
            for (i = 0; i < CurrentYAct.CharaCount; i++)
            {
                CYActCharacter Chara = new CYActCharacter
                {
                    NamePtr = reader.ReadUInt32(),
                    Unknown1 = reader.ReadBytes(8),
                    StatusConditions = reader.ReadUInt32(),
                    Unknown2 = reader.ReadInt32(),
                    ArmsIDPtr = reader.ReadUInt32(),
                    Unknown3 = reader.ReadBytes(8),
                    ConditionPtr1 = reader.ReadUInt32(),
                    ConditionCount1 = reader.ReadUInt32(),
                    Unknown4 = reader.ReadUInt32(),
                    ConditionPtr2 = reader.ReadUInt32(),
                    ConditionCount2 = reader.ReadUInt32(),
                    Unknown5 = reader.ReadUInt32(),
                };
                long returnposc = reader.BaseStream.Position;
                Chara.Name = ReadName(reader, Chara.NamePtr);
                Chara.ArmsID = ReadName(reader, Chara.ArmsIDPtr);

                TreeNode CharaNode = new TreeNode
                {
                    Text = Chara.Name,
                    Tag = Chara
                };

                CharaNode.Nodes.Add(new TreeNode
                {
                    Text = "Animations"
                });

                CharaNode.Nodes.Add(new TreeNode
                {
                    Text = "Effects"
                });

                CharaNode.Nodes.Add(new TreeNode
                {
                    Text = "Conditions 1"
                });

                CharaNode.Nodes.Add(new TreeNode
                {
                    Text = "Conditions 2"
                });

                ReadUNK(reader, Chara.ConditionPtr1, Chara.ConditionCount1, CharaNode.Nodes[2]);
                ReadUNK(reader, Chara.ConditionPtr2, Chara.ConditionCount2, CharaNode.Nodes[3]);
                reader.BaseStream.Seek(returnposc, SeekOrigin.Begin);

                CharacterCollection.Nodes.Add(CharaNode);
            }
            reader.BaseStream.Seek(CurrentYAct.ObjectPtr + 32, SeekOrigin.Begin);
            for (i = 0; i < CurrentYAct.ObjectCount; i++)
            {
                CYActObject Object = new CYActObject()
                {
                    NamePtr = reader.ReadUInt32(),
                    Unknown1 = reader.ReadBytes(8),
                    ArmIDPtr = reader.ReadUInt32(),
                    ConditionPtr = reader.ReadUInt32(),
                    ConditionCount = reader.ReadUInt32(),
                    Unknown2 = reader.ReadUInt32(),
                };
                long returnposo = reader.BaseStream.Position;
                Object.Name = ReadName(reader, Object.NamePtr);
                Object.ArmID = ReadName(reader, Object.ArmIDPtr);

                TreeNode ObjectNode = new TreeNode
                {
                    Text = Object.Name,
                    Tag = Object
                };

                ObjectNode.Nodes.Add(new TreeNode
                {
                    Text = "Animations"
                });

                ObjectNode.Nodes.Add(new TreeNode
                {
                    Text = "Effects"
                });

                ObjectNode.Nodes.Add(new TreeNode
                {
                    Text = "Conditions"
                });

                ReadUNK(reader, Object.ConditionPtr, Object.ConditionCount, ObjectNode);
                reader.BaseStream.Seek(returnposo, SeekOrigin.Begin);

                ObjectCollection.Nodes.Add(ObjectNode);
            }
            reader.BaseStream.Seek(CurrentYAct.ArmsPtr + 32, SeekOrigin.Begin);
            for (i = 0; i < CurrentYAct.ArmsCount; i++)
            {
                CYActArm Arm = new CYActArm()
                {
                    NamePtr = reader.ReadUInt32(),
                    WeaponCategory = reader.ReadUInt32(),
                    ParentPtr = reader.ReadUInt32(),
                    ArmIDPtr = reader.ReadUInt32(),
                };
                Arm.Name = ReadName(reader, Arm.NamePtr);
                Arm.ParentName = ReadName(reader, Arm.ParentPtr);
                if (Arm.ArmIDPtr != 0)
                {
                    Arm.ArmID = ReadName(reader, Arm.ArmIDPtr);
                }

                TreeNode ArmNode = new TreeNode
                {
                    Text = Arm.Name,
                    Tag = Arm
                };

                ArmNode.Nodes.Add(new TreeNode
                {
                    Text = "Animations"
                });

                ArmNode.Nodes.Add(new TreeNode
                {
                    Text = "Effects"
                });

                ArmCollection.Nodes.Add(ArmNode);
            }
            reader.BaseStream.Seek(CurrentYAct.HactEventPtr + 32, SeekOrigin.Begin);
            for (i = 0; i < CurrentYAct.HactEventCount; i++)
            {
                uint NamePtr = reader.ReadUInt32();
                uint HEID = reader.ReadUInt32();
                string Name = ReadName(reader, NamePtr);
                switch (HEID)
                {
                    case 0:

                        EFFECT_DAMAGE Dmg = new EFFECT_DAMAGE
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            DamageVal = reader.ReadUInt32(),
                            EventType = HEID
                        };
                        reader.ReadBytes(28);
                        CurrentYAct.HactEvents.Add(Dmg);
                        break;
                    case 6:

                        EFFECT_LOOP Loop = new EFFECT_LOOP
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            MaxLoopNum = reader.ReadUInt32(),
                            EventType = HEID
                        };
                        reader.ReadBytes(12);
                        Loop.IDs = reader.ReadBytes(4);
                        reader.ReadBytes(12);
                        CurrentYAct.HactEvents.Add(Loop);
                        break;
                    case 7:

                        EFFECT_RENDA Renda = new EFFECT_RENDA
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            ID = reader.ReadInt32(),
                            Count = reader.ReadUInt32(),
                            Button = reader.ReadUInt32(),
                            EventType = HEID
                        };
                        reader.ReadBytes(4);
                        Renda.IDs = reader.ReadBytes(4);
                        reader.ReadBytes(12);
                        CurrentYAct.HactEvents.Add(Renda);
                        break;
                    case 8:

                        EFFECT_TIMING_OK Timing1 = new EFFECT_TIMING_OK
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            ID = reader.ReadInt32(),
                            Button = reader.ReadUInt32(),
                            EventType = HEID
                        };
                        reader.ReadBytes(8);
                        Timing1.IDs = reader.ReadBytes(4);
                        reader.ReadBytes(12);
                        CurrentYAct.HactEvents.Add(Timing1);
                        break;
                    case 9:

                        EFFECT_TIMING_NG Timing2 = new EFFECT_TIMING_NG
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            ID = reader.ReadInt32(),
                            Button = reader.ReadUInt32(),
                            EventType = HEID
                        };
                        reader.ReadBytes(8);
                        Timing2.IDs = reader.ReadBytes(4);
                        reader.ReadBytes(12);
                        CurrentYAct.HactEvents.Add(Timing2);
                        break;
                    case 16:
                        EFFECT_DEAD Dead = new EFFECT_DEAD
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            ID = reader.ReadInt32(),
                            EventType = HEID
                        };
                        reader.ReadBytes(28);
                        CurrentYAct.HactEvents.Add(Dead);
                        break;
                    case 10:
                        EFFECT_NORMAL_BRANCH NBranch = new EFFECT_NORMAL_BRANCH
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID
                        };
                        reader.ReadBytes(16);
                        NBranch.IDs = reader.ReadBytes(4);
                        reader.ReadBytes(12);
                        CurrentYAct.HactEvents.Add(NBranch);
                        break;
                    case 17:
                        EFFECT_FINISH_STATUS FinishS = new EFFECT_FINISH_STATUS
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID,
                            Status = reader.ReadUInt32()
                        };
                        reader.ReadBytes(12);
                        FinishS.IDs = reader.ReadBytes(4);
                        reader.ReadBytes(12);
                        CurrentYAct.HactEvents.Add(FinishS);
                        break;
                    case 11:
                        EFFECT_FINISH Finish = new EFFECT_FINISH
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID,
                        };
                        reader.ReadBytes(16);
                        Finish.IDs = reader.ReadBytes(4);
                        reader.ReadBytes(12);
                        CurrentYAct.HactEvents.Add(Finish);
                        break;
                    case 20:
                        HG_USE Use = new HG_USE
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID
                        };
                        Use.HeatLoss = reader.ReadUInt32();
                        reader.ReadBytes(12);
                        Use.IDs = reader.ReadBytes(4);
                        reader.ReadBytes(12);
                        CurrentYAct.HactEvents.Add(Use);
                        break;
                    case 19:
                        HG_CHK Chk = new HG_CHK
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID,
                            ID = reader.ReadInt32(),
                            Unknown = reader.ReadUInt32()
                        };
                        reader.ReadBytes(24);
                        CurrentYAct.HactEvents.Add(Chk);
                        break;
                    case 21:
                        EFFECT_YACT_BRANCH YBranch = new EFFECT_YACT_BRANCH
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID,
                            YAct = Utils.ReadFixedString(reader, 16),
                            IDs = reader.ReadBytes(4),
                            Unknown = reader.ReadBytes(12)
                        };
                        CurrentYAct.HactEvents.Add(YBranch);
                        break;
                    case 31:
                        EFFECT_ARMS_NAME AName = new()
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID,
                        };
                        reader.ReadBytes(32);
                        CurrentYAct.HactEvents.Add(AName);
                        break;
                    case 2:
                        EFFECT_RELEASE_ARMS Arm1 = new()
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID,
                        };
                        reader.ReadBytes(32);
                        CurrentYAct.HactEvents.Add(Arm1);
                        break;
                    case 3:
                        EFFECT_CATCH_ARMS Arm2 = new()
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID,
                            ArmsID = reader.ReadInt32()
                        };
                        reader.ReadBytes(28);
                        CurrentYAct.HactEvents.Add(Arm2);
                        break;
                    case 34:
                        EFFECT_LOAD_ARMS Arm3 = new()
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            EventType = HEID,
                        };
                        reader.ReadBytes(16);
                        Arm3.IDs = reader.ReadBytes(4);
                        reader.ReadBytes(12);
                        CurrentYAct.HactEvents.Add(Arm3);
                        break;
                    default:
                        UnknownEffect UNK = new UnknownEffect
                        {
                            Name = Name,
                            NamePtr = NamePtr,
                            UnkData = reader.ReadBytes(32),
                            EventType = HEID
                        };
                        CurrentYAct.HactEvents.Add(UNK);
                        break;
                }
            }

            foreach (CYActEvent Event in CurrentYAct.HactEvents)
            {
                EventCollection.Nodes.Add(new TreeNode
                {
                    Text = Event.Name,
                    Tag = Event
                });
            }

            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            CategoryNode.Nodes.Add(YActNode);
        }
        private static void ReadUNK(BinaryReader reader, uint ptr, uint count, TreeNode Node)
        {
            reader.BaseStream.Seek(ptr + 32, SeekOrigin.Begin);
            for (int i = 0; i < count; i++)
            {
                TreeNode Condition = new TreeNode();
                Node.Nodes.Add(Condition);
                int Type = reader.ReadInt32();
                switch(Type)
                {
                    case 6:
                        CRelationCondition Relation = new CRelationCondition();
                        Condition.Text = "Relation Check";
                        Relation.Type = Type;
                        Relation.CharacterAngle = ((float)reader.ReadUInt16() / 65536.0f) * 360.0f;
                        Relation.CharacterArc = ((float)reader.ReadUInt16() / 65536.0f) * 360.0f;
                        Relation.TargetAngle = ((float)reader.ReadUInt16() / 65536.0f) * 360.0f;
                        Relation.TargetArc = ((float)reader.ReadUInt16() / 65536.0f) * 360.0f;
                        Relation.MaxDistance = reader.ReadSingle();
                        Relation.MinDistance = reader.ReadSingle();
                        Relation.Target = Utils.ReadFixedString(reader,16);
                        Condition.Tag = Relation;
                        break;
                    case 1:
                    case 2:
                        CRangeCondition Range = new CRangeCondition();
                        Condition.Text = "Range Check";
                        Range.Type = Type;
                        Range.Value = reader.ReadInt32();
                        Range.CheckType = reader.ReadInt32();
                        Condition.Tag = Range;
                        break;
                    default:
                        CUnkCondition Unk = new CUnkCondition();
                        Unk.Type = Type;
                        Condition.Text = "Unknown";
                        Condition.Tag = Unk;
                        Unk.Unk = reader.ReadBytes(32);
                        break;
                }
            }

        }
        private static string ReadName(BinaryReader reader, uint ptr)
        {
            if (ptr == 0)
                return "";
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(ptr + 32, SeekOrigin.Begin);
            string Name = Utils.ReadString(reader);
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return Name;
        }
    }
}