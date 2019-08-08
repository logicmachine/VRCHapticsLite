using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Networking.Sockets;

namespace VRCHapticsLite
{
    enum HapticsPosition : int
    {
        All = 0,
        Left = 1,
        Right = 2,
        Vest = 3,
        Head = 4,
        Racket = 5,
        HandL = 6,
        HandR = 7,
        FootL = 8,
        FootR = 9,
        ForearmL = 10,
        ForearmR = 11,
        VestFront = 201,
        VestBack = 202,
        GloveLeft = 203,
        GloveRight = 204,
        Custom1 = 251,
        Custom2 = 252,
        Custom3 = 253,
        Custom4 = 254
    }

    class HapticsPlayer
    {
        private MessageWebSocket _webSocket;

        public HapticsPlayer()
        {
        }

        private async void WebSocket_Closed(IWebSocket sender, WebSocketClosedEventArgs args)
        {
            _webSocket?.Dispose();
            _webSocket = null;
            await SetupAsync();
        }

        public async Task SetupAsync()
        {
            var ws = new MessageWebSocket();
            ws.MessageReceived += WebSocket_MessageReceived;
            ws.Closed += WebSocket_Closed;
            while (true)
            {
                try
                {
                    await ws.ConnectAsync(new Uri("ws://127.0.0.1:15881/v2/feedbacks"));
                    break;
                }
                catch (Exception ex)
                {
                    var status = WebSocketError.GetStatus(ex.GetBaseException().HResult);
                    await Task.Delay(1000);
                }
            }
            ws.Control.MessageType = SocketMessageType.Utf8;
            _webSocket = ws;
        }

        private void WebSocket_MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
        {
            try
            {
                using (var reader = args.GetDataReader())
                {
                    reader.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;
                    var message = reader.ReadString(reader.UnconsumedBufferLength);
                }
            }
            catch (Exception ex)
            {
                var status = WebSocketError.GetStatus(ex.GetBaseException().HResult);
            }
        }

        public async Task SendMessage(HapticsPosition pos, byte[] data)
        {
            if(_webSocket == null) { return; }
            // {
            //   "Register": [],
            //   "Submit": [
            //     {
            //       "Type": "frame",
            //       "Key": "key",
            //       "Frame": {
            //         "DurationMillis": 1000,
            //         "Position": PositionType,
            //         "Texture": 0,
            //         "DotPoints": [
            //           {
            //             "Index": 0,
            //             "Intensity": 100
            //           }
            //         ],
            //         "PathPoint": []
            //       }
            //     }
            //   ]
            // }
            var builder = new StringBuilder();
            builder.Append("{");
            builder.Append("\"Register\":[],");
            builder.Append("\"Submit\":[{");
            builder.Append("\"Type\":\"frame\",");
            builder.AppendFormat("\"Key\":\"{0}\",", pos.ToString());
            builder.Append("\"Frame\":{");
            builder.Append("\"DurationMillis\":200,");
            builder.AppendFormat("\"Position\":{0},", (int)pos);
            builder.Append("\"Texture\":0,");
            builder.Append("\"DotPoints\":[");
            for (int i = 0; i < data.Length; ++i)
            {
                if (i != 0) { builder.Append(","); }
                builder.Append("{");
                builder.AppendFormat("\"Index\":{0},", i);
                builder.AppendFormat("\"Intensity\":{0}", (int)data[i]);
                builder.Append("}");
            }
            builder.Append("],");
            builder.Append("\"PathPoints\":[]");
            builder.Append("}");
            builder.Append("}]");
            builder.Append("}");
            var json = builder.ToString();
            using (var writer = new DataWriter(_webSocket.OutputStream))
            {
                writer.WriteString(json);
                await writer.StoreAsync();
                writer.DetachStream();
            }
        }
    }
}
