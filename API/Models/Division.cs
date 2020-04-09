using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    [Table("TB_M_Division")]
    public class Division
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public Nullable<DateTimeOffset> UpdateDate { get; set; }
        public Nullable<DateTimeOffset> DeleteDate { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public Division() { }

        public Division(Division division)
        {
            this.Name = division.Name;
            this.CreateDate = DateTimeOffset.Now;
            this.IsDelete = false;
            this.DepartmentId = division.Department.Id;
        }

        public void Update(Division division)
        {
            this.Name = division.Name;
            this.UpdateDate = DateTimeOffset.Now;
            this.DepartmentId = division.Department.Id;
        }

        public void Delete(Division division)
        {
            this.IsDelete = true;
            this.DeleteDate = DateTimeOffset.Now;
        }
    }
}