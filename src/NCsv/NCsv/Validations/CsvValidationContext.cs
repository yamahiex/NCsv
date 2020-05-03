namespace NCsv.Validations
{
    /// <summary>
    /// 検証に関する情報です。
    /// </summary>
    public class CsvValidationContext : ICsvItemContext
    {
        /// <summary>
        /// 行番号を取得します。
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// 検証する値を取得します。
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// エラーメッセージに含める名前を取得します。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// <see cref="CsvValidationContext"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="lineNumber">行番号。</param>
        /// <param name="value">検証する値。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        public CsvValidationContext(int lineNumber, string value, string name)
        {
            this.LineNumber = lineNumber;
            this.Value = value;
            this.Name = name;
        }
    }
}
