﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5.Message {
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
    public class RequestBatch : Data<RequestBatch.D> {
        public RequestBatch() : base(WebSocketOpCode.RequestBatch) {
            d = new D();
        }

        public class D {
            public string requestId;
            public bool? haltOnFailure;
            public int? executionType;
            public object[] requests;

        }
    }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
}
