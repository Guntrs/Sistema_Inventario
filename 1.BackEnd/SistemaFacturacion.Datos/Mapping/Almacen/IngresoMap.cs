using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SistemaFacturacion.Entidades.Almacen;

namespace SistemaFacturacion.Datos.Mapping.Almacen
{
    public class IngresoMap : IEntityTypeConfiguration<Ingreso>
    {
        public void Configure(EntityTypeBuilder<Ingreso> builder)
        {
            builder.ToTable("Ingreso")
                .HasKey(i => i.IdIngreso);

            //como sera la relacion persona y proveedor
            builder.HasOne(i => i.persona)
                .WithMany(p => p.ingresos)
                .HasForeignKey(i => i.IdProveedor);
        }
    }
}
