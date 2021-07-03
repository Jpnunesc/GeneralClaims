
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
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Test.Tests.service
{
    public class HeroisTest
    {
        readonly Mock<IConfiguration> mockConfig = new Moq.Mock<IConfiguration>();
        readonly Mock<IConfigurationSection> mockSection = new Mock<IConfigurationSection>();
        readonly Mock<IMapper> mapper = new Mock<IMapper>();
        readonly Mock<IFavoritosRepository> mockRepository = new Moq.Mock<IFavoritosRepository>();
        [Fact(DisplayName = "Ao tentar consumir api marvel, se houver registros, deve retornar personagens")]
        public async Task Buscar_registros_apiMarvel()
        {
            HeroisService service = new HeroisService(mapper.Object, mockRepository.Object, mockConfig.Object);
            mockSection.Setup(x => x.Value).Returns("https://gateway.marvel.com:443/v1/public/characters?ts=1627614000000&apikey=6f105de22e65cd2be25d3cdbe5bdfac9&hash=6f9d66847dfaba886d8721c96b9b8474&limit=100");
            mockConfig.Setup(x => x.GetSection(It.Is<string>(k => k == "ApiMarvel"))).Returns(mockSection.Object);
            var result = await service.Get(new Business.IO.Herois.FiltroHerois() { Favorito = null, Nome = null });
            Assert.NotNull(result.Object);
        }
    }
}
