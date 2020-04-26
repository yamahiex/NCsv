using System;

namespace NCsv.Converters
{
    /// <summary>
    /// Nullを許容する<see cref="DateTime"/>のコンバーターです。
    /// </summary>
    internal class NullableDateTimeConverter : DateTimeConverter
    {
        /// <inheritdoc/>
        protected override bool HasRequiredError(string value)
        {
            return false;
        }
    }
}
