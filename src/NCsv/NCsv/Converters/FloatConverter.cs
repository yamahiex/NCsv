namespace NCsv.Converters
{
    /// <summary>
    /// <see cref="float"/>のコンバーターです。
    /// </summary>
    internal class FloatConverter : CsvConverter
    {
        /// <inheritdoc/>
        public override string ConvertToCsvItem(ConvertToCsvItemContext context)
        {
            if (context.ObjectItem == null)
            {
                return string.Empty;
            }

            var format = context.Property.GetCustomAttribute<CsvFormatAttribute>();

            if (format != null)
            {
                return ((float)context.ObjectItem).ToString(format.Format);
            }

            return context.ObjectItem.ToString();
        }

        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(ConvertToObjectItemContext context, out object result, out string errorMessage)
        {
            result = null;
            errorMessage = string.Empty;

            if (HasRequiredError(context.CsvItem))
            {
                errorMessage = CsvConfig.Current.ValidationMessage.GetRequiredError(context);
                return false;
            }

            if (string.IsNullOrEmpty(context.CsvItem))
            {
                return true;
            }

            if (float.TryParse(context.CsvItem, out float x))
            {
                result = x;
                return true;
            }

            errorMessage = CsvConfig.Current.ValidationMessage.GetNumericConvertError(context);
            return false;
        }

        /// <summary>
        /// 必須エラーがあるかどうかを返します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <returns>エラーがある場合にtrue。</returns>
        protected virtual bool HasRequiredError(string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
