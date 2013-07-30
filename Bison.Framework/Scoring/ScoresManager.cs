using Bison.Framework.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Xml;

namespace Bison.Framework.Scoring
{
    /// <summary>
    /// The scores manager class.
    /// </summary>
    public class ScoresManager
    {
        #region Members

        /// <summary>
        /// The name of the scores file.
        /// </summary>
        public const string SCORES_FILE = "scores.xml";

        /// <summary>
        /// The singleton scores manager instance.
        /// </summary>
        private static ScoresManager instance;

        /// <summary>
        /// The scores list dictionary.
        /// </summary>
        private SerializableDictionary<string, ScoreList> scoreLists = new SerializableDictionary<string, ScoreList>();

        #endregion

        #region Constructors

        private ScoresManager()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a new score to the lists.
        /// </summary>
        /// <param name="type">The list type.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The score value.</param>
        public void AddScores(string type, string name, int value)
        {
            if (!scoreLists.ContainsKey(type))
            {
                scoreLists.Add(type, new ScoreList());
            }

            scoreLists[type].AddScore(new Score(name, value));
        }

        /// <summary>
        /// Gets the scores of the given list type.
        /// </summary>
        /// <param name="type">The list type.</param>
        /// <returns>The scores list.</returns>
        public List<Score> GetScores(string type)
        {
            if (scoreLists.ContainsKey(type))
            {
                return scoreLists[type].Scores;
            }

            return new List<Score>();
        }

        /// <summary>
        /// Gets the current high score of the list.
        /// </summary>
        /// <param name="type">The list type.</param>
        /// <returns></returns>
        public int GetHighScore(string type)
        {
            var scores = GetScores(type);

            if (scores.Count > 0)
            {
                return GetScores(type)[0].Value;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Loads the scores lists.
        /// </summary>
        public void Load()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists(SCORES_FILE))
                {
                    using (IsolatedStorageFileStream fileStream = storage.OpenFile(SCORES_FILE, FileMode.Open))
                    {
                        using (XmlReader reader = XmlReader.Create(fileStream))
                        {
                            scoreLists.ReadXml(reader);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Saves the scores lists.
        /// </summary>
        public void Save()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fileStream = storage.CreateFile(SCORES_FILE))
                {
                    using (XmlWriter writer = XmlWriter.Create(fileStream))
                    {
                        scoreLists.WriteXml(writer);
                    }
                }
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton scores manager instance.
        /// </summary>
        public ScoresManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScoresManager();
                }

                return instance;
            }
        }

        #endregion
    }
}
