﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsWebSocket5.Message.Data {
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
    public class GetSceneListResponse : ResponseData {
        public string currentProgramSceneName;
        public string currentPreviewSceneName;
        public List<Scene> scenes;
    }

    public class Scene {
        public int sceneIndex;
        public string sceneName;
    }
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。Null 許容として宣言することをご検討ください。
}