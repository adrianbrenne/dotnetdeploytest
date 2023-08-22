using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TestDeployDO.Models;

public partial class UniboxV1DbContext : DbContext
{
    public UniboxV1DbContext()
    {
    }

    public UniboxV1DbContext(DbContextOptions<UniboxV1DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountsWithdrawalLog> AccountsWithdrawalLogs { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BagCode> BagCodes { get; set; }

    public virtual DbSet<PantLog> PantLogs { get; set; }

    public virtual DbSet<PantLogsBag> PantLogsBags { get; set; }

    public virtual DbSet<PantLogsTimestamp> PantLogsTimestamps { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfilesType> ProfilesTypes { get; set; }

    public virtual DbSet<ProfilesTypesAdmin> ProfilesTypesAdmins { get; set; }

    public virtual DbSet<ProfilesTypesEnterprise> ProfilesTypesEnterprises { get; set; }

    public virtual DbSet<ProfilesTypesPrivate> ProfilesTypesPrivates { get; set; }

    public virtual DbSet<Unibox> Uniboxes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User Id=postgres;Password=Peghes-xipwan-9ficpu1;Server=db.gnkswhfiuclyvnxvwtrj.supabase.co;Port=5432;Database=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn" })
            .HasPostgresEnum("pant_status", new[] { "delivered", "pickup", "complete" })
            .HasPostgresEnum("pgsodium", "key_status", new[] { "default", "valid", "invalid", "expired" })
            .HasPostgresEnum("pgsodium", "key_type", new[] { "aead-ietf", "aead-det", "hmacsha512", "hmacsha256", "auth", "shorthash", "generichash", "kdf", "secretbox", "secretstream", "stream_xchacha20" })
            .HasPostgresEnum("profile_type", new[] { "private", "enterprise", "admin" })
            .HasPostgresEnum("unibox_v1", "pant_status", new[] { "delivered", "pickup", "complete" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "pgjwt")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("pgsodium", "pgsodium")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("accounts_pkey");

            entity.ToTable("accounts", "unibox_v1");

            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.PantCode)
                .HasColumnType("character varying")
                .HasColumnName("pant_code");
        });

        modelBuilder.Entity<AccountsWithdrawalLog>(entity =>
        {
            entity.HasKey(e => e.WithdrawalId).HasName("accounts_withdrawal_logs_pkey");

            entity.ToTable("accounts_withdrawal_logs", "unibox_v1");

            entity.Property(e => e.WithdrawalId).HasColumnName("withdrawal_id");
            entity.Property(e => e.AccountId)
                .ValueGeneratedOnAdd()
                .HasColumnName("account_id");
            entity.Property(e => e.BankAccountNumber)
                .HasColumnType("character varying")
                .HasColumnName("bank_account_number");
            entity.Property(e => e.MoneyRequested).HasColumnName("money_requested");
            entity.Property(e => e.MoneyTransferred).HasColumnName("money_transferred");
            entity.Property(e => e.PhoneNr)
                .HasColumnType("character varying")
                .HasColumnName("phone_nr");
            entity.Property(e => e.TimeRequested)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time_requested");
            entity.Property(e => e.TimeTransferred)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("time_transferred");
            entity.Property(e => e.Transferred).HasColumnName("transferred");
            entity.Property(e => e.Uid).HasColumnName("uid");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountsWithdrawalLogs)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("accounts_withdrawal_logs_account_id_fkey");

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.AccountsWithdrawalLogs)
                .HasForeignKey(d => d.Uid)
                .HasConstraintName("accounts_withdrawal_logs_uid_fkey");
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("addresses_pkey");

            entity.ToTable("addresses", "unibox_v1");

            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.City)
                .HasColumnType("character varying")
                .HasColumnName("city");
            entity.Property(e => e.HouseholdCount).HasColumnName("household_count");
            entity.Property(e => e.PostCode)
                .HasColumnType("character varying")
                .HasColumnName("post_code");
            entity.Property(e => e.Street)
                .HasColumnType("character varying")
                .HasColumnName("street");
        });

        modelBuilder.Entity<BagCode>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("bag_codes_pkey");

            entity.ToTable("bag_codes", "unibox_v1");

            entity.Property(e => e.Code)
                .HasColumnType("character varying")
                .HasColumnName("code");
            entity.Property(e => e.Uid).HasColumnName("uid");
        });

        modelBuilder.Entity<PantLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("pant_logs_pkey");

            entity.ToTable("pant_logs", "unibox_v1");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.Handler).HasColumnName("handler");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_updated");
            entity.Property(e => e.RatingNumber).HasColumnName("rating_number");
            entity.Property(e => e.RatingText).HasColumnName("rating_text");
            entity.Property(e => e.TimestampId).HasColumnName("timestamp_id");
            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.UniboxId).HasColumnName("unibox_id");

            entity.HasOne(d => d.HandlerNavigation).WithMany(p => p.PantLogs)
                .HasForeignKey(d => d.Handler)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("pant_logs_handler_fkey");

            entity.HasOne(d => d.Timestamp).WithMany(p => p.PantLogs)
                .HasForeignKey(d => d.TimestampId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pant_logs_timestamp_id_fkey");

            entity.HasOne(d => d.UidNavigation).WithMany(p => p.PantLogs)
                .HasForeignKey(d => d.Uid)
                .HasConstraintName("pant_logs_uid_fkey");

            entity.HasOne(d => d.Unibox).WithMany(p => p.PantLogs)
                .HasForeignKey(d => d.UniboxId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pant_logs_unibox_id_fkey");
        });

        modelBuilder.Entity<PantLogsBag>(entity =>
        {
            entity.HasKey(e => e.BagId).HasName("pant_logs_bags_pkey");

            entity.ToTable("pant_logs_bags", "unibox_v1");

            entity.Property(e => e.BagId).HasColumnName("bag_id");
            entity.Property(e => e.BagCode)
                .HasColumnType("character varying")
                .HasColumnName("bag_code");
            entity.Property(e => e.Kr).HasColumnName("kr");
            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.UnitsNegligible).HasColumnName("units_negligible");
            entity.Property(e => e.UnitsOk).HasColumnName("units_ok");

            entity.HasOne(d => d.Log).WithMany(p => p.PantLogsBags)
                .HasForeignKey(d => d.LogId)
                .HasConstraintName("pant_logs_bags_log_id_fkey");
        });

        modelBuilder.Entity<PantLogsTimestamp>(entity =>
        {
            entity.HasKey(e => e.TimestampsId).HasName("pant_logs_timestamps_pkey");

            entity.ToTable("pant_logs_timestamps", "unibox_v1");

            entity.Property(e => e.TimestampsId).HasColumnName("timestamps_id");
            entity.Property(e => e.Completed)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completed");
            entity.Property(e => e.Delivered)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("delivered");
            entity.Property(e => e.Pickup)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("pickup");
            entity.Property(e => e.Start)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("profiles_pkey");

            entity.ToTable("profiles", "unibox_v1");

            entity.Property(e => e.Uid)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("uid");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ProfilesTypesId).HasColumnName("profiles_types_id");
            entity.Property(e => e.UniboxId).HasColumnName("unibox_id");

            entity.HasOne(d => d.Account).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profiles_account_id_fkey");

            entity.HasOne(d => d.ProfilesTypes).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.ProfilesTypesId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("profiles_profiles_types_id_fkey");

            entity.HasOne(d => d.Unibox).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.UniboxId)
                .HasConstraintName("profiles_unibox_id_fkey");
        });

        modelBuilder.Entity<ProfilesType>(entity =>
        {
            entity.HasKey(e => e.ProfilesTypesId).HasName("profiles_types_pkey");

            entity.ToTable("profiles_types", "unibox_v1");

            entity.Property(e => e.ProfilesTypesId).HasColumnName("profiles_types_id");
        });

        modelBuilder.Entity<ProfilesTypesAdmin>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("admins_pkey");

            entity.ToTable("profiles_types_admin", "unibox_v1");

            entity.HasIndex(e => e.Email, "admins_email_key").IsUnique();

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("uid");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.NameFamily)
                .HasColumnType("character varying")
                .HasColumnName("name_family");
            entity.Property(e => e.NameGiven)
                .HasColumnType("character varying")
                .HasColumnName("name_given");
            entity.Property(e => e.PhonePrivate)
                .HasColumnType("character varying")
                .HasColumnName("phone_private");
        });

        modelBuilder.Entity<ProfilesTypesEnterprise>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("profiles_types_enterprise_pkey");

            entity.ToTable("profiles_types_enterprise", "unibox_v1", tb => tb.HasComment("Enterprise profiles parameters"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("uid");
            entity.Property(e => e.BankAccountNumber)
                .HasColumnType("character varying")
                .HasColumnName("bank_account_number");
            entity.Property(e => e.ContactEmail)
                .HasColumnType("character varying")
                .HasColumnName("contact_email");
            entity.Property(e => e.ContactName)
                .HasColumnType("character varying")
                .HasColumnName("contact_name");
            entity.Property(e => e.ContactPhone)
                .HasColumnType("character varying")
                .HasColumnName("contact_phone");
            entity.Property(e => e.OrgName)
                .HasColumnType("character varying")
                .HasColumnName("org_name");
            entity.Property(e => e.OrgNr)
                .HasColumnType("character varying")
                .HasColumnName("org_nr");

            entity.HasOne(d => d.UidNavigation).WithOne(p => p.ProfilesTypesEnterprise)
                .HasForeignKey<ProfilesTypesEnterprise>(d => d.Uid)
                .HasConstraintName("profiles_types_enterprise_uid_fkey");
        });

        modelBuilder.Entity<ProfilesTypesPrivate>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("profiles_types_private_pkey");

            entity.ToTable("profiles_types_private", "unibox_v1", tb => tb.HasComment("Private profile parameters"));

            entity.Property(e => e.Uid)
                .ValueGeneratedNever()
                .HasColumnName("uid");
            entity.Property(e => e.BirthYear)
                .HasColumnType("character varying")
                .HasColumnName("birth_year");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasColumnType("character varying")
                .HasColumnName("gender");
            entity.Property(e => e.HouseholdCount)
                .HasDefaultValueSql("'1'::smallint")
                .HasColumnName("household_count");
            entity.Property(e => e.NameFamily)
                .HasColumnType("character varying")
                .HasColumnName("name_family");
            entity.Property(e => e.NameGiven)
                .HasColumnType("character varying")
                .HasColumnName("name_given");
            entity.Property(e => e.PhonePrivate)
                .HasColumnType("character varying")
                .HasColumnName("phone_private");

            entity.HasOne(d => d.UidNavigation).WithOne(p => p.ProfilesTypesPrivate)
                .HasForeignKey<ProfilesTypesPrivate>(d => d.Uid)
                .HasConstraintName("profiles_types_private_uid_fkey");
        });

        modelBuilder.Entity<Unibox>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("uniboxes_pkey");

            entity.ToTable("uniboxes", "unibox_v1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BagLimit).HasColumnName("bag_limit");
            entity.Property(e => e.City)
                .HasColumnType("character varying")
                .HasColumnName("city");
            entity.Property(e => e.IdentifierName)
                .HasColumnType("character varying")
                .HasColumnName("identifier_name");
            entity.Property(e => e.PostalCode)
                .HasColumnType("character varying")
                .HasColumnName("postal_code");
            entity.Property(e => e.Street)
                .HasColumnType("character varying")
                .HasColumnName("street");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
