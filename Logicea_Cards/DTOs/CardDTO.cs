﻿using Logicea_Cards.Validation;
using System.ComponentModel.DataAnnotations;

namespace Logicea_Cards.DTOs
{
    public class CardDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [AlphanumericColorCode]
        public string Color { get; set; } // Ensure format validation
        [OptionsList("To Do", "In Progress", "Done")]
        public string Status { get; set; } = "To Do"; // Default status
    }
}
