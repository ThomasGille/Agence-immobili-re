using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace ServiceWCF.DataContracts
{
    [DataContract]
    public class ResultatListeBiensImmobiliers : ResultatOperation
    {
        [DataMember]
        public ListeBiensImmobiliers Liste { get; private set; }

        public ResultatListeBiensImmobiliers() : base()
        {
            this.Liste = new ListeBiensImmobiliers();
        }
    }
}
