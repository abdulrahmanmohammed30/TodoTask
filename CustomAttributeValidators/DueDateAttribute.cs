using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace TodoTask.CustomAttributeValidators
{
    public class DueDateAttribute:ValidationAttribute
    {
        private readonly string _startDatePropertyName;
        public DueDateAttribute(string startDatePropertyName) {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
{
            if (value == null)
                return null;

            if (value is not DateTime dueDate)
                throw new InvalidOperationException($"Property '{_startDatePropertyName}' not found.");

            var startDateObj = validationContext.ObjectInstance?.GetType().GetProperty(_startDatePropertyName)?.GetValue(validationContext.ObjectInstance);
            if (startDateObj is null)
                return null;

            if (startDateObj is not DateTime startDate)
                throw new InvalidOperationException("The StartDate datatype is invalid.");
                
            if (dueDate < startDate)
                return new ValidationResult(ErrorMessage ?? "DueDate can be less than the StartDate", new string[] { validationContext.MemberName, _startDatePropertyName });
            else return  ValidationResult.Success;
        }
    }
}
