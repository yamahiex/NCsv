namespace NCsv.Converters
{
    /// <summary>
    /// Nullを許容する<see cref="int"/>のコンバーターです。
    /// </summary>
    internal class NullableIntConverter : IntConverter
    {
        /// <inheritdoc/> 
        protected override bool HasRequiredError(string value)
        {
            return false;
        }
    }
}
