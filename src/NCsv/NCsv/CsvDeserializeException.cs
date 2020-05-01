using System;

namespace NCsv
{
    /// <summary>
    /// 逆シリアル化時にエラーが発生した場合にスローされます。
    /// </summary>
    public class CsvDeserializeException : Exception
    {
        /// <summary>
        /// 行番号を取得します。
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// <see cref="CsvDeserializeException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="lineNumber">行番号。</param>
        public CsvDeserializeException(string message, int lineNumber)
            : base(message)
        {
            this.LineNumber = lineNumber;
        }

        /// <summary>
        /// <see cref="CsvDeserializeException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="lineNumber">行番号。</param>
        /// <param name="innerException">例外のもととなった例外。</param>
        public CsvDeserializeException(string message, int lineNumber, Exception innerException)
            : base(message, innerException)
        {
            this.LineNumber = lineNumber;
        }
    }
}
