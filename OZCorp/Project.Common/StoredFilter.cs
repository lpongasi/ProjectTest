using System.Collections.Generic;

namespace Project.Common
{
    public static class QueryFormat
    {
        public static string Select(string column, string tables, string where = null, string groupBy = null, string orderBy = null, bool offsetLimit = false)
            => $"SELECT {column} " +
               $"FROM {tables} " +
               $"{(!string.IsNullOrEmpty(where) ? $"WHERE {where} " : string.Empty)}" +
               $"{(!string.IsNullOrEmpty(groupBy) ? $"GROUP BY {groupBy} " : string.Empty)}" +
               $"{(!string.IsNullOrEmpty(orderBy) ? $"ORDER BY {orderBy} " : string.Empty)}" +
               $"{(offsetLimit ? "LIMIT @Limit OFFSET @Offset" : string.Empty)}";
    }
    public static class SqlReportItem
    {
        private const string ColCount = "COUNT(Distinct poi.itemid,poi.Price) as Count";
        private const string ColTotal = "SUM(poi.Price * poi.Qty) as Total";
        private const string ColList = "i.Id," +
                                       "i.Barcode," +
                                       "i.Name," +
                                       "s.Name as Size," +
                                       "i.Description," +
                                       "c.Name AS Category," +
                                       "sc.Name AS SubCategory," +
                                       "poi.Price," +
                                       "SUM(poi.Qty) as Quantity," +
                                       "SUM(poi.Qty * poi.Price) as Total";

        private const string Tables = "PURCHASEORDERITEM poi " +
                                      "INNER JOIN PURCHASEORDER po on po.Id = poi.PurchaseOrderId " +
                                      "INNER JOIN ITEM i on i.Id = poi.ItemId " +
                                      "INNER JOIN CATEGORY c on c.Id = i.categoryId " +
                                      "INNER JOIN SUBCATEGORY sc on sc.Id = i.subCategoryId " +
                                      "LEFT JOIN SIZE s on s.Id = i.sizeId ";

        private const string Filter = "(@Search IS NULL " +
                                      "OR @Search = '' " +
                                      "OR i.Barcode LIKE @Search " +
                                      "OR i.Name LIKE @Search " +
                                      "OR i.Description LIKE @Search) " +
                                      "AND (@CategoryId IS NULL OR @CategoryId = 0 OR  @CategoryId = i.categoryId) " +
                                      "AND (@SubCategoryId IS NULL OR @SubCategoryId = 0 OR  @SubCategoryId = i.subCategoryId) " +
                                      "AND CAST(po.PoDate AS DATE) BETWEEN CAST(@From AS DATE) and CAST(@To AS DATE) " +
                                      "AND po.PurchaseOrderStatusId in (2,4,5)";


        private const string GroupBy = "poi.itemid, poi.Price";
        private const string OrderBy = "SUM(poi.Qty) Desc";

        public static string SelectCount = QueryFormat.Select(ColCount, Tables, Filter);
        public static string SelectTotal = QueryFormat.Select(ColTotal, Tables, Filter);
        public static string SelectList(bool limitOffset) => QueryFormat.Select(ColList, Tables, Filter, GroupBy, OrderBy, !limitOffset);
    }
    public static class SqlItem
    {
        public static string ImageList(IList<long> ids)
            => QueryFormat.Select(@"ITEMID as 'Key',MIN(FILELOCATION) AS 'Value'", "IMAGELOCATION",
                $"ITEMID IN({string.Join(",", ids) ?? "0"})", "ITEMID");
    }
    public static class SqlReportCompany
    {
        private const string InnerCount = "u.CompanyName";
        private const string InnerTotal = "SUM((poi.Price * poi.qty)"
                                        + " - ((poi.Price * poi.qty) *  poi.discount))"
                                        + " -"
                                        + " (SUM((poi.Price * poi.qty)"
                                        + " - ((poi.Price * poi.qty) *  poi.discount)) * po.Discount)"
                                        + " +"
                                        + " (SUM((poi.Price * poi.qty)"
                                        + " - ((poi.Price * poi.qty) *  poi.discount)) * po.Tax)"
                                        + " +"
                                        + " po.OtherFees  AS Total";
        private static readonly string InnerList = "u.CompanyName," +
                                          "u.CompanyContact," +
                                          "u.CompanyAddress," +
                                          $"u.Discount,{InnerTotal}";
        private const string InnerGroup = "po.Id, u.CompanyName";

        private const string OuterCount = "COUNT(DISTINCT compo.CompanyName) AS Count";
        private const string OuterTotal = "SUM(compo.Total) AS Total";
        private static readonly string OuterList = "compo.CompanyName," +
                                          "compo.CompanyContact," +
                                          "compo.CompanyAddress," +
                                          $"compo.Discount,{OuterTotal}";
        private const string OuterGroup = "compo.CompanyName";
        private const string OuterOrder = "SUM(compo.Total) desc";

        private const string Tables = "purchaseorderitem poi INNER JOIN purchaseorder po on po.Id = poi.PurchaseOrderId INNER JOIN user u on u.Id = po.UserId";
        private const string Filter = "(@Search IS NULL " +
                                      "OR @Search = '' " +
                                      "OR u.CompanyName LIKE @Search " +
                                      "OR u.CompanyContact LIKE @Search " +
                                      "OR u.CompanyAddress LIKE @Search) " +
                                      "AND CAST(po.PoDate AS DATE) BETWEEN CAST(@From AS DATE) AND CAST(@To AS DATE) " +
                                      "AND po.PurchaseOrderStatusId in (2,4,5)";

        public static string SelectCount = QueryFormat.Select(OuterCount,
            $"({QueryFormat.Select(InnerCount, Tables, Filter, InnerGroup)}) compo");
        public static string SelectTotal = QueryFormat.Select(OuterTotal,
            $"({QueryFormat.Select(InnerTotal, Tables, Filter, InnerGroup)}) compo");
        public static string SelectList(bool limitOffset) => QueryFormat.Select(OuterList,
            $"({QueryFormat.Select(InnerList, Tables, Filter, InnerGroup)}) compo",
            null,
            OuterGroup,
            OuterOrder,
            limitOffset);
    }

}
