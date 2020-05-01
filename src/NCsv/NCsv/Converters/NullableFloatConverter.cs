namespace NCsv.Converters
{
    /// <summary>
    /// Nullを許容する<see cref="float"/>のコンバーターです。
    /// </summary>
    internal class NullableFloatConverter : FloatConverter
    {
        /// <inheritdoc/>
        protected override bool HasRequiredError(string value)
        {
            return false;
        }
    }
}
