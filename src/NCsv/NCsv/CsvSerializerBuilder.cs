using System.Collections.Generic;

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
        /// <param name="index">インデックス。</param>
        /// <param name="propertyName">プロパティ名。</param>
        /// <returns><see cref="CsvColumnBuilder{T}"/>。</returns>
        public CsvColumnBuilder<T> AddColumn(int index, string propertyName)
        {
            var builder = new CsvColumnBuilder<T>(index, propertyName);
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
