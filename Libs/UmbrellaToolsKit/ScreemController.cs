﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UmbrellaToolsKit
{
    public class ScreenController : GameObject
    {
        public GraphicsDeviceManager graphics;
        public GraphicsAdapter graphicsAdapter;
        public CameraController CameraManagement;
        public Matrix TransformMatrix;
        private RenderTarget2D screen;

        public ScreenController(GraphicsDeviceManager graphics, GraphicsAdapter graphicsAdapter, GraphicsDevice graphicsDevice, int resolution = 0)
        {
            this.graphics = graphics;
            this.graphicsAdapter = graphicsAdapter;
            this.SetResolutions();
            this.Resolution = resolution;

            this.screen = new RenderTarget2D(graphicsDevice, (int)this.Resolutions[0].X, (int)this.Resolutions[0].Y);
        }

        public List<Vector2> Resolutions = new List<Vector2>();
        public int Resolution { get; set; }
        private void SetResolutions()
        {
            this.Resolutions.Add(new Vector2(1280, 720));
            this.Resolutions.Add(new Vector2(this.graphicsAdapter.CurrentDisplayMode.Width, this.graphicsAdapter.CurrentDisplayMode.Height));
        }

        public Vector2 getCurrentResolutionSize { get => this.Resolutions[this.Resolution]; }
        public Vector2 getCenterScreem
        {
            get => new Vector2(this.Resolutions[this.Resolution].X / 2f, this.Resolutions[this.Resolution].Y / 2f);
        }

        public override void Update(float deltaTime)
        {
            float _screemWidth = graphics.PreferredBackBufferWidth;
            float _screemHeight = graphics.PreferredBackBufferHeight;

            // set new resolutions
            if (_screemHeight != (int)this.Resolutions[this.Resolution].Y || _screemWidth != (int)this.Resolutions[this.Resolution].X)
            {
                graphics.PreferredBackBufferHeight = (int)this.Resolutions[this.Resolution].Y;
                graphics.PreferredBackBufferWidth = (int)this.Resolutions[this.Resolution].X;
                graphics.ApplyChanges();
            }

            float _width = this.graphics.GraphicsDevice.Viewport.Width;
            float _height = this.graphics.GraphicsDevice.Viewport.Height;

            this.Position = new Vector2((_width * this.Scale / 2f) - ((float)this.screen.Width / 2f),
                (_height * this.Scale / 2f) - ((float)this.screen.Height / 2f));

            if (CameraManagement != null && Scene != null)
            {
                this.Scale = this.getCurrentResolutionSize.X / Scene.ScreenOffset.X;
                //CameraManagement.ScreemSize = Scene.ScreemOffset;
                CameraManagement.Scale = this.Scale;
                CameraManagement.update(deltaTime);
            }
        }


        public void BeginDraw(GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch, bool cut = false)
        {
            if (CameraManagement != null) TransformMatrix = CameraManagement.TransformMatrix();
            else TransformMatrix = Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(0, 0, 0);

            GraphicsDevice.Clear(Color.Transparent);
            GraphicsDevice.SetRenderTarget(this.screen);
            spriteBatch.Begin(SpriteSortMode.Immediate, null, SamplerState.PointClamp, null, null, null, TransformMatrix);
        }

        public void EndDraw(GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            // render screem
            this.Sprite = (Texture2D)this.screen;
            this.DrawSprite(spriteBatch);
            spriteBatch.End();
        }

    }
}
