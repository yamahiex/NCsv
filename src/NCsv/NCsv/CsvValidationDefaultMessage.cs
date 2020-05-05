namespace NCsv
{
    /// <summary>
    /// 検証に失敗した場合の既定メッセージです。
    /// </summary>
    public class CsvValidationDefaultMessage : ICsvValidationMessage
    {
        /// <inheritdoc/> 
        public virtual string GetNumericConvertError(ICsvItemContext context)
        {
            return $"{context.Name} must be set to a numeric value.";
        }

        /// <inheritdoc/> 
        public virtual string GetDateTimeConvertError(ICsvItemContext context)
        {
            return $"Set {context.Name} to a value that can be converted to a date and time.";
        }

        /// <inheritdoc/> 
        public virtual string GetBooleanConvertError(ICsvItemContext context)
        {
            return $"Set {context.Name} to a value that can be converted to a truth value.";
        }

        /// <inheritdoc/> 
        public virtual string GetMaxLengthError(ICsvItemContext context, int length)
        {
            return $"{context.Name} must be set within {length} digits.";
        }

        /// <inheritdoc/> 
        public virtual string GetRequiredError(ICsvItemContext context)
        {
            return $"{context.Name} is required.";
        }

        /// <inheritdoc/> 
        public virtual string GetNumberOutOfRangeError(ICsvItemContext context, decimal minValue, decimal maxValue)
        {
            return $"Set {context.Name} to a number from {minValue} to {maxValue}.";
        }

        /// <inheritdoc/> 
        public virtual string GetNumberMinValueError(ICsvItemContext context, decimal minValue)
        {
            return $"{context.Name} must be set to a value greater than or equal to {minValue}.";
        }

        /// <inheritdoc/> 
        public virtual string GetNumberMaxValueError(ICsvItemContext context, decimal maxValue)
        {
            return $"{context.Name} must be less than or equal to {maxValue}.";
        }

        /// <inheritdoc/> 
        public virtual string GetPrecisionError(ICsvItemContext context, int precision)
        {
            return $"The {context.Name} must be set within {precision} digits.";
        }

        /// <inheritdoc/> 
        public virtual string GetPrecisionAndScaleError(ICsvItemContext context, int precision, int scale)
        {
            return $"The {context.Name} must be within {precision - scale} digits of the integer and {scale} digits of the decimal.";
        }

        /// <inheritdoc/> 
        public virtual string GetInvalidFormatError(ICsvItemContext context)
        {
            return $"The format of {context.Name} is invalid.";
        }

        /// <inheritdoc/> 
        public virtual string GetNumberOnlyError(ICsvItemContext context)
        {
            return $"{context.Name} must be set to a number only.";
        }

        /// <inheritdoc/> 
        public virtual string GetItemNotExistError(long lineNumber, string name)
        {
            return "The item does not exist in the CSV.";
        }
    }
}
