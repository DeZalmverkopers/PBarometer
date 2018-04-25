﻿using Domain.Bericht;
using Domain.Dashboards;
using Domain.Deelplatformen;
using Domain.Gebruikers;
using Domain.Gemonitordeitems;
using Domain.IdentityFramework;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL.EF
{
  [DbConfigurationType(typeof(DbConfiguration))]
  public class DbContext : IdentityDbContext<ApplicationUser>
  {
    private readonly bool delaySave;

    //Database Sets
    public DbSet<Bericht> Berichten { get; set; }

    public DbSet<Gebruiker> Gebruikers { get; set; }

    public DbSet<Alert> Alerts { get; set; }
    public DbSet<Dashboard> Dashboards { get; set; }
    public DbSet<Grafiek> Grafieken { get; set; }
    public DbSet<Deelplatform> Deelplatformen { get; set; }

    public DbSet<GemonitordItem> GemonitordeItems { get; set; }
    public DbSet<DetailItem> DetailItems { get; set; }

    public DbContext(bool unitOfWorkPresent = false) : base("PBDb_Barometer")
    {
      delaySave = unitOfWorkPresent;
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      //base.OnModelCreating(modelBuilder);
      //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
      modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

      //Tabelnamen
      modelBuilder.Entity<Alert>().ToTable("Alerts");
      modelBuilder.Entity<Bericht>().ToTable("Berichten");
      modelBuilder.Entity<Dashboard>().ToTable("Dashboards");
      modelBuilder.Entity<Grafiek>().ToTable("Grafieken");
      modelBuilder.Entity<Gebruiker>().ToTable("Gebruikers");
      modelBuilder.Entity<DetailItem>().ToTable("DetailItems");
      modelBuilder.Entity<GemonitordItem>().ToTable("GemonitordeItems");
      modelBuilder.Entity<ItemHistoriek>().ToTable("ItemHistorieken");

      modelBuilder.Entity<IdentityUserLogin>().HasKey(l => l.UserId);
      modelBuilder.Entity<IdentityRole>().HasKey(r => r.Id);
      modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

      //Foreign keys
      //modelBuilder.Entity<Alert>().HasRequired(alert => alert.Gebruiker).WithMany(gebruiker => gebruiker.Alerts);
      //modelBuilder.Entity<Gebruiker>().HasMany(gebruiker => gebruiker.Alerts).WithRequired(alert => alert.Gebruiker);

      //  modelBuilder.Entity<Alert>().HasRequired(alert => alert.GemonitordItem).WithMany(gemonitordItem => gemonitordItem.Alerts);
      //  modelBuilder.Entity<GemonitordItem>().HasMany(gemonitordItem => gemonitordItem.Alerts).WithRequired(alert => alert.GemonitordItem);
      modelBuilder.Entity<Gebruiker>().HasOptional(gebruiker => gebruiker.Dashboard).WithRequired(dashboard => dashboard.Gebruiker);

      //  modelBuilder.Entity<Dashboard>().HasMany(dashboard => dashboard.Grafieken).WithRequired(grafiek => grafiek.Dashboard);
      //  modelBuilder.Entity<Grafiek>().HasRequired(grafiek => grafiek.Dashboard).WithMany(dashboard => dashboard.Grafieken);

      //  modelBuilder.Entity<Grafiek>().HasMany(grafiek => grafiek.GemonitordeItems).WithMany(gemonitordItem => gemonitordItem.Grafieken);
      //  modelBuilder.Entity<GemonitordItem>().HasMany(gemonitordItem => gemonitordItem.Grafieken).WithMany(grafiek => grafiek.GemonitordeItems);

      //modelBuilder.Entity<GemonitordItem>().HasMany(gemonitordItem => gemonitordItem.DetailItems).WithRequired(detailItem => detailItem.GemonitordeItems);
      //  modelBuilder.Entity<DetailItem>().HasRequired(detailItem => detailItem.GemonitordItem).WithMany(gemonitordItem => gemonitordItem.DetailItems);

      //  modelBuilder.Entity<GemonitordItem>().HasMany(gemonitordItem => gemonitordItem.ItemHistorieken).WithRequired(itemHistoriek => itemHistoriek.GemonitordItem);
      //  modelBuilder.Entity<ItemHistoriek>().HasRequired(itemHistoriek => itemHistoriek.GemonitordItem).WithMany(gemonitordItem => gemonitordItem.ItemHistorieken);
    }

    //public override int SaveChanges()
    //{
    //  if (delaySave) return -1;
    //  return base.SaveChanges();
    //}

    internal int CommitChanges()
    {
      if (delaySave)
      {
        return base.SaveChanges();
      }
      throw new InvalidOperationException("No UnitOfWork present, use SaveChanges instead");
    }
  }
}