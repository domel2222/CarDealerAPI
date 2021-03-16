using CarDealerAPI.Extensions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealerAPI.Models.Validators
{
    public class DealerQuerySearchValidator : AbstractValidator<DealerQuerySearch>
    {
        private int[] possibleChose = new[] { 5, 10, 15, 20 };
        public DealerQuerySearchValidator()
        {
            RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize).Custom((value, context)
                 =>
             {
                 if (!possibleChose.Contains(value))
                 {

                     context.AddFailure("Pagesize :", $"Page size must be between {string.Join(",", possibleChose)}");
                 }
             });
        }
    }
}
