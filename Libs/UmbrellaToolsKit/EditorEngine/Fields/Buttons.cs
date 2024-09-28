#if !RELEASE
using ImGuiNET;
#endif

namespace UmbrellaToolsKit.EditorEngine.Fields
{
    public class Buttons
    {
        public static bool ColorButton(string text, System.Numerics.Vector4 color)
        {
            bool clicked = false;
#if !RELEASE
            ImGui.PushStyleColor(ImGuiCol.Button, color);
            clicked = ImGui.Button(text);
            ImGui.PopStyleColor();
#endif
            return clicked;
        }

        public static bool RedButton(string text)
        {
#if !RELEASE
            var redColor = new System.Numerics.Vector4(1, 0, 0, 1);
#endif
            return ColorButton(text, redColor);
        }
    }
}
