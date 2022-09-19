SpeechRecognition = webkitSpeechRecognition || SpeechRecognition;

class Recognizer {
    running = false;

    constructor(interimResults) {
        console.log('class Recognizer');

        // URLを取得
        let url = new URL(window.location.href);

        // URLSearchParamsオブジェクトを取得
        let lang = url.searchParams.get('lang');

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
            console.log(text);
            console.log(isFinal);
            const elem = document.getElementById('result');
            elem.innerText = text;
            elem.setAttribute('data-is-final', isFinal.toString());
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