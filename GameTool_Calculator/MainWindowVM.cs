using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTool_Calculator
{
    /// <summary>
    /// xamlアクセス
    /// </summary>
    class MainWindowVM
    {
        /// <summary>
        /// 参照
        /// </summary>
        private CDataBase m_DB = CDataBase.m_pInstance;

        public string RateName { get { return m_DB.SelectRateData.Name; } set { m_DB.SelectRateData.Name = value; } }
        public float RateValue { get { return m_DB.SelectRateData.Rate; } set { m_DB.SelectRateData.Rate = value; } }
        public float RatePrice { get { return m_DB.SelectRateData.Price; } set { m_DB.SelectRateData.Price = value; } }

    }
}
