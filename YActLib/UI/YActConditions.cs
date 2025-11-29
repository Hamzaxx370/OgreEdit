using System;

namespace YActLib.UI
{
    internal static class CYActConditions
    {
        public static void Draw(Form1 Main, TreeNode Node)
        {
            ogre2.CYActCondition Condition = Node.Tag as ogre2.CYActCondition;

            Label label = Main.CreateHeader("Condition");

            Main.CreateColumn();

            switch (Condition)
            {
                case ogre2.CRelationCondition Rel:
                    label.Text = "Relation";

                    Main.CreateInput("Angle", Rel.CharacterAngle.ToString(), true, 1, delegate (string s) { Rel.CharacterAngle = Utils.TryParse32(s); });
                    Main.CreateInput("Arc", Rel.CharacterArc.ToString(), true, 1, delegate (string s) { Rel.CharacterArc = Utils.TryParse32(s); });

                    Main.CreateInput("Target", Rel.Target, true, 1, delegate (string s) { Rel.Target = s; }, 15);

                    Main.CreateInput("Angle", Rel.TargetAngle.ToString(), true, 1, delegate (string s) { Rel.TargetAngle = Utils.TryParse32(s); });
                    Main.CreateInput("Arc", Rel.TargetArc.ToString(), true, 1, delegate (string s) { Rel.TargetArc = Utils.TryParse32(s); });
                    break;
                case ogre2.CRangeCondition Rng:
                    label.Text = "Range";

                    Main.CreateComboBox("Range", Rng.Type - 1, true, 1, new string[] { "Maximum", "Minimum" }, delegate(int i) { Rng.Type = i + 1; });
                    Main.CreateComboBox("Value Type", Rng.CheckType, true, 1, new string[] { "Health", "Distance" }, delegate(int i) { Rng.CheckType = i; });
                    Main.CreateInput("Value", Rng.Value.ToString(), true, 1, delegate (string s) { Rng.Value = Utils.TryParseS32(s); });

                    break;
            }
            Main.CreateRow();
        }
    }
}