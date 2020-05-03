﻿using System;
using System.Reflection;

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
            var format = context.Property.GetCustomAttribute<CsvFormatAttribute>();

            if (format == null)
            {
                return $"\"{context.ObjectItem}\"";
            }

            if (context.ObjectItem == null)
            {
                return "\"\"";
            }

            return $"\"{((DateTime)context.ObjectItem).ToString(format.Format)}\"";
        }

        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(ConvertToObjectItemContext context, out object? result, out string errorMessage)
        {
            result = null;
            errorMessage = string.Empty;

            if (HasRequiredError(context.CsvItem))
            {
                errorMessage = CsvConfig.Current.Message.GetRequiredError(context);
                return false;
            }

            if (string.IsNullOrEmpty(context.CsvItem))
            {
                return true;
            }

            var dts = new DateTimeString(context.CsvItem);
            if (dts.TryParse(out DateTime x))
            {
                result = x;
                return true;
            }

            errorMessage = CsvConfig.Current.Message.GetDateTimeConvertError(context);
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
