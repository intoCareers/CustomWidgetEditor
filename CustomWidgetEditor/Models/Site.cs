namespace CustomWidgetEditor.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Site
    {
        public int SiteID { get; set; }

        public int OperatorID { get; set; }

        [Required]
        [StringLength(100)]
        public string SiteName { get; set; }

        public int SiteType { get; set; }

        public short Status { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime UsersExpire { get; set; }

        [StringLength(100)]
        public string ContactName { get; set; }

        [StringLength(50)]
        public string ContactEMail { get; set; }

        [StringLength(50)]
        public string SiteAddress1 { get; set; }

        [StringLength(50)]
        public string SiteAddress2 { get; set; }

        [StringLength(50)]
        public string SiteCity { get; set; }

        [StringLength(30)]
        public string SiteCounty { get; set; }

        [StringLength(50)]
        public string SiteState { get; set; }

        [StringLength(50)]
        public string SiteZip { get; set; }

        [StringLength(50)]
        public string SitePhoneNumber { get; set; }

        public int? LinkedSiteID { get; set; }

        [StringLength(20)]
        public string ReferenceID { get; set; }

        public short AllowSetPrivacyOptions { get; set; }

        public short PrivacyOptionFlag { get; set; }

        public short IdeasFlag { get; set; }

        public DateTime IdeasExpires { get; set; }

        public short SkillsFlag { get; set; }

        public short DependableStrengthsFlag { get; set; }

        public DateTime DependableStrengthsExpires { get; set; }

        public short AcadTestPrepFlag { get; set; }

        public DateTime AcadTestPrepExpires { get; set; }

        public short Acad2TestPrepFlag { get; set; }

        public DateTime Acad2TestPrepExpires { get; set; }

        public short CivServTestPrepFlag { get; set; }

        public DateTime CivServTestPrepExpires { get; set; }

        [StringLength(10)]
        public string PetSiteID { get; set; }

        public short CISConnFlag { get; set; }

        [StringLength(10)]
        public string CISConnSchoolID { get; set; }

        [StringLength(25)]
        public string CISConnOrganizations { get; set; }

        [StringLength(256)]
        public string Memo { get; set; }

        public int LoginCounter { get; set; }

        [StringLength(150)]
        public string CoursePlanLink { get; set; }

        [StringLength(150)]
        public string CoursePlanLinkDesc { get; set; }

        public DateTime UEPExpires { get; set; }

        [StringLength(20)]
        public string UEPAuthCode { get; set; }

        public short? UEPFormType { get; set; }

        public DateTime SsiExpires { get; set; }

        [Required]
        [StringLength(100)]
        public string SsiGradYears { get; set; }

        public DateTime ILGreenExpires { get; set; }

        public DateTime LearningExpressExpires { get; set; }

        public DateTime MicroCareerBurstExpires { get; set; }

        public DateTime CPSTExpires { get; set; }

        public DateTime PREPExpires { get; set; }

        [StringLength(20)]
        public string PREPAuthCode { get; set; }

        public virtual Operator Operator { get; set; }
    }
}
