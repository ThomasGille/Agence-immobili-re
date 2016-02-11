using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiceWCF.DataContracts
{
    [DataContract]
    public class ResultatOperation
    {
        [DataContract]
        public enum eTypeErreur
        {
            [EnumMember]
            ErreurNonBloquante = 0,
            [EnumMember]
            ErreurBloquante = 1
        }

        [DataContract]
        public class Erreur
        {
            [DataMember]
            public eTypeErreur Type { get; set; }
            [DataMember]
            public string Message { get; set; }

            public Erreur()
            {
                this.Type = eTypeErreur.ErreurNonBloquante;
                this.Message = "";
            }
            public Erreur(eTypeErreur type, string message)
            {
                this.Type = type;
                this.Message = message;
            }
        }

        private List<Erreur> _erreursBloquantes;
        private List<Erreur> _erreursNonBloquantes;

        [DataMember]
        public bool SuccesExecution { get; set; }
        [DataMember]
        public List<Erreur> ErreursBloquantes { get { return _erreursBloquantes; } }
        [DataMember]
        public List<Erreur> ErreursNonBloquantes { get { return _erreursNonBloquantes; } }

        public ResultatOperation()
        {
            this.SuccesExecution = true;
            this._erreursBloquantes = new List<Erreur>();
            this._erreursNonBloquantes = new List<Erreur>();
        }
    }
}
