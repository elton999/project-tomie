using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using UmbrellaToolsKit.EditorEngine.Attributes;
using UmbrellaToolsKit.EditorEngine.Windows.Interfaces;

namespace UmbrellaToolsKit.EditorEngine.Windows
{
    public class GameSettingsWindow : IWindowEditable
    {
        private GameManagement _gameManagement;
        private IEnumerable<Type> _allSettingsData;
        private object _currentObject = null;
        private string _currentPathFile = null;
        private ContentManager _content;
        private string _projectPath => Environment.CurrentDirectory + "/../../../../Project";

        public const string FILE_EXTENSION = ".xml";

        public GameManagement GameManagement => _gameManagement;
        public IEnumerable<Type> AllSettingsData
        {
            get
            {
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                Assembly projectAssembly = Assembly.GetEntryAssembly();
                Type gameSettingsType = typeof(GameSettingsPropertyAttribute);

                List<Type> types = AttributesHelper.GetTypesWithAttribute(currentAssembly, gameSettingsType).ToList();
                types.AddRange(AttributesHelper.GetTypesWithAttribute(projectAssembly, gameSettingsType));

                return types;
            }
        }

        public GameSettingsWindow(GameManagement gameManagement, ContentManager content)
        {
            _gameManagement = gameManagement;
            _allSettingsData = AllSettingsData;
            _content = content;

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
                    _currentObject = GetInstanceByType(type);
            }
        }

        private void ShowSettingProperty()
        {
            if (_currentObject == null) return;
            if (ImGui.Button("Save"))
                SaveFile(_currentPathFile, _currentObject);

            InspectorClass.DrawAllFields(_currentObject);
        }

        private object GetInstanceByType(Type type)
        {
            bool hasPropertyAttribute = type.GetCustomAttributes(typeof(GameSettingsPropertyAttribute), true).Length > 0;
            if (hasPropertyAttribute)
            {
                var propertyAttribute = type.GetCustomAttributesData();
                string nameFile = (string)propertyAttribute[0].ConstructorArguments[0].Value;
                nameFile += FILE_EXTENSION;
                string pathFile = (string)propertyAttribute[0].ConstructorArguments[1].Value;
                pathFile += nameFile;
                pathFile = _projectPath + pathFile;

                _currentPathFile = pathFile;

                if (!File.Exists(pathFile))
                {
                    var instance = Activator.CreateInstance(type);
                    _currentObject = instance;
                    SaveFile(pathFile, instance);
                    return instance;
                }
                else
                {
                    using (XmlReader reader = XmlReader.Create(pathFile))
                    {
                        return Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Deserialize<GameSettingsProperty>(reader, pathFile);
                    }
                }
            }

            return null;
        }

        private static void SaveFile(string pathFile, object instance)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using (XmlWriter writer = XmlWriter.Create(pathFile, settings))
            {
                Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate.IntermediateSerializer.Serialize(writer, instance, null);
            }
        }
#endif
    }
}
