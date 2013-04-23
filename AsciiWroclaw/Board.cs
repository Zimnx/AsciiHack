using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiWroclaw
{
    class Board
    {
        public enum Field { EMPTY = 0 , HAY = 1, ROCK = 2, DIAMOND = 3, WALL, PLAYER,EXIT }

        Field[,] board;
        int maxScore;

        List<Pair> FallingBlocks;
        public List<Pair> DeadlyBlocks;

        Random random = new Random();

        public Board()
        {
            
            board = new Field[buffer.WindowHeight, buffer.WindowHeight];
            for (int i = 1; i < board.GetLength(0)-1; i++)
                for (int j = 1; j < board.GetLength(1)-1; j++)
                {
                    int x = random.Next(0, 4);
                    if ((Field)x == Field.ROCK)
                        if (random.Next(3) < 2)
                            x = 1;
                                if ((Field)x == Field.DIAMOND)
                        if (random.Next(3) < 2)
                            x = 1;
                    board[i, j] = (Field)x;//Field.HAY;
                    if ((Field)x == Field.DIAMOND)
                        maxScore++;
                }

            for (int i = 0; i < board.GetLength(0); i++)
            {
                board[i, 0] = Field.WALL;
                board[i, buffer.WindowHeight - 1] = Field.WALL;
            }
            for (int i = 0; i < board.GetLength(1); i++)
            {
                board[0, i] = Field.WALL;
                board[buffer.WindowHeight - 1 , i] = Field.WALL;
            }

            board[random.Next(1, 30), random.Next(1, 30)] = Field.EXIT;

            
            FallingBlocks = new List<Pair>();
            DeadlyBlocks = new List<Pair>();
        }

        public Board(string[] b,int score)
        {
            maxScore = score;
            board = new Field[b.Count(), b[0].Count()];
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                    switch (b[i][j])
                    {
                        case '.':
                            board[i, j] = Field.HAY;
                            break;
                        case '#':
                            board[i, j] = Field.WALL;
                            break;
                        case '@':
                            board[i, j] = Field.EMPTY;
                            break;
                        case '&':
                            board[i, j] = Field.DIAMOND;
                            break;
                        case 'Q':
                            board[i, j] = Field.ROCK;
                            break;
                        case 'E':
                            board[i, j] = Field.EXIT;
                            break;
                    }

            FallingBlocks = new List<Pair>();
            DeadlyBlocks = new List<Pair>();
            
        }


        public void Update()
        {
            //if (FallingBlocks != null)
                //FallingBlocks.Clear();

            //FallingBlocks.Clear();
            DeadlyBlocks.Clear();
            List<Pair> temp = new List<Pair>();
            List<Pair> m = new List<Pair>();
            for (int i = board.GetLength(0) - 1; i >= 0; i--)
                for (int j = board.GetLength(1) - 1; j >= 0; j--)
                {
                    if (m.Contains(new Pair(i, j)))
                        continue;
                    switch (board[i, j])
                    {
                        case Field.EMPTY:
                        case Field.PLAYER:
                            if (board[i - 1, j] == Field.ROCK)
                            {
                                m.Add(new Pair(i - 1, j));
                                board[i - 1, j] = Field.EMPTY;
                                board[i, j] = Field.ROCK;
                                temp.Add(new Pair(i, j));
                                if (FallingBlocks.Contains(new Pair(i - 1, j)))
                                    DeadlyBlocks.Add(new Pair(i, j));
                            }
                            break;
                        
                    }
                }
           

            FallingBlocks.Clear();
            FallingBlocks.AddRange(temp);
        }

        public void Render()
        {
            for (int i = 0 ; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    switch (board[i, j])
                    {
                        case Field.HAY:
                            buffer.Draw(".", j, i, 15);
                            break;
                        case Field.DIAMOND:
                            buffer.Draw("&", j, i, 14);
                            break;
                        case Field.EMPTY:
                            buffer.Draw(" ", j, i, 15);
                            break;
                        case Field.WALL:
                            buffer.Draw("#", j, i, 8);
                            break;
                        case Field.ROCK:
                            buffer.Draw("Q", j, i, 10);
                            break;
                        case Field.EXIT:
                            buffer.Draw("E", j, i, 12);
                            break;
                    }
                }
        }

        public Field this[int x, int y]
        {
            get { return board[y, x]; }
            set { board[y, x] = value; }
        }
        public int Score
        {
            get { return maxScore; }
        }
    }
}

