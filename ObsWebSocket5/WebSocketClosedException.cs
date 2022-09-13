using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5 {
    public class WebSocketClosedException : Exception {
        public WebSocketCloseCode? Code { get; set; }
        public WebSocketClosedException(WebSocketCloseCode? code, string? message) : base(message) {
            this.Code = code;
        }
    }
}
