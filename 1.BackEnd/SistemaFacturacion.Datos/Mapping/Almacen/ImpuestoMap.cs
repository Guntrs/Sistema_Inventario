using SistemaFacturacion.Entidades.Almacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFacturacion.Datos.Mapping.Almacen
{
    public class ImpuestoMap : IEntityTypeConfiguration<Impuesto>
    {
        public void Configure(EntityTypeBuilder<Impuesto> builder)
        {
            builder.ToTable("Impuesto")
                .HasKey(c => c.IdImpuesto);
            builder.Property(c => c.TipoDocumento)
                .HasMaxLength(50);
            builder.Property(c => c.Descripcion)
                .HasMaxLength(100);
        }


        
    }
}
