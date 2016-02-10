using System;
using AutoMapper;
using Worker.DAL.Entities;
using Worker.DTO.Entities;

namespace Worker.DTO.Mapping
{
    /// <summary>
    /// The auto mapper config.
    /// </summary>
    public static class AppAutoMapper
    {
        /// <summary>
        /// AppAutoMapper constructor.
        /// </summary>
        static AppAutoMapper()
        {
            MapEntityToDto();
            MapDtoToEntity();
        }

        /// <summary>
        /// Execute a mapping from the source object to a new destination object. The source type is inferred from the source object.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TResult Map<TResult>(object source)
        {
            return Mapper.Map<TResult>(source);
        }

        /// <summary>
        /// Execute a mapping from the source object to the existing destination object.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        /// <summary>
        /// Map entity to dto.
        /// </summary>
        private static void MapEntityToDto()
        {
            Mapper.CreateMap<Employee, EmployeeDto>();
        }

        /// <summary>
        /// Map dto to entity.
        /// </summary>
        private static void MapDtoToEntity()
        {
            Mapper.CreateMap<EmployeeDto, Employee>();
        }
    }
}
