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
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 描画完了後呼び出し
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            ComboBoxItem box = new ComboBoxItem();
            box.Content = CDataBase.m_pInstance.SelectRateData.Name;
            // デリゲートの追加
            box.PreviewMouseLeftButtonUp += BoxItem_PreviewMouseLeftButtonUp;
            RateComboBox.Items.Add(box);

        }

        /// <summary>
        /// 新しいレート生成
        /// 同時に現在に設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickNewRate(object sender, RoutedEventArgs e)
        {
            CRateData rateData = new CRateData();
            CDataBase.m_pInstance.Rates.Add(rateData);
            CDataBase.m_pInstance.SelectRateData = rateData;

            // xamlへの追加
            ComboBoxItem box = new ComboBoxItem();
            box.Content = rateData.Name;
            // デリゲートの追加
            box.PreviewMouseLeftButtonUp += BoxItem_PreviewMouseLeftButtonUp;
            RateComboBox.Items.Add(box);
        }

        private void RateWrite(object sender, RoutedEventArgs e)
        {
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

            for (int i = 0; i < RateComboBox.Items.Count; i++)
            {
                ComboBoxItem box = RateComboBox.Items[i] as ComboBoxItem;
                box.Content = CDataBase.m_pInstance.Rates[i].Name;
            }
            
        }

        /// <summary>
        /// コンボボックスから選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BoxItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ComboBoxItem boxItem = sender as ComboBoxItem;
            foreach (var item in CDataBase.m_pInstance.Rates)
            {
                if(item.Name == boxItem.Content as string)
                {
                    CDataBase.m_pInstance.SelectRateData = item;

                    // xaml側の更新
                    var be = RateNameBox.GetBindingExpression(TextBox.TextProperty);
                    be.UpdateTarget();
                    be = RatePriceBox.GetBindingExpression(TextBox.TextProperty);
                    be.UpdateTarget();
                    be = RateValueBox.GetBindingExpression(TextBox.TextProperty);
                    be.UpdateTarget();
                    break;
                }
            }
        }

  

    }
}
