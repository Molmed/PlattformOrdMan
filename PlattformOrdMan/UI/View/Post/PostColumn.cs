﻿using PlattformOrdMan.Data;
using PlattformOrdMan.Data.Conf;
using PlattformOrdMan.Data.Exception;
using PlattformOrdMan.UI.View.Base;

namespace PlattformOrdMan.UI.View.Post
{
    public class PostColumn
    {
        private readonly PostListViewColumn _colEnum;
        private readonly int _width;

        public int Width => _width;
        public PostListViewColumn ColEnum => _colEnum;

        public int GetColumnWith()
        {
            var col = _colEnum;
            var expression = PostListViewConfColumns.ColEnumName + " = '" + col + "'";
            var rows = PlattformOrdManData.Configuration.PostListViewSelectedColumns.Select(expression);
            if (rows.Length > 0)
            {
                return (int)rows[0][PostListViewConfColumns.ColWidth.ToString()];
            }
            else
            {
                rows = Configuration.GetDefaultPostListViewColumns().Select(expression);
                return (int)rows[0][PostListViewConfColumns.ColWidth.ToString()];
            }
        }

        public PostColumn(PostListViewColumn colEnum, int width)
        {
            _colEnum = colEnum;
            _width = width;
        }
        public ListDataType GetListDataType()
        {
            switch (_colEnum)
            {
                case PostListViewColumn.Amount:
                case PostListViewColumn.InvoiceCategoryCode:
                    return ListDataType.Int32;

                case PostListViewColumn.ApprArrival:
                case PostListViewColumn.ArrivalDate:
                case PostListViewColumn.BookDate:
                case PostListViewColumn.InvoiceDate:
                case PostListViewColumn.OrderDate:
                    return ListDataType.DateTime;

                case PostListViewColumn.ApprPrize:
                case PostListViewColumn.FinalPrize:
                case PostListViewColumn.TotalPrize:
                    return ListDataType.Currency;

                case PostListViewColumn.ArrivalSign:
                case PostListViewColumn.ArtNr:
                case PostListViewColumn.Booker:
                case PostListViewColumn.Comment:
                case PostListViewColumn.DeliveryDeviation:
                case PostListViewColumn.InvoiceClin:
                case PostListViewColumn.InvoiceInst:
                case PostListViewColumn.InvoiceNumber:
                case PostListViewColumn.InvoiceSender:
                case PostListViewColumn.InvoiceStatus:
                case PostListViewColumn.OrderSign:
                case PostListViewColumn.Product:
                case PostListViewColumn.PurchaseSalesOrderNo:
                case PostListViewColumn.Supplier:
                case PostListViewColumn.PlaceOfPurchase:
                case PostListViewColumn.Account:
                case PostListViewColumn.Periodization:
                case PostListViewColumn.VangenSummary:
                    return ListDataType.String;

                default:
                    throw new DataException("Unknown enum type: " + _colEnum);
            }
        }

