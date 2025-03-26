using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgreEdit.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OgreEdit.Writers
{
    public class WriteCSVY2
    {
        private List<uint> StringPtrs = new List<uint>();
        private List<string> Strings = new List<string>();
        private List<string> NStrings;
        private List<uint> CharaPtrs = new List<uint>();
        private List<CSVDataY2.Character> Characters = new List<CSVDataY2.Character>();
        private List<uint> ObjectPtrs = new List<uint>();
        private List<CSVDataY2.Character> NCharacters;
        private List<CSVDataY2.Object> Objects = new List<CSVDataY2.Object>();
        private List<CSVDataY2.Object> NObjects = new List<CSVDataY2.Object>();
        private List<uint> ArmsPtrs = new List<uint>();
        private List<CSVDataY2.Arm> Arms = new List<CSVDataY2.Arm>();
        private List<CSVDataY2.Arm> NArms;
        private List<uint> HEPtrs = new List<uint>();
        private List<CSVDataY2.IHactEvent> HactEvents = new List<CSVDataY2.IHactEvent>();
        private List<CSVDataY2.IHactEvent> NHactEvents;
        private List<uint> UNKDATAPtrs = new List<uint>();
        private List<CSVDataY2.UNKDATA> UNKDATA = new List<CSVDataY2.UNKDATA>();
        private List<CSVDataY2.UNKDATA> NUNKDATA;
        long CharaPos;
        long ArmPos;
        long ObjPos;
        long HEPos;
        long DatPos;
        long StringPos;
        int CharaCNT = 0;
        int HECount = 0;
        int ObjectCount = 0;
        int ArmsCount = 0;
        int UnkCNT = 0;
        public void WriteCSV(CSVDataY2 Data)
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
                        writer.Write((uint)1);
                        writer.Write(new byte[12]);
                        writer.Write((uint)32);
                        writer.Write((uint)0);
                        writer.Write((uint)1);
                        WriteString(writer, "TCAY");
                        writer.Seek((int)writer.BaseStream.Position-1,SeekOrigin.Begin);
                        writer.Write((uint)16);
                        writer.Write((uint)Data.Category1.Count);
                        writer.Write((uint)5840);
                        writer.Write((uint)Data.Category2.Count);
                        long returnpos = writer.BaseStream.Position;
                        writer.Write(new byte[Data.Category1.Count * 64]);
                        writer.Write(new byte[Data.Category2.Count * 64]);
                        
                        SortMyShit(Data);
                        PadMyShit(writer);
                        writer.BaseStream.Seek(StringPos,SeekOrigin.Begin);
                        foreach (string Name in NStrings)
                        {
                            StringPtrs.Add((uint)writer.BaseStream.Position-32);
                            if (Name != null)
                            {
                                WriteString(writer, Name);
                            }
                            
                        }
                        writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
                        foreach(CSVDataY2.CSVEntry YAct in Data.Category1)
                        {
                            WriteString(writer, YAct.Name);
                            AlignData(writer, 16);
                            writer.Write(YAct.FileID);
                            writer.Write(YAct.unk1);
                            writer.Write(YAct.unk2);
                            writer.Write(YAct.unk3);
                            
                            if (YAct.Characters.Count > 0)
                            {
                                WriteChara(writer, YAct.Characters);
                            }
                            else
                            {
                                writer.Write(new byte[8]);
                            }
                            if (YAct.Objects.Count > 0)
                            {
                                WriteObject(writer, YAct.Objects);
                            }
                            else
                            {
                                writer.Write(new byte[8]);
                            }
                            if (YAct.Arms.Count > 0)
                            {
                                WriteArm(writer, YAct.Arms);
                            }
                            else
                            {
                                writer.Write(new byte[8]);
                            }
                            if (YAct.HactEvents.Count > 0)
                            {
                                WriteHactEvent(writer, YAct.HactEvents);
                            }
                            else
                            {
                                writer.Write(new byte[8]);
                            }
                        }
                        foreach (CSVDataY2.CSVEntry YAct in Data.Category2)
                        {
                            WriteString(writer, YAct.Name);
                            AlignData(writer, 16);
                            writer.Write(YAct.FileID);
                            writer.Write(YAct.unk1);
                            writer.Write(YAct.unk2);
                            writer.Write(YAct.unk3);
                            if (YAct.Characters.Count > 0)
                            {
                                WriteChara(writer, YAct.Characters);
                            }
                            else
                            {
                                writer.Write(new byte[8]);
                            }
                            if (YAct.Objects.Count > 0)
                            {
                                WriteObject(writer, YAct.Objects);
                            }
                            else
                            {
                                writer.Write(new byte[8]);
                            }
                            if (YAct.Arms.Count > 0)
                            {
                                WriteArm(writer, YAct.Arms);
                            }
                            else
                            {
                                writer.Write(new byte[8]);
                            }
                            if (YAct.HactEvents.Count > 0)
                            {
                                WriteHactEvent(writer, YAct.HactEvents);
                            }
                            else
                            {
                                writer.Write(new byte[8]);
                            }
                        }
                    }

                }
            }
        }
        private void WriteHactEvent(BinaryWriter writer,List<CSVDataY2.IHactEvent> HactEvent)
        {
            writer.Write(HEPtrs[HECount]);
            writer.Write((uint)HactEvent.Count);
            long returnpos = writer.BaseStream.Position;
            writer.Seek((int)HEPtrs[HECount] + 32, SeekOrigin.Begin);
            foreach (CSVDataY2.IHactEvent HE in HactEvent)
            {
                if (HE is CSVDataY2.EFFECT_DAMAGE DMG)
                {
                    writer.Write(StringPtrs[NStrings.IndexOf(DMG.Name)]);
                    writer.Write((uint)0);
                    writer.Write(DMG.DamageVal);
                    writer.Write(new byte[28]);
                }
                else if (HE is CSVDataY2.UnknownEffect UNK)
                {
                    writer.Write(StringPtrs[NStrings.IndexOf(UNK.Name)]);
                    writer.Write(UNK.UnkData);
                }
                HECount++;
            }
            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
        }
        private void WriteArm(BinaryWriter writer, List<CSVDataY2.Arm> Arms)
        {
            writer.Write(ArmsPtrs[ArmsCount]);
            writer.Write((uint)Arms.Count);
            long returnpos = writer.BaseStream.Position;
            writer.Seek((int)ArmsPtrs[ArmsCount] + 32, SeekOrigin.Begin);
            foreach(CSVDataY2.Arm Arm in Arms)
            {
                writer.Write(StringPtrs[NStrings.IndexOf(Arm.Name1)]);
                writer.Write(Arm.WeaponCount);
                writer.Write(StringPtrs[NStrings.IndexOf(Arm.Name2)]);
                if (Arm.WeaponPointer > 0)
                {
                    writer.Write(StringPtrs[NStrings.IndexOf(Arm.ArmName)]);
                }
                else
                {
                    writer.Write(0);
                }
                ArmsCount++;
            }   
            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
        }
        private void WriteObject(BinaryWriter writer, List<CSVDataY2.Object> Objects)
        {
            writer.Write(ObjectPtrs[ObjectCount]);
            writer.Write((uint)Objects.Count);
            long returnpos = writer.BaseStream.Position;
            writer.Seek((int)ObjectPtrs[ObjectCount] + 32, SeekOrigin.Begin);
            foreach (CSVDataY2.Object Obj in Objects)
            {
                writer.Write(StringPtrs[NStrings.IndexOf(Obj.Name1)]);
                writer.Write(Obj.Unknown1);
                writer.Write(StringPtrs[NStrings.IndexOf(Obj.Name2)]);
                if (Obj.Data.Count>0)
                {
                    WriteUNK(writer, Obj.Data);
                }
                else
                {
                    writer.Write(new byte[8]);
                }
                writer.Write(Obj.Unknown2);
                ObjectCount++;
            }
            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
        }
        private void WriteChara(BinaryWriter writer,List<CSVDataY2.Character> Characters)
        {
            
            writer.Write((int)CharaPtrs[CharaCNT]);
            writer.Write((uint)Characters.Count);
            long returnpos = writer.BaseStream.Position;
            writer.Seek((int)CharaPtrs[CharaCNT] + 32, SeekOrigin.Begin);
            foreach (CSVDataY2.Character Chara in Characters)
            {
                writer.Write(StringPtrs[NStrings.IndexOf(Chara.Name)]);
                writer.Write(Chara.Unknown1);
                writer.Write(Chara.DamageCondition);
                writer.Write(Chara.Unknown2);
                if (Chara.Data1.Count>0)
                {
                    WriteUNK(writer, Chara.Data1);
                }
                else
                {
                    writer.Write(new byte[8]);
                }
                writer.Write(Chara.Unknown3);
                if (Chara.Data2.Count > 0)
                {
                    WriteUNK(writer, Chara.Data2);
                }
                else
                {
                    writer.Write(new byte[8]);
                }
                writer.Write(Chara.Unknown4);
                CharaCNT++;
            }
            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
            
        }
        private void WriteUNK(BinaryWriter writer,List<CSVDataY2.UNKDATA> UNK)
        {
            writer.Write((int)UNKDATAPtrs[UnkCNT]);
            writer.Write(UNK.Count);
            long returnpos = writer.BaseStream.Position;
            writer.Seek((int)UNKDATAPtrs[UnkCNT]+32, SeekOrigin.Begin);
            foreach (CSVDataY2.UNKDATA Dat in UNK)
            {
                writer.Write(Dat.Data);
                UnkCNT++;
            }
            writer.BaseStream.Seek(returnpos, SeekOrigin.Begin);
        }
        private void PadMyShit(BinaryWriter writer)
        {
            CharaPos = writer.BaseStream.Position;
            foreach(CSVDataY2.Character Chara in Characters)
            {
                CharaPtrs.Add((uint)writer.BaseStream.Position - 32);
                writer.Write(new byte[56]);
            }
            ObjPos = writer.BaseStream.Position;
            foreach (CSVDataY2.Object Obj in Objects)
            {
                ObjectPtrs.Add((uint)writer.BaseStream.Position - 32);
                writer.Write(new byte[28]);
            }
            ArmPos = writer.BaseStream.Position;
            foreach (CSVDataY2.Arm Arm in Arms)
            {
                ArmsPtrs.Add((uint)writer.BaseStream.Position - 32);
                writer.Write(new byte[16]);
            }
            HEPos = writer.BaseStream.Position;
            foreach (CSVDataY2.IHactEvent HE in HactEvents)
            {
                HEPtrs.Add((uint)writer.BaseStream.Position - 32);
                writer.Write(new byte[40]);
            }
            DatPos = writer.BaseStream.Position;
            foreach (CSVDataY2.UNKDATA UNK in UNKDATA)
            {
                UNKDATAPtrs.Add((uint)writer.BaseStream.Position - 32);
                writer.Write(new byte[36]);
            }
            StringPos = writer.BaseStream.Position;
        }
        private void SortMyShit(CSVDataY2 Data)
        {
            foreach (CSVDataY2.CSVEntry YAct in Data.Category1)
            {
                foreach (CSVDataY2.Character Chara in YAct.Characters)
                {
                    Strings.Add(Chara.Name);
                    Characters.Add(Chara);
                    foreach (CSVDataY2.UNKDATA Dat1 in Chara.Data1)
                    {
                        UNKDATA.Add(Dat1);
                    }
                    foreach (CSVDataY2.UNKDATA Dat2 in Chara.Data2)
                    {

                        UNKDATA.Add(Dat2);
                    }
                }
                foreach (CSVDataY2.Object Obj in YAct.Objects)
                {
                    Strings.Add(Obj.Name1);
                    Strings.Add(Obj.Name2);

                    Objects.Add(Obj);
                    foreach (CSVDataY2.UNKDATA Dat3 in Obj.Data)
                    {

                        UNKDATA.Add(Dat3);
                    }
                }
                foreach (CSVDataY2.Arm Arm in YAct.Arms)
                {
                    Strings.Add(Arm.Name1);
                    Strings.Add(Arm.Name2);
                    Strings.Add(Arm.ArmName);
                    Arms.Add(Arm);
                }
                foreach (CSVDataY2.IHactEvent HE in YAct.HactEvents)
                {
                    Strings.Add(HE.Name);

                    HactEvents.Add(HE);
                }
            }
            foreach (CSVDataY2.CSVEntry YAct in Data.Category2)
            {
                foreach (CSVDataY2.Character Chara in YAct.Characters)
                {
                    Strings.Add(Chara.Name);
                    Characters.Add(Chara);
                    foreach (CSVDataY2.UNKDATA Dat1 in Chara.Data1)
                    {
                        UNKDATA.Add(Dat1);
                    }
                    foreach (CSVDataY2.UNKDATA Dat2 in Chara.Data2)
                    {

                        UNKDATA.Add(Dat2);
                    }
                }
                foreach (CSVDataY2.Object Obj in YAct.Objects)
                {
                    Strings.Add(Obj.Name1);
                    Strings.Add(Obj.Name2);

                    Objects.Add(Obj);
                    foreach (CSVDataY2.UNKDATA Dat3 in Obj.Data)
                    {
                        UNKDATA.Add(Dat3);
                    }
                }
                foreach (CSVDataY2.Arm Arm in YAct.Arms)
                {
                    Strings.Add(Arm.Name1);
                    Strings.Add(Arm.Name2);
                    Strings.Add(Arm.ArmName);
                    Arms.Add(Arm);
                }
                foreach (CSVDataY2.IHactEvent HE in YAct.HactEvents)
                {
                    Strings.Add(HE.Name);

                    HactEvents.Add(HE);
                }
            }
            MessageBox.Show(UNKDATA.Count.ToString());
            NStrings = Strings.Distinct().ToList();
            
        }
        private void AlignData(BinaryWriter writer, int Alignment)
        {
            long currentpos = writer.BaseStream.Position;
            int padding = (int)(Alignment - currentpos % Alignment) % Alignment;
            writer.Write(new byte[padding]);
        }
        private void WriteString(BinaryWriter writer, string name)
        {
            writer.Write(Encoding.ASCII.GetBytes(name));
            writer.Write((byte)0);
        }
    }

}
