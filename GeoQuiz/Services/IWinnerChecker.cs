namespace GeoQuiz.Services;

public interface IWinnerChecker
{
    public Winner CheckWinner(User firstUser, User secondUser);
}