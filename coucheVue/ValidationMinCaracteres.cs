using System;
using System.Globalization;
using System.Windows.Controls;

namespace RefugeAnimauxWPF.coucheVue
{
    public class ValidationMinCaracteres : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? texte;
            try
            {
                texte = value?.ToString();
                if (string.IsNullOrEmpty(texte) || texte.Length < 2)
                {
                    return new ValidationResult(false, "Ce champ doit contenir au moins 2 caractères");
                }
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Ce champ doit être complété");
            }
            return new ValidationResult(true, null);
        }
    }
}
