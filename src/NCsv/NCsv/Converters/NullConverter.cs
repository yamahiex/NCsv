namespace NCsv.Converters
{
    /// <summary>
    /// コンバーターのNullオブジェクトです。
    /// </summary>
    internal class NullConverter : CsvConverter
    {
        /// <inheritdoc/>
        public override string ConvertToCsvItem(ConvertToCsvItemContext context)
        {
            return string.Empty;
        }

        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(ConvertToObjectItemContext context, out object? result, out string message)
        {
            result = null;
            message = string.Empty;
            return true;
        }
    }
}
