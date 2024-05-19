using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.EditorEngine.Windows.DialogueEditor;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using UmbrellaToolsKit.Input;

namespace Project.UI
{
    public class CutScene : Dialogue
    {
        public struct Frame
        {
            public Texture2D Sprite;
            public string Text;
        }

        private string _path;
        private DialogueFormat _dialogue;
        private List<Frame> _frames;
        private int _currentFrame = -1;
        private bool _isPlaying;

        public List<Frame> Frames => _frames;

        public CutScene(string path) => _path = path;

        public override void Start()
        {
            SetDialogues();
            UpdateFrames();
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            if (_isPlaying && KeyBoardHandler.KeyPressed(Input.INTERACT))
                if (_isAnimationDone)
                    NextFrame();
                else
                    _skip = true;
        }

        public void Play()
        {
            Log("Play");
            NextFrame();
        }

        private void NextFrame()
        {
            Clear();
            _isPlaying = true;
            _skip = false;
            _currentFrame++;
            if (_currentFrame < _frames.Count)
            {
                Clear();
                Say(_frames[_currentFrame].Text);
                Log($"Next Frame {_currentFrame}");
                return;
            }
            if (_currentFrame >= _frames.Count)
            {
                Clear();
                Log("Stopped");
                return;
            }

        }

        #region Settings
        public void SetDialogues()
        {
            using (StreamReader stream = new StreamReader(_path))
            {
                string json = stream.ReadToEnd();
                _dialogue = DialogueFormat.FromJson(json);
                stream.Dispose();
            }
        }

        public void UpdateFrames()
        {
            var firstNode = _dialogue.GetFirstNode();
            _frames = new List<Frame>();

            do
            {
                Log($"next node {firstNode.NextNode}");
                firstNode = _dialogue.GetNodeById(firstNode.NextNode);
                var firstOption = _dialogue.GetNodeById(firstNode.Options[0]);
                var sprite = Content.Load<Texture2D>($"CutScenes/{firstNode.Sprite}");
                string text = firstOption.Content;
                _frames.Add(new Frame() { Sprite = sprite, Text = text });

                Log("node name: " + firstNode.Name);
                Log("text: " + text);
                firstNode = firstOption;
                Log(firstNode.Id.ToString());

            } while (_dialogue.GetNodeById(firstNode.NextNode).Name != "End");
            Log("done");
        }
        #endregion

        #region Drawing
        public override void DrawSprite(SpriteBatch spriteBatch)
        {
            if (IsShowing)
                spriteBatch.Draw(_frames[_currentFrame].Sprite, Vector2.Zero, Color.White);
            base.DrawSprite(spriteBatch);
        }
        #endregion

        #region Log
        private void Log(string value)
        {
#if !RELEASE
            UmbrellaToolsKit.EditorEngine.Log.Write("[Node Frame] " + value);
#endif
        }
        #endregion
    }
}
