using NCsv.Converters;

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
            : base(new CsvColumnAttribute(-1), CreateDummyProperty(), new NullConverter())
        {

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

        /// <summary>
        /// ダミーの<see cref="CsvProperty"/>を作成します。
        /// </summary>
        /// <returns><see cref="CsvProperty"/>。</returns>
        private static CsvProperty CreateDummyProperty()
        {
            var x = new { Dummy = string.Empty };
            return new CsvProperty(x.GetType(), nameof(x.Dummy));
        }
    }
}
