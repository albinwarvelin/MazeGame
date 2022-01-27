using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MazeGame
{
    static class HighScore
    {
        private static Score currentScore;
        private static List<Score> allScores;

        public static void LoadHighScores()
        {
            try
            {
                StreamReader sr = new StreamReader("highscores.txt");

                allScores = new List<Score>();

                using (sr)
                {
                    while (!sr.EndOfStream)
                    {
                        string[] row = sr.ReadLine().Split(",");

                        allScores.Add(new Score(row[0], int.Parse(row[1]), DateTime.Parse(row[2])));
                    }
                }
            }
            catch(FileNotFoundException)
            {
                allScores = new List<Score>();
            }
        }

        public static void SaveHighScores()
        {
            StreamWriter sw = new StreamWriter("highscores.txt");

            using(sw)
            {
                foreach(Score score in allScores)
                {
                    sw.WriteLine(score.ToString());
                }
            }
        }

        public static void SaveScore()
        {
            allScores.Add(currentScore);
            currentScore = null;
        }

        public static List<string> RecentPlayers
        {
            get
            {
                List<string> list = new List<string>();

                for(int i = allScores.Count - 1; i >= 0; i--)
                {
                    if(!list.Contains(allScores[i].Name))
                    {
                        list.Add(allScores[i].Name);
                    }
                    if(list.Count == 3)
                    {
                        break;
                    }
                }

                return list;
            }
        }

        public static List<string> HighscoreList
        {
            get
            {
                List<Score> temp = new List<Score>();
                temp.AddRange(allScores);
                temp.Sort();

                List<String> toReturn = new List<String>();

                for(int i = 0; i < 10; i++)
                {
                    string tempString = "";

                    if(i < temp.Count)
                    {
                        tempString += temp[i].Name;

                        for (int a = temp[i].Name.Length; a < 19; a++)
                        {
                            tempString += " ";
                        }

                        tempString += temp[i].Points;

                        for (int a = temp[i].Points.ToString().Length; a < 12; a++)
                        {
                            tempString += " ";
                        }

                        tempString += temp[i].Date.ToString("dd MMM yy HH:mm");
                    }

                    toReturn.Add(tempString);
                }

                return toReturn;
            }
        }

        public static List<Score> AllScores
        {
            get { return allScores; }
        }

        public static Score CurrentScore
        {
            get { return currentScore; }
            set { currentScore = value; }
        }
    }
}
