using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWCF.SQL
{
    class TraitementsSQL
    {
        public static string DBStr(object valeur, bool trim = true)
        {
            if (object.ReferenceEquals(valeur, System.DBNull.Value))
                return "";
            if (string.IsNullOrEmpty(Convert.ToString(valeur)))
                return "";

            if (trim)
            {
                return Convert.ToString(valeur).Trim();
            }
            else
            {
                return Convert.ToString(valeur);
            }
        }

        public static double DBDbl(object valeur)
        {
            if (object.ReferenceEquals(valeur, System.DBNull.Value))
                return 0;

            double dbl = 0;
            string strDbl = null;

            strDbl = Convert.ToString(valeur);
            if (double.TryParse(strDbl, out dbl))
                return dbl;
            
            strDbl = Convert.ToString(valeur).Replace('.', ',');
            if (double.TryParse(strDbl, out dbl))
                return dbl;

            strDbl = Convert.ToString(valeur).Replace(',', '.');
            if (double.TryParse(strDbl, out dbl))
                return dbl;

            return 0;
        }

        public static int DBInt(object valeur)
        {
            if (object.ReferenceEquals(valeur, System.DBNull.Value))
                return 0;
            if (valeur == null)
                return 0;

            int i = 0;
            string strInt = null;

            strInt = Convert.ToString(valeur);
            if (int.TryParse(strInt, out i))
                return i;

            return 0;
        }

        public static bool DBBool(object valeur)
        {
            if (object.ReferenceEquals(valeur, System.DBNull.Value))
                return false;
            if (valeur == null)
                return false;

            string strBool = Convert.ToString(valeur).ToUpper();
            if (strBool == "1" || strBool == "VRAI" || strBool == "O" || strBool == "T" || strBool == "YES" || strBool == "Y" || strBool == "TRUE")
                return true;

            return false;
        }

        public static DateTime? DBDate(object valeur)
        {
            if (object.ReferenceEquals(valeur, System.DBNull.Value))
                return null;

            return Convert.ToDateTime(valeur);
        }

        public static string FSQL<TSelf>(TSelf valeur)
        {
            Type tValeur = null;

            if (valeur == null)
                tValeur = GetDeclaredType(valeur);
            else
                tValeur = valeur.GetType();

            return FSQL(valeur, tValeur);
        }
        private static string FSQL(object valeur, Type tValeur)
        {
            string strValeur = "";

            if (tValeur == typeof(DateTime) || tValeur == typeof(DateTime?))
            {
                if (valeur == null) return "null";
                strValeur = string.Format("Datetime('{0}')", ((DateTime)valeur).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else if (tValeur == typeof(string) || tValeur == typeof(char) || tValeur == typeof(char?))
            {
                if (valeur == null) return "''";
                strValeur = string.Format("'{0}'", ((string)valeur).Replace("'", "''"));
            }
            else if (tValeur == typeof(Single) || tValeur == typeof(double) || tValeur == typeof(decimal) || tValeur == typeof(Single?) || tValeur == typeof(double?) || tValeur == typeof(decimal?))
            {
                if (valeur == null) return "0";
                strValeur = valeur.ToString().Replace(",", ".");
            }
            else if (tValeur == typeof(bool) || tValeur == typeof(bool?))
            {
                if (valeur == null) return "0";
                strValeur = ((bool)valeur) ? "1" : "0";
            }
            else
            {
                if (valeur == null) return "";
                strValeur = valeur.ToString();
            }

            return strValeur;
        }

        static public Type GetDeclaredType<TSelf>(TSelf self)
        {
            return typeof(TSelf);
        }

        
    }
}
