using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Molmed.PlattformOrdMan.Database;


namespace Molmed.PlattformOrdMan.Data
{
    public class Currency : PlattformOrdManData
    {
        private String MyDescription;
        private String MySymbol;
        private String MyCode;
        private Int32 MyId;

        public Currency(DataReader dataReader)
        {
            MyDescription = dataReader.GetString(CurrencyData.IDENTIFIER);
            MySymbol = dataReader.GetString(CurrencyData.SYMBOL);
            MyCode = dataReader.GetString(CurrencyData.CURRENCY_CODE);
            MyId = dataReader.GetInt32(CurrencyData.CURRENCY_ID);
        }

        public String GetDescription()
        {
            return MyDescription;
        }

        public String GetSymbol()
        {
            return MySymbol;
        }

        public String GetCode()
        {
            return MyCode;
        }

        public override String ToString()
        {
            return GetIdentityString();
        }

        public String GetIdentityString()
        {
            if (IsNotNull(MyDescription))
            {
                return MyDescription;
            }
            return MyCode;
        }

        //private decimal ParsePrice(String priceString)
        //{
        //    int firstInd = -1, lastInd = -1;
        //    string decimalSymbol = PlattformOrdManData.MyCultureInfo.NumberFormat.NumberDecimalSeparator;
        //    // Extract the numeric part of str
        //    NumberStyles style = NumberStyles.Float;
        //    priceString.Replace(".", decimalSymbol);
        //    priceString.Replace(",", decimalSymbol);
        //    decimal price;
        //    // Find indices for numeric characters
        //    // Extract the numeric sequence and parse is to decimal
        //    for (int i = 0; i < priceString.Length; i++)
        //    {
        //        if (firstInd == -1 && (char.IsDigit(priceString[i]) || priceString[i] == '.' || priceString[i] == ','))
        //        {
        //            firstInd = i;
        //        }
        //        if (char.IsDigit(priceString[i]) || priceString[i] == '.' || priceString[i] == ',')
        //        {
        //            lastInd = i;
        //        }
        //    }
        //    if (firstInd > -1 && lastInd > -1 && 
        //        decimal.TryParse(priceString.Substring(firstInd, lastInd - firstInd + 1), style, 
        //        PlattformOrdManData.MyCultureInfo, out price))
        //    {
        //        return price;
        //    }
        //    return -1;
        //}

        public String GetPriceWithCurrencyString(String str)
        { 
            decimal price;
            price = ParsePrice(str);
            return GetPriceWithCurrencyString(price);
        }

        public String GetPriceWithCurrencyString(decimal price)
        {
            String priceStr = TruncDecimals(price.ToString(PlattformOrdManData.MyCultureInfo));
            if (price == PlattformOrdManData.NO_COUNT)
            {
                return "";
            }
            if (IsNotNull(MySymbol) && MySymbol.Length > 0)
            {
                if (MySymbol.Length == 1 && !char.IsLetter(MySymbol[0]))
                {
                    return MySymbol.Trim() + priceStr;
                }
                else
                {
                    return priceStr + " " + MySymbol.Trim();
                }
            }
            else if (IsNotNull(MyCode))
            {
                return priceStr + " " + MyCode;
            }
            return priceStr;
        }

        private String TruncDecimals(String str)
        {
            int residue = 0;
            string decimalDelimiter;
            decimalDelimiter = PlattformOrdManData.MyCultureInfo.NumberFormat.NumberDecimalSeparator;
            if (str.IndexOf(decimalDelimiter) > -1)
            {
                if (str.Substring(str.IndexOf(decimalDelimiter)).Contains("0000"))
                {
                    return str.Substring(0, str.IndexOf(decimalDelimiter));
                }
                residue = Math.Min(3, str.Length - str.IndexOf(decimalDelimiter));
                return str.Substring(0, str.IndexOf(decimalDelimiter) + residue);
            }
            return str;
        }

        public static Int32 GetDescriptionMaxLength()
        {
            return GetColumnLength(CurrencyData.TABLE, CurrencyData.IDENTIFIER);
        }

        public static Int32 GetSymbolMaxLength()
        {
            return GetColumnLength(CurrencyData.TABLE, CurrencyData.SYMBOL);
        }

        public static Int32 GetCurrencyCodeMaxLength()
        {
            return GetColumnLength(CurrencyData.TABLE, CurrencyData.CURRENCY_CODE);
        }

        public Int32 GetId()
        {
            return MyId;
        }

        public bool HasCurrencyCode()
        {
            return IsNotNull(MyCode);
        }

        public void Set()
        {
            Database.UpdateCurrency(GetId(), GetDescription(), GetSymbol(), GetCode());
        }

        public void SetDescription(String description)
        {
            MyDescription = description;
        }

        public void SetCurrencyCode(String code)
        {
            MyCode = code;
        }

        public void SetSymbol(String symbol)
        {
            MySymbol = symbol;
        }
    }
    public class CurrencyList : ArrayList
    {
        public override int Add(object value)
        {
            if (value != null)
            {
                return base.Add(value);
            }
            return -1;
        }

        public override void AddRange(ICollection collection)
        {
            if (collection != null)
            {
                base.AddRange(collection);
            }
        }

        public new Currency this[Int32 index]
        {
            get
            {
                return (Currency)(base[index]);
            }
            set
            {
                base[index] = value;
            }
        }
        public Currency GetById(Int32 id)
        {
            foreach (Currency currency in this)
            {
                if (currency.GetId() == id)
                {
                    return currency;
                }
            }
            return null;
        }

        public Int32 GetIndex(Currency currency)
        {
            Int32 index;

            for (index = 0; index < this.Count; index++)
            {
                if ((((Currency)(this[index])).GetId() == currency.GetId()))
                {
                    return index;
                }
            }
            return -1;
        }

        public Boolean IsMember(Currency currency)
        {
            return (this.GetIndex(currency) >= 0);
        }

        public Boolean IsNotMember(Currency currency)
        {
            return (this.GetIndex(currency) < 0);
        }

        public void Remove(Currency currency)
        {
            Int32 index;

            index = this.GetIndex(currency);
            if (index >= 0)
            {
                this.RemoveAt(index);
            }
        }

    }
}
