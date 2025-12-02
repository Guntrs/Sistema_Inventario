using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SistemaFacturacion.Entidades.Almacen;

namespace SistemaFacturacion.Datos.Mapping.Almacen
{
     public class DetalleIngresoMap : IEntityTypeConfiguration<DetalleIngreso>
    {
        public void Configure(EntityTypeBuilder<DetalleIngreso> builder)
        {
            builder.ToTable("DetalleIngreso")
                 .HasKey(d => d.IdDetalleIngreso);
        }
    }
}
