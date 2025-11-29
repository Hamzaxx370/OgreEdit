using System;

namespace YActLib.UI
{
    internal static class CYActEvents
    {
        public static void Draw(Form1 Main, TreeNode Node)
        {
            ogre2.CYActEvent Event = Node.Tag as ogre2.CYActEvent;

            Label label = Main.CreateHeader("Event");

            Main.CreateColumn();

            Main.CreateInput("Name", Event.Name, true, 1, delegate (string s) { Event.Name = s; Node.Text = s; });

            switch (Event)
            {
                case ogre2.EFFECT_DAMAGE Dmg:
                    label.Text = "Damage";
                    Main.CreateInput("Damage", Dmg.DamageVal.ToString(), true, 1, delegate (string s) { Dmg.DamageVal = Utils.TryParse32(s); });
                    break;
                case ogre2.EFFECT_RENDA Renda:
                    label.Text = "Renda";

                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("ID", Renda.ID.ToString(), true, 1, delegate (string s) { Renda.ID = Utils.TryParseS32(s); });
                    Main.CreateInput("Button", Renda.Button.ToString(), true, 1, delegate (string s) { Renda.Button = Utils.TryParse32(s); });
                    Main.CreateInput("Count", Renda.Count.ToString(), true, 1, delegate (string s) { Renda.Count = Utils.TryParse32(s); });

                    Main.CreateInput("IDs", Renda.IDs[0].ToString(), true, 1, delegate (string s) { Renda.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Renda.IDs[1].ToString(), false, 2, delegate (string s) { Renda.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Renda.IDs[2].ToString(), false, 3, delegate (string s) { Renda.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Renda.IDs[3].ToString(), false, 4, delegate (string s) { Renda.IDs[3] = Utils.TryParse8(s); });
                    break;
                case ogre2.EFFECT_TIMING_OK Timing:
                    label.Text = "Timing OK";
                    Main.CreateInput("ID", Timing.ID.ToString(), true, 1, delegate (string s) { Timing.ID = Utils.TryParseS32(s); });
                    Main.CreateInput("Button", Timing.Button.ToString(), true, 1, delegate (string s) { Timing.Button = Utils.TryParse32(s); });

                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("IDs", Timing.IDs[0].ToString(), true, 1, delegate (string s) { Timing.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Timing.IDs[1].ToString(), false, 2, delegate (string s) { Timing.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Timing.IDs[2].ToString(), false, 3, delegate (string s) { Timing.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Timing.IDs[3].ToString(), false, 4, delegate (string s) { Timing.IDs[3] = Utils.TryParse8(s); });
                    break;
                case ogre2.EFFECT_FINISH_STATUS Status:
                    label.Text = "Status";
                    Main.CreateInput("Status", Status.Status.ToString(), true, 1, delegate (string s) { Status.Status = Utils.TryParse32(s); });

                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("IDs", Status.IDs[0].ToString(), true, 1, delegate (string s) { Status.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Status.IDs[1].ToString(), false, 2, delegate (string s) { Status.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Status.IDs[2].ToString(), false, 3, delegate (string s) { Status.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Status.IDs[3].ToString(), false, 4, delegate (string s) { Status.IDs[3] = Utils.TryParse8(s); });
                    break;
                case ogre2.EFFECT_TIMING_NG Timing:
                    label.Text = "Timing NG";
                    Main.CreateInput("ID", Timing.ID.ToString(), true, 1, delegate (string s) { Timing.ID = Utils.TryParseS32(s); });
                    Main.CreateInput("Button", Timing.Button.ToString(), true, 1, delegate (string s) { Timing.Button = Utils.TryParse32(s); });

                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("IDs", Timing.IDs[0].ToString(), true, 1, delegate (string s) { Timing.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Timing.IDs[1].ToString(), false, 2, delegate (string s) { Timing.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Timing.IDs[2].ToString(), false, 3, delegate (string s) { Timing.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Timing.IDs[3].ToString(), false, 4, delegate (string s) { Timing.IDs[3] = Utils.TryParse8(s); });
                    break;
                case ogre2.EFFECT_DEAD Dead:
                    label.Text = "Dead";
                    Main.CreateInput("ID", Dead.ID.ToString(), true, 1, delegate (string s) { Dead.ID = Utils.TryParseS32(s); });
                    break;
                case ogre2.EFFECT_NORMAL_BRANCH NBranch:
                    label.Text = "Branch";

                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("IDs", NBranch.IDs[0].ToString(), true, 1, delegate (string s) { NBranch.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", NBranch.IDs[1].ToString(), false, 2, delegate (string s) { NBranch.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", NBranch.IDs[2].ToString(), false, 3, delegate (string s) { NBranch.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", NBranch.IDs[3].ToString(), false, 4, delegate (string s) { NBranch.IDs[3] = Utils.TryParse8(s); });
                    break;
                case ogre2.HG_USE Use:
                    label.Text = "Heat Use";

                    Main.CreateInput("Heat loss", Use.HeatLoss.ToString(), true, 1, delegate (string s) { Use.HeatLoss = Utils.TryParse32(s); });

                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("IDs", Use.IDs[0].ToString(), true, 1, delegate (string s) { Use.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Use.IDs[1].ToString(), false, 2, delegate (string s) { Use.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Use.IDs[2].ToString(), false, 3, delegate (string s) { Use.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Use.IDs[3].ToString(), false, 4, delegate (string s) { Use.IDs[3] = Utils.TryParse8(s); });
                    break;
                case ogre2.HG_CHK Use:
                    label.Text = "Heat Chk";

                    Main.CreateInput("ID", Use.ID.ToString(), true, 1, delegate (string s) { Use.ID = Utils.TryParseS32(s); });
                    Main.CreateInput("Unknown", Use.Unknown.ToString(), true, 1, delegate (string s) { Use.Unknown = Utils.TryParse32(s); });
                    break;
                case ogre2.EFFECT_LOOP Loop:
                    label.Text = "Loop";

                    Main.CreateInput("Max", Loop.MaxLoopNum.ToString(), true, 1, delegate (string s) { Loop.MaxLoopNum = Utils.TryParse32(s); });

                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("IDs", Loop.IDs[0].ToString(), true, 1, delegate (string s) { Loop.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[1].ToString(), false, 2, delegate (string s) { Loop.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[2].ToString(), false, 3, delegate (string s) { Loop.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[3].ToString(), false, 4, delegate (string s) { Loop.IDs[3] = Utils.TryParse8(s); });
                    break;
                case ogre2.EFFECT_FINISH Loop:
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("IDs", Loop.IDs[0].ToString(), true, 1, delegate (string s) { Loop.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[1].ToString(), false, 2, delegate (string s) { Loop.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[2].ToString(), false, 3, delegate (string s) { Loop.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[3].ToString(), false, 4, delegate (string s) { Loop.IDs[3] = Utils.TryParse8(s); });
                    break;
                case ogre2.EFFECT_YACT_BRANCH Loop:
                    label.Text = "Branch";

                    Main.CreateInput("YAct", Loop.YAct.ToString(), true, 1, delegate (string s) { Loop.YAct = s; }, 15);

                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("IDs", Loop.IDs[0].ToString(), true, 1, delegate (string s) { Loop.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[1].ToString(), false, 2, delegate (string s) { Loop.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[2].ToString(), false, 3, delegate (string s) { Loop.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[3].ToString(), false, 4, delegate (string s) { Loop.IDs[3] = Utils.TryParse8(s); });
                    break;
                case ogre2.EFFECT_CATCH_ARMS Loop:
                    label.Text = "Branch";

                    Main.CreateInput("Unknown", Loop.ArmsID.ToString(), true, 1, delegate (string s) { Loop.ArmsID = Utils.TryParseS32(s); });
                    break;
                case ogre2.EFFECT_LOAD_ARMS Loop:
                    label.Text = "Branch";

                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();
                    Main.CreateColumn();

                    Main.CreateInput("IDs", Loop.IDs[0].ToString(), true, 1, delegate (string s) { Loop.IDs[0] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[1].ToString(), false, 2, delegate (string s) { Loop.IDs[1] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[2].ToString(), false, 3, delegate (string s) { Loop.IDs[2] = Utils.TryParse8(s); });
                    Main.CreateInput("", Loop.IDs[3].ToString(), false, 4, delegate (string s) { Loop.IDs[3] = Utils.TryParse8(s); });
                    break;
            }
            Main.CreateRow();
        }
    }
}