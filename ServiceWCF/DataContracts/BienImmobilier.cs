using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace ServiceWCF.DataContracts
{
    [DataContract]
    public class BienImmobilier : BienImmobilierBase
    {

        #region Attributs

        string _description = "";
        double _surface = 0;
        int _nbPieces = 0;
        int _numEtage = 0;
        int _nbEtages = 0;
        eTypeChauffage _typeChauffage = eTypeChauffage.Individuel;
        eEnergieChauffage _energieChauffage = eEnergieChauffage.Fioul;
        string _adresse = "";
        
        #endregion

        #region Propriétés

        [DataMember]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        [DataMember]
        public double Surface
        {
            get { return _surface; }
            set { _surface = value; }
        }

        [DataMember]
        public int NbPieces
        {
            get { return _nbPieces; }
            set { _nbPieces = value; }
        }

        [DataMember]
        public int NumEtage
        {
            get { return _numEtage; }
            set { _numEtage = value; }
        }

        [DataMember]
        public int NbEtages
        {
            get { return _nbEtages; }
            set { _nbEtages = value; }
        }

        [DataMember]
        public eTypeChauffage TypeChauffage
        {
            get { return _typeChauffage; }
            set { _typeChauffage = value; }
        }

        [DataMember]
        public eEnergieChauffage EnergieChauffage
        {
            get { return _energieChauffage; }
            set { _energieChauffage = value; }
        }

        [DataMember]
        public string Adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

       
        [DataMember]
        public List<string> PhotosBase64
        {
            get { return _photosBase64; }
            set { _photosBase64 = value; }
        }

        #endregion

        internal BienImmobilier(int id) : base(id) { }
    }
}