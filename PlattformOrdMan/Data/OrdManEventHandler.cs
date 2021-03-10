using PlattformOrdMan.Data.PostData;

namespace PlattformOrdMan.Data
{
    public class OrdManEventHandler : PlattformOrdManData
    {
        public delegate void SupplierUpdateReporter(Supplier supplier);
        public delegate void SupplierCreatedReporter(Supplier supplier);
        public delegate void MerchandiseUpdateReporter(Merchandise merchandise);
        public delegate void MerchandiseCreatedReporter(Merchandise merchandise);
        public delegate void PostUpdateReporter(Post post);
        public delegate void PostCreatedReporter(Post post);
        public delegate void ViewingOptionsEvent();

        public event SupplierUpdateReporter MyOnSupplierUpdate;
        public event SupplierCreatedReporter MyOnSupplierCreate;
        public event MerchandiseUpdateReporter MyOnMerchandiseUpdate;
        public event MerchandiseCreatedReporter MyOnMerchandiseCreate;
        public event PostUpdateReporter MyOnPostUpdate;
        public event PostCreatedReporter MyOnPostCreate;
        public event ViewingOptionsEvent OnViewingOptionsChanged;

        public OrdManEventHandler()
        { 
        
        }

        public void FirePostCreated(Post post)
        {
            if (IsNotNull(MyOnPostCreate))
            {
                MyOnPostCreate(post);
            }
        }

        public void FirePostUpdate(Post post)
        {
            if (IsNotNull(MyOnPostUpdate))
            {
                MyOnPostUpdate(post);
            }
        }

        public void FireViewingOptionsChanged()
        {
            OnViewingOptionsChanged?.Invoke();
        }

        public void FireSupplierCreate(Supplier supplier)
        {
            if (IsNotNull(MyOnSupplierCreate))
            {
                MyOnSupplierCreate(supplier);
            }
        }

        public void FireSupplierUpdate(Supplier supplier)
        {
            if (IsNotNull(MyOnSupplierUpdate))
            {
                MyOnSupplierUpdate(supplier);
            }
        }

        public void FireMerchandiseCreate(Merchandise merchandise)
        {
            if (IsNotNull(MyOnMerchandiseCreate))
            {
                MyOnMerchandiseCreate(merchandise);
            }
        }

        public void FireMerchandiseUpdate(Merchandise merchandise)
        {
            if (IsNotNull(MyOnMerchandiseUpdate))
            {
                MyOnMerchandiseUpdate(merchandise);
            }
        }
    }
}
