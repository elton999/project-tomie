using System;
using System.Collections.Generic;
using System.Reflection;
using ImGuiNET;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine.Windows.Interfaces;

namespace UmbrellaToolsKit.EditorEngine.Windows
{
    public class GameSettingsWindow : IWindowEditable
    {
        private GameManagement _gameManagement;
        private IEnumerable<Type> _allSettingsData;
        private object _currentObject = null;

        public GameManagement GameManagement => _gameManagement;
        public IEnumerable<Type> AllSettingsData
        {
            get
            {
                Assembly myAssembly = Assembly.GetExecutingAssembly();
                Type gameSettingsType = typeof(GameSettingsPropertyAttribute);
                return AttributesHelper.GetTypesWithAttribute(myAssembly, gameSettingsType);
            }
        }

        public GameSettingsWindow(GameManagement gameManagement)
        {
            _gameManagement = gameManagement;
            _allSettingsData = AllSettingsData;

            BarEdtior.OnSwitchEditorWindow += RemoveAsMainWindow;
            BarEdtior.OnOpenGameSettingsEditor += SetAsMainWindow;
        }

        public void SetAsMainWindow()
        {
#if !RELEASE
            EditorArea.OnDrawWindow += ShowWindow;
#endif
        }

        public void RemoveAsMainWindow()
        {
#if !RELEASE
            EditorArea.OnDrawWindow -= ShowWindow;
#endif
        }

        public void ShowWindow(GameTime gameTime)
        {
#if !RELEASE
            uint leftID = ImGui.GetID("MainLeft");
            uint rightID = ImGui.GetID("MainRight");

            var dockSize = new System.Numerics.Vector2(0, 0);

            ImGui.BeginChild("left", new System.Numerics.Vector2(ImGui.GetMainViewport().Size.X * 0.2f, 0));
            ImGui.SetWindowFontScale(1.2f);
            ImGui.DockSpace(leftID, dockSize);
            ImGui.EndChild();
            ImGui.SameLine();

            ImGui.BeginChild("right", new System.Numerics.Vector2(ImGui.GetMainViewport().Size.X * 0.8f, 0));
            ImGui.SetWindowFontScale(1.2f);
            ImGui.DockSpace(rightID, dockSize);
            ImGui.EndChild();
            ImGui.SameLine();

            ImGui.SetNextWindowDockID(rightID, ImGuiCond.Once);
            ImGui.Begin("Game Settings Editor");
            ImGui.SetWindowFontScale(1.2f);
            ShowSettingProperty();
            ImGui.End();

            ImGui.SetNextWindowDockID(leftID, ImGuiCond.Once);
            ImGui.Begin("All Game Settings Data");
            ImGui.SetWindowFontScale(1.2f);
            ShowSettingsList();
            ImGui.End();
#endif
        }

#if !RELEASE
        private void ShowSettingsList()
        {
            foreach (var type in _allSettingsData)
            {
                if (ImGui.Selectable(type.Name))
                    _currentObject = Activator.CreateInstance(type);
            }
        }

        private void ShowSettingProperty()
        {
            if (_currentObject == null) return;

            InspectorClass.DrawAllFields(_currentObject);
        }
#endif
    }
}
