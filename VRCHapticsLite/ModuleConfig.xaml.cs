using System.Windows;
using System.Windows.Controls;

namespace VRCHapticsLite
{
    /// <summary>
    /// ModuleConfig.xaml の相互作用ロジック
    /// </summary>
    public partial class ModuleConfig : UserControl
    {
        public ModuleConfig()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ModuleNameProperty =
            DependencyProperty.Register(
                "ModuleName", typeof(string), typeof(ModuleConfig),
                new PropertyMetadata(
                    string.Empty,
                    new PropertyChangedCallback((s, a) => { (s as ModuleConfig).OnModuleNameChanged(s, a); })));

        public string ModuleName
        {
            get { return (string)GetValue(ModuleNameProperty); }
            set { SetValue(ModuleNameProperty, value); }
        }

        private void OnModuleNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            moduleNameBlock.Text = args.NewValue.ToString();
        }
    }
}
