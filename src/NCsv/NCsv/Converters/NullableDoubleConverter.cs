namespace NCsv.Converters
{
    /// <summary>
    /// Nullを許容する<see cref="double"/>のコンバーターです。
    /// </summary>
    internal class NullableDoubleConverter : DoubleConverter
    {
        /// <inheritdoc/>
        protected override bool HasRequiredError(string value)
        {
            return false;
        }
    }
}
