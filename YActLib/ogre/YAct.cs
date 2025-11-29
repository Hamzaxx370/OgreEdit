using YActLib.Common;

namespace YActLib.ogre1
{
    public class YActHeaderY1
    {
        public uint FileSize;
        public uint EffectTable;
        public uint EffectTableCount;
        public uint Chunk1;
        public uint Chunk1Count;
        public uint Chunk2;
        public uint Chunk2Count;
        public uint MTBW;
        public uint MTBWCount;
        public uint OME;
        public uint OMECount;
        public uint OMT;
        public uint OMTCount;
        public uint TXB;
        public uint TXBCount;
    }
    public class CYActWriterY1
    {
        public List<byte[]> OMTs;
        public List<byte[]> MTBWs;
        public List<byte[]> Models;
        public List<byte[]> Textures;
        public List<EFFECT_AUTHORING> Effects;
        public void WriteYAct(string path, TreeNode Node)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                OMTs = new List<byte[]>();
                MTBWs = new List<byte[]>();
                Models = new List<byte[]>();
                Textures = new List<byte[]>();
                Effects = new List<EFFECT_AUTHORING>();
                List<uint> EffectPointers = new List<uint>();
                List<uint> Chunk1Pointers = new List<uint>();
                List<uint> Chunk2Pointers = new List<uint>();
                List<uint> MTBWPointers = new List<uint>();
                List<uint> OMEPointers = new List<uint>();
                List<uint> OMTPointers = new List<uint>();
                List<uint> TXBPointers = new List<uint>();
                YActHeaderY1 Header = new YActHeaderY1();
                PlayDataY1 YAct = RetreiveYActDataForYActBin(Node);
                writer.Write(new byte[64]);
                Header.EffectTable = (uint)writer.BaseStream.Position;
                writer.Write(new byte[4 * Effects.Count]);
                Header.Chunk1 = (uint)writer.BaseStream.Position;
                writer.Write(new byte[4 * YAct.YActChunk1Data.Count]);
                Header.Chunk2 = (uint)writer.BaseStream.Position;
                writer.Write(new byte[4 * YAct.YActChunk2Data.Count]);
                Header.MTBW = (uint)writer.BaseStream.Position;
                writer.Write(new byte[4 * MTBWs.Count]);
                Header.OME = (uint)writer.BaseStream.Position;
                writer.Write(new byte[4 * Models.Count]);
                Header.OMT = (uint)writer.BaseStream.Position;
                writer.Write(new byte[4 * OMTs.Count]);
                Header.TXB = (uint)writer.BaseStream.Position;
                writer.Write(new byte[4 * Textures.Count]);
                Utils.AlignData(writer, 16);
                BaseReaderAndWriter.WriteEffects(writer, Effects, EffectPointers);
                foreach (byte[] Data in YAct.YActChunk1Data)
                {
                    uint Pointer = (uint)writer.BaseStream.Position;
                    Chunk1Pointers.Add(Pointer);
                    writer.Write(Data);
                }
                Utils.AlignData(writer, 16);
                foreach (byte[] Data in YAct.YActChunk2Data)
                {
                    uint Pointer = (uint)writer.BaseStream.Position;
                    Chunk2Pointers.Add(Pointer);
                    writer.Write(Data);
                }
                Utils.AlignData(writer, 16);
                foreach (byte[] File in MTBWs)
                {
                    WriteFileInYAct(writer, File, MTBWPointers);
                }
                foreach (byte[] File in Models)
                {
                    WriteFileInYAct(writer, File, OMEPointers);
                }
                foreach (byte[] File in OMTs)
                {
                    WriteFileInYAct(writer, File, OMTPointers);
                }
                foreach (byte[] File in Textures)
                {
                    WriteFileInYAct(writer, File, TXBPointers);
                }
                uint FileSize = (uint)writer.BaseStream.Position;
                writer.BaseStream.Seek(0, SeekOrigin.Begin);
                writer.Write(FileSize);
                writer.Write(Header.EffectTable);
                writer.Write(Effects.Count);
                writer.Write(Header.Chunk1);
                writer.Write(Chunk1Pointers.Count);
                writer.Write(Header.Chunk2);
                writer.Write(Chunk2Pointers.Count);
                writer.Write(Header.MTBW);
                writer.Write(MTBWPointers.Count);
                writer.Write(Header.OME);
                writer.Write(OMEPointers.Count);
                writer.Write(Header.OMT);
                writer.Write(OMTPointers.Count);
                writer.Write(Header.TXB);
                writer.Write(TXBPointers.Count);
                writer.BaseStream.Seek(Header.EffectTable, SeekOrigin.Begin);
                foreach (uint Pointer in EffectPointers)
                {
                    writer.Write(Pointer);
                }
                foreach (uint Pointer in Chunk1Pointers)
                {
                    writer.Write(Pointer);
                }
                foreach (uint Pointer in Chunk2Pointers)
                {
                    writer.Write(Pointer);
                }
                foreach (uint Pointer in MTBWPointers)
                {
                    writer.Write(Pointer);
                }
                foreach (uint Pointer in OMEPointers)
                {
                    writer.Write(Pointer);
                }
                foreach (uint Pointer in OMTPointers)
                {
                    writer.Write(Pointer);
                }
                foreach (uint Pointer in TXBPointers)
                {
                    writer.Write(Pointer);
                }
            }
        }
        public static void WriteFileInYAct(BinaryWriter writer, byte[] File, List<uint> List)
        {
            uint Pointer = (uint)writer.BaseStream.Position;
            List.Add(Pointer);
            writer.Write(Pointer + 16);
            writer.Write(File.Length);
            writer.Write(new byte[8]);
            writer.Write(File);
            Utils.AlignData(writer, 16);
        }
        public PlayDataY1 RetreiveYActDataForYActBin(TreeNode Node)
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
                        Entity.EffectIndex = EffectIndex;
                        foreach (TreeNode Child in EntityNode.Nodes)
                        {
                            if (Child.Tag is YActAnimation Anim)
                            {
                                if (Anim.Animation != null && EntityNode.Tag is CYActCamera Camera)
                                {
                                    Anim.AnimID = CamAnimIndex;
                                    MTBWs.Add(Anim.Animation);
                                    CamAnimIndex++;
                                }
                                else if (Anim.Animation != null && Entity is not CYActCamera)
                                {
                                    Anim.AnimID = CharaAnimIndex;
                                    OMTs.Add(Anim.Animation);
                                    CharaAnimIndex++;
                                }
                                else
                                {
                                    Anim.AnimID = -1;
                                }
                                Entity.Animations.Add(Anim);
                            }
                            else if (Child.Tag is EFFECT_AUTHORING EFFECT)
                            {
                                Entity.Effects.Add(EFFECT);
                                Effects.Add(EFFECT);
                                EffectIndex++;
                                EffectCount++;
                            }
                        }
                        Entity.EffectCount = EffectCount;
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
                                    Model.ModelID = ModelIndex;
                                    Models.Add(Model.OME);
                                    Model.TextureID = ModelIndex;
                                    Textures.Add(Model.TXB);
                                    ModelIndex++;
                                }
                                PlayData.ExMotions.Add(Model);
                                break;

                        }
                        break;
                }
            }

            return PlayData;
        }
    }
    public class CYActReaderY1
    {
        /// <summary>
        /// Fills in the remaining entity data
        /// like effects and animation files
        /// </summary>
        /// <param name="path"></param>
        /// <param name="PlayDataNode"></param>
        public static void ReadYAct(string path, TreeNode PlayDataNode)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                YActHeaderY1 Header = new YActHeaderY1()
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
                    TXB = reader.ReadUInt32(),
                    TXBCount = reader.ReadUInt32(),
                };
                PlayDataY1 PlayData = PlayDataNode.Tag as PlayDataY1;
                //Get Unk Data
                PlayData.YActChunk1Data = new List<byte[]>();
                PlayData.YActChunk2Data = new List<byte[]>();
                reader.BaseStream.Seek(Header.Chunk1, SeekOrigin.Begin);
                for (int i = 0; i < Header.Chunk1Count; i++)
                {
                    uint Pointer = reader.ReadUInt32();
                    long returnpos = reader.BaseStream.Position;
                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                    PlayData.YActChunk1Data.Add(reader.ReadBytes(32));
                    reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
                }
                reader.BaseStream.Seek(Header.Chunk2, SeekOrigin.Begin);
                for (int i = 0; i < Header.Chunk2Count; i++)
                {
                    uint Pointer = reader.ReadUInt32();
                    long returnpos = reader.BaseStream.Position;
                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                    PlayData.YActChunk2Data.Add(reader.ReadBytes(32));
                    reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
                }
                //Get Entity Data
                foreach (TreeNode Node in PlayDataNode.Nodes)
                {
                    if (Node.Tag is CYActEntityBase Entity)
                    {
                        //Get Effects
                        for (int i = 0; i < Entity.EffectCount; i++)
                        {
                            reader.BaseStream.Seek(Header.EffectTable + (Entity.EffectIndex * 4) + (i * 4), SeekOrigin.Begin);
                            uint Pointer = reader.ReadUInt32();
                            BaseReaderAndWriter.ReadEffect(Pointer, reader, 0 , Node);
                        }
                        //Get Animations
                        for (int i = 0; i < Node.Nodes.Count; i++)
                        {
                            TreeNode Child = (TreeNode)Node.Nodes[i];
                            if (Child.Tag is YActAnimation Anim)
                            {
                                if ((int)Anim.AnimID != -1)
                                {
                                    if (Node.Tag is CYActCamera Camera)
                                    {
                                        reader.BaseStream.Seek(Header.MTBW + (Anim.AnimID * 4), SeekOrigin.Begin);
                                        uint Pointer = reader.ReadUInt32();
                                        reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                                        uint AnimPtr = reader.ReadUInt32();
                                        int Size = reader.ReadInt32();
                                        reader.BaseStream.Seek(AnimPtr, SeekOrigin.Begin);
                                        Anim.Animation = reader.ReadBytes(Size);
                                        Child.Tag = Anim;
                                    }
                                    else
                                    {
                                        reader.BaseStream.Seek(Header.OMT + (Anim.AnimID * 4), SeekOrigin.Begin);
                                        uint Pointer = reader.ReadUInt32();
                                        reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                                        uint AnimPtr = reader.ReadUInt32();
                                        int Size = reader.ReadInt32();
                                        reader.BaseStream.Seek(AnimPtr, SeekOrigin.Begin);
                                        Anim.Animation = reader.ReadBytes(Size);
                                        Child.Tag = Anim;
                                    }
                                }
                            }
                        }
                        //Handle Models and Textures
                        if (Node.Tag is CYActExMotion Model)
                        {
                            //Get Model
                            if (Model.ModelID != -1)
                            {
                                reader.BaseStream.Seek(Header.OME + (Model.ModelID * 4), SeekOrigin.Begin);
                                uint Pointer = reader.ReadUInt32();
                                reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                                uint FilePtr = reader.ReadUInt32();
                                int Size = reader.ReadInt32();
                                reader.BaseStream.Seek(FilePtr, SeekOrigin.Begin);
                                Model.OME = reader.ReadBytes(Size);

                                //Get Texture
                                reader.BaseStream.Seek(Header.TXB + (Model.TextureID * 4), SeekOrigin.Begin);
                                Pointer = reader.ReadUInt32();
                                reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                                FilePtr = reader.ReadUInt32();
                                Size = reader.ReadInt32();
                                reader.BaseStream.Seek(FilePtr, SeekOrigin.Begin);
                                Model.TXB = reader.ReadBytes(Size);

                                Node.Tag = Model;
                            }

                        }
                    }
                }
            }
        }
    }
}
