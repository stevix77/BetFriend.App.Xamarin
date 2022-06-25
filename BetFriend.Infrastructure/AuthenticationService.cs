﻿namespace BetFriend.Infrastructure
{
    using BetFriend.Domain.Users;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;

    public class AuthenticationService : IAuthenticationService
    {
        public string Token { get; private set; }

        public string UserId { get; private set; }
        public string Username { get; private set; }

        public void SetToken(string token)
        {
            token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwidXNlcm5hbWUiOiJzdGV2aXgiLCJpYXQiOjE1MTYyMzkwMjIsImlkIjoiNTFmZWNiMmYtNTVhZC00MzhhLWJhODYtZWM3MDJmMTlmY2VjIn0.7wbhE9XrkS2M5AriO9VVkQZlt3j6bRYiaq3V1ePPqYk";
            Token = token;
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            UserId = "51fecb2f-55ad-438a-ba86-ec702f19fcec";
            Username = "stevix";
        }
    }
}
