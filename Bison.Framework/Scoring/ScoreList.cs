using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Scoring
{
    /// <summary>
    /// Class representing list of scores.
    /// </summary>
    public class ScoreList
    {
        #region Members

        /// <summary>
        /// The scores list.
        /// </summary>
        private List<Score> scores = new List<Score>();

        /// <summary>
        /// The capacity of the scores list.
        /// </summary>
        public const int SCORES_CAPACITY = 10;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new scores list.
        /// </summary>
        public ScoreList()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new score to the list.
        /// </summary>
        /// <param name="score">The score to add.</param>
        public void AddScore(Score score)
        {
            scores.Add(score);

            if (scores.Count > SCORES_CAPACITY)
            {
                sortScores();
                trimScores();
            }
        }

        /// <summary>
        /// Sorts the scores list from highest score to lowest score.
        /// </summary>
        private void sortScores()
        {
            scores.Sort(
                    (score1, score2) => (score1.Value.CompareTo(score2.Value)));
        }

        /// <summary>
        /// Trims the scores list to the appropriate scores capacity.
        /// </summary>
        private void trimScores()
        {
            scores.RemoveRange(
                SCORES_CAPACITY,
                scores.Count - SCORES_CAPACITY);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the sortes scores list.
        /// </summary>
        public List<Score> Scores
        {
            get
            {
                sortScores();
                return scores;
            }
        }

        #endregion
    }
}
