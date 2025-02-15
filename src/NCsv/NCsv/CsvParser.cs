using NotVisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NCsv
{
    /// <summary>
    /// CSVを解析します。
    /// </summary>
    /// <typeparam name="T">解析する型です。</typeparam>
    internal class CsvParser<T> where T : new()
    {
        /// <summary>
        /// <see cref="CsvColumn"/>のリストです。
        /// </summary>
        private readonly IReadOnlyList<CsvColumn> columns;

        /// <summary>
        /// <see cref="CsvParser{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        private CsvParser()
        {
        }

        /// <summary>
        /// <see cref="CsvParser{T}"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="columns"><see cref="CsvColumn"/>のリスト。</param>
        public CsvParser(IReadOnlyList<CsvColumn> columns)
        {
            this.columns = columns;
        }

        /// <summary>
        /// <typeparamref name="T"/>をもとにして<see cref="CsvParser{T}"/>を作成します。
        /// </summary>
        /// <returns><see cref="CsvParser{T}"/>。</returns>
        public static CsvParser<T> FromType()
        {
            var builder = CsvParserBuilder<T>.FromType();
            return builder.ToCsvParser();
        }

        /// <summary>
        /// ヘッダ行を作成します。
        /// </summary>
        /// <param name="delimiter">区切り文字。</param>
        /// <returns>ヘッダ行。</returns>
        public string CreateHeader(string delimiter = ",")
        {
            var sb = new StringBuilder();
            var first = true;

            foreach (var c in this.columns)
            {
                if (!first)
                {
                    sb.Append(delimiter);
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
        /// <param name="delimiter">区切り文字。</param>
        /// <returns>CSV行。</returns>
        public string CreateCsvLine(T @object, string delimiter = ",")
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
                    sb.Append(delimiter);
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
            using (var parser = new CsvTextFieldParser(new StringReader(csvRow)))
            {
                return CreateObject(new CsvItems(parser.ReadFields(), 1));
            }
        }

        /// <summary>
        /// オブジェクトを作成します。
        /// </summary>
        /// <param name="items"><see cref="CsvItems"/>。</param>
        /// <returns>オブジェクト。</returns>
        public T CreateObject(CsvItems items)
        {
            var result = new T();

            foreach (var column in this.columns)
            {
                if (column.IsNull)
                {
                    continue;
                }

                var context = CreateCsvItemContext(column, items);

                Validate(column, context);

                column.SetValue(result, ConvertToObjectItem(column, context));
            }

            return result;
        }

        /// <summary>
        /// エラーが存在する場合はエラーを返します。
        /// </summary>
        /// <param name="items"><see cref="CsvItems"/>。</param>
        /// <returns><see cref="CsvErrorItem"/>のコレクション。</returns>
        public IEnumerable<CsvErrorItem> GetErrors(CsvItems items)
        {
            foreach (var column in this.columns)
            {
                if (column.IsNull)
                {
                    continue;
                }

                if (!column.TryCreateCsvItemContext(items, out ICsvItemContext context))
                {
                    yield return new CsvErrorItem(CsvMessages.GetItemNotExistError(context.LineNumber, context.Name), context);
                    continue;
                }

                if (!column.Validate(context, out string validateMessage))
                {
                    yield return new CsvErrorItem(validateMessage, context);
                    continue;
                }

                if (!column.TryConvertToObjectItem(context, out _, out string convertMessage))
                {
                    yield return new CsvErrorItem(convertMessage, context);
                    continue;
                }
            }
        }

        /// <summary>
        /// <see cref="CreateCsvItemContext"/>を作成します。作成できない場合は例外をスローします。
        /// </summary>
        /// <param name="column"><see cref="CsvColumn"/>。</param>
        /// <param name="items"><see cref="CsvItems"/>。</param>
        /// <returns></returns>
        private static ICsvItemContext CreateCsvItemContext(CsvColumn column, CsvItems items)
        {
            if (!column.TryCreateCsvItemContext(items, out ICsvItemContext context))
            {
                throw new CsvValidationException(CsvMessages.GetItemNotExistError(context.LineNumber, context.Name), context);
            }

            return context;
        }

        /// <summary>
        /// CSV項目を検証します。検証に失敗した場合は例外をスローします。
        /// </summary>
        /// <param name="column"><see cref="CsvColumn"/>。</param>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        private static void Validate(CsvColumn column, ICsvItemContext context)
        {
            if (!column.Validate(context, out string errorMessage))
            {
                throw new CsvValidationException(errorMessage, context);
            }
        }

        /// <summary>
        /// オブジェクト項目に変換します。変更に失敗した場合は例外をスローします。
        /// </summary>
        /// <param name="column"><see cref="CsvColumn"/>。</param>
        /// <param name="context"><see cref="ICsvItemContext"/>。</param>
        /// <returns>変換結果。</returns>
        private static object ConvertToObjectItem(CsvColumn column, ICsvItemContext context)
        {
            if (!column.TryConvertToObjectItem(context, out object value, out string errorMessage))
            {
                throw new CsvValidationException(errorMessage, context);
            }

            return value;
        }
    }
}
