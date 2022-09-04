using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5.Message {
    public enum WebSocketOpCode {
        Hello = 0,
        Identify = 1,
        Identified = 2,
        Reidentify = 3,
        Event = 5,
        Request = 6,
        RequestResponse = 7,
        RequestBatch = 8,
        RequestBatchResponse = 9
    }
}
