namespace NCsv.Converters
{
    /// <summary>
    /// Nullを許容する<see cref="long"/>のコンバーターです。
    /// </summary>
    internal class NullableLongConverter : LongConverter
    {
        /// <inheritdoc/> 
        protected override bool HasRequiredError(string value)
        {
            return false;
        }
    }
}
