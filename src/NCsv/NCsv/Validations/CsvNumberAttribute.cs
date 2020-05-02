using System;

namespace NCsv.Validations
{
    /// <summary>
    /// 数値検証属性です。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CsvNumberAttribute : CsvValidationAttribute
    {
        /// <summary>
        /// 精度です。
        /// </summary>
        private readonly int precision = 0;

        /// <summary>
        /// 小数部の桁数です。
        /// </summary>
        private readonly int scale = 0;

        /// <summary>
        /// 最小値を取得または設定します。
        /// </summary>
        public string MinValue { get; set; } = string.Empty;

        /// <summary>
        /// 最大値を取得または設定します。
        /// </summary>
        public string MaxValue { get; set; } = string.Empty;

        /// <summary>
        /// 最大値をdecimalで取得します。
        /// </summary>
        private decimal? MinValueDecimal => string.IsNullOrEmpty(this.MinValue) ? (decimal?)null : decimal.Parse(this.MinValue);

        /// <summary>
        /// 最大値をdecimalで取得します。
        /// </summary>
        private decimal? MaxValueDecimal => string.IsNullOrEmpty(this.MaxValue) ? (decimal?)null : decimal.Parse(this.MaxValue);

        /// <summary>
        /// <see cref="CsvNumberAttribute"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="precision">精度。</param>
        /// <param name="scale">小数部の桁数。</param>
        public CsvNumberAttribute(int precision, int scale)
        {
            this.precision = precision;
            this.scale = scale;
        }

        /// <inheritdoc/>
        public override bool Validate(string value, string name, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (!decimal.TryParse(value, out decimal x))
            {
                errorMessage = CsvConfig.Current.Message.GetNumericConvertError(name);
                return false;
            }

            if (HasPrecisionError(x, this.precision, this.scale))
            {
                errorMessage = GetPrecisionErrorMessage(name);
                return false;
            }

            if (HasOutOfRangeError(x))
            {
                errorMessage = CsvConfig.Current.Message.GetNumberOutOfRangeError(name, this.MinValueDecimal ?? 0, this.MaxValueDecimal ?? 0);
                return false;
            }

            if (HasMinValueError(x))
            {
                errorMessage = CsvConfig.Current.Message.GetNumberMinValueError(name, this.MinValueDecimal ?? 0);
                return false;
            }

            if (HasMaxValueError(x))
            {
                errorMessage = CsvConfig.Current.Message.GetNumberMaxValueError(name, this.MaxValueDecimal ?? 0);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 最大値と最小値の範囲外エラーがあるかどうかを返します。
        /// </summary>
        /// <param name="value">チェック対象の値。</param>
        /// <returns>エラーがある場合にtrue。</returns>
        private bool HasOutOfRangeError(decimal value)
        {
            if (this.MaxValueDecimal == null || this.MinValueDecimal == null)
            {
                return false;
            }

            return this.MaxValueDecimal < value || value < this.MinValueDecimal;
        }

        /// <summary>
        /// 最大値エラーがあるかどうかを返します。
        /// </summary>
        /// <param name="value">チェック対象の値。</param>
        /// <returns>エラーがある場合にtrue。</returns>
        private bool HasMaxValueError(decimal value)
        {
            if (this.MaxValueDecimal == null)
            {
                return false;
            }

            return this.MaxValueDecimal < value;
        }

        /// <summary>
        /// 最小値エラーがあるかどうかを返します。
        /// </summary>
        /// <param name="value">チェック対象の値。</param>
        /// <returns>エラーがある場合にtrue。</returns>
        private bool HasMinValueError(decimal value)
        {
            if (this.MinValueDecimal == null)
            {
                return false;
            }

            return value < this.MinValueDecimal;
        }

        /// <summary>
        /// 精度エラーの場合のメッセージを返します。
        /// </summary>
        /// <param name="columnName">列名。</param>
        /// <returns>精度エラーの場合のメッセージ。</returns>
        private string GetPrecisionErrorMessage(string columnName)
        {
            if (this.scale == 0)
            {
                return CsvConfig.Current.Message.GetPrecisionError(columnName, this.precision);
            }
            else
            {
                return CsvConfig.Current.Message.GetPrecisionAndScaleError(columnName, this.precision, this.scale);
            }
        }

        /// <summary>
        /// 精度エラーがあるかどうかを返します。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="precision">精度。</param>
        /// <param name="scale">小数部の桁数。</param>
        /// <returns>エラーがある場合にtrue。</returns>
        private bool HasPrecisionError(decimal x, int precision, int scale = 0)
        {
            if (x == 0)
            {
                return false;
            }

            decimal d = Math.Abs(x * (decimal)Math.Pow(10, scale));
            if ((d - decimal.Truncate(d)) != 0)
            {
                return true;
            }

            if ((int)Math.Log10((double)d) >= precision)
            {
                return true;
            }

            return false;
        }
    }
}
