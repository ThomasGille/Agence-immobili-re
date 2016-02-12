using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ServiceWCF.SQL;
using ServiceWCF.DataContracts;

namespace ServiceWCF.SQL
{
    class GestionnaireBDD
    {
        
        #region Constantes

        public class Const
        {
            public const string DEFAULT_DB_FILENAME = "agence.db";

            public const int INDEX_TABLENAME = 0;
            public const int INDEX_TABLECOLUMNS = 1;

            public const int INDEX_COLUMNNAME = 0;
            public const int INDEX_TYPE = 1;
            public const int INDEX_PRIMARYKEY = 2;
            public const int INDEX_INFOS = 3;
            public const string PRIMARYKEY_VALUE = "PK";

            public static readonly string[] DB_SYSTEM_TABLES = { "sqlite_sequence" };

            public static readonly object[][] DB_REFERENCE_TABLES = { new object[] { DB_BIEN_TABLENAME,
                                                                                     new string[][] { new string[] { DB_BIEN_ID_COLNAME, "INTEGER", PRIMARYKEY_VALUE, "AUTOINCREMENT" }, 
                                                                                                      new string[] { DB_BIEN_TITRE_COLNAME, "TEXT", "", "" }, 
                                                                                                      new string[] { DB_BIEN_TYPETRANSACTION_COLNAME, "INTEGER", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_TYPEBIEN_COLNAME, "INTEGER", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_DESCRIPTION_COLNAME, "TEXT", "", "" },
                                                                                                      new string[] { DB_BIEN_PRIX_COLNAME, "DOUBLE", "", "NOT NULL" },
                                                                                                      new string[] { DB_BIEN_MONTANTCHARGES_COLNAME, "DOUBLE", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_SURFACE_COLNAME, "DOUBLE", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_NBPIECES_COLNAME, "INTEGER", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_NUMETAGE_COLNAME, "INTEGER", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_NBETAGES_COLNAME, "INTEGER", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_TYPECHAUFFAGE_COLNAME, "INTEGER", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_ENERGIECHAUFFAGE_COLNAME, "INTEGER", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_DATEMISEENTRANSACTION_COLNAME, "DATETIME", "", "" }, 
                                                                                                      new string[] { DB_BIEN_TRANSACTIONEFFECTUEE_COLNAME, "INTEGER", "", "NOT NULL" }, 
                                                                                                      new string[] { DB_BIEN_DATETRANSACTION_COLNAME, "DATETIME", "", ""},
                                                                                                      new string[] { DB_BIEN_ADRESSE_COLNAME, "TEXT", "", "" }, 
                                                                                                      new string[] { DB_BIEN_CODEPOSTAL_COLNAME, "TEXT", "", "" },
                                                                                                      new string[] { DB_BIEN_VILLE_COLNAME, "TEXT", "", "" } } },
                                                                      new object[] { DB_PHOTO_TABLENAME,
                                                                                     new string[][] { new string[] { DB_PHOTO_IDBIEN_COLNAME, "INTEGER", PRIMARYKEY_VALUE, "NOT NULL" }, 
                                                                                                      new string[] { DB_PHOTO_NUMPHOTO_COLNAME, "TEXT", PRIMARYKEY_VALUE, "NOT NULL" }, 
                                                                                                      new string[] { DB_PHOTO_BASE64_COLNAME, "TEXT", "", "NOT NULL" } } } };

            public const string DB_BIEN_TABLENAME = "bien";
            public const string DB_BIEN_ID_COLNAME = "id";
            public const string DB_BIEN_TITRE_COLNAME = "titre";
            public const string DB_BIEN_TYPETRANSACTION_COLNAME = "type_transaction";
            public const string DB_BIEN_TYPEBIEN_COLNAME = "type_bien";
            public const string DB_BIEN_DESCRIPTION_COLNAME = "description";
            public const string DB_BIEN_PRIX_COLNAME = "prix";
            public const string DB_BIEN_MONTANTCHARGES_COLNAME = "montant_charges";
            public const string DB_BIEN_SURFACE_COLNAME = "surface";
            public const string DB_BIEN_NBPIECES_COLNAME = "nombre_pieces";
            public const string DB_BIEN_NUMETAGE_COLNAME = "numero_etage";
            public const string DB_BIEN_NBETAGES_COLNAME = "nombre_etages";
            public const string DB_BIEN_TYPECHAUFFAGE_COLNAME = "type_chauffage";
            public const string DB_BIEN_ENERGIECHAUFFAGE_COLNAME = "energie_chauffage";
            public const string DB_BIEN_DATEMISEENTRANSACTION_COLNAME = "date_mise_en_transaction";
            public const string DB_BIEN_TRANSACTIONEFFECTUEE_COLNAME = "transaction_effectuee";
            public const string DB_BIEN_DATETRANSACTION_COLNAME = "date_transaction";
            public const string DB_BIEN_IDBIEN_COLNAME = "id_bien";
            public const string DB_BIEN_ADRESSE_COLNAME = "adresse";
            public const string DB_BIEN_CODEPOSTAL_COLNAME = "code_postal";
            public const string DB_BIEN_VILLE_COLNAME = "ville";

            public const string DB_PHOTO_TABLENAME = "photo";
            public const string DB_PHOTO_IDBIEN_COLNAME = "id_bien";
            public const string DB_PHOTO_NUMPHOTO_COLNAME = "numero_photo";
            public const string DB_PHOTO_BASE64_COLNAME = "base64";
        }

