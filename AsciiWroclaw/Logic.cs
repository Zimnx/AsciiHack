using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;
namespace AsciiWroclaw
{
    class Logic
    {

        public enum GameState { MENU, MENU_ANIMATION, GAME, PAUSE, EXIT, GAME_OVER };
        public delegate void GameStateChangedHandler(Logic.GameState newState);
        public static event GameStateChangedHandler OnGameStateChanged;
        
        bool logicRunning = true;
        long elapsed;

        KeyState keyState = new KeyState();

        StartMenu startMenu = new StartMenu();
        GameOver gameOver;

        Game game = new Game();

        AnimateWindow introAnimation;
        AnimateWindow endAnimation;
        GameState state;

        int temp = 0;

        public Logic()
        {
            
            
            Timer timer = new Timer(1000 / 30);
            timer.Enabled = true;
            timer.Elapsed += Render;
        }

        public void Start()
        {
            introAnimation = new AnimateWindow(
                                    new string[][] { Strings.Title, Strings.Play, Strings.Exit },
                                    new int[] { 5, 5, 0 });
            introAnimation.OnAnimationFinished += introAnimation_OnAnimationFinished;
            state = GameState.MENU_ANIMATION;

            startMenu.OnMenuOptionClick += startMenu_OnMenuOptionClick;

            while (logicRunning) { KeyState.getKeys(); }
        }

        void startMenu_OnMenuOptionClick(Logic.GameState newState)
        {
            changeGameState(newState);
        }

        void changeGameState(GameState newState)
        {
            switch (newState)
            {
                case GameState.EXIT:
                    logicRunning = false;
                    break;
                case GameState.GAME:
                    startGame();
                    break;
                case GameState.GAME_OVER:
                    endGame();
                    break;
            }
        }

        void endGame()
        {
            gameOver = new GameOver();
            state = GameState.GAME_OVER;
            game = null;
            
        }

        void startGame()
        {
            state = GameState.GAME;
            game = new Game(); 
        }
        
        void introAnimation_OnAnimationFinished()
        {
            state = GameState.MENU;
        }

        void Render(object sender, ElapsedEventArgs e)
        {
            buffer.Clear();

            switch(state)
            {
                case GameState.MENU_ANIMATION:
                    introAnimation.Render();
                    break;
                case GameState.MENU:
                    startMenu.Render();
                    break;
                case GameState.GAME:
                    game.Render();
                    break;
                case GameState.GAME_OVER:
                    gameOver.Render();
                    break;
            }
            if (e.SignalTime.Ticks - elapsed > 1000000) {
                elapsed = e.SignalTime.Ticks;

                switch (state)
                {
                    case GameState.MENU_ANIMATION:
                        introAnimation.Update();
                        break;
                    //case GameState.MENU:
                    //    startMenu.Render();
                    //    break;
                    case GameState.GAME:
                        game.Update();
                        if (game.dead)
                            changeGameState(GameState.GAME_OVER);
                        break;
                    case GameState.GAME_OVER:
                        gameOver.Update();
                        break;
                }    
            }
            

            //startMenu.DrawTitle();
            //introAnimation.Render();
            buffer.Print();
        }
    }
}
