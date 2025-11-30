namespace YActLib.Common
{
    public class CVectorParam
    {
        public float[] Base = { 0.0f, 0.0f, 0.0f };
        public float Angle = 0.0f;
        public float[] Change = { 0.0f, 0.0f, 0.0f };
    }

    public class CVertexParam
    {
        public float[] RGBAChange = { 0.0f, 0.0f, 0.0f, 0.0f };
        public float[] RGBABase = { 0.0f, 0.0f, 0.0f, 0.0f };

        public int ScaleFlag;
        public float[] ScaleChange = { 0.0f, 0.0f, 0.0f, 0.0f };
        public float[] Scale = { 0.0f, 0.0f, 0.0f, 0.0f };

        public float[] RotationVelRange = { 0.0f, 0.0f, 0.0f };
        public float[] RotationBase = { 0.0f, 0.0f, 0.0f };
        public float[] AngVelRange = { 0.0f, 0.0f, 0.0f };
        public float[] AngVelBase = { 0.0f, 0.0f, 0.0f };

        public int UVFlag = 0;
        public float[] UVRange = { 0.0f, 0.0f };
        public float[] UVBase = { 0.0f, 0.0f };
        public byte Columns = 1;
        public byte Rows = 1;
        public byte StartFrame = 0;
        public byte EndFrame = 0;
        public float Width = 0.5f;
        public float Height = 0.25f;
    }

    public class CParticleElement
    {
        public int Type = 1;
        public int EffectType = 1;
        public int InheritType = 0;
        public float TimeScale = 0.0f;
        public float[] MEFFELEMENT =
        {
            0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f,
            0.0f, 0.0f, 0.0f, 0.0f,
        };
    }

    public class CParticleEmitter
    {
        // Header
        public int IsEnabled = 1;
        public int GenerateMin = 1;
        public int GenerateMax = 1;
        public int IsSphere = 0;
        public int UseSurfaceNormals = 0;
        public int TransformFlag = 0;
        public float RadiusMin = 0;
        public float RadiusMax = 0;
        public float Angle = 0;
        public float Rot1 = 0;
        public float Rot2 = 0;
        public float ScaleYMin = 1.0f;
        public float ScaleYMax = 1.0f;
        public float ScaleXZMin = 1.0f;
        public float ScaleXZMax = 1.0f;
        public float DirAngleMin = 0;
        public float DirAngleMax = 0;
        public int EPointer = 0;
        public byte[] unk2;

        // Emitter
        public short ElementCount = 0;
        public short DelayMin = 0;
        public short DelayMax = 0;
        public short CycleLenMin = 15;
        public short CycleLenMax = 15;
        public ushort unk3 = 0;

        public float LifeTime = 30.0f;
        public float InverseTimeScale = 0.0f;
        public float UnkAngle = 0.0f;

        public int PoolSize = 8;
        public int VertexType = 2;
        public int RenderState = 0;
        public short ModelID = 0;
        public short TexID = 0;

        public uint Pointer;

        public float TimeScale = -1;

        public CVectorParam VectorParameters = new CVectorParam();

        public CVertexParam VertexParameters = new CVertexParam();

        public byte[]  Data1 = new byte[16];

        public float FrameRate = 30.0f;

        public byte[] Data2 = new byte[24];

        public List<CParticleElement> PosElements;
        public List<CParticleElement> ScaleElements;
        public List<CParticleElement> Color1Elements;
        public List<CParticleElement> RotElements;
        public List<CParticleElement> UVElements;
        public List<CParticleElement> PatternElements;
        public List<CParticleElement> Color2Elements;
    }

    public class CParticle
    {
        public string Name = "";
        public int ID = 0; // Only in y2
        public int Unk = 0;
        public List<CParticleEmitter> Emitters = new List<CParticleEmitter>();
    }

    public class CParticleReaderAndWriter
    {
        public static void WriteParticle(string path, int game, TreeNode ParticleNode)
        {
            int em_counter = 0;
            int el_count = 0;
            int emitter_size = 240;
            if (game == 1)
            {
                emitter_size = 80;
            }
            
            CParticle Particle = ParticleNode.Tag as CParticle;
            Particle.Emitters = new List<CParticleEmitter>();

            foreach (TreeNode node in ParticleNode.Nodes)
            {
                CParticleEmitter Emitter = node.Tag as CParticleEmitter;

                Emitter.PosElements = new List<CParticleElement>();
                Emitter.ScaleElements = new List<CParticleElement>();
                Emitter.Color1Elements = new List<CParticleElement>();
                Emitter.RotElements = new List<CParticleElement>();
                Emitter.UVElements = new List<CParticleElement>();
                Emitter.PatternElements = new List<CParticleElement>();
                Emitter.Color2Elements = new List<CParticleElement>();

                Particle.Emitters.Add(Emitter);

                foreach (TreeNode categoryNode in node.Nodes)
                {
                    foreach (TreeNode elementNode in categoryNode.Nodes)
                    {
                        el_count++;
                        CParticleElement Element = elementNode.Tag as CParticleElement;
                        if (Element == null)
                            continue;

                        switch (Element.Type)
                        {
                            case 0:
                                Emitter.PosElements.Add(Element);
                                break;

                            case 1:
                                Emitter.ScaleElements.Add(Element);
                                break;

                            case 2:
                                Emitter.Color1Elements.Add(Element);
                                break;

                            case 3:
                                Emitter.RotElements.Add(Element);
                                break;

                            case 4:
                                Emitter.UVElements.Add(Element);
                                break;

                            case 5:
                                Emitter.PatternElements.Add(Element);
                                break;

                            case 6:
                                Emitter.Color2Elements.Add(Element);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                Utils.WriteFixedString(writer, Particle.Name, 12);
                writer.Write((int)0);
                writer.Write((int)Particle.Emitters.Count);
                writer.Write((int)Particle.Emitters.Count);
                writer.Write(el_count);
                writer.Write(Particle.Unk);
                writer.Write((int)48);
                writer.Write((int)0);
                writer.Write(Particle.ID);
                writer.Write((int)0);

                int HeaderStart = 48;
                int EmitterStart = HeaderStart + (Particle.Emitters.Count * emitter_size);
                int EmitterEnd = EmitterStart + (Particle.Emitters.Count * 272);
                int ElementEnd = EmitterEnd + (el_count * 64);
                int TotalSize = ElementEnd - 48;

                int CurrElementPtr = EmitterEnd;
                int CurrElementGrpPtr = ElementEnd;
                int CurrEmitterPtr = EmitterStart;

                int Size = 0;

                writer.Write(new byte[TotalSize]);
                writer.Seek(HeaderStart, SeekOrigin.Begin);
                foreach (CParticleEmitter Emitter in Particle.Emitters)
                {
                    writer.Write(Emitter.IsEnabled);
                    writer.Write(Emitter.GenerateMin);
                    writer.Write(Emitter.GenerateMax);
                    writer.Write(Emitter.IsSphere);
                    writer.Write(Emitter.UseSurfaceNormals);
                    writer.Write(Emitter.TransformFlag);
                    writer.Write(Emitter.RadiusMin);
                    writer.Write(Emitter.RadiusMax);
                    writer.Write((int)(Emitter.Angle * 65536.0f / 360.0f));
                    writer.Write((int)(Emitter.Rot1 * 65536.0f / 360.0f));
                    writer.Write((int)(Emitter.Rot2 * 65536.0f / 360.0f));
                    writer.Write(Emitter.ScaleYMin);
                    writer.Write(Emitter.ScaleYMax);
                    writer.Write(Emitter.ScaleXZMin);
                    writer.Write(Emitter.ScaleXZMax);
                    writer.Write((int)(Emitter.DirAngleMin * 65536.0f / 360.0f));
                    writer.Write((int)(Emitter.DirAngleMax * 65536.0f / 360.0f));
                    writer.Write(CurrEmitterPtr);
                    if (game == 0 && (Emitter.unk2 == null || Emitter.unk2.Count() != 168))
                    {
                        Emitter.unk2 = new byte[168];
                        writer.Write(Emitter.unk2);
                    }
                    else if (game == 1 && (Emitter.unk2 == null || Emitter.unk2.Count() != 8))
                    {
                        Emitter.unk2 = new byte[8];
                        writer.Write(Emitter.unk2);
                    }
                    else { writer.Write(Emitter.unk2); };
                    
                    CurrEmitterPtr += 272;
                }
                foreach (CParticleEmitter emitter in Particle.Emitters)
                {
                    writer.Write(emitter.ElementCount);
                    writer.Write(emitter.DelayMin);
                    writer.Write(emitter.DelayMax);
                    writer.Write(emitter.CycleLenMin);
                    writer.Write(emitter.CycleLenMax);
                    writer.Write(emitter.unk3);
                    writer.Write(emitter.LifeTime);
                    writer.Write(emitter.InverseTimeScale);
                    writer.Write((int)(emitter.UnkAngle * 65536.0f / 360.0f));
                    writer.Write(emitter.PoolSize);
                    writer.Write(emitter.VertexType);
                    writer.Write(emitter.RenderState);
                    writer.Write(emitter.ModelID);
                    writer.Write(emitter.TexID);

                    writer.Write(CurrElementGrpPtr);

                    writer.Write(emitter.TimeScale);

                    writer.Write(emitter.VectorParameters.Base[0]);
                    writer.Write(emitter.VectorParameters.Base[1]);
                    writer.Write(emitter.VectorParameters.Base[2]);
                    writer.Write((int)(emitter.VectorParameters.Angle * 65536.0f / 360.0f));
                    writer.Write(emitter.VectorParameters.Change[0]);
                    writer.Write(emitter.VectorParameters.Change[1]);
                    writer.Write(emitter.VectorParameters.Change[2]);
                    writer.Write(0);

                    writer.Write(emitter.VertexParameters.RGBAChange[0]);
                    writer.Write(emitter.VertexParameters.RGBAChange[1]);
                    writer.Write(emitter.VertexParameters.RGBAChange[2]);
                    writer.Write(emitter.VertexParameters.RGBAChange[3]);
                    writer.Write(emitter.VertexParameters.RGBABase[0]);
                    writer.Write(emitter.VertexParameters.RGBABase[1]);
                    writer.Write(emitter.VertexParameters.RGBABase[2]);
                    writer.Write(emitter.VertexParameters.RGBABase[3]);
                    writer.Write(emitter.VertexParameters.ScaleFlag);
                    writer.Write(emitter.VertexParameters.ScaleChange[0]);
                    writer.Write(emitter.VertexParameters.ScaleChange[1]);
                    writer.Write(emitter.VertexParameters.ScaleChange[2]);
                    writer.Write(emitter.VertexParameters.ScaleChange[3]);
                    writer.Write(emitter.VertexParameters.Scale[0]);
                    writer.Write(emitter.VertexParameters.Scale[1]);
                    writer.Write(emitter.VertexParameters.Scale[2]);
                    writer.Write(emitter.VertexParameters.Scale[3]);
                    writer.Write((int)(emitter.VertexParameters.RotationVelRange[0] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.RotationVelRange[1] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.RotationVelRange[2] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.RotationBase[0] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.RotationBase[1] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.RotationBase[2] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.AngVelRange[0] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.AngVelRange[1] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.AngVelRange[2] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.AngVelBase[0] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.AngVelBase[1] * 65536.0f / 360.0f));
                    writer.Write((int)(emitter.VertexParameters.AngVelBase[2] * 65536.0f / 360.0f));
                    writer.Write(emitter.VertexParameters.UVFlag);
                    writer.Write(emitter.VertexParameters.UVRange[0]);
                    writer.Write(emitter.VertexParameters.UVRange[1]);
                    writer.Write(emitter.VertexParameters.UVBase[0]);
                    writer.Write(emitter.VertexParameters.UVBase[1]);
                    writer.Write(emitter.VertexParameters.Columns);
                    writer.Write(emitter.VertexParameters.Rows);
                    writer.Write(emitter.VertexParameters.StartFrame);
                    writer.Write(emitter.VertexParameters.EndFrame);
                    writer.Write(emitter.VertexParameters.Width);
                    writer.Write(emitter.VertexParameters.Height);

                    writer.Write(emitter.Data1);
                    writer.Write(emitter.FrameRate);
                    writer.Write(emitter.Data2);

                    int ReturnPos = (int)writer.BaseStream.Position;

                    int el_ct = 0;
                    writer.Seek(CurrElementGrpPtr, SeekOrigin.Begin);
                    if ( emitter.PosElements.Count > 0 )
                    {
                        el_ct++;
                        writer.Write(CurrElementPtr);
                        writer.Seek(CurrElementPtr, SeekOrigin.Begin);

                        foreach (CParticleElement element in emitter.PosElements)
                        {
                            writer.Write(element.Type);
                            writer.Write(element.EffectType);
                            writer.Write(element.InheritType);
                            writer.Write(element.TimeScale);
                            for (int f = 0; f < 12; f++)
                            {
                                writer.Write(element.MEFFELEMENT[f]);
                            }
                            CurrElementPtr += 64;
                        }
                        CurrElementGrpPtr += 4;
                        writer.Seek(CurrElementGrpPtr, SeekOrigin.Begin);
                    }
                    if (emitter.ScaleElements.Count > 0)
                    {
                        el_ct++;
                        writer.Write(CurrElementPtr);
                        writer.Seek(CurrElementPtr, SeekOrigin.Begin);

                        foreach (CParticleElement element in emitter.ScaleElements)
                        {
                            writer.Write(element.Type);
                            writer.Write(element.EffectType);
                            writer.Write(element.InheritType);
                            writer.Write(element.TimeScale);
                            for (int f = 0; f < 12; f++)
                            {
                                writer.Write(element.MEFFELEMENT[f]);
                            }
                            CurrElementPtr += 64;
                        }
                        CurrElementGrpPtr += 4;
                        writer.Seek(CurrElementGrpPtr, SeekOrigin.Begin);
                    }
                    if (emitter.Color1Elements.Count > 0)
                    {
                        el_ct++;
                        writer.Write(CurrElementPtr);
                        writer.Seek(CurrElementPtr, SeekOrigin.Begin);

                        foreach (CParticleElement element in emitter.Color1Elements)
                        {
                            writer.Write(element.Type);
                            writer.Write(element.EffectType);
                            writer.Write(element.InheritType);
                            writer.Write(element.TimeScale);
                            for (int f = 0; f < 12; f++)
                            {
                                writer.Write(element.MEFFELEMENT[f]);
                            }
                            CurrElementPtr += 64;
                        }
                        CurrElementGrpPtr += 4;
                        writer.Seek(CurrElementGrpPtr, SeekOrigin.Begin);
                    }
                    if (emitter.RotElements.Count > 0)
                    {
                        el_ct++;
                        writer.Write(CurrElementPtr);
                        writer.Seek(CurrElementPtr, SeekOrigin.Begin);

                        foreach (CParticleElement element in emitter.RotElements)
                        {
                            writer.Write(element.Type);
                            writer.Write(element.EffectType);
                            writer.Write(element.InheritType);
                            writer.Write(element.TimeScale);
                            for (int f = 0; f < 12; f++)
                            {
                                writer.Write(element.MEFFELEMENT[f]);
                            }
                            CurrElementPtr += 64;
                        }
                        CurrElementGrpPtr += 4;
                        writer.Seek(CurrElementGrpPtr, SeekOrigin.Begin);
                    }
                    if (emitter.UVElements.Count > 0)
                    {
                        el_ct++;
                        writer.Write(CurrElementPtr);
                        writer.Seek(CurrElementPtr, SeekOrigin.Begin);

                        foreach (CParticleElement element in emitter.UVElements)
                        {
                            writer.Write(element.Type);
                            writer.Write(element.EffectType);
                            writer.Write(element.InheritType);
                            writer.Write(element.TimeScale);
                            for (int f = 0; f < 12; f++)
                            {
                                writer.Write(element.MEFFELEMENT[f]);
                            }
                            CurrElementPtr += 64;
                        }
                        CurrElementGrpPtr += 4;
                        writer.Seek(CurrElementGrpPtr, SeekOrigin.Begin);
                    }
                    if (emitter.PatternElements.Count > 0)
                    {
                        el_ct++;
                        writer.Write(CurrElementPtr);
                        writer.Seek(CurrElementPtr, SeekOrigin.Begin);

                        foreach (CParticleElement element in emitter.PatternElements)
                        {
                            writer.Write(element.Type);
                            writer.Write(element.EffectType);
                            writer.Write(element.InheritType);
                            writer.Write(element.TimeScale);
                            for (int f = 0; f < 12; f++)
                            {
                                writer.Write(element.MEFFELEMENT[f]);
                            }
                            CurrElementPtr += 64;
                        }
                        CurrElementGrpPtr += 4;
                        writer.Seek(CurrElementGrpPtr, SeekOrigin.Begin);
                    }
                    if (emitter.Color2Elements.Count > 0)
                    {
                        el_ct++;
                        writer.Write(CurrElementPtr);
                        writer.Seek(CurrElementPtr, SeekOrigin.Begin);

                        foreach (CParticleElement element in emitter.Color2Elements)
                        {
                            writer.Write(element.Type);
                            writer.Write(element.EffectType);
                            writer.Write(element.InheritType);
                            writer.Write(element.TimeScale);
                            for (int f = 0; f < 12; f++)
                            {
                                writer.Write(element.MEFFELEMENT[f]);
                            }
                            CurrElementPtr += 64;
                        }
                        CurrElementGrpPtr += 4;
                        writer.Seek(CurrElementGrpPtr, SeekOrigin.Begin);
                    }
                    Size = ((int)writer.BaseStream.Position);
                    writer.Seek(ReturnPos, SeekOrigin.Begin);
                }
                writer.Seek(Size, SeekOrigin.Begin);
                Utils.AlignData(writer, 16);
                Size = ((int)writer.BaseStream.Position);
                writer.Seek(12, SeekOrigin.Begin);
                writer.Write(Size);

            }
        }

        public static void ReadParticle(string path, int game, TreeView MainTree)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                CParticle Particle = new CParticle();
                Particle.Name = YActLib.Utils.ReadFixedString(reader, 12);
                int FileSize = reader.ReadInt32();
                int EmitterCount = reader.ReadInt32();
                EmitterCount = reader.ReadInt32();
                int ElementCount = reader.ReadInt32();
                Particle.Unk = reader.ReadInt32();
                int SPtr = reader.ReadInt32();
                int Pad = reader.ReadInt32();
                if ( game == 1 )
                {
                    Particle.ID = reader.ReadInt32();
                }

                TreeNode PNode = new TreeNode()
                {
                    Text = Particle.Name,
                    Tag = Particle
                };

                reader.BaseStream.Seek(SPtr, SeekOrigin.Begin);

                for (int i = 0; i < EmitterCount; i++)
                {
                    CParticleEmitter Emitter = new CParticleEmitter();
                    Emitter.IsEnabled = reader.ReadInt32();
                    Emitter.GenerateMin = reader.ReadInt32();
                    Emitter.GenerateMax = reader.ReadInt32();
                    Emitter.IsSphere = reader.ReadInt32();
                    Emitter.UseSurfaceNormals = reader.ReadInt32();
                    Emitter.TransformFlag = reader.ReadInt32();
                    Emitter.RadiusMin = reader.ReadSingle();
                    Emitter.RadiusMax = reader.ReadSingle();
                    Emitter.Angle = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.Rot1 = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.Rot2 = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.ScaleYMin = reader.ReadSingle();
                    Emitter.ScaleYMax = reader.ReadSingle();
                    Emitter.ScaleXZMin = reader.ReadSingle();
                    Emitter.ScaleXZMax = reader.ReadSingle();
                    Emitter.DirAngleMin = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.DirAngleMax = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;

                    Emitter.EPointer = reader.ReadInt32();
                    if ( game == 1 )
                    {
                        Emitter.unk2 = reader.ReadBytes(8);
                    }
                    else
                    {
                        Emitter.unk2 = reader.ReadBytes(168);
                    }
                    long EReturnPos = reader.BaseStream.Position;
                    reader.BaseStream.Seek(Emitter.EPointer, SeekOrigin.Begin);

                    Emitter.ElementCount = reader.ReadInt16();
                    Emitter.DelayMin = reader.ReadInt16();
                    Emitter.DelayMax = reader.ReadInt16();
                    Emitter.CycleLenMin = reader.ReadInt16();
                    Emitter.CycleLenMax = reader.ReadInt16();
                    Emitter.unk3 = reader.ReadUInt16();
                    Emitter.LifeTime = reader.ReadSingle();
                    Emitter.InverseTimeScale = reader.ReadSingle();
                    Emitter.UnkAngle = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.PoolSize = reader.ReadInt32();
                    Emitter.VertexType = reader.ReadInt32();
                    Emitter.RenderState = reader.ReadInt32();
                    Emitter.ModelID = reader.ReadInt16();
                    Emitter.TexID = reader.ReadInt16();
                    Emitter.Pointer = reader.ReadUInt32();
                    Emitter.TimeScale = reader.ReadSingle();
                    
                    Emitter.VectorParameters.Base[0] = reader.ReadSingle();
                    Emitter.VectorParameters.Base[1] = reader.ReadSingle();
                    Emitter.VectorParameters.Base[2] = reader.ReadSingle();
                    Emitter.VectorParameters.Angle = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VectorParameters.Change[0] = reader.ReadSingle();
                    Emitter.VectorParameters.Change[1] = reader.ReadSingle();
                    Emitter.VectorParameters.Change[2] = reader.ReadSingle();
                    reader.ReadInt32();

                    Emitter.VertexParameters.RGBAChange[0] = reader.ReadSingle();
                    Emitter.VertexParameters.RGBAChange[1] = reader.ReadSingle();
                    Emitter.VertexParameters.RGBAChange[2] = reader.ReadSingle();
                    Emitter.VertexParameters.RGBAChange[3] = reader.ReadSingle();
                    Emitter.VertexParameters.RGBABase[0] = reader.ReadSingle();
                    Emitter.VertexParameters.RGBABase[1] = reader.ReadSingle();
                    Emitter.VertexParameters.RGBABase[2] = reader.ReadSingle();
                    Emitter.VertexParameters.RGBABase[3] = reader.ReadSingle();
                    Emitter.VertexParameters.ScaleFlag = reader.ReadInt32();
                    Emitter.VertexParameters.ScaleChange[0] = reader.ReadSingle();
                    Emitter.VertexParameters.ScaleChange[1] = reader.ReadSingle();
                    Emitter.VertexParameters.ScaleChange[2] = reader.ReadSingle();
                    Emitter.VertexParameters.ScaleChange[3] = reader.ReadSingle();
                    Emitter.VertexParameters.Scale[0] = reader.ReadSingle();
                    Emitter.VertexParameters.Scale[1] = reader.ReadSingle();
                    Emitter.VertexParameters.Scale[2] = reader.ReadSingle();
                    Emitter.VertexParameters.Scale[3] = reader.ReadSingle();
                    Emitter.VertexParameters.RotationVelRange[0] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.RotationVelRange[1] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.RotationVelRange[2] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.RotationBase[0] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.RotationBase[1] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.RotationBase[2] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.AngVelRange[0] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.AngVelRange[1] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.AngVelRange[2] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.AngVelBase[0] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.AngVelBase[1] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.AngVelBase[2] = ((float)reader.ReadInt32() / 65536.0f) * 360.0f;
                    Emitter.VertexParameters.UVFlag = reader.ReadInt32();
                    Emitter.VertexParameters.UVRange[0] = reader.ReadSingle();
                    Emitter.VertexParameters.UVRange[1] = reader.ReadSingle();
                    Emitter.VertexParameters.UVBase[0] = reader.ReadSingle();
                    Emitter.VertexParameters.UVBase[1] = reader.ReadSingle();
                    Emitter.VertexParameters.Columns = reader.ReadByte();
                    Emitter.VertexParameters.Rows = reader.ReadByte();
                    Emitter.VertexParameters.StartFrame = reader.ReadByte();
                    Emitter.VertexParameters.EndFrame = reader.ReadByte();
                    Emitter.VertexParameters.Width = reader.ReadSingle();
                    Emitter.VertexParameters.Height = reader.ReadSingle();

                    Emitter.Data1 = reader.ReadBytes(16);
                    Emitter.FrameRate = reader.ReadSingle();
                    Emitter.Data2 = reader.ReadBytes(24);

                    TreeNode EMNode = new TreeNode()
                    {
                        Text = "Emitter",
                        Tag = Emitter
                    };
                    PNode.Nodes.Add(EMNode);

                    TreeNode PEMNode = new TreeNode()
                    {
                        Text = "Position Elements"
                    };
                    TreeNode SEMNode = new TreeNode()
                    {
                        Text = "Scale Elements"
                    };
                    TreeNode C1EMNode = new TreeNode()
                    {
                        Text = "Color 1 Elements"
                    };
                    TreeNode REMNode = new TreeNode()
                    {
                        Text = "Rotation Elements"
                    };
                    TreeNode UEMNode = new TreeNode()
                    {
                        Text = "UV Elements"
                    };
                    TreeNode PAEMNode = new TreeNode()
                    {
                        Text = "Pattern Elements"
                    };
                    TreeNode C2EMNode = new TreeNode()
                    {
                        Text = "Color 2 Elements"
                    };

                    EMNode.Nodes.Add(PEMNode);
                    EMNode.Nodes.Add(SEMNode);
                    EMNode.Nodes.Add(C1EMNode);
                    EMNode.Nodes.Add(C2EMNode);
                    EMNode.Nodes.Add(REMNode);
                    EMNode.Nodes.Add(UEMNode);
                    EMNode.Nodes.Add(PAEMNode);

                    reader.BaseStream.Seek(Emitter.Pointer, SeekOrigin.Begin);

                    for (int e = 0; e < Emitter.ElementCount; e++)
                    {
                        int ptr = reader.ReadInt32();
                        long EMReturnPos = reader.BaseStream.Position;
                        reader.BaseStream.Seek(ptr, SeekOrigin.Begin);
                        while (true)
                        {
                            CParticleElement Element = new CParticleElement();
                            Element.Type = reader.ReadInt32();
                            Element.EffectType = reader.ReadInt32();
                            Element.InheritType = reader.ReadInt32();
                            Element.TimeScale = reader.ReadSingle();
                            for (int f = 0; f < 12; f++)
                            {
                                Element.MEFFELEMENT[f] = reader.ReadSingle();
                            }
                            TreeNode ElementNode = new TreeNode()
                            {
                                Tag = Element,
                                Text = "Element"
                            };
                            switch (Element.Type) {
                                default:
                                    break;
                                case 0:
                                    PEMNode.Nodes.Add(ElementNode);
                                    break;
                                case 1:
                                    SEMNode.Nodes.Add(ElementNode);
                                    break;
                                case 2:
                                    C1EMNode.Nodes.Add(ElementNode);
                                    break;
                                case 3:
                                    REMNode.Nodes.Add(ElementNode);
                                    break;
                                case 4:
                                    UEMNode.Nodes.Add(ElementNode);
                                    break;
                                case 5:
                                    PAEMNode.Nodes.Add(ElementNode);
                                    break;
                                case 6:
                                    C2EMNode.Nodes.Add(ElementNode);
                                    break;
                            }
                            if (Element.TimeScale == 1.0f)
                            {
                                break;
                            }
                        }
                        reader.BaseStream.Seek(EMReturnPos, SeekOrigin.Begin);
                    }

                    reader.BaseStream.Seek(EReturnPos, SeekOrigin.Begin);
                }

                MainTree.Nodes.Add(PNode);
            }
        }
    }
}