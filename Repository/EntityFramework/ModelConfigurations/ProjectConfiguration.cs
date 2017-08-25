using Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Repository.EntityFramework.ModelConfigurations
{
    /// <summary>
    /// Represents the entity type configuration for the <see cref="Customer"/> entity.
    /// </summary>
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>CustomerTypeConfiguration</c> class.
        /// </summary>
        public ProjectConfiguration()
        {
            HasKey(c => c.ID);
            Property(c => c.ID)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.ProjectNo)
                .IsRequired()
                .HasMaxLength(36);
            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(30);
           

            ToTable("Project");
        }
        #endregion
    }
}
