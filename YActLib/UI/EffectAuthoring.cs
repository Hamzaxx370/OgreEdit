using YActLib.Common;

namespace YActLib.UI
{
    internal static class CEffectAuthoring
    {
        public static void Draw(Form1 Main, TreeNode Node)
        {
            Common.EFFECT_AUTHORING Effect = Node.Tag as Common.EFFECT_AUTHORING;
            Label Head = Main.CreateHeader(Effect.Name);

            Main.CreateComboBox("Play Type", ((int)Effect.PlayFlag), true, 1, new string[] {"Normal","OneShot"}, delegate(int i) { Effect.PlayFlag = ((uint)i); });
            Main.CreateInput("Start Frame", Effect.StartFrame.ToString(), true, 1, delegate (string s) { Effect.StartFrame = Utils.TryParseFP(s); });
            Main.CreateInput("End Frame", Effect.EndFrame.ToString(), true, 1, delegate (string s) { Effect.EndFrame = Utils.TryParseFP(s); });
            Main.CreateInput("Speed", Effect.Speed.ToString(), true, 1, delegate (string s) { Effect.Speed = Utils.TryParseFP(s); });
            Main.CreateInput("Bone ID", Effect.BoneID.ToString(), true, 1, delegate (string s) { Effect.BoneID = Utils.TryParse32(s); });

            switch (Effect)
            {
                case EFFECT_DAMAGE Damage:
                    Head.Text = "Damage";
                    Main.CreateInput("Damage Value", Damage.Damage.ToString(), true, 1, delegate (string s) { Damage.Damage = Utils.TryParse32(s); });
                    break;
                case EFFECT_LOOP Loop:
                    Head.Text = "Loop";
                    Main.CreateInput("Max Loops", Loop.MaxLoopNum.ToString(), true, 1, delegate (string s) { Loop.MaxLoopNum = Utils.TryParse32(s); });
                    Main.CreateInput("Flag", Loop.Flag.ToString(), true, 1, delegate (string s) { Loop.Flag = Utils.TryParseS32(s); });
                    break;
                case EFFECT_NORMAL_BRANCH NBranch:
                    Head.Text = "Normal Branch";
                    Main.CreateInput("ID", NBranch.ID.ToString(), true, 1, delegate (string s) { NBranch.ID = Utils.TryParseS32(s); });
                    break;
                case EFFECT_DEAD Dead:
                    Head.Text = "Dead";
                    Main.CreateInput("ID", Dead.ID.ToString(), true, 1, delegate (string s) { Dead.ID = Utils.TryParseS32(s); });
                    break;
                case EFFECT_FINISH_STATUS Status:
                    Head.Text = "Normal Branch";
                    Main.CreateInput("Status", Status.Status.ToString(), true, 1, delegate (string s) { Status.Status = Utils.TryParseS32(s); });
                    break;
                case EFFECT_TIMING Timing:
                    Head.Text = "Button Timing";
                    Main.CreateInput("Button", Timing.Button.ToString(), true, 1, delegate (string s) { Timing.Button = Utils.TryParse32(s); });
                    Main.CreateInput("ID", Timing.ID.ToString(), true, 1, delegate (string s) { Timing.ID = Utils.TryParseS32(s); });
                    break;
                case EFFECT_RENDA Renda:
                    Head.Text = "Button Renda";
                    Main.CreateInput("Button", Renda.Button.ToString(), true, 1, delegate (string s) { Renda.Button = Utils.TryParse32(s); });
                    Main.CreateInput("Count", Renda.Count.ToString(), true, 1, delegate (string s) { Renda.Count = Utils.TryParse32(s); });
                    Main.CreateInput("ID", Renda.ID.ToString(), true, 1, delegate (string s) { Renda.ID = Utils.TryParseS32(s); });
                    break;
                case EFFECT_COUNTER_BRANCH CBranch:
                    Head.Text = "Branch";
                    Main.CreateInput("Unknown 1", CBranch.Unknown1.ToString(), true, 1, delegate (string s) { CBranch.Unknown1 = Utils.TryParseS32(s); });
                    Main.CreateInput("Unknown 2", CBranch.Unknown2.ToString(), true, 1, delegate (string s) { CBranch.Unknown2 = Utils.TryParseS32(s); });
                    break;
                case EFFECT_COUNTER_UP CUp:
                    Head.Text = "Up";
                    Main.CreateInput("Unknown", CUp.Unknown.ToString(), true, 1, delegate (string s) { CUp.Unknown = Utils.TryParseS32(s); });
                    break;
                case EFFECT_COUNTER_RESET CReset:
                    Head.Text = "Reset";
                    Main.CreateInput("Unknown", CReset.Unknown.ToString(), true, 1, delegate (string s) { CReset.Unknown = Utils.TryParseS32(s); });
                    break;
                case EFFECT_SOUND Sound:
                    Head.Text = "Sound";
                    Main.CreateInput("Sound Speed", Sound.SoundSpeed.ToString(), true, 1, delegate (string s) { Sound.SoundSpeed = Utils.TryParseFP(s); });
                    Main.CreateInput("Container ID", Sound.ContainerID.ToString(), true, 1, delegate (string s) { Sound.ContainerID = Utils.TryParse16(s); });
                    Main.CreateInput("Voice ID", Sound.VoiceID.ToString(), true, 1, delegate (string s) { Sound.VoiceID = Utils.TryParse16(s); });
                    break;
                case EFFECT_PARTICLE_TRAIL Trail:
                    Head.Text = "Trail";
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("RGBA 1", Trail.RGBA1[2].ToString(), true, 1, delegate (string s) { Trail.RGBA1[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Trail.RGBA1[1].ToString(), false, 2, delegate (string s) { Trail.RGBA1[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Trail.RGBA1[0].ToString(), false, 3, delegate (string s) { Trail.RGBA1[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Trail.RGBA1[3].ToString(), false, 4, delegate (string s) { Trail.RGBA1[3] = Utils.TryParse8(s); });

                    Main.CreateInput("RGBA 2", Trail.RGBA2[2].ToString(), true, 1, delegate (string s) { Trail.RGBA2[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Trail.RGBA2[1].ToString(), false, 2, delegate (string s) { Trail.RGBA2[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Trail.RGBA2[0].ToString(), false, 3, delegate (string s) { Trail.RGBA2[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Trail.RGBA2[3].ToString(), false, 4, delegate (string s) { Trail.RGBA2[3] = Utils.TryParse8(s); });

                    Main.CreateInput("Param 1", Trail.TrailParam1.ToString(), true, 1, delegate (string s) { Trail.TrailParam1 = Utils.TryParseFP(s); });
                    Main.CreateInput("Param 2", Trail.TrailParam2.ToString(), true, 1, delegate (string s) { Trail.TrailParam2 = Utils.TryParseFP(s); });
                    Main.CreateInput("Param 3", Trail.TrailParam3.ToString(), true, 1, delegate (string s) { Trail.TrailParam3 = Utils.TryParseFP(s); });

                    Main.CreateInput("Condition", Trail.Unknown1.ToString(), true, 1, delegate (string s) { Trail.Unknown1 = Utils.TryParse32(s); });
                    Main.CreateInput("Unknown", Trail.Unknown2.ToString(), true, 1, delegate (string s) { Trail.Unknown2 = Utils.TryParse32(s); });

                    Main.CreateInput("Flags", Trail.Flags[0].ToString(), true, 1, delegate (string s) { Trail.Flags[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Trail.Flags[1].ToString(), false, 2, delegate (string s) { Trail.Flags[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Trail.Flags[2].ToString(), false, 3, delegate (string s) { Trail.Flags[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Trail.Flags[3].ToString(), false, 4, delegate (string s) { Trail.Flags[3] = Utils.TryParse8(s); });

                    break;
                case EFFECT_PARTICLE_NORMAL Particle:
                    Head.Text = "Particle";
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("PTCL ID", Particle.ptclID.ToString(), true, 1, delegate (string s) { Particle.ptclID = Utils.TryParse32(s); });
                    Main.CreateInput("Condition", Particle.Condition.ToString(), true, 1, delegate (string s) { Particle.Condition = Utils.TryParse32(s); });

                    Main.CreateInput("Location", Particle.Location[0].ToString(), true, 1, delegate (string s) { Particle.Location[0] = Utils.TryParseFP(s); });
                    Main.CreateInput("", Particle.Location[1].ToString(), false, 2, delegate (string s) { Particle.Location[1] = Utils.TryParseFP(s); });
                    Main.CreateInput("", Particle.Location[2].ToString(), false, 3, delegate (string s) { Particle.Location[2] = Utils.TryParseFP(s); });

                    Main.CreateInput("Rotation", Particle.Rotation[0].ToString(), true, 1, delegate (string s) { Particle.Rotation[0] = Utils.TryParseFP(s); });
                    Main.CreateInput("", Particle.Rotation[1].ToString(), false, 2, delegate (string s) { Particle.Rotation[1] = Utils.TryParseFP(s); });
                    Main.CreateInput("", Particle.Rotation[2].ToString(), false, 3, delegate (string s) { Particle.Rotation[2] = Utils.TryParseFP(s); });

                    Main.CreateInput("Flags", Particle.Flags[0].ToString(), true, 1, delegate (string s) { Particle.Flags[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Particle.Flags[1].ToString(), false, 2, delegate (string s) { Particle.Flags[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Particle.Flags[2].ToString(), false, 3, delegate (string s) { Particle.Flags[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Particle.Flags[3].ToString(), false, 4, delegate (string s) { Particle.Flags[3] = Utils.TryParse8(s); });

                    break;
                case EFFECT_SCREEN_FLASH ScrF:
                    Head.Text = "Flash";
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("Unknown 1", ScrF.Unknown1.ToString(), true, 1, delegate (string s) { ScrF.Unknown1 = Utils.TryParse32(s); });
                    Main.CreateInput("Unknown 2", ScrF.Unknown2.ToString(), true, 1, delegate (string s) { ScrF.Unknown2 = Utils.TryParseFP(s); });

                    Main.CreateInput("RGBA", ScrF.RGBA[2].ToString(), true, 1, delegate (string s) { ScrF.RGBA[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", ScrF.RGBA[1].ToString(), false, 2, delegate (string s) { ScrF.RGBA[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", ScrF.RGBA[0].ToString(), false, 3, delegate (string s) { ScrF.RGBA[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", ScrF.RGBA[3].ToString(), false, 4, delegate (string s) { ScrF.RGBA[3] = Utils.TryParse8(s); });

                    break;
                case EFFECT_AFTER_IMAGE AImage:
                    Head.Text = "After Image";
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("Unknown 1", AImage.Unknown1.ToString(), true, 1, delegate (string s) { AImage.Unknown1 = Utils.TryParse32(s); });
                    Main.CreateInput("Unknown 2", AImage.Unknown2.ToString(), true, 1, delegate (string s) { AImage.Unknown2 = Utils.TryParseFP(s); });

                    Main.CreateInput("Param 1", AImage.Param1.ToString(), true, 1, delegate (string s) { AImage.Param1 = Utils.TryParseFP(s); });
                    Main.CreateInput("Param 2", AImage.Param2.ToString(), true, 1, delegate (string s) { AImage.Param2 = Utils.TryParseFP(s); });

                    Main.CreateInput("Scale", AImage.Scale.ToString(), true, 1, delegate (string s) { AImage.Scale = Utils.TryParseFP(s); });

                    Main.CreateInput("RGBA", AImage.RGBA[2].ToString(), true, 1, delegate (string s) { AImage.RGBA[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", AImage.RGBA[1].ToString(), false, 2, delegate (string s) { AImage.RGBA[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", AImage.RGBA[0].ToString(), false, 3, delegate (string s) { AImage.RGBA[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", AImage.RGBA[3].ToString(), false, 4, delegate (string s) { AImage.RGBA[3] = Utils.TryParse8(s); });
                    break;
                case EFFECT_VIBRATION Vibration:
                    Head.Text = "Vibration";

                    Main.CreateInput("Rotor 1", Vibration.Vibration1.ToString(), true, 1, delegate (string s) { Vibration.Vibration1 = Utils.TryParse32(s); });
                    Main.CreateInput("Rotor 2", Vibration.Vibration2.ToString(), true, 1, delegate (string s) { Vibration.Vibration2 = Utils.TryParse32(s); });
                    break;
                case YACT_EVENT Event:
                    Head.Text = Event.Name;

                    Main.CreateInput("Name", Event.Name, true, 1, delegate (string s) { Event.Name = s; Node.Text = s; Head.Text = s; });
                    break;
            }
            Main.CreateRow();
        }
    }
}