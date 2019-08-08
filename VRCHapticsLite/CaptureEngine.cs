using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;
using Windows.Graphics.DirectX.Direct3D11;

namespace VRCHapticsLite
{
    delegate void FrameArrivedEventHandler(CapturedBitmap frame);

    class CaptureEngine : IDisposable
    {
        private Direct3D11CaptureFramePool _framePool;
        private GraphicsCaptureSession _session;
        private SizeInt32 _lastSize;

        private IDirect3DDevice _device;
        private SharpDX.Direct3D11.Device _d3dDevice;

        public event FrameArrivedEventHandler FrameArrived;

        public CaptureEngine(IDirect3DDevice device, GraphicsCaptureItem item)
        {
            _device = device;
            _d3dDevice = Direct3D11Helper.CreateSharpDXDevice(_device);

            _framePool = Direct3D11CaptureFramePool.Create(
                _device, DirectXPixelFormat.B8G8R8A8UIntNormalized, 2, item.Size);
            _session = _framePool.CreateCaptureSession(item);
            _lastSize = item.Size;

            _framePool.FrameArrived += OnFrameArrived;
        }

        public void Dispose()
        {
            _session?.Dispose();
            _framePool?.Dispose();
            _d3dDevice?.Dispose();
        }

        public void StartCapture()
        {
            _session.StartCapture();
        }

        private void OnFrameArrived(Direct3D11CaptureFramePool sender, object args)
        {
            var newSize = false;
            using (var frame = sender.TryGetNextFrame())
            {
                if (frame.ContentSize.Width != _lastSize.Width ||
                    frame.ContentSize.Height != _lastSize.Height)
                {
                    newSize = true;
                    _lastSize = frame.ContentSize;
                }
                using (var bitmap = Direct3D11Helper.CreateSharpDXTexture2D(frame.Surface))
                {
                    FrameArrived?.Invoke(new CapturedBitmap(bitmap));
                }
            }
            if (newSize)
            {
                _framePool.Recreate(_device, DirectXPixelFormat.B8G8R8A8UIntNormalized, 2, _lastSize);
            }
        }
    }
}
