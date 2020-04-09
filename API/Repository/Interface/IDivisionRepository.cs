using API.Models;
using API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IDivisionRepository
    {
        IEnumerable<DivisionVM> Get();
        Task<IEnumerable<DivisionVM>> Get(int Id);
        int Create(DivisionVM division);
        int Update(int Id, DivisionVM division);
        int Delete(int Id);
    }
}
