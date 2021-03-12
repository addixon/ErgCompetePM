using System.ComponentModel.DataAnnotations;

namespace BO
{
    public enum PMCommandType
    {
        [Display(Description ="Short Command")]
        Short = 0,

        [Display(Description = "Long Command")]
        Long = 1
    }
}
