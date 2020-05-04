using NotVisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NCsv
{
    /// <summary>
    /// <see cref="CsvColumn"/>のファーストクラスコレクションです。
    /// </summary>
    /// <typeparam name="T">変換するするクラスの型。</typeparam>
    internal class CsvColumns<T>
    {
        /// <summary>
        /// 変換対象のカラムです。
        /// </summary>
        private readonly IReadOnlyList<CsvColumn> columns;

        /// <summary>
        /// <see cref="CsvColumns{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public CsvColumns()
        {
            this.columns = CsvColumn.CreateColumns<T>();
        }

        /// <summary>
        /// ヘッダ行を作成します。
        /// </summary>
        /// <returns>ヘッダ行。</returns>
        public string CreateHeader()
        {
            var sb = new StringBuilder();
            var first = true;

            foreach (var c in this.columns)
            {
                if (!first)
                {
                    sb.Append(",");
                }

                first = false;

                c.AppendName(sb);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Csv行を作成します。
        /// </summary>
        /// <param name="object"></param>
        /// <returns>CSV行。</returns>
        public string CreateCsvLine(T @object)
        {
            if (@object == null)
            {
                throw new ArgumentNullException("The object is null.");
            }

            var sb = new StringBuilder();
            var first = true;

            foreach (var c in this.columns)
            {
                if (!first)
                {
                    sb.Append(",");
                }

                first = false;

                var value = c.GetValue(@object);
                sb.Append(c.ConvertToCsvItem(value));
            }

            return sb.ToString();
        }

        /// <summary>
        /// オブジェクトを作成します。
        /// </summary>
        /// <param name="csvRow">CSV行。</param>
        /// <returns>オブジェクト。</returns>
        internal T CreateObject(string csvRow)
        {
            using var parser = new CsvTextFieldParser(new StringReader(csvRow));
            return CreateObject(new CsvItems(parser.ReadFields(), -1));
        }

        /// <summary>
        /// オブジェクトを作成します。
        /// </summary>
        /// <param name="items"><see cref="CsvItems"/>。</param>
        /// <returns>オブジェクト。</returns>
        public T CreateObject(CsvItems items)
        {
            var result = Activator.CreateInstance<T>();

            if (result == null)
            {
                throw new InvalidOperationException("Unable to instantiate.");
            }

            foreach (var column in this.columns)
            {
                if (column.IsNull)
                {
                    continue;
                }

                Validate(column, items);

                column.SetValue(result, ConvertToObjectItem(column, items));
            }

            return result;
        }

        /// <summary>
        /// CSV項目を検証します。検証に失敗した場合は例外をスローします。
        /// </summary>
        /// <param name="column"><see cref="CsvColumn"/>。</param>
        /// <param name="items"><see cref="CsvItems"/>。</param>
        private static void Validate(CsvColumn column, CsvItems items)
        {
            if (!column.Validate(items, out string errorMessage))
            {
                throw new CsvParseException(errorMessage);
            }
        }

        /// <summary>
        /// オブジェクト項目に変換します。変更に失敗した場合は例外をスローします。
        /// </summary>
        /// <param name="column"><see cref="CsvColumn"/>。</param>
        /// <param name="items"><see cref="CsvItems"/>。</param>
        /// <returns>変換結果。</returns>
        private static object? ConvertToObjectItem(CsvColumn column, CsvItems items)
        {
            if (!column.TryConvertToObjectItem(items, out object? value, out string errorMessage))
            {
                throw new CsvParseException(errorMessage);
            }

            return value;
        }
    }
}
