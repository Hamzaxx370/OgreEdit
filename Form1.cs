using OgreEdit.Data;
using OgreEdit.Readers;
using OgreEdit.Writers;
using System;
using System.Drawing.Text;
using System.Security.Cryptography;
using static OgreEdit.Data.CSVData;
using static OgreEdit.Data.Effects;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace OgreEdit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            treeView1.AfterSelect += SelectHandler;
            comboBox1.SelectedIndex = 0;
        }

        public YactData Data = new YactData();
        public YactDataY2 DataY2 = new YactDataY2();
        public MovePropData PropData = new MovePropData();
        public CSVDataY2 CSVDataY2 = new CSVDataY2();
        public CSVData CSVData = new CSVData();
        public List<string> HitBoxNames = new List<string>()
        {
            "None",
            "Right Arm",
            "Left Arm",
            "Right Leg",
            "Left Leg"
        };
        public List<uint> HitBoxIDs = new List<uint>()
        {
            0,
            2048,
            16384,
            6,
            32
        };
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private TreeNode AddNodeHactEvent(CSVDataY2.IHactEvent HactEvent)
        {
            TreeNode Node = new TreeNode(HactEvent.Name)
            {
                Tag = HactEvent
            };
            return Node;
        }
        private TreeNode AddNodeYActCategory(string Name)
        {
            TreeNode Node = new TreeNode(Name)
            {
                Tag = "No"
            };
            treeView1.Nodes.Add(Node);
            return Node;
        }
        private TreeNode AddNodeCSVYAct(CSVDataY2.CSVEntry YAct)
        {
            TreeNode Node = new TreeNode(YAct.Name)
            {
                Tag = YAct
            };
            return Node;
        }
        private TreeNode AddNodeCSVYActY1(CSVData.YAct YAct)
        {
            TreeNode Node = new TreeNode(YAct.FileID.ToString())
            {
                Tag = YAct
            };
            return Node;
        }
        private TreeNode AddNodeCSVChara(CSVDataY2.Character Chara)
        {
            TreeNode Node = new TreeNode(Chara.Name)
            {
                Tag = Chara
            };
            return Node;
        }
        private TreeNode AddNodeCSVPlayerY1(CSVData.Player Chara)
        {
            TreeNode Node = new TreeNode("KIRYU")
            {
                Tag = Chara
            };
            return Node;
        }
        private TreeNode AddNodeCSVEnemyY1(CSVData.Enemy Enemy, int i)
        {
            TreeNode Node = new TreeNode("ENEMY" + i.ToString())
            {
                Tag = Enemy
            };
            return Node;
        }
        private TreeNode AddNodeCSVObjectY1(CSVData.Object Object, int i)
        {
            TreeNode Node = new TreeNode("OBJECT" + i.ToString())
            {
                Tag = Object
            };
            return Node;
        }
        private TreeNode AddNodeCSVArmY1(CSVData.Arm Object, int i)
        {
            TreeNode Node = new TreeNode("ARM" + i.ToString())
            {
                Tag = Object
            };
            return Node;
        }
        private TreeNode AddNodeCSVModelY1(CSVData.Unk4 Object, int i)
        {
            TreeNode Node = new TreeNode("MODEL" + i.ToString())
            {
                Tag = Object
            };
            return Node;
        }
        private void AddNodeProp(string Name, MovePropData.Property Tag)
        {
            TreeNode Node = new TreeNode(Name)
            {
                Tag = Tag
            };
            treeView1.Nodes.Add(Node);
        }
        private void AddNodeAnim(string Name, YactDataY2.CamCharaInfoEntry Tag, TreeNode NodeP)
        {
            TreeNode Node = new TreeNode(Name)
            {
                Tag = Tag
            };
            NodeP.Nodes.Add(Node);
        }

        private void AddNodeAnimY1(string Name, CSVData.Anim Tag, TreeNode NodeP)
        {
            TreeNode Node = new TreeNode(Name)
            {
                Tag = Tag
            };
            NodeP.Nodes.Add(Node);
        }

        public TreeNode AddNodeChara(string Name, YactDataY2.CharaInfo Info, TreeNode Parent)
        {
            TreeNode Node = new TreeNode(Name)
            {
                Tag = Info
            };
            Parent.Nodes.Add(Node);
            return Node;
        }

        public TreeNode AddNodeCam(string Name, YactDataY2.CameraInfo Info, TreeNode Parent)
        {
            TreeNode Node = new TreeNode(Name)
            {
                Tag = Info
            };
            Parent.Nodes.Add(Node);
            return Node;
        }
        public TreeNode AddNodeCamY1(string Name, CSVData.CameraInfo Info, TreeNode NodeP)
        {
            TreeNode Node = new TreeNode(Name)
            {
                Tag = Info
            };
            NodeP.Nodes.Add(Node);
            return Node;
        }
        private TreeNode AddNodeEffect(string Name, Effects.IEffect Effect)
        {
            TreeNode Node = new TreeNode(Name)
            {
                Tag = Effect
            };
            return Node;
        }

        private void EffectHandler()
        {
            TreeNode SelectedNode = treeView1.SelectedNode;
            if (SelectedNode.Tag is Effects.IEffect GeneralData)
            {

                if (SelectedNode.Tag is Effects.HactEvent HE)
                {
                    Label label = CreateLabelTitle("Event", 0, 0);

                    Label labelp = CreateLabel("Parent ID", 0, 1);
                    TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                    {
                        HE.ParentID = TryParse32(NewText);
                        SelectedNode.Tag = HE;
                    });

                    Label labelfs = CreateLabel("Frame Start", 0, 2);
                    TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                    {
                        HE.FrameStart = TryParseFP(NewText);
                        SelectedNode.Tag = HE;
                    });

                    Label labelfe = CreateLabel("Frame End", 0, 3);
                    TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                    {

                        HE.FrameEnd = TryParseFP(NewText);
                        SelectedNode.Tag = HE;

                    });

                    Label labels = CreateLabel("Speed", 0, 4);
                    TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                    {

                        HE.Speed = TryParseFP(NewText);
                        SelectedNode.Tag = HE;

                    });


                    CreateLabel("Name", 0, 5);
                    CreateText(HE.Name.ToString(), 1, 5, (newtext) =>
                    {

                        HE.Name = (newtext);
                        SelectedNode.Text = (newtext);
                        SelectedNode.Tag = HE;
                    });
                }
                else if (SelectedNode.Text == "Damage Effect")
                {
                    if (SelectedNode.Tag is Effects.Damage Dmg)
                    {
                        Label label = CreateLabelTitle("Damage", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {

                            Dmg.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = Dmg;

                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {

                            Dmg.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = Dmg;

                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {

                            Dmg.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = Dmg;

                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {

                            Dmg.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = Dmg;

                        });


                        Label Vald = CreateLabel("Damage Value", 0, 5);
                        TextBox Textd = CreateText(Dmg.DamageVal.ToString(), 1, 5, (NewText) =>
                        {

                            Dmg.DamageVal = TryParse32(NewText);
                            SelectedNode.Tag = Dmg;
                            GeneralData = Dmg;

                        });

                    }

                }
                else if (SelectedNode.Text == "Sound Cue Effect")
                {
                    if (SelectedNode.Tag is Effects.SoundCue Cue)
                    {
                        Label label = CreateLabelTitle("Sound", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {

                            Cue.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = Cue;

                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {

                            Cue.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = Cue;

                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {

                            Cue.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = Cue;

                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {

                            Cue.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = Cue;

                        });

                        Label labelb = CreateLabel("Bone Number", 0, 5);
                        TextBox Textb = CreateText(Cue.BoneNumber.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            Cue.BoneNumber = TryParse32(NewText);
                            SelectedNode.Tag = Cue;
                            GeneralData = Cue;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelc = CreateLabel("Container ID", 0, 6);
                        TextBox Textc = CreateText(Cue.ContainerID.ToString(), 1, 6, (NewText) =>
                        {
                            var OG = GeneralData;
                            Cue.ContainerID = TryParse16(NewText);
                            SelectedNode.Tag = Cue;
                            GeneralData = Cue;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelv = CreateLabel("Voice ID", 0, 7);
                        TextBox Textv = CreateText(Cue.VoiceID.ToString(), 1, 7, (NewText) =>
                        {
                            var OG = GeneralData;
                            Cue.VoiceID = TryParse16(NewText);
                            SelectedNode.Tag = Cue;
                            GeneralData = Cue;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }

                }
                else if (SelectedNode.Text == "Particle Effect")
                {
                    if (SelectedNode.Tag is Effects.ParticleNormal PTCL)
                    {
                        Label label = CreateLabelTitle("Particle", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelb = CreateLabel("Bone Number", 0, 5);
                        TextBox Textb = CreateText(PTCL.BoneNumber.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.BoneNumber = TryParse32(NewText);
                            SelectedNode.Tag = PTCL;
                            GeneralData = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelpt = CreateLabel("PTCL ID", 0, 6);
                        TextBox Textpt = CreateText(PTCL.ptclID.ToString(), 1, 6, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.ptclID = TryParse32(NewText);
                            SelectedNode.Tag = PTCL;
                            GeneralData = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelpr1 = CreateLabel("PTCL Parameter 1", 0, 7);
                        TextBox Textpr1 = CreateText(PTCL.PTCLParam1.ToString(), 1, 7, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.PTCLParam1 = TryParseFP(NewText);
                            SelectedNode.Tag = PTCL;
                            GeneralData = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelpr2 = CreateLabel("PTCL Parameter 2", 0, 8);
                        TextBox Textpr2 = CreateText(PTCL.PTCLParam2.ToString(), 1, 8, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.PTCLParam2 = TryParseFP(NewText);
                            SelectedNode.Tag = PTCL;
                            GeneralData = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelb1 = CreateLabel("Copy Location", 0, 9);
                        TextBox Textb1 = CreateText(PTCL.Flags[0].ToString(), 1, 9, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.Flags[0] = TryParse8(NewText);
                            SelectedNode.Tag = PTCL;
                            GeneralData = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelb2 = CreateLabel("Copy Rotation", 0, 10);
                        TextBox Textb2 = CreateText(PTCL.Flags[1].ToString(), 1, 10, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.Flags[1] = TryParse8(NewText);
                            SelectedNode.Tag = PTCL;
                            GeneralData = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelu = CreateLabel("Unknown", 0, 11);
                        TextBox Textu = CreateText(PTCL.Unknown.ToString(), 1, 11, (NewText) =>
                        {
                            var OG = GeneralData;
                            PTCL.Unknown = TryParse32(NewText);
                            SelectedNode.Tag = PTCL;
                            GeneralData = PTCL;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }
                }
                else if (SelectedNode.Text == "Trail Effect")
                {
                    if (SelectedNode.Tag is Effects.ParticleTrail Trail)
                    {
                        Label label = CreateLabelTitle("Trail", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelb = CreateLabel("Bone Number", 0, 5);
                        TextBox Textb = CreateText(Trail.BoneNumber.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.BoneNumber = TryParse32(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelt1 = CreateLabel("Trail Parameter 1", 0, 6);
                        TextBox Textt1 = CreateText(Trail.TrailParam1.ToString(), 1, 6, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.TrailParam1 = TryParseFP(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelt2 = CreateLabel("Trail Parameter 2", 0, 7);
                        TextBox Textt2 = CreateText(Trail.TrailParam2.ToString(), 1, 7, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.TrailParam2 = TryParseFP(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelt3 = CreateLabel("Trail Parameter 3", 0, 8);
                        TextBox Textt3 = CreateText(Trail.TrailParam3.ToString(), 1, 8, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.TrailParam3 = TryParseFP(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelb1 = CreateLabel("Copy Location", 0, 9);
                        TextBox Textb1 = CreateText(Trail.Flags[0].ToString(), 1, 9, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.Flags[0] = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelb2 = CreateLabel("Copy Rotation", 0, 10);
                        TextBox Textb2 = CreateText(Trail.Flags[1].ToString(), 1, 10, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.Flags[1] = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelrgb1 = CreateLabel("RGBA 1", 0, 11);
                        TextBox Textr1 = CreateText(Trail.RGBA1.Red.ToString(), 1, 11, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.RGBA1.Red = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Textg1 = CreateText(Trail.RGBA1.Green.ToString(), 2, 11, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.RGBA1.Green = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Textbu1 = CreateText(Trail.RGBA1.Blue.ToString(), 3, 11, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.RGBA1.Blue = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Texta1 = CreateText(Trail.RGBA1.Alpha.ToString(), 4, 11, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.RGBA1.Alpha = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelrgb2 = CreateLabel("RGBA 2", 0, 12);
                        TextBox Textr2 = CreateText(Trail.RGBA2.Red.ToString(), 1, 12, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.RGBA2.Red = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Textg2 = CreateText(Trail.RGBA2.Green.ToString(), 2, 12, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.RGBA2.Green = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Textbu2 = CreateText(Trail.RGBA2.Blue.ToString(), 3, 12, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.RGBA2.Blue = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Texta2 = CreateText(Trail.RGBA2.Alpha.ToString(), 4, 12, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.RGBA2.Alpha = TryParse8(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelu1 = CreateLabel("Unknown 1", 0, 13);
                        TextBox Textu1 = CreateText(Trail.Unknown1.ToString(), 1, 13, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.Unknown1 = TryParse32(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelu2 = CreateLabel("Unknown 2", 0, 14);
                        TextBox Textu2 = CreateText(Trail.Unknown2.ToString(), 1, 14, (NewText) =>
                        {
                            var OG = GeneralData;
                            Trail.Unknown2 = TryParse32(NewText);
                            SelectedNode.Tag = Trail;
                            GeneralData = Trail;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }
                }
                else if (SelectedNode.Text == "Loop Effect")
                {
                    var OG = GeneralData;
                    if (SelectedNode.Tag is Effects.Loop Loop)
                    {
                        Label label = CreateLabelTitle("Loop", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            Loop.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = Loop;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            Loop.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = Loop;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            Loop.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = Loop;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            Loop.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = Loop;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Vall = CreateLabel("Max Loop Count", 0, 5);
                        TextBox Textl = CreateText(Loop.MaxLoopNum.ToString(), 1, 5, (NewText) =>
                        {
                            Loop.MaxLoopNum = TryParse32(NewText);
                            SelectedNode.Tag = Loop;
                            GeneralData = Loop;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }

                }
                else if (SelectedNode.Text == "Normal Branch Effect")
                {
                    if (SelectedNode.Tag is Effects.NormalBranch LoopE)
                    {
                        Label label = CreateLabelTitle("Branch", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu = CreateLabel("ID", 0, 5);
                        TextBox Textu = CreateText(LoopE.ID.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ID = TryParseS32(NewText);
                            SelectedNode.Tag = LoopE;
                            GeneralData = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });


                    }

                }
                else if (SelectedNode.Text == "Counter Branch Effect")
                {
                    if (SelectedNode.Tag is Effects.CounterBranch LoopE)
                    {
                        Label label = CreateLabelTitle("Branch", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu1 = CreateLabel("Unknown 1", 0, 5);
                        TextBox Textu1 = CreateText(LoopE.Unknown1.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Unknown1 = TryParseS32(NewText);
                            SelectedNode.Tag = LoopE;
                            GeneralData = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu2 = CreateLabel("Unknown 2", 0, 6);
                        TextBox Textu2 = CreateText(LoopE.Unknown2.ToString(), 1, 6, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Unknown2 = TryParseS32(NewText);
                            SelectedNode.Tag = LoopE;
                            GeneralData = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }

                }
                else if (SelectedNode.Text == "Finish Effect")
                {
                    if (SelectedNode.Tag is Effects.Finish LoopE)
                    {
                        Label label = CreateLabelTitle("Finish", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                    }

                }
                else if (SelectedNode.Text == "Counter Up Effect")
                {
                    if (SelectedNode.Tag is Effects.CounterUp LoopE)
                    {
                        Label label = CreateLabelTitle("Counter Up", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu1 = CreateLabel("Unknown", 0, 5);
                        TextBox Textu1 = CreateText(LoopE.Unknown.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Unknown = TryParseS32(NewText);
                            SelectedNode.Tag = LoopE;
                            GeneralData = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }

                }
                else if (SelectedNode.Text == "Counter Reset Effect")
                {
                    if (SelectedNode.Tag is Effects.CounterReset LoopE)
                    {
                        Label label = CreateLabelTitle("Reset", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu1 = CreateLabel("Unknown", 0, 5);
                        TextBox Textu1 = CreateText(LoopE.Unknown.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Unknown = TryParseS32(NewText);
                            SelectedNode.Tag = LoopE;
                            GeneralData = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                    }

                }
                else if (SelectedNode.Text == "Dead Effect 1")
                {
                    if (SelectedNode.Tag is Effects.Dead1 LoopE)
                    {
                        Label label = CreateLabelTitle("Dead", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu1 = CreateLabel("ID", 0, 5);
                        TextBox Textu1 = CreateText(LoopE.ID.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ID = TryParseS32(NewText);
                            SelectedNode.Tag = LoopE;
                            GeneralData = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }

                }

                else if (SelectedNode.Text == "Dead Effect 2")
                {
                    if (SelectedNode.Tag is Effects.Dead2 LoopE)
                    {
                        Label label = CreateLabelTitle("Dead", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu1 = CreateLabel("ID", 0, 5);
                        TextBox Textu1 = CreateText(LoopE.ID.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            LoopE.ID = TryParseS32(NewText);
                            SelectedNode.Tag = LoopE;
                            GeneralData = LoopE;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }

                }

                else if (SelectedNode.Text == "Button Window Effect")
                {
                    if (SelectedNode.Tag is Effects.ButtonTiming ButtonW)
                    {
                        Label label = CreateLabelTitle("Button", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonW.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = ButtonW;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonW.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = ButtonW;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonW.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = ButtonW;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonW.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = ButtonW;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valb = CreateLabel("Button", 0, 5);
                        TextBox Textb = CreateText(ButtonW.Button.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonW.Button = TryParse32(NewText);
                            SelectedNode.Tag = ButtonW;
                            GeneralData = ButtonW;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        Label Vali = CreateLabel("ID", 0, 6);
                        TextBox Texti = CreateText(ButtonW.ID.ToString(), 1, 6, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonW.ID = TryParseS32(NewText);
                            SelectedNode.Tag = ButtonW;
                            GeneralData = ButtonW;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }

                }
                else if (SelectedNode.Text == "Button Spam Effect")
                {
                    if (SelectedNode.Tag is Effects.ButtonSpam ButtonS)
                    {
                        Label label = CreateLabelTitle("Button", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonS.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = ButtonS;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonS.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = ButtonS;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonS.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = ButtonS;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonS.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = ButtonS;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valb = CreateLabel("Button", 0, 5);
                        TextBox Textb = CreateText(ButtonS.Button.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonS.Button = TryParse16(NewText);
                            SelectedNode.Tag = ButtonS;
                            GeneralData = ButtonS;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        Label Valc = CreateLabel("Count", 0, 6);
                        TextBox Textc = CreateText(ButtonS.Count.ToString(), 1, 6, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonS.Count = TryParse16(NewText);
                            SelectedNode.Tag = ButtonS;
                            GeneralData = ButtonS;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        Label Vali = CreateLabel("ID", 0, 7);
                        TextBox Texti = CreateText(ButtonS.ID.ToString(), 1, 7, (NewText) =>
                        {
                            var OG = GeneralData;
                            ButtonS.ID = TryParseS32(NewText);
                            SelectedNode.Tag = ButtonS;
                            GeneralData = ButtonS;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                    }

                }
                else if (SelectedNode.Text == "Screen Flash Effect")
                {
                    if (SelectedNode.Tag is Effects.ScreenFlash ScrF)
                    {
                        Label label = CreateLabelTitle("Screen Flash", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu1 = CreateLabel("Unknown 1", 0, 5);
                        TextBox Textu1 = CreateText(ScrF.Unknown1.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.Unknown1 = TryParse32(NewText);
                            SelectedNode.Tag = ScrF;
                            GeneralData = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu2 = CreateLabel("Unknown 2", 0, 6);
                        TextBox Textu2 = CreateText(ScrF.Unknown2.ToString(), 1, 6, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.Unknown2 = TryParseFP(NewText);
                            SelectedNode.Tag = ScrF;
                            GeneralData = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelrgb1 = CreateLabel("RGBA", 0, 7);
                        TextBox Textr1 = CreateText(ScrF.RGBA.Red.ToString(), 1, 7, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.RGBA.Red = TryParse8(NewText);
                            SelectedNode.Tag = ScrF;
                            GeneralData = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Textg1 = CreateText(ScrF.RGBA.Green.ToString(), 2, 7, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.RGBA.Green = TryParse8(NewText);
                            SelectedNode.Tag = ScrF;
                            GeneralData = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Textbu1 = CreateText(ScrF.RGBA.Blue.ToString(), 3, 7, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.RGBA.Blue = TryParse8(NewText);
                            SelectedNode.Tag = ScrF;
                            GeneralData = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Texta1 = CreateText(ScrF.RGBA.Alpha.ToString(), 4, 7, (NewText) =>
                        {
                            var OG = GeneralData;
                            ScrF.RGBA.Alpha = TryParse8(NewText);
                            SelectedNode.Tag = ScrF;
                            GeneralData = ScrF;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });


                    }

                }
                else if (SelectedNode.Text == "After Image Effect")
                {
                    if (SelectedNode.Tag is Effects.AfterImage AImage)
                    {
                        Label label = CreateLabelTitle("Screen", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu1 = CreateLabel("Unknown 1", 0, 5);
                        TextBox Textu1 = CreateText(AImage.Unknown1.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.Unknown1 = TryParse32(NewText);
                            SelectedNode.Tag = AImage;
                            GeneralData = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valu2 = CreateLabel("Unknown 2", 0, 6);
                        TextBox Textu2 = CreateText(AImage.Unknown2.ToString(), 1, 6, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.Unknown2 = TryParseFP(NewText);
                            SelectedNode.Tag = AImage;
                            GeneralData = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valp1 = CreateLabel("Parameter 1", 0, 7);
                        TextBox Textp1 = CreateText(AImage.Param1.ToString(), 1, 7, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.Param1 = TryParseFP(NewText);
                            SelectedNode.Tag = AImage;
                            GeneralData = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valp2 = CreateLabel("Parameter 2", 0, 8);
                        TextBox Textp2 = CreateText(AImage.Param2.ToString(), 1, 8, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.Param2 = TryParseFP(NewText);
                            SelectedNode.Tag = AImage;
                            GeneralData = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valsc = CreateLabel("Scale", 0, 9);
                        TextBox Textsc = CreateText(AImage.Scale.ToString(), 1, 9, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.Scale = TryParseFP(NewText);
                            SelectedNode.Tag = AImage;
                            GeneralData = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelrgb1 = CreateLabel("RGBA", 0, 10);
                        TextBox Textr1 = CreateText(AImage.RGBA.Red.ToString(), 1, 10, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.RGBA.Red = TryParse8(NewText);
                            SelectedNode.Tag = AImage;
                            GeneralData = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Textg1 = CreateText(AImage.RGBA.Green.ToString(), 2, 10, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.RGBA.Green = TryParse8(NewText);
                            SelectedNode.Tag = AImage;
                            GeneralData = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Textbu1 = CreateText(AImage.RGBA.Blue.ToString(), 3, 10, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.RGBA.Blue = TryParse8(NewText);
                            SelectedNode.Tag = AImage;
                            GeneralData = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });
                        TextBox Texta1 = CreateText(AImage.RGBA.Alpha.ToString(), 4, 10, (NewText) =>
                        {
                            var OG = GeneralData;
                            AImage.RGBA.Alpha = TryParse8(NewText);
                            SelectedNode.Tag = AImage;
                            GeneralData = AImage;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });


                    }

                }
                else if (SelectedNode.Text == "Vibration Effect")
                {
                    if (SelectedNode.Tag is Effects.CtrlVibration Vib)
                    {
                        Label label = CreateLabelTitle("Vibration", 0, 0);

                        Label labelp = CreateLabel("Parent ID", 0, 1);
                        TextBox TextP = CreateText(GeneralData.ParentID.ToString(), 1, 1, (NewText) =>
                        {
                            var OG = GeneralData;
                            Vib.ParentID = TryParse32(NewText);
                            SelectedNode.Tag = Vib;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfs = CreateLabel("Frame Start", 0, 2);
                        TextBox Textfs = CreateText(GeneralData.FrameStart.ToString(), 1, 2, (NewText) =>
                        {
                            var OG = GeneralData;
                            Vib.FrameStart = TryParseFP(NewText);
                            SelectedNode.Tag = Vib;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labelfe = CreateLabel("Frame End", 0, 3);
                        TextBox Textfe = CreateText(GeneralData.FrameEnd.ToString(), 1, 3, (NewText) =>
                        {
                            var OG = GeneralData;
                            Vib.FrameEnd = TryParseFP(NewText);
                            SelectedNode.Tag = Vib;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label labels = CreateLabel("Speed", 0, 4);
                        TextBox Texts = CreateText(GeneralData.Speed.ToString(), 1, 4, (NewText) =>
                        {
                            var OG = GeneralData;
                            Vib.Speed = TryParseFP(NewText);
                            SelectedNode.Tag = Vib;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valv1 = CreateLabel("Rotor 1", 0, 5);
                        TextBox Textv1 = CreateText(Vib.Vibration1.ToString(), 1, 5, (NewText) =>
                        {
                            var OG = GeneralData;
                            Vib.Vibration1 = TryParse32(NewText);
                            SelectedNode.Tag = Vib;
                            GeneralData = Vib;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });

                        Label Valv2 = CreateLabel("Rotor 2", 0, 6);
                        TextBox Textv2 = CreateText(Vib.Vibration2.ToString(), 1, 6, (NewText) =>
                        {
                            var OG = GeneralData;
                            Vib.Vibration2 = TryParse32(NewText);
                            SelectedNode.Tag = Vib;
                            GeneralData = Vib;
                            ChangeEffect(SelectedNode, GeneralData, OG);
                        });


                    }

                }
                else if (SelectedNode.Text == "Screen Shake Effect")
                {
                    if (SelectedNode.Tag is Effects.ScreenShakeProp Shake)
                    {
                        Label label = CreateLabelTitle("Screen", 0, 0);


                        Label Valv1 = CreateLabel("Intensity", 0, 5);
                        TextBox Textv1 = CreateText(Shake.Intensity.ToString(), 1, 5);

                        Label Valv2 = CreateLabel("Flag", 0, 6);
                        TextBox Textv2 = CreateText(Shake.Flag.ToString(), 1, 6);


                    }
                }
            }

        }
        private void ChangeEffect(TreeNode SelectedNode, Effects.IEffect GeneralData, Effects.IEffect OG)
        {

        }
        private void SelectHandler(object sender, TreeViewEventArgs e)
        {
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ResumeLayout();
            TreeNode SelectedNode = e.Node;
            if (SelectedNode.Text == "Character Animation")
            {
                if (SelectedNode.Tag is byte[] OMT)
                {
                    int Index = Data.OMTs.IndexOf(OMT);
                    Label title = CreateLabelTitle("Character", 0, 0);
                    Button button1 = CreateButton("Import", 1, 1);
                    Button button2 = CreateButton("Export", 1, 2);
                    button1.Click += (s, e) =>
                    {
                        byte[] OMT = ChangeFile("OMT");
                        Data.OMTs[Index] = OMT;
                    };
                    button2.Click += (s, e) =>
                    {
                        ExportFile("OMT", Data.OMTs[Index]);
                    };
                }

            }
            else if (SelectedNode.Text == "Camera Animation")
            {
                if (SelectedNode.Tag is byte[] MTBW)
                {
                    int Index = Data.MTBWs.IndexOf(MTBW);
                    Label title = CreateLabelTitle("Camera", 0, 0);
                    Button button1 = CreateButton("Import", 1, 1);
                    Button button2 = CreateButton("Export", 1, 2);
                    button1.Click += (s, e) =>
                    {
                        byte[] MTBW = ChangeFile("MTBW");
                        Data.MTBWs[Index] = MTBW;
                    };
                    button2.Click += (s, e) =>
                    {
                        ExportFile("MTBW", Data.MTBWs[Index]);
                    };
                }

            }
            else if (SelectedNode.Tag is MovePropData.Property Prop)
            {
                tableLayoutPanel1.SuspendLayout();
                int Index = PropData.PropData.IndexOf(Prop);
                CreateLabelTitle("Property", 0, 0);
                CreateLabel("Type", 0, 1);
                CreateText(Prop.Type.ToString(), 1, 1, (newtext) =>
                {
                    Prop.Type = TryParse32(newtext);
                    SelectedNode.Tag = Prop;
                    PropData.PropData[Index] = Prop;
                });
                CreateLabel("Frame Start", 0, 2);
                CreateText(Prop.FrameStart.ToString(), 1, 2, (newtext) =>
                {
                    Prop.FrameStart = TryParseFP(newtext);
                    SelectedNode.Tag = Prop;
                    PropData.PropData[Index] = Prop;
                });
                CreateLabel("Frame End", 0, 3);
                CreateText(Prop.FrameEnd.ToString(), 1, 3, (newtext) =>
                {
                    Prop.FrameEnd = TryParseFP(newtext);
                    SelectedNode.Tag = Prop;
                    PropData.PropData[Index] = Prop;
                });
                CreateLabel("Hit Box", 0, 4);
                int TextIndex = HitBoxIDs.IndexOf(Prop.HitBox);
                ComboBox Hitbx = new ComboBox();
                Hitbx.Items.AddRange(HitBoxNames.ToArray());
                Hitbx.SelectedIndex = TextIndex;
                Hitbx.SelectedIndexChanged += (s, e) =>
                {
                    Prop.HitBox = HitBoxIDs[Hitbx.SelectedIndex];
                    SelectedNode.Tag = Prop;
                    PropData.PropData[Index] = Prop;
                };
                tableLayoutPanel1.Controls.Add(Hitbx, 1, 4);
                tableLayoutPanel1.ResumeLayout();
            }
            else if (SelectedNode.Tag is CSVDataY2.EFFECT_DAMAGE DMG)
            {
                tableLayoutPanel1.SuspendLayout();
                int Flag = -1;
                if (SelectedNode.Parent.Parent.Text is "Category 1")
                {
                    Flag = 1;
                }
                else
                {
                    Flag = 2;
                }
                int IndexP = -1;
                int Index = -1;
                if (Flag == 1)
                {
                    if (SelectedNode.Parent.Tag is CSVDataY2.CSVEntry YActP)
                    {
                        IndexP = CSVDataY2.Category1.IndexOf(YActP);
                        Index = CSVDataY2.Category1[IndexP].HactEvents.IndexOf(DMG);
                    }
                }
                else
                {
                    if (SelectedNode.Parent.Tag is CSVDataY2.CSVEntry YActP)
                    {
                        IndexP = CSVDataY2.Category2.IndexOf(YActP);
                        Index = CSVDataY2.Category2[IndexP].HactEvents.IndexOf(DMG);
                    }
                }
                CreateLabelTitle("Damage", 0, 0);
                CreateLabel("Name", 0, 1);
                CreateText(DMG.Name.ToString(), 1, 1, (newtext) =>
                {
                    var old = DMG;
                    DMG.Name = (newtext);
                    SelectedNode.Text = (newtext);
                    SelectedNode.Tag = DMG;
                    if (Flag == 1)
                    {
                        CSVDataY2.Category1[IndexP].HactEvents[Index] = DMG;
                    }
                    else
                    {
                        CSVDataY2.Category2[IndexP].HactEvents[Index] = DMG;
                    }
                });

                CreateLabel("Damage Value", 0, 2);
                CreateText(DMG.DamageVal.ToString(), 1, 2, (newtext) =>
                {
                    DMG.DamageVal = TryParse32(newtext);
                    SelectedNode.Tag = DMG;
                    if (Flag == 1)
                    {
                        CSVDataY2.Category1[IndexP].HactEvents[Index] = DMG;
                    }
                    else
                    {
                        CSVDataY2.Category2[IndexP].HactEvents[Index] = DMG;
                    }
                });
                tableLayoutPanel1.ResumeLayout();
            }
            else if (SelectedNode.Tag is CSVDataY2.CSVEntry YAct)
            {
                tableLayoutPanel1.SuspendLayout();
                int Flag = -1;
                if (SelectedNode.Parent.Text is "Category 1")
                {
                    Flag = 1;
                }
                else
                {
                    Flag = 2;
                }
                CreateLabelTitle("YAct", 0, 0);
                CreateLabel("File ID", 0, 1);
                CreateText(YAct.FileID.ToString(), 1, 1, (newtext) =>
                {
                    var old = YAct;
                    YAct.FileID = TryParse32(newtext);
                    SelectedNode.Tag = YAct;
                    if (Flag == 1)
                    {
                        CSVDataY2.Category1[CSVDataY2.Category1.IndexOf(old)] = YAct;
                    }
                    else
                    {
                        CSVDataY2.Category2[CSVDataY2.Category2.IndexOf(old)] = YAct;
                    }
                });
                tableLayoutPanel1.ResumeLayout();
            }
            else if (SelectedNode.Tag is CSVDataY2.Character Chara)
            {
                tableLayoutPanel1.SuspendLayout();
                int Flag = -1;
                int IndexP = -1;
                int Index = -1;
                if (SelectedNode.Parent.Parent.Text is "Category 1")
                {
                    Flag = 1;
                    if (SelectedNode.Parent.Tag is CSVDataY2.CSVEntry YActP)
                    {
                        IndexP = CSVDataY2.Category1.IndexOf(YActP);
                        Index = CSVDataY2.Category1[IndexP].Characters.IndexOf(Chara);
                    }

                }
                else
                {
                    Flag = 2;
                    if (SelectedNode.Parent.Tag is CSVDataY2.CSVEntry YActP)
                    {
                        IndexP = CSVDataY2.Category2.IndexOf(YActP);
                        Index = CSVDataY2.Category2[IndexP].Characters.IndexOf(Chara);
                    }
                }
                CreateLabelTitle("Character", 0, 0);
                CreateLabel("Name", 0, 1);
                CreateText(Chara.Name.ToString(), 1, 1, (newtext) =>
                {
                    var old = Chara;
                    Chara.Name = (newtext);
                    SelectedNode.Text = (newtext);
                    SelectedNode.Tag = Chara;
                    if (Flag == 1)
                    {
                        CSVDataY2.Category1[IndexP].Characters[Index] = Chara;
                    }
                    else
                    {
                        CSVDataY2.Category2[IndexP].Characters[Index] = Chara;
                    }
                });
                tableLayoutPanel1.ResumeLayout();
            }
            else if (SelectedNode.Tag is YactDataY2.CamCharaInfoEntry Info)
            {
                tableLayoutPanel1.SuspendLayout();
                TreeNode Parent = SelectedNode.Parent;
                int flag = -1;
                int ParentIndex = 0;
                int Index = 0;
                if (Parent.Tag is YactDataY2.CameraInfo CAInfo)
                {
                    ParentIndex = DataY2.CamInfos.IndexOf(CAInfo);
                    Index = DataY2.CamInfos[ParentIndex].Info.IndexOf(Info);
                    flag = 0;
                }
                else if (Parent.Tag is YactDataY2.CharaInfo CHInfo)
                {
                    ParentIndex = DataY2.CharaInfos.IndexOf(CHInfo);
                    Index = DataY2.CharaInfos[ParentIndex].Info.IndexOf(Info);
                    flag = 1;
                }
                Label label = CreateLabelTitle("Animation", 0, 0);

                Label labelfs = CreateLabel("Frame Start", 0, 1);
                Label labelfe = CreateLabel("Frame End", 0, 2);
                Label labels = CreateLabel("Speed", 0, 3);
                TextBox textfs = CreateText(Info.FrameStart.ToString(), 1, 1, (NewText) =>
                {
                    Info.FrameStart = TryParseFP(NewText);
                    SelectedNode.Tag = Info;
                    if (flag == 0)
                    {
                        DataY2.CamInfos[ParentIndex].Info[Index] = Info;
                    }
                    else
                    {
                        DataY2.CharaInfos[ParentIndex].Info[Index] = Info;
                    }
                });
                TextBox textfe = CreateText(Info.FrameEnd.ToString(), 1, 2, (NewText) =>
                {
                    Info.FrameEnd = TryParseFP(NewText);
                    SelectedNode.Tag = Info;
                    if (flag == 0)
                    {
                        DataY2.CamInfos[ParentIndex].Info[Index] = Info;
                    }
                    else
                    {
                        DataY2.CharaInfos[ParentIndex].Info[Index] = Info;
                    }
                });
                TextBox texts = CreateText(Info.Speed.ToString(), 1, 3, (NewText) =>
                {
                    Info.Speed = TryParseFP(NewText);
                    SelectedNode.Tag = Info;
                    if (flag == 0)
                    {
                        DataY2.CamInfos[ParentIndex].Info[Index] = Info;
                    }
                    else
                    {
                        DataY2.CharaInfos[ParentIndex].Info[Index] = Info;
                    }
                });
                Button buttonE = CreateButton("Export", 1, 4);
                Button buttonI = CreateButton("Import", 1, 5);
                buttonE.Click += (s, e) =>
                {
                    if (Parent.Tag is YactDataY2.CameraInfo CAInfo)
                    {
                        ExportFile("MTBW", Data.MTBWs[(int)Info.AnimID]);
                    }
                    else if (Parent.Tag is YactDataY2.CharaInfo CHInfo)
                    {
                        ExportFile("OMT", Data.OMTs[(int)Info.AnimID]);
                    }
                };
                buttonI.Click += (s, e) =>
                {
                    if (Parent.Tag is YactDataY2.CameraInfo CAInfo)
                    {
                        byte[] MTBW = ChangeFile("MTBW");
                        Data.MTBWs[(int)Info.AnimID] = MTBW;
                    }
                    else if (Parent.Tag is YactDataY2.CharaInfo CHInfo)
                    {
                        byte[] OMT = ChangeFile("OMT");
                        Data.OMTs[(int)Info.AnimID] = OMT;
                    }
                };
                tableLayoutPanel1.ResumeLayout();
            }
            else if (SelectedNode.Tag is Effects.IEffect GeneralData)
            {
                tableLayoutPanel1.SuspendLayout();
                EffectHandler();
                tableLayoutPanel1.ResumeLayout();
            }
            else if (SelectedNode.Tag is CSVData.YAct YActY1)
            {
                tableLayoutPanel1.SuspendLayout();
                Label Header = CreateLabelTitle("YAct", 0, 0);
                Label labelf = CreateLabel("File ID", 0, 1);
                TextBox Textf = CreateText(YActY1.FileID.ToString(), 1, 1, (newtext) =>
                {
                    TreeNode Parent = SelectedNode.Parent;
                    if (Parent.Text == "Category 1")
                    {
                        int Index = CSVData.Category1.IndexOf(YActY1);
                        YActY1.FileID = TryParse32(newtext);
                        CSVData.Category1[Index] = YActY1;
                        SelectedNode.Tag = YActY1;
                    }
                    else if (Parent.Text == "Category 2")
                    {
                        int Index = CSVData.Category2.IndexOf(YActY1);
                        YActY1.FileID = TryParse32(newtext);
                        CSVData.Category2[Index] = YActY1;
                        SelectedNode.Tag = YActY1;
                    }
                });
                tableLayoutPanel1.ResumeLayout();
            }
            else if (SelectedNode.Tag is CSVData.Player Player)
            {
                Label Header = CreateLabelTitle("Player", 0, 0);
                Label labelc = CreateLabel("Condition", 0, 1);
                TextBox Textc = CreateText(Player.Condition.ToString(), 1, 1, (NewText) =>
                    {
                        if (SelectedNode.Parent.Parent.Text == "Category 1")
                        {
                            if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                            {
                                int Index = CSVData.Category1.IndexOf(YAct);
                                Player.Condition = TryParse32(NewText);
                                YAct.Player = Player;
                                SelectedNode.Tag = Player;
                                SelectedNode.Parent.Tag = YAct;
                                CSVData.Category1[Index] = YAct;
                            }
                        }
                        else
                        {
                            if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                            {
                                int Index = CSVData.Category2.IndexOf(YAct);
                                Player.Condition = TryParse32(NewText);
                                YAct.Player = Player;
                                SelectedNode.Tag = Player;
                                SelectedNode.Parent.Tag = YAct;
                                CSVData.Category2[Index] = YAct;
                            }
                        }
                    });
                Label labelch = CreateLabel("Health Condition", 0, 2);
                TextBox Textch = CreateText(Player.HealthCondition.ToString(), 1, 2, (NewText) =>
                {
                    if (SelectedNode.Parent.Parent.Text == "Category 1")
                    {
                        if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                        {
                            int Index = CSVData.Category1.IndexOf(YAct);
                            Player.HealthCondition = TryParse32(NewText);
                            YAct.Player = Player;
                            SelectedNode.Tag = Player;
                            SelectedNode.Parent.Tag = YAct;
                            CSVData.Category1[Index] = YAct;
                        }
                    }
                    else
                    {
                        if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                        {
                            int Index = CSVData.Category2.IndexOf(YAct);
                            Player.HealthCondition = TryParse32(NewText);
                            YAct.Player = Player;
                            SelectedNode.Tag = Player;
                            SelectedNode.Parent.Tag = YAct;
                            CSVData.Category2[Index] = YAct;
                        }
                    }
                });
            }
            else if (SelectedNode.Tag is CSVData.Enemy Enemy)
            {
                Label Header = CreateLabelTitle("Enemy", 0, 0);
                Label labelc1 = CreateLabel("Condition 1", 0, 1);
                TextBox Textc1 = CreateText(Enemy.Condition1.ToString(), 1, 1, (NewText) =>
                {
                    if (SelectedNode.Parent.Parent.Text == "Category 1")
                    {
                        if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                        {
                            int Index = CSVData.Category1.IndexOf(YAct);
                            int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                            Enemy.Condition1 = TryParse32(NewText);
                            YAct.Enemies[EnemyIndex] = Enemy;
                            SelectedNode.Tag = Enemy;
                            SelectedNode.Parent.Tag = YAct;
                            CSVData.Category1[Index] = YAct;
                        }
                    }
                    else
                    {
                        if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                        {
                            int Index = CSVData.Category2.IndexOf(YAct);
                            int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                            Enemy.Condition1 = TryParse32(NewText);
                            YAct.Enemies[EnemyIndex] = Enemy;
                            SelectedNode.Tag = Enemy;
                            SelectedNode.Parent.Tag = YAct;
                            CSVData.Category2[Index] = YAct;
                        }
                    }
                });
                Label labelc = CreateLabel("Condition 2", 0, 2);
                TextBox Textc = CreateText(Enemy.Condition2.ToString(), 1, 2, (NewText) =>
                {
                    if (SelectedNode.Parent.Parent.Text == "Category 1")
                    {
                        if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                        {
                            int Index = CSVData.Category1.IndexOf(YAct);
                            int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                            Enemy.Condition2 = TryParse32(NewText);
                            YAct.Enemies[EnemyIndex] = Enemy;
                            SelectedNode.Tag = Enemy;
                            SelectedNode.Parent.Tag = YAct;
                            CSVData.Category1[Index] = YAct;
                        }
                    }
                    else
                    {
                        if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                        {
                            int Index = CSVData.Category2.IndexOf(YAct);
                            int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                            Enemy.Condition2 = TryParse32(NewText);
                            YAct.Enemies[EnemyIndex] = Enemy;
                            SelectedNode.Tag = Enemy;
                            SelectedNode.Parent.Tag = YAct;
                            CSVData.Category2[Index] = YAct;
                        }
                    }
                });
                Label labelch = CreateLabel("Health Condition", 0, 3);
                TextBox Textch = CreateText(Enemy.HealthCondition.ToString(), 1, 3, (NewText) =>
                {
                    if (SelectedNode.Parent.Parent.Text == "Category 1")
                    {
                        if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                        {
                            int Index = CSVData.Category1.IndexOf(YAct);
                            int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                            Enemy.HealthCondition = TryParse32(NewText);
                            YAct.Enemies[EnemyIndex] = Enemy;
                            SelectedNode.Tag = Enemy;
                            SelectedNode.Parent.Tag = YAct;
                            CSVData.Category1[Index] = YAct;
                        }
                    }
                    else
                    {
                        if (SelectedNode.Parent.Tag is CSVData.YAct YAct)
                        {
                            int Index = CSVData.Category2.IndexOf(YAct);
                            int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                            Enemy.HealthCondition = TryParse32(NewText);
                            YAct.Enemies[EnemyIndex] = Enemy;
                            SelectedNode.Tag = Enemy;
                            SelectedNode.Parent.Tag = YAct;
                            CSVData.Category2[Index] = YAct;
                        }
                    }
                });
            }
            else if (SelectedNode.Tag is CSVData.Anim Anim)
            {
                tableLayoutPanel1.SuspendLayout();
                Label Header = CreateLabelTitle("Animation", 0, 0);
                Label labelfs = CreateLabel("Frame Start", 0, 1);
                TextBox Textfs = CreateText(Anim.FrameStart.ToString(), 1, 1, (NewText) =>
                {
                    if (SelectedNode.Parent.Parent.Parent.Text == "Category 1")
                    {
                        if (SelectedNode.Parent.Parent.Tag is CSVData.YAct YAct)
                        {
                            if (SelectedNode.Parent.Tag is CSVData.Player Player)
                            {
                                int AnimIndex = Player.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Player.Info[AnimIndex] = Anim;
                                YAct.Player = Player;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Player;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Enemy Enemy)
                            {
                                int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                                int AnimIndex = Enemy.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Enemy.Info[AnimIndex] = Anim;
                                YAct.Enemies[EnemyIndex] = Enemy;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Enemy;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.CameraInfo Camera)
                            {
                                int CamIndex = YAct.CamInfos.IndexOf(Camera);
                                int AnimIndex = Camera.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Camera.Info[AnimIndex] = Anim;
                                YAct.CamInfos[CamIndex] = Camera;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Camera;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Object Object)
                            {
                                int CamIndex = YAct.Objects.IndexOf(Object);
                                int AnimIndex = Object.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Object.Info[AnimIndex] = Anim;
                                YAct.Objects[CamIndex] = Object;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Object;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Arm Arm)
                            {
                                int CamIndex = YAct.Arms.IndexOf(Arm);
                                int AnimIndex = Arm.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Arm.Info[AnimIndex] = Anim;
                                YAct.Arms[CamIndex] = Arm;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Arm;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Unk4 Model)
                            {
                                int CamIndex = YAct.UnknownC4.IndexOf(Model);
                                int AnimIndex = Model.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Model.Info[AnimIndex] = Anim;
                                YAct.UnknownC4[CamIndex] = Model;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Model;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                        }
                    }
                    else
                    {
                        if (SelectedNode.Parent.Parent.Tag is CSVData.YAct YAct)
                        {
                            if (SelectedNode.Parent.Tag is CSVData.Player Player)
                            {
                                int AnimIndex = Player.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Player.Info[AnimIndex] = Anim;
                                YAct.Player = Player;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Player;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Enemy Enemy)
                            {
                                int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                                int AnimIndex = Enemy.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Enemy.Info[AnimIndex] = Anim;
                                YAct.Enemies[EnemyIndex] = Enemy;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Enemy;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.CameraInfo Camera)
                            {
                                int CamIndex = YAct.CamInfos.IndexOf(Camera);
                                int AnimIndex = Camera.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Camera.Info[AnimIndex] = Anim;
                                YAct.CamInfos[CamIndex] = Camera;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Camera;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Object Object)
                            {
                                int CamIndex = YAct.Objects.IndexOf(Object);
                                int AnimIndex = Object.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Object.Info[AnimIndex] = Anim;
                                YAct.Objects[CamIndex] = Object;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Object;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Arm Arm)
                            {
                                int CamIndex = YAct.Arms.IndexOf(Arm);
                                int AnimIndex = Arm.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Arm.Info[AnimIndex] = Anim;
                                YAct.Arms[CamIndex] = Arm;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Arm;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Unk4 Model)
                            {
                                int CamIndex = YAct.UnknownC4.IndexOf(Model);
                                int AnimIndex = Model.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Model.Info[AnimIndex] = Anim;
                                YAct.UnknownC4[CamIndex] = Model;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Model;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                        }
                    }

                });
                Label labelfe = CreateLabel("Frame End", 0, 2);
                TextBox Textfe = CreateText(Anim.FrameEnd.ToString(), 1, 2, (NewText) =>
                {
                    if (SelectedNode.Parent.Parent.Parent.Text == "Category 1")
                    {
                        if (SelectedNode.Parent.Parent.Tag is CSVData.YAct YAct)
                        {
                            if (SelectedNode.Parent.Tag is CSVData.Player Player)
                            {
                                int AnimIndex = Player.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Player.Info[AnimIndex] = Anim;
                                YAct.Player = Player;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Player;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Enemy Enemy)
                            {
                                int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                                int AnimIndex = Enemy.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Enemy.Info[AnimIndex] = Anim;
                                YAct.Enemies[EnemyIndex] = Enemy;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Enemy;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.CameraInfo Camera)
                            {
                                int CamIndex = YAct.CamInfos.IndexOf(Camera);
                                int AnimIndex = Camera.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Camera.Info[AnimIndex] = Anim;
                                YAct.CamInfos[CamIndex] = Camera;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Camera;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Object Object)
                            {
                                int CamIndex = YAct.Objects.IndexOf(Object);
                                int AnimIndex = Object.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Object.Info[AnimIndex] = Anim;
                                YAct.Objects[CamIndex] = Object;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Object;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Arm Arm)
                            {
                                int CamIndex = YAct.Arms.IndexOf(Arm);
                                int AnimIndex = Arm.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Arm.Info[AnimIndex] = Anim;
                                YAct.Arms[CamIndex] = Arm;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Arm;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Unk4 Model)
                            {
                                int CamIndex = YAct.UnknownC4.IndexOf(Model);
                                int AnimIndex = Model.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Model.Info[AnimIndex] = Anim;
                                YAct.UnknownC4[CamIndex] = Model;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Model;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                        }
                    }
                    else
                    {
                        if (SelectedNode.Parent.Parent.Tag is CSVData.YAct YAct)
                        {
                            if (SelectedNode.Parent.Tag is CSVData.Player Player)
                            {
                                int AnimIndex = Player.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Player.Info[AnimIndex] = Anim;
                                YAct.Player = Player;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Player;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Enemy Enemy)
                            {
                                int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                                int AnimIndex = Enemy.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Enemy.Info[AnimIndex] = Anim;
                                YAct.Enemies[EnemyIndex] = Enemy;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Enemy;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.CameraInfo Camera)
                            {
                                int CamIndex = YAct.CamInfos.IndexOf(Camera);
                                int AnimIndex = Camera.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Camera.Info[AnimIndex] = Anim;
                                YAct.CamInfos[CamIndex] = Camera;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Camera;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Object Object)
                            {
                                int CamIndex = YAct.Objects.IndexOf(Object);
                                int AnimIndex = Object.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Object.Info[AnimIndex] = Anim;
                                YAct.Objects[CamIndex] = Object;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Object;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Arm Arm)
                            {
                                int CamIndex = YAct.Arms.IndexOf(Arm);
                                int AnimIndex = Arm.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Arm.Info[AnimIndex] = Anim;
                                YAct.Arms[CamIndex] = Arm;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Arm;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Unk4 Model)
                            {
                                int CamIndex = YAct.UnknownC4.IndexOf(Model);
                                int AnimIndex = Model.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameEnd = TryParseFP(NewText);
                                Model.Info[AnimIndex] = Anim;
                                YAct.UnknownC4[CamIndex] = Model;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Model;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                        }
                    }

                });
                Label labels = CreateLabel("Speed", 0, 3);
                TextBox Texts = CreateText(Anim.Speed.ToString(), 1, 3, (NewText) =>
                {
                    if (SelectedNode.Parent.Parent.Parent.Text == "Category 1")
                    {
                        if (SelectedNode.Parent.Parent.Tag is CSVData.YAct YAct)
                        {
                            if (SelectedNode.Parent.Tag is CSVData.Player Player)
                            {
                                int AnimIndex = Player.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Player.Info[AnimIndex] = Anim;
                                YAct.Player = Player;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Player;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Enemy Enemy)
                            {
                                int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                                int AnimIndex = Enemy.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Enemy.Info[AnimIndex] = Anim;
                                YAct.Enemies[EnemyIndex] = Enemy;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Enemy;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.CameraInfo Camera)
                            {
                                int CamIndex = YAct.CamInfos.IndexOf(Camera);
                                int AnimIndex = Camera.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Camera.Info[AnimIndex] = Anim;
                                YAct.CamInfos[CamIndex] = Camera;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Camera;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Object Object)
                            {
                                int CamIndex = YAct.Objects.IndexOf(Object);
                                int AnimIndex = Object.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Object.Info[AnimIndex] = Anim;
                                YAct.Objects[CamIndex] = Object;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Object;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Arm Arm)
                            {
                                int CamIndex = YAct.Arms.IndexOf(Arm);
                                int AnimIndex = Arm.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Arm.Info[AnimIndex] = Anim;
                                YAct.Arms[CamIndex] = Arm;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Arm;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Unk4 Model)
                            {
                                int CamIndex = YAct.UnknownC4.IndexOf(Model);
                                int AnimIndex = Model.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Model.Info[AnimIndex] = Anim;
                                YAct.UnknownC4[CamIndex] = Model;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Model;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                        }
                    }
                    else
                    {
                        if (SelectedNode.Parent.Parent.Tag is CSVData.YAct YAct)
                        {
                            if (SelectedNode.Parent.Tag is CSVData.Player Player)
                            {
                                int AnimIndex = Player.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Player.Info[AnimIndex] = Anim;
                                YAct.Player = Player;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Player;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Enemy Enemy)
                            {
                                int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                                int AnimIndex = Enemy.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.FrameStart = TryParseFP(NewText);
                                Enemy.Info[AnimIndex] = Anim;
                                YAct.Enemies[EnemyIndex] = Enemy;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Enemy;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.CameraInfo Camera)
                            {
                                int CamIndex = YAct.CamInfos.IndexOf(Camera);
                                int AnimIndex = Camera.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Camera.Info[AnimIndex] = Anim;
                                YAct.CamInfos[CamIndex] = Camera;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Camera;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Object Object)
                            {
                                int CamIndex = YAct.Objects.IndexOf(Object);
                                int AnimIndex = Object.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Object.Info[AnimIndex] = Anim;
                                YAct.Objects[CamIndex] = Object;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Object;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Arm Arm)
                            {
                                int CamIndex = YAct.Arms.IndexOf(Arm);
                                int AnimIndex = Arm.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Arm.Info[AnimIndex] = Anim;
                                YAct.Arms[CamIndex] = Arm;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Arm;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Unk4 Model)
                            {
                                int CamIndex = YAct.UnknownC4.IndexOf(Model);
                                int AnimIndex = Model.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Anim.Speed = TryParseFP(NewText);
                                Model.Info[AnimIndex] = Anim;
                                YAct.UnknownC4[CamIndex] = Model;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Model;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                        }
                    }

                });
                Button buttonE = CreateButton("Export", 1, 4);
                Button buttonI = CreateButton("Import", 1, 5);
                buttonE.Click += (s, e) =>
                {
                    if (SelectedNode.Parent.Tag is CSVData.Player Player)
                    {
                        ExportFile("OMT", Anim.AnimationForYAct);
                    }
                    else if (SelectedNode.Parent.Tag is CSVData.Enemy Enemy)
                    {
                        ExportFile("OMT", Anim.AnimationForYAct);
                    }
                    else if (SelectedNode.Parent.Tag is CSVData.CameraInfo Camera)
                    {
                        ExportFile("MTBW", Anim.AnimationForYAct);
                    }
                    else if (SelectedNode.Parent.Tag is CSVData.Object Object)
                    {
                        ExportFile("MTBW", Anim.AnimationForYAct);
                    }
                    else if (SelectedNode.Parent.Tag is CSVData.Arm Arm)
                    {
                        ExportFile("MTBW", Anim.AnimationForYAct);
                    }
                    else if (SelectedNode.Parent.Tag is CSVData.Unk4 Model)
                    {
                        ExportFile("MTBW", Anim.AnimationForYAct);
                    }
                };
                buttonI.Click += (s, e) =>
                {
                    if (SelectedNode.Parent.Parent.Parent.Text == "Category 1")
                    {
                        if (SelectedNode.Parent.Parent.Tag is CSVData.YAct YAct)
                        {
                            if (SelectedNode.Parent.Tag is CSVData.Player Player)
                            {
                                int AnimIndex = Player.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Player.Info[AnimIndex] = Anim;
                                YAct.Player = Player;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Player;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Enemy Enemy)
                            {
                                int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                                int AnimIndex = Enemy.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                Byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Enemy.Info[AnimIndex] = Anim;
                                YAct.Enemies[EnemyIndex] = Enemy;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Enemy;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.CameraInfo Camera)
                            {
                                int CamIndex = YAct.CamInfos.IndexOf(Camera);
                                int AnimIndex = Camera.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                byte[] MTBW = ChangeFile("MTBW");
                                Anim.AnimationForYAct = MTBW;
                                Camera.Info[AnimIndex] = Anim;
                                YAct.CamInfos[CamIndex] = Camera;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Camera;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Object Object)
                            {
                                int CamIndex = YAct.Objects.IndexOf(Object);
                                int AnimIndex = Object.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Object.Info[AnimIndex] = Anim;
                                YAct.Objects[CamIndex] = Object;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Object;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Arm Arm)
                            {
                                int CamIndex = YAct.Arms.IndexOf(Arm);
                                int AnimIndex = Arm.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Arm.Info[AnimIndex] = Anim;
                                YAct.Arms[CamIndex] = Arm;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Arm;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Unk4 Model)
                            {
                                int CamIndex = YAct.UnknownC4.IndexOf(Model);
                                int AnimIndex = Model.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category1.IndexOf(YAct);
                                byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Model.Info[AnimIndex] = Anim;
                                YAct.UnknownC4[CamIndex] = Model;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Model;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category1[YActIndex] = YAct;
                            }
                        }
                    }
                    else
                    {
                        if (SelectedNode.Parent.Parent.Tag is CSVData.YAct YAct)
                        {
                            if (SelectedNode.Parent.Tag is CSVData.Player Player)
                            {
                                int AnimIndex = Player.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Player.Info[AnimIndex] = Anim;
                                YAct.Player = Player;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Player;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Enemy Enemy)
                            {
                                int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                                int AnimIndex = Enemy.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                Byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Enemy.Info[AnimIndex] = Anim;
                                YAct.Enemies[EnemyIndex] = Enemy;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Enemy;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.CameraInfo Camera)
                            {
                                int CamIndex = YAct.CamInfos.IndexOf(Camera);
                                int AnimIndex = Camera.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                byte[] MTBW = ChangeFile("MTBW");
                                Anim.AnimationForYAct = MTBW;
                                Camera.Info[AnimIndex] = Anim;
                                YAct.CamInfos[CamIndex] = Camera;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Camera;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Object Object)
                            {
                                int CamIndex = YAct.Objects.IndexOf(Object);
                                int AnimIndex = Object.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Object.Info[AnimIndex] = Anim;
                                YAct.Objects[CamIndex] = Object;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Object;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Arm Arm)
                            {
                                int CamIndex = YAct.Arms.IndexOf(Arm);
                                int AnimIndex = Arm.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Arm.Info[AnimIndex] = Anim;
                                YAct.Arms[CamIndex] = Arm;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Arm;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                            else if (SelectedNode.Parent.Tag is CSVData.Unk4 Model)
                            {
                                int CamIndex = YAct.UnknownC4.IndexOf(Model);
                                int AnimIndex = Model.Info.IndexOf(Anim);
                                int YActIndex = CSVData.Category2.IndexOf(YAct);
                                byte[] OMT = ChangeFile("OMT");
                                Anim.AnimationForYAct = OMT;
                                Model.Info[AnimIndex] = Anim;
                                YAct.UnknownC4[CamIndex] = Model;
                                SelectedNode.Tag = Anim;
                                SelectedNode.Parent.Tag = Model;
                                SelectedNode.Parent.Parent.Tag = YAct;
                                CSVData.Category2[YActIndex] = YAct;
                            }
                        }
                    }
                };
                tableLayoutPanel1.ResumeLayout();
            }
        }

        public byte[] ChangeFile(string Type)
        {
            using (OpenFileDialog OpenFile = new OpenFileDialog())
            {
                if (Type == "OMT")
                {
                    OpenFile.Filter = "PS2 OMT(*.omt,.dat) |*.omt;*.dat";
                    OpenFile.Title = "Import OMT";
                }
                else if (Type == "MTBW")
                {
                    OpenFile.Filter = "PS2 MTBW(*.MTBW,) |*.MTBW";
                    OpenFile.Title = "Import MTBW";
                }
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    string path = OpenFile.FileName;
                    using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                    {
                        byte[] File = reader.ReadBytes((int)reader.BaseStream.Length);
                        return File;
                    }
                }
            }
            byte[] Null = { 0x00 };
            return Null;
        }
        private void ExportFile(string Type, byte[] NewFile)
        {
            using (SaveFileDialog OpenFile = new SaveFileDialog())
            {
                if (Type == "OMT")
                {
                    OpenFile.Filter = "PS2 OMT(*.omt,.dat) |*.omt;*.dat";
                    OpenFile.Title = "Export OMT";
                }
                else if (Type == "MTBW")
                {
                    OpenFile.Filter = "PS2 MTBW(*.MTBW,) |*.MTBW";
                    OpenFile.Title = "Export MTBW";
                }
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    string path = OpenFile.FileName;
                    using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
                    {
                        writer.Write(NewFile);
                    }
                }
            }
        }
        public Label CreateLabelTitle(string Text, int Col, int Row)
        {
            Label Label = new Label();
            Label.Text = Text;
            Label.AutoSize = true;
            Label.Anchor = AnchorStyles.Right;
            Label.Font = new Font("Arial", 14, FontStyle.Bold);
            tableLayoutPanel1.Controls.Add(Label, Col, Row);
            return Label;
        }
        public Label CreateLabel(string Text, int Col, int Row)
        {
            Label Label = new Label();
            Label.Text = Text;
            Label.AutoSize = true;
            Label.Anchor = AnchorStyles.Right;
            tableLayoutPanel1.Controls.Add(Label, Col, Row);
            return Label;
        }
        public TextBox CreateText(string Text, int Col, int Row, Action<string> ChangedHandlder = null)
        {
            TextBox TextBox = new TextBox();
            TextBox.Text = Text;
            TextBox.Width = 80;
            if (ChangedHandlder != null)
            {
                TextBox.TextChanged += (sender, e) =>
                {
                    ChangedHandlder?.Invoke(TextBox.Text);
                };
            }

            tableLayoutPanel1.Controls.Add(TextBox, Col, Row);
            return TextBox;
        }
        public Button CreateButton(string Text, int Col, int Row)
        {
            Button Button = new Button();
            Button.Text = Text;
            Button.Width = 100;
            tableLayoutPanel1.Controls.Add(Button, Col, Row);
            return Button;
        }

        public uint TryParse32(string text)
        {
            uint result = 0;
            if (uint.TryParse(text, out uint Value))
            {
                result = Value;
            }
            return result;
        }

        public int TryParseS32(string text)
        {
            int result = 0;
            if (int.TryParse(text, out int Value))
            {
                result = Value;
            }
            return result;
        }

        public ushort TryParse16(string text)
        {
            ushort result = 0;
            if (ushort.TryParse(text, out ushort Value))
            {
                result = Value;
            }
            return result;
        }

        public byte TryParse8(string text)
        {
            byte result = 0;
            if (byte.TryParse(text, out byte Value))
            {
                result = Value;
            }
            return result;
        }

        public float TryParseFP(string text)
        {
            float result = 0;
            if (float.TryParse(text, out float Value))
            {
                result = Value;
            }
            return result;
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void AddEffectToParent(TreeNode Node, Effects.IEffect Effect)
        {
            if (treeView1.SelectedNode != null)
            {
                if (treeView1.SelectedNode.Tag is YactDataY2.CharaInfo Chara)
                {
                    int Index = DataY2.CharaInfos.IndexOf(Chara);
                    Chara.Effects.Add(Effect);
                    DataY2.CharaInfos[Index] = Chara;
                    treeView1.SelectedNode.Nodes.Add(Node);
                }
                else if (treeView1.SelectedNode.Tag is YactDataY2.CameraInfo Cam)
                {
                    int Index = DataY2.CamInfos.IndexOf(Cam);
                    Cam.Effects.Add(Effect);
                    DataY2.CamInfos[Index] = Cam;
                    treeView1.SelectedNode.Nodes.Add(Node);
                }
                else if (treeView1.SelectedNode.Tag is CSVData.Player Player)
                {
                    if (treeView1.SelectedNode.Parent.Tag is CSVData.YAct YAct)
                    {
                        if (treeView1.SelectedNode.Parent.Parent.Text == "Category 1")
                        {
                            int Index = CSVData.Category1.IndexOf(YAct);
                            Player.Effects.Add(Effect);
                            YAct.Player = Player;
                            treeView1.SelectedNode.Tag = Player;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category1[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                        else
                        {
                            int Index = CSVData.Category2.IndexOf(YAct);
                            Player.Effects.Add(Effect);
                            YAct.Player = Player;
                            treeView1.SelectedNode.Tag = Player;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category2[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                    }
                }
                else if (treeView1.SelectedNode.Tag is CSVData.Enemy Enemy)
                {
                    if (treeView1.SelectedNode.Parent.Tag is CSVData.YAct YAct)
                    {
                        if (treeView1.SelectedNode.Parent.Parent.Text == "Category 1")
                        {
                            int Index = CSVData.Category1.IndexOf(YAct);
                            int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                            Enemy.Effects.Add(Effect);
                            YAct.Enemies[EnemyIndex] = Enemy;
                            treeView1.SelectedNode.Tag = Enemy;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category1[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                        else
                        {
                            int Index = CSVData.Category2.IndexOf(YAct);
                            int EnemyIndex = YAct.Enemies.IndexOf(Enemy);
                            Enemy.Effects.Add(Effect);
                            YAct.Enemies[EnemyIndex] = Enemy;
                            treeView1.SelectedNode.Tag = Enemy;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category2[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                    }

                }
                else if (treeView1.SelectedNode.Tag is CSVData.Object Object)
                {
                    if (treeView1.SelectedNode.Parent.Tag is CSVData.YAct YAct)
                    {
                        if (treeView1.SelectedNode.Parent.Parent.Text == "Category 1")
                        {
                            int Index = CSVData.Category1.IndexOf(YAct);
                            int EnemyIndex = YAct.Objects.IndexOf(Object);
                            Object.Effects.Add(Effect);
                            YAct.Objects[EnemyIndex] = Object;
                            treeView1.SelectedNode.Tag = Object;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category1[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                        else
                        {
                            int Index = CSVData.Category2.IndexOf(YAct);
                            int EnemyIndex = YAct.Objects.IndexOf(Object);
                            Object.Effects.Add(Effect);
                            YAct.Objects[EnemyIndex] = Object;
                            treeView1.SelectedNode.Tag = Object;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category2[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                    }

                }
                else if (treeView1.SelectedNode.Tag is CSVData.Unk4 Model)
                {
                    if (treeView1.SelectedNode.Parent.Tag is CSVData.YAct YAct)
                    {
                        if (treeView1.SelectedNode.Parent.Parent.Text == "Category 1")
                        {
                            int Index = CSVData.Category1.IndexOf(YAct);
                            int EnemyIndex = YAct.UnknownC4.IndexOf(Model);
                            Model.Effects.Add(Effect);
                            YAct.UnknownC4[EnemyIndex] = Model;
                            treeView1.SelectedNode.Tag = Model;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category1[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                        else
                        {
                            int Index = CSVData.Category2.IndexOf(YAct);
                            int EnemyIndex = YAct.UnknownC4.IndexOf(Model);
                            Model.Effects.Add(Effect);
                            YAct.UnknownC4[EnemyIndex] = Model;
                            treeView1.SelectedNode.Tag = Model;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category2[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                    }

                }
                else if (treeView1.SelectedNode.Tag is CSVData.CameraInfo Camera)
                {
                    if (treeView1.SelectedNode.Parent.Tag is CSVData.YAct YAct)
                    {
                        if (treeView1.SelectedNode.Parent.Parent.Text == "Category 1")
                        {
                            int Index = CSVData.Category1.IndexOf(YAct);
                            int EnemyIndex = YAct.CamInfos.IndexOf(Camera);
                            Camera.Effects.Add(Effect);
                            YAct.CamInfos[EnemyIndex] = Camera;
                            treeView1.SelectedNode.Tag = Camera;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category1[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                        else
                        {
                            int Index = CSVData.Category2.IndexOf(YAct);
                            int EnemyIndex = YAct.CamInfos.IndexOf(Camera);
                            Camera.Effects.Add(Effect);
                            YAct.CamInfos[EnemyIndex] = Camera;
                            treeView1.SelectedNode.Tag = Camera;
                            treeView1.SelectedNode.Parent.Tag = YAct;
                            CSVData.Category2[Index] = YAct;
                            treeView1.SelectedNode.Nodes.Add(Node);
                        }
                    }
                }
            }
        }

        private void damageEffectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.Damage Dmg = new Effects.Damage
            {
                FrameStart = 0,
                FrameEnd = 0,
                DamageVal = 0,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Damage Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void soundCueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.SoundCue Cue = new Effects.SoundCue
            {
                FrameStart = 0,
                FrameEnd = 0,
                ParentID = 0,
                BoneNumber = 0,
                ContainerID = 0,
                VoiceID = 0,
                Speed = 1,
                SoundSpeed = 1
            };
            TreeNode Node = AddNodeEffect("Sound Cue Effect", Cue);
            AddEffectToParent(Node, Cue);
        }

        private void importYActToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.OMTs.Clear();
            Data.MTBWs.Clear();
            Data.Effects.Clear();
            Data.Chunk1.Clear();
            Data.Chunk2.Clear();
            DataY2.CharaInfos.Clear();
            DataY2.CamInfos.Clear();
            using (OpenFileDialog OpenFile = new OpenFileDialog())
            {
                OpenFile.Filter = "PS2 YAct(*.bin) |*.bin";
                OpenFile.Title = "Open YAct";
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    string path = OpenFile.FileName;
                    if (comboBox1.SelectedItem.ToString() == "Yakuza 1")
                    {
                        if (treeView1.SelectedNode != null)
                        {
                            if (treeView1.SelectedNode.Tag is CSVData.YAct YAct)
                            {
                                int Index = -1;
                                if (treeView1.SelectedNode.Parent.Text == "Category 1")
                                {
                                    Index = CSVData.Category1.IndexOf(YAct);
                                }
                                else
                                {
                                    Index = CSVData.Category2.IndexOf(YAct);
                                }
                                TreeNode Node = treeView1.SelectedNode;
                                ReadYact Reader = new ReadYact();
                                Reader.ReadYactFile(path, ref Data, ref YAct, ref Node);
                                if (treeView1.SelectedNode.Parent.Text == "Category 1")
                                {
                                    CSVData.Category1[Index] = YAct;
                                }
                                else
                                {
                                    CSVData.Category2[Index] = YAct;
                                }
                                foreach (TreeNode Child in Node.Nodes)
                                {
                                    if (Child.Tag is CSVData.Enemy Enemy)
                                    {
                                        foreach (Effects.IEffect Effect in Enemy.Effects)
                                        {
                                            TreeNode ENode = new TreeNode();
                                            if (Effect is Effects.Damage Dmg)
                                            {
                                                ENode = AddNodeEffect("Damage Effect", Dmg);
                                            }
                                            else if (Effect is Effects.SoundCue Cue)
                                            {
                                                ENode = AddNodeEffect("Sound Cue Effect", Cue);
                                            }
                                            else if (Effect is Effects.ParticleNormal Ptcl)
                                            {
                                                ENode = AddNodeEffect("Particle Effect", Ptcl);
                                            }
                                            else if (Effect is Effects.ParticleTrail Trail)
                                            {
                                                ENode = AddNodeEffect("Trail Effect", Trail);
                                            }
                                            else if (Effect is Effects.ButtonTiming BtnT)
                                            {
                                                ENode = AddNodeEffect("Button Window Effect", BtnT);
                                            }
                                            else if (Effect is Effects.ButtonSpam BtnS)
                                            {
                                                ENode = AddNodeEffect("Button Spam Effect", BtnS);
                                            }
                                            else if (Effect is Effects.Loop Loop)
                                            {
                                                ENode = AddNodeEffect("Loop Effect", Loop);
                                            }
                                            else if (Effect is Effects.NormalBranch LoopE)
                                            {
                                                ENode = AddNodeEffect("Normal Branch Effect", LoopE);
                                            }
                                            else if (Effect is Effects.CounterBranch CBranch)
                                            {
                                                ENode = AddNodeEffect("Counter Branch Effect", CBranch);
                                            }
                                            else if (Effect is Effects.CounterUp CUp)
                                            {
                                                ENode = AddNodeEffect("Counter Up Effect", CUp);
                                            }
                                            else if (Effect is Effects.CounterReset CReset)
                                            {
                                                ENode = AddNodeEffect("Counter Reset Effect", CReset);
                                            }
                                            else if (Effect is Effects.Finish Finish)
                                            {
                                                ENode = AddNodeEffect("Finish Effect", Finish);
                                            }
                                            else if (Effect is Effects.Dead1 Dead)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 1", Dead);
                                            }
                                            else if (Effect is Effects.Dead2 Dead2)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 2", Dead2);
                                            }
                                            else if (Effect is Effects.ChangeFinishStatus CFinish)
                                            {
                                                ENode = AddNodeEffect("Change Finish Status Effect", CFinish);
                                            }
                                            else if (Effect is Effects.ScreenFlash ScrF)
                                            {
                                                ENode = AddNodeEffect("Screen Flash Effect", ScrF);
                                            }
                                            else if (Effect is Effects.AfterImage AImage)
                                            {
                                                ENode = AddNodeEffect("After Image Effect", AImage);
                                            }
                                            else if (Effect is Effects.CtrlVibration Vib)
                                            {
                                                ENode = AddNodeEffect("Vibration Effect", Vib);
                                            }
                                            else if (Effect is Effects.UnknownEffect Unk)
                                            {
                                                ENode = AddNodeEffect("Unknown Effect", Unk);
                                            }
                                            else if (Effect is Effects.HactEvent HE)
                                            {
                                                ENode = AddNodeEffect(HE.Name, HE);
                                            }
                                            Child.Nodes.Add(ENode);
                                        }
                                    }
                                    else if (Child.Tag is CSVData.Player Player)
                                    {
                                        foreach (Effects.IEffect Effect in Player.Effects)
                                        {
                                            TreeNode ENode = new TreeNode();
                                            if (Effect is Effects.Damage Dmg)
                                            {
                                                ENode = AddNodeEffect("Damage Effect", Dmg);
                                            }
                                            else if (Effect is Effects.SoundCue Cue)
                                            {
                                                ENode = AddNodeEffect("Sound Cue Effect", Cue);
                                            }
                                            else if (Effect is Effects.ParticleNormal Ptcl)
                                            {
                                                ENode = AddNodeEffect("Particle Effect", Ptcl);
                                            }
                                            else if (Effect is Effects.ParticleTrail Trail)
                                            {
                                                ENode = AddNodeEffect("Trail Effect", Trail);
                                            }
                                            else if (Effect is Effects.ButtonTiming BtnT)
                                            {
                                                ENode = AddNodeEffect("Button Window Effect", BtnT);
                                            }
                                            else if (Effect is Effects.ButtonSpam BtnS)
                                            {
                                                ENode = AddNodeEffect("Button Spam Effect", BtnS);
                                            }
                                            else if (Effect is Effects.Loop Loop)
                                            {
                                                ENode = AddNodeEffect("Loop Effect", Loop);
                                            }
                                            else if (Effect is Effects.NormalBranch LoopE)
                                            {
                                                ENode = AddNodeEffect("Normal Branch Effect", LoopE);
                                            }
                                            else if (Effect is Effects.CounterBranch CBranch)
                                            {
                                                ENode = AddNodeEffect("Counter Branch Effect", CBranch);
                                            }
                                            else if (Effect is Effects.CounterUp CUp)
                                            {
                                                ENode = AddNodeEffect("Counter Up Effect", CUp);
                                            }
                                            else if (Effect is Effects.CounterReset CReset)
                                            {
                                                ENode = AddNodeEffect("Counter Reset Effect", CReset);
                                            }
                                            else if (Effect is Effects.Finish Finish)
                                            {
                                                ENode = AddNodeEffect("Finish Effect", Finish);
                                            }
                                            else if (Effect is Effects.Dead1 Dead)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 1", Dead);
                                            }
                                            else if (Effect is Effects.Dead2 Dead2)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 2", Dead2);
                                            }
                                            else if (Effect is Effects.ChangeFinishStatus CFinish)
                                            {
                                                ENode = AddNodeEffect("Change Finish Status Effect", CFinish);
                                            }
                                            else if (Effect is Effects.ScreenFlash ScrF)
                                            {
                                                ENode = AddNodeEffect("Screen Flash Effect", ScrF);
                                            }
                                            else if (Effect is Effects.AfterImage AImage)
                                            {
                                                ENode = AddNodeEffect("After Image Effect", AImage);
                                            }
                                            else if (Effect is Effects.CtrlVibration Vib)
                                            {
                                                ENode = AddNodeEffect("Vibration Effect", Vib);
                                            }
                                            else if (Effect is Effects.UnknownEffect Unk)
                                            {
                                                ENode = AddNodeEffect("Unknown Effect", Unk);
                                            }
                                            else if (Effect is Effects.HactEvent HE)
                                            {
                                                ENode = AddNodeEffect(HE.Name, HE);
                                            }
                                            Child.Nodes.Add(ENode);
                                        }
                                    }
                                    else if (Child.Tag is CSVData.CameraInfo Camera)
                                    {
                                        foreach (Effects.IEffect Effect in Camera.Effects)
                                        {
                                            TreeNode ENode = new TreeNode();
                                            if (Effect is Effects.Damage Dmg)
                                            {
                                                ENode = AddNodeEffect("Damage Effect", Dmg);
                                            }
                                            else if (Effect is Effects.SoundCue Cue)
                                            {
                                                ENode = AddNodeEffect("Sound Cue Effect", Cue);
                                            }
                                            else if (Effect is Effects.ParticleNormal Ptcl)
                                            {
                                                ENode = AddNodeEffect("Particle Effect", Ptcl);
                                            }
                                            else if (Effect is Effects.ParticleTrail Trail)
                                            {
                                                ENode = AddNodeEffect("Trail Effect", Trail);
                                            }
                                            else if (Effect is Effects.ButtonTiming BtnT)
                                            {
                                                ENode = AddNodeEffect("Button Window Effect", BtnT);
                                            }
                                            else if (Effect is Effects.ButtonSpam BtnS)
                                            {
                                                ENode = AddNodeEffect("Button Spam Effect", BtnS);
                                            }
                                            else if (Effect is Effects.Loop Loop)
                                            {
                                                ENode = AddNodeEffect("Loop Effect", Loop);
                                            }
                                            else if (Effect is Effects.NormalBranch LoopE)
                                            {
                                                ENode = AddNodeEffect("Normal Branch Effect", LoopE);
                                            }
                                            else if (Effect is Effects.CounterBranch CBranch)
                                            {
                                                ENode = AddNodeEffect("Counter Branch Effect", CBranch);
                                            }
                                            else if (Effect is Effects.CounterUp CUp)
                                            {
                                                ENode = AddNodeEffect("Counter Up Effect", CUp);
                                            }
                                            else if (Effect is Effects.CounterReset CReset)
                                            {
                                                ENode = AddNodeEffect("Counter Reset Effect", CReset);
                                            }
                                            else if (Effect is Effects.Finish Finish)
                                            {
                                                ENode = AddNodeEffect("Finish Effect", Finish);
                                            }
                                            else if (Effect is Effects.Dead1 Dead)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 1", Dead);
                                            }
                                            else if (Effect is Effects.Dead2 Dead2)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 2", Dead2);
                                            }
                                            else if (Effect is Effects.ChangeFinishStatus CFinish)
                                            {
                                                ENode = AddNodeEffect("Change Finish Status Effect", CFinish);
                                            }
                                            else if (Effect is Effects.ScreenFlash ScrF)
                                            {
                                                ENode = AddNodeEffect("Screen Flash Effect", ScrF);
                                            }
                                            else if (Effect is Effects.AfterImage AImage)
                                            {
                                                ENode = AddNodeEffect("After Image Effect", AImage);
                                            }
                                            else if (Effect is Effects.CtrlVibration Vib)
                                            {
                                                ENode = AddNodeEffect("Vibration Effect", Vib);
                                            }
                                            else if (Effect is Effects.UnknownEffect Unk)
                                            {
                                                ENode = AddNodeEffect("Unknown Effect", Unk);
                                            }
                                            else if (Effect is Effects.HactEvent HE)
                                            {
                                                ENode = AddNodeEffect(HE.Name, HE);
                                            }
                                            Child.Nodes.Add(ENode);
                                        }
                                    }
                                    else if (Child.Tag is CSVData.Object Object)
                                    {
                                        foreach (Effects.IEffect Effect in Object.Effects)
                                        {
                                            TreeNode ENode = new TreeNode();
                                            if (Effect is Effects.Damage Dmg)
                                            {
                                                ENode = AddNodeEffect("Damage Effect", Dmg);
                                            }
                                            else if (Effect is Effects.SoundCue Cue)
                                            {
                                                ENode = AddNodeEffect("Sound Cue Effect", Cue);
                                            }
                                            else if (Effect is Effects.ParticleNormal Ptcl)
                                            {
                                                ENode = AddNodeEffect("Particle Effect", Ptcl);
                                            }
                                            else if (Effect is Effects.ParticleTrail Trail)
                                            {
                                                ENode = AddNodeEffect("Trail Effect", Trail);
                                            }
                                            else if (Effect is Effects.ButtonTiming BtnT)
                                            {
                                                ENode = AddNodeEffect("Button Window Effect", BtnT);
                                            }
                                            else if (Effect is Effects.ButtonSpam BtnS)
                                            {
                                                ENode = AddNodeEffect("Button Spam Effect", BtnS);
                                            }
                                            else if (Effect is Effects.Loop Loop)
                                            {
                                                ENode = AddNodeEffect("Loop Effect", Loop);
                                            }
                                            else if (Effect is Effects.NormalBranch LoopE)
                                            {
                                                ENode = AddNodeEffect("Normal Branch Effect", LoopE);
                                            }
                                            else if (Effect is Effects.CounterBranch CBranch)
                                            {
                                                ENode = AddNodeEffect("Counter Branch Effect", CBranch);
                                            }
                                            else if (Effect is Effects.CounterUp CUp)
                                            {
                                                ENode = AddNodeEffect("Counter Up Effect", CUp);
                                            }
                                            else if (Effect is Effects.CounterReset CReset)
                                            {
                                                ENode = AddNodeEffect("Counter Reset Effect", CReset);
                                            }
                                            else if (Effect is Effects.Finish Finish)
                                            {
                                                ENode = AddNodeEffect("Finish Effect", Finish);
                                            }
                                            else if (Effect is Effects.Dead1 Dead)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 1", Dead);
                                            }
                                            else if (Effect is Effects.Dead2 Dead2)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 2", Dead2);
                                            }
                                            else if (Effect is Effects.ChangeFinishStatus CFinish)
                                            {
                                                ENode = AddNodeEffect("Change Finish Status Effect", CFinish);
                                            }
                                            else if (Effect is Effects.ScreenFlash ScrF)
                                            {
                                                ENode = AddNodeEffect("Screen Flash Effect", ScrF);
                                            }
                                            else if (Effect is Effects.AfterImage AImage)
                                            {
                                                ENode = AddNodeEffect("After Image Effect", AImage);
                                            }
                                            else if (Effect is Effects.CtrlVibration Vib)
                                            {
                                                ENode = AddNodeEffect("Vibration Effect", Vib);
                                            }
                                            else if (Effect is Effects.UnknownEffect Unk)
                                            {
                                                ENode = AddNodeEffect("Unknown Effect", Unk);
                                            }
                                            else if (Effect is Effects.HactEvent HE)
                                            {
                                                ENode = AddNodeEffect(HE.Name, HE);
                                            }
                                            Child.Nodes.Add(ENode);
                                        }
                                    }
                                    else if (Child.Tag is CSVData.Unk4 Model)
                                    {
                                        foreach (Effects.IEffect Effect in Model.Effects)
                                        {
                                            TreeNode ENode = new TreeNode();
                                            if (Effect is Effects.Damage Dmg)
                                            {
                                                ENode = AddNodeEffect("Damage Effect", Dmg);
                                            }
                                            else if (Effect is Effects.SoundCue Cue)
                                            {
                                                ENode = AddNodeEffect("Sound Cue Effect", Cue);
                                            }
                                            else if (Effect is Effects.ParticleNormal Ptcl)
                                            {
                                                ENode = AddNodeEffect("Particle Effect", Ptcl);
                                            }
                                            else if (Effect is Effects.ParticleTrail Trail)
                                            {
                                                ENode = AddNodeEffect("Trail Effect", Trail);
                                            }
                                            else if (Effect is Effects.ButtonTiming BtnT)
                                            {
                                                ENode = AddNodeEffect("Button Window Effect", BtnT);
                                            }
                                            else if (Effect is Effects.ButtonSpam BtnS)
                                            {
                                                ENode = AddNodeEffect("Button Spam Effect", BtnS);
                                            }
                                            else if (Effect is Effects.Loop Loop)
                                            {
                                                ENode = AddNodeEffect("Loop Effect", Loop);
                                            }
                                            else if (Effect is Effects.NormalBranch LoopE)
                                            {
                                                ENode = AddNodeEffect("Normal Branch Effect", LoopE);
                                            }
                                            else if (Effect is Effects.CounterBranch CBranch)
                                            {
                                                ENode = AddNodeEffect("Counter Branch Effect", CBranch);
                                            }
                                            else if (Effect is Effects.CounterUp CUp)
                                            {
                                                ENode = AddNodeEffect("Counter Up Effect", CUp);
                                            }
                                            else if (Effect is Effects.CounterReset CReset)
                                            {
                                                ENode = AddNodeEffect("Counter Reset Effect", CReset);
                                            }
                                            else if (Effect is Effects.Finish Finish)
                                            {
                                                ENode = AddNodeEffect("Finish Effect", Finish);
                                            }
                                            else if (Effect is Effects.Dead1 Dead)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 1", Dead);
                                            }
                                            else if (Effect is Effects.Dead2 Dead2)
                                            {
                                                ENode = AddNodeEffect("Dead Effect 2", Dead2);
                                            }
                                            else if (Effect is Effects.ChangeFinishStatus CFinish)
                                            {
                                                ENode = AddNodeEffect("Change Finish Status Effect", CFinish);
                                            }
                                            else if (Effect is Effects.ScreenFlash ScrF)
                                            {
                                                ENode = AddNodeEffect("Screen Flash Effect", ScrF);
                                            }
                                            else if (Effect is Effects.AfterImage AImage)
                                            {
                                                ENode = AddNodeEffect("After Image Effect", AImage);
                                            }
                                            else if (Effect is Effects.CtrlVibration Vib)
                                            {
                                                ENode = AddNodeEffect("Vibration Effect", Vib);
                                            }
                                            else if (Effect is Effects.UnknownEffect Unk)
                                            {
                                                ENode = AddNodeEffect("Unknown Effect", Unk);
                                            }
                                            else if (Effect is Effects.HactEvent HE)
                                            {
                                                ENode = AddNodeEffect(HE.Name, HE);
                                            }
                                            Child.Nodes.Add(ENode);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (comboBox1.SelectedItem.ToString() == "Yakuza 2")
                    {
                        treeView1.Nodes.Clear();
                        ReadYactY2 Reader = new ReadYactY2();
                        Reader.ReadY2Yact(path, ref Data, ref DataY2);
                    }
                    if (comboBox1.SelectedIndex == 0)
                    {

                    }
                    else if (comboBox1.SelectedIndex == 1)
                    {
                        TreeNode YActNode = AddNodeYActCategory("YAct");
                        foreach (YactDataY2.CharaInfo Chara in DataY2.CharaInfos)
                        {
                            TreeNode Node = AddNodeChara(Chara.Name, Chara, YActNode);
                            foreach (YactDataY2.CamCharaInfoEntry Info in Chara.Info)
                            {
                                AddNodeAnim("Animation", Info, Node);
                            }
                            foreach (Effects.IEffect Effect in Chara.Effects)
                            {
                                TreeNode ENode = new TreeNode();
                                if (Effect is Effects.Damage Dmg)
                                {
                                    ENode = AddNodeEffect("Damage Effect", Dmg);
                                }
                                else if (Effect is Effects.SoundCue Cue)
                                {
                                    ENode = AddNodeEffect("Sound Cue Effect", Cue);
                                }
                                else if (Effect is Effects.ParticleNormal Ptcl)
                                {
                                    ENode = AddNodeEffect("Particle Effect", Ptcl);
                                }
                                else if (Effect is Effects.ParticleTrail Trail)
                                {
                                    ENode = AddNodeEffect("Trail Effect", Trail);
                                }
                                else if (Effect is Effects.ButtonTiming BtnT)
                                {
                                    ENode = AddNodeEffect("Button Window Effect", BtnT);
                                }
                                else if (Effect is Effects.ButtonSpam BtnS)
                                {
                                    ENode = AddNodeEffect("Button Spam Effect", BtnS);
                                }
                                else if (Effect is Effects.Loop Loop)
                                {
                                    ENode = AddNodeEffect("Loop Effect", Loop);
                                }
                                else if (Effect is Effects.NormalBranch LoopE)
                                {
                                    ENode = AddNodeEffect("Normal Branch Effect", LoopE);
                                }
                                else if (Effect is Effects.ScreenFlash ScrF)
                                {
                                    ENode = AddNodeEffect("Screen Flash Effect", ScrF);
                                }
                                else if (Effect is Effects.AfterImage AImage)
                                {
                                    ENode = AddNodeEffect("After Image Effect", AImage);
                                }
                                else if (Effect is Effects.CtrlVibration Vib)
                                {
                                    ENode = AddNodeEffect("Vibration Effect", Vib);
                                }
                                else if (Effect is Effects.UnknownEffect Unk)
                                {
                                    ENode = AddNodeEffect("Unknown Effect", Unk);
                                }
                                else if (Effect is Effects.HactEvent HE)
                                {
                                    ENode = AddNodeEffect(HE.Name, HE);
                                }
                                Node.Nodes.Add(ENode);
                            }
                        }
                        foreach (YactDataY2.CameraInfo Camera in DataY2.CamInfos)
                        {
                            TreeNode Node = AddNodeCam("Camera", Camera, YActNode);
                            treeView1.SelectedNode = treeView1.Nodes[treeView1.Nodes.Count - 1];
                            foreach (YactDataY2.CamCharaInfoEntry Info in Camera.Info)
                            {
                                AddNodeAnim("Animation", Info, Node);
                            }
                            foreach (Effects.IEffect Effect in Camera.Effects)
                            {
                                TreeNode ENode = new TreeNode();
                                if (Effect is Effects.Damage Dmg)
                                {
                                    ENode = AddNodeEffect("Damage Effect", Dmg);
                                }
                                else if (Effect is Effects.SoundCue Cue)
                                {
                                    ENode = AddNodeEffect("Sound Cue Effect", Cue);
                                }
                                else if (Effect is Effects.ParticleNormal Ptcl)
                                {
                                    ENode = AddNodeEffect("Particle Effect", Ptcl);
                                }
                                else if (Effect is Effects.ParticleTrail Trail)
                                {
                                    ENode = AddNodeEffect("Trail Effect", Trail);
                                }
                                else if (Effect is Effects.ButtonTiming BtnT)
                                {
                                    ENode = AddNodeEffect("Button Window Effect", BtnT);
                                }
                                else if (Effect is Effects.ButtonSpam BtnS)
                                {
                                    ENode = AddNodeEffect("Button Spam Effect", BtnS);
                                }
                                else if (Effect is Effects.Loop Loop)
                                {
                                    ENode = AddNodeEffect("Loop Effect", Loop);
                                }
                                else if (Effect is Effects.NormalBranch LoopE)
                                {
                                    ENode = AddNodeEffect("Normal Branch Effect", LoopE);
                                }
                                else if (Effect is Effects.ScreenFlash ScrF)
                                {
                                    ENode = AddNodeEffect("Screen Flash Effect", ScrF);
                                }
                                else if (Effect is Effects.AfterImage AImage)
                                {
                                    ENode = AddNodeEffect("After Image Effect", AImage);
                                }
                                else if (Effect is Effects.CtrlVibration Vib)
                                {
                                    ENode = AddNodeEffect("Vibration Effect", Vib);
                                }
                                else if (Effect is Effects.UnknownEffect Unk)
                                {
                                    ENode = AddNodeEffect("Unknown Effect", Unk);
                                }
                                else if (Effect is Effects.HactEvent HE)
                                {
                                    ENode = AddNodeEffect(HE.Name, HE);
                                }
                                Node.Nodes.Add(ENode);
                            }
                        }
                    }
                }
            }
        }

        private void exportYActToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (treeView1.SelectedNode != null)
                {
                    if (treeView1.SelectedNode.Tag is CSVData.YAct YAct)
                    {
                        WriteYact Write = new WriteYact();
                        if (treeView1.SelectedNode != null)
                        {
                            int Index = -1;
                            if (treeView1.SelectedNode.Parent.Text == "Category 1")
                            {
                                Index = CSVData.Category1.IndexOf(YAct);
                            }
                            else
                            {
                                Index = CSVData.Category2.IndexOf(YAct);
                            }
                            TreeNode Node = treeView1.SelectedNode;
                            tableLayoutPanel1.Controls.Clear();
                            Data.OMTs.Clear();
                            Data.MTBWs.Clear();
                            Data.Effects.Clear();
                            Write.WriteNewYact(Data, ref YAct, ref Node);
                            if (treeView1.SelectedNode.Parent.Text == "Category 1")
                            {
                                CSVData.Category1[Index] = YAct;
                            }
                            else
                            {
                                CSVData.Category2[Index] = YAct;
                            }
                        }

                    }
                }
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                if (treeView1.SelectedNode != null)
                {
                    if (treeView1.SelectedNode.Text == "YAct")
                    {
                        WriteYactY2 Write = new WriteYactY2();
                        Write.WriteYact(ref DataY2, ref Data, treeView1.SelectedNode);
                    }
                }

            }


        }

        private void particleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.ParticleNormal Particle = new Effects.ParticleNormal
            {
                FrameStart = 0,
                FrameEnd = 0,
                ParentID = 0,
                BoneNumber = 11,
                ptclID = 357,
                PTCLParam1 = 0,
                PTCLParam2 = 0,
                Unknown = 15,
                Flags = new byte[] { 1, 1, 0, 0 },
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Particle Effect", Particle);
            AddEffectToParent(Node, Particle);

        }

        private void trailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.ParticleTrail Trail = new Effects.ParticleTrail
            {
                FrameStart = 0,
                FrameEnd = 0,
                ParentID = 0,
                BoneNumber = 14,
                TrailParam1 = (float)-4.5,
                TrailParam2 = 1,
                TrailParam3 = 7,
                Unknown1 = 15,
                Unknown2 = 2,
                RGBA1 = new Effects.RGBA8
                {
                    Red = 255,
                    Green = 255,
                    Blue = 255,
                    Alpha = 0,
                },
                RGBA2 = new Effects.RGBA8
                {
                    Red = 255,
                    Green = 255,
                    Blue = 255,
                    Alpha = 80,
                },
                Flags = new byte[] { 1, 1, 1, 0 },
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Trail Effect", Trail);
            AddEffectToParent(Node, Trail);
        }

        private void screenFlashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.ScreenFlash ScrF = new Effects.ScreenFlash
            {
                FrameStart = 0,
                FrameEnd = 0,
                ParentID = 0,
                Unknown1 = 10,
                Unknown2 = 10,
                RGBA = new Effects.RGBA8
                {
                    Red = 255,
                    Green = 255,
                    Blue = 255,
                    Alpha = 80,
                },
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Screen Flash Effect", ScrF);
            AddEffectToParent(Node, ScrF);
        }

        private void afterImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.AfterImage AImage = new Effects.AfterImage
            {
                FrameStart = 0,
                FrameEnd = 0,
                ParentID = 0,
                Unknown1 = 3,
                Unknown2 = 8,
                Param1 = (float)0.4,
                Param2 = (float)0.6,
                Scale = (float)1.1,
                RGBA = new Effects.RGBA8
                {
                    Red = 128,
                    Green = 128,
                    Blue = 128,
                    Alpha = 40,
                },
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("After Image Effect", AImage);
            AddEffectToParent(Node, AImage);
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            Data.OMTs.Clear();
            Data.MTBWs.Clear();
            Data.Effects.Clear();
            Data.Chunk1.Clear();
            Data.Chunk2.Clear();
            PropData.Effects.Clear();
            PropData.PropData.Clear();
            using (OpenFileDialog OpenFile = new OpenFileDialog())
            {
                OpenFile.Filter = "PS2 Move Property(*.dat) |*.dat";
                OpenFile.Title = "Open Move Property";
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    string path = OpenFile.FileName;
                    ReadMoveProps Reader = new ReadMoveProps();
                    Reader.ReadBep(path, ref PropData, ref Data);
                    foreach (MovePropData.Property Property in PropData.PropData)
                    {
                        AddNodeProp("Property", Property);
                    }
                    foreach (Effects.IEffect Effect in Data.Effects)
                    {
                        if (Effect is Effects.ParticleNormal Ptcl)
                        {
                            AddNodeEffect("Particle Effect", Ptcl);
                        }
                        else if (Effect is Effects.ParticleTrail Trail)
                        {
                            AddNodeEffect("Trail Effect", Trail);
                        }
                        else if (Effect is Effects.ScreenShakeProp Shake)
                        {
                            AddNodeEffect("Screen Shake Effect", Shake);
                        }
                    }
                }
            }
        }

        private void importCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            using (OpenFileDialog OpenFile = new OpenFileDialog())
            {
                OpenFile.Filter = "PS2 YAct CSV(*.bin) |*.bin";
                OpenFile.Title = "Open PS2 YAct CSV";
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    if (comboBox1.SelectedIndex == 1)
                    {
                        string path = OpenFile.FileName;
                        ReadCSVY2 Reader = new ReadCSVY2();
                        Reader.ReadCSV(path, ref CSVDataY2);
                        TreeNode C1 = AddNodeYActCategory("Category 1");
                        TreeNode C2 = AddNodeYActCategory("Category 2");
                        foreach (CSVDataY2.CSVEntry YAct in CSVDataY2.Category1)
                        {
                            TreeNode YActNode = AddNodeCSVYAct(YAct);
                            foreach (CSVDataY2.Character Chara in YAct.Characters)
                            {
                                TreeNode CNode = AddNodeCSVChara(Chara);
                                YActNode.Nodes.Add(CNode);
                            }
                            foreach (CSVDataY2.IHactEvent HactEvent in YAct.HactEvents)
                            {
                                TreeNode HNode = AddNodeHactEvent(HactEvent);
                                YActNode.Nodes.Add(HNode);
                            }
                            C1.Nodes.Add(YActNode);
                        }
                        foreach (CSVDataY2.CSVEntry YAct in CSVDataY2.Category2)
                        {
                            TreeNode YActNode = AddNodeCSVYAct(YAct);
                            foreach (CSVDataY2.Character Chara in YAct.Characters)
                            {
                                TreeNode CNode = AddNodeCSVChara(Chara);
                                YActNode.Nodes.Add(CNode);
                            }
                            foreach (CSVDataY2.IHactEvent HactEvent in YAct.HactEvents)
                            {
                                TreeNode HNode = AddNodeHactEvent(HactEvent);
                                YActNode.Nodes.Add(HNode);
                            }
                            C2.Nodes.Add(YActNode);
                        }
                    }
                    else
                    {
                        string path = OpenFile.FileName;
                        treeView1.Nodes.Clear();
                        CSVData.Category1.Clear();
                        CSVData.Category2.Clear();
                        ReadCSV Reader = new ReadCSV();
                        Reader.ReadCSVFile(path, CSVData);
                        TreeNode C1 = AddNodeYActCategory("Category 1");
                        TreeNode C2 = AddNodeYActCategory("Category 2");
                        foreach (CSVData.YAct YAct in CSVData.Category1)
                        {
                            TreeNode YActNode = AddNodeCSVYActY1(YAct);
                            TreeNode PNode = new TreeNode();
                            if (YAct.PlayerPointer != 0)
                            {
                                PNode = AddNodeCSVPlayerY1(YAct.Player);
                                YActNode.Nodes.Add(PNode);
                                foreach (CSVData.Anim Info in YAct.Player.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, PNode);
                                }
                            }


                            int i = 0;
                            foreach (CSVData.Enemy Enemy in YAct.Enemies)
                            {
                                TreeNode ENode = AddNodeCSVEnemyY1(Enemy, i);
                                foreach (CSVData.Anim Info in Enemy.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, ENode);
                                }
                                i++;
                                YActNode.Nodes.Add(ENode);
                            }
                            i = 0;
                            foreach (CSVData.Object Object in YAct.Objects)
                            {
                                TreeNode ENode = AddNodeCSVObjectY1(Object, i);
                                foreach (CSVData.Anim Info in Object.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, ENode);
                                }
                                i++;
                                YActNode.Nodes.Add(ENode);
                            }
                            i = 0;
                            foreach (CSVData.Arm Arm in YAct.Arms)
                            {
                                TreeNode ENode = AddNodeCSVArmY1(Arm, i);
                                foreach (CSVData.Anim Info in Arm.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, ENode);
                                }
                                i++;
                                YActNode.Nodes.Add(ENode);
                            }
                            i = 0;
                            foreach (CSVData.Unk4 Model in YAct.UnknownC4)
                            {
                                TreeNode ENode = AddNodeCSVModelY1(Model, i);
                                foreach (CSVData.Anim Info in Model.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, ENode);
                                }
                                i++;
                                YActNode.Nodes.Add(ENode);
                            }
                            foreach (CSVData.CameraInfo Camera in YAct.CamInfos)
                            {
                                TreeNode CNode = AddNodeCamY1("Camera", Camera, YActNode);
                                foreach (CSVData.Anim Info in Camera.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, CNode);
                                }
                            }
                            C1.Nodes.Add(YActNode);
                        }
                        foreach (CSVData.YAct YAct in CSVData.Category2)
                        {
                            TreeNode YActNode = AddNodeCSVYActY1(YAct);
                            TreeNode PNode = new TreeNode();
                            if (YAct.PlayerPointer != 0)
                            {
                                PNode = AddNodeCSVPlayerY1(YAct.Player);
                                YActNode.Nodes.Add(PNode);
                                foreach (CSVData.Anim Info in YAct.Player.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, PNode);
                                }
                            }


                            int i = 0;
                            foreach (CSVData.Enemy Enemy in YAct.Enemies)
                            {
                                TreeNode ENode = AddNodeCSVEnemyY1(Enemy, i);
                                foreach (CSVData.Anim Info in Enemy.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, ENode);
                                }
                                i++;
                                YActNode.Nodes.Add(ENode);
                            }
                            i = 0;
                            foreach (CSVData.Object Object in YAct.Objects)
                            {
                                TreeNode ENode = AddNodeCSVObjectY1(Object, i);
                                foreach (CSVData.Anim Info in Object.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, ENode);
                                }
                                i++;
                                YActNode.Nodes.Add(ENode);
                            }
                            i = 0;
                            foreach (CSVData.Arm Arm in YAct.Arms)
                            {
                                TreeNode ENode = AddNodeCSVArmY1(Arm, i);
                                foreach (CSVData.Anim Info in Arm.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, ENode);
                                }
                                i++;
                                YActNode.Nodes.Add(ENode);
                            }
                            i = 0;
                            foreach (CSVData.Unk4 Model in YAct.UnknownC4)
                            {
                                TreeNode ENode = AddNodeCSVModelY1(Model, i);
                                foreach (CSVData.Anim Info in Model.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, ENode);
                                }
                                i++;
                                YActNode.Nodes.Add(ENode);
                            }
                            int CamCount = 0;
                            foreach (CSVData.CameraInfo Camera in YAct.CamInfos)
                            {
                                TreeNode CNode = AddNodeCamY1("CAMERA" + CamCount.ToString(), Camera, YActNode);
                                CamCount++;
                                foreach (CSVData.Anim Info in Camera.Info)
                                {
                                    AddNodeAnimY1("Animation", Info, CNode);
                                }
                            }
                            C2.Nodes.Add(YActNode);
                        }
                    }
                }
            }
        }

        private void exportCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                WriteCSVY2 Writer = new WriteCSVY2();
                Writer.WriteCSV(CSVDataY2);
            }
            else
            {
                WriteCSV Writer = new WriteCSV();
                Writer.WriteCSVFile(CSVData);
            }
        }

        private void eFFECTDAMAGEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.HactEvent HE = new HactEvent
            {
                Name = "EFFECT_DAMAGE0",
                ParentID = 1,
                FrameStart = 0,
                FrameEnd = 0,
                Speed = 0,
                BoneNumber = 0,
            };
            TreeNode Node = AddNodeEffect(HE.Name, HE);
            AddEffectToParent(Node, HE);
        }

        private void eFFECTDAMAGEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag is CSVDataY2.CSVEntry Entry)
            {
                int Flag = -1;
                int IndexP = -1;
                CSVDataY2.EFFECT_DAMAGE DMG = new CSVDataY2.EFFECT_DAMAGE
                {
                    Name = "EFFECT_DAMAGE0",
                    DamageVal = 100
                };
                Entry.HactEvents.Add(DMG);
                treeView1.SelectedNode.Tag = Entry;
                TreeNode Node = AddNodeHactEvent(DMG);
                treeView1.SelectedNode.Nodes.Add(Node);
                if (treeView1.SelectedNode.Parent.Text == "Category 1")
                {
                    Flag = 1;
                    IndexP = CSVDataY2.Category1.IndexOf(Entry);
                    CSVDataY2.Category1[IndexP] = Entry;
                }
                else
                {
                    Flag = 2;
                    IndexP = CSVDataY2.Category2.IndexOf(Entry);
                    CSVDataY2.Category2[IndexP] = Entry;
                }

            }


        }


        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag is Effects.IEffect Effect)
            {
                treeView1.SelectedNode.Remove();
            }
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MovePropData.Property Prop = new MovePropData.Property
            {
                Type = 3,
                FrameStart = 0,
                FrameEnd = 0,
                HitBox = 2048
            };
            AddNodeProp("Property", Prop);
            PropData.PropData.Add(Prop);
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag is MovePropData.Property Prop)
            {
                treeView1.SelectedNode.Remove();
                PropData.PropData.RemoveAt(PropData.PropData.IndexOf(Prop));
            }
        }

        private void exportMovePropertyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadMoveProps writer = new ReadMoveProps();
            writer.WriteBep(PropData, Data);
        }

        private void screenShakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.ScreenShakeProp ScrShk = new Effects.ScreenShakeProp
            {
                ParentID = 0,
                FrameStart = 0,
                FrameEnd = 0,
                Speed = 1,
                Intensity = 2,
                Flag = 15
            };
            AddNodeEffect("Screen Shake Effect", ScrShk);
            Data.Effects.Add(ScrShk);
        }

        private void buttonSpamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.ButtonSpam Dmg = new Effects.ButtonSpam
            {
                FrameStart = 0,
                FrameEnd = 0,
                ID = 0,
                ParentID = 0,
                Speed = 1,
                Button = 0,
                Count = 8,
            };
            TreeNode Node = AddNodeEffect("Button Spam Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void normalBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.NormalBranch Dmg = new Effects.NormalBranch
            {
                FrameStart = 0,
                FrameEnd = 0,
                ID = 0,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Normal Branch Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void counterUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.CounterUp Dmg = new Effects.CounterUp
            {
                FrameStart = 0,
                FrameEnd = 0,
                Unknown = 0,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Counter Up Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void counterBranchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.CounterBranch Dmg = new Effects.CounterBranch
            {
                FrameStart = 0,
                FrameEnd = 0,
                Unknown1 = 0,
                Unknown2 = 2,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Counter Branch Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void counterResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.CounterReset Dmg = new Effects.CounterReset
            {
                FrameStart = 0,
                FrameEnd = 0,
                Unknown = 0,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Counter Reset Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void changeFinishStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.ChangeFinishStatus Dmg = new Effects.ChangeFinishStatus
            {
                FrameStart = 0,
                FrameEnd = 0,
                Status = 0,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Change Finish Status Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void deadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.Dead1 Dmg = new Effects.Dead1
            {
                FrameStart = 0,
                FrameEnd = 0,
                ID = 0,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Dead Effect 1", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void dead2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.Dead2 Dmg = new Effects.Dead2
            {
                FrameStart = 0,
                FrameEnd = 0,
                ID = 0,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Dead Effect 2", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void loopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.Loop Dmg = new Effects.Loop
            {
                FrameStart = 0,
                FrameEnd = 0,
                MaxLoopNum = 10,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Loop Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void failConditionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.Finish Dmg = new Effects.Finish
            {
                FrameStart = 0,
                FrameEnd = 0,
                ParentID = 0,
                Speed = 1,
            };
            TreeNode Node = AddNodeEffect("Finish Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }

        private void buttonWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Effects.ButtonTiming Dmg = new Effects.ButtonTiming
            {
                FrameStart = 0,
                FrameEnd = 0,
                ID = 0,
                ParentID = 0,
                Speed = 1,
                Button = 0,
            };
            TreeNode Node = AddNodeEffect("Button Window Effect", Dmg);
            AddEffectToParent(Node, Dmg);
        }
    }
}
