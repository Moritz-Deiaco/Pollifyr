using JWT.Algorithms;
using JWT.Builder;
using MoonCore.Abstractions;
using MoonCore.Helpers;
using MoonCoreUI.Services;
using Pollifyr.App.Database.Models;
using Pollifyr.App.Exceptions;
using Pollifyr.App.Helpers.Utils;
using Pollifyr.App.Services.Partials;

namespace Pollifyr.App.Services.Auth;

public class AuthService
{
    private readonly Repository<User> Users;
    private readonly CookieService CookieService;
    private readonly DateTimeService DateTimeService;

    private readonly string Secret;

    public AuthService(Repository<User> users, CookieService cookieService, DateTimeService dateTimeService, ConfigService configService)
    {
        Users = users;
        CookieService = cookieService;
        DateTimeService = dateTimeService;
        Secret = configService.Get().Security.Secret;
    }


    public async Task<string> Register(string email, string password, string username)
    {

        var user = await AddUser(email, username, password);
        

        if (user == null)
        {
            throw new DisplayException("Error while creating the user, maybe the email or username is taken.");
        }
        
        return await GenerateToken(user);
    }

    public async Task<User?> AddUser(string email, string username, string password)
    {
        var emailTaken = Users.Get().FirstOrDefault(x => x.Email == email) != null;

        var usernameTaken = Users.Get().FirstOrDefault(x => x.Username == username) != null;

        if (emailTaken || usernameTaken)
            return null;

        var admin = !Users.Get().Any(x => x.Admin);
            
        
        var user = Users.Add(new User()
        {
            Email = email.ToLower(),
            Password = HashHelper.HashToString(password),
            Username = username,
            Admin = admin,
            TokenValidTimestamp = DateTimeService.GetCurrent().AddDays(-5),
        });

        return user;
    }
    
    public async Task<string> Login(string email, string password)
    {
        var user = VerifyEmailAndPassword(email, password);

        if (user == null)
            throw new DisplayException("Invalid email or password.");
        
        return await GenerateToken(user);
    }

    public async Task<List<User>> GetAll()
    {
        return Users.Get().ToList();
    }
    
    public async Task<string> GenerateToken(User user)
    {
        var token = JwtBuilder.Create()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Secret)
            .AddClaim("exp", new DateTimeOffset(DateTimeService.GetCurrent().AddDays(10)).ToUnixTimeSeconds())
            .AddClaim("iat", DateTimeService.GetCurrentUnixSeconds())
            .AddClaim("userid", user.Id)
            .Encode();

        return token;
    }
    
    public Task ChangePassword(User user, string password)
    {
        user.Password = HashHelper.HashToString(password);
        user.TokenValidTimestamp = DateTimeService.GetCurrent();
        Users.Update(user);

        return Task.CompletedTask;
    }

    public async Task ChangeDetails(User user, string email, string username, bool admin)
    {
        user.Email = email;
        user.Username = username;
        user.Admin = admin;
        
        Users.Update(user);
    }

    public async Task ChangeEmail(User user, string email)
    {
        if(!Users.Get().Any(x => x.Email == email))
        {
            user.Email = email;
            Users.Update(user);
        }
        else
        {
            throw new DisplayException("A user with this email already exists.");
        }

    }
    
    public async Task ChangeUsername(User user, string username)
    {
        if(!Users.Get().Any(x => x.Username == username))
        {
            user.Username = username;
            Users.Update(user);
        }
        else
        {
            throw new DisplayException("A user with this username already exists.");
        }

    }
    
    public User? VerifyEmailAndPassword(string email, string password)
    {
        var user = GetUserByEmail(email);

        if (user == null) // Unknown email
            return null;

        // Verify password
        return !HashHelper.Verify(password, user.Password) ? null : user;
    }

    private User? GetUserByEmail(string email)
    {
        email = email
            .Trim()
            .ToLower();

        var user = Users
            .Get()
            .FirstOrDefault(x => x.Email == email);

        return user;
    }

    public async Task<bool> UsersExist()
    {
        return Users.Get().Any();
    }
    
    
}

