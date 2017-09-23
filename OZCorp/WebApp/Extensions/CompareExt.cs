using System;
using System.Collections.Generic;
using System.Linq;
using Project.Entities.Product;

namespace WebApp.Extensions
{
    public static class CompareExt
    {
        public static string GetChangesFrom(this Item item, Item compare)
        {
            var changes = string.Empty;

            if (item != null && compare != null)
            {
                changes +=
                    string.Join(", ", GetMessages(
                         item.Barcode.CompareTo(compare.Barcode, "Barcode")
                        , item.Name.CompareTo(compare.Name, "Name")
                        , item.Description.CompareTo(compare.Description, "Description")
                        , item.UnitCost.ToString("N2").CompareTo(compare.UnitCost.ToString("N2"), "UnitCost")
                        , item.Price.ToString("N2").CompareTo(compare.Price.ToString("N2"), "Price")
                        , item.Qty.ToString().CompareTo(compare.Qty.ToString(), "Qty")
                        , item.QtyNotification.ToString().CompareTo(compare.QtyNotification.ToString(), "Qty Notification")
                        , item.StockNo.CompareTo(compare.StockNo, "StockNo")
                        , item.PartNo.CompareTo(compare.PartNo, "PartNo")
                        , item.Location.CompareTo(compare.Location, "Location")
                        ));
            }
            return changes;
        }

        public static IList<string> GetMessages(params string[] message)
        {
            return message.Where(w => !string.IsNullOrEmpty(w)).ToList();
        }

        public static string CompareTo(this string item, string comparedItem, string displayName = "Change", string message = "{0}: {1} => {2}")
        {
            item = item ?? string.Empty;
            comparedItem = comparedItem ?? string.Empty;
            return !item.Equals(comparedItem) ? string.Format(message,
                displayName, item, comparedItem) : string.Empty;
        }

        public static bool IsDecimal<T>(this T type, out decimal variable)
        {
            var isDecimal = decimal.TryParse(type?.ToString(), out decimal parseVariable);
            var result = new[] { typeof(Decimal?), typeof(Decimal), typeof(decimal?), typeof(decimal) }.Contains(typeof(T))
                        && isDecimal;
            variable = parseVariable;
            return result;
        }
    }
}
