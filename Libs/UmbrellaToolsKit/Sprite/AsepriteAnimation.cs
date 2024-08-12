using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace UmbrellaToolsKit.Sprite
{
    public class AsepriteAnimation
    {
        public Rectangle Body { get; set; }
        public AsepriteDefinitions AsepriteDefinitions { get; set; }

        public AsepriteAnimation(AsepriteDefinitions AsepriteDefinitions)
        {
            this.AsepriteDefinitions = AsepriteDefinitions;
        }

        #region play animation
        public int frame;
        public int frameCurrent;
        private float frameTimerCount;
        private List<float> maxFrame = new List<float>();
        private AnimationDirection direction;
        public enum AnimationDirection { FORWARD, LOOP, PING_PONG }
        private bool checkedFirstframe;

        private int a_from;
        private int a_to;
        public string tag;

        public bool lastFrame
        {
            get
            {
                if (maxFrame != null)
                {
                    if (maxFrame[maxFrame.Count - 1] < frameTimerCount && tag != null) return true;
                }
                return false;
            }
        }

        public void Restart() => tag = "";

        public void Play(float deltaTime, string tag, AnimationDirection aDirection = AnimationDirection.FORWARD)
        {
            if (tag != this.tag)
            {
                int i = 0;
                while (i < AsepriteDefinitions.Tags.Count)
                {
                    if (tag == AsepriteDefinitions.Tags[i].Name)
                    {
                        a_from = AsepriteDefinitions.Tags[i].from;
                        a_to = AsepriteDefinitions.Tags[i].to;
                        tag = AsepriteDefinitions.Tags[i].Name;
                        direction = aDirection;
                        frameCurrent = 0;
                        frameTimerCount = 0;
                        maxFrame = new List<float>();
                        checkedFirstframe = false;
                        break;
                    }
                    i++;
                }

                i = 0;
                while (i + a_from <= a_to)
                {
                    int i_frame = a_from + i;
                    int last_frame = i - 1;

                    if (i > 0) maxFrame.Add((AsepriteDefinitions.Duration[i_frame]) + maxFrame[last_frame]);
                    else maxFrame.Add(AsepriteDefinitions.Duration[i_frame]);

                    i++;
                }
            }

            frameTimerCount += deltaTime;

            if (frameTimerCount >= maxFrame[frameCurrent] || (frameCurrent == 0 && !checkedFirstframe))
            {
                if (a_to > (frameCurrent + a_from) && checkedFirstframe) frameCurrent++;
                else if (direction == AnimationDirection.LOOP)
                {
                    frameCurrent = 0;
                    frameTimerCount = 0;
                    checkedFirstframe = false;
                }

                frame = (int)(frameCurrent + a_from);
                Body = AsepriteDefinitions.Bodys[frame];

                checkedFirstframe = true;
            }

        }
        #endregion
    }
}
