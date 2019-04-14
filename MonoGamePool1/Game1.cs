using System;
using System.Collections;
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
        public static Texture2D PixelBox;
        public static Texture2D BlankCircle;

        public static SpriteFont font;
        public static SpriteFont TextBoxFont;

        public static Vector2 DisplacementMarker;

        public static bool EndGame = false;
        public static bool PlacingCueBall = false;
        public static bool HittingCueBall;
        public static int BorderWidth = 40;
        public static int ScreenHeight = 548;
        public static int ScreenWidth = 1096; //increased from 1046
        public static int BallDiam = 22;                                                //SCALE: 1px = 0.25cm = 0.0025m
        public List<Ball> BallsList = new List<Ball>();                                 //Use 9ft Pool Table (274cm x 137cm, 1096px x 548px)
        public List<Pocket> PocketList = new List<Pocket>();                            //Max Velocity should be 16m/s which is 64px/s
        public List<Pocket> OuterPockets = new List<Pocket>();
        public List<Ball> Graveyard = new List<Ball>();
        public List<Ball> RecentlyPotted = new List<Ball>();
        public static Color FirstTapped = Color.White;

        public static DiagonalLine PoolCue;
        public static DiagonalLine SightLine;

        public static Button SaveButton;
        public static Button LoadButton;
        public static Button HighScoresButton;
        public static Button ResetButton;
        public static Button PauseButton;
        public static Button DebugButton;
        public static Button HelpButton;

        public static StackTextBox TypeBox;
        public static StackTextBox NameBox;

        public static NumBox RowsBox;
        public static MiniGraph SpeedTimeGraph;
        public static MiniGraph DisplacementTimeGraph;
        public static MiniGraph EkTimeGraph;
        public static MiniGraph CentripetalForceGraph;
        public static List<Player> Players = new List<Player>();
        public static string Player1Name = "Player 1";
        public static string Player2Name = "Player 2";
        public static int CurrentPlayer = 0;
        public static SwitchBox SightSelect;
        public static HighScores hs;

        public static readonly Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D> { { "Circle", BlankCircle }, { "Box", BlankBox } };

        public Game1()
        {
            instance = this;
            
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = ScreenHeight + 100,
                PreferredBackBufferWidth = ScreenWidth + 400
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
            base.Initialize();
            Vector2 origin = new Vector2(174, 274);

            Init.InitialiseBalls(ref BallsList, BlankCircle);
            Init.InitialisePockets(ref OuterPockets, 26, Color.LightGray);
            Init.InitialisePockets(ref PocketList, 22, Color.Black);
            PoolCue = new DiagonalLine(5, origin, new Vector2(ScreenWidth, 274), Color.DarkOrange, true);
            SightLine = new DiagonalLine(3, origin, new Vector2(274, 274), Color.Sienna, false);
            string tempString = FileSaving.ObjectToString(BallsList[0]);
            SaveButton = new Button(new Vector2(ScreenWidth + 230, 30), "SAVE", Color.Blue, font);
            LoadButton = new Button(new Vector2(ScreenWidth + 230 + 66, 30), "LOAD", Color.Blue, font);
            HighScoresButton = new Button(new Vector2(ScreenWidth + 230, 60), "HIGHSCORES", Color.Blue, font);
            ResetButton = new Button(new Vector2(ScreenWidth + 230 + 66, 90), "RESET", Color.Blue, font);
            PauseButton = new Button(new Vector2(ScreenWidth + 230, 90), "PAUSE", Color.Blue, font);
            DebugButton = new Button(new Vector2(ScreenWidth + 230, 120),"DEBUG", Color.Blue, font);
            HelpButton = new Button(new Vector2(ScreenWidth + 230 + 66, 120), "HELP", Color.Blue, font);

            RowsBox = new NumBox(new Vector2(ScreenWidth + 230, 180), TextBoxFont, 14);

            TypeBox = new StackTextBox(new Vector2(ScreenWidth + 230, 210), TextBoxFont, 14, Player1Name + ":");
            NameBox = new StackTextBox(new Vector2(ScreenWidth + 230, 240), TextBoxFont, 14, Player2Name + ":");

            SightSelect = new SwitchBox(new Vector2(ScreenWidth + 235, 270), TextBoxFont);
            
            SpeedTimeGraph = new MiniGraph(new Vector2(ScreenWidth + 15, 30), 175, 100, "Speed-Time");
            DisplacementTimeGraph = new MiniGraph(new Vector2(ScreenWidth + 15, 170), 175, 100, "Displacement-Time");
            EkTimeGraph = new MiniGraph(new Vector2(ScreenWidth + 15, 310), 175, 100, "Kinetic Energy-Time");
            CentripetalForceGraph = new MiniGraph(new Vector2(ScreenWidth + 15, 450), 175, 100, "Centripetal Force-Time");

            Players.Add(new Player(1, Player1Name));
            Players.Add(new Player(2, Player2Name));


            //Speaking instructional information on startup
            System.Speech.Synthesis.SpeechSynthesizer speechSynthesizer = new System.Speech.Synthesis.SpeechSynthesizer();

            speechSynthesizer.SpeakAsync("Welcome to eight ball pool. Please enter your names in the text box below");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            PixelBox = new Texture2D(graphics.GraphicsDevice, 1, 1);
            PixelBox.SetData(new[] { Color.White });
            BlankBox = Content.Load<Texture2D>("White Box");
            BlankCircle = Content.Load<Texture2D>("White Ball PNG");

            font = Content.Load<SpriteFont>("EndGameFont");
            TextBoxFont = Content.Load<SpriteFont>("EqualSpacedText");
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
                this.Exit();

            if (!EndGame) //No updates after the game has ended
            {
                SaveButton.Update(Input.mousePosition);
                LoadButton.Update(Input.mousePosition);
                HighScoresButton.Update(Input.mousePosition);
                ResetButton.Update(Input.mousePosition);
                PauseButton.Update(Input.mousePosition);
                DebugButton.Update(Input.mousePosition);
                HelpButton.Update(Input.mousePosition);
                RowsBox.Update();

                TypeBox.UpdatePressed(); //Update textboxes
                if (TypeBox.Pressed)
                {
                    NameBox.Pressed = false;
                    TypeBox.UpdateLetters();
                }
                NameBox.UpdatePressed();
                if (NameBox.Pressed)
                {
                    TypeBox.Pressed = false;
                    NameBox.UpdateLetters();
                }

                if (TypeBox.Output.Length > 0)
                {
                    if (TypeBox.ValidEntry() && Player.ValidateName(TypeBox.Output))
                    {
                        Players[0].SetName(TypeBox.Output);
                    }
                    else
                    {
                        TypeBox.Valid = false;
                    }
                    TypeBox.Clear();
                }

                if (NameBox.Output.Length > 0)
                {
                    if (NameBox.ValidEntry() && Player.ValidateName(NameBox.Output))
                    {
                        Players[1].SetName(NameBox.Output);
                    }
                    else
                    {
                        NameBox.Valid = false;
                    }
                    NameBox.Clear();
                }

                SightSelect.Update();
                if ((SightSelect.State && !Debug.sightStatus) || (!SightSelect.State && Debug.sightStatus))
                {
                    Debug.sightStatus = !Debug.sightStatus;
                }

                string CompUsername = Environment.UserName;
                if (SaveButton.Pressed && Input.LeftMouseJustClicked()) //Update buttons
                {
                    string path = @"C:\Users\" + CompUsername + @"\source\repos\MonoGamePool1\MonoGamePool1\SaveFiles\";
                    GameStatus.SaveGame(BallsList, path);
                }
                if (LoadButton.Pressed && Input.LeftMouseJustClicked())
                {
                    string path = @"C:\Users\" + CompUsername + @"\source\repos\MonoGamePool1\MonoGamePool1\SaveFiles\2019-3-20-10-8-40";
                    BallsList = GameStatus.LoadGame(path);
                }
                if (HighScoresButton.Pressed && Input.LeftMouseJustClicked())
                {
                    hs = new HighScores();
                    hs.Show();
                }
                if (ResetButton.Pressed && Input.LeftMouseJustClicked())
                {
                    GameStatus.ResetGame(ref BallsList, ref Graveyard, SpeedTimeGraph, DisplacementTimeGraph, EkTimeGraph, CentripetalForceGraph, BlankCircle, BlankBox);
                }
                if (PauseButton.Pressed && Input.LeftMouseJustClicked())
                {
                    GameStatus.PauseGame(ref BallsList);
                }
                if (DebugButton.Pressed && Input.LeftMouseJustClicked())
                {
                    Debug.DebugGame();
                }
                if (HelpButton.Pressed && Input.LeftMouseJustClicked())
                {
                    ProcessStartInfo info = new ProcessStartInfo("https://www.colorado.edu/umc/sites/default/files/attached-files/8-ball_rules_bca.pdf");
                    Process.Start(info);
                }

                for (int a = 0; a < BallsList.Count; a++) //Updating balls
                {
                    Ball ballA = BallsList[a];
                    ballA.Update();
                    ballA = Collisions.Ball_Wall(ballA, BorderWidth, ScreenWidth, ScreenHeight);
                    for (int b = 0; b < BallsList.Count; b++)
                    {
                        Ball ballB = BallsList[b];
                        if (a == b) continue;

                        bool collided;
                        Tuple<Ball, Ball> temp = Collisions.Ball_Ball(ballA, ballB, out collided);
                        BallsList[a] = temp.Item1;
                        BallsList[b] = temp.Item2;
                    }
                    Collisions.Ball_Pocket(BallsList[a], PocketList, ref BallsList, a, BlankCircle, Graveyard, ScreenWidth, ref PlacingCueBall, ref EndGame);

                    if (General.NoBallsMoving(BallsList) && GameStatus.Velocities.Count == 0) //Check if the turn has ended
                    {
                        if (GamePlay.InTurn)
                        {
                            GamePlay.EndTurn(ref Players, ref CurrentPlayer, ref BallsList);
                            GamePlay.InTurn = false;
                        }

                        Ball cueBall = BallsList[BallsList.Count() - 1];
                        if (!PlacingCueBall)
                        {
                            if (DisplacementMarker != cueBall.Center)
                            {
                                DisplacementMarker = cueBall.Center;
                            }
                            BallsList[a] = Debug.PingBall(BallsList[a]);
                            PoolCue = Updates.UpdatePoolCue(PoolCue, Input.mousePosition, cueBall.Center);
                            if (Debug.sightStatus)
                            {
                                SightLine = Updates.UpdateSightLine(SightLine, Input.mousePosition, cueBall.Center, PoolCue);
                            }
                        }
                        else
                        {
                            GamePlay.PlaceCueBall(ref cueBall, Input.mousePosition, ref PlacingCueBall);
                        }
                    }
                }

                if (BallsList.Count + Graveyard.Count > 15) //Update graphs
                {
                    Ball ball = BallsList[BallsList.Count - 1];
                    SpeedTimeGraph.Update((float)Physics.Pythagoras1(ball.Velocity.X, ball.Velocity.Y));
                    DisplacementTimeGraph.Update(Vector2.Distance(DisplacementMarker, ball.Center));
                    EkTimeGraph.Update((float)Physics.KineticEnergy(ball.Mass, (float)Physics.Pythagoras1(ball.Velocity.X, ball.Velocity.Y)));
                    CentripetalForceGraph.Update(CircularMotion.CentripetalForce(ball.Velocity, ball.Radius, ball.Mass));
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
            GraphicsDevice.Clear(Color.SaddleBrown);

            spriteBatch.Begin();

            Stopwatch watch = new Stopwatch(); //Create watch and start it
            watch.Start();
            Graphics.DrawPockets(spriteBatch, OuterPockets); //Draw main Pool Table
            Graphics.DrawBoard(spriteBatch);
            Graphics.DrawPockets(spriteBatch, PocketList);

            if (PlacingCueBall) //Line for when the cue ball is being placed
            {
                spriteBatch.Draw(BlankBox, new Rectangle(274, BorderWidth, 1, ScreenHeight - 2 * BorderWidth), Color.LightGray);
            }

            Graphics.DrawBalls(spriteBatch, BallsList); //Draw balls on table to screen
            if (General.NoBallsMoving(BallsList) && Input.MouseWithinArea(Vector2.Zero, new Vector2(ScreenWidth, ScreenHeight)) && GameStatus.Velocities.Count == 0 && !PlacingCueBall && !EndGame)
            {
                PoolCue.Draw(spriteBatch);
                if (Debug.sightStatus) //Draws Sight Line to screen if active
                {
                    SightLine.Draw(spriteBatch);
                }
            }
            Graphics.DrawScoreBox(spriteBatch, Graveyard);

            SaveButton.Draw(spriteBatch); //Draws all the buttons to screen
            LoadButton.Draw(spriteBatch);
            HighScoresButton.Draw(spriteBatch);
            ResetButton.Draw(spriteBatch);
            PauseButton.Draw(spriteBatch);
            DebugButton.Draw(spriteBatch);
            HelpButton.Draw(spriteBatch);

            TypeBox.Draw(spriteBatch);
            NameBox.Draw(spriteBatch);

            SightSelect.Draw(spriteBatch);
            string sightText = "Ina";
            if (Debug.sightStatus)
            {
                sightText = "A";
            }
            spriteBatch.DrawString(SightSelect.Font, "Sight Line " + sightText + "ctive", new Vector2(SightSelect.Origin.X + SightSelect.Dimensions.X + 10, SightSelect.Origin.Y), Color.Black, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 0);


            RowsBox.Draw(spriteBatch);
            spriteBatch.DrawString(RowsBox.Font, "Rows", new Vector2(RowsBox.Origin.X + RowsBox.Dimensions.X + 15, RowsBox.Origin.Y), Color.Black);

            SpeedTimeGraph.Draw(spriteBatch); //Draws MiniGraphs to screen
            DisplacementTimeGraph.Draw(spriteBatch);
            EkTimeGraph.Draw(spriteBatch);
            CentripetalForceGraph.Draw(spriteBatch);

            if (TypeBox.Pressed && TypeBox.Timer < TextBox.BlinkTimer / 2) //Draws blinking line on textboxes if active and appropriate
            {
                TypeBox.DrawBlinkingKeyLine(spriteBatch, TypeBox.Chars.GetLength());               
            }
            if (NameBox.Pressed && NameBox.Timer < TextBox.BlinkTimer / 2)
            {
                NameBox.DrawBlinkingKeyLine(spriteBatch, NameBox.Chars.GetLength());                
            }
            
            Debug.ShowCoords(spriteBatch); //Debug information
            Debug.DrawBoundingBoxes(spriteBatch, BallsList, BlankBox);

            for (int x = 0; x < Players.Count; x++)
            {
                Vector2 origin = new Vector2(50, 570 + 40 * x);
                spriteBatch.DrawString(TextBoxFont, Players[x].Name, origin, Players[x].Colour);
            }
            if (!EndGame) //Player turn text
            {
                spriteBatch.DrawString(TextBoxFont, Players[CurrentPlayer].Name + "'s turn", new Vector2(ScreenWidth * 0.43f, ScreenHeight + 3), Players[CurrentPlayer].Colour);
                spriteBatch.DrawString(TextBoxFont, Players[CurrentPlayer].Shots + " shots remaining", new Vector2(ScreenWidth * 0.2f, ScreenHeight + 3), Players[CurrentPlayer].Colour);
            }
            else //Endgame text
            {
                spriteBatch.DrawString(TextBoxFont, "GAME OVER", new Vector2(ScreenWidth * 0.43f, ScreenHeight + 3), Color.Black);
            }


            watch.Stop();

            //Draw the amount of milliseconds it takes to draw everything else (16.66... ms or less is 60fps)
            if (Debug.speedTest && !EndGame) //Debug information
            {
                spriteBatch.DrawString(font, watch.Elapsed.TotalMilliseconds.ToString("N3") + "ms", new Vector2(72, 11), Color.White);
            }

            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
