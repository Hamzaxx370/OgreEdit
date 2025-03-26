using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OgreEdit.Data;

namespace OgreEdit.Writers
{
    public class WriteCSV
    {
        public void WriteCSVFile(CSVData Data)
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
                        writer.Write((uint)2);
                        writer.Write(new byte[12]);
                        CSVData.CategoryFileHeader C1Header = new CSVData.CategoryFileHeader
                        {
                            Pointer = 48,
                            Size = 0,
                            Index = 1,
                            Identifier = "TCAY"
                        };
                        CSVData.CategoryYActInfo C1Info = new CSVData.CategoryYActInfo
                        {
                            Pointer = 16,
                            Count = (uint)Data.Category1.Count
                        };
                        CSVData.CategoryFileHeader C2Header = new CSVData.CategoryFileHeader
                        {
                            Pointer = 0,
                            Size = 0,
                            Index = 2,
                            Identifier = "TCAY"
                        };
                        CSVData.CategoryYActInfo C2Info = new CSVData.CategoryYActInfo
                        {
                            Pointer = 16,
                            Count = (uint)Data.Category2.Count
                        };
                        writer.Write(new byte[32]);
                        writer.Write(C1Info.Pointer);
                        writer.Write(C1Info.Count);
                        writer.Write(new byte[8]);
                        foreach (CSVData.YAct YAct in Data.Category1)
                        {
                            WriteYAct(YAct, writer);
                        }
                        C2Header.Pointer = (uint)writer.BaseStream.Position;
                        C1Header.Size = (uint)writer.BaseStream.Position - 48;
                        writer.Write(C2Info.Pointer);
                        writer.Write(C2Info.Count);
                        writer.Write(new byte[8]);
                        foreach (CSVData.YAct YAct in Data.Category2)
                        {
                            WriteYAct(YAct, writer);
                        }
                        C2Header.Size = (uint)writer.BaseStream.Position - C2Header.Pointer;
                        writer.Seek(16, SeekOrigin.Begin);
                        writer.Write(C1Header.Pointer);
                        writer.Write(C1Header.Size);
                        writer.Write(C1Header.Index);
                        WriteString(writer,C1Header.Identifier);
                        writer.Write(C2Header.Pointer);
                        writer.Write(C2Header.Size);
                        writer.Write(C2Header.Index);
                        WriteString(writer, C2Header.Identifier);
                    }
                }
            }
        }

        private void WriteString(BinaryWriter writer, string name)
        {
            writer.Write(Encoding.ASCII.GetBytes(name));
        }
        private void WriteYAct(CSVData.YAct YAct,BinaryWriter writer)
        {
            List<uint> AnimPtrs = new List<uint>();
            List<uint> AnimPtrPos = new List<uint>();
            uint Start = (uint)writer.BaseStream.Position;
            writer.Write(YAct.UnkHdr);
            writer.Write(YAct.FileID);
            writer.Write((uint)0);
            writer.Write((uint)128);
            writer.Write((uint)YAct.CamInfos.Count);
            writer.Write(new byte[48]);
            foreach (CSVData.CameraInfo Cam in YAct.CamInfos)
            {
                writer.Write(Cam.Unk);
                writer.Write(Cam.EffectIndex);
                writer.Write(Cam.EffectCount);
                AnimPtrPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Cam.Info.Count);
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
                writer.Write((uint)YAct.Player.Info.Count);
                writer.Write(YAct.Player.HealthCondition);
            }
            uint EnemyPos = (uint)writer.BaseStream.Position - Start;
            foreach (CSVData.Enemy Enemy in YAct.Enemies)
            {
                writer.Write(Enemy.Condition1);
                writer.Write(Enemy.Unknown1);
                writer.Write(Enemy.Condition2);
                writer.Write(Enemy.Unknown2);
                writer.Write(Enemy.EffectIndex);
                writer.Write(Enemy.EffectCount);
                AnimPtrPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Enemy.Info.Count);
                writer.Write(Enemy.HealthCondition);
            }
            uint ObjectPos = (uint)writer.BaseStream.Position - Start;
            foreach (CSVData.Object Object in YAct.Objects)
            {
                writer.Write(Object.Unknown1);
                writer.Write(Object.EffectIndex);
                writer.Write(Object.EffectCount);
                writer.Write(Object.Unknown2);
                AnimPtrPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Object.Info.Count);
                writer.Write(Object.Unknown3);
            }
            uint ArmPos = (uint)writer.BaseStream.Position - Start;
            List<uint> ArmPtrsPos = new List<uint>();
            List<uint> MdlPtrsPos = new List<uint>();
            List<uint> ArmPtrs = new List<uint>();
            List<uint> MdlPtrs = new List<uint>();
            foreach (CSVData.Arm Arm in YAct.Arms)
            {
                writer.Write(Arm.Unknown1);
                ArmPtrsPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Arm.Info.Count);
                writer.Write(Arm.Unknown2);
            }
            uint ModelPos = (uint)writer.BaseStream.Position - Start;
            foreach (CSVData.Unk4 Model in YAct.UnknownC4)
            {
                writer.Write(Model.Unknown1);
                writer.Write(Model.EffectIndex);
                writer.Write(Model.EffectCount);
                MdlPtrsPos.Add((uint)writer.BaseStream.Position);
                writer.Write((uint)0);
                writer.Write((uint)Model.Info.Count);
                writer.Write(Model.Unknown2);
            }
            uint C5Pos = (uint)writer.BaseStream.Position - Start;
            foreach (CSVData.CameraInfo Cam in YAct.CamInfos)
            {
                
                AnimPtrs.Add((uint)writer.BaseStream.Position - Start);
                
                foreach (CSVData.Anim Anim in Cam.Info)
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
            }
           
            AnimPtrs.Add((uint)writer.BaseStream.Position - Start);
            if (YAct.PlayerPointer != 0)
            {
                foreach (CSVData.Anim Anim in YAct.Player.Info)
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
            }
            foreach (CSVData.Enemy Enemy in YAct.Enemies)
            {
                
                AnimPtrs.Add((uint)writer.BaseStream.Position - Start);
                
                foreach (CSVData.Anim Anim in Enemy.Info)
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
            }
            foreach (CSVData.Object Enemy in YAct.Objects)
            {
                
                AnimPtrs.Add((uint)writer.BaseStream.Position - Start);
                
                foreach (CSVData.Anim Anim in Enemy.Info)
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
            }
            foreach (CSVData.Unk4 Enemy in YAct.UnknownC4)
            {

                MdlPtrs.Add((uint)writer.BaseStream.Position - Start);

                foreach (CSVData.Anim Anim in Enemy.Info)
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
            }
            foreach (CSVData.Arm Enemy in YAct.Arms)
            {
                
                ArmPtrs.Add((uint)writer.BaseStream.Position - Start);
               
                foreach (CSVData.Anim Anim in Enemy.Info)
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
            }
            uint EntrySize = (uint)writer.BaseStream.Position - Start;
            int p = 0;
            foreach (uint Pointer in AnimPtrPos)
            {
                writer.BaseStream.Seek(Pointer,SeekOrigin.Begin);
                writer.Write(AnimPtrs[p]);
                p++;
            }
            p = 0;
            foreach (uint Pointer in MdlPtrsPos)
            {
                writer.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                writer.Write(MdlPtrs[p]);
                p++;
            }
            p = 0;
            foreach (uint Pointer in ArmPtrsPos)
            {
                writer.BaseStream.Seek(Pointer, SeekOrigin.Begin);
                writer.Write(ArmPtrs[p]);
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
                writer.Write(new byte[4]);
            }
            writer.BaseStream.Seek(Start + 84, SeekOrigin.Begin);
            writer.Write(EnemyPos);
            writer.Write((uint)YAct.Enemies.Count);
            writer.BaseStream.Seek(Start + 92, SeekOrigin.Begin);
            writer.Write(ObjectPos);
            writer.Write((uint)YAct.Objects.Count);
            writer.BaseStream.Seek(Start + 100, SeekOrigin.Begin);
            writer.Write(ArmPos);
            writer.Write((uint)YAct.Arms.Count);
            writer.BaseStream.Seek(Start + 108, SeekOrigin.Begin);
            writer.Write(ModelPos);
            writer.Write((uint)YAct.UnknownC4.Count);
            writer.BaseStream.Seek(Start + 116, SeekOrigin.Begin);
            writer.Write(EntrySize);
            writer.BaseStream.Seek(Start + EntrySize, SeekOrigin.Begin);
        }
    }
}
