using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

namespace GameTool_Calculator
{

    [DataContract] // シリアライズ用属性
    class CRateData : jsonData
    {
        [DataMember]
        string m_strName;

        [DataMember]
        float m_fRate = 1.0f;

        [DataMember]
        float m_fPrice = 1.0f;

        public string Name { get => m_strName; set => m_strName = value; }
        public float Rate { get => m_fRate; set => m_fRate = value; }
        public float Price { get => m_fPrice; set => m_fPrice = value; }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path"></param>
        public CRateData()
        {
        }

        /// <summary>
        /// 読み込み
        /// </summary>
        /// <param name="json"></param>
        protected override void ReadCallBack(object json)
        {
            CRateData data = json as CRateData;

            this.m_strName = data.m_strName;
            this.m_fRate = data.m_fRate;
            this.m_fPrice = data.m_fPrice;

        }

    }
}
