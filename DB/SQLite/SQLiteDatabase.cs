using Community.CsharpSqlite;
using System;
using System.IO;

namespace UnityExtensions.DB
{
    public class SQLiteDatabase
    {
        #region Attributes

        private Sqlite3.sqlite3 m_connection;

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public string filePath { get; private set; }

        public Sqlite3.sqlite3 connection
        {
            get { return m_connection; }
        }

        public bool isOpen
        {
            get { return m_connection != null; }
        }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        public SQLiteDatabase(string filePath)
        {
            this.filePath = filePath;
            m_connection = null;
        }

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        // Create or open an existing database
        public void Open()
        {
            if (m_connection != null)
                throw new Exception("Error: the database already open!");

            if (Sqlite3.sqlite3_open(filePath, out m_connection) != Sqlite3.SQLITE_OK)
            {
                m_connection = null;

                throw new IOException("Error when opening database " + filePath + " !");
            }
        }

        public void Close()
        {
            if (m_connection != null)
            {
                Sqlite3.sqlite3_close(m_connection);

                m_connection = null;
            }
        }

        public SQLiteQuery CreateQuery(string sqlQuery)
        {
            return new SQLiteQuery(this, sqlQuery);
        }

        #endregion Other Methods

        #endregion Methods
    }
}