namespace YActLib.UI 
{
    internal static class CAnimationPlayDataTable
    {
        public static void Draw(Form1 Main, TreeNode Node)
        {
            Common.YActAnimation Anim = Node.Tag as Common.YActAnimation;

            Main.CreateHeader("Animation");

            Main.CreateColumn();
            Main.CreateColumn();

            Main.CreateButton("Animation", "Import", true, 1, delegate
            {
                using (OpenFileDialog OpenFile = new OpenFileDialog())
                {
                    OpenFile.Filter = "PS2 Animation|*.dat;*.omt;*.mtb;*.mtbw";
                    OpenFile.Title = "Open PS2 Animation";
                    if (OpenFile.ShowDialog() == DialogResult.OK)
                    {
                        Anim.Animation = File.ReadAllBytes(OpenFile.FileName);
                    }
                }
            });
            Main.CreateButton("", "Export", false, 2, delegate
            {
                using (SaveFileDialog SaveFile = new SaveFileDialog())
                {
                    SaveFile.Filter = "PS2 Animation|*.dat;*.omt;*.mtb;*.mtbw";
                    SaveFile.Title = "Open PS2 Animation";
                    if (SaveFile.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(SaveFile.FileName, Anim.Animation);
                    }
                }
            });
            Main.CreateButton("", "Delete", false, 3, delegate
            {
                Anim.Animation = null;
            });

            Main.CreateInput("Unknown", Anim.Unk.ToString(), true, 1, delegate (string s) { Anim.Unk = Utils.TryParse32(s); });
            Main.CreateInput("Start Frame", Anim.FrameStart.ToString(), true, 1, delegate (string s) { Anim.FrameStart = Utils.TryParseFP(s); });
            Main.CreateInput("End Frame", Anim.FrameEnd.ToString(), true, 1, delegate (string s) { Anim.FrameEnd = Utils.TryParseFP(s); });
            Main.CreateInput("Speed", Anim.Speed.ToString(), true, 1, delegate (string s) { Anim.Speed = Utils.TryParseFP(s); });
            Main.CreateRow();
        }
    }
}