using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NCsv
{
    /// <summary>
    /// <see cref="CsvSerializer{T}"/>を構築します。
    /// </summary>
    /// <typeparam name="T">シリアル化する型です。</typeparam>
    public class CsvSerializerBuilder<T> where T : new()
    {
        /// <summary>
        /// <see cref="CsvParserBuilder{T}"/>です。
        /// </summary>
        private readonly List<CsvColumnBuilder<T>> builders = new List<CsvColumnBuilder<T>>();

        /// <summary>
        /// CSV列を追加します。
        /// </summary>
        /// <typeparam name="TProperty">プロパティの型。</typeparam>
        /// <param name="index">インデックス。</param>
        /// <param name="property">プロパティ。</param>
        /// <returns><see cref="CsvColumnBuilder{T}"/>。</returns>
        public CsvColumnBuilder<T> AddColumn<TProperty>(int index, Expression<Func<T, TProperty>> property)
        {
            var builder = new CsvColumnBuilder<T>(index, ((MemberExpression)property.Body).Member.Name);
            this.builders.Add(builder);
            return builder;
        }

        /// <summary>
        /// このインスタンスの値を<see cref="CsvSerializer{T}"/>に変換します。
        /// </summary>
        /// <returns><see cref="CsvSerializer{T}"/>。</returns>
        public CsvSerializer<T> ToCsvSerializer()
        {
            return new CsvSerializer<T>(CreateCsvParser());
        }

        /// <summary>
        /// <see cref="CsvParser{T}"/>を作成します。
        /// </summary>
        /// <returns><see cref="CsvParser{T}"/>。</returns>
        private CsvParser<T> CreateCsvParser()
        {
            var cb = new CsvParserBuilder<T>();

            foreach (var builder in this.builders)
            {
                cb.AddColumn(builder.ToCsvColumn());
            }

            return cb.ToCsvParser();
        }
    }
}
