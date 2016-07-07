using System;
using SpeechLib;

namespace CommonLibs.Speech
{
    internal class SpeechBufferStream : ISpeechBaseStream
    {
        private byte[] _buffer;

        public int Read(out object buffer, int numberOfBytes)
        {
            var clone = new byte[numberOfBytes];
            Array.Copy(_buffer, 0, clone, 0, numberOfBytes);
            buffer = clone;
            return Math.Min(numberOfBytes, _buffer.Length);
        }

        public int Write(object buffer)
        {
            _buffer = buffer as byte[];
            if (_buffer != null)
                return _buffer.Length;
            return 0;
        }

        public object Seek(object position, SpeechStreamSeekPositionType origin = SpeechStreamSeekPositionType.SSSPTRelativeToStart)
        {
            var pos = (int)position;
            return _buffer[pos];
        }

        public SpAudioFormat Format
        {
            get;
            set;
        }
    }
}