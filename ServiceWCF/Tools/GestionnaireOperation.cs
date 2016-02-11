using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ServiceModel.Web;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;

namespace ServiceWCF.Tools
{

    internal delegate TResult Operation<TResult, TParam1, TParam2>(SQL.GestionnaireBDD bdd, Log log, TParam1 parametre, params TParam2[] autresParametres) where TResult : DataContracts.ResultatOperation;

    class GestionnaireOperation
    {
        internal static TResult ExecuterOperation<TResult, TParam1, TParam2>(Operation<TResult, TParam1, TParam2> operation, TParam1 parametre, params TParam2[] autresParametres) where TResult : DataContracts.ResultatOperation, new()
        {
            SQL.GestionnaireBDD bdd = new SQL.GestionnaireBDD(Agence.DATABASE_PATH);
            TResult resultat = null;
            Log l = new Log();

            // Vérification qu'il n'y a pas eu d'erreur à l'initialisation de la base de données
            if (bdd.ErrorMessage != "")
            {
                resultat = new TResult();
                Tools.GestionnaireOperation.GererErreur(resultat, l, bdd.ErrorMessage);
                return resultat;
            }

            // Connexion à la base de données
            if (!bdd.ConnexionBDD())
            {
                resultat = new TResult();
                Tools.GestionnaireOperation.GererErreur(resultat, l, bdd.ErrorMessage);
                return resultat;
            }

            // Exécution de l'opération passée en paramètres
            try
            {
                resultat = operation.Invoke(bdd, l, parametre, autresParametres);
            }
            catch (Exception ex)
            {
                if (resultat == null) resultat = new TResult();
                Tools.GestionnaireOperation.GererErreur(resultat, l, ex);
            }
            finally
            {
                // Déconnexion à la base de données
                if (!bdd.DeconnexionBDD())
                {
                    if (resultat == null) resultat = new TResult();
                    Tools.GestionnaireOperation.GererErreur(resultat, l, bdd.ErrorMessage);
                }
            }

            return resultat;
        }


        internal static void GererErreur(DataContracts.ResultatOperation resultat,
                                         Log log,
                                         Exception ex,
                                         EventLogEntryType typeLog = EventLogEntryType.Error,
                                         bool ajouterLogFichier = true,
                                         bool ajouterLogWindows = false)
        {
            GererErreur(resultat, log, ex.Message, ex.ToString(), typeLog: typeLog, nomModule: ex.Source, ajouterLogFichier: ajouterLogFichier, ajouterLogWindows: ajouterLogWindows);
        }
        internal static void GererErreur(DataContracts.ResultatOperation resultat,
                                         Log log,
                                         string message, 
                                         string messageDetaille = "",
                                         EventLogEntryType typeLog = EventLogEntryType.Error, 
                                         string nomModule = "",
                                         int codeErreur = 0,
                                         bool ajouterLogFichier = true,
                                         bool ajouterLogWindows = false)
        {
            // Ajout de l'erreur au résultat renvoyé au client
            switch (typeLog)
            {
                case EventLogEntryType.Error:
                case EventLogEntryType.FailureAudit:
                    resultat.SuccesExecution = false;
                    resultat.ErreursBloquantes.Add(new DataContracts.ResultatOperation.Erreur(DataContracts.ResultatOperation.eTypeErreur.ErreurBloquante, message));
                    break;
                case EventLogEntryType.Warning:
                    resultat.SuccesExecution = false;
                    resultat.ErreursNonBloquantes.Add(new DataContracts.ResultatOperation.Erreur(DataContracts.ResultatOperation.eTypeErreur.ErreurNonBloquante, message));
                    break;
                default:
                    // On ne renvoie pas l'information au client, c'est normal
                    break;
            }

            // Ajout de l'erreur dans le log
            if (messageDetaille == "") messageDetaille=message;
            log.AddEventLog(messageDetaille, typeLog, nomModule, codeErreur, ajouterLogFichier, ajouterLogWindows);
        }



        internal static bool LireParametreURIEntier(string nomParametre, out int valeurParametre)
        {
            valeurParametre = 0;
            string strValue;

            if (!LireParametreURI(nomParametre, out strValue))
                return false;

            if (!string.IsNullOrEmpty(strValue))
                return int.TryParse(strValue, out valeurParametre);

            return false;
        }
        internal static bool LireParametreURI<T>(string nomParametre, out T valeurParametre)
        {
            valeurParametre = default(T);
            string strValue;

            if (!LireParametreURI(nomParametre, out strValue))
                return false;

            if (!string.IsNullOrEmpty(strValue))
            {
                try
                {
                    valeurParametre = Deserialize<T>(strValue);
                    return true;
                }
                catch { }
            }
                
            return false;
        }
        private static bool LireParametreURI(string nomParametre, out string valeurParametre)
        {
            valeurParametre = "";
            WebOperationContext context = WebOperationContext.Current;
            if (context == null) return false;

            UriTemplateMatch uriMatch = context.IncomingRequest.UriTemplateMatch;
            if (uriMatch == null) return false;

            valeurParametre = uriMatch.QueryParameters[nomParametre];

            return true;
        }
        private static T Deserialize<T>(string rawXml)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(rawXml)))
            {
                DataContractSerializer formatter0 =
                    new DataContractSerializer(typeof(T));
                return (T)formatter0.ReadObject(reader);
            }
        }

    }
}
