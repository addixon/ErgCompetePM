using System.ComponentModel.DataAnnotations;

namespace PM.BO
{
    public enum PMVersion
    {
        [Display(Description ="Concept2 Performance Monitor 3 (PM3)")]
        PM3 = 3,

        [Display(Description = "Concept2 Performance Monitor 5 (PM5)")]
        PM5 = 5
    }
}
