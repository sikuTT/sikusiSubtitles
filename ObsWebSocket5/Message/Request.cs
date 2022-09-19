using ObsWebSocket5.Message.Data.Request;

namespace ObsWebSocket5.Message {
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
    public class Request : Data<Request.D> {
        public Request() : base(WebSocketOpCode.Request) {
            d = new D();
        }

    public class D {
            public string requestType;
            public string requestId;
            public RequestData? requestData;
        }
    }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
}
