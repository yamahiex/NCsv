using System.Text;

namespace NCsv
{
    /// <summary>
    /// <see cref="CsvColumn"/>のNullオブジェクトです。
    /// </summary>
    internal class NullColumn : CsvColumn
    {
        /// <inheritdoc/>
        public override bool IsNull => true;

        /// <inheritdoc/>
        protected override string Name => string.Empty;

        /// <summary>
        /// <see cref="NullColumn"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public NullColumn()
            : base(new CsvColumnAttribute(-1), null, null)
        {

        }

        /// <inheritdoc/>
        public override string ConvertToCsvItem(object objectItem)
        {
            return string.Empty;
        }

        /// <inheritdoc/>
        public override bool TryConvertToObjectItem(ICsvItemContext context, out object result, out string errorMessage)
        {
            result = null;
            errorMessage = string.Empty;
            return true;
        }

        /// <inheritdoc/>
        public override bool Validate(ICsvItemContext context, out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }

        /// <inheritdoc/>
        public override object GetValue(object obj)
        {
            return string.Empty;
        }

        /// <inheritdoc/>
        public override void SetValue(object obj, object value)
        {
        }

        /// <inheritdoc/>
        public override void AppendName(StringBuilder sb)
        {
            sb.Append(string.Empty);
        }
    }
}
