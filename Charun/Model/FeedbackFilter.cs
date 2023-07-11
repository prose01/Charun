﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Charun.Model
{
    public class FeedbackFilter
    {
        public string FeedbackId { get; set; }

        public DateTime? DateSentStart { get; set; }

        public DateTime? DateSentEnd { get; set; }

        public DateTime? DateSeenStart { get; set; }

        public DateTime? DateSeenEnd { get; set; }

        public string FromProfileId { get; set; }

        public string FromName { get; set; }

        public string AdminProfileId { get; set; }

        public string AdminName { get; set; }

        public FeedbackType? FeedbackType { get; set; }

        [StringLength(2000, ErrorMessage = "Message length cannot be more than 2000 characters long.")]
        public string Message { get; set; }

        public bool? Open { get; set; }

        public string Countrycode { get; set; }

        public string Languagecode { get; set; }
    }
}
