using System;

namespace MazeGame
{
    /// <summary>
    /// Single score instance.s
    /// </summary>
    class Score : IComparable<Score>
    {
        private readonly string name;
        private int score;
        private readonly DateTime date;

        /// <summary>
        /// Creates a score instance.
        /// </summary>
        /// <param name="name">Name of scoreholder.</param>
        /// <param name="score">Score in levels finished.</param>
        /// <param name="date">Date and time of score.</param>
        public Score(string name, int score, DateTime date)
        {
            this.name = name;
            this.score = score;
            this.date = date;
        }

        /// <summary>
        /// Implements compareTo method of IComparable so that scores can be sorted by score value.
        /// </summary>
        /// <param name="score">Score to compare.</param>
        /// <returns></returns>
        public int CompareTo(Score score)
        {
            if (this.score > score.Value) return -1;
            if (this.score == score.Value) return 0;
            return 1;
        }

        /// <summary>
        /// Provides score in string form for optimal storage in csv form.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name + "," + score + "," + date.ToString();
        }

        /// <summary>
        /// Name of scoreholder property.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Score property.
        /// </summary>
        public int Value
        {
            get { return score; }
            set { score = value; }
        }

        /// <summary>
        /// DateTime property.
        /// </summary>
        public DateTime Date
        {
            get { return date; }
        }
    }
}
