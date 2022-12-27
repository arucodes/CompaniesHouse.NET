﻿using System;
using System.Collections.Generic;

namespace CompaniesHouse.Tests.ResourceBuilders
{
    public class FilingHistoryItemAnnotation
    {
        public string Annotation { get; set; }

        public DateTime DateOfAnnotation { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> DescriptionValues { get; set; }
    }
}
