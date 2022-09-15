using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5.Message {
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
    public class RequestResponse<T> : Data<RequestResponse<T>.D> {
        RequestResponse() : base(WebSocketOpCode.RequestResponse) { }

        public class D {
            public string requestType;
            public string requestId;
            public RequestStatus requestStatus;
            public T? responseData;
        }

        public class RequestStatus {
            public bool result;
            public ObsWebSocket5.RequestStatus code;
            public string? comment;

        }
    }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
}
