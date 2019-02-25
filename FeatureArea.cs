using System.ComponentModel.DataAnnotations;

namespace ComponentFunTime
{
    public class FeatureArea
    {
        public FeatureArea()
        {
        }

        public FeatureArea(FeatureArea other)
        {
            Name = other.Name;
            Percent = other.Percent;
        }

        [Required]
        public string Name { get; set; }

        [Range(1.0, 100.0, ErrorMessage = "Invalid Focus %")]
        public decimal Percent { get; set; }
    }
}
