using System;
using System.Windows.Controls;

namespace Salon.Extensions
{
    public static class Extensions
    {
        public static bool Validate(this TextBox tb, bool isRequired)
        {
            return !(isRequired && tb.Text.Length <= 0);
        }

        public static bool Validate(this TextBox tb, bool isRequired, Func<string, bool> validationStrategy)
        {
            bool shouldValidate = !(validationStrategy == null);
            var isValid = true;

            if (isRequired && tb.Text.Length <= 0)
            {
                isValid = false;
                goto rtrn;
            }

            if (!isRequired && tb.Text.Length == 0)
            {
                goto rtrn;
            }

            isValid = !shouldValidate || validationStrategy(tb.Text);

            rtrn:
            return isValid;
        }
    }
}