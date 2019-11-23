using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace S3Test.Models
{
    public partial class phoenixgstContext : DbContext
    {
        public phoenixgstContext()
        {
        }

        public phoenixgstContext(DbContextOptions<phoenixgstContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Assessee> Assessee { get; set; }
        public virtual DbSet<Branchmaster> Branchmaster { get; set; }
        public virtual DbSet<Monthmain> Monthmain { get; set; }
        public virtual DbSet<Monthyear> Monthyear { get; set; }
        public virtual DbSet<Partymaster> Partymaster { get; set; }
        public virtual DbSet<Trananx> Trananx { get; set; }
        public virtual DbSet<Trananxdet> Trananxdet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=reladmin@123;Database=phoenixgst");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Assessee>(entity =>
            {
                entity.ToTable("assessee", "gst2019");

                entity.Property(e => e.AssesseeId)
                    .HasColumnName("assessee_id")
                    .HasDefaultValueSql("nextval('gst2019.assessee_id_seq'::regclass)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.AltMobile)
                    .HasColumnName("alt_mobile")
                    .HasMaxLength(11);

                entity.Property(e => e.BusinessType).HasColumnName("business_type");

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.Designation)
                    .HasColumnName("designation")
                    .HasMaxLength(50);

                entity.Property(e => e.EmailId)
                    .HasColumnName("email_id")
                    .HasMaxLength(30);

                entity.Property(e => e.EntityId).HasColumnName("entity_id");

                entity.Property(e => e.FinYear).HasColumnName("fin_year");

                entity.Property(e => e.Gstin)
                    .IsRequired()
                    .HasColumnName("gstin")
                    .HasMaxLength(15);

                entity.Property(e => e.IsSez).HasColumnName("is_sez");

                entity.Property(e => e.LegalName)
                    .IsRequired()
                    .HasColumnName("legal_name")
                    .HasMaxLength(100);

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(10);

                entity.Property(e => e.Pan)
                    .HasColumnName("pan")
                    .HasMaxLength(10);

                entity.Property(e => e.PinCode)
                    .HasColumnName("pin_code")
                    .HasMaxLength(6);

                entity.Property(e => e.Place)
                    .HasColumnName("place")
                    .HasMaxLength(100);

                entity.Property(e => e.RegDate)
                    .HasColumnName("reg_date")
                    .HasColumnType("date");

                entity.Property(e => e.ResponsiblePerson)
                    .HasColumnName("responsible_person")
                    .HasMaxLength(100);

                entity.Property(e => e.StateCode).HasColumnName("state_code");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Tan)
                    .HasColumnName("tan")
                    .HasMaxLength(10);

                entity.Property(e => e.TradeName)
                    .HasColumnName("trade_name")
                    .HasMaxLength(100);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(20);

                entity.Property(e => e.UserPassword)
                    .HasColumnName("user_password")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Branchmaster>(entity =>
            {
                entity.HasKey(e => e.BranchId)
                    .HasName("branchmaster_pkey");

                entity.ToTable("branchmaster", "gst2019");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasDefaultValueSql("nextval('gst2019.branch_id_seq'::regclass)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.BranchName)
                    .HasColumnName("branch_name")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Monthmain>(entity =>
            {
                entity.HasKey(e => e.TranId)
                    .HasName("monthmain_pkey");

                entity.ToTable("monthmain", "gst2019");

                entity.Property(e => e.TranId)
                    .HasColumnName("tran_id")
                    .HasDefaultValueSql("nextval('gst2019.tran_id_seq'::regclass)");

                entity.Property(e => e.AckDate)
                    .HasColumnName("ack_date")
                    .HasColumnType("date");

                entity.Property(e => e.AckNum)
                    .HasColumnName("ack_num")
                    .HasMaxLength(20);

                entity.Property(e => e.CheckedBy)
                    .HasColumnName("checked_by")
                    .HasMaxLength(100);

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.Compared).HasColumnName("compared");

                entity.Property(e => e.EntryType).HasColumnName("entry_type");

                entity.Property(e => e.GstrNum).HasColumnName("gstr_num");

                entity.Property(e => e.MonthId).HasColumnName("month_id");

                entity.Property(e => e.PreparedBy)
                    .HasColumnName("prepared_by")
                    .HasMaxLength(100);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Submit).HasColumnName("submit");

                entity.Property(e => e.Uploaded).HasColumnName("uploaded");

                entity.Property(e => e.UploadedBy)
                    .HasColumnName("uploaded_by")
                    .HasMaxLength(100);

                entity.HasOne(d => d.Month)
                    .WithMany(p => p.Monthmain)
                    .HasPrincipalKey(p => p.MonthId)
                    .HasForeignKey(d => d.MonthId)
                    .HasConstraintName("monthmain_month_id_fkey");
            });

            modelBuilder.Entity<Monthyear>(entity =>
            {
                entity.ToTable("monthyear", "gst2019");

                entity.HasIndex(e => e.MonthId)
                    .HasName("monthyear_month_id_key")
                    .IsUnique();

                entity.Property(e => e.MonthyearId)
                    .HasColumnName("monthyear_id")
                    .HasDefaultValueSql("nextval('gst2019.monthyear_id_seq'::regclass)");

                entity.Property(e => e.MonthId).HasColumnName("month_id");

                entity.Property(e => e.Quarterly).HasColumnName("quarterly");

                entity.Property(e => e.RtnPeriod).HasColumnName("rtn_period");
            });

            modelBuilder.Entity<Partymaster>(entity =>
            {
                entity.HasKey(e => e.PartyId)
                    .HasName("partymaster_pkey");

                entity.ToTable("partymaster", "gst2019");

                entity.Property(e => e.PartyId)
                    .HasColumnName("party_id")
                    .HasDefaultValueSql("nextval('gst2019.party_id_seq'::regclass)");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255);

                entity.Property(e => e.ClientId).HasColumnName("client_id");

                entity.Property(e => e.EmailId)
                    .HasColumnName("email_id")
                    .HasMaxLength(30);

                entity.Property(e => e.EntityId).HasColumnName("entity_id");

                entity.Property(e => e.Gstin)
                    .HasColumnName("gstin")
                    .HasMaxLength(15);

                entity.Property(e => e.IsSez).HasColumnName("is_sez");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(10);

                entity.Property(e => e.Pan)
                    .HasColumnName("pan")
                    .HasMaxLength(10);

                entity.Property(e => e.PartyName)
                    .HasColumnName("party_name")
                    .HasMaxLength(100);

                entity.Property(e => e.PartyType).HasColumnName("party_type");

                entity.Property(e => e.Place)
                    .HasColumnName("place")
                    .HasMaxLength(100);

                entity.Property(e => e.StateCode).HasColumnName("state_code");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SupplyType).HasColumnName("supply_type");

                entity.Property(e => e.Tan)
                    .HasColumnName("tan")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Trananx>(entity =>
            {
                entity.HasKey(e => e.AnxId)
                    .HasName("trananx_pkey");

                entity.ToTable("trananx", "gst2019");

                entity.Property(e => e.AnxId)
                    .HasColumnName("anx_id")
                    .HasDefaultValueSql("nextval('gst2019.anx_id_seq'::regclass)");

                entity.Property(e => e.ActionFlag)
                    .HasColumnName("action_flag")
                    .HasMaxLength(30);

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.Checksum)
                    .HasColumnName("checksum")
                    .HasMaxLength(100);

                entity.Property(e => e.ClaimRfnd).HasColumnName("claim_rfnd");

                entity.Property(e => e.CompareType).HasColumnName("compare_type");

                entity.Property(e => e.DiffRate)
                    .HasColumnName("diff_rate")
                    .HasColumnType("numeric");

                entity.Property(e => e.DocValue)
                    .HasColumnName("doc_value")
                    .HasColumnType("numeric");

                entity.Property(e => e.ExportType).HasColumnName("export_type");

                entity.Property(e => e.FinYear).HasColumnName("fin_year");

                entity.Property(e => e.Flag)
                    .HasColumnName("flag")
                    .HasMaxLength(30);

                entity.Property(e => e.IgstAct).HasColumnName("igst_act");

                entity.Property(e => e.IsAmended).HasColumnName("is_amended");

                entity.Property(e => e.IsRefund).HasColumnName("is_refund");

                entity.Property(e => e.ItcEnteredBlc)
                    .HasColumnName("itc_entered_blc")
                    .HasMaxLength(10);

                entity.Property(e => e.ItcEntitle)
                    .HasColumnName("itc_entitle")
                    .HasMaxLength(50);

                entity.Property(e => e.MonthId).HasColumnName("month_id");

                entity.Property(e => e.OrgDocDate)
                    .HasColumnName("org_doc_date")
                    .HasColumnType("date");

                entity.Property(e => e.OrgDocNum)
                    .HasColumnName("org_doc_num")
                    .HasMaxLength(50);

                entity.Property(e => e.OrgDocType).HasColumnName("org_doc_type");

                entity.Property(e => e.OrgGstin)
                    .HasColumnName("org_gstin")
                    .HasMaxLength(15);

                entity.Property(e => e.OrgTradeName)
                    .HasColumnName("org_trade_name")
                    .HasMaxLength(100);

                entity.Property(e => e.PartyId).HasColumnName("party_id");

                entity.Property(e => e.PortCode)
                    .HasColumnName("port_code")
                    .HasMaxLength(100);

                entity.Property(e => e.Pos).HasColumnName("pos");

                entity.Property(e => e.RefndElg).HasColumnName("refnd_elg");

                entity.Property(e => e.RelChecksum)
                    .HasColumnName("rel_checksum")
                    .HasMaxLength(100);

                entity.Property(e => e.RevisedDocDate)
                    .HasColumnName("revised_doc_date")
                    .HasColumnType("date");

                entity.Property(e => e.RevisedDocNum)
                    .HasColumnName("revised_doc_num")
                    .HasMaxLength(50);

                entity.Property(e => e.RevisedDocType).HasColumnName("revised_doc_type");

                entity.Property(e => e.RevisedGstin)
                    .HasColumnName("revised_gstin")
                    .HasMaxLength(15);

                entity.Property(e => e.RevisedTradeName)
                    .HasColumnName("revised_trade_name")
                    .HasMaxLength(100);

                entity.Property(e => e.SectionId).HasColumnName("section_id");

                entity.Property(e => e.ShippingDate)
                    .HasColumnName("shipping_date")
                    .HasMaxLength(50);

                entity.Property(e => e.ShippingNum)
                    .HasColumnName("shipping_num")
                    .HasMaxLength(50);

                entity.Property(e => e.SupplyType).HasColumnName("supply_type");

                entity.Property(e => e.TableRef)
                    .HasColumnName("table_ref")
                    .HasMaxLength(10);

                entity.Property(e => e.TranId).HasColumnName("tran_id");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Trananx)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("trananx_branch_id_fkey");

                entity.HasOne(d => d.Party)
                    .WithMany(p => p.Trananx)
                    .HasForeignKey(d => d.PartyId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("trananx_party_id_fkey");

                entity.HasOne(d => d.Tran)
                    .WithMany(p => p.Trananx)
                    .HasForeignKey(d => d.TranId)
                    .HasConstraintName("trananx_tran_id_fkey");
            });

            modelBuilder.Entity<Trananxdet>(entity =>
            {
                entity.HasKey(e => e.DetId)
                    .HasName("trananxdet_pkey");

                entity.ToTable("trananxdet", "gst2019");

                entity.Property(e => e.DetId)
                    .HasColumnName("det_id")
                    .HasDefaultValueSql("nextval('gst2019.det_id_seq'::regclass)");

                entity.Property(e => e.AnxId).HasColumnName("anx_id");

                entity.Property(e => e.Cess)
                    .HasColumnName("cess")
                    .HasColumnType("numeric");

                entity.Property(e => e.Cgst)
                    .HasColumnName("cgst")
                    .HasColumnType("numeric");

                entity.Property(e => e.Eligibility).HasColumnName("eligibility");

                entity.Property(e => e.Hsnsac)
                    .HasColumnName("hsnsac")
                    .HasMaxLength(10);

                entity.Property(e => e.Igst)
                    .HasColumnName("igst")
                    .HasColumnType("numeric");

                entity.Property(e => e.Rate)
                    .HasColumnName("rate")
                    .HasColumnType("numeric");

                entity.Property(e => e.Sgst)
                    .HasColumnName("sgst")
                    .HasColumnType("numeric");

                entity.Property(e => e.Taxable)
                    .HasColumnName("taxable")
                    .HasColumnType("numeric");

                entity.HasOne(d => d.Anx)
                    .WithMany(p => p.Trananxdet)
                    .HasForeignKey(d => d.AnxId)
                    .HasConstraintName("trananxdet_anx_id_fkey");
            });

            modelBuilder.HasSequence("tran_id_seq", "gst2019");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
