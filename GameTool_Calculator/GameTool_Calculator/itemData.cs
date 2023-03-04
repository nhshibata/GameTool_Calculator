using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace GameTool_Calculator
{
    [DataContract] // シリアライズ用属性

    class CItemData : jsonData
    {
        [DataMember]
        string m_strName;

        [DataMember]
        int m_nMoney = 2;

        public string Name { get => m_strName; set => m_strName = value; }
        public int Money { get => m_nMoney; set => m_nMoney = value; }


        protected override void ReadCallBack(object json)
        {
            throw new NotImplementedException();
        }
    }

}
