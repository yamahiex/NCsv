namespace NCsv.Converters
{
    /// <summary>
    /// <see cref="string"/>のコンバーターです。
    /// </summary>
    internal class StringConverter : CsvConverter
    {
        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(CsvConvertContext context, string csvItem, out object? result, out string errorMessage)
        {
            result = csvItem;
            errorMessage = string.Empty;
            return true;
        }
    }
}
