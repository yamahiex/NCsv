namespace NCsv.Converters
{
    /// <summary>
    /// <see cref="bool"/>のコンバーターです。
    /// </summary>
    internal class BoolConverter : CsvConverter
    {
        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(ConvertToObjectItemContext context, out object? result, out string errorMessage)
        {
            result = null;
            errorMessage = string.Empty;

            if (bool.TryParse(context.CsvItem, out bool x))
            {
                result = x;
                return true;
            }

            if (string.IsNullOrEmpty(context.CsvItem) || context.CsvItem == "0")
            {
                result = false;
                return true;
            }

            if (context.CsvItem == "1")
            {
                result = true;
                return true;
            }

            errorMessage = CsvConfig.Current.ValidationMessage.GetBooleanConvertError(context);
            return false;
        }
    }
}
