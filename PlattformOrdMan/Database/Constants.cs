using System;
using PlattformOrdMan.Data.Conf;

namespace PlattformOrdMan.Database
{
    public struct DataIdentifierData
    {
        public const String IDENTIFIER = "identifier";
    }

    public struct DataIdentityData
    {
        public const String ID = "id";
    }

    public struct DataCommentData
    {
        public const String COMMENT = "comment";
    }

    public struct ArticleNumberData
    {
        public const String ARTICLE_NUMBER_ID = "id";
        public const String IDENTIFIER = "identifier";
        public const String MERCHANDISE_ID = "merchandise_id";
        public const String ACTIVE = "active";
        public const String ARTICLE_NUMBER_PREFIX = "article_number_";
    }

    public struct CurrencyData
    {
        public const String IDENTIFIER = "identifier";
        public const String CURRENCY_CODE = "currency_code";
        public const String CURRENCY_ID = "currency_id";
        public const String SYMBOL = "symbol";
        public const String TABLE = "currency";
    }

    public struct CustomerNumberData
    {
        public const String IDENTIFIER = "identifier";
        public const String DESCRIPTION = "description";
        public const String CUSTOMER_NUMBER_ID = "id";
        public const String PLACE_OF_PURCHASE = "place_of_purchase";
        public const String SUPPLIER_ID = "supplier_id";
        public const String TABLE = "customer_number";
        public const String ENABLED = "enabled";
    }

    public struct InvoiceCategoryData
    {
        public const String NUMBER = "number";
        public const String IDENTIFIER = "identifier";
        public const String INVOICE_CATEGORY_ID = "id";
    }

    public struct LoginData
    {
        public const String CHIASMA_BARCODE = "chiasma_barcode";
    }

    public struct LogRowData
    {
        public const String DATE_TIME = "date_time";
        public const String ID = "id";
        public const String IDENTIFIER = "identifier";
        public const String OPERATION = "operation";
        public const String USER_ID = "authority_id";
    }

    public struct PlaceOfPurchaseData
    {
        public const string PLACE_OF_PURCHASE_ID = "place_of_purchase_id";
        public const string CODE = "code";
    }

    public struct PostData
    {
        public const String POST_ID = "id";
        public const String ARTICLE_NUMBER_ID = "article_number_id";
        public const String COMMENT = "comment";
        public const String AUTHORITY_ID_BOOKER = "authority_id_booker";
        public const String BOOK_DATE = "book_date";
        public const String MERCHANDISE_ID = "merchandise_id";
        public const String SUPPLIER_ID = "supplier_id";
        public const String APPR_PRIZE = "appr_prize";
        public const String ORDER_DATE = "order_date";
        public const String AUTHORITY_ID_ORDERER = "authority_id_orderer";
        public const String PREDICTED_ARRIVAL = "predicted_arrival";
        public const String INVOICE_INST = "invoice_inst";
        public const String INVOICE_CLIN = "invoice_clin";
        public const String ARRIVAL_DATE = "arrival_date";
        public const String ARRIVAL_SIGN = "arrival_sign";
        public const String AMOUNT = "amount";
        public const String INVOICE_STATUS = "invoice_status";
        public const String INVOICE_ABSENT = "invoice_absent";
        public const String AUTHORITY_ID_INVOICER = "authority_id_invoicer";
        public const String INVOICE_DATE = "invoice_date";
        public const String CURRENCY_ID = "currency_id";
        public const String BOOKER_FROM_DATE = "booker_from_date";
        public const String TIME_RESTRICTION_TO_COMPLETED_POSTS = "time_restriction_to_completed_posts";
        public const String ARTICLE_NUMBER_PREFIX = "article_number_";
        public const String INVOICE_NUMBER = "invoice_number";
        public const String FINAL_PRIZE = "final_prize";
        public const String AUTHORITY_ID_CONFIRMED_ORDER = "authority_id_confirmed_order";
        public const String CONFIRMED_ORDER_DATE = "confirmed_order_date";
        public const String DELIVERY_DEVIATION = "delivery_deviation";
        public const String PURCHASE_ORDER_NO = "purchase_order_no";
        public const String SALES_ORDER_NO = "sales_order_no";
        public const String PLACE_OF_PURCHASE = "place_of_purchase";
        public const String CUSTOMER_NUMBER_ID = "customer_number_id";
        public const String MERCHANDISE_IDENTIFIER = "merchandise_identifier";
        public const String MERCHANDISE_AMOUNT = "merchandise_amount";
        public const String MERCHANDISE_ENABLED = "merchandise_enabled";
        public const String MERCHANDISE_COMMENT = "merchandise_comment";
        public const String SUPPLIER_IDENTIFIER = "supplier_identifier";
        public const String INVOICE_CATEGORY_NUMBER = "invoice_category_number";
        public const String ATTENTION_FLAG = "attention_flag";
        public const String PERIODIZATION = "periodization";
        public const String HAS_PERIODIZATION = "has_periodization";
        public const String PERIODIZATION_ANSWERED = "periodization_answered";
        public const String ACCOUNT = "account";
        public const String HAS_ACCOUNT = "has_account";
        public const String ACCOUNT_ANSWERED = "account_answered";
    }

