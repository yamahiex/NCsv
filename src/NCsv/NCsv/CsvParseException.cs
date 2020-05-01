using System;

namespace NCsv
{
    /// <summary>
    /// CSV解析時にエラーが発生した場合にスローされます。
    /// </summary>
    internal class CsvParseException : Exception
    {
        /// <summary>
        /// <see cref="CsvParseException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public CsvParseException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// <see cref="CsvParseException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="innerException">例外のもととなった例外。</param>
        public CsvParseException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
