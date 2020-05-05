namespace NCsv
{
    /// <summary>
    /// CSV項目に関する情報です。
    /// </summary>
    internal class CsvItemContext : ICsvItemContext
    {
        /// <inheritdoc/>
        public string Name { get; private set; }

        /// <inheritdoc/>
        public long LineNumber { get; private set; }

        /// <inheritdoc/>
        public string Value { get; private set; }

        /// <summary>
        /// <see cref="CsvItemContext"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="name">エラーメッセージに含める名前</param>
        /// <param name="lineNumber">行番号。</param>
        /// <param name="value">値。</param>
        public CsvItemContext(string name, long lineNumber, string value)
        {
            this.LineNumber = lineNumber;
            this.Value = value;
            this.Name = name;
        }
    }
}
