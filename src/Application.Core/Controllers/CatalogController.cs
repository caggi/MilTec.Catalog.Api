using Domain.Entities;
using Domain.Enums;
using Infra.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Application.Core.Controllers
{
    /// <summary>
    /// Controladora destinada ao gerenciamento dos endpoints do cátalogo.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class CatalogController : ControllerBase
    {
        /// <summary>
        /// Repositório responsável por gerenciar o acesso aos livros do catálogo.
        /// </summary>
        private readonly CatalogRepository _repository;

        /// <summary>
        /// Inicializa a controladora recebendo por injeção de dependência o CatalogRepository.
        /// </summary>
        /// <param name="catalogRepository">Instância do CatalogRepository.</param>
        public CatalogController(CatalogRepository catalogRepository)
        {
            _repository = catalogRepository;
        }

        /// <summary>
        /// Busca os livros do catálogo.
        /// É possível buscar os livros por todos os atributos de sua especificação, como nome do livre ou autor.
        /// O resultado pode ser ordenado pelo preço.
        /// </summary>
        ///
        /// <param name="order">Define a ordenação da listagem dos livros.</param>
        /// <param name="query">Busca os livros que possuem em sua especificação a consulta informada.</param>
        ///
        /// <returns>Uma lista contendo os livros do catálogo.</returns>
        ///
        /// <response code="200">Retorna a lista dos livros do catálogo</response>
        /// <response code="500">Ops! Alguma coisa está errada.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IList<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(string? query, BooksOrders? order)
        {
            return base.Ok(await _repository.Get(query, order));
        }

        /// <summary>
        /// Recupera um livro específico por id exclusivo.
        /// </summary>
        ///
        /// <param name="id">Identificador exclusivo do livro.</param>
        ///
        /// <returns>O livro respectivo ao identificador informado.</returns>
        /// 
        /// <response code="200">O livro respectivo ao identificador informado.</response>
        /// <response code="404">O livro não foi encontrado para o identificador informado.</response>
        /// <response code="500">Ops! Alguma coisa está errada.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync(int id)
        {
            if (await _repository.Exist(id))
            {
                var result = await _repository.Get(id);

                if (result is not null)
                    return base.Ok(result);
            }

            return base.NotFound();
        }

        /// <summary>
        /// Calcula o valor do frete em 20% o valor do livro.
        /// </summary>
        ///
        /// <param name="id">Identificador exclusivo do livro.</param>
        ///
        /// <returns>O valor do frete de acordo com o livro respectivo ao identificador informado.</returns>
        /// 
        /// <response code="200">O valor do frete de acordo com o livro respectivo ao identificador informado.</response>
        /// <response code="400">O valor do frete não pode ser calculado pois o livro respectivo ao identificador informado não pôde ser encontrado.</response>
        /// <response code="500">Ops! Alguma coisa está errada.</response>
        [HttpGet("/catalog/calculate-shipping/{id}")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CalculateShipping(int id)
        {
            if (await _repository.Exist(id))
            {
                var result = await _repository.CalculateShipping(id);

                if (result is not null)
                    return base.Ok(result);
            }

            return base.BadRequest();
        }
    }
}
