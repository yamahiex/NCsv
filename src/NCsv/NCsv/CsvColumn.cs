using NCsv.Converters;
using NCsv.Validations;
using System.Reflection;
using System.Text;

namespace NCsv
{
    /// <summary>
    /// CSVカラムです。
    /// </summary>
    internal class CsvColumn
    {
        /// <summary>
        /// <see cref="CsvColumnAttribute"/>を取得します。
        /// </summary>
        private readonly CsvColumnAttribute attribute;

        /// <summary>
        /// <see cref="PropertyInfo"/>です。
        /// </summary>
        private readonly CsvProperty property;

        /// <summary>
        /// <see cref="CsvConverter"/>です。
        /// </summary>
        private readonly CsvConverter converter;

        /// <summary>
        /// Nullを表す<see cref="CsvColumn"/>です。
        /// </summary>
        public static readonly CsvColumn Null = new NullColumn();

        /// <summary>
        /// インデックスを取得します。
        /// </summary>
        public int Index => this.attribute.Index;

        /// <summary>
        /// Nullかどうかを取得します。
        /// </summary>
        public virtual bool IsNull => false;

        /// <summary>
        /// 名称を取得します。
        /// </summary>
        protected virtual string Name => this.attribute.Name ?? this.property.Name;

        /// <summary>
        /// <see cref="CsvColumn"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="attribute"><see cref="CsvColumnAttribute"/>。</param>
        /// <param name="property"><see cref="CsvProperty"/>。</param>
        /// <param name="converter"><see cref="CsvConverter"/>。</param>
        public CsvColumn(CsvColumnAttribute attribute, CsvProperty property, CsvConverter converter)
        {
            this.attribute = attribute;
            this.property = property;
            this.converter = converter;
        }

        /// <summary>
        /// <see cref="ICsvItemContext"/>の作成を試みます。
        /// </summary>
        /// <param name="items"><see cref="CsvItems"/>。</param>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>作成できた場合にtrue。</returns>
        public bool TryCreateCsvItemContext(CsvItems items, out ICsvItemContext context)
        {
            if (!items.TryGetItem(this.attribute.Index, out string csvItem))
            {
                context = new CsvItemContext(this.Name, items.LineNumber, string.Empty);
                return false;
            }

            context = new CsvItemContext(this.Name, items.LineNumber, csvItem);
            return true;
        }

        /// <summary>
        /// CSV項目を検証します。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <returns>検証に成功した場合にtrue。</returns>
        public virtual bool Validate(ICsvItemContext context, out string errorMessage)
        {
            return this.property.Validate(new CsvValidationContext(context), out errorMessage);
        }

        /// <summary>
        /// オブジェクト項目をCSV項目に変換します。
        /// </summary>
        /// <param name="objectItem">オブジェクト項目。</param>
        /// <returns>CSV項目。</returns>
        public virtual string ConvertToCsvItem(object objectItem)
        {
            var context = new ConvertToCsvItemContext(this.property, this.Name, objectItem);
            var csvItem = this.converter.ConvertToCsvItem(context);
            return this.converter.CsvItemEscape(csvItem);
        }

        /// <summary>
        /// CSV項目からオブジェクト項目への変換を試みます。
        /// </summary>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <param name="result">変換結果。</param>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <returns>変換に成功した場合にtrue。</returns>
        public virtual bool TryConvertToObjectItem(ICsvItemContext context, out object result, out string errorMessage)
        {
            return this.converter.TryConvertToObjectItem(new ConvertToObjectItemContext(this.property, context), out result, out errorMessage);
        }

        /// <summary>
        /// 値を設定します。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public virtual void SetValue(object obj, object value)
        {
            this.property.SetValue(obj, value);
        }

        /// <summary>
        /// 値を取得します。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual object GetValue(object obj)
        {
            return this.property.GetValue(obj);
        }

        /// <summary>
        /// 列名を追加します。
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/>。</param>
        public virtual void AppendName(StringBuilder sb)
        {
            sb.Append(this.converter.CsvItemEscape(this.Name));
        }
    }
}
