using System;
using UnityExtensions.Localization;

namespace UnityExtensions.Localization
{
    public enum Locale
    {
        ar_DZ,
        en_US,
        fr_FR,
        es_ES
    }
}

public static class LocaleExtensions
{
    public static string Name(this Locale locale)
    {
        return Enum.GetName(locale.GetType(), locale);
    }

    public static string Language(this Locale locale)
    {
        switch (locale)
        {
            case Locale.ar_DZ:
                return "Arabic";

            case Locale.en_US:
                return "English";

            case Locale.fr_FR:
                return "French";

            case Locale.es_ES:
                return "Spanish";

            default:
                throw new NotImplementedException();
        }
    }

    public static string Title(this Locale locale)
    {
        switch (locale)
        {
            case Locale.ar_DZ:
                return "Arabic locale for Algeria";

            case Locale.en_US:
                return "English locale for the USA";

            case Locale.fr_FR:
                return "French locale for France";

            case Locale.es_ES:
                return "Spanish locale for Spain";

            default:
                throw new NotImplementedException();
        }
    }
}