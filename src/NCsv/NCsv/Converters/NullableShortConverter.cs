namespace NCsv.Converters
{
    /// <summary>
    /// Nullを許容する<see cref="short"/>のコンバーターです。
    /// </summary>
    internal class NullableShortConverter : ShortConverter
    {
        /// <inheritdoc/> 
        protected override bool HasRequiredError(string value)
        {
            return false;
        }
    }
}
