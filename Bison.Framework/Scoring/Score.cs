using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bison.Framework.Scoring
{
    /// <summary>
    /// Class representing a simple score.
    /// </summary>
    public class Score
    {
        #region Members

        public const string DEFAULT_NAME = "Unnamed";

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The score value.
        /// </summary>
        private int value;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new score with default values.
        /// </summary>
        public Score()
            :this (DEFAULT_NAME, 0)
        {
        }

        /// <summary>
        /// Creates a new score.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The score value.</param>
        public Score(string name, int value)
        {
            Name = name;
            Value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.name = DEFAULT_NAME;
                }
                else
                {
                    this.name = value;
                }
                
            }
        }

        /// <summary>
        /// Gets or sets the score value.
        /// </summary>
        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        #endregion
    }
}
