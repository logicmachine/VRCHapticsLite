namespace VRCHapticsLite
{
    class MainViewModel
    {
        public ModuleConfigViewModel Head { get; } = new ModuleConfigViewModel();
        public ModuleConfigViewModel Vest { get; } = new ModuleConfigViewModel();
        public ModuleConfigViewModel LeftArm { get; } = new ModuleConfigViewModel();
        public ModuleConfigViewModel RightArm { get; } = new ModuleConfigViewModel();
    }
}
