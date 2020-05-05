using System;

namespace NCsv
{
    /// <summary>
    /// CSVの検証に失敗した場合にスローされる例外です。
    /// </summary>
    public class CsvValidationException : Exception
    {
        /// <summary>
        /// 行番号を取得します。
        /// </summary>
        public long LineNumber { get; private set; }

        /// <summary>
        /// <see cref="CsvValidationException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="lineNumber">行番号。</param>
        public CsvValidationException(string message, long lineNumber)
            : base(message)
        {
            this.LineNumber = lineNumber;
        }

        /// <summary>
        /// <see cref="CsvValidationException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="lineNumber">行番号。</param>
        /// <param name="innerException">例外のもととなった例外。</param>
        public CsvValidationException(string message, long lineNumber, Exception innerException)
            : base(message, innerException)
        {
            this.LineNumber = lineNumber;
        }
    }
}
