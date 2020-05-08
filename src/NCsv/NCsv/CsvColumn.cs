using NCsv.Converters;
using NCsv.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <see cref="CsvConverter"/>です。
        /// </summary>
        private readonly CsvConverter converter;

        /// <summary>
        /// Nullを表す<see cref="CsvColumn"/>です。
        /// </summary>
        public static readonly CsvColumn Null = new NullColumn();

        /// <summary>
        /// Nullかどうかを取得します。
        /// </summary>
        public virtual bool IsNull => false;

        /// <summary>
        /// <see cref="PropertyInfo"/>を取得します。
        /// </summary>
        protected CsvProperty Property { get; private set; }

        /// <summary>
        /// 名称を取得します。
        /// </summary>
        protected virtual string Name => this.attribute.Name ?? this.Property.Name;

        /// <summary>
        /// <see cref="CsvColumn"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="attribute"><see cref="CsvColumnAttribute"/>。</param>
        /// <param name="property"><see cref="PropertyInfo"/>。</param>
        /// <param name="converter"><see cref="CsvConverter"/>。</param>
        public CsvColumn(CsvColumnAttribute attribute, PropertyInfo property, CsvConverter converter)
        {
            this.attribute = attribute;
            this.Property = new CsvProperty(property);
            this.converter = converter;
        }

        /// <summary>
        /// 指定した型に設定されている最大インデックス分の<see cref="CsvColumn"/>を作成します。
        /// </summary>
        /// <typeparam name="T">対象の型。</typeparam>
        /// <returns><see cref="CsvColumn"/>のリスト。</returns>
        /// <remarks>
        /// <see cref="CsvColumnAttribute.Index"/>の値が連続していない場合を考慮して、
        /// <see cref="CsvColumn"/>存在しない場合は<see cref="NullColumn"/>を設定しています。
        /// </remarks>
        public static List<CsvColumn> CreateColumns<T>()
        {
            var result = new List<CsvColumn>();

            var columns = CsvColumn.Create<T>().ToDictionary(x => x.attribute.Index);
            var max = columns.Max(x => x.Key) + 1;

            for (var i = 0; i < max; i++)
            {
                if (columns.ContainsKey(i))
                {
                    result.Add(columns[i]);
                }
                else
                {
                    result.Add(CsvColumn.Null);
                }
            }

            return result;
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
        public bool Validate(ICsvItemContext context, out string errorMessage)
        {
            return this.Property.Validate(new CsvValidationContext(context), out errorMessage);
        }

        /// <summary>
        /// オブジェクト項目をCSV項目に変換します。
        /// </summary>
        /// <param name="objectItem">オブジェクト項目。</param>
        /// <returns>CSV項目。</returns>
        public string ConvertToCsvItem(object objectItem)
        {
            var context = new ConvertToCsvItemContext(this.Property, this.Name, objectItem);
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
        public bool TryConvertToObjectItem(ICsvItemContext context, out object result, out string errorMessage)
        {
            return this.converter.TryConvertToObjectItem(new ConvertToObjectItemContext(this.Property, context), out result, out errorMessage);
        }

        /// <summary>
        /// 値を設定します。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public virtual void SetValue(object obj, object value)
        {
            this.Property.SetValue(obj, value);
        }

        /// <summary>
        /// 値を取得します。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual object GetValue(object obj)
        {
            return this.Property.GetValue(obj);
        }

        /// <summary>
        /// 列名を追加します。
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/>。</param>
        public void AppendName(StringBuilder sb)
        {
            sb.Append(this.converter.CsvItemEscape(this.Name));
        }

        /// <summary>
        /// <see cref="CsvColumn"/>を作成します。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static List<CsvColumn> Create<T>()
        {
            var type = typeof(T);
            var result = new List<CsvColumn>();

            foreach (var property in type.GetProperties())
            {
                var attribute = property.GetCustomAttribute<CsvColumnAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                result.Add(new CsvColumn(attribute, property, GetConverter(property)));
            }

            ValidateDuplicate(result);

            return result.OrderBy(x => x.attribute.Index).ToList();
        }

        /// <summary>
        /// 重複がある場合は例外をスローします。
        /// </summary>
        /// <param name="columns"></param>
        private static void ValidateDuplicate(IEnumerable<CsvColumn> columns)
        {
            var duplicates = columns.GroupBy(x => x.attribute.Index).Where(g => g.Count() > 1).Select(g => g.FirstOrDefault()).ToList();

            if (duplicates.Count > 0)
            {
                throw new InvalidOperationException("Duplicate indexes.");
            }
        }

        /// <summary>
        /// <see cref="CsvConverter"/>を返します。<see cref="CsvConverter"/>が存在しない場合は例外をスローします。
        /// </summary>
        /// <param name="property"><see cref="PropertyInfo"/>。</param>
        /// <returns><see cref="CsvConverter"/>。</returns>
        private static CsvConverter GetConverter(PropertyInfo property)
        {
            var a = property.GetCustomAttribute<CsvConverterAttribute>();

            if (a != null)
            {
                return a.CreateConverter();
            }

            var converter = DefaultConverters.GetOrNull(property.PropertyType);

            if (converter != null)
            {
                return converter;
            }

            throw new InvalidOperationException("I couldn't find a converter for the property type.");
        }
    }
}
