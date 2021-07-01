
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Xunit;
using Business.IO.Users;
using Business.Services;
using Business.Interfaces.Repositories;
using Domain.Entity;
using Business.Interfaces.Services;
using Domain.Entitys;

namespace Test.Tests.service
{
    public class HeroisTest
    {
        Mock<IUserService> mockService = new Moq.Mock<IUserService>();
        Mock<IMapper> mapper = new Mock<IMapper>();
        Mock<IFavoritosRepository> mockRepository = new Moq.Mock<IFavoritosRepository>();
        [Fact(DisplayName = "Ao tentar cadastrar Produto, Quando dados corretos, deve retornar cadastrado")]
        public async void CadastrarTipoAdministracao_ComDadosCorretos_DeveRetornarCadastrado()
        {

            //var obj = new ProdutoInput()
            //{
            //    Nome = "Nome Teste",
            //    Imagem = "ajsdalkshdlashdhcnakjcdhasdalsdkaohidalsdnalksdhlakshdlaksdnalkshclasncajkshfasldhalsdaslçdlkhaodhalksdnlaçsdhalçshalçcnassçlchbaçksdhaç",
            //    Valor = 30,
            //};
            //mapper.Setup(m => m.Map<ProdutoEntity, ProdutoInput>(It.IsAny<ProdutoEntity>())).Returns(obj);
            //ProdutoService service = new ProdutoService(mapper.Object, mockRepository.Object);
            // var result = await service.Save(obj);

            //Assert.Null(result.Object);

        }
    }
}
