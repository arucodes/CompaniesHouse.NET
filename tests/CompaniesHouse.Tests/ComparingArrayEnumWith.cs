using System;
using System.Collections.Generic;
using System.Linq;
using CompaniesHouse.Tests.MapProviders;
using FluentAssertions.Equivalency;

namespace CompaniesHouse.Tests
{
    public class ComparingArrayEnumWith<TMapProvider, TEnum> : IEquivalencyStep
        where TMapProvider : IEnumDataMapProvider<TEnum>, new()
        where TEnum : struct
    {
        private readonly IReadOnlyDictionary<string, TEnum> _dictionary;
        private readonly Type _enumType;

        public ComparingArrayEnumWith()
        {
            _enumType = typeof(TEnum[]);
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum");
            }

            var provider = Activator.CreateInstance<TMapProvider>();

            _dictionary = provider.Map;
        }

        public bool CanHandle(IEquivalencyValidationContext context, IEquivalencyAssertionOptions config)
        {
            var subjectType = config.GetSubjectType(context);

            return subjectType != null && subjectType == _enumType && context.Expectation is string;
        }

        public bool Handle(IEquivalencyValidationContext context, IEquivalencyValidator parent, IEquivalencyAssertionOptions config)
        {
            var expected = _dictionary[(string)context.Expectation];

            return ((TEnum[])context.Subject).Contains(expected);
        }
    }
}