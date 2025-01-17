using System;
using System.Globalization;

namespace NCsv.Converters
{
    /// <summary>
    /// <see cref="DateTime"/>のコンバーターです。
    /// </summary>
    internal class DateTimeConverter : CsvConverter
    {
        /// <inheritdoc/>
        public override string ConvertToCsvItem(ConvertToCsvItemContext context)
        {
            if (context.ObjectItem == null)
            {
                return string.Empty;
            }

            var objectItem = (DateTime)context.ObjectItem;
            var format = context.Property.GetCustomAttribute<CsvFormatAttribute>();

            if (format != null)
            {
                return objectItem.ToString(format.Format, CultureInfo.InvariantCulture);
            }

            return objectItem.ToString(CultureInfo.InvariantCulture);
        }

        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(ConvertToObjectItemContext context, out object result, out string errorMessage)
        {
            result = null;
            errorMessage = string.Empty;

            if (HasRequiredError(context.CsvItem))
            {
                errorMessage = CsvMessages.GetRequiredError(context);
                return false;
            }

            if (string.IsNullOrEmpty(context.CsvItem))
            {
                return true;
            }

            var format = context.Property.GetCustomAttribute<CsvFormatAttribute>();

            if (format != null)
            {
                return TryParseByFormat(context, format, out result, out errorMessage);
            }

            return TryParse(context, out result, out errorMessage);
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

        /// <summary>
        /// 書式を指定して<see cref="DateTime"/>への変換を試みます。
        /// </summary>
        /// <param name="context"><see cref="ConvertToObjectItemContext"/>。</param>
        /// <param name="format"><see cref="CsvFormatAttribute"/>。</param>
        /// <param name="result">変換結果。</param>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <returns>変換に成功した場合にtrue。</returns>
        private static bool TryParseByFormat(ConvertToObjectItemContext context, CsvFormatAttribute format, out object result, out string errorMessage)
        {
            result = null;
            errorMessage = string.Empty;
            var dts = new DateTimeString(context.CsvItem);

            if (dts.TryParse(format.Format, out DateTime dt))
            {
                result = dt;
                return true;
            }

            errorMessage = CsvMessages.GetDateTimeFormatError(context, format.Format);
            return false;
        }

        /// <summary>
        /// <see cref="DateTime"/>への変換を試みます。
        /// </summary>
        /// <param name="context"><see cref="ConvertToObjectItemContext"/>。</param>
        /// <param name="result">変換結果。</param>
        /// <param name="errorMessage">エラーメッセージ。</param>
        /// <returns>変換に成功した場合にtrue。</returns>
        private static bool TryParse(ConvertToObjectItemContext context, out object result, out string errorMessage)
        {
            result = null;
            errorMessage = string.Empty;
            var dts = new DateTimeString(context.CsvItem);

            if (dts.TryParse(out DateTime dt))
            {
                result = dt;
                return true;
            }

            errorMessage = CsvMessages.GetDateTimeConvertError(context);
            return false;
        }
    }
}
