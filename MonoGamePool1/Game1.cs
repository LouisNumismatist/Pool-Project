using System;
using System.Diagnostics; //For Stopwatch
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
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
        readonly GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public static Game1 instance;

        public static Texture2D BlankBox;
        public static Texture2D BlankCircle;

        public static SpriteFont font;
        public static SpriteFont TextBoxFont;

        public static bool HittingCueBall;
        public static int BorderWidth = 40;
        public static int ScreenHeight = 548;
        public static int ScreenWidth = 1096; //increased from 1046
        public static int BallDiam = 22;                                                //SCALE: 1px = 0.25cm = 0.0025m
        public List<Ball> BallsList = new List<Ball>();                                 //Use 9ft Pool Table (274cm x 137cm, 1096px x 548px)
        public List<Pocket> PocketList = new List<Pocket>();                            //Max Velocity should be 16m/s which is 64px/s
        public List<Pocket> OuterPockets = new List<Pocket>();
        public List<Ball> Graveyard = new List<Ball>();
        public List<Button> ButtonList = new List<Button>();
        public List<DiagonalLine> DiagonalLines = new List<DiagonalLine>();
        public static DiagonalLine PoolCue;
        public static DiagonalLine SightLine;
        public static Button SaveButton;
        public static Button LoadButton;
        public static Button ResetButton;
        public static Button PauseButton;
        public static Button DebugButton;
        public static RegTextBox TypeBox;
        public static StackTextBox NameBox;
        public static NumBox RowsBox;
        public static MiniGraph SpeedTimeGraph;
        public static MiniGraph ForceTimeGraph;
        public List<Player> Players = new List<Player>();
        public static string Player1Name = "PlayerA";
        public static string Player2Name = "PlayerB";
        public static int CurrentPlayer = 0;
        public static Ball LastBall;
        public static SwitchBox SightSelect;

        public static readonly Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D> { { "Circle", BlankCircle }, { "Box", BlankBox } };

        //NEWTONSOFT
        public Game1()
        {
            instance = this;

            //ProcessStartInfo info = new ProcessStartInfo("www.rbwhitaker.wikidot.com/xna-tutorials");
            //Process.Start(info);
            
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = ScreenHeight + 100,
                PreferredBackBufferWidth = ScreenWidth + 200
            };
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

            //System.Speech.Synthesis.SpeechSynthesizer speechSynthesizer = new System.Speech.Synthesis.SpeechSynthesizer();
            //speechSynthesizer.Speak("hello");


            Init.InitialiseBalls(ref BallsList, BlankCircle);
            Init.InitialisePockets(ref OuterPockets, 26, Color.LightGray);
            Init.InitialisePockets(ref PocketList, 22, Color.Black);
            //PoolCue = Init.InitialisePoolCue();
            //SightLine = Init.InitialiseSightLine();
            PoolCue = new DiagonalLine(0, 3, new Vector2(174, 274), new Vector2(ScreenWidth, 274), Color.DarkOrange, true);
            SightLine = new DiagonalLine(0, 5, new Vector2(174, 274), new Vector2(274, 274), Color.Sienna, false); // - (5 / 2)
            string tempString = FileSaving.ObjectToString(BallsList[0]);
            //Ball tempBall = FileSaving.StringToObject(tempString, BallsDict);
            SaveButton = new Button(new Vector2(ScreenWidth + 50, 30), "SAVE", Color.Blue, font);
            LoadButton = new Button(new Vector2(ScreenWidth + 50, 60), "LOAD", Color.Blue, font);
            ResetButton = new Button(new Vector2(ScreenWidth + 50, 90), "RESET", Color.Blue, font);
            PauseButton = new Button(new Vector2(ScreenWidth + 50, 120), "PAUSE", Color.Blue, font);
            DebugButton = new Button(new Vector2(ScreenWidth + 50, 150),"DEBUG", Color.Blue, font);
            TypeBox = new RegTextBox(new Vector2(ScreenWidth + 25, 510), TextBoxFont, 10, "Enter:");
            NameBox = new StackTextBox(new Vector2(ScreenWidth + 25, 530), 12, TextBoxFont, "Name:");

            SightSelect = new SwitchBox(new Vector2(ScreenWidth + 25, 560), TextBoxFont);

            RowsBox = new NumBox(new Vector2(ScreenWidth + 50, 180), TextBoxFont, 14);

            SpeedTimeGraph = new MiniGraph(new Vector2(ScreenWidth + 15, 210), 175, 100);
            ForceTimeGraph = new MiniGraph(new Vector2(ScreenWidth + 15, 350), 175, 100);

            Players.Add(new Player(1, Player1Name));
            Players.Add(new Player(2, Player2Name));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //Tuple<Color, string> TextureTuple = Debug.ChangeTextures();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            BlankBox = Content.Load<Texture2D>("White Box");
            BlankCircle = Content.Load<Texture2D>("White Ball PNG");

            font = Content.Load<SpriteFont>("EndGameFont");
            TextBoxFont = Content.Load<SpriteFont>("EqualSpacedText");


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
                //FileSaving.WriteToFile(@"C:\Users\Louis\source\repos\MonoGamePool1", BallsList); DO NOT UNCOMMENT
                this.Exit();
            // TODO: Add your update logic here

            SaveButton.Update(Input.mousePosition);
            LoadButton.Update(Input.mousePosition);
            ResetButton.Update(Input.mousePosition);
            PauseButton.Update(Input.mousePosition);
            DebugButton.Update(Input.mousePosition);
            RowsBox.Update();
            TypeBox.UpdatePressed();
            NameBox.UpdatePressed();

            SightSelect.Update();
            if ((SightSelect.State && !Debug.sightStatus) || (!SightSelect.State && Debug.sightStatus))
            {
                Debug.sightStatus = !Debug.sightStatus;
            }

            if (TypeBox.Pressed)
            {
                TypeBox.UpdateLetters();
            }
            if (SaveButton.Pressed && Input.LeftMouseJustClicked())
            {
                GameStatus.SaveGame(BallsList);
            }
            if (LoadButton.Pressed && Input.LeftMouseJustClicked())
            {
                //PCName = Enviroment.UserName
                string path = @"C:\Users\Louis\source\repos\MonoGamePool1\MonoGamePool1\SaveFiles\2018-12-18-12-57-59";
                BallsList = GameStatus.LoadGame(path);
            }
            if (ResetButton.Pressed && Input.LeftMouseJustClicked())
            {
                GameStatus.ResetGame(ref BallsList, ref Graveyard, SpeedTimeGraph, ForceTimeGraph, BlankCircle, BlankBox);
            }
            if (PauseButton.Pressed && Input.LeftMouseJustClicked())
            {
                GameStatus.PauseGame(ref BallsList);
            }
            if (DebugButton.Pressed && Input.LeftMouseJustClicked())
            {
                GameStatus.DebugGame();
            }
            for (int a = 0; a < BallsList.Count; a++)
            {
                BallsList[a].Update();
                BallsList[a] = Collisions.Ball_Wall(BallsList[a], BorderWidth, ScreenWidth, ScreenHeight);
                for (int b = a + 1; b < BallsList.Count; b++)
                {
                    Tuple<Ball, Ball> TempTuple = Collisions.Ball_Ball(BallsList[a], BallsList[b]);
                    BallsList[a] = TempTuple.Item1;
                    BallsList[b] = TempTuple.Item2;
                }
                BallsList = Collisions.Ball_Pocket(BallsList[a], PocketList, BallsList, a, BlankCircle, Graveyard, ScreenWidth);
                
                if (General.NoBallsMoving(BallsList) && GameStatus.Velocities.Count == 0)
                {
                    BallsList[a] = Debug.PingBall(BallsList[a]);
                    PoolCue = Updates.UpdatePoolCue(PoolCue, Input.mousePosition, BallsList[BallsList.Count - 1].Center);
                    if (Debug.sightStatus)
                    {
                        SightLine = Updates.UpdateSightLine(SightLine, Input.mousePosition, BallsList[BallsList.Count - 1].Center);
                    }
                }
                else
                {
                    //PoolCue.End = PoolCue.Start;
                    //SightLine.End = SightLine.Start;
                }
                
            }
            Updates.UpdateCurrentPlayer(ref CurrentPlayer, Players);
            if (BallsList.Count + Graveyard.Count > 15)
            {
                SpeedTimeGraph.Update((float)Physics.Pythagoras1(BallsList[BallsList.Count - 1].Velocity.X, BallsList[BallsList.Count - 1].Velocity.Y));
                ForceTimeGraph.Update((float)Physics.NewtonAcc(BallsList[BallsList.Count - 1]));
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SaddleBrown);

            spriteBatch.Begin();

            Stopwatch watch = new Stopwatch(); //Create watch and start it
            watch.Start();
            Graphics.DrawPockets(spriteBatch, OuterPockets);
            Graphics.DrawBoard(spriteBatch);
            Graphics.DrawPockets(spriteBatch, PocketList);
            Graphics.DrawBalls(spriteBatch, BallsList);
            Graphics.DrawScoreBox(spriteBatch, Graveyard);

            SaveButton.Draw(spriteBatch);
            LoadButton.Draw(spriteBatch);
            ResetButton.Draw(spriteBatch);
            PauseButton.Draw(spriteBatch);
            DebugButton.Draw(spriteBatch);

            TypeBox.Draw(spriteBatch);

            SightSelect.Draw(spriteBatch);
            if (Debug.sightStatus)
            {
                spriteBatch.DrawString(SightSelect.Font, "Sight Line Active", new Vector2(SightSelect.Origin.X + SightSelect.Dimensions.X + 10, SightSelect.Origin.Y), Color.Black, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.DrawString(SightSelect.Font, "Sight Line Inactive", new Vector2(SightSelect.Origin.X + SightSelect.Dimensions.X + 10, SightSelect.Origin.Y), Color.Black, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);
            }

            RowsBox.Draw(spriteBatch);
            spriteBatch.DrawString(RowsBox.Font, "Rows", new Vector2(RowsBox.Origin.X + RowsBox.Dimensions.X + 15, RowsBox.Origin.Y), Color.Black);

            SpeedTimeGraph.Draw(spriteBatch);
            ForceTimeGraph.Draw(spriteBatch);

            if (TypeBox.Pressed && TypeBox.Timer < TextBox.BlinkTimer / 2)
            {
                TypeBox.DrawBlinkingKeyLine(spriteBatch, TypeBox.Pointer);
            }
            //Graphics.DrawCushionDiagonals(spriteBatch, DiagonalLines, BlankBox);
            if (General.NoBallsMoving(BallsList) && Input.MouseWithinArea(Vector2.Zero, new Vector2(ScreenWidth, ScreenHeight)) && GameStatus.Velocities.Count == 0)
            {
                Graphics.DrawDiagonalLine(spriteBatch, PoolCue);
                if (Debug.sightStatus)
                {
                    Graphics.DrawDiagonalLine(spriteBatch, SightLine);
                }
            }
            Debug.ShowCoords(spriteBatch);

            for (int x = 0; x < Players.Count; x++)
            {
                Vector2 origin = new Vector2(50, 570 + 40 * x);
                //spriteBatch.Draw(spriteBatch, new Rectangle(origin.X, origin.Y, ));
                spriteBatch.DrawString(TextBoxFont, Players[x].Name, origin, Color.Black);
            }

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

//Highscores entered into database  X
//Names checked against previous names
//Passwords entered (relational database and hashing)
//Use textbox stack to enter text
//Sort scores (sorting algorithms - work out which is best)

//File path for saving to file as constant
//Use folders for days? - File selection for time in days

//Add trig again
//SORT COLLISIONS

//Lines as objects?

//MiniGraphs have Queues in them - use Physics equations and Queues (A-Level Standard) - engineering circular motion suvat & Impulse, Torque?

//BACKTRACK SEARCH BOX - reduce complexity to make it a stack again (no extra stack functions)
//NumBox chars R -> L?

//Stack and Queue for textbox - limited letter range in queue, extras put onto start and end stacks

//MVC (Model-Controller-View)

//Try excepts around line drawing / file checking
