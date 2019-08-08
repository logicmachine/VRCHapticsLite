using System.Windows.Media;
using Reactive.Bindings;

namespace VRCHapticsLite
{
    class ModuleConfigViewModel
    {
        public ReactiveProperty<ImageSource> Image { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<bool> Enabled { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<int> Power { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> X { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> Y { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> Width { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> Height { get; } = new ReactiveProperty<int>(0);
    }
}
