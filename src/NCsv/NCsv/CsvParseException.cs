using System;

namespace NCsv
{
    /// <summary>
    /// CSVの解析に失敗した場合にスローされる例外です。
    /// </summary>
    public class CsvParseException : Exception
    {
        /// <summary>
        /// 行番号を取得します。
        /// </summary>
        public long LineNumber { get; private set; }

        /// <summary>
        /// エラー行を取得します。
        /// </summary>
        public string ErrorLine { get; private set; }

        /// <summary>
        /// <see cref="CsvParseException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="lineNumber">行番号。</param>
        /// <param name="errorLine">エラー行。</param>
        public CsvParseException(string message, long lineNumber, string errorLine)
            : base(message)
        {
            this.LineNumber = lineNumber;
            this.ErrorLine = errorLine;
        }

        /// <summary>
        /// <see cref="CsvParseException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="lineNumber">行番号。</param>
        /// <param name="errorLine">エラー行。</param>
        /// <param name="innerException">例外のもととなった例外。</param>
        public CsvParseException(string message, long lineNumber, string errorLine, Exception innerException)
            : base(message, innerException)
        {
            this.LineNumber = lineNumber;
            this.ErrorLine = errorLine;
        }
    }
}
