using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Lidgren.Network;
using System.Linq;

namespace Contra
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Sprite> sprites;

        Dictionary<long, Vector2> positions = new Dictionary<long, Vector2>();
        NetClient client;
        AnimatedSprite player;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            
            Content.RootDirectory = "Content";

            //Client
            NetPeerConfiguration config = new NetPeerConfiguration("xnaapp");
            config.EnableMessageType(NetIncomingMessageType.DiscoveryResponse);

            client = new NetClient(config);
            client.Start();            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            sprites = new List<Sprite>();
            client.DiscoverLocalPeers(14242);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            CreatePlayer();

            //CreatePlayer();

            //var input2 = new Input { Up = Keys.Up, Down = Keys.Down, Left = Keys.NumPad4, Right = Keys.NumPad6 };
            //AnimatedSprite player2 = new AnimatedSprite(Content.Load<Texture2D>("player"), new Vector2(600, 200), 1, 9, 4);
            //player2.Input = input2;
            //player2.velocity = 4f;
            //sprites.Add(player2);

            // TODO: use this.Content to load your game content here
        }

        private void CreatePlayer()
        {
            var input = new Input { Up = Keys.Up, Down = Keys.Down, Left = Keys.Left, Right = Keys.Right };
            player = new AnimatedSprite(Content.Load<Texture2D>("player"), new Vector2(200, 200), 1, 9, 4);
            player.Input = input;
            player.velocity = 4f;
            //sprites.Add(player);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // read messages
            NetIncomingMessage msg;
            while ((msg = client.ReadMessage()) != null)
            {
                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.DiscoveryResponse:
                        // just connect to first server discovered
                        client.Connect(msg.SenderEndPoint);
                        break;
                    case NetIncomingMessageType.Data:
                        // server sent a position update
                        long who = msg.ReadInt64();
                        float x = msg.ReadInt32();
                        float y = msg.ReadInt32();
                        positions[who] = new Vector2(x, y);

                        if (x != 0 && y != 0) ;
                            player.Position = new Vector2(x, y);

                        break;
                }
            }

            foreach (Sprite sprite in sprites)
            {
                sprite.Update(gameTime);                
            }

            player.Update(gameTime);

            NetOutgoingMessage om = client.CreateMessage();
            om.Write((int)player.Position.X); // very inefficient to send a full Int32 (4 bytes) but we'll use this for simplicity
            om.Write((int)player.Position.Y);
            client.SendMessage(om, NetDeliveryMethod.Unreliable);
           
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Sprite sprite in sprites)
            {
                sprite.Draw(spriteBatch,sprite.Position);
            }

            player.Draw(spriteBatch,player.Position);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
