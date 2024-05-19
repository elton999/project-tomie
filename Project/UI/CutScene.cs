using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.EditorEngine.Windows.DialogueEditor;
using System.IO;
using System.Collections.Generic;

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

        public List<Frame> Frames => _frames;

        public CutScene(string path) => _path = path;

        public override void Start()
        {
            SetDialogues();
            UpdateFrames();
            base.Start();
        }

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

            } while (_dialogue.GetNodeById(firstNode.NextNode).NextNode >= 0);
            Log("done");
        }

        private void Log(string value)
        {
#if !RELEASE
            UmbrellaToolsKit.EditorEngine.Log.Write("[Node Frame] " + value);
#endif
        }
    }
}
