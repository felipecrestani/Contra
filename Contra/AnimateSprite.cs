using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Contra
{
    public class AnimatedSprite : Sprite
    {
        public Input Input { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int currentFrame;
        public int totalFrames;
        private int currentUpdate;
        private int UpdatesPerFrame;
        public bool isActive = true;
        public Direction Direction;

        public AnimatedSprite(Texture2D _texture, Vector2 _position, int rows, int columns, int updatesPerFrame)
            : base(_position, _texture)
        {
            Texture = _texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            Position = _position;
            UpdatesPerFrame = updatesPerFrame;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
            {
                Position.X -= velocity;
                Direction = Direction.Left;
            }
            if (Keyboard.GetState().IsKeyDown(Input.Right))
            {
                Position.X += velocity;
                Direction = Direction.Right;
            }

            currentUpdate++;
            if (currentUpdate == UpdatesPerFrame)
            {
                currentUpdate = 0;
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            if (Direction == Direction.Left)
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, (float)Math.PI, new Vector2(width, height), SpriteEffects.FlipVertically, 0);
            else 
                spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}