using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Lavatest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tileset;
        Texture2D arrows;
        GameMap gameMap = GameMap.getInstance();
        const int PreferredWidth = 1920;
        const int PreferredHeight = 1080;
        int TargetWidth;
        int TargetHeight;
        Matrix Scale;
        long tick = 0;
        EventQueue queue = EventQueue.getInstance();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.PreferredBackBufferWidth = PreferredWidth;
            graphics.PreferredBackBufferHeight = PreferredHeight;
            Content.RootDirectory = "Content";
            TargetWidth = gameMap.getDimensionSize(1) * 55 * gameMap.getDimensionSize(0);
            TargetHeight = gameMap.getDimensionSize(2) * 55;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f);
            //this.IsFixedTimeStep = false;
            //graphics.SynchronizeWithVerticalRetrace = false;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.Initialize();
            float scaleX = (float)PreferredWidth / (float)TargetWidth;
            float scaleY = (float)PreferredHeight / (float)TargetHeight;
            Scale = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));
            int lifespan = 20;
            int origin = 19;
            int tickOffset = 3;

            //queue.enqueue(new LavaSpellEffect(new Vector3Int(0, 6, 2), DirectionEnum.s, lifespan, origin), tickOffset); tickOffset = tickOffset + lifespan + origin + 2;
            //queue.enqueue(new LavaSpellEffect(new Vector3Int(0, 18, 18), DirectionEnum.n, lifespan, origin), tickOffset); tickOffset = tickOffset + lifespan + origin + 2;
            //queue.enqueue(new LavaSpellEffect(new Vector3Int(0, 6, 2), DirectionEnum.se, lifespan, origin), tickOffset); tickOffset = tickOffset + lifespan + origin + 2;
            queue.enqueue(new LavaSpellEffect(new Vector3Int(0, 18, 18), DirectionEnum.ne, lifespan, origin), tickOffset); tickOffset = tickOffset + lifespan + origin + 2;
            //queue.enqueue(new LavaSpellEffect(new Vector3Int(0, 18, 18), DirectionEnum.se, lifespan, origin), tickOffset); tickOffset = tickOffset + lifespan + origin + 2;

        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tileset = Content.Load<Texture2D>("tileset");
            arrows = Content.Load<Texture2D>("arrows");
            //background = Content.Load<Texture2D>("background");
            // TODO: use this.Content to load your game content here
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
            if((long)gameTime.TotalGameTime.TotalSeconds > tick)
            {
                ++tick;
                queue.nextTick();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Scale);
            //spriteBatch.Draw(tileset, new Rectangle(0, 0, 800, 480), Color.White);
            for (int i = 0; i < gameMap.getDimensionSize(0); i++)
                for (int k = 0; k < gameMap.getDimensionSize(1); k++)
                    for (int l = 0; l < gameMap.getDimensionSize(2); l++)
                    {
                        int x = k * 55 + i * gameMap.getDimensionSize(1) * 55;
                        int y = l * 55;
                        spriteBatch.Draw(tileset, new Rectangle(x, y, 55, 55), gameMap.getRect(i, k, l), Color.White);
                        if(gameMap.getDirection(i,k,l)!=DirectionEnum.none)
                        {
                            spriteBatch.Draw(arrows, new Rectangle(x, y, 55, 55), gameMap.getDirectionRect(i, k, l), Color.White);
                        }
                    }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
