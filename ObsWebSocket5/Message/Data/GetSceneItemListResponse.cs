using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5.Message.Data {
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
    public class GetSceneItemListResponse : ResponseData {
        public SceneItems[] sceneItems;
    }

    public class SceneItems {
        public string inputKind;
        public bool? isGroup;
        public string sceneItemBlendMode;
        public bool sceneItemEnabled;
        public int sceneItemId;
        public int sceneItemIndex;
        public bool sceneItemLocked;
        public SceneItemTransform sceneItemTransform;
        public string sourceName;
        public string sourceType;
    }

    public class SceneItemTransform {
        public int alignment;
        public int boundsAlignment;
        public double boundsHeight;
        public string boundsType;
        public double boundsWidth;
        public int cropBottom;
        public int cropLeft;
        public int cropRight;
        public int cropTop;
        public double height;
        public double positionX;
        public double positionY;
        public double rotation;
        public double scaleX;
        public double scaleY;
        public double sourceHeight;
        public double sourceWidth;
        public double width;
    }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
}
