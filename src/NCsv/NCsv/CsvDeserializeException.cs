using System;

namespace NCsv
{
    /// <summary>
    /// 逆シリアル化時にエラーが発生した場合にスローされます。
    /// </summary>
    public class CsvDeserializeException : Exception
    {
        /// <summary>
        /// <see cref="CsvDeserializeException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        public CsvDeserializeException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// <see cref="CsvDeserializeException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="innerException">例外のもととなった例外。</param>
        public CsvDeserializeException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}
