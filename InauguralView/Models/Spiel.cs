﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InauguralView.Models
{
    public class SpielEntry
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }

        [JsonProperty(PropertyName = "Spiel")]
        public Spiel Spiel { get; set; }
    }

    public class Spiel
    {
        [JsonProperty(PropertyName = "Speaker")]
        public String Speaker { get; set; }

        [JsonProperty(PropertyName = "Category")]
        public String Category { get; set; }

        [JsonProperty(PropertyName = "Date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "Paragraphs")]
        public IList<string> Paragraphs { get; set; }
    }

    public class SpielAnalytics
    {
        [JsonProperty(PropertyName = "SummaryAnalytics")]
        public ParagraphAnalytics SummaryAnalytics { get; set; }

        [JsonProperty(PropertyName = "ParagraphAnalytics")]
        public IList<ParagraphAnalytics> ParagraphAnalytics { get; set; }

    }

    public class ParagraphAnalytics
    {
        [JsonProperty(PropertyName = "Words")]
        public int Words { get; set; }

        [JsonProperty(PropertyName = "Characters")]
        public int Characters { get; set; }

        [JsonProperty(PropertyName = "Sentiment")]
        public double Sentiment { get; set; }

        [JsonProperty(PropertyName = "KeyPhrases")]
        public IList<string> KeyPhrases { get; set; }

    }
}
