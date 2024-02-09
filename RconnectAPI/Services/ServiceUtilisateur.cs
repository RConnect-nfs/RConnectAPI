using RconnectAPI.Models;

namespace RconnectAPI.Services
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;
    using MongoDB.Driver;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class ServiceUtilisateur
    {
        private readonly IMongoCollection<Utilisateur> _utilisateurs;
        private readonly PasswordHasher<Utilisateur> _passwordHasher = new PasswordHasher<Utilisateur>();
        private readonly IConfiguration _configuration;

        public ServiceUtilisateur(IMongoClient client, string DatabaseName, IConfiguration configuration)
        {
            var database = client.GetDatabase(DatabaseName);
            _utilisateurs = database.GetCollection<Utilisateur>("utilisateurs");
            _configuration = configuration;

        }
        private byte[] GetSigningKey()
        {
            var base64Key = _configuration["Jwt:SecretKey"];
            return Convert.FromBase64String(base64Key);
        }

        public async Task<Utilisateur> InscrireAsync(InscriptionDTO inscription)
        {
            var utilisateur = new Utilisateur
            {
                NomUtilisateur = inscription.NomUtilisateur,
                Email = inscription.Email,
                MotDePasse = _passwordHasher.HashPassword(null, inscription.MotDePasse)
            };

            await _utilisateurs.InsertOneAsync(utilisateur);
            return utilisateur;
        }

        public async Task<Utilisateur?> TrouverParEmailMotDePasseAsync(string email, string motDePasse)
        {
            var utilisateur = await _utilisateurs.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (utilisateur != null)
            {
                // Utilisez _passwordHasher.VerifyHashedPassword
                var result = _passwordHasher.VerifyHashedPassword(utilisateur, utilisateur.MotDePasse, motDePasse);
                if (result == PasswordVerificationResult.Success)
                {
                    return utilisateur;
                }
            }
            return null;
        }




        public string GenererJwtToken(Utilisateur utilisateur)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = GetSigningKey();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString()),
                new Claim(ClaimTypes.Email, utilisateur.Email)
            }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                // Gérer l'exception ou la journaliser
                throw new InvalidOperationException("Une erreur s'est produite lors de la génération du token JWT.", ex);
            }
        }
    }
}
