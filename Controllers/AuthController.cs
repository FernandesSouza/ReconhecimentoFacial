using DetectarFace.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DetectarFace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IDetectarFaces _reconhecimento;

        public AuthController(ITokenService tokenService, IDetectarFaces reconhecimento)
        {
            _tokenService = tokenService;
            _reconhecimento = reconhecimento;
        }

        [HttpPost]
        public async Task<ActionResult<dynamic>> Login()
        {
            try
            {
                bool service = await _reconhecimento.ReconhecimentoFacial();
                if(service == true)
                {
                    var token =  _tokenService.GerarToken(service);

                    return Ok(new { Token = token });
                }
                else
                {
                    return BadRequest();

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocorreu um erro interno ao processar a solicitação." + ex });
            }


        }
    }
}
