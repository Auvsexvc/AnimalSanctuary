﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebClientApp.Data;
using WebClientApp.Enums;
using WebClientApp.Interfaces;
using WebClientApp.Validators;
using WebClientApp.ViewModels.Base;

namespace WebClientApp.ViewModels
{
    public sealed class UpdateAnimalViewModel : ProfileImageBase, IBaseViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [DisplayName("Name")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Date of birth")]
        [DateMustNotBeFuture]
        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        [DisplayName("Health state")]
        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        [DisplayName("Registration date")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Specie")]
        public AnimalSpecie Specie { get; set; } = new AnimalSpecie();

        [DisplayName("Type")]
        public AnimalType Type { get; set; } = new AnimalType();

        [DisplayName("Facility")]
        public Facility Facility { get; set; } = new Facility();

        [DisplayName("Specie ID")]
        public Guid SpecieId { get; set; }

        [DisplayName("Type ID")]
        public Guid TypeId { get; set; }

        [DisplayName("Facility ID")]
        public Guid FacilityId { get; set; }
    }
}