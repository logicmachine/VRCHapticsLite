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

namespace VRCHapticsLite
{
    /// <summary>
    /// ColorRangeControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorRangeControl : UserControl
    {
        public ColorRangeControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register(
                "LabelText", typeof(string), typeof(ColorRangeControl),
                new PropertyMetadata(
                    string.Empty,
                    new PropertyChangedCallback((s, a) => { (s as ColorRangeControl).OnLabelTextChanged(s, a); })));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        private void OnLabelTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            LabelBlock.Text = args.NewValue.ToString();
        }
    }
}
