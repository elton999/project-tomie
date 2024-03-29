﻿#if !RELEASE
using ImGuiNET;
using MonoGame.ImGui.Standard;
#endif
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolsKit.EditorEngine.Windows.Interfaces;

namespace UmbrellaToolsKit.EditorEngine
{
    public class EditorMain
    {
#if !RELEASE
        private ImGUIRenderer _imGUIRenderer;
#endif
        private BarEdtior _mainBarEditor;
        private EditorArea _editorArea;

        private GameManagement _gameManagement;
        private Game _game;

        //windows
        private IWindowEditable _sceneEditor;
        private IWindowEditable _dialogueEditor;
        private bool _showEditor = false;
        private bool _showEditorKeyUp = true;

        public static event Action OnDrawOverLayer;

        public EditorMain(Game game, GameManagement gameManagement)
        {
            _game = game;
            _gameManagement = gameManagement;
 #if !RELEASE
            _imGUIRenderer = new ImGUIRenderer(game).Initialize().RebuildFontAtlas();
#endif
            _mainBarEditor = new BarEdtior();
            _editorArea = new EditorArea();

            _sceneEditor = new Windows.SceneEditorWindow(_gameManagement);
            _dialogueEditor = new Windows.DialogueEditorWindow(_gameManagement);
        }

        public void Draw(GameTime gameTime)
        {
#if !RELEASE
            if (Keyboard.GetState().IsKeyDown(Keys.F12) && !_showEditorKeyUp)
            {
                _showEditor = !_showEditor;
                _showEditorKeyUp = true;
            }

            if (Keyboard.GetState().IsKeyUp(Keys.F12))
                _showEditorKeyUp = false;
            if (!_showEditor) return;

            _imGUIRenderer.BeginLayout(gameTime);

            var ImGuiIO = ImGui.GetIO();
            ImGuiIO.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

            _mainBarEditor.Draw(gameTime);
            _editorArea.Draw(gameTime);
            _imGUIRenderer.EndLayout();

            OnDrawOverLayer?.Invoke();
#endif
        }
    }
}
