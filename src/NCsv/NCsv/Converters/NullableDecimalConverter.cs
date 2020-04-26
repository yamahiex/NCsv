namespace NCsv.Converters
{
    /// <summary>
    /// Nullを許容する<see cref="decimal"/>のコンバーターです。
    /// </summary>
    internal class NullableDecimalConverter : DecimalConverter
    {
        /// <inheritdoc/>
        protected override bool HasRequiredError(string value)
        {
            return false;
        }
    }
}
