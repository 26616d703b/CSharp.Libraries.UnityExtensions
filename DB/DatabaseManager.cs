using System;
using System.IO;
using UnityEngine;

namespace UnityExtensions.DB
{
    public class DatabaseManager
    {
        #region Attributes

        private static DatabaseManager m_instance;
        private static readonly object m_lock = new object();

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public static DatabaseManager instance
        {
            get
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        m_instance = new DatabaseManager(Application.dataPath + "/Databases/localization.db",
                            Application.dataPath + "/Databases/user.db");
                    }

                    return m_instance;
                }
            }
        }

        public SQLiteDatabase localizationDatabase { get; private set; }

        public SQLiteDatabase userDatabase { get; private set; }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        public DatabaseManager(string localizationDatabaseFilePath, string userDatabaseFilePath)
        {
            if (!File.Exists(localizationDatabaseFilePath) || !File.Exists(userDatabaseFilePath))
                throw new Exception("One (or all) of the databases files are missing.");

            localizationDatabase = new SQLiteDatabase(localizationDatabaseFilePath);
            userDatabase = new SQLiteDatabase(userDatabaseFilePath);
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        #endregion Other Methods

        #endregion Methods
    }
}