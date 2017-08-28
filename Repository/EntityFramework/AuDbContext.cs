﻿using Domain.Model;
using Repository.EntityFramework.ModelConfigurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Repository.EntityFramework
{
    /// <summary>
    /// 表示专用于AuDbContext案例的数据访问上下文。
    /// </summary>
    public sealed class AuDbContext : DbContext
    {
        #region Ctor
        /// <summary>
        /// 构造函数，初始化一个新的<c>ByteartRetailDbContext</c>实例。
        /// </summary>
        public AuDbContext()
            : base("Audb")
        {
            //Database.SetInitializer<AuDbContext>(null);
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets a set of <c>User</c>s.
        /// </summary>
        public DbSet<Project> Projects
        {
            get { return Set<Project>(); }
        }
                 
        #endregion

        #region Protected Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Configurations
                .Add(new ProjectConfiguration());
               
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
