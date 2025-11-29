namespace YActLib
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            yActToolStripMenuItem = new ToolStripMenuItem();
            yActToolStripMenuItem2 = new ToolStripMenuItem();
            yActPlayDataToolStripMenuItem = new ToolStripMenuItem();
            particleToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            yActToolStripMenuItem3 = new ToolStripMenuItem();
            yActPlayDataToolStripMenuItem2 = new ToolStripMenuItem();
            particleToolStripMenuItem1 = new ToolStripMenuItem();
            yActToolStripMenuItem1 = new ToolStripMenuItem();
            effectsToolStripMenuItem = new ToolStripMenuItem();
            EffectDamage = new ToolStripMenuItem();
            EffectLoop = new ToolStripMenuItem();
            EffectNormBranch = new ToolStripMenuItem();
            EffectDead1 = new ToolStripMenuItem();
            EffectDead2 = new ToolStripMenuItem();
            EffectFinishStatus = new ToolStripMenuItem();
            EffectTiming = new ToolStripMenuItem();
            EffectRenda = new ToolStripMenuItem();
            EffectCntrBranch = new ToolStripMenuItem();
            EffectCntrUp = new ToolStripMenuItem();
            EffectCntrReset = new ToolStripMenuItem();
            EffectSound = new ToolStripMenuItem();
            EffectParticle = new ToolStripMenuItem();
            EffectTrail = new ToolStripMenuItem();
            EffectScreenFlash = new ToolStripMenuItem();
            EffectAfterImage = new ToolStripMenuItem();
            EffectVibration = new ToolStripMenuItem();
            EffectEvent = new ToolStripMenuItem();
            RemoveEffect = new ToolStripMenuItem();
            animationsToolStripMenuItem = new ToolStripMenuItem();
            AddAnim = new ToolStripMenuItem();
            RemoveAnim = new ToolStripMenuItem();
            yActPlayDataToolStripMenuItem1 = new ToolStripMenuItem();
            entityToolStripMenuItem = new ToolStripMenuItem();
            addToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            yActEventsToolStripMenuItem = new ToolStripMenuItem();
            EventDamage = new ToolStripMenuItem();
            EventRenda = new ToolStripMenuItem();
            EventTimingOk = new ToolStripMenuItem();
            EventTimingNG = new ToolStripMenuItem();
            EventFinishStatus = new ToolStripMenuItem();
            EventFinish = new ToolStripMenuItem();
            EventDead = new ToolStripMenuItem();
            EventNormalBranch = new ToolStripMenuItem();
            EventYActBranch = new ToolStripMenuItem();
            EventLoop = new ToolStripMenuItem();
            HgChk = new ToolStripMenuItem();
            HgUse = new ToolStripMenuItem();
            EventReleaseArms = new ToolStripMenuItem();
            EventCatchArms = new ToolStripMenuItem();
            EventLoadArms = new ToolStripMenuItem();
            EventArmsName = new ToolStripMenuItem();
            deleteToolStripMenuItem1 = new ToolStripMenuItem();
            conditionsToolStripMenuItem = new ToolStripMenuItem();
            Range = new ToolStripMenuItem();
            Relation = new ToolStripMenuItem();
            removeToolStripMenuItem = new ToolStripMenuItem();
            particleToolStripMenuItem2 = new ToolStripMenuItem();
            emitterToolStripMenuItem = new ToolStripMenuItem();
            elementToolStripMenuItem = new ToolStripMenuItem();
            removeToolStripMenuItem1 = new ToolStripMenuItem();
            MainTree = new TreeView();
            GameCB = new ComboBox();
            DataTable = new TableLayoutPanel();
            MenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // MenuStrip
            // 
            MenuStrip.BackColor = SystemColors.Control;
            MenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, yActToolStripMenuItem1, yActPlayDataToolStripMenuItem1, particleToolStripMenuItem2 });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(800, 24);
            MenuStrip.TabIndex = 0;
            MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { yActToolStripMenuItem, exportToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // yActToolStripMenuItem
            // 
            yActToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { yActToolStripMenuItem2, yActPlayDataToolStripMenuItem, particleToolStripMenuItem });
            yActToolStripMenuItem.Name = "yActToolStripMenuItem";
            yActToolStripMenuItem.Size = new Size(110, 22);
            yActToolStripMenuItem.Text = "Import";
            // 
            // yActToolStripMenuItem2
            // 
            yActToolStripMenuItem2.Name = "yActToolStripMenuItem2";
            yActToolStripMenuItem2.Size = new Size(144, 22);
            yActToolStripMenuItem2.Text = "YAct";
            yActToolStripMenuItem2.Click += yActToolStripMenuItem2_Click;
            // 
            // yActPlayDataToolStripMenuItem
            // 
            yActPlayDataToolStripMenuItem.Name = "yActPlayDataToolStripMenuItem";
            yActPlayDataToolStripMenuItem.Size = new Size(144, 22);
            yActPlayDataToolStripMenuItem.Text = "YActPlayData";
            yActPlayDataToolStripMenuItem.Click += yActPlayDataToolStripMenuItem_Click;
            // 
            // particleToolStripMenuItem
            // 
            particleToolStripMenuItem.Name = "particleToolStripMenuItem";
            particleToolStripMenuItem.Size = new Size(144, 22);
            particleToolStripMenuItem.Text = "Particle";
            particleToolStripMenuItem.Click += particleToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { yActToolStripMenuItem3, yActPlayDataToolStripMenuItem2, particleToolStripMenuItem1 });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            exportToolStripMenuItem.Size = new Size(110, 22);
            exportToolStripMenuItem.Text = "Export";
            // 
            // yActToolStripMenuItem3
            // 
            yActToolStripMenuItem3.Name = "yActToolStripMenuItem3";
            yActToolStripMenuItem3.Size = new Size(144, 22);
            yActToolStripMenuItem3.Text = "YAct";
            yActToolStripMenuItem3.Click += yActToolStripMenuItem3_Click;
            // 
            // yActPlayDataToolStripMenuItem2
            // 
            yActPlayDataToolStripMenuItem2.Name = "yActPlayDataToolStripMenuItem2";
            yActPlayDataToolStripMenuItem2.Size = new Size(144, 22);
            yActPlayDataToolStripMenuItem2.Text = "YActPlayData";
            yActPlayDataToolStripMenuItem2.Click += yActPlayDataToolStripMenuItem2_Click;
            // 
            // particleToolStripMenuItem1
            // 
            particleToolStripMenuItem1.Name = "particleToolStripMenuItem1";
            particleToolStripMenuItem1.Size = new Size(144, 22);
            particleToolStripMenuItem1.Text = "Particle";
            particleToolStripMenuItem1.Click += particleToolStripMenuItem1_Click;
            // 
            // yActToolStripMenuItem1
            // 
            yActToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { effectsToolStripMenuItem, animationsToolStripMenuItem });
            yActToolStripMenuItem1.Name = "yActToolStripMenuItem1";
            yActToolStripMenuItem1.Size = new Size(43, 20);
            yActToolStripMenuItem1.Text = "YAct";
            // 
            // effectsToolStripMenuItem
            // 
            effectsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { EffectDamage, EffectLoop, EffectNormBranch, EffectDead1, EffectDead2, EffectFinishStatus, EffectTiming, EffectRenda, EffectCntrBranch, EffectCntrUp, EffectCntrReset, EffectSound, EffectParticle, EffectTrail, EffectScreenFlash, EffectAfterImage, EffectVibration, EffectEvent, RemoveEffect });
            effectsToolStripMenuItem.Name = "effectsToolStripMenuItem";
            effectsToolStripMenuItem.Size = new Size(135, 22);
            effectsToolStripMenuItem.Text = "Effects";
            // 
            // EffectDamage
            // 
            EffectDamage.Name = "EffectDamage";
            EffectDamage.Size = new Size(223, 22);
            EffectDamage.Text = "EFFECT_DAMAGE";
            EffectDamage.Click += CreateEffect;
            // 
            // EffectLoop
            // 
            EffectLoop.Name = "EffectLoop";
            EffectLoop.Size = new Size(223, 22);
            EffectLoop.Text = "EFFECT_LOOP";
            EffectLoop.Click += CreateEffect;
            // 
            // EffectNormBranch
            // 
            EffectNormBranch.Name = "EffectNormBranch";
            EffectNormBranch.Size = new Size(223, 22);
            EffectNormBranch.Text = "EFFECT_NORMAL_BRANCH";
            EffectNormBranch.Click += CreateEffect;
            // 
            // EffectDead1
            // 
            EffectDead1.Name = "EffectDead1";
            EffectDead1.Size = new Size(223, 22);
            EffectDead1.Text = "EFFECT_DEAD1";
            EffectDead1.Click += CreateEffect;
            // 
            // EffectDead2
            // 
            EffectDead2.Name = "EffectDead2";
            EffectDead2.Size = new Size(223, 22);
            EffectDead2.Text = "EFFECT_DEAD2";
            EffectDead2.Click += CreateEffect;
            // 
            // EffectFinishStatus
            // 
            EffectFinishStatus.Name = "EffectFinishStatus";
            EffectFinishStatus.Size = new Size(223, 22);
            EffectFinishStatus.Text = "EFFECT_FINISH_STATUS";
            EffectFinishStatus.Click += CreateEffect;
            // 
            // EffectTiming
            // 
            EffectTiming.Name = "EffectTiming";
            EffectTiming.Size = new Size(223, 22);
            EffectTiming.Text = "EFFECT_TIMING";
            EffectTiming.Click += CreateEffect;
            // 
            // EffectRenda
            // 
            EffectRenda.Name = "EffectRenda";
            EffectRenda.Size = new Size(223, 22);
            EffectRenda.Text = "EFFECT_RENDA";
            EffectRenda.Click += CreateEffect;
            // 
            // EffectCntrBranch
            // 
            EffectCntrBranch.Name = "EffectCntrBranch";
            EffectCntrBranch.Size = new Size(223, 22);
            EffectCntrBranch.Text = "EFFECT_COUNTER_BRANCH";
            EffectCntrBranch.Click += CreateEffect;
            // 
            // EffectCntrUp
            // 
            EffectCntrUp.Name = "EffectCntrUp";
            EffectCntrUp.Size = new Size(223, 22);
            EffectCntrUp.Text = "EFFECT_COUNTER_UP";
            EffectCntrUp.Click += CreateEffect;
            // 
            // EffectCntrReset
            // 
            EffectCntrReset.Name = "EffectCntrReset";
            EffectCntrReset.Size = new Size(223, 22);
            EffectCntrReset.Text = "EFFECT_COUNTER_RESET";
            EffectCntrReset.Click += CreateEffect;
            // 
            // EffectSound
            // 
            EffectSound.Name = "EffectSound";
            EffectSound.Size = new Size(223, 22);
            EffectSound.Text = "EFFECT_SOUND";
            EffectSound.Click += CreateEffect;
            // 
            // EffectParticle
            // 
            EffectParticle.Name = "EffectParticle";
            EffectParticle.Size = new Size(223, 22);
            EffectParticle.Text = "EFFECT_PARTICLE";
            EffectParticle.Click += CreateEffect;
            // 
            // EffectTrail
            // 
            EffectTrail.Name = "EffectTrail";
            EffectTrail.Size = new Size(223, 22);
            EffectTrail.Text = "EFFECT_TRAIL";
            EffectTrail.Click += CreateEffect;
            // 
            // EffectScreenFlash
            // 
            EffectScreenFlash.Name = "EffectScreenFlash";
            EffectScreenFlash.Size = new Size(223, 22);
            EffectScreenFlash.Text = "EFFECT_SCREEN_FLASH";
            // 
            // EffectAfterImage
            // 
            EffectAfterImage.Name = "EffectAfterImage";
            EffectAfterImage.Size = new Size(223, 22);
            EffectAfterImage.Text = "EFFECT_AFTER_IMAGE";
            EffectAfterImage.Click += CreateEffect;
            // 
            // EffectVibration
            // 
            EffectVibration.Name = "EffectVibration";
            EffectVibration.Size = new Size(223, 22);
            EffectVibration.Text = "EFFECT_VIBRATION";
            EffectVibration.Click += CreateEffect;
            // 
            // EffectEvent
            // 
            EffectEvent.Name = "EffectEvent";
            EffectEvent.Size = new Size(223, 22);
            EffectEvent.Text = "EVENT";
            EffectEvent.Click += CreateEffect;
            // 
            // RemoveEffect
            // 
            RemoveEffect.Name = "RemoveEffect";
            RemoveEffect.Size = new Size(223, 22);
            RemoveEffect.Text = "Delete";
            RemoveEffect.Click += DeleteEffect;
            // 
            // animationsToolStripMenuItem
            // 
            animationsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { AddAnim, RemoveAnim });
            animationsToolStripMenuItem.Name = "animationsToolStripMenuItem";
            animationsToolStripMenuItem.Size = new Size(135, 22);
            animationsToolStripMenuItem.Text = "Animations";
            // 
            // AddAnim
            // 
            AddAnim.Name = "AddAnim";
            AddAnim.Size = new Size(107, 22);
            AddAnim.Text = "Add";
            AddAnim.Click += CreateAnimation;
            // 
            // RemoveAnim
            // 
            RemoveAnim.Name = "RemoveAnim";
            RemoveAnim.Size = new Size(107, 22);
            RemoveAnim.Text = "Delete";
            RemoveAnim.Click += DeleteAnimation;
            // 
            // yActPlayDataToolStripMenuItem1
            // 
            yActPlayDataToolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { entityToolStripMenuItem, yActEventsToolStripMenuItem, conditionsToolStripMenuItem });
            yActPlayDataToolStripMenuItem1.Name = "yActPlayDataToolStripMenuItem1";
            yActPlayDataToolStripMenuItem1.Size = new Size(89, 20);
            yActPlayDataToolStripMenuItem1.Text = "YActPlayData";
            // 
            // entityToolStripMenuItem
            // 
            entityToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { addToolStripMenuItem, deleteToolStripMenuItem });
            entityToolStripMenuItem.Name = "entityToolStripMenuItem";
            entityToolStripMenuItem.Size = new Size(180, 22);
            entityToolStripMenuItem.Text = "Entity";
            // 
            // addToolStripMenuItem
            // 
            addToolStripMenuItem.Name = "addToolStripMenuItem";
            addToolStripMenuItem.Size = new Size(107, 22);
            addToolStripMenuItem.Text = "Add";
            addToolStripMenuItem.Click += addToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.Size = new Size(107, 22);
            deleteToolStripMenuItem.Text = "Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // yActEventsToolStripMenuItem
            // 
            yActEventsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { EventDamage, EventRenda, EventTimingOk, EventTimingNG, EventFinishStatus, EventFinish, EventDead, EventNormalBranch, EventYActBranch, EventLoop, HgChk, HgUse, EventReleaseArms, EventCatchArms, EventLoadArms, EventArmsName, deleteToolStripMenuItem1 });
            yActEventsToolStripMenuItem.Name = "yActEventsToolStripMenuItem";
            yActEventsToolStripMenuItem.Size = new Size(180, 22);
            yActEventsToolStripMenuItem.Text = "YAct Events";
            // 
            // EventDamage
            // 
            EventDamage.Name = "EventDamage";
            EventDamage.Size = new Size(220, 22);
            EventDamage.Text = "EFFECT_DAMAGE";
            EventDamage.Click += AddEvent;
            // 
            // EventRenda
            // 
            EventRenda.Name = "EventRenda";
            EventRenda.Size = new Size(220, 22);
            EventRenda.Text = "EFFECT_RENDA";
            EventRenda.Click += AddEvent;
            // 
            // EventTimingOk
            // 
            EventTimingOk.Name = "EventTimingOk";
            EventTimingOk.Size = new Size(220, 22);
            EventTimingOk.Text = "EFFECT_TIMING_OK";
            EventTimingOk.Click += AddEvent;
            // 
            // EventTimingNG
            // 
            EventTimingNG.Name = "EventTimingNG";
            EventTimingNG.Size = new Size(220, 22);
            EventTimingNG.Text = "EFFECT_TIMING_NG";
            EventTimingNG.Click += AddEvent;
            // 
            // EventFinishStatus
            // 
            EventFinishStatus.Name = "EventFinishStatus";
            EventFinishStatus.Size = new Size(220, 22);
            EventFinishStatus.Text = "EFFECT_FINISH_STATUS";
            EventFinishStatus.Click += AddEvent;
            // 
            // EventFinish
            // 
            EventFinish.Name = "EventFinish";
            EventFinish.Size = new Size(220, 22);
            EventFinish.Text = "EFFECT_FINISH";
            EventFinish.Click += AddEvent;
            // 
            // EventDead
            // 
            EventDead.Name = "EventDead";
            EventDead.Size = new Size(220, 22);
            EventDead.Text = "EFFECT_DEAD";
            EventDead.Click += AddEvent;
            // 
            // EventNormalBranch
            // 
            EventNormalBranch.Name = "EventNormalBranch";
            EventNormalBranch.Size = new Size(220, 22);
            EventNormalBranch.Text = "EFFECT_NORMAL_BRANCH";
            EventNormalBranch.Click += AddEvent;
            // 
            // EventYActBranch
            // 
            EventYActBranch.Name = "EventYActBranch";
            EventYActBranch.Size = new Size(220, 22);
            EventYActBranch.Text = "EFFECT_YACT_BRANCH";
            EventYActBranch.Click += AddEvent;
            // 
            // EventLoop
            // 
            EventLoop.Name = "EventLoop";
            EventLoop.Size = new Size(220, 22);
            EventLoop.Text = "EFFECT_LOOP";
            EventLoop.Click += AddEvent;
            // 
            // HgChk
            // 
            HgChk.Name = "HgChk";
            HgChk.Size = new Size(220, 22);
            HgChk.Text = "HG_CHK";
            HgChk.Click += AddEvent;
            // 
            // HgUse
            // 
            HgUse.Name = "HgUse";
            HgUse.Size = new Size(220, 22);
            HgUse.Text = "HG_USE";
            HgUse.Click += AddEvent;
            // 
            // EventReleaseArms
            // 
            EventReleaseArms.Name = "EventReleaseArms";
            EventReleaseArms.Size = new Size(220, 22);
            EventReleaseArms.Text = "EFFECT_RELEASE_ARMS";
            EventReleaseArms.Click += AddEvent;
            // 
            // EventCatchArms
            // 
            EventCatchArms.Name = "EventCatchArms";
            EventCatchArms.Size = new Size(220, 22);
            EventCatchArms.Text = "EFFECT_CATCH_ARMS";
            EventCatchArms.Click += AddEvent;
            // 
            // EventLoadArms
            // 
            EventLoadArms.Name = "EventLoadArms";
            EventLoadArms.Size = new Size(220, 22);
            EventLoadArms.Text = "EFFECT_LOAD_ARMS";
            EventLoadArms.Click += AddEvent;
            // 
            // EventArmsName
            // 
            EventArmsName.Name = "EventArmsName";
            EventArmsName.Size = new Size(220, 22);
            EventArmsName.Text = "EFFECT_ARMS_NAME";
            EventArmsName.Click += AddEvent;
            // 
            // deleteToolStripMenuItem1
            // 
            deleteToolStripMenuItem1.Name = "deleteToolStripMenuItem1";
            deleteToolStripMenuItem1.Size = new Size(220, 22);
            deleteToolStripMenuItem1.Text = "Delete";
            deleteToolStripMenuItem1.Click += deleteToolStripMenuItem1_Click;
            // 
            // conditionsToolStripMenuItem
            // 
            conditionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { Range, Relation, removeToolStripMenuItem });
            conditionsToolStripMenuItem.Name = "conditionsToolStripMenuItem";
            conditionsToolStripMenuItem.Size = new Size(180, 22);
            conditionsToolStripMenuItem.Text = "Conditions";
            // 
            // Range
            // 
            Range.Name = "Range";
            Range.Size = new Size(180, 22);
            Range.Text = "Range Check";
            Range.Click += AddCondition;
            // 
            // Relation
            // 
            Relation.Name = "Relation";
            Relation.Size = new Size(180, 22);
            Relation.Text = "Relation Check";
            Relation.Click += AddCondition;
            // 
            // removeToolStripMenuItem
            // 
            removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            removeToolStripMenuItem.Size = new Size(180, 22);
            removeToolStripMenuItem.Text = "Delete";
            removeToolStripMenuItem.Click += RemoveCondition;
            // 
            // particleToolStripMenuItem2
            // 
            particleToolStripMenuItem2.DropDownItems.AddRange(new ToolStripItem[] { emitterToolStripMenuItem, elementToolStripMenuItem, removeToolStripMenuItem1 });
            particleToolStripMenuItem2.Name = "particleToolStripMenuItem2";
            particleToolStripMenuItem2.Size = new Size(58, 20);
            particleToolStripMenuItem2.Text = "Particle";
            // 
            // emitterToolStripMenuItem
            // 
            emitterToolStripMenuItem.Name = "emitterToolStripMenuItem";
            emitterToolStripMenuItem.Size = new Size(117, 22);
            emitterToolStripMenuItem.Text = "Emitter";
            emitterToolStripMenuItem.Click += emitterToolStripMenuItem_Click;
            // 
            // elementToolStripMenuItem
            // 
            elementToolStripMenuItem.Name = "elementToolStripMenuItem";
            elementToolStripMenuItem.Size = new Size(117, 22);
            elementToolStripMenuItem.Text = "Element";
            elementToolStripMenuItem.Click += elementToolStripMenuItem_Click;
            // 
            // removeToolStripMenuItem1
            // 
            removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            removeToolStripMenuItem1.Size = new Size(117, 22);
            removeToolStripMenuItem1.Text = "Remove";
            removeToolStripMenuItem1.Click += removeToolStripMenuItem1_Click;
            // 
            // MainTree
            // 
            MainTree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MainTree.Location = new Point(0, 24);
            MainTree.Name = "MainTree";
            MainTree.Size = new Size(236, 426);
            MainTree.TabIndex = 2;
            // 
            // GameCB
            // 
            GameCB.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            GameCB.FormattingEnabled = true;
            GameCB.Items.AddRange(new object[] { "Yakuza 1", "Yakuza 2" });
            GameCB.Location = new Point(667, 420);
            GameCB.Name = "GameCB";
            GameCB.Size = new Size(121, 23);
            GameCB.TabIndex = 3;
            GameCB.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // DataTable
            // 
            DataTable.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataTable.AutoScroll = true;
            DataTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            DataTable.ColumnCount = 3;
            DataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            DataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            DataTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            DataTable.Location = new Point(281, 24);
            DataTable.Name = "DataTable";
            DataTable.RowCount = 1;
            DataTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            DataTable.Size = new Size(507, 390);
            DataTable.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(DataTable);
            Controls.Add(GameCB);
            Controls.Add(MainTree);
            Controls.Add(MenuStrip);
            MainMenuStrip = MenuStrip;
            Name = "Form1";
            Text = "YActLib";
            Load += Form1_Load;
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem yActToolStripMenuItem;
        private ToolStripMenuItem yActToolStripMenuItem1;
        private ToolStripMenuItem yActPlayDataToolStripMenuItem1;
        private ToolStripMenuItem yActToolStripMenuItem2;
        private ToolStripMenuItem yActPlayDataToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem yActToolStripMenuItem3;
        private ToolStripMenuItem yActPlayDataToolStripMenuItem2;
        private ToolStripMenuItem effectsToolStripMenuItem;
        private ToolStripMenuItem animationsToolStripMenuItem;
        private TreeView MainTree;
        private ComboBox GameCB;
        private TableLayoutPanel DataTable;
        private ToolStripMenuItem EffectDamage;
        private ToolStripMenuItem EffectLoop;
        private ToolStripMenuItem EffectNormBranch;
        private ToolStripMenuItem EffectDead1;
        private ToolStripMenuItem EffectDead2;
        private ToolStripMenuItem EffectFinishStatus;
        private ToolStripMenuItem EffectTiming;
        private ToolStripMenuItem EffectRenda;
        private ToolStripMenuItem EffectCntrBranch;
        private ToolStripMenuItem EffectCntrUp;
        private ToolStripMenuItem EffectCntrReset;
        private ToolStripMenuItem EffectSound;
        private ToolStripMenuItem EffectParticle;
        private ToolStripMenuItem EffectTrail;
        private ToolStripMenuItem EffectScreenFlash;
        private ToolStripMenuItem EffectAfterImage;
        private ToolStripMenuItem EffectVibration;
        private ToolStripMenuItem AddAnim;
        private ToolStripMenuItem RemoveAnim;
        private ToolStripMenuItem RemoveEffect;
        private ToolStripMenuItem entityToolStripMenuItem;
        private ToolStripMenuItem addToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem yActEventsToolStripMenuItem;
        private ToolStripMenuItem EventDamage;
        private ToolStripMenuItem EventRenda;
        private ToolStripMenuItem EventTimingOk;
        private ToolStripMenuItem EventTimingNG;
        private ToolStripMenuItem EventFinishStatus;
        private ToolStripMenuItem EventFinish;
        private ToolStripMenuItem EventDead;
        private ToolStripMenuItem EventNormalBranch;
        private ToolStripMenuItem EventYActBranch;
        private ToolStripMenuItem EventLoop;
        private ToolStripMenuItem HgChk;
        private ToolStripMenuItem HgUse;
        private ToolStripMenuItem EventReleaseArms;
        private ToolStripMenuItem EventCatchArms;
        private ToolStripMenuItem EventLoadArms;
        private ToolStripMenuItem EventArmsName;
        private ToolStripMenuItem deleteToolStripMenuItem1;
        private ToolStripMenuItem conditionsToolStripMenuItem;
        private ToolStripMenuItem Range;
        private ToolStripMenuItem Relation;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ToolStripMenuItem EffectEvent;
        private ToolStripMenuItem particleToolStripMenuItem;
        private ToolStripMenuItem particleToolStripMenuItem1;
        private ToolStripMenuItem particleToolStripMenuItem2;
        private ToolStripMenuItem emitterToolStripMenuItem;
        private ToolStripMenuItem elementToolStripMenuItem;
        private ToolStripMenuItem removeToolStripMenuItem1;
    }
}
