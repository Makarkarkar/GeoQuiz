using System.Text.Json;
using GeoQuiz.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GeoQuiz.SignalR;

public class LobbyHub : Hub
{
    private readonly GeoQuizContext _context;
    private readonly ILandmarkService _landmarkService;
    private readonly IWinnerChecker _winnerChecker;
    
    public LobbyHub( GeoQuizContext context, ILandmarkService landmarkService, IWinnerChecker winnerChecker)
    {
        _landmarkService = landmarkService;
        _winnerChecker = winnerChecker;
        _context = context;
    }
    public async Task EndGame(JsonElement jsonUser)
    {
        User user = JsonSerializer.Deserialize<User>(jsonUser);
        Console.WriteLine($"USER {user.UserName} {user.GUID} {user.Count} {user.LobbyGUID}");
        Lobby lobby;
        lobby = _context.Lobbies.FirstOrDefault(l => l.GUID == user.LobbyGUID);
        if (lobby.FirstUserGUID == user.GUID)
        {
            lobby.FirstUserCount = user.Count;
            await _context.SaveChangesAsync();
            
            if (lobby.SecondUserCount != null)
            {
                var secondUser = new User
                {
                    UserName = lobby.SecondUserName,
                    Count = lobby.SecondUserCount
                };
               Winner winner = _winnerChecker.CheckWinner(user, secondUser);
               await Clients.Group(lobby.GUID).SendAsync("Winner", winner);
            }
        }
        else if (lobby.SecondUserGUID == user.GUID)
        {
            lobby.SecondUserCount = user.Count;
            await _context.SaveChangesAsync();
            if (lobby.FirstUserCount != null)
            {
                var firstUser = new User()
                {
                    UserName = lobby.FirstUserName,
                    Count = lobby.FirstUserCount
                };
                Winner winner = _winnerChecker.CheckWinner(firstUser, user);
                 await Clients.Group(lobby.GUID).SendAsync("Winner", winner);
            }
        }
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var user = new User
        {
            UserName = httpContext.Request.Query["userName"],
            GUID = Guid.NewGuid().ToString()
        };
        
        Lobby lobby;
        lobby =  _context.Lobbies.FirstOrDefault(x => !string.IsNullOrEmpty(x.FirstUserGUID) && string.IsNullOrEmpty(x.SecondUserGUID));
        
        if (lobby == null)
        {
            var newLobby = new Lobby
            {
                GUID = Guid.NewGuid().ToString(),
                FirstUserGUID = user.GUID,
                FirstUserName = user.UserName
            };
            _context.Lobbies.Add(newLobby);
            await _context.SaveChangesAsync();
            user.LobbyGUID = newLobby.GUID;
            await Groups.AddToGroupAsync(Context.ConnectionId, newLobby.GUID);
            await Clients.Caller.SendAsync("UserGuid", user);

        }
        else { 
            var newLobby = lobby;
            lobby.SecondUserGUID = user.GUID;
            lobby.SecondUserName = user.UserName;
            await _context.SaveChangesAsync();
            user.LobbyGUID = newLobby.GUID;
            await Groups.AddToGroupAsync(Context.ConnectionId, newLobby.GUID);
            var landmarks = _landmarkService.GetRandomLandmarks(10);
            await Clients.Caller.SendAsync("UserGuid", user);
            await Clients.Group(newLobby.GUID).SendAsync("GameStarted", landmarks);
        }
    }
    
}