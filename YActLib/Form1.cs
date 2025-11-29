using YActLib.Common;

namespace YActLib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataTable.RowStyles.Clear();
            DataTable.RowCount = 0;
            MainTree.AfterSelect += DrawData;
            GameCB.SelectedIndex = 0;
        }

        private void DrawData(object sender, TreeViewEventArgs e)
        {
            TreeNode selected = e.Node;
            DataTable.SuspendLayout();
            SuspendLayout();
            ClearAll();
            DefaultColumns();
            switch (selected.Tag)
            {
                case Common.YActAnimation:
                    UI.CAnimationPlayDataTable.Draw(this, selected);
                    break;
                case Common.EFFECT_AUTHORING:
                    UI.CEffectAuthoring.Draw(this, selected);
                    break;
                case ogre2.CYActPlayEntity:
                    UI.CPlayDataEntity.Draw(this, selected);
                    break;
                case ogre2.CYActEvent:
                    UI.CYActEvents.Draw(this, selected);
                    break;
                case ogre2.CYActCondition:
                    UI.CYActConditions.Draw(this, selected);
                    break;
                case Common.CParticle:
                    UI.CParticleUI.DrawHeader(this, selected);
                    break;
                case Common.CParticleEmitter:
                    UI.CParticleUI.DrawEmitter(this, selected);
                    break;
                case Common.CParticleElement:
                    UI.CParticleUI.DrawElement(this, selected);
                    break;
            }
            DataTable.ResumeLayout();
            ResumeLayout();
        }

        private void ClearAll()
        {
            DataTable.Controls.Clear();
            DataTable.RowCount = 0;
            DataTable.RowStyles.Clear();
            DataTable.ColumnCount = 0;
            DataTable.ColumnStyles.Clear();
        }

        private void DefaultColumns()
        {
            DataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            DataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            DataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            DataTable.ColumnCount = 3;
        }

        public void CreateColumn()
        {
            DataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            DataTable.ColumnCount++;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddEffect()
        {

        }

        private void yActToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode != null)
            {
                TreeNode Node = MainTree.SelectedNode;
                if (Node.Tag is ogre1.PlayDataY1 || Node.Tag is ogre2.YAct)
                {
                    using (OpenFileDialog OpenFile = new OpenFileDialog())
                    {
                        OpenFile.Filter = "PS2 YAct(*.bin) |*.bin";
                        OpenFile.Title = "Open PS2 YAct";
                        if (OpenFile.ShowDialog() == DialogResult.OK)
                        {
                            if (GameCB.SelectedIndex == 0)
                            {
                                ogre1.CYActReaderY1.ReadYAct(OpenFile.FileName, Node);
                            }
                            else
                            {
                                ogre2.CYActReaderAndWriter.ReadYAct(OpenFile.FileName, Node);
                            }
                        }
                    }
                }
            }
        }

        private void yActPlayDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTree.Nodes.Clear();
            using (OpenFileDialog OpenFile = new OpenFileDialog())
            {
                OpenFile.Filter = "PS2 YActPlayData (*.bin) |*.bin";
                OpenFile.Title = "Open PS2 YActPlayData";
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    ClearAll();
                    if (GameCB.SelectedIndex == 0)
                    {
                        ogre1.CPlayDataReaderY1.ReadYActPlayDataFile(OpenFile.FileName, MainTree);
                    }
                    else
                    {
                        ogre2.CYActPlayReaderAndWriter.ReadCSV(OpenFile.FileName, MainTree);
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void yActToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
                return;
            if (MainTree.SelectedNode.Tag is ogre1.PlayDataY1 || MainTree.SelectedNode.Tag is ogre2.YAct)
            {
                using (SaveFileDialog SaveFile = new SaveFileDialog())
                {
                    SaveFile.Filter = "PS2 YAct(*.bin) |*.bin";
                    SaveFile.Title = "Save PS2 YAct";
                    if (SaveFile.ShowDialog() == DialogResult.OK)
                    {
                        if (GameCB.SelectedIndex == 0)
                        {
                            ogre1.CYActWriterY1 writer = new ogre1.CYActWriterY1();
                            writer.WriteYAct(SaveFile.FileName, MainTree.SelectedNode);
                        }
                        else
                        {
                            ogre2.CYActReaderAndWriter.WriteYAct(SaveFile.FileName, MainTree.SelectedNode);
                        }
                    }
                }
            }
        }

        private void yActPlayDataToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog SaveFile = new SaveFileDialog())
            {
                SaveFile.Filter = "PS2 YActPlayData (*.bin) |*.bin";
                SaveFile.Title = "Save PS2 YActPlayData";
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    if (GameCB.SelectedIndex == 0)
                        ogre1.CPlayDataWriterY1.WriteFile(SaveFile.FileName, MainTree);
                    else
                    {
                        ogre2.CYActPlayReaderAndWriter w = new ogre2.CYActPlayReaderAndWriter();
                        w.WriteCSV(SaveFile.FileName, MainTree);
                    }
                }
            }
        }

        public Label CreateHeader(string Label)
        {
            Label label = new Label();
            label.Anchor = AnchorStyles.Left;
            label.AutoSize = true;
            label.Font = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Point);
            label.Location = new Point(42, 5);
            label.Size = new Size(195, 10);
            label.TabIndex = 0;
            label.Text = Label;

            DataTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            DataTable.RowCount++;
            DataTable.Controls.Add(label, 0, DataTable.RowCount - 1);

            return label;
        }

        public void CreateLabel(string Label)
        {
            Label label = new Label();
            label.Anchor = AnchorStyles.Left;
            label.AutoSize = true;
            label.Text = Label;

            CreateRow();
            DataTable.Controls.Add(label, 0, DataTable.RowCount - 1);
        }

        public void CreateButton(string Label, string ButtonLabel, bool NewRow, int Column, Action OnClick)
        {
            Button button = new Button();
            button.Text = ButtonLabel;
            button.AutoSize = true;
            button.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            button.Click += delegate { OnClick?.Invoke(); };

            if (NewRow)
                CreateLabel(Label);

            DataTable.Controls.Add(button, Column, DataTable.RowCount - 1);
        }

        public void CreateComboBox(string Label, int Default, bool NewRow, int Column, string[] options, Action<int> OnChange)
        {
            ComboBox box = new ComboBox();
            box.AutoSize = true;
            box.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            foreach (string s in options)
                box.Items.Add(s);

            box.SelectedIndexChanged += delegate { OnChange?.Invoke(box.SelectedIndex); };

            box.SelectedIndex = Default;
            if (NewRow)
                CreateLabel(Label);

            DataTable.Controls.Add(box, Column, DataTable.RowCount - 1);
        }

        public void CreateCheckBox(string Label, bool Default, bool NewRow, int Column, Action<CheckBox> OnClick, int Tag = -1)
        {
            CheckBox button = new CheckBox();
            //button.Text = ;
            button.Checked = Default;
            button.Tag = Tag;
            button.AutoSize = true;
            button.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            button.CheckedChanged += delegate { OnClick?.Invoke(button); };

            if (NewRow)
                CreateLabel(Label);

            DataTable.Controls.Add(button, Column, DataTable.RowCount - 1);
        }

        public void CreateInput(string Label, string DefaultValue, bool NewRow, int Column, Action<string> OnEdit, int Max = -1)
        {
            TextBox textbox = new TextBox();
            textbox.Text = DefaultValue;
            textbox.AutoSize = true;
            textbox.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            textbox.TextChanged += delegate { OnEdit?.Invoke(textbox.Text); };

            if (Max != -1)
                textbox.MaxLength = Max;

            if (NewRow)
                CreateLabel(Label);

            DataTable.Controls.Add(textbox, Column, DataTable.RowCount - 1);
        }
        public void CreateRow()
        {
            DataTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            DataTable.RowCount++;
        }

        private void CreateEffect(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode != null)
            {
                TreeNode Node = MainTree.SelectedNode;
                if (Node.Tag is EFFECT_AUTHORING)
                {
                    TreeNode Parent = Node.Parent;
                    Parent.Nodes.Remove(Node);
                }
                else if ((Node.Tag is CYActEntityBase && Node.Tag is not ogre2.CYActPlayEntity) || (Node.Parent.Tag is ogre2.CYActPlayEntity && Node.Text == "Effects"))
                {
                    var Item = sender as ToolStripMenuItem;
                    EFFECT_AUTHORING Effect = new EFFECT_AUTHORING();
                    switch (Item.Text)
                    {
                        case "EFFECT_DAMAGE":
                            Effect = new EFFECT_DAMAGE();
                            break;
                        case "EFFECT_LOOP":
                            Effect = new EFFECT_LOOP();
                            break;
                        case "EFFECT_RENDA":
                            Effect = new EFFECT_RENDA();
                            break;
                        case "EFFECT_TIMING":
                            Effect = new EFFECT_TIMING();
                            break;
                        case "EFFECT_DEAD1":
                            Effect = new EFFECT_DEAD();
                            Effect.TypeID = 16;
                            break;
                        case "EFFECT_DEAD2":
                            Effect = new EFFECT_DEAD();
                            Effect.TypeID = 17;
                            break;
                        case "EFFECT_NORMAL_BRANCH":
                            Effect = new EFFECT_NORMAL_BRANCH();
                            break;
                        case "EFFECT_SOUND":
                            Effect = new EFFECT_SOUND();
                            break;
                        case "EFFECT_PARTICLE":
                            Effect = new EFFECT_PARTICLE_NORMAL();
                            break;
                        case "EFFECT_TRAIL":
                            Effect = new EFFECT_PARTICLE_TRAIL();
                            break;
                        case "EFFECT_SCREEN_FLASH":
                            Effect = new EFFECT_SCREEN_FLASH();
                            break;
                        case "EFFECT_AFTER_IMAGE":
                            Effect = new EFFECT_AFTER_IMAGE();
                            break;
                        case "EFFECT_VIBRATION":
                            Effect = new EFFECT_VIBRATION();
                            break;
                        case "EVENT":
                            Effect = new YACT_EVENT();
                            break;

                    }
                    Effect.Add();
                    Node.Nodes.Add(new TreeNode
                    {
                        Text = Effect.Name,
                        Tag = Effect
                    });
                }
            }
        }
        private void DeleteEffect(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode != null)
            {
                TreeNode Node = MainTree.SelectedNode;
                if (Node.Tag is EFFECT_AUTHORING)
                {
                    TreeNode Parent = Node.Parent;
                    Parent.Nodes.Remove(Node);
                }
            }
        }
        private void CreateAnimation(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
            {
                return;
            }
            TreeNode SelectedNode = MainTree.SelectedNode;
            if ((SelectedNode.Tag is CYActEntityBase && SelectedNode.Tag is not ogre2.CYActPlayEntity) || (SelectedNode.Parent.Tag is ogre2.CYActPlayEntity && SelectedNode.Text == "Animations"))
            {
                SelectedNode.Nodes.Add(new TreeNode
                {
                    Text = "Animation",
                    Tag = new YActAnimation
                    {
                        Unk = 0,
                        FrameStart = 0,
                        FrameEnd = 0,
                        Speed = 1,
                        AnimID = 0,
                        Unknown1 = 0,
                        Unknown3 = 0,
                        Unknown2 = 0
                    }
                });
            }
        }
        private void DeleteAnimation(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
            {
                return;
            }
            TreeNode SelectedNode = MainTree.SelectedNode;
            if (SelectedNode.Tag is YActAnimation)
            {
                TreeNode Parent = SelectedNode.Parent;
                Parent.Nodes.Remove(SelectedNode);
            }
        }

        private void AddEvent(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
                return;

            TreeNode SelectedNode = MainTree.SelectedNode;

            // Make sure it's inside YAct and specifically the "YAct Events" branch
            if (SelectedNode.Parent.Tag is not ogre2.YAct || SelectedNode.Text != "YAct Events")
                return;

            var Button = sender as ToolStripMenuItem;
            ogre2.CYActEvent evt = null;

            switch (Button.Text)
            {
                case "EFFECT_DAMAGE": evt = new ogre2.EFFECT_DAMAGE(); break;
                case "EFFECT_RENDA": evt = new ogre2.EFFECT_RENDA(); break;
                case "EFFECT_TIMING_OK": evt = new ogre2.EFFECT_TIMING_OK(); break;
                case "EFFECT_FINISH_STATUS": evt = new ogre2.EFFECT_FINISH_STATUS(); break;
                case "EFFECT_TIMING_NG": evt = new ogre2.EFFECT_TIMING_NG(); break;
                case "EFFECT_FINISH": evt = new ogre2.EFFECT_FINISH(); break;
                case "EFFECT_DEAD": evt = new ogre2.EFFECT_DEAD(); break;
                case "EFFECT_NORMAL_BRANCH": evt = new ogre2.EFFECT_NORMAL_BRANCH(); break;
                case "HG_USE": evt = new ogre2.HG_USE(); break;
                case "HG_CHK": evt = new ogre2.HG_CHK(); break;
                case "EFFECT_LOOP": evt = new ogre2.EFFECT_LOOP(); break;
                case "EFFECT_YACT_BRANCH": evt = new ogre2.EFFECT_YACT_BRANCH(); break;
                case "EFFECT_ARMS_NAME": evt = new ogre2.EFFECT_ARMS_NAME(); break;
                case "EFFECT_RELEASE_ARMS": evt = new ogre2.EFFECT_RELEASE_ARMS(); break;
                case "EFFECT_CATCH_ARMS": evt = new ogre2.EFFECT_CATCH_ARMS(); break;
                case "EFFECT_LOAD_ARMS": evt = new ogre2.EFFECT_LOAD_ARMS(); break;
            }

            if (evt != null)
            {
                SelectedNode.Nodes.Add(new TreeNode
                {
                    Text = Button.Text,
                    Tag = evt
                });
            }
        }
        private void AddCondition(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
                return;

            TreeNode SelectedNode = MainTree.SelectedNode;

            if (SelectedNode.Parent.Tag is not ogre2.YAct &&
                (SelectedNode.Text != "Conditions 1" &&
                 SelectedNode.Text != "Conditions 2" &&
                 SelectedNode.Text != "Conditions"))
                return;

            var Button = sender as ToolStripMenuItem;
            ogre2.CYActCondition cond = null;

            switch (Button.Text)
            {
                case "Range Check": cond = new ogre2.CRangeCondition(); break;
                case "Relation Check": cond = new ogre2.CRelationCondition(); break;
            }

            if (cond != null)
            {
                SelectedNode.Nodes.Add(new TreeNode
                {
                    Text = Button.Text,
                    Tag = cond
                });
            }
        }
        private void RemoveCondition(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode != null && MainTree.SelectedNode.Tag is ogre2.CYActCondition)
            {
                TreeNode parent = MainTree.SelectedNode.Parent;
                parent.Nodes.Remove(MainTree.SelectedNode);
            }
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
            {
                return;
            }
            TreeNode SelectedNode = MainTree.SelectedNode;
            if (SelectedNode.Parent.Tag is not ogre2.YAct)
                return;

            TreeNode Node = new();

            TreeNode Anims = new TreeNode
            {
                Text = "Animations"
            };

            TreeNode Effects = new TreeNode
            {
                Text = "Effects"
            };

            TreeNode Conditions = new TreeNode
            {
                Text = "Conditions"
            };

            TreeNode Conditions1 = new TreeNode
            {
                Text = "Conditions 1"
            };

            TreeNode Conditions2 = new TreeNode
            {
                Text = "Conditions 2"
            };

            switch (SelectedNode.Text)
            {
                case "Cameras":
                    Node.Text = "CAMERA";
                    Node.Tag = new ogre1.CYActCamera();
                    Node.Nodes.Add(Anims);
                    Node.Nodes.Add(Effects);
                    break;
                case "Characters":
                    Node.Text = "ENTITY";
                    Node.Tag = new ogre2.CYActCharacter();
                    Node.Nodes.Add(Anims);
                    Node.Nodes.Add(Effects);
                    Node.Nodes.Add(Conditions1);
                    Node.Nodes.Add(Conditions2);
                    break;
                case "Objects":
                    Node.Text = "ENTITY";
                    Node.Tag = new ogre2.CYActObject();
                    Node.Nodes.Add(Anims);
                    Node.Nodes.Add(Effects);
                    Node.Nodes.Add(Conditions);
                    break;
                case "Arms":
                    Node.Text = "ENTITY";
                    Node.Tag = new ogre2.CYActArm();
                    Node.Nodes.Add(Anims);
                    Node.Nodes.Add(Effects);
                    break;
                case "ExMotions":
                    Node.Text = "ExMotion";
                    Node.Tag = new ogre1.CYActExMotion();
                    Node.Nodes.Add(Anims);
                    Node.Nodes.Add(Effects);
                    break;
                default:
                    return;
            }
            SelectedNode.Nodes.Add(Node);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
                return;
            if (MainTree.SelectedNode.Tag is ogre2.CYActEvent)
                MainTree.SelectedNode.Parent.Nodes.Remove(MainTree.SelectedNode);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
                return;
            if (MainTree.SelectedNode.Tag is ogre2.CYActPlayEntity || MainTree.SelectedNode.Tag is ogre1.CYActExMotion || MainTree.SelectedNode.Tag is ogre1.CYActCamera)
                MainTree.SelectedNode.Parent.Nodes.Remove(MainTree.SelectedNode);
        }

        private void particleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainTree.Nodes.Clear();
            using (OpenFileDialog OpenFile = new OpenFileDialog())
            {
                OpenFile.Filter = "PS2 Particle (*.ptcl) |*.ptcl";
                OpenFile.Title = "Open PS2 Particle";
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    ClearAll();
                    Common.CParticleReaderAndWriter.ReadParticle(OpenFile.FileName, GameCB.SelectedIndex, MainTree);
                }
            }
        }

        private void particleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
                return;
            if (MainTree.SelectedNode.Tag is Common.CParticle)
            {
                using (SaveFileDialog SaveFile = new SaveFileDialog())
                {
                    SaveFile.Filter = "PS2 Particle(*.ptcl) |*.ptcl";
                    SaveFile.Title = "Save PS2 Particle";
                    if (SaveFile.ShowDialog() == DialogResult.OK)
                    {
                        Common.CParticleReaderAndWriter.WriteParticle(SaveFile.FileName, GameCB.SelectedIndex, MainTree.SelectedNode);
                    }
                }
            }
        }

        private void emitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
                return;
            if (MainTree.SelectedNode.Tag is Common.CParticle)
            {
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

                TreeNode EMNode = new TreeNode()
                {
                    Text = "Emitter",
                    Tag = new CParticleEmitter()
                };

                EMNode.Nodes.Add(PEMNode);
                EMNode.Nodes.Add(SEMNode);
                EMNode.Nodes.Add(C1EMNode);
                EMNode.Nodes.Add(C2EMNode);
                EMNode.Nodes.Add(REMNode);
                EMNode.Nodes.Add(UEMNode);
                EMNode.Nodes.Add(PAEMNode);

                MainTree.SelectedNode.Nodes.Add(EMNode);
            }
        }

        private void elementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
                return;
            if (MainTree.SelectedNode.Parent.Tag is Common.CParticleEmitter)
            {
                CParticleElement Element = new CParticleElement();
                TreeNode ELNode = new TreeNode()
                {
                    Text = "Element",
                    Tag = Element
                };
                MainTree.SelectedNode.Nodes.Add(ELNode);
                switch (MainTree.SelectedNode.Text)
                {
                    case "Position Elements":
                        Element.Type = 0;
                        break;
                    case "Scale Elements":
                        Element.Type = 1;
                        break;
                    case "Color 1 Elements":
                        Element.Type = 2;
                        break;
                    case "Rotation Elements":
                        Element.Type = 3;
                        break;
                    case "UV Elements":
                        Element.Type = 4;
                        break;
                    case "Pattern Elements":
                        Element.Type = 5;
                        break;
                    case "Color 2 Elements":
                        Element.Type = 6;
                        break;
                }
            }
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MainTree.SelectedNode == null)
                return;

            if (MainTree.SelectedNode.Tag is Common.CParticleEmitter || MainTree.SelectedNode.Tag is Common.CParticleElement)
                MainTree.SelectedNode.Parent.Nodes.Remove(MainTree.SelectedNode);
        }
    }
}