    public struct MerchandiseData
    {
        public const String MERCHANDISE_ID = "id";
        public const String SUPPLIER_ID = "supplier_id";
        public const String ENABLED = "enabled";
        public const String AMOUNT = "amount";
        public const String APPR_PRIZE = "appr_prize";
        public const String STORAGE = "storage";
        public const String ARTICLE_NUMBER_ID = "article_number_id";
        public const String ARTICLE_NUMBER = "article_number";
        public const String CATEGORY = "category";
        public const String INVOICE_CATEGORY_ID = "invoice_category_id";
        public const String CURRENCY_ID = "currency_id";
        public const String SUPPLIER_IDENTIFIER = "supplier_identifier";
    }

    public struct ConfigurationData
    {
        public const String SHOW_ENABLED_PRODUCTS_ONLY = "shown_enabled_products_only";
        public const bool DEFAULT_SHOW_ENABLED_PRODUCTS_ONLY = false;
        public const String TIME_INTERVAL_FOR_POSTS = "time_interval_for_posts";
        public const int DEFAULT_TIME_INTERVAL_FOR_POSTS = 3;
        public const bool DEFAULT_TIME_RESTRICTION_FOR_COMPLETED_POSTS_ONLY = false;
        public const string TIME_RESTRICTION_FOR_COMPLETED_POSTS_ONLY = "time_restriction_for_completed_posts_only";
        public const string PLACE_OF_PURCHASE = "place_of_purchase";
        public const string EDIT_POST_TAB = "edit_post_tab";
        public const EditPostTab DEFAULT_EDIT_POST_TAB = EditPostTab.Delivery;
    }

    public struct SupplierData
    {
        public const String SUPPLIER_ID = "id";
        public const String IDENTIFIER = "identifier";
        public const String SHORT_NAME = "short_name";
        public const String ENABLED = "enabled";
        public const String TEL_NR  = "tel_nr";
        public const String CUSTOMER_NR_INST = "customer_nr_inst";
        public const String CUSTOMER_NR_CLIN = "customer_nr_clin";
        public const String CONTRACT_TERMINATE = "contract_terminate";
    }

    public struct TimeIntervalsForPostsData
    {
        public const String DESCRIPTION = "description";
        public const String MONTHS = "months";
    }

    public struct UserData
    {
        public const String ACCOUNT_STATUS = "account_status";
        public const String COMMENT = "comment";
        public const String IDENTIFIER = "identifier";
        public const String NAME = "name";
        public const String TABLE = "authority";
        public const String USER_ID = "id";
        public const String USER_TYPE = "user_type";
        public const String PLACE_OF_PURCHASE = "place_of_purchase";
    }

}
