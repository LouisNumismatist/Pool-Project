using System;
using System.Diagnostics; //for Stopwatch
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MonoGamePool1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public static Game1 instance;

        public static Texture2D YellowBall;
        public static Texture2D RedBall;
        public Texture2D EightBall;
        public Texture2D CueBall;
        public Texture2D TableInner;
        public Texture2D TableOuter;
        public Texture2D PoolCueTexture;
        public Texture2D SightLineTexture;
        public static Texture2D SmallWhiteSquare;
        public static SpriteFont font;
        public static SpriteFont TextBoxFont;

        public static bool HittingCueBall;
        public static int BorderWidth = 40;
        public static int ScreenHeight = 548;
        public static int ScreenWidth = 1046;
        public static int BallDiam = 22;
        public List<Ball> BallsList = new List<Ball>();
        public List<Pocket> PocketList = new List<Pocket>();
        public List<Rectangle> OuterPockets = new List<Rectangle>();
        public List<Ball> Graveyard = new List<Ball>();
        public List<Button> ButtonList = new List<Button>();
        public List<DiagonalLine> DiagonalLines = new List<DiagonalLine>();
        public static DiagonalLine PoolCue;
        public static DiagonalLine SightLine;
        Dictionary<string, Texture2D> BallsDict = new Dictionary<string, Texture2D> { };
        public static Button SaveButton;
        public static Button LoadButton;
        public static Button ResetButton;
        public static Button PauseButton;
        public static Button DebugButton;
        public static TextBox TypeBox;

        //NEWTONSOFT
        public Game1()
        {
            instance = this;

//            ProcessStartInfo info = new ProcessStartInfo("www.youtube.com");
//            Process.Start(info);

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = ScreenHeight + 100;
            graphics.PreferredBackBufferWidth = ScreenWidth + 200;
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
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

            base.Initialize();
            Init.InitialiseBalls(ref BallsList, CueBall, YellowBall, RedBall, EightBall);
            Init.InitialiseOuterPockets(ref OuterPockets);
            Init.InitialisePockets(ref PocketList);
            Init.InitialiseDiagonalLines(ref DiagonalLines, TableOuter);
            //DiagonalLine PoolCue = Init.InitialisePoolCue(PoolCueTexture);
            PoolCue = Init.InitialisePoolCue(PoolCueTexture);
            //Console.WriteLine("Pool Cue: {0}, {1}", PoolCue.Start, PoolCue.End);
            //Console.WriteLine("Pool Cue Thickness: {0}", PoolCue.Thickness);
            //DiagonalLine SightLine = Init.InitialiseSightLine(TableOuter);
            SightLine = Init.InitialiseSightLine(TableOuter);
            /**Console.WriteLine("Ball15: {0}", BallsList[15].Center);
            Console.WriteLine("A, {0}", MathHelper.ToDegrees((float)Physics.TanAngle(new Vector2(0, 1), Vector2.Zero)));
            Console.WriteLine("B, {0}", MathHelper.ToDegrees((float)Physics.TanAngle(new Vector2(1, 1), Vector2.Zero)));
            Console.WriteLine("C, {0}", MathHelper.ToDegrees((float)Physics.TanAngle(new Vector2(1, 0), Vector2.Zero)));
            Console.WriteLine("D, {0}", MathHelper.ToDegrees((float)Physics.TanAngle(new Vector2(1, -1), Vector2.Zero)));
            Console.WriteLine("E, {0}", MathHelper.ToDegrees((float)Physics.TanAngle(new Vector2(0, -1), Vector2.Zero)));
            Console.WriteLine("F, {0}", MathHelper.ToDegrees((float)Physics.TanAngle(new Vector2(-1, -1), Vector2.Zero)));
            Console.WriteLine("G, {0}", MathHelper.ToDegrees((float)Physics.TanAngle(new Vector2(-1, 0), Vector2.Zero)));
            Console.WriteLine("H, {0}", MathHelper.ToDegrees((float)Physics.TanAngle(new Vector2(-1, 1), Vector2.Zero)));**/
            //Console.WriteLine(BallsList[0]);
            string tempString = FileSaving.ObjectToString(BallsList[0]);
            //Console.WriteLine(tempString);
            Ball tempBall = FileSaving.StringToObject(tempString, BallsDict);
            //Console.WriteLine(tempBall);
            SaveButton = Init.InitialiseButton(new Vector2(ScreenWidth + 50, 30), "SAVE", TableOuter, font);
            LoadButton = Init.InitialiseButton(new Vector2(ScreenWidth + 50, 60), "LOAD", TableOuter, font);
            ResetButton = Init.InitialiseButton(new Vector2(ScreenWidth + 50, 90), "RESET", TableOuter, font);
            PauseButton = Init.InitialiseButton(new Vector2(ScreenWidth + 50, 120), "PAUSE", TableOuter, font);
            DebugButton = Init.InitialiseButton(new Vector2(ScreenWidth + 50, 150), "DEBUG", TableOuter, font);
            TypeBox = Init.InitialiseTextBox(new Vector2(ScreenWidth + 25, 510), 150, TableOuter, TextBoxFont);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            Tuple<Color, string> TextureTuple = Debug.ChangeTextures();

            spriteBatch = new SpriteBatch(GraphicsDevice);
            TableInner = Content.Load<Texture2D>(TextureTuple.Item2);

            SmallWhiteSquare = new Texture2D(GraphicsDevice, 1, 1);
            SmallWhiteSquare.SetData(new[] { Color.White});

            TableOuter = Content.Load<Texture2D>("White Box");
            PoolCueTexture = Content.Load<Texture2D>("Brown Border");
            SightLineTexture = Content.Load<Texture2D>("Brown Border");
            font = Content.Load<SpriteFont>("EndGameFont");
            TextBoxFont = Content.Load<SpriteFont>("EqualSpacedText");

            if (Debug.color == "mario")
            {
                CueBall = Content.Load<Texture2D>("Bowser_Shell");
                RedBall = Content.Load<Texture2D>("Red_Shell");
                YellowBall = Content.Load<Texture2D>("Green_Shell");
                EightBall = Content.Load<Texture2D>("Blue_Shell");
                BallsDict.Add("Bowser_Shell", CueBall);
                BallsDict.Add("Red_Shell", RedBall);
                BallsDict.Add("Green_Shell", YellowBall);
                BallsDict.Add("Blue_Shell", EightBall);
            }
            else
            {
                /**CueBall = Content.Load<Texture2D>("Sneddon");
                RedBall = Content.Load<Texture2D>("Sneddon");
                YellowBall = Content.Load<Texture2D>("Sneddon");
                EightBall = Content.Load<Texture2D>("Sneddon");**/
                CueBall = Content.Load<Texture2D>("Grey Ball");
                RedBall = Content.Load<Texture2D>("Red Ball");
                YellowBall = Content.Load<Texture2D>("Yellow Ball");
                EightBall = Content.Load<Texture2D>("Black Ball");
                BallsDict.Add("Grey Ball", CueBall);
                BallsDict.Add("Red Ball", RedBall);
                BallsDict.Add("Yellow Ball", YellowBall);
                BallsDict.Add("Black Ball", EightBall);
            }

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
        /// 
        protected override void Update(GameTime gameTime)
        {
            Input.UpdateInputs();
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                //FileSaving.WriteToFile(@"C:\Users\Louis\source\repos\MonoGamePool1", BallsList);
                this.Exit();
            // TODO: Add your update logic here
            SaveButton = UpdateClass.UpdateButton(SaveButton, Input.mousePosition);
            LoadButton = UpdateClass.UpdateButton(LoadButton, Input.mousePosition);
            ResetButton = UpdateClass.UpdateButton(ResetButton, Input.mousePosition);
            PauseButton = UpdateClass.UpdateButton(PauseButton, Input.mousePosition);
            DebugButton = UpdateClass.UpdateButton(DebugButton, Input.mousePosition);
            TypeBox = UpdateClass.UpdateTextBoxPressed(TypeBox, Input.mousePosition);
            if (TypeBox.Pressed)
            {
                TypeBox = UpdateClass.UpdateTextBoxLetters(TypeBox);
            }
            if (SaveButton.Pressed && Input.LeftMouseJustClicked())
            {
                ButtonFunctions.SaveGame(BallsList);
            }
            if (LoadButton.Pressed && Input.LeftMouseJustClicked())
            {
                string path = @"C:\Users\Louis\source\repos\MonoGamePool1\MonoGamePool1\SaveFiles\2018-12-11-12-42-20";
                BallsList = ButtonFunctions.LoadGame(path, BallsDict);
            }
            if (ResetButton.Pressed && Input.LeftMouseJustClicked())
            {
                ButtonFunctions.ResetGame(ref BallsList, ref Graveyard, CueBall, YellowBall, RedBall, EightBall);
            }
            if (PauseButton.Pressed && Input.LeftMouseJustClicked())
            {
                ButtonFunctions.PauseGame(ref BallsList);
            }
            if (DebugButton.Pressed && Input.LeftMouseJustClicked())
            {
                ButtonFunctions.DebugGame();
            }
            /**if (TypeBox.Pressed && Input.LeftMouseJustClicked())
            {
                TypeBox.Pressed = !TypeBox.Pressed;
            }**/
            for (int a = 0; a < BallsList.Count; a++)
            {
                //Ball BallA = BallsList[a];
                BallsList[a] = UpdateClass.UpdateBall(BallsList[a]);
                BallsList[a] = Collisions.Ball_Wall(BallsList[a], BorderWidth, ScreenWidth, ScreenHeight);
                for (int b = a + 1; b < BallsList.Count; b++)
                {
                    Tuple<Ball, Ball> TempTuple = Collisions.Ball_Ball(BallsList[a], BallsList[b]);
                    BallsList[a] = TempTuple.Item1;
                    BallsList[b] = TempTuple.Item2;
                }
                BallsList = Collisions.Ball_Pocket(BallsList[a], PocketList, BallsList, a, CueBall, Graveyard, ScreenWidth);

                if (General.NoBallsMoving(BallsList) && ButtonFunctions.Velocities.Count == 0)
                {
                    BallsList[a] = Debug.PingBall(BallsList[a]);
                    PoolCue = UpdateClass.UpdatePoolCue(PoolCue, Input.mousePosition, BallsList[BallsList.Count - 1].Center);
                    if (Debug.sightStatus)
                    {
                        SightLine = UpdateClass.UpdateSightLine(SightLine, Input.mousePosition, BallsList[BallsList.Count - 1].Center);
                    }
                }
                else
                {
                    //PoolCue.End = PoolCue.Start;
                    //SightLine.End = SightLine.Start;
                }
                
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Tuple<Color, string> TextureTuple = Debug.ChangeTextures();
            GraphicsDevice.Clear(TextureTuple.Item1);
            //GraphicsDevice.Clear(Color.DimGray);
            spriteBatch.Begin();

            //Create watch and start it
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Graphics.DrawOuterPockets(spriteBatch, TableOuter, CueBall);
            Graphics.DrawBoard(spriteBatch, TableInner, TableOuter);
            Graphics.DrawPockets(spriteBatch, PocketList, EightBall);
            Graphics.DrawBalls(spriteBatch, BallsList, EightBall);
            Graphics.DrawScoreBox(spriteBatch, Graveyard, TableOuter, EightBall);
            Graphics.DrawButton(ref SaveButton, spriteBatch);
            Graphics.DrawButton(ref LoadButton, spriteBatch);
            Graphics.DrawButton(ref ResetButton, spriteBatch);
            Graphics.DrawButton(ref PauseButton, spriteBatch);
            Graphics.DrawButton(ref DebugButton, spriteBatch);
            Graphics.DrawTextBox(ref TypeBox, spriteBatch);
            if (TypeBox.Pressed && TypeBox.Timer < ButtonFunctions.BlinkTimer / 2)
            {
                Graphics.DrawBlinkingKeyLine(TypeBox, spriteBatch);
            }
            //Graphics.DrawCushionDiagonals(spriteBatch, DiagonalLines, TableOuter);
            if (General.NoBallsMoving(BallsList) && General.MouseWithinArea(Input.mousePosition, Vector2.Zero, new Vector2(ScreenWidth, ScreenHeight)) && ButtonFunctions.Velocities.Count == 0)
            {
                Graphics.DrawPoolCue(spriteBatch, PoolCue, PoolCueTexture);
                if (Debug.sightStatus)
                {
                    Graphics.DrawSightLine(spriteBatch, SightLine, SightLineTexture);
                }
            }
            Debug.ShowCoords(spriteBatch);

            watch.Stop();

            //Draw the amount of milliseconds it takes to draw everything else (16.66... ms or less is 60fps)
            if (Debug.speedTest)
            {
                spriteBatch.DrawString(font, watch.Elapsed.TotalMilliseconds.ToString("N3") + "ms", new Vector2(4), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);
            //Update(gameTime);

            //1046 x 548
            //ForestGreen and SaddleBrown
            //Draw outer pockets (silver lining?)
        }
    }
}