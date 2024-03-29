﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebClientApp.Enums;
using WebClientApp.Interfaces;
using WebClientApp.ViewModels.Base;

namespace WebClientApp.ViewModels
{
    public sealed class AnimalViewModel : ProfileImagePathBase, IBaseWithPictureViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Date of birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Sex")]
        public Sex? Sex { get; set; }

        [DisplayName("Health state")]
        public string? HealthState { get; set; }

        [DisplayName("Attitude")]
        public Attitude? Attitude { get; set; }

        [DisplayName("Registration date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Specie")]
        public string Specie { get; set; } = string.Empty;

        [DisplayName("Type")]
        public string Type { get; set; } = string.Empty;

        [DisplayName("Facility")]
        public string Facility { get; set; } = string.Empty;
    }
}