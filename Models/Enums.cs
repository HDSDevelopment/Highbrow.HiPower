using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.Models
{
    public enum TitleType { Mr = 1, Ms = 2, Mrs = 3 }

    public enum GenderType { Both = 1, Male = 2, Female = 3 }

    public enum MaritalStatusType { Both = 1, Single = 1, Married = 1 }

    public enum BloodGroupType
    {
        A_Positive = 1, A_Negative = 2, B_Positive = 3, B_Negative = 4,
        O_Positive = 5, O_Negative = 6, AB_Positive = 7, AB_Negative = 8
    }

    public enum ReligionType
    {
        Hindu = 1, Christian = 2, Muslim = 3, Other = 4
    }

    public enum NationalityType
    {
        Indian = 1, Others = 2
    }

    public enum RelationType
    {
        Father = 1, Mother = 2, Spouse = 3, Son = 4, Daughter = 5
    }
    

}