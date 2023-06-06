using System.ComponentModel.DataAnnotations;

namespace GeoQuiz;

public class Lobby
{
    [Key]
    public string GUID { get; set; }
    public string? FirstUserGUID { get; set; }
    public string? SecondUserGUID { get; set; }
    public string? FirstUserName { get; set; }
    public string? SecondUserName { get; set; }
    public int? FirstUserCount { get; set; }
    public int? SecondUserCount { get; set; }
}