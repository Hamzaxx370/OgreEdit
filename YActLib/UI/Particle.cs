using System;
using System.Reflection;
using YActLib.Common;

namespace YActLib.UI
{
    internal static class CParticleUI
    {
        public static void DrawHeader(Form1 Main, TreeNode Node)
        {
            Common.CParticle Particle = Node.Tag as Common.CParticle;
            Main.CreateHeader(Particle.Name);

            Main.CreateColumn();
            Main.CreateColumn();

            Main.CreateInput("Name", Particle.Name, true, 1, delegate (string s) { Particle.Name = s; Node.Text = s; }, 12);
            Main.CreateInput("ID", Particle.ID.ToString(), true, 1, delegate (string s) { Particle.ID = Utils.TryParseS32(s); }, 15);
            Main.CreateRow();
        }
        public static void DrawEmitter(Form1 Main, TreeNode Node)
        {
            Common.CParticleEmitter Emitter = Node.Tag as Common.CParticleEmitter;
            Main.CreateHeader("Emitter");
            Main.CreateColumn();
            Main.CreateColumn();

            Main.CreateHeader("Particle Param");
            Main.CreateRow();

            Main.CreateComboBox("Status", ((int)Emitter.IsEnabled), true, 1, new string[] { "Disabled", "Enabled" }, delegate (int i) { Emitter.IsEnabled = ((int)i); });
            Main.CreateInput("Generate Min", Emitter.GenerateMin.ToString(), true, 1, delegate (string s) { Emitter.GenerateMin = Utils.TryParseS32(s); }, 15);
            Main.CreateInput("Generate Max", Emitter.GenerateMax.ToString(), true, 1, delegate (string s) { Emitter.GenerateMax = Utils.TryParseS32(s); }, 15);
            Main.CreateComboBox("Emit Shape", ((int)Emitter.IsSphere), true, 1, new string[] { "2D Circle", "3D Sphere" }, delegate (int i) { Emitter.IsSphere = ((int)i); });
            Main.CreateComboBox("Use Surface Normals", ((int)Emitter.UseSurfaceNormals), true, 1, new string[] { "Disabled", "Enabled" }, delegate (int i) { Emitter.UseSurfaceNormals = ((int)i); });
            Main.CreateInput("Transform Flag", Emitter.TransformFlag.ToString(), true, 1, delegate (string s) { Emitter.TransformFlag = Utils.TryParseS32(s); }, 15);
            Main.CreateInput("Emit Radius Min", Emitter.RadiusMin.ToString(), true, 1, delegate (string s) { Emitter.RadiusMin = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Emit Radius Max", Emitter.RadiusMax.ToString(), true, 1, delegate (string s) { Emitter.RadiusMax = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Angle Distribution", Emitter.Angle.ToString(), true, 1, delegate (string s) { Emitter.Angle = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Rot Y Variation", Emitter.Rot1.ToString(), true, 1, delegate (string s) { Emitter.Rot1 = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Rot Z Variation", Emitter.Rot2.ToString(), true, 1, delegate (string s) { Emitter.Rot2 = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale Y Min", Emitter.ScaleYMin.ToString(), true, 1, delegate (string s) { Emitter.ScaleYMin = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale Y Max", Emitter.ScaleYMax.ToString(), true, 1, delegate (string s) { Emitter.ScaleYMax = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale XZ Min", Emitter.ScaleXZMin.ToString(), true, 1, delegate (string s) { Emitter.ScaleXZMin = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale XZ Max", Emitter.ScaleXZMax.ToString(), true, 1, delegate (string s) { Emitter.ScaleXZMax = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Dir Angle Min", Emitter.DirAngleMin.ToString(), true, 1, delegate (string s) { Emitter.DirAngleMin = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Dir Angle Max", Emitter.DirAngleMax.ToString(), true, 1, delegate (string s) { Emitter.DirAngleMax = Utils.TryParseFP(s); }, 15);
            Main.CreateHeader("Emitter Param");
            Main.CreateRow();

            Main.CreateInput("Delay Min", Emitter.DelayMin.ToString(), true, 1, delegate (string s) { Emitter.DelayMin = Utils.TryParseS16(s); }, 15);
            Main.CreateInput("Delay Max", Emitter.DelayMax.ToString(), true, 1, delegate (string s) { Emitter.DelayMax = Utils.TryParseS16(s); }, 15);

            Main.CreateInput("Emission Min", Emitter.CycleLenMin.ToString(), true, 1, delegate (string s) { Emitter.CycleLenMin = Utils.TryParseS16(s); }, 15);
            Main.CreateInput("Emission Max", Emitter.CycleLenMax.ToString(), true, 1, delegate (string s) { Emitter.CycleLenMax = Utils.TryParseS16(s); }, 15);

            Main.CreateInput("Life Time", Emitter.LifeTime.ToString(), true, 1, delegate (string s) { Emitter.LifeTime = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Inverse Time Scale", Emitter.InverseTimeScale.ToString(), true, 1, delegate (string s) { Emitter.InverseTimeScale = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Unknown Angle", Emitter.UnkAngle.ToString(), true, 1, delegate (string s) { Emitter.UnkAngle = Utils.TryParseFP(s); }, 15);

            Main.CreateInput("Emission Pool Size", Emitter.PoolSize.ToString(), true, 1, delegate (string s) { Emitter.PoolSize = Utils.TryParseS32(s); }, 15);

            Main.CreateComboBox("Vertex Type", ((int)Emitter.VertexType), true, 1, new string[] { "Unknown", "Billboard", "Mesh 1", "Mesh 2" }, delegate (int i) { Emitter.VertexType = ((int)i); });
            Main.CreateInput("Render State", Emitter.RenderState.ToString(), true, 1, delegate (string s) { Emitter.RenderState = Utils.TryParseS32(s); }, 15);
            Main.CreateInput("Model ID", Emitter.ModelID.ToString(), true, 1, delegate (string s) { Emitter.ModelID = Utils.TryParseS16(s); }, 15);
            Main.CreateInput("Texture ID", Emitter.TexID.ToString(), true, 1, delegate (string s) { Emitter.TexID = Utils.TryParseS16(s); }, 15);

            Main.CreateInput("Time Scale", Emitter.TimeScale.ToString(), true, 1, delegate (string s) { Emitter.TimeScale = Utils.TryParseFP(s); }, 15);

            Main.CreateHeader("Vector Param");
            Main.CreateRow();
            Main.CreateInput("Position Change", Emitter.VectorParameters.Base[0].ToString(), true, 1, delegate (string s) { Emitter.VectorParameters.Base[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Vertical Change", Emitter.VectorParameters.Base[1].ToString(), true, 1, delegate (string s) { Emitter.VectorParameters.Base[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Position Change?", Emitter.VectorParameters.Base[2].ToString(), true, 1, delegate (string s) { Emitter.VectorParameters.Base[2] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Change Angle", Emitter.VectorParameters.Angle.ToString(), true, 1, delegate (string s) { Emitter.VectorParameters.Angle = Utils.TryParseFP(s); }, 15);

            Main.CreateInput("Unk Param 1", Emitter.VectorParameters.Change[0].ToString(), true, 1, delegate (string s) { Emitter.VectorParameters.Change[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Unk Param 2", Emitter.VectorParameters.Change[1].ToString(), true, 1, delegate (string s) { Emitter.VectorParameters.Change[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Change Multiplier", Emitter.VectorParameters.Change[2].ToString(), true, 1, delegate (string s) { Emitter.VectorParameters.Change[2] = Utils.TryParseFP(s); }, 15);

            Main.CreateHeader("Vertex Param");
            Main.CreateRow();
            DrawVertexParam(Main, Emitter.VertexParameters);
            Main.CreateRow();
        }

        public static void DrawVertexParam(Form1 Main, CVertexParam VertexParameters)
        {
            Main.CreateHeader("RGBA Change");
            Main.CreateInput("R", VertexParameters.RGBAChange[0].ToString(), true, 1, delegate (string s) { VertexParameters.RGBAChange[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("G", VertexParameters.RGBAChange[1].ToString(), true, 1, delegate (string s) { VertexParameters.RGBAChange[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("B", VertexParameters.RGBAChange[2].ToString(), true, 1, delegate (string s) { VertexParameters.RGBAChange[2] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("A", VertexParameters.RGBAChange[3].ToString(), true, 1, delegate (string s) { VertexParameters.RGBAChange[3] = Utils.TryParseFP(s); }, 15);

            Main.CreateHeader("RGBA");
            Main.CreateInput("R", VertexParameters.RGBABase[0].ToString(), true, 1, delegate (string s) { VertexParameters.RGBABase[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("G", VertexParameters.RGBABase[1].ToString(), true, 1, delegate (string s) { VertexParameters.RGBABase[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("B", VertexParameters.RGBABase[2].ToString(), true, 1, delegate (string s) { VertexParameters.RGBABase[2] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("A", VertexParameters.RGBABase[3].ToString(), true, 1, delegate (string s) { VertexParameters.RGBABase[3] = Utils.TryParseFP(s); }, 15);

            Main.CreateHeader("Scale");
            Main.CreateInput("Scale Flag", VertexParameters.ScaleFlag.ToString(), true, 1, delegate (string s) { VertexParameters.ScaleFlag = Utils.TryParseS32(s); }, 15);
            Main.CreateInput("Scale Change W", VertexParameters.ScaleChange[0].ToString(), true, 1, delegate (string s) { VertexParameters.ScaleChange[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale Change X", VertexParameters.ScaleChange[1].ToString(), true, 1, delegate (string s) { VertexParameters.ScaleChange[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale Change Y", VertexParameters.ScaleChange[2].ToString(), true, 1, delegate (string s) { VertexParameters.ScaleChange[2] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale Change Z", VertexParameters.ScaleChange[3].ToString(), true, 1, delegate (string s) { VertexParameters.ScaleChange[3] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale Base W", VertexParameters.Scale[0].ToString(), true, 1, delegate (string s) { VertexParameters.Scale[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale Base X", VertexParameters.Scale[1].ToString(), true, 1, delegate (string s) { VertexParameters.Scale[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale Base Y", VertexParameters.Scale[2].ToString(), true, 1, delegate (string s) { VertexParameters.Scale[2] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Scale Base Z", VertexParameters.Scale[3].ToString(), true, 1, delegate (string s) { VertexParameters.Scale[3] = Utils.TryParseFP(s); }, 15);

            Main.CreateHeader("Rotation");
            Main.CreateInput("Rotation Base Range X", VertexParameters.RotationVelRange[0].ToString(), true, 1, delegate (string s) { VertexParameters.RotationVelRange[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Rotation Base Range Y", VertexParameters.RotationVelRange[1].ToString(), true, 1, delegate (string s) { VertexParameters.RotationVelRange[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Rotation Base Range Z", VertexParameters.RotationVelRange[2].ToString(), true, 1, delegate (string s) { VertexParameters.RotationVelRange[2] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Rotation Base X", VertexParameters.RotationBase[0].ToString(), true, 1, delegate (string s) { VertexParameters.RotationBase[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Rotation Base Y", VertexParameters.RotationBase[1].ToString(), true, 1, delegate (string s) { VertexParameters.RotationBase[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Rotation Base Z", VertexParameters.RotationBase[2].ToString(), true, 1, delegate (string s) { VertexParameters.RotationBase[2] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Ang Vel Range X", VertexParameters.AngVelRange[0].ToString(), true, 1, delegate (string s) { VertexParameters.AngVelRange[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Ang Vel Range Y", VertexParameters.AngVelRange[1].ToString(), true, 1, delegate (string s) { VertexParameters.AngVelRange[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Ang Vel Range Z", VertexParameters.AngVelRange[2].ToString(), true, 1, delegate (string s) { VertexParameters.AngVelRange[2] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Ang Vel Base X", VertexParameters.AngVelBase[0].ToString(), true, 1, delegate (string s) { VertexParameters.AngVelBase[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Ang Vel Base Y", VertexParameters.AngVelBase[1].ToString(), true, 1, delegate (string s) { VertexParameters.AngVelBase[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Ang Vel Base Z", VertexParameters.AngVelBase[2].ToString(), true, 1, delegate (string s) { VertexParameters.AngVelBase[2] = Utils.TryParseFP(s); }, 15);

            Main.CreateHeader("UV");
            Main.CreateInput("UV Flag", VertexParameters.UVFlag.ToString(), true, 1, delegate (string s) { VertexParameters.UVFlag = Utils.TryParseS32(s); }, 15);
            Main.CreateInput("UV Range X", VertexParameters.UVRange[0].ToString(), true, 1, delegate (string s) { VertexParameters.UVRange[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("UV Range Y", VertexParameters.UVRange[1].ToString(), true, 1, delegate (string s) { VertexParameters.UVRange[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("UV Base X", VertexParameters.UVBase[0].ToString(), true, 1, delegate (string s) { VertexParameters.UVBase[0] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("UV Base Y", VertexParameters.UVBase[1].ToString(), true, 1, delegate (string s) { VertexParameters.UVBase[1] = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Columns", VertexParameters.Columns.ToString(), true, 1, delegate (string s) { VertexParameters.Columns = Utils.TryParse8(s); }, 15);
            Main.CreateInput("Rows", VertexParameters.Rows.ToString(), true, 1, delegate (string s) { VertexParameters.Rows = Utils.TryParse8(s); }, 15);
            Main.CreateInput("Start Frame", VertexParameters.StartFrame.ToString(), true, 1, delegate (string s) { VertexParameters.StartFrame = Utils.TryParse8(s); }, 15);
            Main.CreateInput("End Frame", VertexParameters.EndFrame.ToString(), true, 1, delegate (string s) { VertexParameters.EndFrame = Utils.TryParse8(s); }, 15);
            Main.CreateInput("Width", VertexParameters.Width.ToString(), true, 1, delegate (string s) { VertexParameters.Width = Utils.TryParseFP(s); }, 15);
            Main.CreateInput("Height", VertexParameters.Height.ToString(), true, 1, delegate (string s) { VertexParameters.Height = Utils.TryParseFP(s); }, 15);
        }

        public static void DrawElement(Form1 Main, TreeNode Node)
        {
            CParticleElement Element = Node.Tag as CParticleElement;
            Main.CreateHeader("Particle Element");

            Main.CreateColumn();
            Main.CreateColumn();

            Main.CreateInput("Effect Type", Element.EffectType.ToString(), true, 1, delegate (string s) { Element.EffectType = Utils.TryParseS32(s); }, 15);
            Main.CreateInput("Inherit Type", Element.InheritType.ToString(), true, 1, delegate (string s) { Element.InheritType = Utils.TryParseS32(s); }, 15);
            Main.CreateInput("Time Scale", Element.TimeScale.ToString(), true, 1, delegate (string s) { Element.TimeScale = Utils.TryParseFP(s); }, 15);

            switch (Element.Type)
            {
                case 0:
                    for (int i = 0; i < 12; i++)
                    {
                        int index = i;
                        Main.CreateInput($"Float {index}", Element.MEFFELEMENT[index].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[index] = Utils.TryParseFP(s); }, 15);
                    }
                    break;
                case 1:
                    Main.CreateHeader("Base Scale");
                    Main.CreateInput("W", Element.MEFFELEMENT[0].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[0] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("X", Element.MEFFELEMENT[1].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[1] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("Y", Element.MEFFELEMENT[2].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[2] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("Z", Element.MEFFELEMENT[3].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[3] = Utils.TryParseFP(s); }, 15);

                    Main.CreateHeader("Position Change");
                    Main.CreateInput("W", Element.MEFFELEMENT[4].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[4] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("X", Element.MEFFELEMENT[5].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[5] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("Y", Element.MEFFELEMENT[6].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[6] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("Z", Element.MEFFELEMENT[7].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[7] = Utils.TryParseFP(s); }, 15);

                    Main.CreateHeader("Scale Change");
                    Main.CreateInput("W", Element.MEFFELEMENT[8].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[8] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("X", Element.MEFFELEMENT[9].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[9] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("Y", Element.MEFFELEMENT[10].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[10] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("Z", Element.MEFFELEMENT[11].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[11] = Utils.TryParseFP(s); }, 15);
                    break;
                case 6:
                case 2:
                    Main.CreateHeader("Base Color");
                    Main.CreateInput("R", Element.MEFFELEMENT[0].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[0] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("G", Element.MEFFELEMENT[1].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[1] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("B", Element.MEFFELEMENT[2].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[2] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("A", Element.MEFFELEMENT[3].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[3] = Utils.TryParseFP(s); }, 15);

                    Main.CreateHeader("Color Change 1");
                    Main.CreateInput("R", Element.MEFFELEMENT[4].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[4] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("G", Element.MEFFELEMENT[5].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[5] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("B", Element.MEFFELEMENT[6].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[6] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("A", Element.MEFFELEMENT[7].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[7] = Utils.TryParseFP(s); }, 15);

                    Main.CreateHeader("Color Change 2");
                    Main.CreateInput("R", Element.MEFFELEMENT[8].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[8] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("G", Element.MEFFELEMENT[9].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[9] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("B", Element.MEFFELEMENT[10].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[10] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("A", Element.MEFFELEMENT[11].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[11] = Utils.TryParseFP(s); }, 15);
                    break;
                case 4:
                    Main.CreateHeader("UV Scroll");
                    Main.CreateInput("X", Element.MEFFELEMENT[4].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[4] = Utils.TryParseFP(s); }, 15);
                    Main.CreateInput("Y", Element.MEFFELEMENT[5].ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[5] = Utils.TryParseFP(s); }, 15);
                    break;
                case 3:
                    Main.CreateHeader("Base Rotation");
                    Main.CreateInput("X", ((Element.MEFFELEMENT[0] / 65536.0f) * 360.0f).ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[0] = Utils.TryParseFP(s) * 65536.0f / 360.0f; }, 15);
                    Main.CreateInput("Y", ((Element.MEFFELEMENT[1] / 65536.0f) * 360.0f).ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[1] = Utils.TryParseFP(s) * 65536.0f / 360.0f; }, 15);
                    Main.CreateInput("Z", ((Element.MEFFELEMENT[2] / 65536.0f) * 360.0f).ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[2] = Utils.TryParseFP(s) * 65536.0f / 360.0f; }, 15);
                    
                    Main.CreateHeader("Rotation Change");
                    Main.CreateInput("X", ((Element.MEFFELEMENT[4] / 65536.0f) * 360.0f).ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[4] = Utils.TryParseFP(s) * 65536.0f / 360.0f; }, 15);
                    Main.CreateInput("Y", ((Element.MEFFELEMENT[5] / 65536.0f) * 360.0f).ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[5] = Utils.TryParseFP(s) * 65536.0f / 360.0f; }, 15);
                    Main.CreateInput("Z", ((Element.MEFFELEMENT[6] / 65536.0f) * 360.0f).ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[6] = Utils.TryParseFP(s) * 65536.0f / 360.0f; }, 15);
                    
                    Main.CreateHeader("Rotation Change?");
                    Main.CreateInput("X", ((Element.MEFFELEMENT[8] / 65536.0f) * 360.0f).ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[8] = Utils.TryParseFP(s) * 65536.0f / 360.0f; }, 15);
                    Main.CreateInput("Y", ((Element.MEFFELEMENT[9] / 65536.0f) * 360.0f).ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[9] = Utils.TryParseFP(s) * 65536.0f / 360.0f; }, 15);
                    Main.CreateInput("Z", ((Element.MEFFELEMENT[10] / 65536.0f) * 360.0f).ToString(), true, 1, delegate (string s) { Element.MEFFELEMENT[10] = Utils.TryParseFP(s) * 65536.0f / 360.0f; }, 15);
                    break;
            }
            Main.CreateRow();
        }
    }
}