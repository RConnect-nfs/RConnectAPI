namespace RconnectAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RconnectAPI.Models;
    using RconnectAPI.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ServiceUtilisateur _serviceUtilisateur;

        public AuthController(ServiceUtilisateur serviceUtilisateur)
        {
            _serviceUtilisateur = serviceUtilisateur;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Inscription([FromBody] InscriptionDTO dto)
        {
            var utilisateur = await _serviceUtilisateur.InscrireAsync(dto);
            if (utilisateur == null)
            {
                return BadRequest();
            }
            return Ok(utilisateur);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Connexion([FromBody] ConnexionDTO dto)
        {
            var utilisateur = await _serviceUtilisateur.TrouverParEmailMotDePasseAsync(dto.Email, dto.MotDePasse);
            if (utilisateur == null)
            {
                return Unauthorized("Identifiants incorrects.");
            }

            var token = _serviceUtilisateur.GenererJwtToken(utilisateur);
            return Ok(new { token = token });
        }
    }
}
