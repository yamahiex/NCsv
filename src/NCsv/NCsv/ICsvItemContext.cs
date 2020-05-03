namespace NCsv
{
    /// <summary>
    /// CSV項目に関する情報です。
    /// </summary>
    public interface ICsvItemContext
    {
        /// <summary>
        /// 行番号を取得します。
        /// </summary>
        int LineNumber { get; }

        /// <summary>
        /// 値を取得します。
        /// </summary>
        string Value { get; }

        /// <summary>
        /// エラーメッセージに含める名前を取得します。
        /// </summary>
        string Name { get; }
    }
}
