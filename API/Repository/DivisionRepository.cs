using API.Models;
using API.ViewModel;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace API.Repository
{
    public class DivisionRepository
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

        DynamicParameters parameters = new DynamicParameters();

        public IEnumerable<DivisionVM> Get()
        {
            var procName = "SP_ViewDivision";
            var view = connection.Query<DivisionVM>(procName, commandType: CommandType.StoredProcedure);
            return view;
        }

        public async Task<IEnumerable<DivisionVM>> Get(int Id)
        {
            var procName = "SP_DetailDivision";
            parameters.Add("@Id", Id);
            var detail = await connection.QueryAsync<DivisionVM>(procName, parameters, commandType: CommandType.StoredProcedure);
            return detail;
        }

        public int Create(DivisionVM division)
        {
            var procName = "SP_InsertDivision";
            parameters.Add("@Name", division.DivisionName);
            parameters.Add("@DepartmentId", division.DepartmentId);
            var create = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int Id)
        {
            var procName = "SP_DeleteDivision";
            parameters.Add("@Id", Id);
            var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return update;
        }

        public int Update(int Id, DivisionVM division)
        {
            var procName = "SP_UpdateDivision";
            parameters.Add("@Id", Id);
            parameters.Add("@Name", division.DivisionName);
            parameters.Add("@DepartmentId", division.DepartmentId);
            var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return update;
        }
    }
}