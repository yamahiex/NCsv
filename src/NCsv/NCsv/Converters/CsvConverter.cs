using System.Text.RegularExpressions;

namespace NCsv.Converters
{
    /// <summary>
    /// CSV項目を変換します。
    /// </summary>
    public abstract class CsvConverter
    {
        /// <summary>
        /// CSV項目の特殊な値を検索するための正規表現です。
        /// </summary>
        private static readonly Regex specialValueRegex = new Regex("[,\"\r\n]+");

        /// <summary>
        /// オブジェクト項目をCSV項目に変換します。
        /// </summary>
        /// <param name="context"><see cref="ConvertToCsvItemContext"/>。</param>
        /// <returns>変換結果。</returns>
        public virtual string ConvertToCsvItem(ConvertToCsvItemContext context)
        {
            return context.ObjectItem?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// CSV項目からオブジェクト項目への変換を試みます。
        /// </summary>
        /// <param name="context"><see cref="ConvertToObjectItemContext"/>。</param>
        /// <param name="result">変換結果。</param>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <returns>変換に成功した場合にtrue。</returns>
        public abstract bool TryConvertToObjectItem(ConvertToObjectItemContext context, out object result, out string errorMessage);

        /// <summary>
        /// CSV項目をエスケープします。
        /// </summary>
        /// <param name="csvItem">CSV項目。</param>
        /// <returns>エスケープした結果。</returns>
        public virtual string CsvItemEscape(string csvItem)
        {
            var result = csvItem.Replace("\"", "\"\"");

            if (specialValueRegex.IsMatch(csvItem))
            {
                result = $"\"{result}\"";
            }

            return result;
        }
    }
}
