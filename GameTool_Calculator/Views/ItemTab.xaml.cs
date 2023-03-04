using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameTool_Calculator
{
    /// <summary>
    /// ItemTab.xaml の相互作用ロジック
    /// </summary>
    public partial class ItemTab : UserControl
    {
        public ItemTab()
        {
            InitializeComponent();
        }

        private void ItemWrite(object sender, RoutedEventArgs e)
        {
            // ファイル保存名が設定されている時
            if (ItemFileName.Text != null)
            {
                CDataBase.m_pInstance.SelectItemData.FilePath = ItemFileName.Text;
                CDataBase.m_pInstance.SelectItemData.Write();
                return;
            }

            // ファイル保存名が設定されていない時は、上書き判定にする
            DispDialog dd = new DispDialog();
            string name = dd.OpenDialog();
            if (name != null)
            {
                CDataBase.m_pInstance.SelectItemData.FilePath = name;
                CDataBase.m_pInstance.SelectItemData.Write();
            }
        }

        private void DispItemComboBox(object sender, MouseButtonEventArgs e)
        {
            ItemComboBox.Items.Clear();
            for (int i = 0; i < CDataBase.m_pInstance.Items.Count; i++)
            {
                ComboBoxItem box = new ComboBoxItem();
                // 名前設定
                box.Content = (CDataBase.m_pInstance.Items[i] as CItemData).Name;
                // デリゲートの追加
                box.PreviewMouseLeftButtonUp += BoxItem_PreviewMouseLeftButtonUp;

                ItemComboBox.Items.Add(box);
            }

        }

        /// <summary>
        /// 新しいレート生成
        /// 同時に現在に設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickNewItem(object sender, RoutedEventArgs e)
        {
            // 名前が設定されてないなら、保存しない
            if (CDataBase.m_pInstance.SelectItemData.Name == null)
            {
                CDataBase.m_pInstance.DataDelete(EDataType.ITEM);
            }
            // 新しいデータ作成
            CItemData ItemData = new CItemData();
            CDataBase.m_pInstance.Items.Add(ItemData);
            CDataBase.m_pInstance.SelectItemData = ItemData;

            BindingUpdate();
        }

        /// <summary>
        /// コンボボックスから選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ComboBoxItem boxItem = sender as ComboBoxItem;
            foreach (CItemData item in CDataBase.m_pInstance.Items)
            {
                if (item.Name != boxItem.Content as string)
                    continue;

                CDataBase.m_pInstance.SelectItemData = item;

                BindingUpdate();
                break;
            }
        }

        /// <summary>
        /// xaml側の表示更新
        /// </summary>
        public void BindingUpdate()
        {
            var be = ItemNameBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateTarget();
            be = ItemMoneyBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateTarget();
        }


    }
}
