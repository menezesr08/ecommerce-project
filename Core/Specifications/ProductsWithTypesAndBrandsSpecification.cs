using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
        
        /*
        The role of this method is to create the conditions that we will pass onto the specification evaluator.
        So this particular method is for the clause: x => x.id == id
        We are also including the product type and product brand.
        To set the where clause, we need to set the criteria of the base specification as we will be checking and applying
        this criteria in the evaluator class. This allows us to set the criteria (from BaseSpecification) to anything we want.
        */
        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}