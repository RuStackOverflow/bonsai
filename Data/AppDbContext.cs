﻿using Bonsai.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bonsai.Data
{
    /// <summary>
    /// Main data context of the application.
    /// </summary>
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<AppConfig> Config => Set<AppConfig>();
        public virtual DbSet<Changeset> Changes => Set<Changeset>();
        public virtual DbSet<Media> Media => Set<Media>();
        public virtual DbSet<MediaTag> MediaTags => Set<MediaTag>();
        public virtual DbSet<MediaEncodingJob> MediaJobs => Set<MediaEncodingJob>();
        public virtual DbSet<Page> Pages => Set<Page>();
        public virtual DbSet<PageAlias> PageAliases => Set<PageAlias>();
        public virtual DbSet<Relation> Relations => Set<Relation>();
        public virtual DbSet<PageDraft> PageDrafts => Set<PageDraft>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().HasMany(x => x.Changes).WithOne(x => x.Author);
            builder.Entity<AppUser>().HasOne(x => x.Page).WithMany().IsRequired(false).HasForeignKey(x => x.PageId);

            builder.Entity<Changeset>().HasOne(x => x.Author).WithMany();

            builder.Entity<Page>().HasIndex(x => x.Key).IsUnique(true);
            builder.Entity<Page>().HasIndex(x => x.IsDeleted).IsUnique(false);
            builder.Entity<Page>().HasMany(x => x.Aliases).WithOne(x => x.Page).IsRequired();
            builder.Entity<Page>().HasOne(x => x.MainPhoto).WithMany().IsRequired(false).HasForeignKey(x => x.MainPhotoId);

            builder.Entity<PageAlias>().HasIndex(x => x.Key).IsUnique(true);

            builder.Entity<PageDraft>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).IsRequired(true);
            builder.Entity<PageDraft>().HasIndex(x => x.PageId).IsUnique(false);

            builder.Entity<Relation>().HasOne(x => x.Source).WithMany(x => x.Relations).HasForeignKey(x => x.SourceId);
            builder.Entity<Relation>().HasOne(x => x.Destination).WithMany().HasForeignKey(x => x.DestinationId);
            builder.Entity<Relation>().HasOne(x => x.Event).WithMany().HasForeignKey(x => x.EventId);
            builder.Entity<Relation>().HasIndex(x => x.IsComplementary).IsUnique(false);
            builder.Entity<Relation>().HasIndex(x => x.IsDeleted).IsUnique(false);

            builder.Entity<Media>().HasOne(x => x.Uploader).WithMany().IsRequired(false);
            builder.Entity<Media>().HasIndex(x => x.Key).IsUnique(true);
            builder.Entity<Media>().HasIndex(x => x.IsDeleted).IsUnique(false);

            builder.Entity<MediaEncodingJob>().HasOne(x => x.Media).WithOne().HasForeignKey<MediaEncodingJob>(x => x.MediaId).IsRequired(true);

            builder.Entity<MediaTag>().HasOne(x => x.Media).WithMany(x => x.Tags);
            builder.Entity<MediaTag>().HasOne(x => x.Object).WithMany(x => x.MediaTags);
        }
    }
}
