
using Microsoft.EntityFrameworkCore;

namespace GeoQuiz.Services;

public class LandmarkService :ILandmarkService
{
    private readonly GeoQuizContext _context;
    
    public LandmarkService( GeoQuizContext context)
    {
        _context = context;
    }

    public ICollection<Landmark> GetRandomLandmarks(int num)
    {
        // var landmarks = _context.Database.ExecuteSqlRaw(
        //     $"SELECT * FROM landmarks ORDER BY RANDOM() LIMIT {num}");
        var landmarks = _context.Landmarks.OrderBy(x => Guid.NewGuid()).Take(num).ToList();
        return landmarks;
    }

    public Landmark GetById(int id)
    {
        var landmark = _context.Landmarks.First(l=>l.Id == id);
        return landmark;
    }
}