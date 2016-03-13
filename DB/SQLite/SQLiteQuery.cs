using Community.CsharpSqlite;
using System;

namespace UnityExtensions.DB
{
    public class SQLiteQuery
    {
        #region Attributes

        private string[] m_columnNames;
        private int[] m_columnTypes;
        private readonly Sqlite3.sqlite3 m_connection;
        private readonly Sqlite3.Vdbe m_vm;

        #endregion Attributes

        #region Methods

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        public SQLiteQuery(SQLiteDatabase database, string query)
        {
            m_connection = database.connection;

            if (Sqlite3.sqlite3_prepare_v2(m_connection, query, query.Length, ref m_vm, 0) != Sqlite3.SQLITE_OK)
                throw new Exception("Error with prepare query! error:" + Sqlite3.sqlite3_errmsg(m_connection));
        }

        #endregion Inherited Methods

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________
        public void Release()
        {
            if (Sqlite3.sqlite3_reset(m_vm) != Sqlite3.SQLITE_OK)
                throw new Exception("Error with sqlite3_reset!");

            if (Sqlite3.sqlite3_finalize(m_vm) != Sqlite3.SQLITE_OK)
                throw new Exception("Error with sqlite3_finalize!");
        }

        public bool Step()
        {
            switch (Sqlite3.sqlite3_step(m_vm))
            {
                case Sqlite3.SQLITE_DONE:
                    return false;

                case Sqlite3.SQLITE_ROW:
                    {
                        var columnCount = Sqlite3.sqlite3_column_count(m_vm);
                        m_columnNames = new string[columnCount];
                        m_columnTypes = new int[columnCount];

                        try
                        {
                            // reads columns one by one
                            for (var i = 0; i < columnCount; i++)
                            {
                                m_columnNames[i] = Sqlite3.sqlite3_column_name(m_vm, i);
                                m_columnTypes[i] = Sqlite3.sqlite3_column_type(m_vm, i);
                            }
                        }
                        catch
                        {
                            throw new Exception("SQLite fail to read column's names and types! error: " +
                                                Sqlite3.sqlite3_errmsg(m_connection));
                        }

                        return true;
                    }
            }

            throw new Exception("SQLite step fail! error: " + Sqlite3.sqlite3_errmsg(m_connection));
        }

        private int GetFieldIndex(string field)
        {
            for (var i = 0; i < m_columnNames.Length; i++)
            {
                if (m_columnNames[i] == field)
                    return i;
            }

            throw new Exception("SQLite unknown field name: " + field);
        }

        public bool IsNull(string field)
        {
            var i = GetFieldIndex(field);

            return Sqlite3.SQLITE_NULL == m_columnTypes[i];
        }

        public byte[] GetBlob(string field)
        {
            var i = GetFieldIndex(field);

            if (Sqlite3.SQLITE_BLOB == m_columnTypes[i])
                return Sqlite3.sqlite3_column_blob(m_vm, i);

            throw new Exception("SQLite wrong field type (expecting byte[]) : " + field);
        }

        public double GetDouble(string field)
        {
            var i = GetFieldIndex(field);

            if (Sqlite3.SQLITE_FLOAT == m_columnTypes[i])
                return Sqlite3.sqlite3_column_double(m_vm, i);

            throw new Exception("SQLite wrong field type (expecting Double) : " + field);
        }

        public int GetInteger(string field)
        {
            var i = GetFieldIndex(field);

            if (Sqlite3.SQLITE_INTEGER == m_columnTypes[i])
                return Sqlite3.sqlite3_column_int(m_vm, i);

            throw new Exception("SQLite wrong field type (expecting Integer) : " + field);
        }

        public string GetString(string field)
        {
            var i = GetFieldIndex(field);

            if (Sqlite3.SQLITE_TEXT == m_columnTypes[i])
                return Sqlite3.sqlite3_column_text(m_vm, i);

            throw new Exception("SQLite wrong field type (expecting String) : " + field);
        }
    }

    #endregion Other Methods

    #endregion Methods
}