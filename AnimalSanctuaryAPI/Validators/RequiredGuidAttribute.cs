using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Validators
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class RequiredGuidAttribute : ValidationAttribute
    {
        public RequiredGuidAttribute() => ErrorMessage = "{0} is required.";

        public override bool IsValid(object? value)
        {
            return value is Guid && !Guid.Empty.Equals(value);
        }
    }
}