using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace dojoleague.Models {
    public class Dojo : BaseEntity {
        public Dojo() {
            ninjas = new List<Ninja>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Info { get; set; }
        public ICollection<Ninja> ninjas { get; set; }
    }
}