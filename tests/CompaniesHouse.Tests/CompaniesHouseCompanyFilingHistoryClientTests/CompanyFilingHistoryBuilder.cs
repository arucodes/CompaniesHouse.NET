﻿using System.Linq;
using CompaniesHouse.Tests.ResourceBuilders;
using AutoFixture;

namespace CompaniesHouse.Tests.CompaniesHouseCompanyFilingHistoryClientTests
{
    public class CompanyFilingHistoryBuilder
    {
        public static CompanyFilingHistory Build(CompaniesHouseCompanyFilingHistoryClientTestCase testCase)
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new UniversalDateSpecimenBuilder<FilingHistoryItem>(x => x.DateOfProcessing));
            fixture.Customizations.Add(new UniversalDateSpecimenBuilder<FilingHistoryItemAnnotation>(x => x.DateOfAnnotation));
            fixture.Customizations.Add(new UniversalDateSpecimenBuilder<FilingHistoryItemAssociatedFiling>(x => x.Date));
            fixture.Customizations.Add(new UniversalDateSpecimenBuilder<FilingHistoryItemResolution>(x => x.DateOfProcessing));

            var annotations = fixture.Build<FilingHistoryItemAnnotation>()
               .CreateMany().ToArray();

            var associatedFilings = fixture.Build<FilingHistoryItemAssociatedFiling>()
               .CreateMany().ToArray();

            var resolutions = fixture.Build<FilingHistoryItemResolution>()
                .With(x => x.Category, testCase.ResolutionCategory)
                .With(x => x.Subcategory, testCase.Subcategory)
               .CreateMany().ToArray();

            var items = fixture.Build<FilingHistoryItem>()
                .With(x => x.Category, testCase.Category)
                .With(x => x.Subcategory, testCase.Subcategory)
                .With(x => x.Annotations, annotations)
                .With(x => x.AssociatedFilings, associatedFilings)
                .With(x => x.Resolutions, resolutions)
                .CreateMany().ToArray();

            var filingHistory = fixture.Build<CompanyFilingHistory>()
                .With(x => x.HistoryStatus, testCase.HistoryStatus)
                .With(x => x.Items, items)
                .Create();

            return filingHistory;
        }

        public static FilingHistoryItem BuildOne(CompaniesHouseCompanyFilingHistoryClientTestCase testCase)
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(new UniversalDateSpecimenBuilder<FilingHistoryItem>(x => x.DateOfProcessing));
            fixture.Customizations.Add(new UniversalDateSpecimenBuilder<FilingHistoryItemAnnotation>(x => x.DateOfAnnotation));
            fixture.Customizations.Add(new UniversalDateSpecimenBuilder<FilingHistoryItemAssociatedFiling>(x => x.Date));
            fixture.Customizations.Add(new UniversalDateSpecimenBuilder<FilingHistoryItemResolution>(x => x.DateOfProcessing));

            var annotations = fixture.Build<FilingHistoryItemAnnotation>()
                .CreateMany()
                .ToArray();

            var associatedFilings = fixture.Build<FilingHistoryItemAssociatedFiling>()
                .CreateMany()
                .ToArray();

            var resolutions = fixture.Build<FilingHistoryItemResolution>()
                .With(x => x.Category, testCase.ResolutionCategory)
                .With(x => x.Subcategory, testCase.Subcategory)
                .CreateMany()
                .ToArray();

           return fixture.Build<FilingHistoryItem>()
                .With(x => x.Category, testCase.Category)
                .With(x => x.Subcategory, testCase.Subcategory)
                .With(x => x.Annotations, annotations)
                .With(x => x.AssociatedFilings, associatedFilings)
                .With(x => x.Resolutions, resolutions)
                .Create();
        }
    }
}
