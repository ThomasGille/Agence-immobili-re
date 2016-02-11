using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace ServiceWCF.DataContracts
{
    [DataContract]
    public class ListeBiensImmobiliers
    {
        // Liste des biens immobiliers
        [DataMember]
        public List<BienImmobilierBase> List { get; private set; }

        // Nombre total de biens immobiliers disponibles
        [DataMember]
        public int TotalCount { get; set; }

        // Numéro à partir duquel commence la séquence
        [DataMember]
        public int Start { get; set; }

        // Numéro de page
        [DataMember]
        public int Page { get; set; }

        // Nombre de biens immobiliers dans la séquence
        [DataMember]
        public int TakeCount { get; set; }

        // Nombre de biens immobiliers sélectionnés
        [DataMember]
        public int SelectCount { get; set; }

        // Nombre de pages disponibles
        [DataMember]
        public int PagesCount { get; set; }

        public ListeBiensImmobiliers()
        {
            this.List = new List<BienImmobilierBase>();
        }
    }
}
