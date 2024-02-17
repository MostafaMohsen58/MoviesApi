using AutoMapper;

namespace MoviesApi.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie,MovieDetailsDto>();
            CreateMap<MovieDto, Movie>()
                    .ForMember(s => s.Poster, o => o.Ignore());

        }
    }
}
