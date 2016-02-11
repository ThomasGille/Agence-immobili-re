using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace ServiceWCF.SQL
{
    class GestionnaireSQL : IDisposable
    {
        private SQLiteConnection _cnx;
        private String _cheminBase;
        private SQLiteCommand _commande;
        private SQLiteTransaction _transaction;

        public bool IsConnected
        {
            get { return _cnx.State == ConnectionState.Open; }
        }

        public GestionnaireSQL(String cheminBase)
        {
            _cheminBase = cheminBase;
            _cheminBase = _cheminBase.Replace(@"\", @"/");
            _cnx = new SQLiteConnection("Data Source=" + _cheminBase + ";Version=3");
            _commande = new SQLiteCommand(_cnx);
        }

        public void AddParameter(String colonne, object valeur) {
            if (valeur == null)
                _commande.Parameters.AddWithValue(colonne, DBNull.Value);
            else
                _commande.Parameters.AddWithValue(colonne, valeur);
        }

        public DataSet ExecuteReader(string sqlRequest) {
            DataSet rslt = null;
            
            try {
                if (sqlRequest == String.Empty)
                    throw new Exception("Aucune requête SQL à executer.");
                
                _commande.CommandText = sqlRequest;
                _commande.Transaction = _transaction;

                var adapter = new SQLiteDataAdapter(_commande);

                rslt = new DataSet();
                adapter.Fill(rslt);
            }
            catch (Exception e) {
                throw new ExceptionSQL(e.Message, sqlRequest, e);
            }
            return rslt;
        }

        public int ExecuteNonQuery(string sqlRequest) {
            try {
                if (sqlRequest == String.Empty)
                    throw new Exception("Aucune requête SQL à executer.");

                _commande.CommandText = sqlRequest;
                _commande.Transaction = _transaction;

                return _commande.ExecuteNonQuery();
            }
            catch (Exception e) {
                throw new ExceptionSQL(e.Message, sqlRequest, e);
            }
        }

        public void BeginTransaction() {
            _transaction = _cnx.BeginTransaction();
        }

        public void Commit() {
            _transaction.Commit();
            _transaction = null;
        }

        public void Rollback() {
            if (_transaction != null)
                _transaction.Rollback();
            _transaction = null;
        }
        
        public void Open() {
            _cnx.Open();
        }

        public void Close() {
            _cnx.Close();
        }

        public void Dispose() {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            _commande.Dispose();
            _cnx.Close();
            _cnx.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
