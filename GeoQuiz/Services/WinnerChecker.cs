namespace GeoQuiz.Services;

public class WinnerChecker : IWinnerChecker
{

    public Winner CheckWinner(User firstUser, User secondUser)
    {
        int? difference = firstUser.Count - secondUser.Count;
        if (difference < 0)
        {
            return new Winner
            {
                UserName = firstUser.UserName,
                Differnce = -difference
            };
        }
        else if (difference > 0)
        {
            return new Winner
            {
                UserName = secondUser.UserName,
                Differnce = difference
            };
        }
        else
        {
            return new Winner
            {
                UserName = "Ничья",
                Differnce = difference
            };
        }
    }
}