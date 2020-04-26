namespace NCsv
{
    /// <summary>
    /// CSV項目を変換します。
    /// </summary>
    public abstract class CsvConverter
    {
        /// <summary>
        /// オブジェクト項目をCSV項目に変換します。
        /// </summary>
        /// <param name="context"><see cref="CsvConvertContext"/>。</param>
        /// <param name="objectItem">オブジェクト項目。</param>
        /// <returns>変換結果。</returns>
        public virtual string ConvertToCsvItem(CsvConvertContext context, object? objectItem)
        {
            return $"\"{objectItem}\"";
        }

        /// <summary>
        /// CSV項目からオブジェクト項目への変換を試みます。
        /// </summary>
        /// <param name="context"><see cref="CsvConvertContext"/>。</param>
        /// <param name="csvItem">CSV項目。</param>
        /// <param name="result">変換結果。</param>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <returns>変換に成功した場合にtrue。</returns>
        public abstract bool TryConvertToObjectItem(CsvConvertContext context, string csvItem, out object? result, out string errorMessage);
    }
}
