using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sikusiSubtitles.Translation {
    public abstract class TranslationService : sikusiSubtitles.Service {
        public TranslationService(ServiceManager serviceManager, string name, string displayName, int index) : base(serviceManager, TranslationServiceManager.ServiceName, name, displayName, index) {
        }

        /**
         * テキストを翻訳する
         * 
         * @param[in] text 翻訳をするテキスト
         * @param[in] from 翻訳元のテキストの言語。nullの場合は自動判定する
         * @param[in] to 翻訳先の言語を指定する
         */
        public abstract Task<TranslationResult> TranslateAsync(string text, string? from, string to);

        /**
         * テキストを翻訳する
         * 
         * @param[in] text 翻訳をするテキスト
         * @param[in] from 翻訳元のテキストの言語。nullの場合は自動判定する
         * @param[in] toList 翻訳先の言語を指定する
         */
        public abstract Task<TranslationResult> TranslateAsync(string text, string? from, string[] toList);

        /**
         * 翻訳先として指定可能な言語の一覧を返す
         * 
         * @return Tuple<string, string>のリストでItem1が内部で使用する文字コード（APIに渡す文字コード）でItem2が画面に表示する文字コード。
         * {"ja-JP", "日本語"}, {"en-US", "英語"} のような感じ。
         */
        public abstract List<Tuple<string, string>> GetLanguages();
    }
}
