using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace GameTool_Calculator
{

    [DataContract] // シリアライズ用属性
    class CPlayerData : jsonData
    {
        [DataMember]
        Dictionary<string, int> m_aActionData = new Dictionary<string, int>();

        public Dictionary<string, int> ActionData { get => m_aActionData; set => m_aActionData = value; }

        protected override void ReadCallBack(object json)
        {
            throw new NotImplementedException();
        }
    }

}
