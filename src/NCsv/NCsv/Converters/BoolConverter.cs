namespace NCsv.Converters
{
    /// <summary>
    /// <see cref="bool"/>のコンバーターです。
    /// </summary>
    internal class BoolConverter : CsvConverter
    {
        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(CsvConvertContext context, string csvItem, out object? result, out string errorMessage)
        {
            result = null;
            errorMessage = string.Empty;

            if (bool.TryParse(csvItem, out bool x))
            {
                result = x;
                return true;
            }

            if (string.IsNullOrEmpty(csvItem) || csvItem == "0")
            {
                result = false;
                return true;
            }

            if (csvItem == "1")
            {
                result = true;
                return true;
            }

            errorMessage = CsvConfig.Current.Message.GetBooleanConvertError(context.Name);
            return false;
        }
    }
}
