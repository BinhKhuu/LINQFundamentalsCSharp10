namespace LINQSamples
{
    public class SamplesViewModel : ViewModelBase
    {
        #region GroupByQuery
        /// <summary>
        /// Group products by Size property. orderby is optional, but generally used
        /// </summary>
        public List<IGrouping<string, Product>> GroupByQuery()
        {
            List<IGrouping<string, Product>> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();

            // Write Query Syntax Here
            list = (from p in products
                    group p by p.Size).ToList();
            return list;
        }
        #endregion

        #region GroupByMethod
        /// <summary>
        /// Group products by Size property. orderby is optional, but generally used
        /// </summary>
        public List<IGrouping<string, Product>> GroupByMethod()
        {
            List<IGrouping<string, Product>> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();

            // Write Method Syntax Here
            list = products.GroupBy(p => p.Size).ToList();

            return list;
        }
        #endregion

        #region GroupByIntoQuery
        /// <summary>
        /// Group products by Size property. 'into' is optional.
        /// </summary>
        public List<IGrouping<string, Product>> GroupByIntoQuery()
        {
            List<IGrouping<string, Product>> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();

            // Write Query Syntax Here
            list = (from p in products
                    group p by p.Size into sizes
                    select sizes).ToList();

            return list;
        }
        #endregion

        #region GroupByUsingKeyQuery
        /// <summary>
        /// After selecting 'into' new variable, can sort on the 'Key' property. Key property has the value of what you grouped on.
        /// </summary>
        public List<IGrouping<string, Product>> GroupByUsingKeyQuery()
        {
            List<IGrouping<string, Product>> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();

            // Write Query Syntax Here

            // Order grouping by max size then each product order by Product ID Descending
            var a = (from p in products
                     group p by p.Size into sizes
                     let productSizeGroup = sizes.Max(p => p.Size)
                     orderby productSizeGroup
                     select new
                     {
                         Key = sizes.Key,
                         Products = (from prod in sizes
                                     orderby prod.ProductID descending
                                     select prod).ToList(),
                     })
                    .GroupBy(p => p.Key)
                    .ToList();

            // Order grouping by max product ID for each grouping
            list = (from p in products
                    group p by p.Size into sizes
                    let productSizeGroup = sizes.Max(p => p.ProductID)
                    orderby productSizeGroup descending
                    select sizes)
                .ToList();

            return list;
        }
        #endregion

        #region GroupByUsingKeyMethod
        /// <summary>
        /// After selecting 'into' new variable, can sort on the 'Key' property. Key property has the value of what you grouped on.
        /// </summary>
        public List<IGrouping<string, Product>> GroupByUsingKeyMethod()
        {
            List<IGrouping<string, Product>> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();

            // Write Method Syntax Here
            list = products.GroupBy(p => p.Size)
                    .OrderBy(p => p.Key)
                    .ToList();

            return list;
        }
        #endregion

        #region GroupByWhereQuery
        /// <summary>
        /// Group products by Size property and where the group has more than 2 members
        /// This simulates a HAVING clause in SQL
        /// </summary>
        public List<IGrouping<string, Product>> GroupByWhereQuery()
        {
            List<IGrouping<string, Product>> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();


            // sort before grouping
            list = (from product in products
                    orderby product.ProductID descending
                    group product by product.Size into sizes
                    where sizes.Count() > 2
                    select sizes)
                    .ToList();

            return list;
        }
        #endregion

        #region GroupByWhereMethod
        /// <summary>
        /// Group products by Size property and where the group has more than 2 members
        /// This simulates a HAVING clause in SQL
        /// </summary>
        public List<IGrouping<string, Product>> GroupByWhereMethod()
        {
            List<IGrouping<string, Product>> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();

            // Write Method Syntax Here
            list = products.GroupBy(p => p.Size)
                .OrderBy(p => p.Key)
                .Where(p => p.Count() > 2)
                .ToList();

            return list;
        }
        #endregion

        #region GroupBySubQueryQuery
        /// <summary>
        /// Group Sales by SalesOrderID, add Products into new Sales Order object using a subquery
        /// </summary>
        public List<SaleProducts> GroupBySubQueryQuery()
        {
            List<SaleProducts> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Query Syntax Here
            list = (from s in sales
                    orderby s.SalesOrderID descending
                    group s by s.SalesOrderID into salesOrderGroup
                    select new SaleProducts
                    {
                        SalesOrderID = salesOrderGroup.Key,
                        Products = (from product in products
                                    join s in sales on product.ProductID equals s.ProductID
                                    orderby product.ProductID descending
                                    where s.SalesOrderID == salesOrderGroup.Key
                                    select product)
                                    .ToList()
                    }).ToList();

            return list;
        }
        #endregion

        #region GroupBySubQueryMethod
        /// <summary>
        /// Group Sales by SalesOrderID, add Products into new Sales Order object using a subquery
        /// </summary>
        public List<SaleProducts> GroupBySubQueryMethod()
        {
            List<SaleProducts> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();
            // Load all Sales Data
            List<SalesOrder> sales = SalesOrderRepository.GetAll();

            // Write Method Syntax Here
            list = sales.GroupBy(s => s.SalesOrderID)
                .OrderBy(s => s.Key)
                .Select(s => new SaleProducts
                {
                    SalesOrderID = s.Key,
                    Products = products.OrderBy(p => p.ProductID)
                        .Join(s, 
                            prod => prod.ProductID,
                            sale => sale.ProductID,
                            (prod, sale) => prod)
                        .ToList()     
                }).ToList();

            return list;
        }
        #endregion

        #region GroupByDistinctQuery
        /// <summary>
        /// The Distinct() operator can be simulated using the GroupBy() and FirstOrDefault() operators
        /// In this sample you put distinct product colors into another collection using LINQ
        /// </summary>
        public List<string> GroupByDistinctQuery()
        {
            List<string> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();

            // Write Query Syntax Here
            list = (from p in products
                    orderby p.ProductID descending
                    group p by p.Color into groupedColours
                    select groupedColours.FirstOrDefault().Color).ToList();
            return list;
        }
        #endregion

        #region GroupByDistinctMethod
        /// <summary>
        /// The Distinct() operator can be simulated using the GroupBy() and FirstOrDefault() operators
        /// In this sample you put distinct product colors into another collection using LINQ
        /// </summary>
        public List<string> GroupByDistinctMethod()
        {
            List<string> list = null;
            // Load all Product Data
            List<Product> products = ProductRepository.GetAll();

            // Write Method Syntax Here
            list = products.OrderByDescending(p => p.ProductID)
                .GroupBy(p => p.Color)
                .Select(p => p.FirstOrDefault().Color).ToList();
            return list;
        }
        #endregion
    }
}
