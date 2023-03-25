using AutoMapper;
using PoqAssessment.Profiles;

namespace PoqAssessment.Tests.Mocks;

public class MapperMock
{
    public static IMapper MockMapper()
    {
        //auto mapper configuration
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProductsProfile());
        });

        return mockMapper.CreateMapper();
    }

}

