using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Contra
{
    public abstract class Sprite
    {
        public Vector2 Position;
        public float velocity = 2f;
        public Texture2D Texture;

        public Sprite(Vector2 _position, Texture2D _texture)
        {
            Position = _position;
            Texture = _texture;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

        }
    }
}
