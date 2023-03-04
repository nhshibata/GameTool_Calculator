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
    /// RateTab.xaml の相互作用ロジック
    /// </summary>
    public partial class RateTab : UserControl
    {
        public RateTab()
        {
            InitializeComponent();
        }

        private void RateWrite(object sender, RoutedEventArgs e)
        {
            if (RateFileName.Text != null)
            {
                CDataBase.m_pInstance.SelectRateData.FilePath = RateFileName.Text;
                CDataBase.m_pInstance.SelectRateData.Write();
                return;
            }

            DispDialog dd = new DispDialog();
            string name = dd.OpenDialog();
            if (name != null)
            {
                CDataBase.m_pInstance.SelectRateData.FilePath = name;
                CDataBase.m_pInstance.SelectRateData.Write();
            }
        }

        private void RateComboBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            RateComboBox.Items.Clear();
            for (int i = 0; i < CDataBase.m_pInstance.Rates.Count; i++)
            {
                ComboBoxItem box = new ComboBoxItem();
                // 名前設定
                box.Content = (CDataBase.m_pInstance.Rates[i] as CRateData).Name;
                // デリゲートの追加
                box.PreviewMouseLeftButtonUp += BoxItem_PreviewMouseLeftButtonUp;

                RateComboBox.Items.Add(box);
            }

        }

        /// <summary>
        /// 新しいレート生成
        /// 同時に現在に設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickNewRate(object sender, RoutedEventArgs e)
        {
            // 名前が設定されてないなら、保存しない
            if (CDataBase.m_pInstance.SelectRateData.Name == null)
            {
                CDataBase.m_pInstance.DataDelete(EDataType.RATE);
            }
            // 新しいデータ作成
            CRateData rateData = new CRateData();
            CDataBase.m_pInstance.Rates.Add(rateData);
            CDataBase.m_pInstance.SelectRateData = rateData;

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
            foreach (CRateData item in CDataBase.m_pInstance.Rates)
            {
                if (item.Name != boxItem.Content as string)
                    continue;

                CDataBase.m_pInstance.SelectRateData = item;

                BindingUpdate();
                break;
            }
        }

        /// <summary>
        /// xaml側の表示更新
        /// </summary>
        public void BindingUpdate()
        {
            var be = RateNameBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateTarget();
            be = RatePriceBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateTarget();
            be = RateValueBox.GetBindingExpression(TextBox.TextProperty);
            be.UpdateTarget();
        }

    }
}
