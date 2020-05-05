using System;

namespace NCsv
{
    /// <summary>
    /// CSVの検証に失敗した場合にスローされる例外です。
    /// </summary>
    public class CsvValidationException : Exception
    {
        /// <summary>
        /// 検証項目の名前を取得します。
        /// </summary>
        public ICsvItemContext Context { get; private set; }

        /// <summary>
        /// <see cref="CsvValidationException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        public CsvValidationException(string message, ICsvItemContext context)
            : base(message)
        {
            this.Context = context;
        }

        /// <summary>
        /// <see cref="CsvValidationException"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="innerException">例外のもととなった例外。</param>
        public CsvValidationException(string message, ICsvItemContext context, Exception innerException)
            : base(message, innerException)
        {
            this.Context = context;
        }
    }
}
