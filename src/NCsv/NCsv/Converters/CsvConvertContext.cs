namespace NCsv.Converters
{
    /// <summary>
    /// CSV変換に関する情報です。
    /// </summary>
    public class CsvConvertContext
    {
        /// <summary>
        /// <see cref="CsvProperty"/>を取得します。
        /// </summary>
        public CsvProperty Property { get; private set; }

        /// <summary>
        /// エラーメッセージに含める名前。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// <see cref="CsvConvertContext"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="property"><see cref="CsvProperty"/>。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        internal CsvConvertContext(CsvProperty property, string name)
        {
            this.Property = property;
            this.Name = name;
        }
    }
}
