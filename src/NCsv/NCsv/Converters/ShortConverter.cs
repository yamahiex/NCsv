﻿using System.Globalization;
using System.Reflection;

namespace NCsv.Converters
{
    /// <summary>
    /// <see cref="short"/>のコンバーターです。
    /// </summary>
    internal class ShortConverter : CsvConverter
    {
        /// <inheritdoc/>
        public override string ConvertToCsvItem(CsvConvertContext context, object? objectItem)
        {
            var format = context.Property.GetCustomAttribute<CsvFormatAttribute>();

            if (format == null)
            {
                return objectItem?.ToString() ?? string.Empty;
            }

            if (objectItem == null)
            {
                return "\"\"";
            }

            return $"\"{((short)objectItem).ToString(format.Format)}\"";
        }

        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(CsvConvertContext context, string csvItem, out object? result, out string errorMessage)
        {
            result = null;
            errorMessage = string.Empty;

            if (HasRequiredError(csvItem))
            {
                errorMessage = NCsvConfig.Current.Message.GetRequiredError(context.Name);
                return false;
            }

            if (string.IsNullOrEmpty(csvItem))
            {
                return true;
            }

            if (short.TryParse(csvItem, NumberStyles.AllowThousands, NumberFormatInfo.CurrentInfo, out short x))
            {
                result = x;
                return true;
            }

            errorMessage = NCsvConfig.Current.Message.GetNumericConvertError(context.Name);
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