#if !RELEASE
using ImGuiNET;
using MonoGame.ImGui.Standard.Extensions;
#endif
using Microsoft.Xna.Framework;

namespace UmbrellaToolsKit.EditorEngine.Fields
{
	public class Field
	{
		public static void DrawVector(string name, ref Vector2 vector)
		{
#if !RELEASE
			if (ImGui.BeginTable($"##{name}", 3))
			{
				ImGui.TableNextColumn();
				ImGui.TextUnformatted(name);
				ImGui.TableNextColumn();
				ImGui.PushStyleColor(ImGuiCol.FrameBg, new System.Numerics.Vector4(1, 0, 0, 0.5f));
				ImGui.InputFloat("x", ref vector.X);
				ImGui.PopStyleColor();
				ImGui.TableNextColumn();
				ImGui.PushStyleColor(ImGuiCol.FrameBg, new System.Numerics.Vector4(0, 1, 0, 0.5f));
				ImGui.InputFloat("y", ref vector.Y);
				ImGui.PopStyleColor();
				ImGui.EndTable();
			}
#endif
		}

		public static void DrawVector(string name, ref Vector3 vector)
		{
#if !RELEASE
			if (ImGui.BeginTable($"##{name}", 4))
			{
				ImGui.TableNextColumn();
				ImGui.TextUnformatted(name);
				ImGui.TableNextColumn();
				ImGui.PushStyleColor(ImGuiCol.FrameBg, new System.Numerics.Vector4(1, 0, 0, 0.5f));
				ImGui.InputFloat("x", ref vector.X);
				ImGui.PopStyleColor();
				ImGui.TableNextColumn();
				ImGui.PushStyleColor(ImGuiCol.FrameBg, new System.Numerics.Vector4(0, 1, 0, 0.5f));
				ImGui.InputFloat("y", ref vector.Y);
				ImGui.PopStyleColor();
				ImGui.TableNextColumn();
				ImGui.PushStyleColor(ImGuiCol.FrameBg, new System.Numerics.Vector4(0, 0, 1, 0.5f));
				ImGui.InputFloat("z", ref vector.Z);
				ImGui.PopStyleColor();
				ImGui.EndTable();
			}
#endif
		}

		public static void DrawFloat(string name, ref float value)
		{
#if !RELEASE
			TableFormatBegin(name);
			ImGui.InputFloat("", ref value);
			TableFormatEnd();
#endif
		}

		public static void DrawInt(string name, ref int value)
		{
#if !RELEASE
			ImGui.InputInt(name, ref value);
#endif
		}
		public static void DrawString(string name, ref string value)
		{
#if !RELEASE
			TableFormatBegin(name);
			if (value == null) value = "";
			ImGui.InputText("", ref value, 255);
			TableFormatEnd();
#endif
		}

		public static void DrawLongText(string name, ref string value)
		{
#if !RELEASE
			TableFormatBegin(name, 1);
			if (value == null) value = "";
			ImGui.InputTextMultiline("", ref value, 500, (Vector2.One * 500).ToNumericVector2(), ImGuiInputTextFlags.EnterReturnsTrue);
			TableFormatEnd();
#endif
		}

#if !RELEASE
		public static void DrawBoolean(string name, ref bool value) => ImGui.Checkbox(name, ref value);
#endif

		public static void TableFormatBegin(string name, int columns = 2)
		{
#if !RELEASE
			ImGui.BeginTable($"##{name}", columns);
			ImGui.TableNextColumn();
			ImGui.TextUnformatted(name);
			ImGui.TableNextColumn();
#endif
		}

		public static void TableFormatEnd()
		{
#if !RELEASE
			ImGui.EndTable();
#endif
		}
	}
}
