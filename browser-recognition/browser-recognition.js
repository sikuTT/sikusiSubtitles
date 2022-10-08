SpeechRecognition = webkitSpeechRecognition || SpeechRecognition;

class Recognizer {
    running = false;

    constructor(interimResults) {
        // URLを取得
        let url = new URL(window.location.href);

        // 音声認識する言語を取得
        let lang = url.searchParams.get('lang');

        // 結果の送信先
        let port= url.searchParams.get('port');

        // WebSocket
        var webSocket = new WebSocket(`ws://127.0.0.1:${port}/`);
        webSocket.onopen = () => console.log('WebSocket Connected');
        webSocket.onclose = () => window.close();
        webSocket.onerror = () => window.close();

        this.recognition = new SpeechRecognition();
        console.log(this.recognition.lang);
        if (lang)
            this.recognition.lang = lang;
        this.recognition.interimResults = interimResults;
        this.recognition.continuous = false;

        this.recognition.onstart = (e) => {
            console.log('onstart');
            this.running = true;
        }

        this.recognition.onend = (e) => {
            console.log('Recognizer.onend');
            if (this.running) {
                this.recognition.start();
            }
        }

        /*
        this.recognition.onaudiostart = (e) => {
            console.log('onaudiostart', e);
        }

        this.recognition.onaudioend = (e) => {
            console.log('onaudioend', e);
        }
        */

        this.recognition.onresult = (e) => {
            console.log('Recognizer.onresult', e);
            console.log('length = ', e.results.length);
            var text = '';
            var isFinal = true;
            for (const result of e.results) {
                text += result[0].transcript;
                if (result.isFinal === false)
                    isFinal = false;
            }
            console.log(isFinal + ': ' + text);

            // 音声認識結果を表示
            const elem = document.getElementById('result');
            elem.innerText = text;

            // 音声認識結果を送信する
            var data = { text, recognized: isFinal };
            webSocket.send(JSON.stringify(data));
        }

        /*
        this.recognition.onnomatch = (e) => {
            console.log('onnomatch', e);
        }

        this.recognition.onerror = (e) => {
            console.log('onerror', e);
        }
        */
        this.recognition.onerror = (e) => {
            console.log('Recognizer.onerror', e);
        }

        this.recognition.onsoundstart = (e) => {
            console.log('Recognizer.onsoundstart', e);
        }
        this.recognition.onsoundend = (e) => {
            console.log('Recognizer.onsoundend', e);
        }
        this.recognition.onspeechstart = (e) => {
            console.log('Recognizer.onspeechstart', e);
        }
        this.recognition.onspeechend = (e) => {
            console.log('Recognizer.onspeechend', e);
        }
    }

    start() {
        console.log('Recognizer start');
        if (this.recognition !== undefined) {
            return this.recognition.start();
        }
    }

    stop() {
        console.log('Recognizer stop');
        if (this.recognition !== undefined) {
            this.running = false;
            return this.recognition.stop();
        }
    }

    abort() {
        console.log('Recognizer abort');
        if (this.recognition !== undefined) {
            this.running = false;
            this.recognition.abort();
        }
    }
}