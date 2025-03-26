using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgreEdit.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OgreEdit.Readers
{
    public class ReadMoveProps
    {
        public void ReadBep(string path,ref MovePropData PropData, ref YactData Data)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                MovePropData.Header Header = new MovePropData.Header
                {
                    PropertyOffset = reader.ReadUInt32(),
                    PropertyCount = reader.ReadUInt32(),
                    EffectsOffset = reader.ReadUInt32(),
                    EffectsCount = reader.ReadUInt32(),
                };
                reader.BaseStream.Seek(Header.PropertyOffset,SeekOrigin.Begin);
                int i;
                for (i=0;i<Header.PropertyCount;i++)
                {
                    MovePropData.Property Property = new MovePropData.Property
                    {
                        Type = reader.ReadUInt32(),
                        FrameStart = reader.ReadSingle(),
                        FrameEnd = reader.ReadSingle(),
                    };
                    reader.ReadBytes(8);
                    Property.HitBox = reader.ReadUInt32();
                    reader.ReadBytes(8);
                    PropData.PropData.Add(Property);
                }
                reader.BaseStream.Seek(Header.EffectsOffset, SeekOrigin.Begin);
                int e;
                for (e = 0; e < Header.EffectsCount; e++)
                {
                    uint Start = (uint)reader.BaseStream.Position;
                    ReadEffect(Start, reader, ref Data);
                }
            }
        }
        public void WriteBep(MovePropData PropData, YactData Data)
        {
            using (SaveFileDialog OpenFile = new SaveFileDialog())
            {
                OpenFile.Filter = "PS2 Move Poperty(*.dat) |*.dat";
                OpenFile.Title = "Export Move Property";
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    string path = OpenFile.FileName;
                    using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                    {
                        writer.Write((uint)32);
                        writer.Write((uint)PropData.PropData.Count);
                        writer.Write((uint)((uint)32 + 32 * PropData.PropData.Count));
                        writer.Write((uint)Data.Effects.Count);
                        writer.Write((uint)32 + 32 * PropData.PropData.Count+ 96*Data.Effects.Count);
                        AlignData(writer, 16);
                        foreach (MovePropData.Property Prop in PropData.PropData)
                        {
                            writer.Write(Prop.Type);
                            writer.Write(Prop.FrameStart);
                            writer.Write(Prop.FrameEnd);
                            writer.Write(new byte[8]);
                            writer.Write(Prop.HitBox);
                            writer.Write(new byte[8]);
                        }
                        WriteEffects(writer, Data);
                    }
                }
            }
        }
        private void WriteEffects(BinaryWriter writer, YactData Data)
        {
            foreach (Effects.IEffect effect in Data.Effects)
            {
                uint Ptr = (uint)writer.BaseStream.Position;
                writer.Write(effect.ParentID);
                writer.Write(effect.FrameStart);
                writer.Write(effect.FrameEnd);
                writer.Write(effect.Speed);
                if (effect is Effects.ScreenShakeProp ScrShk)
                {
                    writer.Write(new byte[28]);
                    writer.Write((uint)3);
                    writer.Write(new byte[12]);
                    writer.Write(ScrShk.Intensity);
                    writer.Write(new byte[12]);
                    writer.Write(ScrShk.Flag);
                    writer.Write(new byte[16]);
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
                
            }
        }
        private void AlignData(BinaryWriter writer, int Alignment)
        {
            long currentpos = writer.BaseStream.Position;
            int padding = (int)(Alignment - currentpos % Alignment) % Alignment;
            writer.Write(new byte[padding]);
        }
        public void ReadEffect(uint Start ,BinaryReader reader ,ref YactData Data)
        {
            reader.BaseStream.Seek(Start + 44, SeekOrigin.Begin);
            uint EffectType = reader.ReadUInt32();
            reader.BaseStream.Seek(Start + 60, SeekOrigin.Begin);
            uint EffectSubType = reader.ReadUInt32();
            reader.BaseStream.Seek(Start, SeekOrigin.Begin);
            switch (EffectType)
            {
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
                            reader.BaseStream.Seek(Start + 96, SeekOrigin.Begin);
                            Data.Effects.Add(PTCLTrl);
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
                            reader.BaseStream.Seek(Start + 96, SeekOrigin.Begin);
                            Data.Effects.Add(PTCLNrml);
                            break;
                    }
                    break;
                case 3:
                    Effects.ScreenShakeProp ScrnShk = new Effects.ScreenShakeProp
                    {
                        ParentID = reader.ReadUInt32(),
                        FrameStart = reader.ReadSingle(),
                        FrameEnd = reader.ReadSingle(),
                        Speed = reader.ReadSingle(),
                    };
                    reader.BaseStream.Seek(Start + 60, SeekOrigin.Begin);
                    ScrnShk.Intensity = reader.ReadUInt32();
                    reader.BaseStream.Seek(Start + 76, SeekOrigin.Begin);
                    ScrnShk.Flag = reader.ReadUInt32();
                    reader.BaseStream.Seek(Start + 96, SeekOrigin.Begin);
                    Data.Effects.Add(ScrnShk);
                    break;
            }
        }
    }
}
