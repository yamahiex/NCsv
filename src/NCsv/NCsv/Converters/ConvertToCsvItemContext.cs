namespace NCsv.Converters
{
    /// <summary>
    /// <see cref="CsvConverter.ConvertToCsvItem(ConvertToCsvItemContext)"/>に関する情報です。
    /// </summary>
    public class ConvertToCsvItemContext : CsvConvertContext
    {
        /// <summary>
        /// オブジェクト項目を取得します。
        /// </summary>
        public object ObjectItem { get; private set; }

        /// <summary>
        /// <see cref="ConvertToCsvItemContext"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="property"><see cref="CsvProperty"/>。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        /// <param name="objectItem">オブジェクト項目。</param>
        public ConvertToCsvItemContext(CsvProperty property, string name, object objectItem)
            : base(property, name)
        {
            this.ObjectItem = objectItem;
        }
    }
}
