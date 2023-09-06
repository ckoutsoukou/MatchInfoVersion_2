﻿using MatchInfo.API.Utilities;
using MatchInfo.WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace MatchInfo.WebApi.Entities
{
    [Index(nameof(MatchDateTime), nameof(TeamA), nameof(TeamB), nameof(Sport), IsUnique = true)]
    public class Match: IEntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the match date time.
        /// </summary>
        [Required(ErrorMessage = "MatchDateTime is required")]
        public DateTime MatchDateTime { get; set; }

        /// <summary>
        /// Gets or sets the team A.
        /// </summary>
        [Required(ErrorMessage = "TeamA is required")]
        [StringLength(30, ErrorMessage = "TeamA exceeded string length (30)")]
        public required string TeamA { get; set; }

        /// <summary>
        /// Gets or sets the team B.
        /// </summary>
        [Required(ErrorMessage = "TeamB is required")]
        [StringLength(30, ErrorMessage = "TeamB exceeded string length (30)")]
        public required string TeamB { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sport.
        /// </summary>
        [Required(ErrorMessage = "Sport is required")]
        [StringRangeAttributeUtils(AllowableValues = new[] { "1", "2" }, ErrorMessage = "Sport must be either 'Football' or 'Basketball'.")]
        public byte Sport { get;set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, ErrorMessage = "Specifier exceeded string length (50)")]

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the collection of matchOdds.
        /// </summary>
        public virtual ICollection<MatchOdd> MatchOdds { get; set; } = new List<MatchOdd>();

     

    }
}
