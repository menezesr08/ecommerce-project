using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        /*
        Here we are setting the criteria from the base specification which is usually just a where clause. 
        The query is an or else / and else query and it works like this:
        If the brandId has a value, then !brandId.HasValue evaluates to false
        Because brandId evaluates to false, we execute the statement on the other side of the or symbol (||)
        so in this case we are checking to see if the brandId exists in the database
        */
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
            : base(x =>
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains
                (productParams.Search)) &&
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            // Default ordering
            AddOrderBy(x => x.Name);
            /* Applypaging takes two parameters: skip and take
            Skip works by taking the page size * page index -1
            We do because lets say we are on page 1. We would like to display some elements on page 1 but if we did
            page size * 1 then we would get page size. So we are setting the skip parameter to page size which means it will skip
            that amount. If products for page 1. 
            The correct way to do page size * 1 - 1 which is 0. This tell us that for page 1, we don't want to skip any items. 
            The take parameter retrieves the number of products as dictacted by the take parameter. This allows the user to retrieve 
            as many products as they would like.
            By default the pageIndex is 1. So if there is no page index being set, then we would return 6 products as pagesize is 6
            */
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
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