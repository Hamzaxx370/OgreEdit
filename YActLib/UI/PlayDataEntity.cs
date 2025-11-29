using System;

namespace YActLib.UI
{
    internal static class CPlayDataEntity
    {
        private static string[] HumanConditions = new string[]
        {
            "No Stance",
            "Grabbing Front",
            "Grabbing Back",
            "Grabbing Leg",
            "Grabbed Front",
            "Grabbed Back",
            "Flag6",
            "Spawn",
            "Downed Up",
            "Downed Down",
            "Flag10",
            "Attack",
            "Stance",
            "Dead",
            "Feel The Heat",
            "Feel The Heat",
            "Parry Stunned",
        };

        public static void Draw(Form1 Main, TreeNode Node)
        {
            ogre2.CYActPlayEntity Entity = Node.Tag as ogre2.CYActPlayEntity;

            Label label = Main.CreateHeader("Entity");

            Main.CreateColumn();

            Main.CreateInput("Name", Entity.Name, true, 1, delegate (string s) { Entity.Name = s; Node.Text = s; }, 15);

            switch (Entity)
            {
                case ogre2.CYActCharacter Chara:
                    label.Text = "Character";
                    Main.CreateInput("Arms required", Chara.ArmsID, true, 1, delegate (string s) { Chara.ArmsID = s; }, 15);
                    for (int i = 0; i < 17; i++)
                    {
                        Main.CreateCheckBox(HumanConditions[i], (Chara.StatusConditions & (1 << i)) != 0, true, 1, delegate (CheckBox box) {
                            if (box.Checked)
                            {
                                Chara.StatusConditions = Chara.StatusConditions | (1u << (int)box.Tag);
                            }
                            else
                            {
                                Chara.StatusConditions = Chara.StatusConditions & ~(1u << (int)box.Tag);
                            }
                            }, i);
                    }
                    break;
                case ogre2.CYActObject Obj:
                    label.Text = "Object";
                    Main.CreateInput("Arms ID", Obj.ArmID, true, 1, delegate (string s) { Obj.ArmID = s; }, 15);
                    Main.CreateInput("ExMotion Index", Obj.Unk.ToString(), true, 1, delegate (string s) { Obj.Unk = Utils.TryParseS32(s); });
                    break;
                case ogre2.CYActArm Arm:
                    label.Text = "Arm";
                    Main.CreateInput("Arms Name", Arm.ArmID, true, 1, delegate (string s) { Arm.ArmID = s; }, 15);
                    Main.CreateInput("Parent", Arm.ParentName, true, 1, delegate (string s) { Arm.ParentName = s; }, 15);
                    Main.CreateInput("Flag", Arm.WeaponCategory.ToString(), true, 1, delegate (string s) { Arm.WeaponCategory = Utils.TryParse32(s); });
                    Main.CreateInput("ExMotion Index", Arm.Unk.ToString(), true, 1, delegate (string s) { Arm.Unk = Utils.TryParseS32(s); });
                    break;
            }
            Main.CreateRow();
        }
    }
}