using System.Reflection;

namespace NCsv
{
    /// <summary>
    /// CSV変換のコンテキストです。
    /// </summary>
    public class CsvConvertContext
    {
        /// <summary>
        /// <see cref="PropertyInfo"/>を取得します。
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// エラーメッセージに含める名前。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// <see cref="CsvConvertContext"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="propertyInfo"><see cref="PropertyInfo"/>。</param>
        /// <param name="name">エラーメッセージに含める名前。</param>
        internal CsvConvertContext(PropertyInfo propertyInfo, string name)
        {
            this.Property = propertyInfo;
            this.Name = name;
        }
    }
}
