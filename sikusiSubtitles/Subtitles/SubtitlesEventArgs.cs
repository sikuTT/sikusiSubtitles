namespace sikusiSubtitles.Subtitles {
    public class TranslationText {
        public string Language { get; set; } = "";
        public string Text { get; set; } = "";

        public TranslationText() {}

        public TranslationText(string language) {
            Language = language;
        }

        public TranslationText(string language, string text) {
            Language = language;
            Text = text;
        }
    }

    public class SubtitlesText {
        public bool Recognized { get; set; }
        public string VoiceText { get; set; }
        public List<TranslationText> TranslationTexts { get; set; }

        public SubtitlesText(bool recognized, string voiceText, List<TranslationText> translationTexts) {
            this.Recognized = recognized;
            this.VoiceText = voiceText;
            this.TranslationTexts = translationTexts;
        }
    }

    public class SubtitlesEventArgs {
    }
}
