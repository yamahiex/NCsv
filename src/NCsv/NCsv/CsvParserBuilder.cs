using NCsv.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NCsv
{
    /// <summary>
    /// <see cref="CsvParser{T}"/>を構築します。
    /// </summary>
    /// <typeparam name="T">解析する型です。</typeparam>
    internal class CsvParserBuilder<T> where T : new()
    {
        /// <summary>
        /// <see cref="CsvColumn"/>のリストです。
        /// </summary>
        private readonly List<CsvColumn> columns = new List<CsvColumn>();

        /// <summary>
        /// <see cref="CsvParserBuilder{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public CsvParserBuilder()
        {
        }

        /// <summary>
        /// <see cref="CsvParserBuilder{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="columns"><see cref="CsvColumn"/>のコレクション。</param>
        public CsvParserBuilder(IEnumerable<CsvColumn> columns)
        {
            this.columns.AddRange(columns);
        }

        /// <summary>
        /// <typeparamref name="T"/>をもとにして<see cref="CsvParserBuilder{T}"/>を作成します。
        /// </summary>
        /// <returns></returns>
        public static CsvParserBuilder<T> FromType()
        {
            var columns = CreateColumnsFromType().ToList();

            if (columns.Count == 0)
            {
                throw new InvalidOperationException("Set the CsvColumnAttribute to the property to be serialized.");
            }

            return new CsvParserBuilder<T>(columns);
        }

        /// <summary>
        /// <see cref="CsvColumn"/>を追加します。
        /// </summary>
        /// <param name="column"><see cref="CsvColumn"/></param>
        /// <returns><see cref="CsvParserBuilder{T}"/></returns>
        public CsvParserBuilder<T> AddColumn(CsvColumn column)
        {
            this.columns.Add(column);
            return this;
        }

        /// <summary>
        /// このインスタンスの値を<see cref="CsvParser{T}"/>に変換します。
        /// </summary>
        /// <returns><see cref="CsvParser{T}"/>。</returns>
        public CsvParser<T> ToCsvParser()
        {
            Validate();

            return new CsvParser<T>(CreateCompletedColumns());
        }

        /// <summary>
        /// 指定した型に設定されている最大インデックス分の<see cref="CsvColumn"/>を作成します。
        /// </summary>
        /// <returns><see cref="CsvColumn"/>のリスト。</returns>
        /// <remarks>
        /// <see cref="CsvColumnAttribute.Index"/>の値が連続していない場合を考慮して、
        /// <see cref="CsvColumn"/>存在しない場合は<see cref="NullColumn"/>を設定しています。
        /// </remarks>
        private List<CsvColumn> CreateCompletedColumns()
        {
            var result = new List<CsvColumn>();

            var dic = this.columns.ToDictionary(x => x.Index);
            var max = dic.Max(x => x.Key) + 1;

            for (var i = 0; i < max; i++)
            {
                if (dic.ContainsKey(i))
                {
                    result.Add(dic[i]);
                }
                else
                {
                    result.Add(CsvColumn.Null);
                }
            }

            return result;
        }

        /// <summary>
        /// 検証します。
        /// 検証に失敗した場合は例外をスローします。
        /// </summary>
        private void Validate()
        {
            if (this.columns.Count == 0)
            {
                throw new InvalidOperationException("Please add a column.");
            }

            if (HasIndexDuplicate())
            {
                throw new InvalidOperationException("Duplicate indexes.");
            }
        }

        /// <summary>
        /// インデックスが重複しているかどうかを返します。
        /// </summary>
        private bool HasIndexDuplicate()
        {
            var duplicates = this.columns.GroupBy(x => x.Index).Where(g => g.Count() > 1).Select(g => g.FirstOrDefault()).ToList();

            if (duplicates.Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// <typeparamref name="T"/>から<see cref="CsvColumn"/>のコレクションを作成します。
        /// </summary>
        /// <returns><see cref="CsvColumn"/>のコレクション。</returns>
        private static IEnumerable<CsvColumn> CreateColumnsFromType()
        {
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                var attribute = property.GetCustomAttribute<CsvColumnAttribute>();

                if (attribute == null)
                {
                    continue;
                }

                yield return new CsvColumn(attribute, new CsvProperty(type, property.Name), GetConverter(property));
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
