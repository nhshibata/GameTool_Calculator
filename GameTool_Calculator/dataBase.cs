using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace GameTool_Calculator
{
    enum EDataType : int
    {
        RATE,
        ITEM,
        ENEMY,
        PLAYER,
        MAX
    }

    [DataContract] // シリアライズ用属性
    class CDataBase : jsonData
    {
        static public CDataBase m_pInstance = new CDataBase();

        //--- 参照用
        List<jsonData> m_aSelectData = new List<jsonData>((int)EDataType.MAX);

        internal CRateData SelectRateData {
            get => m_aSelectData[(int)EDataType.RATE] as CRateData; set => m_aSelectData[(int)EDataType.RATE] = value; }
        internal CItemData SelectItemData {
            get => m_aSelectData[(int)EDataType.ITEM] as CItemData; set => m_aSelectData[(int)EDataType.ITEM] = value; }
        internal CEnemyData SelectEnemyData {
            get => m_aSelectData[(int)EDataType.ENEMY] as CEnemyData; set => m_aSelectData[(int)EDataType.ENEMY] = value;}
        internal CPlayerData SelectPlayerData {
            get => m_aSelectData[(int)EDataType.PLAYER] as CPlayerData; set => m_aSelectData[(int)EDataType.PLAYER] = value; }

        //--- 管理用
        [DataMember]
        List<CRateData> m_aRateDatas = new List<CRateData>();
        [DataMember]
        List<CItemData> m_aItemDatas = new List<CItemData>();
        [DataMember]
        List<CEnemyData> m_aEnemyDatas = new List<CEnemyData>();
        [DataMember]
        List<CPlayerData> m_aPlayerDatas = new List<CPlayerData>();

        internal List<CRateData> Rates { get => m_aRateDatas; set => m_aRateDatas = value; }
        internal List<CItemData> Items { get => m_aItemDatas; set => m_aItemDatas = value; }
        internal List<CEnemyData> Enemys { get => m_aEnemyDatas; set => m_aEnemyDatas = value; }
        internal List<CPlayerData> Players { get => m_aPlayerDatas; set => m_aPlayerDatas = value; }


        CDataBase()
        {
            // 保存ファイル名
            FilePath = "DataBase.json";

            m_aSelectData.Add(new CRateData());
            m_aSelectData.Add(new CItemData());
            m_aSelectData.Add(new CEnemyData());
            m_aSelectData.Add(new CPlayerData());

            // 初期データ設定
            Rates.Add(SelectRateData);

            Items.Add(SelectItemData);

            Enemys.Add(SelectEnemyData);

            Players.Add(SelectPlayerData);

        }

        ~CDataBase()
        {
            Write();
        }


        /// <summary>
        /// 選択中のデータを消去
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool DataDelete(EDataType type)
        {
            switch (type)
            {
                case EDataType.RATE:
                    // なければfalse、あれば消去
                    if (SelectRateData == null)
                        return false;
                    Rates.Remove(SelectRateData);
                    // 配列が空でなければ先頭を格納
                    SelectRateData = Rates.Count == 0 ? null : Rates.First();
                    return true;
                case EDataType.ITEM:
                    // なければfalse、あれば消去
                    if (SelectItemData == null)
                        return false;
                    Items.Remove(SelectItemData);
                    // 配列が空でなければ先頭を格納
                    SelectItemData = Items.Count == 0 ? null : Items.First();
                    return true;
                case EDataType.ENEMY:
                    // なければfalse、あれば消去
                    if (SelectEnemyData == null)
                        return false;
                    Enemys.Remove(SelectEnemyData);
                    // 配列が空でなければ先頭を格納
                    SelectEnemyData = Enemys.Count == 0 ? null : Enemys.First();
                    return true;
                case EDataType.PLAYER:
                    // なければfalse、あれば消去
                    if (SelectPlayerData == null)
                        return false;
                    Players.Remove(SelectPlayerData);
                    // 配列が空でなければ先頭を格納
                    SelectPlayerData = Players.Count == 0 ? null : Players.First();
                    return true;
            }
            return false;
        }


        protected override void ReadCallBack(object json)
        {
            CDataBase DB = json as CDataBase;
            if (DB == null)
                return;

            this.Rates = DB.Rates;
            this.Items = DB.Items;
            this.Enemys = DB.Enemys;
            this.Players = DB.Players;

            // 選択設定
            // 0ならnull
            if(DB.Rates.Count() != 0)
                this.SelectRateData = DB.Rates.First() as CRateData;
            if(DB.Items.Count() != 0)
                this.SelectItemData = DB.Items.First() as CItemData;
            if(DB.Enemys.Count() != 0)
                this.SelectEnemyData = DB.Enemys.First() as CEnemyData;
            if(DB.Players.Count() != 0)
                this.SelectPlayerData = DB.Players.First() as CPlayerData;
        }

    }
}
