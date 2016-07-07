using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using SpeechLib;

namespace CommonLibs.Speech
{
    public class SpeechWriter
    {
        private readonly SpVoice _voice;
        private readonly List<string> _errors;

        public bool IsError { get { return _errors.Any(); } }
        public List<string> Errors { get { return _errors; } }

        public class Voice
        {
            private readonly SpObjectToken _spObjectToken;
            internal Voice(SpObjectToken token)
            {
                _spObjectToken = token;
            }

            public string Name { get { return _spObjectToken.GetDescription(); } }

            internal SpObjectToken Token { get { return _spObjectToken; } }

        }

        public SpeechWriter()
        {
            _voice = new SpVoice();
            
            var voices = _voice.GetVoices(null, "Language=419");
            _voice.Voice = voices.Item(0);

            //TODO: KOA: необходимо избавится от SpMemoryStream...
            _voice.AudioOutputStream = new SpMemoryStream(); //= new SpeechBufferStream();
            _errors = new List<string>();
        }

        public Voice[] AvailibleVoices()
        {
            var tokens = _voice.GetVoices(null, "Language=419");

            var voices = new Voice[tokens.Count];
            for (var i = 0; i < tokens.Count; i++)
            {
                var token = tokens.Item(i) as SpObjectToken;
                var voice = new Voice(token);
                voices[i] = voice;
            }
            return voices;
        }


        public void Write(string textLine, Stream outStream, Voice voice, int speed)
        {
            _voice.Rate = speed;
            _voice.Voice = voice.Token;
            Write(textLine, outStream);
        }

        public void Write(string textLine, Stream outStream, Voice voice)
        {
            _voice.Voice = voice.Token;
            Write(textLine, outStream);
        }

        public void Write(string textLine, Stream outStream, Voice voice, int speed, int volume)
        {
            _voice.Volume = volume;
            _voice.Rate = speed;
            _voice.Voice = voice.Token;
            Write(textLine, outStream);
        }

        public void Write(string textLine, Stream outStream)
        {
            if (string.IsNullOrEmpty(textLine) || outStream == null || !outStream.CanWrite)
                return;

            RepeatSpeak(textLine);
            var spFileStream = ((SpMemoryStream)_voice.AudioOutputStream);
            //object buf;
            //var count = spFileStream.Read(out buf, 59000);
            //var data2 = buf as byte[];
            //var s0 = data2.Max();
            var data = spFileStream.GetData() as byte[];
            if (data != null)
                outStream.Write(data, 0, data.Length);
            //var s = data.Max();

            //var dataw = spFileStream.GetData() as byte[];
            //var t = dataw.Length;
            //var s2 = dataw.Max();
            //Voice.AudioOutputStream.Write()
            //return spFileStream.GetData() as byte[];
        }

        private void RepeatSpeak(string textLine)
        {
            if (string.IsNullOrEmpty(textLine))
                return;

            _voice.AudioOutputStream = new SpMemoryStream();

            var isDone = TrySpeak(textLine);
            if (isDone)
                return;

            var words = textLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length > 1)
            {
                var len = words.Length / 2;
                RepeatSpeak(string.Join(" ", words, 0, len));
                RepeatSpeak(string.Join(" ", words, len, words.Length - len));
            }
            else
            {
                var len = textLine.Length / 2;
                RepeatSpeak(string.Join(" ", textLine, 0, len));
                RepeatSpeak(string.Join(" ", textLine, len, textLine.Length - len));
            }
        }

        private bool TrySpeak(string textLine)
        {
            try
            {
                _voice.Speak(textLine);
                _voice.WaitUntilDone(Timeout.Infinite);
                return true;
            }
            catch (Exception ex)
            {
                //Логируем ошибку для дальнейшего анализа.
                //Предпологается что код сюда попадать не должен,
                //а если попал, то будет предпринята попытка исправить положение
                _errors.Add(string.Format("{0}: \r\n {1}", ex, textLine));
            }
            return false;
        }

    }
}
