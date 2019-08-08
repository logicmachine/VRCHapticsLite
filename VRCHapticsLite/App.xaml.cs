using System.Windows;
using Windows.System;

namespace VRCHapticsLite
{
    public partial class App : Application
    {
        public App()
        {
            _controller = CoreMessagingHelper.CreateDispatcherQueueControllerForCurrentThread();
        }

        private DispatcherQueueController _controller;
    }
}
