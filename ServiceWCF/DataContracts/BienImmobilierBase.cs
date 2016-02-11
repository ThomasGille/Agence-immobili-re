using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiceWCF.DataContracts
{
    [DataContract]
    public class BienImmobilierBase
    {

        #region Enumérations

        [DataContract]
        public enum eTypeTransaction
        {
            [EnumMember]
            Vente = 0,
            [EnumMember]
            Location = 1
        }

        [DataContract]
        public enum eTypeBien
        {
            [EnumMember]
            Appartement = 0,
            [EnumMember]
            Maison = 1,
            [EnumMember]
            Garage = 2,
            [EnumMember]
            Terrain = 3
        }

        [DataContract]
        public enum eTypeChauffage
        {
            [EnumMember]
            Aucun = 0,
            [EnumMember]
            Individuel = 1,
            [EnumMember]
            Collectif = 2
        }

        [DataContract]
        public enum eEnergieChauffage
        {
            [EnumMember]
            Aucun = 0,
            [EnumMember]
            Fioul = 1,
            [EnumMember]
            Gaz = 2,
            [EnumMember]
            Electrique = 3,
            [EnumMember]
            Bois = 4
        }

        #endregion

        #region Attributs

        protected int _id;
        protected string _titre = "";
        protected eTypeTransaction _typeTransaction = eTypeTransaction.Vente;
        protected eTypeBien _typeBien = eTypeBien.Appartement;
        protected double _prix = 0;
        protected double _montantCharges = 0;
        protected DateTime? _dateMiseEnTransaction = null;
        protected bool _transactionEffectuee = false;
        protected DateTime? _dateTransaction = null;
        protected string _codePostal = "";
        protected string _ville = "";
        protected string _photoPrincipaleBase64 = "";
        protected List<string> _photosBase64 = new List<string>();

        #endregion

        #region Propriétés

        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public string Titre
        {
            get { return _titre; }
            set { _titre = value; }
        }

        [DataMember]
        public eTypeTransaction TypeTransaction
        {
            get { return _typeTransaction; }
            set { _typeTransaction = value; }
        }

        [DataMember]
        public eTypeBien TypeBien
        {
            get { return _typeBien; }
            set { _typeBien = value; }
        }

        [DataMember]
        public double Prix
        {
            get { return _prix; }
            set { _prix = value; }
        }

        [DataMember]
        public double MontantCharges
        {
            get { return _montantCharges; }
            set { _montantCharges = value; }
        }

        [DataMember]
        public DateTime? DateMiseEnTransaction
        {
            get { return _dateMiseEnTransaction; }
            set { _dateMiseEnTransaction = value; }
        }

        [DataMember]
        public bool TransactionEffectuee
        {
            get { return _transactionEffectuee; }
            set { _transactionEffectuee = value; }
        }

        [DataMember]
        public DateTime? DateTransaction
        {
            get { return _dateTransaction; }
            set { _dateTransaction = value; }
        }

        [DataMember]
        public string CodePostal
        {
            get { return _codePostal; }
            set { _codePostal = value; }
        }

        [DataMember]
        public string Ville
        {
            get { return _ville; }
            set { _ville = value; }
        }

        [DataMember]
        public string PhotoPrincipaleBase64
        {
            get { return _photoPrincipaleBase64; }
            set { _photoPrincipaleBase64 = value; }
        }
        
        #endregion

        internal BienImmobilierBase(int id)
        {
            this._id = id;
        }

    }

}