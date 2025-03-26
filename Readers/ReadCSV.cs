using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgreEdit.Data;

namespace OgreEdit.Readers
{
    public class ReadCSV
    {
        public void ReadCSVFile(string path,CSVData Data)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                uint Count = reader.ReadUInt32();
                reader.ReadBytes(12);
                ReadCategory(reader, ref Data.Category1);
                ReadCategory(reader, ref Data.Category2);
            }
        }
        private void ReadCategory(BinaryReader reader,ref List<CSVData.YAct> Category)
        {
            CSVData.CategoryFileHeader CategoryHeader = new CSVData.CategoryFileHeader
            {
                Pointer = reader.ReadUInt32(),
                Size = reader.ReadUInt32(),
                Index = reader.ReadUInt32(),
                Identifier = ReadFixedString(reader, 4)
            };
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(CategoryHeader.Pointer , SeekOrigin.Begin);
            CSVData.CategoryYActInfo Info = new CSVData.CategoryYActInfo
            {
                Pointer = reader.ReadUInt32(),
                Count = reader.ReadUInt32(),
            };
            reader.ReadBytes(8);
            for (int y = 0; y < Info.Count; y++)
            {
                long YActStart = reader.BaseStream.Position;
                CSVData.YAct YAct = new CSVData.YAct
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
                    ArmsPointer = reader.ReadUInt32(),
                    ArmsCount = reader.ReadUInt32(),
                    UNK4Pointer = reader.ReadUInt32(),
                    UNK4Count = reader.ReadUInt32(),
                    UNK5Pointer = reader.ReadUInt32(),
                    UNK5Count = reader.ReadUInt32(),
                    Enemies = new List<CSVData.Enemy>(),
                    Objects = new List<CSVData.Object>(),
                    Arms = new List<CSVData.Arm>(),
                    CamInfos = new List<CSVData.CameraInfo>(),
                    UnknownC4 = new List<CSVData.Unk4>()
                };
                reader.BaseStream.Seek(YActStart + YAct.PlayerPointer, SeekOrigin.Begin);
                if (YAct.PlayerPointer != 0)
                {
                    YAct.Player = ReadPlayerData(reader, (uint)YActStart);
                }
                reader.BaseStream.Seek(YActStart + YAct.EnemiesPointer, SeekOrigin.Begin);
                for (int e = 0;e<YAct.EnemiesCount;e++)
                {
                    CSVData.Enemy Enemy = ReadEnemyData(reader,(uint)YActStart);
                    YAct.Enemies.Add(Enemy);
                }
                reader.BaseStream.Seek(YActStart + YAct.ObjectsPointer, SeekOrigin.Begin);
                for (int e = 0; e < YAct.ObjectsCount; e++)
                {
                    CSVData.Object Object = ReadObjectData(reader, (uint)YActStart);
                    YAct.Objects.Add(Object);
                }
                reader.BaseStream.Seek(YActStart + YAct.ArmsPointer, SeekOrigin.Begin);
                for (int e = 0; e < YAct.ArmsCount; e++)
                {
                    CSVData.Arm Arm = ReadArmData(reader, (uint)YActStart);
                    YAct.Arms.Add(Arm);
                }
                reader.BaseStream.Seek(YActStart + YAct.UNK4Pointer, SeekOrigin.Begin);
                for (int e = 0; e < YAct.UNK4Count; e++)
                {
                    CSVData.Unk4 UNK4 = ReadChunk4Data(reader, (uint)YActStart);
                    YAct.UnknownC4.Add(UNK4);
                }
                reader.BaseStream.Seek(YActStart + YAct.CamInfoPtr, SeekOrigin.Begin);
                for (int e = 0; e < YAct.CamInfoCount; e++)
                {
                    CSVData.CameraInfo Camera = ReadCameraData(reader, (uint)YActStart);
                    YAct.CamInfos.Add(Camera);
                }
                reader.BaseStream.Seek(YActStart + YAct.Size, SeekOrigin.Begin);
                Category.Add(YAct);
            }
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
        }
        CSVData.CameraInfo ReadCameraData(BinaryReader reader,uint Start)
        {
            CSVData.CameraInfo Camera = new CSVData.CameraInfo
            {
                Unk = reader.ReadUInt32(),
                EffectIndex = reader.ReadUInt32(),
                EffectCount = reader.ReadUInt32(),
                Pointer = reader.ReadUInt32(),
                Count = reader.ReadUInt32(),
                Info = new List<CSVData.Anim>(),
                Effects = new List<Effects.IEffect>()
            };
            reader.ReadBytes(12);
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(Start + Camera.Pointer, SeekOrigin.Begin);
            for (int i = 0; i < Camera.Count; i++)
            {
                CSVData.Anim Anim = ReadFrameInfo(reader);
                Camera.Info.Add(Anim);
            }
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return Camera;
        }
        private CSVData.Unk4 ReadChunk4Data(BinaryReader reader, uint Start)
        {
            CSVData.Unk4 Unk4 = new CSVData.Unk4
            {
                Unknown1 = reader.ReadBytes(16),
                EffectIndex = reader.ReadUInt32(),
                EffectCount = reader.ReadUInt32(),
                Pointer = reader.ReadUInt32(),
                Count = reader.ReadUInt32(),
                Unknown2 = reader.ReadBytes(32),
                Info = new List<CSVData.Anim>(),
                Effects = new List<Effects.IEffect>()
            };
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(Start + Unk4.Pointer, SeekOrigin.Begin);
            for (int i = 0; i < Unk4.Count; i++)
            {
                CSVData.Anim Anim = ReadFrameInfo(reader);
                Unk4.Info.Add(Anim);
            }
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return Unk4;
        }
        private CSVData.Arm ReadArmData(BinaryReader reader,uint Start)
        {
            CSVData.Arm Arm = new CSVData.Arm
            {
                Unknown1 = reader.ReadBytes(20),
                Pointer = reader.ReadUInt32(),
                Count = reader.ReadUInt32(),
                Unknown2 = reader.ReadBytes(52),
                Info = new List<CSVData.Anim>(),
                Effects = new List<Effects.IEffect>()
            };
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(Start + Arm.Pointer, SeekOrigin.Begin);
            for (int i = 0; i < Arm.Count; i++)
            {
                CSVData.Anim Anim = ReadFrameInfo(reader);
                Arm.Info.Add(Anim);
            }
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return Arm;
        }
        private CSVData.Object ReadObjectData(BinaryReader reader, uint Start)
        {
            CSVData.Object Object = new CSVData.Object
            {
                Unknown1 = reader.ReadBytes(24),
                EffectIndex = reader.ReadUInt32(),
                EffectCount = reader.ReadUInt32(),
                Unknown2 = reader.ReadUInt32(),
                Pointer = reader.ReadUInt32(),
                Count = reader.ReadUInt32(),
                Unknown3 = reader.ReadBytes(20),
                Info = new List<CSVData.Anim>(),
                Effects = new List<Effects.IEffect>()
            };
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(Start + Object.Pointer, SeekOrigin.Begin);
            for (int i = 0; i < Object.Count; i++)
            {
                CSVData.Anim Anim = ReadFrameInfo(reader);
                Object.Info.Add(Anim);
            }
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return Object;
        }
        private CSVData.Enemy ReadEnemyData(BinaryReader reader,uint Start)
        {
            CSVData.Enemy Enemy = new CSVData.Enemy
            {
                Condition1 = reader.ReadUInt32(),
                Unknown1 = reader.ReadBytes(28),
                Condition2 = reader.ReadUInt32(),
                Unknown2 = reader.ReadBytes(8),
                EffectIndex = reader.ReadUInt32(),
                EffectCount = reader.ReadUInt32(),
                Pointer = reader.ReadUInt32(),
                Count = reader.ReadUInt32(),
                HealthCondition = reader.ReadUInt32(),
                Info = new List<CSVData.Anim>(),
                Effects = new List<Effects.IEffect>()
            };
            long returnpos = reader.BaseStream.Position;
            reader.BaseStream.Seek(Start + Enemy.Pointer, SeekOrigin.Begin);
            for (int i = 0; i < Enemy.Count; i++)
            {
                CSVData.Anim Anim = ReadFrameInfo(reader);
                Enemy.Info.Add(Anim);
            }
            reader.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            return Enemy;
        }
        private CSVData.Anim ReadFrameInfo(BinaryReader reader)
        {
            CSVData.Anim Anim = new CSVData.Anim();
            Anim.Unk = reader.ReadUInt32();
            Anim.FrameStart = reader.ReadSingle();
            Anim.FrameEnd = reader.ReadSingle();
            Anim.Speed = reader.ReadSingle();
            Anim.AnimID = reader.ReadUInt32();
            Anim.Unknown1 = reader.ReadUInt32();
            Anim.Unknown2 = reader.ReadUInt32();
            Anim.Unknown3 = reader.ReadUInt32();
            return Anim;
        }

        private CSVData.Player ReadPlayerData(BinaryReader reader,uint Start)
        {
            CSVData.Player Player = new CSVData.Player
            {
                Unknown1 = reader.ReadUInt32(),
                Condition = reader.ReadUInt32(),
                Unknown2 = reader.ReadBytes(28),
                EffectIndex = reader.ReadUInt32(),
                EffectCount = reader.ReadUInt32(),
                Unknown3 = reader.ReadBytes(8),
                Pointer = reader.ReadUInt32(),
                Count = reader.ReadUInt32(),
                HealthCondition = reader.ReadUInt32(),
                Info = new List<CSVData.Anim>(),
                Effects = new List<Effects.IEffect>()
            };
            reader.BaseStream.Seek(Start+Player.Pointer,SeekOrigin.Begin);
            for (int i = 0;i<Player.Count;i++)
            {
                CSVData.Anim Anim = ReadFrameInfo(reader);
                Player.Info.Add(Anim);
            }
            return Player;
        }
        private string ReadFixedString(BinaryReader reader, int Count)
        {
            byte[] Bytes = reader.ReadBytes(Count);
            return Encoding.ASCII.GetString(Bytes).TrimEnd('\0');
        }
    }
}
