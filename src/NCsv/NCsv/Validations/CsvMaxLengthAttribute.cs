﻿using System;
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
        /// バイト単位で検証するかどうかを取得または設定します。
        /// </summary>
        public bool LengthAsByte { get; set; } = false;

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

            if (this.LengthAsByte)
            {
                if (Encoding.GetEncoding("Shift_JIS").GetByteCount(context.Value) <= this.maxLength)
                {
                    return true;
                }
            }
            else
            {
                if (context.Value.Length <= this.maxLength)
                {
                    return true;
                }
            }

            errorMessage = CsvConfig.Current.Message.GetMaxLengthError(context, this.maxLength);

            return false;
        }
    }
}
