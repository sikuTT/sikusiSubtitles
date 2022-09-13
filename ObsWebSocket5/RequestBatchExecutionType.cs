using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5 {
    public enum RequestBatchExecutionType {
        None = -1,
        SerialRealtime = 0,
        SerialFrame = 1,
        Parallel = 2,
    }
}