        public string GetHeader()
        {
            switch (_colEnum)
            {
                case PostListViewColumn.Amount:
                case PostListViewColumn.Comment:
                case PostListViewColumn.Supplier:
                case PostListViewColumn.Booker:
                case PostListViewColumn.Account:
                case PostListViewColumn.Periodization:
                    return _colEnum.ToString();
                case PostListViewColumn.VangenSummary:
                    return "Summary";

                case PostListViewColumn.InvoiceCategoryCode:
                    return "Invoice category code";
                case PostListViewColumn.ApprArrival:
                    return "Appr. arrival";
                case PostListViewColumn.ArrivalDate:
                    return "Arrival date";
                case PostListViewColumn.BookDate:
                    return "Date";
                case PostListViewColumn.InvoiceDate:
                    return "Invoice check date";
                case PostListViewColumn.OrderDate:
                    return "Order date";
                case PostListViewColumn.ApprPrize:
                    return "Appr. prize";
                case PostListViewColumn.FinalPrize:
                    return "Final prize";
                case PostListViewColumn.TotalPrize:
                    return "Total prize";
                case PostListViewColumn.ArrivalSign:
                    return "Arrival sign.";
                case PostListViewColumn.ArtNr:
                    return "Art. no";
                case PostListViewColumn.DeliveryDeviation:
                    return "Delivery deviation";
                case PostListViewColumn.InvoiceClin:
                    return "Invoice clin";
                case PostListViewColumn.InvoiceInst:
                    return "Invoice inst";
                case PostListViewColumn.InvoiceNumber:
                    return "Invoice number";
                case PostListViewColumn.InvoiceSender:
                    return "Invoice checker";
                case PostListViewColumn.InvoiceStatus:
                    return "Invoice status";
                case PostListViewColumn.OrderSign:
                    return "Order sign";
                case PostListViewColumn.Product:
                    return "Product";
                case PostListViewColumn.PurchaseSalesOrderNo:
                    return "PO + SO";
                case PostListViewColumn.PlaceOfPurchase:
                    return "Group";
                default:
                    throw new DataException("Unknown enum type: " + _colEnum);
            }
        }

        public string GetString(Data.PostData.Post post)
        {
            switch (_colEnum)
            {
                case PostListViewColumn.Account:
                    return post.GetAccountString();
                case PostListViewColumn.Periodization:
                    return post.GetPeriodizationString();
                case PostListViewColumn.VangenSummary:
                    return post.GetVangenSummaryString();
                case PostListViewColumn.Amount:
                    return post.GetAmountString();
                case PostListViewColumn.InvoiceCategoryCode:
                    return post.GetInvoiceCategoryCodeString2();
                case PostListViewColumn.ApprArrival:
                    return post.GetPredictedArrival();
                case PostListViewColumn.ArrivalDate:
                    return post.GetArrivalDateString();
                case PostListViewColumn.BookDate:
                    return post.GetBookDate();
                case PostListViewColumn.InvoiceDate:
                    return post.GetInvoiceDateString();
                case PostListViewColumn.OrderDate:
                    return post.GetOrderDateString();
                case PostListViewColumn.ApprPrize:
                    return post.GetPriceWithCurrencyString();
                case PostListViewColumn.FinalPrize:
                    return post.GetFinalPrizeWithCurrencyString();
                case PostListViewColumn.TotalPrize:
                    return post.GetTotalPrizeWithCurrencyString();
                case PostListViewColumn.ArrivalSign:
                    return post.GetArrivalSignUserName();
                case PostListViewColumn.ArtNr:
                    return post.GetArticleNumberString();
                case PostListViewColumn.Booker:
                    return post.GetBookerName();
                case PostListViewColumn.Comment:
                    return post.GetCommentForListView();
                case PostListViewColumn.DeliveryDeviation:
                    return post.GetDeliveryDeviation();
                case PostListViewColumn.InvoiceClin:
                    return post.GetInvoiceClinString();
                case PostListViewColumn.InvoiceInst:
                    return post.GetInvoiceInstString();
                case PostListViewColumn.InvoiceNumber:
                    return post.GetInvoiceNumber();
                case PostListViewColumn.InvoiceSender:
                    return post.GetInvoicerUserName();
                case PostListViewColumn.InvoiceStatus:
                    return post.GetInvoiceStatusString();
                case PostListViewColumn.OrderSign:
                    return post.GetOrdererName();
                case PostListViewColumn.Product:
                    return post.GetMerchandiseName2();
                case PostListViewColumn.PurchaseSalesOrderNo:
                    return post.GetPurchaseAndSalesOrderNo();
                case PostListViewColumn.Supplier:
                    return post.GetSupplierName2();
                case PostListViewColumn.PlaceOfPurchase:
                    return post.GetPlaceOfPurchaseString();
                default:
                    throw new DataException("Unknown post list view column: " + _colEnum);
            }
        }

    }
}
