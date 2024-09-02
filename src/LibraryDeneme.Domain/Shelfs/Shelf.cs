using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using LibraryDeneme.Books;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Emailing;
using Volo.Abp.Uow;

namespace LibraryDeneme.Shelfs
{
    public class Shelf : FullAuditedAggregateRoot<Guid>
    {
        public string ShelfName { get; set; }

        public SType ShelfType { get; set; }

        public BolumType ShelfBolum { get; set; }

        private Shelf()
        {

        }
        internal Shelf(
            Guid id,
            string shelfname,
            SType shelftype,
            BolumType shelfbolum)
            : base(id)
        {
            SetName(shelfname);
            ShelfType = shelftype;
            ShelfBolum = shelfbolum;

        }
        internal Shelf ChangeName(string shelfname)
        {
            SetName(shelfname);
            return this;
        }
        private void SetName(string shelfname)
        {
            ShelfName = Check.NotNullOrWhiteSpace(
                shelfname,
                nameof(shelfname),
                maxLength: ShelfConsts.MaxShelfNameLength);
        }
    }
    
}
