using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTool_Calculator
{
    class CDataBase
    {
        static public CDataBase m_pInstance = new CDataBase();

        CRateData m_SelectRateData;

        List<CRateData> m_aRates = new List<CRateData>();
        List<CItemData> m_aItems = new List<CItemData>();
        List<CEnemyData> m_aEnemys = new List<CEnemyData>();
        List<CPlayerData> m_aPlayers = new List<CPlayerData>();

        internal List<CRateData> Rates { get => m_aRates; set => m_aRates = value; }
        internal List<CItemData> Items { get => m_aItems; set => m_aItems = value; }
        internal List<CEnemyData> Enemys { get => m_aEnemys; set => m_aEnemys = value; }
        internal List<CPlayerData> Players { get => m_aPlayers; set => m_aPlayers = value; }

        internal CRateData SelectRateData { get => m_SelectRateData; set => m_SelectRateData = value; }

        CDataBase()
        {
            SelectRateData = new CRateData();
            Rates.Add(SelectRateData);
        }



    }
}
