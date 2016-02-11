using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Web;

namespace ServiceWCF.DataContracts
{
    [DataContract]
    public class ResultatBienImmobilier : ResultatOperation
    {
        [DataMember]
        public BienImmobilier Bien { get; set; }

        public ResultatBienImmobilier() : base()
        {
            this.Bien = null;
        }
    }
}
