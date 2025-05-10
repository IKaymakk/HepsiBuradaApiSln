using HepsiBuradaApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiBuradaApi.Domain.Entities
{
    public class Category : EntityBase, IEntityBase
    {
        public Category()
        {
        }

        public Category(int parentId, string name, int priorty)
        {
            ParentId = parentId;
            Priorty = priorty;
            Name = name;
        }
        public  int ParentId { get; set; }
        public  string Name { get; set; }
        public  int Priorty { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
