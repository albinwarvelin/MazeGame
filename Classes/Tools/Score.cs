using System;
using System.Collections.Generic;
using System.Text;

namespace MazeGame
{
    class Score : IComparable<Score>
    {
        private readonly string name;
        private int score;
        private readonly DateTime date;

        public Score(string name, int score, DateTime date)
        {
            this.name = name;
            this.score = score;
            this.date = date;
        }

        public int CompareTo(Score score)
        {
            if (this.score > score.Points) return -1;
            if (this.score == score.Points) return 0;
            return 1;
        }

        public override string ToString()
        {
            return name + "," + score + "," + date.ToString(); 
        }

        public string ToHighscoreList()
        {
            string toReturn = "";



            return toReturn;
        }

        public string Name
        { 
            get { return name; } 
        }

        public int Points
        {
            get { return score; }
            set { score = value; }
        }

        public DateTime Date
        {
            get { return date; }
        }
    }
}
