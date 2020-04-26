namespace NCsv.Converters
{
    /// <summary>
    /// コンバーターのNullオブジェクトです。
    /// </summary>
    internal class NullConverter : CsvConverter
    {
        /// <inheritdoc/>
        public override string ConvertToCsvItem(CsvConvertContext context, object? objectItem)
        {
            return string.Empty;
        }

        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(CsvConvertContext context, string csvItem, out object? result, out string message)
        {
            result = null;
            message = string.Empty;
            return true;
        }
    }
}
