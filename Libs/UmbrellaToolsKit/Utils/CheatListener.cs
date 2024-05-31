using System;
using System.Collections.Generic;
using UmbrellaToolsKit.Input;
using UmbrellaToolsKit.EditorEngine;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace UmbrellaToolsKit.Utils
{
    public class CheatListener : GameObject
    {
        private CheatListener _instance;
        private List<Tuple<Keys, Action>> _cheatList = new List<Tuple<Keys, Action>>();

        public override void Start() => _instance = _instance == null ? this : _instance;

        public override void Update(GameTime gameTime)
        {
            foreach (var cheat in _cheatList)
                if (KeyBoardHandler.KeyPressed(cheat.Item1))
                    Execute(cheat);
        }

        public static void Execute(Tuple<Keys, Action> cheat)
        {
            Log.Write($"Executing cheat {cheat.Item2.Method.Name}");
            cheat.Item2?.Invoke();
        }

        public void AddCheat(Keys key, Action action = null)
        {
            KeyBoardHandler.AddInput(key);
            _cheatList.Add(new Tuple<Keys, Action>(key, action));
        }

        public void RemoveCheat(Keys key)
        {
            foreach (var cheat in _cheatList)
            {
                if (cheat.Item1 == key)
                {
                    _cheatList.Remove(cheat);
                    return;
                }
            }
        }

        public override void OnDestroy() => _cheatList.Clear();

        public override void Dispose()
        {
            _cheatList.Clear();
            _instance = null;
            base.Dispose();
        }
    }
}