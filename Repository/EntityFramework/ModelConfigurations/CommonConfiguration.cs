using Domain;
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
    public class CommonConfiguration<T> :
        EntityTypeConfiguration<T> where T : class, IAggregateRoot
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of <c>CustomerTypeConfiguration</c> class.
        /// </summary>
        public CommonConfiguration(Action<CommonConfiguration<T>> action)
        {
            HasKey(c => c.Id);
            Property(c => c.Id)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(c => c.Rid)
                .IsRequired();
            if (action != null)
                action(this);
            //Property(c => c.ProjectNo)
            //    .IsRequired()
            //    .HasMaxLength(36);
            //Property(c => c.Name)
            //    .IsRequired()
            //    .HasMaxLength(30);
            var t = this;
            ToTable(typeof(T).Name);
        }
        #endregion
    }
}
