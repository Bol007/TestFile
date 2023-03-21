namespace TestFile1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public  class CBS_LN_APP
    {
        [Key]
        [StringLength(22)]
        public string CBS_APP_NO { get; set; }

        [StringLength(3)]
        public string CBS_ISIC1_CD { get; set; }

        [StringLength(10)]
        public string CBS_ISIC2_CD { get; set; }

        [StringLength(3)]
        public string CBS_ISIC3_CD { get; set; }

        public decimal? CBS_MPAYAMT { get; set; }

        [StringLength(200)]
        public string CBS_CA_NO { get; set; }

        [StringLength(5)]
        public string CBS_COMMITTEE { get; set; }

        [StringLength(2)]
        public string CBS_APPROVE_CD { get; set; }

        [StringLength(5)]
        public string CBS_REASON_CD { get; set; }

        [StringLength(100)]
        public string CBS_APPROVE_COMMENT { get; set; }

        [StringLength(10)]
        public string CBS_MATURITYDATE { get; set; }

        [StringLength(5)]
        public string CBS_COL { get; set; }

        [StringLength(5)]
        public string CBS_PAYSTATUS { get; set; }

        [StringLength(50)]
        public string BATCH_UPDATE_DTM { get; set; }

        [StringLength(50)]
        public string CBS_CA_DATE { get; set; }
    }
}
