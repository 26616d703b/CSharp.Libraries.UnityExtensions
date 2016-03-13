using System;
using System.Collections.Generic;
using UnityExtensions.DB;

namespace UnityExtensions.Localization
{
    public class LocaleManager
    {
        #region Attributes

        private static LocaleManager m_instance;
        private static readonly object m_lock = new object();

        #endregion Attributes

        #region Methods

        #region Accessors and Mutators

        //_____________________________________________________________________ ACCESSORS AND MUTATORS _____________________________________________________________________

        public static LocaleManager instance
        {
            get
            {
                lock (m_lock)
                {
                    if (m_instance == null)
                    {
                        m_instance = new LocaleManager();
                    }

                    return m_instance;
                }
            }
        }

        public Locale currentLocale { get; private set; }

        #endregion Accessors and Mutators

        #region Inherited Methods

        //_______________________________________________________________________ INHERITED METHODS _______________________________________________________________________

        #endregion Inherited Methods

        #region Events

        //_____________________________________________________________________________ EVENTS _____________________________________________________________________________

        public delegate void LocaleChangeEventHandler();

        public event LocaleChangeEventHandler OnLocaleChange;

        #endregion Events

        #region Other Methods

        //__________________________________________________________________________ OTHER METHODS _________________________________________________________________________

        public List<Locale> AvailableLocales()
        {
            var locales = new List<Locale>();

            var database = DatabaseManager.instance.localizationDatabase;
            database.Open();

            var query = database.CreateQuery("SELECT * FROM Locale");

            while (query.Step())
            {
                locales.Add((Locale)Enum.Parse(typeof(Locale), query.GetString("Key")));
            }

            query.Release();

            database.Close();

            return locales;
        }

        public void ChangeLocale(Locale newLocale)
        {
            currentLocale = newLocale;

            OnLocaleChange();
        }

        public string GetValue(string key)
        {
            var value = string.Empty;

            var database = DatabaseManager.instance.localizationDatabase;
            database.Open();

            var query =
                database.CreateQuery("SELECT Value FROM Content WHERE Key='" + key + "' AND Locale='" +
                                     currentLocale + "'");
            query.Step();

            value = query.GetString("Value");

            query.Release();
            database.Close();

            return value;
        }

        #endregion Other Methods

        #endregion Methods
    }
}