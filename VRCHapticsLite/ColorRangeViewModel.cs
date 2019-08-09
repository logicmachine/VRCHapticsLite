using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace VRCHapticsLite
{
    class ColorRangeViewModel
    {
        public ReactiveProperty<int> MinR { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> MaxR { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> MinG { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> MaxG { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> MinB { get; } = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> MaxB { get; } = new ReactiveProperty<int>(0);
    }
}
