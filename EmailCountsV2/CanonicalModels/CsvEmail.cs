﻿namespace EmailCountsV2.CanonicalModels
{
    using System;

    public class CsvEmail
    {
        public DateTime SentDateTime { get; set; }

        public string SenderAddress { get; set; }

        public string RecipientAddress { get; set; }
    }
}
