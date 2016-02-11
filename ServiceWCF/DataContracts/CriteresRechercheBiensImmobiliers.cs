using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ServiceWCF.DataContracts
{
    [DataContract]
    public class CriteresRechercheBiensImmobiliers
    {
        #region Enumérations

        [DataContract]
        public enum eChampTri
        {
            [EnumMember]
            Id = 0,
            [EnumMember]
            Titre = 1,
            [EnumMember]
            TypeTransaction = 2,
            [EnumMember]
            TypeBien = 3,
            [EnumMember]
            Description = 4,
            [EnumMember]
            Prix = 5,
            [EnumMember]
            MontantCharges = 6,
            [EnumMember]
            DateMiseEnTransaction = 7,
            [EnumMember]
            TransactionEffectuee = 8,
            [EnumMember]
            DateTransaction = 9,
            [EnumMember]
            Surface = 10,
            [EnumMember]
            NbPieces = 11,
            [EnumMember]
            NumEtage = 12,
            [EnumMember]
            NbEtages = 13,
            [EnumMember]
            TypeChauffage = 14,
            [EnumMember]
            EnergieChauffage = 15,
            [EnumMember]
            Adresse = 16,
            [EnumMember]
            CodePostal = 17,
            [EnumMember]
            Ville = 18
        }

        [DataContract]
        public enum eOrdreTri
        {
            [EnumMember]
            Montant = 0,
            [EnumMember]
            Descendant = 1
        }

        #endregion

        #region Structures

        [DataContract]
        public struct Tri
        {
            [DataMember]
            public eChampTri Champ;
            [DataMember]
            public eOrdreTri Ordre;
        }

        #endregion

        #region Attributs

        string _titreContient = "";
        BienImmobilierBase.eTypeTransaction? _typeTransaction = null;
        BienImmobilierBase.eTypeBien? _typeBien = null;
        string _descriptionContient = "";
        double _prix1 = -1;
        double _prix2 = -1;
        double _montantCharges1 = -1;
        double _montantCharges2 = -1;
        DateTime? _dateMiseEnTransaction1 = null;
        DateTime? _dateMiseEnTransaction2 = null;
        bool? _transactionEffectuee = null;
        DateTime? _dateTransaction1 = null;
        DateTime? _dateTransaction2 = null;
        double _surface1 = -1;
        double _surface2 = -1;
        int _nbPieces1 = -1;
        int _nbPieces2 = -1;
        int _numEtage1 = -1;
        int _numEtage2 = -1;
        int _nbEtages1 = -1;
        int _nbEtages2 = -1;
        BienImmobilierBase.eTypeChauffage? _typeChauffage = null;
        BienImmobilierBase.eEnergieChauffage? _energieChauffage = null;
        string _adresseContient = "";
        string _codePostal = "";
        string _ville = "";
        List<Tri> _tris = new List<Tri>();

        #endregion

        #region Propriétés

        [DataMember]
        public string TitreContient
        {
            get { return _titreContient; }
            set { _titreContient = value; }
        }

        [DataMember]
        public BienImmobilierBase.eTypeTransaction? TypeTransaction
        {
            get { return _typeTransaction; }
            set { _typeTransaction = value; }
        }

        [DataMember]
        public BienImmobilierBase.eTypeBien? TypeBien
        {
            get { return _typeBien; }
            set { _typeBien = value; }
        }

        [DataMember]
        public string DescriptionContient
        {
            get { return _descriptionContient; }
            set { _descriptionContient = value; }
        }

        [DataMember]
        public double Prix1
        {
            get { return _prix1; }
            set { _prix1 = value; }
        }
        [DataMember]
        public double Prix2
        {
            get { return _prix2; }
            set { _prix2 = value; }
        }

        [DataMember]
        public double MontantCharges1
        {
            get { return _montantCharges1; }
            set { _montantCharges1 = value; }
        }
        [DataMember]
        public double MontantCharges2
        {
            get { return _montantCharges2; }
            set { _montantCharges2 = value; }
        }

        [DataMember]
        public DateTime? DateMiseEnTransaction1
        {
            get { return _dateMiseEnTransaction1; }
            set { _dateMiseEnTransaction1 = value; }
        }
        [DataMember]
        public DateTime? DateMiseEnTransaction2
        {
            get { return _dateMiseEnTransaction2; }
            set { _dateMiseEnTransaction2 = value; }
        }

        [DataMember]
        public bool? TransactionEffectuee
        {
            get { return _transactionEffectuee; }
            set { _transactionEffectuee = value; }
        }

        [DataMember]
        public DateTime? DateTransaction1
        {
            get { return _dateTransaction1; }
            set { _dateTransaction1 = value; }
        }
        [DataMember]
        public DateTime? DateTransaction2
        {
            get { return _dateTransaction2; }
            set { _dateTransaction2 = value; }
        }

        [DataMember]
        public double Surface1
        {
            get { return _surface1; }
            set { _surface1 = value; }
        }
        [DataMember]
        public double Surface2
        {
            get { return _surface2; }
            set { _surface2 = value; }
        }

        [DataMember]
        public int NbPieces1
        {
            get { return _nbPieces1; }
            set { _nbPieces1 = value; }
        }
        [DataMember]
        public int NbPieces2
        {
            get { return _nbPieces2; }
            set { _nbPieces2 = value; }
        }

        [DataMember]
        public int NumEtage1
        {
            get { return _numEtage1; }
            set { _numEtage1 = value; }
        }
        [DataMember]
        public int NumEtage2
        {
            get { return _numEtage2; }
            set { _numEtage2 = value; }
        }

        [DataMember]
        public int NbEtages1
        {
            get { return _nbEtages1; }
            set { _nbEtages1 = value; }
        }
        [DataMember]
        public int NbEtages2
        {
            get { return _nbEtages2; }
            set { _nbEtages2 = value; }
        }

        [DataMember]
        public BienImmobilierBase.eTypeChauffage? TypeChauffage
        {
            get { return _typeChauffage; }
            set { _typeChauffage = value; }
        }

        [DataMember]
        public BienImmobilierBase.eEnergieChauffage? EnergieChauffage
        {
            get { return _energieChauffage; }
            set { _energieChauffage = value; }
        }

        [DataMember]
        public string AdresseContient
        {
            get { return _adresseContient; }
            set { _adresseContient = value; }
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

        public Tri? TriPrincipal
        {
            get
            {
                if (_tris == null || _tris.Count <= 0)
                    return null;
                else
                    return _tris[0];
            }
        }

        [DataMember]
        public List<Tri> Tris
        {
            get { return _tris; }
            private set { _tris = value; }
        }

        #endregion

        internal CriteresRechercheBiensImmobiliers()
        {
            _tris.Add(new Tri());
        }
    }
}
