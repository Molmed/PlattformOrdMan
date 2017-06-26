using System;
using System.Collections.Generic;
using System.Text;
using Molmed.PlattformOrdMan.Database;
using Molmed.PlattformOrdMan.Data.Exception;
using PlattformOrdMan.Properties;


namespace Molmed.PlattformOrdMan.Data
{
    public class CurrencyManager : PlattformOrdManData
    {
        private static CurrencyList MyCurrencies = null;
        private static Currency MyDefaultCurrency = null;

        public CurrencyManager()
            : base()
        { 
        }

        public static Currency CreateCurrency(String description, String symbol, String currencyCode)
        {
            DataReader dataReader = null;
            Currency currency = null;

            // Check parameters.
            if (IsNotNull(description))
            {
                CheckLength(description, "description", Currency.GetDescriptionMaxLength());
            }
            if (IsNotNull(symbol))
            {
                CheckLength(symbol, "symbol", Currency.GetSymbolMaxLength());            
            }
            if (IsNotNull(currencyCode))
            {
                CheckLength(currencyCode, "currency_code", Currency.GetCurrencyCodeMaxLength());                            
            }

            try
            {
                // Create currency in database.
                dataReader = Database.CreateCurrency(description, symbol, currencyCode);
                if (dataReader.Read())
                {
                    currency = new Currency(dataReader);
                }
                else
                {
                    throw new DataException("Failed to create currency ");
                }
            }
            finally
            {
                CloseDataReader(dataReader);
            }

            // Add the user to in-memory list.
            if (MyCurrencies != null)
            {
                MyCurrencies.Add(currency);
            }

            return currency; 
        }

        public static bool DeleteCurrencies(CurrencyList deleteCurrencies)
        {
            Database.BeginTransaction();
            try
            {
                foreach (Currency currency in deleteCurrencies)
                {
                    Database.DeleteCurrency(currency.GetId());
                }
                Database.CommitTransaction();
                return true;
            }
            catch
            {
                Database.RollbackTransaction();
                return false;
            }            
        }

        public static Currency GetCurrency(int currencyId)
        {
            LoadCurrencies();
            return MyCurrencies.GetById(currencyId);
        }

        public static CurrencyList GetCurrencies()
        {
            LoadCurrencies();
            return MyCurrencies;
        }

        public static Currency GetDefaultCurrency()
        {
            LoadCurrencies();
            return MyDefaultCurrency;
        }

        private static void LoadCurrencies()
        {
            DataReader dataReader = null;

            if (IsNull(MyCurrencies))
            {
                try
                {
                    // Get information about all currencies from database.
                    dataReader = Database.GetCurrencies();
                    MyCurrencies = new CurrencyList();
                    while (dataReader.Read())
                    {
                        MyCurrencies.Add(new Currency(dataReader));
                    }
                    dataReader.Close();

                    // Get default currency
                    foreach (Currency currency in MyCurrencies)
                    {
                        if (currency.HasCurrencyCode() && currency.GetCode() == Settings.Default.DefaultCurrency)
                        {
                            MyDefaultCurrency = currency;
                            break;
                        }
                    }
                    if(IsNull(MyDefaultCurrency))
                    {
                        throw new DataException("Could not find default currency!");
                    }
                }
                catch
                {
                    MyCurrencies = null;
                    MyDefaultCurrency = null;
                    throw;
                }
                finally
                {
                    CloseDataReader(dataReader);
                }
            }
        }

    }
}
