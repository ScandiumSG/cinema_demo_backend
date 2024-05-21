﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace cinemaServer.Models.PureModels
{
    [Table("playable_movies")]
    public class Movie
    {
        [Key]
        [Column("movie_id")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Column("title")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Column("description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Column("runtime")]
        [JsonPropertyName("runtime")]
        public int Runtime { get; set; }

        [Column("release_year")]
        [JsonPropertyName("year")]
        public int Year { get; set; }

        //public ICollection<IPerson> Actors { get; set; } = new List<IPerson>();

        //public IPerson? Director { get; set; }

        [Column("rating")]
        [JsonPropertyName("rating")]
        public ERatings Rating { get; set; } = 0;
    }
}
