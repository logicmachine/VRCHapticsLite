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
    /// IntegerTextBox.xaml の相互作用ロジック
    /// </summary>
    public partial class IntegerTextBox : UserControl
    {
        public IntegerTextBox()
        {
            InitializeComponent();
            this.Value = 0;
        }

        public static readonly DependencyProperty ValueProperty =
             DependencyProperty.Register(
                 "Value", typeof(int), typeof(IntegerTextBox),
                 new PropertyMetadata(0, new PropertyChangedCallback(OnValueChanged)));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
                textBox.Text = value.ToString();
            }
        }

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as IntegerTextBox;
            if (control != null)
            {
                control.textBox.Text = control.Value.ToString();
            }
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            var control = sender as TextBox;
            if (control != null)
            {
                int value = 0;
                if (int.TryParse(control.Text, out value))
                {
                    this.Value = value;
                }
                else
                {
                    this.Value = 0;
                }
                control.Text = Value.ToString();
            }
        }

        private void IncrementButtonClick(object sender, RoutedEventArgs e)
        {
            if (Value < int.MaxValue) { Value = Value + 1; }
        }

        private void DecrementButtonClick(object sender, RoutedEventArgs e)
        {
            if (Value > 0) { Value = Value - 1; }
        }
    }
}
