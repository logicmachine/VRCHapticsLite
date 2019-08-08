using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VRCHapticsLite
{
    class CapturedBitmap
    {
        SharpDX.Direct3D11.Texture2D _texture;

        public CapturedBitmap(SharpDX.Direct3D11.Texture2D texture)
        {
            _texture = texture;
        }

        public byte[] GetPixelBytes(int left, int top, int width, int height)
        {
            if(width <= 0 || height <= 0) { return new byte[] { }; }
            var device = _texture.Device;
            var description =
                new SharpDX.Direct3D11.Texture2DDescription()
                {
                    Width = width,
                    Height = height,
                    Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                    ArraySize = 1,
                    MipLevels = 1,
                    SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                    Usage = SharpDX.Direct3D11.ResourceUsage.Staging,
                    BindFlags = SharpDX.Direct3D11.BindFlags.None,
                    CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.Read,
                    OptionFlags = SharpDX.Direct3D11.ResourceOptionFlags.None
                };
            byte[] result = new byte[width * height * 4];
            using (var staging = new SharpDX.Direct3D11.Texture2D(device, description))
            {
                var textureWidth  = _texture.Description.Width;
                var textureHeight = _texture.Description.Height;
                var srcX0 = Math.Max(0, left);
                var srcY0 = Math.Max(0, top);
                var srcX1 = Math.Min(textureWidth,  Math.Max(0, left + width));
                var srcY1 = Math.Min(textureHeight, Math.Max(0, top  + height));
                var region = new SharpDX.Direct3D11.ResourceRegion(srcX0, srcY0, 0, srcX1, srcY1, 1);
                var dstX0 = Math.Max(0, -left);
                var dstY0 = Math.Max(0, -top);
                device.ImmediateContext.CopySubresourceRegion(_texture, 0, region, staging, 0, dstX0, dstY0);
                var box = device.ImmediateContext.MapSubresource(
                    staging, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);
                for(int i = 0; i < height; ++i)
                {
                    Marshal.Copy(box.DataPointer + i * box.RowPitch, result, i * width * 4, width * 4);
                }
                device.ImmediateContext.UnmapSubresource(staging, 0);
            }
            return result;
        }
    }
}
