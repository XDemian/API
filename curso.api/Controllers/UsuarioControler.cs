using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Configuration;
using curso.api.Filter;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


namespace curso.api.Controllers
{
    [Route("api/v1/usuario")]
    [ApiController]
   // [Authorize] // tem que verificar o porque o Swegger nao es
    public class UsuarioControler : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private  readonly IAuthenticationService _authenticationService;
        public UsuarioControler( 
            IUsuarioRepository usuarioRepository ,
            IAuthenticationService authenticationService )
        {
            _usuarioRepository = usuarioRepository;
            _authenticationService = authenticationService;
        } 

        [SwaggerResponse(statusCode: 200, description: "Sucesso ao Autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatorio", Type = typeof(ValidarCampoViewModelIOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", Type = typeof(ErroGenericoViewModel))]

        [HttpPost]
        [Route("logar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            var  usuario = _usuarioRepository.ObterUsuario(loginViewModelInput.Login);

            if (usuario == null)
            {
                return BadRequest("Houve um Erro ao Tentar Acessar.");
            }
            //if (usuario.Senha != loginViewModel.Senha.GerarCriptografada())
            //{
            //    return BadRequest("Houve um erro ao Tentar acessar.");
            //}

            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = usuario.Codigo,
                Login = loginViewModelInput.Login,
                Email = usuario.Email 
            };

            var token = _authenticationService.GerarToken(usuarioViewModelOutput);

            return Ok(new 
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao Autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos Obrigatorio", Type = typeof(ValidarCampoViewModelIOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro Interno", Type = typeof(ErroGenericoViewModel))]
        [HttpPost]
        [Route("registrar")]
        [ValidacaoModelStateCustomizado]
        public IActionResult Registrar(RegistrarViewModelInput loginViewModelInput)
        {
            //var migracoesPendentes = contexto.Database.GetPendingMigrations();

            //if (migracoesPendentes.Count() > 0)
            //{
            //    contexto.Database.Migrate();
            //}

            var usuario = new Usuario();
            usuario.Login = loginViewModelInput.Login;
            usuario.Senha = loginViewModelInput.Senha;
            usuario.Email = loginViewModelInput.Email;
             _usuarioRepository.Adicionar(usuario);
            _usuarioRepository.Commit();

            return Created("", loginViewModelInput); // Status 201 Criando Registro, Status 200 Registro Criado , Status 400  Erro ao Criar Registro algum campo não esta preechido, Status 500 Erro generico.
        }
    }

}
