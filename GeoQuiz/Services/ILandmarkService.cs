
namespace GeoQuiz.Services;

public interface ILandmarkService
{
    public ICollection<Landmark> GetRandomLandmarks(int num);
    public Landmark GetById(int id);
}