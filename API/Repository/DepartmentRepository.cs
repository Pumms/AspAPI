using API.Migrations;
using API.Models;
using API.Repository.Interface;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Description;
using System.Web.UI.WebControls;

namespace API.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);

        DynamicParameters parameters = new DynamicParameters();

        public IEnumerable<Department> Get()
        {
            var procName = "SP_ViewDept";
            var view = connection.Query<Department>(procName, commandType: CommandType.StoredProcedure);
            return view;
        }

        public async Task<IEnumerable<Department>> Get(int Id)
        {
            var procName = "SP_DetailDept";
            parameters.Add("@Id", Id);
            var detail = await connection.QueryAsync<Department>(procName, parameters, commandType: CommandType.StoredProcedure);
            return detail;
        }

        public int Create(Department department)
        {
            var procName = "SP_InsertDept";
            parameters.Add("@Name", department.Name);
            var create = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int Id)
        {
            var procName = "SP_DeleteDept";
            parameters.Add("@Id", Id);
            var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return update;
        }

        public int Update(int Id, Department department)
        {
            var procName = "SP_UpdateDept";
            parameters.Add("@Id", Id);
            parameters.Add("@Name", department.Name);
            var update = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
            return update;
        }
    }
}