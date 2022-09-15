using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5.Message {
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
    public class Hello : Data<Hello.D> {
        public Hello() : base(WebSocketOpCode.Hello) {}

        public class D {
            public string obsWebSocketVersion;
            public int rpcVersion;
            public Authentication? authentication;
        };

        public class Authentication {
            public string challenge;
            public string salt;
        }
    }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
}
