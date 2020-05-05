using System;

namespace NCsv.Validations
{
    /// <summary>
    /// 必須検証属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsvRequiredAttribute : CsvValidationAttribute
    {
        /// <summary>
        /// 0を空とみなすかどうかを取得または設定します。
        /// </summary>
        public bool ZeroIsEmpty { get; set; } = false;

        /// <summary>
        /// <see cref="CsvRequiredAttribute"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public CsvRequiredAttribute()
        {
        }

        /// <inheritdoc/>
        public override bool Validate(CsvValidationContext context, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (IsEmpty(context.Value))
            {
                errorMessage = CsvConfig.Current.ValidationMessage.GetRequiredError(context);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 空かどうかを返します。
        /// </summary>
        /// <param name="value"></param>
        /// <returns>空の場合にtrue。</returns>
        private bool IsEmpty(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return true;
            }

            if (this.ZeroIsEmpty && IsZero(value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 0かどうかを返します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <returns>0の場合にtrue。</returns>
        private bool IsZero(string value)
        {
            return decimal.TryParse(value, out decimal result) && result == 0;
        }
    }
}
