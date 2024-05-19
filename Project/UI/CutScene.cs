using UmbrellaToolsKit;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.EditorEngine.Windows.DialogueEditor;
using System.IO;

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

        public CutScene(string path) => _path = path;

        public override void Start()
        {
            base.Start();
            using (StreamReader stream = new StreamReader(_path))
            {
                string json = stream.ReadToEnd();
                _dialogue = DialogueFormat.FromJson(json);
            }
        }
    }
}
