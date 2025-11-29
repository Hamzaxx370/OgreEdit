namespace YActLib.Common
{
    public class EFFECT_AUTHORING
    {
        //Base effect data
        public uint PlayFlag { get; set; }
        public float StartFrame { get; set; }
        public float EndFrame { get; set; }
        public float Speed { get; set; }
        public uint BoneID { get; set; }
        public uint Type { get; set; }
        public uint TypeID { get; set; }
        public string Name { get; set; } //Only in Yakuza 2
        public virtual void Add()
        {
            PlayFlag = 0;
            StartFrame = 0;
            EndFrame = 0;
            Speed = 1;
            BoneID = 0;
        }
    }

    public class YACT_EVENT : EFFECT_AUTHORING
    {
        public virtual void Add()
        {
            PlayFlag = 0;
            StartFrame = 0;
            EndFrame = 0;
            Speed = 1;
            BoneID = 0;
            Type = 4;
            TypeID = 0;
        }
    }

    /// <summary>
    /// Used for undocumented types
    /// </summary>
    public class EFFECT_UNKNOWN : EFFECT_AUTHORING
    {
        public byte[] Data;
    }

    //Type 4 (HAct Events)

    /// <summary>
    /// (Type 4, TypeID 0)
    /// Damages a character at a specific frame
    /// </summary>
    public class EFFECT_DAMAGE : EFFECT_AUTHORING
    {
        public uint Damage;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_DAMAGE";
            Damage = 100;
            Type = 4;
            TypeID = 0;
        }
    }
    /// <summary>
    /// (Type 4, TypeID 6)
    /// Loops for a specified number of times, unless a renda event is succesful
    /// </summary>
    public class EFFECT_LOOP : EFFECT_AUTHORING
    {
        public uint MaxLoopNum;
        public int Flag;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_LOOP";
            MaxLoopNum = 10;
            Flag = 0;
            Type = 4;
            TypeID = 6;
        }
    }
    /// <summary>
    /// (Type 4, TypeID 9)
    /// Branches from the start frame to end frame if an effect with the same ID succeeds
    /// </summary>
    public class EFFECT_NORMAL_BRANCH : EFFECT_AUTHORING
    {
        public int ID;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_NORMAL_BRANCH";
            ID = 0;
            Type = 4;
            TypeID = 9;
        }
    }
    /// <summary>
    /// (Type 4, TypeID 16)
    /// (Type 4, TypeID 17)
    /// Succeeds if a character is dead, triggering branches with the same ID
    /// </summary>
    public class EFFECT_DEAD : EFFECT_AUTHORING
    {
        public int ID;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_DEAD";
            ID = 0;
            Type = 4;
            TypeID = 16;
        }
    }
    /// <summary>
    /// (Type 4, TypeID 18)
    /// Changes the finish status of characters
    /// </summary>
    public class EFFECT_FINISH_STATUS : EFFECT_AUTHORING
    {
        public int Status;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_FINISH_STATUS";
            Status = 0;
            Type = 4;
            TypeID = 18;
        }
    }
    /// <summary>
    /// (Type 4, TypeID 8)
    /// Adds a button prompt that when pressed triggers branches with the same ID
    /// </summary>
    public class EFFECT_TIMING : EFFECT_AUTHORING
    {
        public uint Button;
        public int ID;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_TIMING";
            Button = 1;
            ID = 0;
            Type = 4;
            TypeID = 8;
        }
    }
    /// <summary>
    /// (Type 4, TypeID 7)
    /// Adds a button prompt that when spammed triggers branches with the same ID
    /// </summary>
    public class EFFECT_RENDA : EFFECT_AUTHORING
    {
        public uint Button;
        public uint Count;
        public int ID;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_RENDA";
            Button = 1;
            Count = 8;
            ID = 0;
            Type = 4;
            TypeID = 7;
        }
    }
    //No clue what those do

    /// <summary>
    /// (Type 4, TypeID 13)
    /// </summary>
    public class EFFECT_COUNTER_BRANCH : EFFECT_AUTHORING
    {
        public int Unknown1;
        public int Unknown2;
    }
    /// <summary>
    /// (Type 4, TypeID 11)
    /// </summary>
    public class EFFECT_COUNTER_UP : EFFECT_AUTHORING
    {
        public int Unknown;
    }
    /// <summary>
    /// (Type 4, TypeID 12)
    /// </summary>
    public class EFFECT_COUNTER_RESET : EFFECT_AUTHORING
    {
        public int Unknown;
    }
    /// <summary>
    /// Types 20 and 21 (Sounds)
    /// No clue what the differences are
    /// </summary>
    public class EFFECT_SOUND : EFFECT_AUTHORING
    {
        public float SoundSpeed;
        public ushort ContainerID;
        public ushort VoiceID;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_SOUND";
            BoneID = 11;
            SoundSpeed = 1;
            ContainerID = 2473;
            VoiceID = 6;
            Type = 20;
            TypeID = 0;
        }
    }
    //Type 1 (Particles)

    /// <summary>
    /// (Type 1, TypeID > 0)
    /// A normal particle called with an ID
    /// </summary>
    public class EFFECT_PARTICLE_NORMAL : EFFECT_AUTHORING
    {
        public uint ptclID;
        public float[] Location;
        public float[] Rotation;
        public uint Condition;
        public byte[] Flags;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_PARTICLE";
            BoneID = 11;
            Type = 1;
            ptclID = 357;
            Location = [0.7f, 0, 0.8f];
            Rotation = [0, 0, 0];
            Condition = 15;
            Flags = [0, 0, 0, 0];
        }
    }
    /// <summary>
    /// (Type 1, TypeID 0)
    /// A special trail particle that can be colored and adjusted
    /// </summary>
    public class EFFECT_PARTICLE_TRAIL : EFFECT_AUTHORING
    {
        public byte[] RGBA1;
        public byte[] RGBA2;
        public float TrailParam1;
        public float TrailParam2;
        public float TrailParam3;
        public uint Unknown1;
        public uint Unknown2;
        public byte[] Flags;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_TRAIL";
            BoneID = 3;
            Type = 1;
            TypeID = 0;
            RGBA1 = [255, 92, 213, 117];
            RGBA2 = [255, 92, 213, 117];
            TrailParam1 = -4;
            TrailParam2 = 0;
            TrailParam3 = 6;
            Unknown1 = 15;
            Unknown2 = 2;
            Flags = [0, 1, 1, 1];
        }
    }

    //Type 0 (Camera Effects)

    /// <summary>
    /// (Type 0, TypeID 49)
    /// (Type 0, TypeID 41)
    /// A screen flash that can be colored
    /// </summary>
    public class EFFECT_SCREEN_FLASH : EFFECT_AUTHORING
    {
        public uint Unknown1;
        public float Unknown2;
        public byte[] RGBA;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_SCREEN_FLASH";
            BoneID = 3;
            Type = 0;
            TypeID = 0x31;
            RGBA = [255, 255, 255, 80];
            Unknown1 = 10;
            Unknown2 = 10;
        }

    }
    /// <summary>
    /// (Type 3)
    /// A screen shake that is only used in motion properties
    /// </summary>
    public class EFFECT_SCREEN_SHAKE : EFFECT_AUTHORING
    {
        public uint ShakeIntensity;
        public uint Condition;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_SCREEN_SHAKE";
            ShakeIntensity = 2;
            Condition = 15;
        }

    }
    /// <summary>
    /// (Type 0, TypeID 43)
    /// A special after image effect that can be colored and scaled
    /// </summary>
    public class EFFECT_AFTER_IMAGE : EFFECT_AUTHORING
    {
        public uint Unknown1;
        public float Unknown2;
        public float Param1;
        public float Param2;
        public float Scale;
        public byte[] RGBA;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_AFTER_IMAGE";
            RGBA = [255, 255, 255, 40];

            BoneID = 3;
            Type = 0;
            TypeID = 43;
            Unknown1 = 3;
            Unknown2 = 8f;
            Param1 = 0.4f;
            Param2 = 0.6f;
            Scale = 1.1f;
        }
    }
    /// <summary>
    /// (Type 0, TypeID 76)
    /// Yeah its considered a camera effect
    /// </summary>
    public class EFFECT_VIBRATION : EFFECT_AUTHORING
    {
        public uint Vibration1;
        public uint Vibration2;
        public override void Add()
        {
            base.Add();
            Name = "EFFECT_VIBRATION";
            BoneID = 3;
            Type = 0;
            TypeID = 76;
            Vibration1 = 0;
            Vibration2 = 0;
        }
    }
    /// <summary>
    /// Common data reader between y1 and y2
    /// </summary>
    public class BaseReaderAndWriter
    {
        public static void WriteEffects(BinaryWriter writer, List<EFFECT_AUTHORING> Effects, List<uint> EffectPointers)
        {
            foreach (EFFECT_AUTHORING effect in Effects)
            {
                uint Ptr = (uint)writer.BaseStream.Position;
                EffectPointers.Add(Ptr);
                writer.Write(effect.PlayFlag);
                writer.Write(effect.StartFrame);
                writer.Write(effect.EndFrame);
                writer.Write(effect.Speed);
                writer.Write(effect.BoneID);
                if (effect is EFFECT_DAMAGE Dmg)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[28]);
                    writer.Write(Dmg.Damage);
                    writer.Write(new byte[16]);
                }
                else if (effect is EFFECT_SOUND Cue)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)20);
                    writer.Write(new byte[8]);
                    writer.Write(Cue.SoundSpeed);
                    writer.Write(Cue.ContainerID);
                    writer.Write(Cue.VoiceID);
                    writer.Write(new byte[32]);
                }
                else if (effect is EFFECT_PARTICLE_NORMAL PTCL)
                {

                    writer.Write(new byte[12]);
                    writer.Write(PTCL.Location[0]);
                    writer.Write(PTCL.Location[1]);
                    writer.Write(PTCL.Location[2]);
                    writer.Write((uint)1);
                    writer.Write(PTCL.Rotation[0]);
                    writer.Write(PTCL.Rotation[1]);
                    writer.Write(PTCL.Rotation[2]);
                    writer.Write(PTCL.ptclID);
                    writer.Write(new byte[12]);
                    writer.Write(PTCL.Condition);
                    writer.Write(new byte[12]);
                    writer.Write(PTCL.Flags);
                }
                else if (effect is EFFECT_PARTICLE_TRAIL Trail)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)1);
                    writer.Write(new byte[4]);
                    writer.Write(Trail.TrailParam1);
                    writer.Write(Trail.TrailParam2);
                    writer.Write((uint)0);
                    writer.Write(Trail.RGBA1);
                    writer.Write(Trail.RGBA2);
                    writer.Write(Trail.TrailParam3);
                    writer.Write(Trail.Unknown1);
                    writer.Write(new byte[8]);
                    writer.Write(Trail.Unknown2);
                    writer.Write(Trail.Flags);
                }
                else if (effect is EFFECT_LOOP Loop)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)6);
                    writer.Write(new byte[12]);
                    writer.Write(Loop.MaxLoopNum);
                    writer.Write(new byte[16]);
                }
                else if (effect is EFFECT_NORMAL_BRANCH LoopE)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)9);
                    writer.Write(new byte[12]);
                    writer.Write(LoopE.ID);
                    writer.Write(new byte[16]);
                }
                else if (effect is EFFECT_DEAD Dead)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)16);
                    writer.Write(new byte[12]);
                    writer.Write(Dead.ID);
                    writer.Write(new byte[16]);
                }
                else if (effect.Type == 4 && effect.TypeID == 10)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)10);
                    writer.Write(new byte[32]);
                }
                else if (effect is EFFECT_TIMING Button)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)8);
                    writer.Write(new byte[12]);
                    writer.Write(Button.ID);
                    writer.Write(new byte[12]);
                    writer.Write(Button.Button);
                }
                else if (effect is EFFECT_RENDA ButtonS)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)7);
                    writer.Write(new byte[12]);
                    writer.Write(ButtonS.ID);
                    writer.Write(new byte[12]);
                    writer.Write((short)ButtonS.Button);
                    writer.Write((short)ButtonS.Count);
                }
                else if (effect is EFFECT_COUNTER_BRANCH CBranch)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)13);
                    writer.Write(new byte[12]);
                    writer.Write(CBranch.Unknown1);
                    writer.Write(new byte[12]);
                    writer.Write(CBranch.Unknown2);
                }
                else if (effect is EFFECT_COUNTER_UP CUp)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)11);
                    writer.Write(new byte[12]);
                    writer.Write(CUp.Unknown);
                    writer.Write(new byte[16]);
                }
                else if (effect is EFFECT_COUNTER_RESET CReset)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)12);
                    writer.Write(new byte[12]);
                    writer.Write(CReset.Unknown);
                    writer.Write(new byte[16]);
                }
                else if (effect is EFFECT_FINISH_STATUS CStatus)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[12]);
                    writer.Write((uint)18);
                    writer.Write(new byte[12]);
                    writer.Write(CStatus.Status);
                    writer.Write(new byte[16]);
                }
                else if (effect is EFFECT_UNKNOWN Unk)
                {
                    writer.Write(Unk.Data);
                }
                else if (effect is EFFECT_VIBRATION Vib)
                {
                    writer.Write(new byte[40]);
                    writer.Write((uint)0x4C);
                    writer.Write(new byte[12]);
                    writer.Write(Vib.Vibration1);
                    writer.Write(new byte[12]);
                    writer.Write(Vib.Vibration2);
                }
                else if (effect is EFFECT_SCREEN_FLASH ScrFlsh)
                {
                    writer.Write(new byte[12]);
                    writer.Write(ScrFlsh.Unknown1);
                    writer.Write(new byte[12]);
                    writer.Write(ScrFlsh.Unknown2);
                    writer.Write(ScrFlsh.RGBA);
                    writer.Write(new byte[4]);
                    writer.Write((uint)0x31);
                    writer.Write(new byte[32]);
                }
                else if (effect is EFFECT_AFTER_IMAGE AImage)
                {
                    writer.Write(new byte[12]);
                    writer.Write(AImage.Unknown1);
                    writer.Write(new byte[12]);
                    writer.Write(AImage.Unknown2);
                    writer.Write(AImage.Param1);
                    writer.Write(AImage.Param2);
                    writer.Write((uint)0x2B);
                    writer.Write(AImage.Scale);
                    writer.Write(new byte[4]);
                    writer.Write(AImage.RGBA);
                    writer.Write(new byte[20]);
                }
                else if (effect is EFFECT_SCREEN_SHAKE Shake)
                {
                    writer.Write(new byte[0x18]);
                    writer.Write((uint)3);
                    writer.Write(new byte[12]);
                    writer.Write(Shake.ShakeIntensity);
                    writer.Write(new byte[12]);
                    writer.Write(Shake.Condition);
                    writer.Write(new byte[0x10]);
                }
                else if (effect is YACT_EVENT Event)
                {
                    writer.Write(new byte[24]);
                    writer.Write((uint)4);
                    writer.Write(new byte[28]);
                    writer.Write((int)0);
                    writer.Write(new byte[16]);
                }
            }
        }
        /// <summary>
        /// General effect reader
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="reader"></param>
        /// <param name="Version"></param>
        /// <returns>
        /// Effect type
        /// </returns>
        public static TreeNode ReadEffect(uint Start, BinaryReader reader, int Version, TreeNode Node)
        {
            reader.BaseStream.Seek(Start + 44, SeekOrigin.Begin);
            uint EffectType = reader.ReadUInt32();
            reader.BaseStream.Seek(Start + 60, SeekOrigin.Begin);
            uint EffectSubType = reader.ReadUInt32();
            reader.BaseStream.Seek(Start, SeekOrigin.Begin);
            EFFECT_AUTHORING Effect = new EFFECT_AUTHORING();
            string Name;
            switch (EffectType)
            {
                case 0:
                    switch (EffectSubType)
                    {
                        case 0x31:
                        case 0x29:
                            EFFECT_SCREEN_FLASH ScrFlsh = new EFFECT_SCREEN_FLASH
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 32, SeekOrigin.Begin);
                            ScrFlsh.Unknown1 = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 48, SeekOrigin.Begin);
                            ScrFlsh.Unknown2 = reader.ReadSingle();
                            ScrFlsh.RGBA = reader.ReadBytes(4);
                            Effect = ScrFlsh;
                            Name = "EFFECT_SCREEN_FLASH";
                            break;
                        case 0X2B:
                            EFFECT_AFTER_IMAGE AImage = new EFFECT_AFTER_IMAGE()
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
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
                            AImage.RGBA = reader.ReadBytes(4);
                            Effect = AImage;
                            Name = "EFFECT_AFTER_IMAGE";
                            break;
                        case 0x4C:
                            EFFECT_VIBRATION Vibration = new EFFECT_VIBRATION
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Vibration.Vibration1 = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            Vibration.Vibration2 = reader.ReadUInt32();
                            Effect = Vibration;
                            Name = "EFFECT_VIBRATION";
                            break;
                        default:
                            EFFECT_UNKNOWN DefEffect0 = new EFFECT_UNKNOWN
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                                Data = reader.ReadBytes(76)
                            };
                            Effect = DefEffect0;
                            Name = "EFFECT_UNKNOWN";
                            break;
                    }
                    break;
                case 1:
                    switch (EffectSubType)
                    {
                        case 0:
                            EFFECT_PARTICLE_TRAIL PTCLTrl = new EFFECT_PARTICLE_TRAIL
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 52, SeekOrigin.Begin);
                            PTCLTrl.TrailParam1 = reader.ReadSingle();
                            PTCLTrl.TrailParam2 = reader.ReadSingle();
                            reader.BaseStream.Seek(Start + 64, SeekOrigin.Begin);
                            PTCLTrl.RGBA1 = reader.ReadBytes(4);
                            PTCLTrl.RGBA2 = reader.ReadBytes(4);
                            PTCLTrl.TrailParam3 = reader.ReadSingle();
                            PTCLTrl.Unknown1 = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 88, SeekOrigin.Begin);
                            PTCLTrl.Unknown2 = reader.ReadUInt32();
                            PTCLTrl.Flags = reader.ReadBytes(4);
                            Effect = PTCLTrl;
                            Name = "EFFECT_TRAIL";
                            break;
                        default:
                            EFFECT_PARTICLE_NORMAL PTCLNrml = new EFFECT_PARTICLE_NORMAL
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 32, SeekOrigin.Begin);
                            PTCLNrml.Location = new float[3];
                            PTCLNrml.Location[0] = reader.ReadSingle();
                            PTCLNrml.Location[1] = reader.ReadSingle();
                            PTCLNrml.Location[2] = reader.ReadSingle();
                            reader.BaseStream.Seek(Start + 48, SeekOrigin.Begin);
                            PTCLNrml.Rotation = new float[3];
                            PTCLNrml.Rotation[0] = reader.ReadSingle();
                            PTCLNrml.Rotation[1] = reader.ReadSingle();
                            PTCLNrml.Rotation[2] = reader.ReadSingle();
                            reader.BaseStream.Seek(Start + 60, SeekOrigin.Begin);
                            PTCLNrml.ptclID = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            PTCLNrml.Condition = reader.ReadUInt32();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            PTCLNrml.Flags = reader.ReadBytes(4);
                            Effect = PTCLNrml;
                            Name = "EFFECT_PARTICLE";
                            break;
                    }
                    break;
                case 3:
                    EFFECT_SCREEN_SHAKE ScrShake = new EFFECT_SCREEN_SHAKE
                    {
                        PlayFlag = reader.ReadUInt32(),
                        StartFrame = reader.ReadSingle(),
                        EndFrame = reader.ReadSingle(),
                        Speed = reader.ReadSingle(),
                        BoneID = reader.ReadUInt32(),
                        ShakeIntensity = EffectSubType
                    };
                    reader.BaseStream.Seek(Start + 0x4c, SeekOrigin.Begin);
                    ScrShake.Condition = reader.ReadUInt32();
                    Effect = ScrShake;
                    Name = "EFFECT_SCREEN_SHAKE";
                    break;
                case 4:
                    switch (EffectSubType)
                    {
                        default:
                            EFFECT_UNKNOWN DefEffect4 = new EFFECT_UNKNOWN
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                                Data = reader.ReadBytes(76)
                            };
                            Effect = DefEffect4;
                            Name = "EFFECT_UNKNOWN";
                            break;
                        case 0:
                            if (Version == 0)
                            {
                                EFFECT_DAMAGE Damage = new EFFECT_DAMAGE
                                {
                                    PlayFlag = reader.ReadUInt32(),
                                    StartFrame = reader.ReadSingle(),
                                    EndFrame = reader.ReadSingle(),
                                    Speed = reader.ReadSingle(),
                                    BoneID = reader.ReadUInt32(),
                                };
                                reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                                Damage.Damage = reader.ReadUInt32();
                                Effect = Damage;
                                Name = "EFFECT_DAMAGE";
                            }
                            else
                            {
                                YACT_EVENT Event = new YACT_EVENT
                                {
                                    PlayFlag = reader.ReadUInt32(),
                                    StartFrame = reader.ReadSingle(),
                                    EndFrame = reader.ReadSingle(),
                                    Speed = reader.ReadSingle(),
                                    BoneID = reader.ReadUInt32(),
                                };
                                Name = "EVENT";
                                Effect = Event;
                            }

                                break;
                        case 6:
                            EFFECT_LOOP Loop = new EFFECT_LOOP
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Loop.MaxLoopNum = reader.ReadUInt32();
                            Effect = Loop;
                            Name = "EFFECT_LOOP";
                            break;
                        case 7:
                            EFFECT_RENDA Spam = new EFFECT_RENDA
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Spam.ID = reader.ReadInt32();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            Spam.Button = reader.ReadUInt16();
                            Spam.Count = reader.ReadUInt16();
                            Effect = Spam;
                            Name = "EFFECT_RENDA";
                            break;
                        case 8:
                            EFFECT_TIMING Timing = new EFFECT_TIMING
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Timing.ID = reader.ReadInt16();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            Timing.Button = reader.ReadUInt16();
                            Effect = Timing;
                            Name = "EFFECT_TIMING";
                            break;
                        case 9:
                            EFFECT_NORMAL_BRANCH Branch = new EFFECT_NORMAL_BRANCH
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Branch.ID = reader.ReadInt32();
                            Effect = Branch;
                            Name = "EFFECT_NORMAL_BRANCH";
                            break;
                        case 13:
                            EFFECT_COUNTER_BRANCH CounterBranch = new EFFECT_COUNTER_BRANCH
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            CounterBranch.Unknown1 = reader.ReadInt32();
                            reader.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
                            CounterBranch.Unknown2 = reader.ReadInt32();
                            Effect = CounterBranch;
                            Name = "EFFECT_COUNTER_BRANCH";
                            break;
                        case 10:
                            EFFECT_AUTHORING Finish = new EFFECT_AUTHORING
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            Effect = Finish;
                            Name = "EFFECT_FINISH";
                            break;
                        case 11:
                            EFFECT_COUNTER_UP CounterUp = new EFFECT_COUNTER_UP
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            CounterUp.Unknown = reader.ReadInt32();
                            Effect = CounterUp;
                            Name = "EFFECT_COUNTER_UP";
                            break;
                        case 12:
                            EFFECT_COUNTER_RESET CounterReset = new EFFECT_COUNTER_RESET
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            CounterReset.Unknown = reader.ReadInt32();
                            Effect = CounterReset;
                            Name = "EFFECT_COUNTER_RESET";
                            break;
                        case 18:
                            EFFECT_FINISH_STATUS CFinish = new EFFECT_FINISH_STATUS
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            CFinish.Status = reader.ReadInt32();
                            Effect = CFinish;
                            Name = "EFFECT_FINISH_STATUS";
                            break;
                        case 17:
                        case 16:
                            EFFECT_DEAD Dead = new EFFECT_DEAD
                            {
                                PlayFlag = reader.ReadUInt32(),
                                StartFrame = reader.ReadSingle(),
                                EndFrame = reader.ReadSingle(),
                                Speed = reader.ReadSingle(),
                                BoneID = reader.ReadUInt32(),
                            };
                            reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                            Dead.ID = reader.ReadInt32();
                            Effect = Dead;
                            Name = "EFFECT_DEAD";
                            break;

                    }
                    break;
                case 20:
                case 21:
                    EFFECT_SOUND Cue = new EFFECT_SOUND
                    {
                        PlayFlag = reader.ReadUInt32(),
                        StartFrame = reader.ReadSingle(),
                        EndFrame = reader.ReadSingle(),
                        Speed = reader.ReadSingle(),
                        BoneID = reader.ReadUInt32(),
                    };
                    reader.BaseStream.Seek(Start + 56, SeekOrigin.Begin);
                    Cue.SoundSpeed = reader.ReadSingle();
                    Cue.ContainerID = reader.ReadUInt16();
                    Cue.VoiceID = reader.ReadUInt16();
                    Effect = Cue;
                    Name = "EFFECT_SOUND";
                    break;
                default:
                    EFFECT_UNKNOWN DefEffect = new EFFECT_UNKNOWN
                    {
                        PlayFlag = reader.ReadUInt32(),
                        StartFrame = reader.ReadSingle(),
                        EndFrame = reader.ReadSingle(),
                        Speed = reader.ReadSingle(),
                        BoneID = reader.ReadUInt32(),
                    };
                    Effect = DefEffect;
                    Name = "EFFECT_UNKNOWN";
                    break;
            }
            Effect.Type = EffectType;
            Effect.TypeID = EffectSubType;
            Effect.Name = Name;
            TreeNode ENode = new TreeNode()
            {
                Name = Name,
                Text = Name,
                Tag = Effect
            };
            Node.Nodes.Add(ENode);
            return ENode;
        }
    }
}