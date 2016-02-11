using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceWCF.SQL
{
    class ExceptionSQL : Exception
    {
        private string _requeteSql;

        public ExceptionSQL(string message)
            : base(message)
        {
        }

        public ExceptionSQL(string message, string requeteSQL, Exception inner)
            : base(String.Format("{0}\r\nSQL : {1}", message, requeteSQL), inner)
        {
            _requeteSql = requeteSQL;
        }
    }
}
