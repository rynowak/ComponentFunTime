using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ComponentFunTime
{
    public class Person
    {
        public Person()
        {
        }

        public Person(Person other)
        {
            Id = other.Id;
            Name = other.Name;
            Salary = other.Salary;
            Areas = new List<FeatureArea>(other.Areas.Select(a => new FeatureArea(a)));
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.01, 1000000.00, ErrorMessage = "You can't pay someone that amount.")]
        public decimal Salary { get; set; }

        public List<FeatureArea> Areas { get; } = new List<FeatureArea>();
    }
}
