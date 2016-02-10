using AutoMapper;
using Worker.DTO.Entities;
using Worker.Web.ViewModel;
using Worker.Web.ViewModel.Employee;

namespace Worker.Web
{
    /// <summary>
    /// The auto mapper config.
    /// </summary>
    public static class AutoMapperConfig
    {
        /// <summary>
        /// The register mapping.
        /// </summary>
        public static void RegisterMapping()
        {
            MapDtoToViewModel();
            MapViewModelToDto();
        }

        /// <summary>
        /// The map dto to view model.
        /// </summary>
        private static void MapDtoToViewModel()
        {
            Mapper.CreateMap<EmployeeDto, EmployeeViewModel>();
        }

        /// <summary>
        /// The map view model to dto.
        /// </summary>
        private static void MapViewModelToDto()
        {
            Mapper.CreateMap<EmployeeViewModel, EmployeeDto>();
        }
    }
}