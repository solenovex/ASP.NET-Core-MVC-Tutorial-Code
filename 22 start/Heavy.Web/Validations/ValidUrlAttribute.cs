using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Heavy.Web.Validations
{
    public class ValidUrlAttribute : Attribute, IModelValidator
    {
        public string ErrorMessage { get; set; }

        public IEnumerable<ModelValidationResult> Validate(
            ModelValidationContext context)
        {
            var url = context.Model as string;
            if (url != null && Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return Enumerable.Empty<ModelValidationResult>();
            }

            return new List<ModelValidationResult>
            {
                new ModelValidationResult(string.Empty, ErrorMessage)
            };
        }
    }
}
