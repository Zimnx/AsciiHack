using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace AsciiWroclaw
{
    class Game
    {
        Board board;
        Player player;
        int currentLevel;

        public Game()
        {
            KeyState.OnKeyPressed += KeyState_OnKeyPressed;
            currentLevel = 1;
            //LoadLevel(currentLevel);
            player = new Player(30, 30);
            board = new Board();
            board[30, 30] = Board.Field.EMPTY;
            board[30, 29] = Board.Field.HAY;
        }

        void KeyState_OnKeyPressed(Keys key)
        {
            switch(key)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Left:
                case Keys.Right:
                    tryMovePlayer(key);
                    break;
            }
        }

        void tryMovePlayer(Keys key)
        {
            bool canMove = true;
            int newX = player.positionX;
            int newY = player.positionY;
            int farX = newX;
            int farY = newY;

            switch (key)
            {
                case Keys.Up:
                    newY -= 1;
                    farY -= 2;
                    break;
                case Keys.Down:
                    newY += 1;
                    farY += 2;
                    break;
                case Keys.Left:
                    newX -= 1;
                    farX -= 2;
                    break;
                case Keys.Right:
                    newX += 1;
                    farX += 2;
                    break;
            }


            if (board[newX, newY] == Board.Field.WALL ||
                (board[newX, newY] == Board.Field.ROCK && (board[farX, farY] != Board.Field.EMPTY)) ||
                (board[newX,newY] == Board.Field.EXIT && player.points != board.Score))
            {
                canMove = false;
            }

            if (!canMove)
                return;
            
            switch (board[newX, newY])
            {
                case Board.Field.ROCK: // za nim na pewno jest wole miejsce
                    if (newY < player.positionY)
                    {
                        canMove = false;
                        break;
                    }
                    board[newX, newY] = Board.Field.EMPTY;
                    board[farX, farY] = Board.Field.ROCK;
                    break;
                case Board.Field.DIAMOND:
                    player.points++;
                    break;
                case Board.Field.EXIT:
                    LoadLevel(++currentLevel);
                    return;
            }
            if (!canMove)
                return;

            board[newX, newY] = Board.Field.EMPTY;


            board[player.positionX, player.positionY] = Board.Field.EMPTY;
            player.positionX = newX;
            player.positionY = newY;
            board[newX, newY] = Board.Field.PLAYER;
        }

        public void Update()
        {
            board.Update();
            player.Update();

            if (board[player.positionX, player.positionY] == Board.Field.ROCK)
            {
                board[player.positionX, player.positionY - 1] = Board.Field.ROCK;
                board[player.positionX, player.positionY] = Board.Field.EMPTY;
            }

            checkFallingBlocks();
        }

        public bool dead = false;
        void checkFallingBlocks()
        {
            if (board.DeadlyBlocks == null)
                return;
            foreach (var p in board.DeadlyBlocks)
            {
                if (p.Y == player.positionX && p.X == player.positionY)
                    dead = true;
            }
        }

        public void Render()
        {
            board.Render();
            player.Render();
            printScore();
        }
        private void printScore()
        {
            buffer.Draw(String.Format("Score:{0}/{1}",player.points,board.Score), 0, buffer.WindowHeight - 1, 15);
        }

        private void LoadLevel(int level)
        {
            var levelFile = File.Open(Environment.CurrentDirectory + "/Levels/Level" + level + ".txt", FileMode.Open);
            StreamReader sr = new StreamReader(levelFile);
            string[] playerPos = sr.ReadLine().Split(' ');
            player = new Player(Int32.Parse(playerPos[0]), Int32.Parse(playerPos[1]));
            board = new Board(sr.ReadToEnd().Split(new string[] {"\n","\r"}, StringSplitOptions.RemoveEmptyEntries),Int32.Parse(playerPos[2]));
            sr.Close();
        }
    }
}
