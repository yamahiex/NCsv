using System;
using System.Text;

namespace NCsv.Validations
{
    /// <summary>
    /// 指定した最大桁数を超えていないことを検証します。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsvMaxLengthAttribute : CsvValidationAttribute
    {
        /// <summary>
        /// 最大桁数です。
        /// </summary>
        private readonly int maxLength = 0;

        /// <summary>
        /// <see cref="CsvMaxLengthAttribute"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="maxLength">この桁数を超えている場合はエラーにします。</param>
        public CsvMaxLengthAttribute(int maxLength)
        {
            this.maxLength = maxLength;
        }

        /// <inheritdoc/>
        public override bool Validate(CsvValidationContext context, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (context.Value.Length <= this.maxLength)
            {
                return true;
            }

            errorMessage = CsvConfig.Current.ValidationMessage.GetMaxLengthError(context, this.maxLength);

            return false;
        }
    }
}
