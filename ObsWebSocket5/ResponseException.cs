using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5 {
    public class ResponseException : Exception {
        public RequestStatusCode? Code { get; set; }
        public ResponseException(RequestStatusCode? code, string? message) : base(message) {
            this.Code = code;
        }
    }
}
