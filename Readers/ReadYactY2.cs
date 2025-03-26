using OgreEdit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static OgreEdit.Data.YactDataY2;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace OgreEdit.Readers
{
    public class ReadYactY2
    {
        
        public void ReadY2Yact(string path,ref YactData Data, ref YactDataY2 DataY2)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                Header Header = ParseHeader(reader, ref Data);

                reader.BaseStream.Seek(Header.CameraInfoPointer, SeekOrigin.Begin);
                int i;
                for (i=0;i<Header.CameraInfoCount;i++)
                {
                    CameraInfo CamInfo = ReadCamInfo(reader,Header.EffectsPointer);
                    DataY2.CamInfos.Add(CamInfo);
                }

                reader.BaseStream.Seek(Header.CharaInfoPointer, SeekOrigin.Begin);
                for (i = 0; i < Header.CharaInfoCount; i++)
                {
                    CharaInfo CharaInfo = ReadCharaInfo(reader,Header.EffectsPointer);
                    DataY2.CharaInfos.Add(CharaInfo);
                }
                reader.BaseStream.Seek(Header.EffectsPointer, SeekOrigin.Begin);
                
                reader.BaseStream.Seek(Header.Chunk7Pointer, SeekOrigin.Begin);
                for (i = 0; i < Header.Chunk7Count; i++)
                {
                    uint Pointer = reader.ReadUInt32();
                    long returnpos = reader.BaseStream.Position;
                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                    Data.Chunk1.Add(reader.ReadBytes(32));
                    reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
                }
                reader.BaseStream.Seek(Header.Chunk8Pointer, SeekOrigin.Begin);
                for (i = 0; i < Header.Chunk8Count; i++)
                {
                    uint Pointer = reader.ReadUInt32();
                    long returnpos = reader.BaseStream.Position;
                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                    Data.Chunk2.Add(reader.ReadBytes(32));
                    reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
                }
                reader.BaseStream.Seek(Header.CamAnimPointer, SeekOrigin.Begin);
                for (i = 0; i < Header.CamAnimCount; i++)
                {
                    uint Pointer = reader.ReadUInt32();
                    long returnpos = reader.BaseStream.Position;
                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                    uint MPointer = reader.ReadUInt32();
                    int MSize = reader.ReadInt32();
                    reader.BaseStream.Seek(MPointer, SeekOrigin.Begin);
                    
                    byte[] MTBW = reader.ReadBytes(MSize);
                    Data.MTBWs.Add(MTBW);
                    reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);

                }
                reader.BaseStream.Seek(Header.CharaAnimPointer, SeekOrigin.Begin);
                for (i = 0; i < Header.CharaAnimCount; i++)
                {
                    uint Pointer = reader.ReadUInt32();
                    long returnpos = reader.BaseStream.Position;
                    reader.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                    uint OPointer = reader.ReadUInt32();
                    int OSize = reader.ReadInt32();
                    reader.BaseStream.Seek(OPointer, SeekOrigin.Begin);
                    byte[] OMT = reader.ReadBytes(OSize);
                    Data.OMTs.Add(OMT);
                    reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);

                }
            }
        }

        private string ReadString(BinaryReader reader)
        {
            var Result = new StringBuilder();
            
            while (true)
            {
                char chara = reader.ReadChar();
                if (chara == '\0')
                {
                    break;
                }
                else
                {
                    Result.Append(chara);
                }
            }
            return Result.ToString();
        }

        private CharaInfo ReadCharaInfo(BinaryReader reader,uint EffectPtr)
        {
            CharaInfo CharaInfo = new CharaInfo();
            CharaInfo.NamePtr = reader.ReadUInt32();
            CharaInfo.Pointer = reader.ReadUInt32();
            CharaInfo.Count = reader.ReadUInt32();
            CharaInfo.EffectIndex = reader.ReadUInt32();
            CharaInfo.EffectCount = reader.ReadUInt32();
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(CharaInfo.Pointer, SeekOrigin.Begin);
            CharaInfo.Info = new List<CamCharaInfoEntry>();
            CharaInfo.Effects = new List<Effects.IEffect>();
            for (int i =0;i<CharaInfo.Count;i++)
            {
                CamCharaInfoEntry Anim = new CamCharaInfoEntry();
                Anim.Unk = reader.ReadUInt32();
                Anim.FrameStart = reader.ReadSingle();
                Anim.FrameEnd = reader.ReadSingle();
                Anim.Speed = reader.ReadSingle();
                Anim.AnimID = reader.ReadUInt32();
                Anim.Unknown1 = reader.ReadUInt32();
                Anim.Unknown2 = reader.ReadUInt32();
                Anim.Unknown3 = reader.ReadUInt32();
                CharaInfo.Info.Add(Anim);
            }
            reader.BaseStream.Seek(CharaInfo.NamePtr, SeekOrigin.Begin);
            CharaInfo.Name = ReadString(reader);
            reader.BaseStream.Seek(EffectPtr + 16 * CharaInfo.EffectIndex, SeekOrigin.Begin);
            for (int i = 0; i < CharaInfo.EffectCount; i++)
            {
                EffectHeaderY2 Efct = new EffectHeaderY2();
                Efct.NamePtr = reader.ReadUInt32();
                reader.ReadBytes(8);
                Efct.Pointer = reader.ReadUInt32();
                long returnpose = reader.BaseStream.Position;
                if (Efct.NamePtr != 0)
                {
                    reader.BaseStream.Seek(Efct.NamePtr, SeekOrigin.Begin);
                    Efct.Name = ReadString(reader);
                }
                else
                {
                    Efct.Name = "";
                }
                reader.BaseStream.Seek(Efct.Pointer, SeekOrigin.Begin);
                Effects.IEffect Effect = ReadEffect(Efct.Pointer, reader, Efct.Name, 0);
                CharaInfo.Effects.Add(Effect);
                reader.BaseStream.Seek(returnpose, SeekOrigin.Begin);
            }
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return CharaInfo;
        }

        private CameraInfo ReadCamInfo(BinaryReader reader,uint EffectPtr)
        {
            CameraInfo Info = new CameraInfo();
            Info.Unk = reader.ReadUInt32();
            Info.EffectIndex = reader.ReadUInt32();
            Info.EffectCount = reader.ReadUInt32();
            Info.Pointer = reader.ReadUInt32();
            Info.Count = reader.ReadUInt32();
            long returnpos = reader.BaseStream.Position+12;
            reader.BaseStream.Seek(Info.Pointer, SeekOrigin.Begin);
            Info.Info = new List<CamCharaInfoEntry>();
            Info.Effects = new List<Effects.IEffect>();
            for (int i = 0;i<Info.Count;i++)
            {
                CamCharaInfoEntry Anim = new CamCharaInfoEntry();
                Anim.Unk = reader.ReadUInt32();
                Anim.FrameStart = reader.ReadSingle();
                Anim.FrameEnd = reader.ReadSingle();
                Anim.Speed = reader.ReadSingle();
                Anim.AnimID = reader.ReadUInt32();
                Anim.Unknown1 = reader.ReadUInt32();
                Anim.Unknown2 = reader.ReadUInt32();
                Anim.Unknown3 = reader.ReadUInt32();
                Info.Info.Add(Anim);
            }
            reader.BaseStream.Seek(EffectPtr + 16 * Info.EffectIndex, SeekOrigin.Begin);
            for (int i = 0; i < Info.EffectCount; i++)
            {
                EffectHeaderY2 Efct = new EffectHeaderY2();
                Efct.NamePtr = reader.ReadUInt32();
                reader.ReadBytes(8);
                Efct.Pointer = reader.ReadUInt32();
                long returnpose = reader.BaseStream.Position;
                if (Efct.NamePtr != 0)
                {
                    reader.BaseStream.Seek(Efct.NamePtr, SeekOrigin.Begin);
                    Efct.Name = ReadString(reader);
                }
                else
                {
                    Efct.Name = "";
                }
                reader.BaseStream.Seek(Efct.Pointer, SeekOrigin.Begin);
                Effects.IEffect Effect = ReadEffect(Efct.Pointer, reader, Efct.Name, 0);
                Info.Effects.Add(Effect);
                reader.BaseStream.Seek(returnpose, SeekOrigin.Begin);
            }
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return Info;
        }

        public Effects.IEffect ReadEffect(uint Start, BinaryReader reader, string name,uint Parent)
        {
            reader.BaseStream.Seek(Start + 44, SeekOrigin.Begin);
            uint EffectType = reader.ReadUInt32();
            reader.BaseStream.Seek(Start + 60, SeekOrigin.Begin);
            uint EffectSubType = reader.ReadUInt32();
            reader.BaseStream.Seek(Start, SeekOrigin.Begin);
            switch (EffectType)
            {
                case 0:
                    switch (EffectSubType)
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
                            ScrFlsh.ParentID = Parent;
                            return ScrFlsh;
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
                            AImage.ParentID = Parent;
                            return AImage;
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
                            Vibration.ParentID = Parent;
                            return Vibration;
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
                            DefEffect0.ParentID = Parent;
                            return DefEffect0;
                            break;

                    }
                    break;
                case 1:
                    switch (EffectSubType)
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
                            PTCLTrl.ParentID = Parent;
                            return PTCLTrl;
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
                            PTCLNrml.ParentID = Parent;
                            return PTCLNrml;
                            break;
                    }
                    break;
                case 4:

                    Effects.HactEvent HactEvent = new Effects.HactEvent
                    {
                        Name = name,
                        ParentID = reader.ReadUInt32(),
                        FrameStart = reader.ReadSingle(),
                        FrameEnd = reader.ReadSingle(),
                        Speed = reader.ReadSingle(),
                        BoneNumber = reader.ReadUInt32(),
                    };
                    reader.BaseStream.Seek(Start + 96,SeekOrigin.Begin);
                    HactEvent.ParentID = Parent;
                    return HactEvent;
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
                    Cue.ParentID = Parent;
                    return Cue;
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
                    DefEffect.ParentID = Parent;
                    return DefEffect;
                    break;
            }
        }


        private Header ParseHeader(BinaryReader reader,ref YactData Data)
        {
            Data.UnknownHdr = reader.ReadBytes(32);
            Header Header = new Header();
            Header.CameraInfoPointer = reader.ReadUInt32();
            Header.CameraInfoCount = reader.ReadUInt32();
            Header.CharaInfoPointer = reader.ReadUInt32();
            Header.CharaInfoCount = reader.ReadUInt32();
            Header.Chunk3Pointer = reader.ReadUInt32();
            Header.Chunk3Count = reader.ReadUInt32();
            Header.ArmsInfoPointer = reader.ReadUInt32();
            Header.ArmsInfoCount = reader.ReadUInt32();
            Header.ObjectsInfoPointer = reader.ReadUInt32();
            Header.ObjectsInfoCount = reader.ReadUInt32();
            Header.EffectsPointer = reader.ReadUInt32();
            Header.EffectsCount = reader.ReadUInt32();
            Header.Chunk7Pointer = reader.ReadUInt32();
            Header.Chunk7Count = reader.ReadUInt32();
            Header.Chunk8Pointer = reader.ReadUInt32();
            Header.Chunk8Count = reader.ReadUInt32();
            Header.CamAnimPointer = reader.ReadUInt32();
            Header.CamAnimCount = reader.ReadUInt32();
            Header.Chunk10Pointer = reader.ReadUInt32();
            Header.Chunk10Count = reader.ReadUInt32();
            Header.CharaAnimPointer = reader.ReadUInt32();
            Header.CharaAnimCount = reader.ReadUInt32();
            Header.StringsPointer = reader.ReadUInt32();
            Header.StringsCount = reader.ReadUInt32();
            Header.Chunk13Pointer = reader.ReadUInt32();
            Header.Chunk13Count = reader.ReadUInt32();
            return Header;
        }
    }
}
