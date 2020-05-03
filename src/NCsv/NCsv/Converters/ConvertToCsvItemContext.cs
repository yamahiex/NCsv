using System.Reflection;

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
        public object? ObjectItem { get; private set; }

        /// <summary>
        /// <see cref="ConvertToCsvItemContext"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/>。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        /// <param name="value">オブジェクト項目。</param>
        public ConvertToCsvItemContext(PropertyInfo propertyInfo, string name, object? value)
            : base(propertyInfo, name)
        {
            this.ObjectItem = value;
        }
    }
}
