using System.ComponentModel;

public enum ERatings
{
    [Description("General Audiences")]
    G = 0,

    [Description("Parental Guidance Suggested")]
    PG = 1,

    [Description("Parents Strongly Cautioned")]
    PG13 = 2,

    [Description("Restricted")]
    R = 3,

    [Description("No One 17 and Under Admitted")]
    NC17 = 4
}
