using DetectarFace.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DetectarFace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetectarController : ControllerBase
    {

        private readonly IDetectarFaces _service;

        public DetectarController(IDetectarFaces service)
        {
            _service = service;
        }


        [HttpPost("detectar")]
        public async Task<IActionResult> DetectarFaces(IFormFile file)
        {

            var result = await _service.DetectarFace(file);
            return Ok(new { faceFileName = result });
        }


        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetFaceResult([FromRoute] string fileName)
        {

            if (!System.IO.File.Exists(fileName))
                return NotFound();

            var bytes = System.IO.File.ReadAllBytes(fileName);

            return File(bytes, "image/jpeg");
        }

        [HttpGet]
        public async Task<IActionResult> Reconhecimento()
        {
           
            try
            {
                // Chamar o método de reconhecimento facial
                bool resultado = await _service.ReconhecimentoFacial();
                // Retornar o resultado
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Em caso de erro, retornar um status 500 com uma mensagem de erro
                return StatusCode(500, $"Erro durante o reconhecimento facial: {ex.Message}");
            }

        }


    }
}
