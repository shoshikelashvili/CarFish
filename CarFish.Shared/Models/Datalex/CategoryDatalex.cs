using System.ComponentModel.DataAnnotations;

namespace CarFish.Shared.Models.Datalex
{
    public class CategoryDatalex
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ShowInHomePage { get; set; }
        public string ImageUrl { get; set; }
    }
}