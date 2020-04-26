using System;

namespace NCsv
{
    /// <summary>
    /// フォーマットを指定するための属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsvFormatAttribute : Attribute
    {
        /// <summary>
        /// フォーマット文字列を取得します。
        /// </summary>
        internal string Format { get; private set; }

        /// <summary>
        /// <see cref="CsvFormatAttribute"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="format">フォーマット文字列。</param>
        public CsvFormatAttribute(string format)
        {
            this.Format = format;
        }
    }
}
