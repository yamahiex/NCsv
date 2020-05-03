namespace NCsv.Converters
{
    /// <summary>
    /// <see cref="string"/>のコンバーターです。
    /// </summary>
    internal class StringConverter : CsvConverter
    {
        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(ConvertToObjectItemContext context, out object? result, out string errorMessage)
        {
            result = context.CsvItem;
            errorMessage = string.Empty;
            return true;
        }
    }
}
