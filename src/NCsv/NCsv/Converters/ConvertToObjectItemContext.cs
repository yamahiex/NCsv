using System.Reflection;

namespace NCsv.Converters
{
    /// <summary>
    /// <see cref="CsvConverter.TryConvertToObjectItem(ConvertToObjectItemContext, out object?, out string)"/>に関する情報です。
    /// </summary>
    public class ConvertToObjectItemContext : CsvConvertContext, ICsvItemContext
    {
        /// <summary>
        /// 行番号を取得します。
        /// </summary>
        public int LineNumber { get; private set; }

        /// <summary>
        /// CSV項目を取得します。
        /// </summary>
        public string CsvItem { get; private set; }

        string ICsvItemContext.Value => this.CsvItem;

        /// <summary>
        /// <see cref="ConvertToObjectItemContext"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/>。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        /// <param name="lineNumber">行番号</param>
        /// <param name="value">CSV項目の値。</param>
        public ConvertToObjectItemContext(PropertyInfo propertyInfo, string name, int lineNumber, string value)
            : base(propertyInfo, name)
        {
            this.LineNumber = lineNumber;
            this.CsvItem = value;
        }
    }
}
