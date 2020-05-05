using System;

namespace NCsv.Validations
{
    /// <summary>
    /// 数字のみであることを検証します。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsvNumberOnlyAttribute : CsvRegularExpressionAttribute
    {
        /// <summary>
        /// <see cref="CsvNumberOnlyAttribute"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public CsvNumberOnlyAttribute()
        {
            this.Pattern = "^[0-9]+$";
        }

        /// <inheritdoc/>
        protected override string GetErrorMessage(ICsvItemContext context)
        {
            return CsvConfig.Current.ValidationMessage.GetNumberOnlyError(context);
        }
    }
}