        #endregion

        
        private string _errorMessage;
        private string _databasePath;
        private GestionnaireSQL _cnx;

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        internal GestionnaireBDD(string databasePath)
        {
            this._errorMessage = "";

            try
            {
                this._databasePath = (databasePath == "") ? System.IO.Path.Combine(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL"), Const.DEFAULT_DB_FILENAME) : databasePath;
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return;
            }

            try
            {
                this._cnx = new GestionnaireSQL(this._databasePath);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
            }
        }


        public bool ConnexionBDD(bool validerBdd = true)
        {
            _errorMessage = "";

            try
            {
                if (_cnx.IsConnected)
                    _cnx.Close();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
            
            try
            {
                _cnx.Open();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            if (validerBdd) return ValiderBDD();

            return true;
        }

        public bool DeconnexionBDD()
        {
            _errorMessage = "";

            try
            {
                if (_cnx.IsConnected)
                    _cnx.Close();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            return true;
        }

        public bool ValiderBDD()
        {
            _errorMessage = "";

            // Vérification que la connexion est bien ouverte
            if (!_cnx.IsConnected) {
                _errorMessage="La connexion n'est pas ouverte !";
                return false;
            }

            // Vérification de l'intégrité de la base
            return VerifierIntegriteBase(Const.DB_REFERENCE_TABLES);
        }

        public ListeBiensImmobiliers LireContenuBDD(CriteresRechercheBiensImmobiliers criteres, int? page, int? nbBiens)
        {
            string req = "";
            string where = "";
            string limit = "";
            DataSet ds = null;
            ListeBiensImmobiliers liste = new ListeBiensImmobiliers();

            _errorMessage = "";

            // Vérification que la connexion est bien ouverte
            if (!_cnx.IsConnected)
            {
                _errorMessage = "La connexion n'est pas ouverte !";
                return null;
            }

            // Génération de l'expression SQL LIMIT et calcul des compteurs
            int nbBiensTotal, nbPages, numBien;
            limit = GenererLimit(ref page, ref nbBiens, out nbBiensTotal, out nbPages, out numBien);
            if (limit == null) return null;

            // Génération de la clause SQL WHERE selon les critères
            where = GenererWhere(criteres);

            // Génération de la requête
            req = string.Format("SELECT *, ({0}) AS {1} FROM {2} {3} {4} {5}",
                                GenererSelectionPhotoPrincipale(),
                                Const.DB_PHOTO_BASE64_COLNAME,
                                Const.DB_BIEN_TABLENAME,
                                where,
                                limit,
                                (criteres == null) ? "" : GenererOrderBy(criteres.TriPrincipal, criteres.Tris));
            
            // Exécution de la requête
            try
            {
                ds = _cnx.ExecuteReader(req);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return null;
            }

            // Affectation des résultats
            try
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    BienImmobilierBase bien;
                    ChargerBienImmobilier(dr, out bien);
                    bien.PhotoPrincipaleBase64 = TraitementsSQL.DBStr(dr[Const.DB_PHOTO_BASE64_COLNAME]);
                    liste.List.Add(bien);
                }

                liste.Page = (int)page;
                liste.PagesCount = nbPages;
                liste.Start = numBien + 1;
                liste.TakeCount = (int)nbBiens;
                liste.TotalCount = nbBiensTotal;

                // Lecture du nombre de lignes retrounées selon les critères
                req = string.Format("SELECT COUNT(*) FROM {0} {1}",
                                    Const.DB_BIEN_TABLENAME,
                                    where);
                try
                {
                    ds = _cnx.ExecuteReader(req);
                    liste.SelectCount = (ds.Tables[0].Rows.Count > 0) ? TraitementsSQL.DBInt(ds.Tables[0].Rows[0][0]) : 0;
                }
                catch (Exception ex)
                {
                    _errorMessage = ex.Message;
                    liste.SelectCount = 0;
                    return liste;
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return null;
            }

            return liste;
        }

        public BienImmobilier LireDetailsBienImmobilier(int id)
        {
            string req = "";
            DataSet ds = null;
            BienImmobilier bien = null;

            _errorMessage = "";

            // Vérification que la connexion est bien ouverte
            if (!_cnx.IsConnected)
            {
                _errorMessage = "La connexion n'est pas ouverte !";
                return null;
            }

            // Génération de la requête
            req = string.Format("SELECT * FROM {0} WHERE {1}={2}",
                                Const.DB_BIEN_TABLENAME,
                                Const.DB_BIEN_ID_COLNAME,
                                TraitementsSQL.FSQL(id));

            // Exécution de la requête
            try
            {
                ds = _cnx.ExecuteReader(req);
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    _errorMessage = string.Format("Aucun bien immobilier n'a été trouvé avec l'identifiant n°{0}.", id);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return null;
            }

            // Affectation des détails du bien
            try
            {
                ChargerBienImmobilier(ds.Tables[0].Rows[0], out bien);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return null;
            }

            // Lecture des photos
            if (!LirePhotos(id, ref bien))
                return null;

            return bien;
        }

        public bool AjouterBienImmobilier(BienImmobilier bien)
        {
            string req = "";

            _errorMessage = "";

            // Vérification que la connexion est bien ouverte
            if (!_cnx.IsConnected)
            {
                _errorMessage = "La connexion n'est pas ouverte !";
                return false;
            }

            // Insertion du bien
            req = string.Format("INSERT INTO {0} ({1}, {3}, {5}, {7}, {9},  {11}, {13}, {15}, {17}, {19}, {21}, {23}, {25}, {27}, {29}, {31}, {33}, {35})" +
                                "         VALUES ({2}, {4}, {6}, {8}, {10}, {12}, {14}, {16}, {18}, {20}, {22}, {24}, {26}, {28}, {30}, {32}, {34}, {36})",
                                Const.DB_BIEN_TABLENAME,
                                Const.DB_BIEN_TITRE_COLNAME, TraitementsSQL.FSQL(bien.Titre),
                                Const.DB_BIEN_TYPETRANSACTION_COLNAME, TraitementsSQL.FSQL((int)bien.TypeTransaction),
                                Const.DB_BIEN_TYPEBIEN_COLNAME, TraitementsSQL.FSQL((int)bien.TypeBien),
                                Const.DB_BIEN_DESCRIPTION_COLNAME, TraitementsSQL.FSQL(bien.Description),
                                Const.DB_BIEN_PRIX_COLNAME, TraitementsSQL.FSQL(bien.Prix),
                                Const.DB_BIEN_MONTANTCHARGES_COLNAME, TraitementsSQL.FSQL(bien.MontantCharges),
                                Const.DB_BIEN_SURFACE_COLNAME, TraitementsSQL.FSQL(bien.Surface),
                                Const.DB_BIEN_NBPIECES_COLNAME, TraitementsSQL.FSQL(bien.NbPieces),
                                Const.DB_BIEN_NUMETAGE_COLNAME, TraitementsSQL.FSQL(bien.NumEtage),
                                Const.DB_BIEN_NBETAGES_COLNAME, TraitementsSQL.FSQL(bien.NbEtages),
                                Const.DB_BIEN_TYPECHAUFFAGE_COLNAME, TraitementsSQL.FSQL((int)bien.TypeChauffage),
                                Const.DB_BIEN_ENERGIECHAUFFAGE_COLNAME, TraitementsSQL.FSQL((int)bien.EnergieChauffage),
                                Const.DB_BIEN_DATEMISEENTRANSACTION_COLNAME, TraitementsSQL.FSQL(bien.DateMiseEnTransaction),
                                Const.DB_BIEN_TRANSACTIONEFFECTUEE_COLNAME, TraitementsSQL.FSQL(bien.TransactionEffectuee),
                                Const.DB_BIEN_DATETRANSACTION_COLNAME, TraitementsSQL.FSQL(bien.DateTransaction),
                                Const.DB_BIEN_ADRESSE_COLNAME, TraitementsSQL.FSQL(bien.Adresse),
                                Const.DB_BIEN_CODEPOSTAL_COLNAME, TraitementsSQL.FSQL(bien.CodePostal),
                                Const.DB_BIEN_VILLE_COLNAME, TraitementsSQL.FSQL(bien.Ville));
            try
            {
                _cnx.ExecuteNonQuery(req);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            // Lecture de l'identifiant
            int idBien = -1;
            req = string.Format("SELECT MAX({0}) FROM {1}",
                                Const.DB_BIEN_ID_COLNAME,
                                Const.DB_BIEN_TABLENAME);
            try
            {
                DataSet ds = _cnx.ExecuteReader(req);
                idBien = TraitementsSQL.DBInt(ds.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            // Insertion des photos
            if (!AjouterPhotos(idBien, bien.PhotoPrincipaleBase64, bien.PhotosBase64)) return false;

            return true;
        }

        public bool ModifierBienImmobilier(BienImmobilier bien)
        {
            string req = "";

            _errorMessage = "";

            // Vérification que la connexion est bien ouverte
            if (!_cnx.IsConnected)
            {
                _errorMessage = "La connexion n'est pas ouverte !";
                return false;
            }

            // Ouverture d'une transaction
            try
            {
                _cnx.BeginTransaction();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            // Modification du bien
            req = string.Format("UPDATE {0} SET {1}={2}, {3}={4}, {5}={6}, {7}={8}, {9}={10}, {11}={12}, {13}={14}, {15}={16}, " +
                                "               {17}={18}, {19}={20}, {21}={22}, {23}={24}, {25}={26}, {27}={28}, {29}={30}, "+
                                "               {31}={32}, {33}={34}, {35}={36} " +
                                "WHERE {37}={38}",
                                Const.DB_BIEN_TABLENAME,
                                Const.DB_BIEN_TITRE_COLNAME, TraitementsSQL.FSQL(bien.Titre),
                                Const.DB_BIEN_TYPETRANSACTION_COLNAME, TraitementsSQL.FSQL((int)bien.TypeTransaction),
                                Const.DB_BIEN_TYPEBIEN_COLNAME, TraitementsSQL.FSQL((int)bien.TypeBien),
                                Const.DB_BIEN_DESCRIPTION_COLNAME, TraitementsSQL.FSQL(bien.Description),
                                Const.DB_BIEN_PRIX_COLNAME, TraitementsSQL.FSQL(bien.Prix),
                                Const.DB_BIEN_MONTANTCHARGES_COLNAME, TraitementsSQL.FSQL(bien.MontantCharges),
                                Const.DB_BIEN_SURFACE_COLNAME, TraitementsSQL.FSQL(bien.Surface),
                                Const.DB_BIEN_NBPIECES_COLNAME, TraitementsSQL.FSQL(bien.NbPieces),
                                Const.DB_BIEN_NUMETAGE_COLNAME, TraitementsSQL.FSQL(bien.NumEtage),
                                Const.DB_BIEN_NBETAGES_COLNAME, TraitementsSQL.FSQL(bien.NbEtages),
                                Const.DB_BIEN_TYPECHAUFFAGE_COLNAME, TraitementsSQL.FSQL((int)bien.TypeChauffage),
                                Const.DB_BIEN_ENERGIECHAUFFAGE_COLNAME, TraitementsSQL.FSQL((int)bien.EnergieChauffage),
                                Const.DB_BIEN_DATEMISEENTRANSACTION_COLNAME, TraitementsSQL.FSQL(bien.DateMiseEnTransaction),
                                Const.DB_BIEN_TRANSACTIONEFFECTUEE_COLNAME, TraitementsSQL.FSQL(bien.TransactionEffectuee),
                                Const.DB_BIEN_DATETRANSACTION_COLNAME, TraitementsSQL.FSQL(bien.DateTransaction),
                                Const.DB_BIEN_ADRESSE_COLNAME, TraitementsSQL.FSQL(bien.Adresse),
                                Const.DB_BIEN_CODEPOSTAL_COLNAME, TraitementsSQL.FSQL(bien.CodePostal),
                                Const.DB_BIEN_VILLE_COLNAME, TraitementsSQL.FSQL(bien.Ville),
                                Const.DB_BIEN_ID_COLNAME, TraitementsSQL.FSQL(bien.Id));
            try
            {
                _cnx.ExecuteNonQuery(req);
            }
            catch (Exception ex)
            {
                _cnx.Rollback();
                _errorMessage = ex.Message;
                return false;
            }

            // Suppression des photos
            if (!SupprimerPhotos(bien.Id))
            {
                _cnx.Rollback();
                return false;
            }

            // Insertion des photos
            if (!AjouterPhotos(bien.Id, bien.PhotoPrincipaleBase64, bien.PhotosBase64))
            {
                _cnx.Rollback();
                return false;
            }

            // Fermeture de la transaction
            try
            {
                _cnx.Commit();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            return true;
        }

        public bool SupprimerBienImmobilier(int id)
        {
            string req = "";

            _errorMessage = "";

            // Vérification que la connexion est bien ouverte
            if (!_cnx.IsConnected)
            {
                _errorMessage = "La connexion n'est pas ouverte !";
                return false;
            }

            // Ouverture d'une transaction
            try
            {
                _cnx.BeginTransaction();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            // Suppression du bien
            req = string.Format("DELETE FROM {0} WHERE {1}={2}",
                                Const.DB_BIEN_TABLENAME,
                                Const.DB_BIEN_ID_COLNAME,
                                TraitementsSQL.FSQL(id));
            try
            {
                _cnx.ExecuteNonQuery(req);
            }
            catch (Exception ex)
            {
                _cnx.Rollback();
                _errorMessage = ex.Message;
                return false;
            }

            // Suppression des photos
            if (!SupprimerPhotos(id))
            {
                _cnx.Rollback();
                return false;
            }

            // Fermeture de la transaction
            try
            {
                _cnx.Commit();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            return true;
        }



        #region Gestion des tables

        private bool VerifierIntegriteBase(object[][] tables)
        {
            string req = "";
            DataSet ds = null;

            // Lecture des tables exitantes dans la base de données
            req = "SELECT name FROM sqlite_master WHERE type='table'";
            try
            {
                ds = _cnx.ExecuteReader(req);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }


            // On compare la liste des tables de référence avec la liste des colonnes existantes :

            // Pour chaque table de la liste de référence
            foreach (object[] table in tables)
            {
                string nomTable = (string)table[Const.INDEX_TABLENAME];

                // On regarde si cette table existe réellement dans la base
                bool tableExiste = false;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (TraitementsSQL.DBStr(dr["name"]).ToLower() == nomTable.ToLower())
                    {
                        tableExiste = true;
                        break;
                    }
                }

                // Vérifie l'intégrité de cette table
                if (!VerifierIntegriteTable(nomTable, (string[][])table[Const.INDEX_TABLECOLUMNS], tableExiste)) return false;
            }

            // Pour chaque table de la base
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string nomTable = TraitementsSQL.DBStr(dr["name"]).ToLower();

                // On regarde si cette table est dans les références
                bool tableExiste = Const.DB_SYSTEM_TABLES.Contains(nomTable);
                if (!tableExiste)
                {
                    foreach (object[] table in tables)
                    {
                        if (nomTable == ((string)table[Const.INDEX_TABLENAME]).ToLower())
                        {
                            tableExiste = true;
                            break;
                        }
                    }
                }

                // Si la table n'est pas référencée, on la supprime
                if (!tableExiste)
                {
                    req = "DROP TABLE " + nomTable;
                    try
                    {
                        _cnx.ExecuteNonQuery(req);
                    }
                    catch (Exception ex)
                    {
                        _errorMessage = ex.Message;
                        return false;
                    }
                }
            }

            return true;
        }

        private bool VerifierIntegriteTable(string nom, string[][] colonnes, bool existe)
        {
            if (existe)
            {
                // Si la table existe, on vérifie les colonnes
                if (!VerifierColonnes(nom, colonnes)) return false;
            }
            else
            {
                // Si la table n'existe pas, on la crée
                if (!CreerTable(nom, colonnes)) return false;
            }

            return true;
        }

        private bool CreerTable(string nom, string[][] colonnes)
        {
            string req = "";
            int nbCles = 0;

            // Vérification du nombre de colonnes
            if (colonnes.Length <= 0) {
                this._errorMessage = string.Format("Impossible de créer une table ne possédant aucune colonne ! (table : {0})", nom);
                return false;
            }

            // Calcul du nombre de clés primaires
            foreach (string[] colonne in colonnes) {
                if (colonne[Const.INDEX_PRIMARYKEY].ToLower() == Const.PRIMARYKEY_VALUE.ToLower()) nbCles++;
            }
            if (nbCles == 0) {
                this._errorMessage = string.Format("Impossible de créer une table ne possédant aucune clé primaire ! (table : {0})", nom);
                return false;
            }

            // Génération de la requête
            req = string.Format("CREATE TABLE {0}(", nom);
            foreach (string[] colonne in colonnes) {
                if (!req.EndsWith("(")) req += ", ";
                req += colonne[Const.INDEX_COLUMNNAME] + " ";
                req += colonne[Const.INDEX_TYPE] + " ";
                if(nbCles == 1 && colonne[Const.INDEX_PRIMARYKEY].ToLower() == Const.PRIMARYKEY_VALUE.ToLower()) req += "PRIMARY KEY ";
                req += colonne[Const.INDEX_INFOS];
            }
            if (nbCles > 1) {
                req += ", PRIMARY KEY (";
                foreach (string[] colonne in colonnes) {
                    if (colonne[Const.INDEX_PRIMARYKEY].ToLower() == Const.PRIMARYKEY_VALUE.ToLower()) {
                        if (!req.EndsWith("(")) req += ", ";
                        req += colonne[Const.INDEX_COLUMNNAME] + " ";
                    }
                }
                req += ")";
            }
            req += ")";

            // Exécution de la requête
            try
            {
                _cnx.ExecuteNonQuery(req);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            return true;
        }

        private bool VerifierColonnes(string nom, string[][] colonnes)
        {
            string req = "";
            DataSet ds = null;

            // Lecture des colonnes existantes
            req = string.Format("PRAGMA table_info('{0}')", nom);
            try
            {
                ds = _cnx.ExecuteReader(req);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }


            // On compare la liste des colonnes de référence avec la liste des colonnes existantes :

            // Pour chaque colonne de la liste de référence
            foreach (string[] colonne in colonnes)
            {
                // On regarde si cette colonne existe réellement dans la table
                bool colonneExiste = false;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (TraitementsSQL.DBStr(dr["name"]).ToLower() == colonne[Const.INDEX_COLUMNNAME].ToLower())
                    {
                        colonneExiste = true;
                        break;
                    }
                }

                // Si la colonne n'existe pas, on l'ajoute
                if (!colonneExiste)
                {
                    req = string.Format("ALTER TABLE {0} ADD COLUMN {1} {2}", 
                                        nom, 
                                        colonne[Const.INDEX_COLUMNNAME],
                                        colonne[Const.INDEX_TYPE],
                                        colonne[Const.INDEX_INFOS]);
                    try
                    {
                        _cnx.ExecuteNonQuery(req);
                    }
                    catch (Exception ex)
                    {
                        _errorMessage = ex.Message;
                        return false;
                    }
                }
            }

            // Pour chaque colonne de la table, on compte le nombre de colonnes non référencée
            int nbColNonReferencees = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                // On regarde si cette colonne est dans les références
                bool colonneExiste = false;
                foreach (string[] colonne in colonnes)
                {
                    if (TraitementsSQL.DBStr(dr["name"]).ToLower() == colonne[Const.INDEX_COLUMNNAME].ToLower())
                    {
                        colonneExiste = true;
                        break;
                    }
                }

                // Si la colonne n'est pas référencée, on la compte
                if (!colonneExiste)
                    nbColNonReferencees++;
            }

            // S'il existe des colonnes non référencées, il faut recopier la table
            if (nbColNonReferencees > 0) {
                if (!RecopierTable(nom, colonnes)) return false;
            }

            return true;
        }

        private bool RecopierTable(string nom, string[][] colonnes)
        {
            // Dans certains cas, nous avons besoin de renommer une table, créer une nouvelle table, copier les données de l'une à l'autre, et supprimer l'ancienne table.
            string req = "";

            // On commence une transaction
            try
            {
                _cnx.BeginTransaction();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
            
            // On renomme l'ancienne table
            req = string.Format("ALTER TABLE {0} RENAME TO {0}_old", nom);
            try
            {
                _cnx.ExecuteNonQuery(req);
            }
            catch (Exception ex)
            {
                _cnx.Rollback();
                _errorMessage = ex.Message;
                return false;
            }

            // On crée la nouvelle table
            if (!CreerTable(nom, colonnes)) return false;

            // On copie les données
            string cols = "";
            foreach (string[] colonne in colonnes)
            {
                if (!colonne[Const.INDEX_INFOS].ToLower().Contains("autoincrement"))
                {
                    if (cols != "") cols += ", ";
                    cols += colonne[Const.INDEX_COLUMNNAME];
                }
            }
            req = string.Format("INSERT INTO {0} ({1}) SELECT {1} FROM {0}_old", nom, cols);
            try
            {
                _cnx.ExecuteNonQuery(req);
            }
            catch (Exception ex)
            {
                _cnx.Rollback();
                _errorMessage = ex.Message;
                return false;
            }

            // On supprime l'ancienne table
            req = string.Format("DROP TABLE {0}_old", nom);
            try
            {
                _cnx.ExecuteNonQuery(req);
            }
            catch (Exception ex)
            {
                _cnx.Rollback();
                _errorMessage = ex.Message;
                return false;
            }

            // On valide la transaction
            try
            {
                _cnx.Commit();
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }
            
            return true;
        }

        #endregion

        #region Génération de la requête de recherche

        private string GenererSelectionPhotoPrincipale()
        {
            return string.Format("SELECT {0} FROM {1} " +
                                 "WHERE {2}={3} AND {4}=1", 
                                 Const.DB_PHOTO_BASE64_COLNAME,
                                 Const.DB_PHOTO_TABLENAME,
                                 Const.DB_BIEN_ID_COLNAME,
                                 Const.DB_PHOTO_IDBIEN_COLNAME,
                                 Const.DB_PHOTO_NUMPHOTO_COLNAME);
        }

        private string GenererLimit(ref int? page, ref int?  nbBiens, out int nbBiensTotal, out int nbPages, out int numBien)
        {
            nbBiensTotal = -1;
            nbPages = 1;
            numBien = 0;

            // Lecture du nombre de biens total
            try
            {
                DataSet ds = _cnx.ExecuteReader(string.Format("SELECT COUNT(*) FROM {0}", Const.DB_BIEN_TABLENAME));
                nbBiensTotal = TraitementsSQL.DBInt(ds.Tables[0].Rows[0][0]);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return null;
            }

            if (page == null || nbBiens == null || page <= 0 || nbBiens <= 0)
            {
                page = 1;
                nbBiens = nbBiensTotal;
                return "";
            }

            // Calcul des autres compteurs
            nbPages = nbBiensTotal / (int)nbBiens + ((nbBiensTotal % nbBiens)>0 ? 1 : 0);
            numBien = (int)nbBiens * ((int)page - 1);
            if (page > nbPages) page = nbPages;
            if (numBien >= nbBiensTotal) numBien = nbBiensTotal - 1;
            
            // Génération de l'expression SQL LIMIT
            return string.Format("LIMIT {0} OFFSET {1}", nbBiens, numBien);
        }

        private string GenererWhere(CriteresRechercheBiensImmobiliers criteres)
        {
            string where = "";

            if (criteres == null) return "";

            if (!string.IsNullOrEmpty(criteres.TitreContient))
                where += " AND " + Const.DB_BIEN_TITRE_COLNAME + " LIKE '%" + criteres.TitreContient.Replace("'", "''") + "%'";

            where += GenererRechercheEntre(Const.DB_BIEN_DATEMISEENTRANSACTION_COLNAME,
                                           criteres.DateMiseEnTransaction1,
                                           criteres.DateMiseEnTransaction2,
                                           null, "Datetime");

            where += GenererRechercheEntre(Const.DB_BIEN_DATETRANSACTION_COLNAME,
                                           criteres.DateTransaction1,
                                           criteres.DateTransaction2,
                                           null, "Datetime");

            if (!string.IsNullOrEmpty(criteres.DescriptionContient))
                where += " AND " + Const.DB_BIEN_DESCRIPTION_COLNAME + " LIKE '%" + criteres.DescriptionContient.Replace("'", "''") + "%'";

            if (criteres.EnergieChauffage != null && criteres.EnergieChauffage != (BienImmobilierBase.eEnergieChauffage)(-1))
                where += " AND " + Const.DB_BIEN_ENERGIECHAUFFAGE_COLNAME + "=" + TraitementsSQL.FSQL((int)criteres.EnergieChauffage);

            where += GenererRechercheEntre(Const.DB_BIEN_MONTANTCHARGES_COLNAME,
                                           criteres.MontantCharges1,
                                           criteres.MontantCharges2,
                                           -1);

            where += GenererRechercheEntre(Const.DB_BIEN_NBETAGES_COLNAME,
                                           criteres.NbEtages1,
                                           criteres.NbEtages2,
                                           -1);

            where += GenererRechercheEntre(Const.DB_BIEN_NBPIECES_COLNAME,
                                           criteres.NbPieces1,
                                           criteres.NbPieces2,
                                           -1);
            
            where += GenererRechercheEntre(Const.DB_BIEN_NUMETAGE_COLNAME,
                                           criteres.NumEtage1,
                                           criteres.NumEtage2,
                                           -1);
            
            where += GenererRechercheEntre(Const.DB_BIEN_PRIX_COLNAME,
                                           criteres.Prix1,
                                           criteres.Prix2,
                                           -1);
            
            where += GenererRechercheEntre(Const.DB_BIEN_SURFACE_COLNAME,
                                           criteres.Surface1,
                                           criteres.Surface2,
                                           -1);

            if (criteres.TransactionEffectuee != null)
                where += " AND " + Const.DB_BIEN_TRANSACTIONEFFECTUEE_COLNAME + "=" + TraitementsSQL.FSQL(criteres.TransactionEffectuee);

            if (criteres.TypeBien != null && criteres.TypeBien != (BienImmobilierBase.eTypeBien)(-1))
                where += " AND " + Const.DB_BIEN_TYPEBIEN_COLNAME + "=" + TraitementsSQL.FSQL((int)criteres.TypeBien);

            if (criteres.TypeChauffage != null && criteres.TypeChauffage != (BienImmobilierBase.eTypeChauffage)(-1))
                where += " AND " + Const.DB_BIEN_TYPECHAUFFAGE_COLNAME + "=" + TraitementsSQL.FSQL((int)criteres.TypeChauffage);

            if (criteres.TypeTransaction != null && criteres.TypeTransaction != (BienImmobilierBase.eTypeTransaction)(-1))
                where += " AND "+ Const.DB_BIEN_TYPETRANSACTION_COLNAME + "=" + TraitementsSQL.FSQL((int)criteres.TypeTransaction);

            if (!string.IsNullOrEmpty(criteres.AdresseContient))
                where += " AND " + Const.DB_BIEN_ADRESSE_COLNAME + " LIKE '%" + criteres.AdresseContient.Replace("'", "''") + "%'";

            if (!string.IsNullOrEmpty(criteres.CodePostal))
                where += " AND " + Const.DB_BIEN_CODEPOSTAL_COLNAME + " LIKE '" + criteres.CodePostal+"%'";

            if (!string.IsNullOrEmpty(criteres.Ville))
                where += " AND " + Const.DB_BIEN_VILLE_COLNAME + " LIKE '" +criteres.Ville+"'";

            if (where != "")
            {
                if(where.StartsWith(" AND ")) where = where.Remove(0, 5);
                where = "WHERE " + where;
            }

            return where;
        }
        private string GenererRechercheEntre<T>(string champ, T valeur1, T valeur2, T valeurNulle, string function = "")
        {
            if (EqualityComparer<T>.Default.Equals(valeur1, valeurNulle) && EqualityComparer<T>.Default.Equals(valeur2, valeurNulle))
                return "";
            else if (EqualityComparer<T>.Default.Equals(valeur1, valeurNulle))
                return " AND " + champ + " <= " + function + ((function == "") ? "" : "(") + TraitementsSQL.FSQL(valeur2) + ((function == "") ? "" : ")");
            else if (EqualityComparer<T>.Default.Equals(valeur2, valeurNulle))
                return " AND " + champ + " >= " + function + ((function == "") ? "" : "(") + TraitementsSQL.FSQL(valeur1) + ((function == "") ? "" : ")");
            else if (EqualityComparer<T>.Default.Equals(valeur1, valeur2))
                return " AND " + champ + " = " + function + ((function == "") ? "" : "(") + TraitementsSQL.FSQL(valeur1) + ((function == "") ? "" : ")");
            else
                return " AND (" + champ + " >= " + function + ((function == "") ? "" : "(") + TraitementsSQL.FSQL(valeur1) + ((function == "") ? "" : ")") +
                   " AND " + champ + " <= " + function + ((function == "") ? "" : "(") + TraitementsSQL.FSQL(valeur2) + ((function == "") ? "" : ")") + ")";
        }

        private string GenererOrderBy(CriteresRechercheBiensImmobiliers.Tri? triPrincipal, List<CriteresRechercheBiensImmobiliers.Tri> autresTris)
        {
            string orderby = "";

            if (triPrincipal == null) return "";

            orderby = GenererOrderBy((CriteresRechercheBiensImmobiliers.Tri)triPrincipal);
            if (orderby == "") return "";
            
            orderby = "ORDER BY " + orderby;
            for (int i = 1; i < autresTris.Count - 1; i++)
            {
                orderby += ", " + GenererOrderBy(autresTris[i]);
            }

            return orderby;
        }
        private string GenererOrderBy(CriteresRechercheBiensImmobiliers.Tri tri)
        {
            string orderby = "";

            switch (tri.Champ)
            {
                case CriteresRechercheBiensImmobiliers.eChampTri.DateMiseEnTransaction:
                    orderby = Const.DB_BIEN_DATEMISEENTRANSACTION_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.DateTransaction:
                    orderby = Const.DB_BIEN_DATETRANSACTION_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.Description:
                    orderby = Const.DB_BIEN_DESCRIPTION_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.EnergieChauffage:
                    orderby = Const.DB_BIEN_ENERGIECHAUFFAGE_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.Id:
                    orderby = Const.DB_BIEN_ID_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.MontantCharges:
                    orderby = Const.DB_BIEN_MONTANTCHARGES_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.NbEtages:
                    orderby = Const.DB_BIEN_NBETAGES_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.NbPieces:
                    orderby = Const.DB_BIEN_NBPIECES_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.NumEtage:
                    orderby = Const.DB_BIEN_NUMETAGE_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.Prix:
                    orderby = Const.DB_BIEN_PRIX_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.Surface:
                    orderby = Const.DB_BIEN_SURFACE_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.TransactionEffectuee:
                    orderby = Const.DB_BIEN_TRANSACTIONEFFECTUEE_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.TypeBien:
                    orderby = Const.DB_BIEN_TYPEBIEN_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.TypeChauffage:
                    orderby = Const.DB_BIEN_TYPECHAUFFAGE_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.TypeTransaction:
                    orderby = Const.DB_BIEN_TYPETRANSACTION_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.Adresse:
                    orderby = Const.DB_BIEN_ADRESSE_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.CodePostal:
                    orderby = Const.DB_BIEN_CODEPOSTAL_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.Ville:
                    orderby = Const.DB_BIEN_VILLE_COLNAME;
                    break;
                case CriteresRechercheBiensImmobiliers.eChampTri.Titre:
                    orderby = Const.DB_BIEN_TITRE_COLNAME;
                    break;
            }

            if (orderby != "")
            {
                if (tri.Ordre == CriteresRechercheBiensImmobiliers.eOrdreTri.Descendant)
                    orderby += " DESC";
                else
                    orderby += " ASC";
            }

            return orderby;
        }

        #endregion

        #region Lecture / ajout / suppression des photos

        private bool LirePhotos(int idBien, ref BienImmobilier bien)
        {
            string req = "";
            DataSet ds = null;

            try
            {
                req = string.Format("SELECT * FROM {0} WHERE {1}={2} ORDER BY {3} ASC",
                                    Const.DB_PHOTO_TABLENAME,
                                    Const.DB_PHOTO_IDBIEN_COLNAME,
                                    TraitementsSQL.FSQL(idBien),
                                    Const.DB_PHOTO_NUMPHOTO_COLNAME);
                ds = _cnx.ExecuteReader(req);

                for (int i = 0 ; i < ds.Tables[0].Rows.Count ; i++)
                {
                    if (i == 0) bien.PhotoPrincipaleBase64 = TraitementsSQL.DBStr(ds.Tables[0].Rows[i][Const.DB_PHOTO_BASE64_COLNAME]);
                    bien.PhotosBase64.Add(TraitementsSQL.DBStr(ds.Tables[0].Rows[i][Const.DB_PHOTO_BASE64_COLNAME]));
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            return true;
        }

        private bool AjouterPhotos(int idBien, string photoPrincipale, List<string> photos)
        {
            string req = "INSERT INTO " + Const.DB_PHOTO_TABLENAME + " VALUES (" + TraitementsSQL.FSQL(idBien) + ", {0}, {1})";
            
            // Affectation des photos
            try
            {
                int cpt = 1;

                // Photo principale
                if (!string.IsNullOrEmpty(photoPrincipale) && !photos.Contains(photoPrincipale))
                {
                    _cnx.ExecuteNonQuery(string.Format(req, TraitementsSQL.FSQL(cpt), TraitementsSQL.FSQL(photoPrincipale)));
                    cpt++;
                }

                // Autres photos
                foreach (string photo in photos)
                {
                    if (!string.IsNullOrEmpty(photo))
                    {
                        _cnx.ExecuteNonQuery(string.Format(req, TraitementsSQL.FSQL(cpt), TraitementsSQL.FSQL(photo)));
                        cpt++;
                    }
                }
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            return true;
        }

        private bool SupprimerPhotos(int idBien)
        {
            string req = string.Format("DELETE FROM {0} WHERE {1}={2}",
                                       Const.DB_PHOTO_TABLENAME,
                                       Const.DB_PHOTO_IDBIEN_COLNAME,
                                       TraitementsSQL.FSQL(idBien));

            // Suppression des photos
            try
            {
                _cnx.ExecuteNonQuery(req);
            }
            catch (Exception ex)
            {
                _errorMessage = ex.Message;
                return false;
            }

            return true;
        }

        #endregion

        #region Chargement des objets

        private void ChargerBienImmobilier(DataRow dr, out BienImmobilierBase bien)
        {
            bien = new BienImmobilierBase(TraitementsSQL.DBInt(dr[Const.DB_BIEN_ID_COLNAME]));
            bien.DateMiseEnTransaction = TraitementsSQL.DBDate(dr[Const.DB_BIEN_DATEMISEENTRANSACTION_COLNAME]);
            bien.DateTransaction = TraitementsSQL.DBDate(dr[Const.DB_BIEN_DATETRANSACTION_COLNAME]);
            bien.MontantCharges = TraitementsSQL.DBDbl(dr[Const.DB_BIEN_MONTANTCHARGES_COLNAME]);
            bien.Prix = TraitementsSQL.DBDbl(dr[Const.DB_BIEN_PRIX_COLNAME]);
            bien.Titre = TraitementsSQL.DBStr(dr[Const.DB_BIEN_TITRE_COLNAME]);
            bien.TransactionEffectuee = TraitementsSQL.DBBool(dr[Const.DB_BIEN_TRANSACTIONEFFECTUEE_COLNAME]);
            bien.TypeBien = (BienImmobilierBase.eTypeBien)TraitementsSQL.DBInt(dr[Const.DB_BIEN_TYPEBIEN_COLNAME]);
            bien.TypeTransaction = (BienImmobilierBase.eTypeTransaction)TraitementsSQL.DBInt(dr[Const.DB_BIEN_TYPETRANSACTION_COLNAME]);
            bien.CodePostal = TraitementsSQL.DBStr(dr[Const.DB_BIEN_CODEPOSTAL_COLNAME]);
            bien.Ville = TraitementsSQL.DBStr(dr[Const.DB_BIEN_VILLE_COLNAME]);
        }

        private void ChargerBienImmobilier(DataRow dr, out BienImmobilier bien)
        {
            bien = new BienImmobilier(TraitementsSQL.DBInt(dr[Const.DB_BIEN_ID_COLNAME]));
            bien.DateMiseEnTransaction = TraitementsSQL.DBDate(dr[Const.DB_BIEN_DATEMISEENTRANSACTION_COLNAME]);
            bien.DateTransaction = TraitementsSQL.DBDate(dr[Const.DB_BIEN_DATETRANSACTION_COLNAME]);
            bien.Description = TraitementsSQL.DBStr(dr[Const.DB_BIEN_DESCRIPTION_COLNAME]);
            bien.MontantCharges = TraitementsSQL.DBDbl(dr[Const.DB_BIEN_MONTANTCHARGES_COLNAME]);
            bien.Prix = TraitementsSQL.DBDbl(dr[Const.DB_BIEN_PRIX_COLNAME]);
            bien.Titre = TraitementsSQL.DBStr(dr[Const.DB_BIEN_TITRE_COLNAME]);
            bien.TransactionEffectuee = TraitementsSQL.DBBool(dr[Const.DB_BIEN_TRANSACTIONEFFECTUEE_COLNAME]);
            bien.TypeBien = (BienImmobilierBase.eTypeBien)TraitementsSQL.DBInt(dr[Const.DB_BIEN_TYPEBIEN_COLNAME]);
            bien.TypeTransaction = (BienImmobilierBase.eTypeTransaction)TraitementsSQL.DBInt(dr[Const.DB_BIEN_TYPETRANSACTION_COLNAME]);
            bien.CodePostal = TraitementsSQL.DBStr(dr[Const.DB_BIEN_CODEPOSTAL_COLNAME]);
            bien.Ville = TraitementsSQL.DBStr(dr[Const.DB_BIEN_VILLE_COLNAME]);
            bien.EnergieChauffage = (BienImmobilierBase.eEnergieChauffage)TraitementsSQL.DBInt(dr[Const.DB_BIEN_ENERGIECHAUFFAGE_COLNAME]);
            bien.NbEtages = TraitementsSQL.DBInt(dr[Const.DB_BIEN_NBETAGES_COLNAME]);
            bien.NbPieces = TraitementsSQL.DBInt(dr[Const.DB_BIEN_NBPIECES_COLNAME]);
            bien.NumEtage = TraitementsSQL.DBInt(dr[Const.DB_BIEN_NUMETAGE_COLNAME]);
            bien.Surface = TraitementsSQL.DBDbl(dr[Const.DB_BIEN_SURFACE_COLNAME]);
            bien.TypeChauffage = (BienImmobilierBase.eTypeChauffage)TraitementsSQL.DBInt(dr[Const.DB_BIEN_TYPECHAUFFAGE_COLNAME]);
            bien.Adresse = TraitementsSQL.DBStr(dr[Const.DB_BIEN_ADRESSE_COLNAME]);
        }

        #endregion

    }
}
